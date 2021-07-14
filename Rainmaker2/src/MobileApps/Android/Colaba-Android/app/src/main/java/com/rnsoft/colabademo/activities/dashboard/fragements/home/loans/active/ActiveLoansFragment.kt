package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import androidx.core.view.isVisible
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.facebook.shimmer.ShimmerFrameLayout
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import com.rnsoft.colabademo.activities.dashboard.fragements.home.BaseFragment
import com.rnsoft.colabademo.databinding.ActiveLoanFragmentBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

@AndroidEntryPoint
class ActiveLoansFragment : BaseFragment() , LoanItemClickListener  ,  LoanFilterInterface {
    private var _binding: ActiveLoanFragmentBinding? = null
    private val binding get() = _binding!!


    private  var rowLoading: ProgressBar? = null
    private lateinit var activeRecycler: RecyclerView
    private var activeLoansList: ArrayList<LoanItem> = ArrayList()
    private lateinit var activeAdapter: LoansAdapter
    //private lateinit var loading: ProgressBar
    private var shimmerContainer: ShimmerFrameLayout?=null
    ////////////////////////////////////////////////////////////////////////////
    private val pageSize: Int = 20
    private val loanFilter: Int = 1

    private var pageNumber: Int = 1
    //private var orderBy: Int = 0
    //private var assignedToMe: Boolean = false

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        _binding = ActiveLoanFragmentBinding.inflate(inflater, container, false)
        val view = binding.root

        shimmerContainer = view.findViewById(R.id.shimmer_view_container) as ShimmerFrameLayout
        shimmerContainer?.startShimmer()

        rowLoading = view.findViewById(R.id.active_row_loader)

        activeRecycler = view.findViewById(R.id.active_recycler)

        val linearLayoutManager = LinearLayoutManager(activity)
        activeAdapter = LoansAdapter(activeLoansList, this@ActiveLoansFragment)
        activeRecycler.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            activeAdapter = LoansAdapter(activeLoansList, this@ActiveLoansFragment)
            this.adapter = activeAdapter
        }


        val token: TypeToken<ArrayList<LoanItem>> = object : TypeToken<ArrayList<LoanItem>>() {}
        val gson = Gson()
        if(sharedPreferences.contains(AppConstant.oldActiveLoans)) {
            sharedPreferences.getString(AppConstant.oldActiveLoans, "")?.let { oldLoans ->
                //val list: List<LoanItem> = gson.fromJson(oldLoans, ceptype)
                //Log.e("convered-", list.toString())
                val oldJsonList: ArrayList<LoanItem> = gson.fromJson(oldLoans, token.type)
                Log.e("oldJsonList-", oldJsonList.toString())
                val oldAdapter = LoansAdapter(oldJsonList , this@ActiveLoansFragment)
                activeRecycler.adapter = oldAdapter
                oldAdapter.notifyDataSetChanged()
                shimmerContainer?.stopShimmer()
                shimmerContainer?.isVisible = false
            }
        }



        val scrollListener = object : EndlessRecyclerViewScrollListener(linearLayoutManager) {
            override fun onLoadMore(page: Int, totalItemsCount: Int, view: RecyclerView?) {
                // Triggered only when new data needs to be appended to the list
                // Add whatever code is needed to append new items to the bottom of the list
                rowLoading?.visibility = View.VISIBLE
                pageNumber++
                loadActiveApplications()
            }
        }
        activeRecycler.addOnScrollListener(scrollListener)




        //loading.visibility = View.VISIBLE
        loanViewModel.activeLoansArrayList.observe(viewLifecycleOwner, Observer {
            //val result = it ?: return@Observer
            rowLoading?.visibility = View.INVISIBLE
            if(it.size>0) {
                //activeLoansList = it
                //val lastSize = activeLoansList.size
                shimmerContainer?.stopShimmer()
                shimmerContainer?.isVisible = false
                activeRecycler.adapter = activeAdapter
                activeLoansList.addAll(it)
                activeAdapter.notifyDataSetChanged()
            }
            else
                Log.e("should-stop"," here....")
        })

        loadDataFromDatabase(loanFilter)



        return view
    }



    private fun loadActiveApplications() {
        //loading.visibility = View.VISIBLE
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.activeloanApiDateTime.isEmpty())
                AppSetting.activeloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())


            loanViewModel.getActiveLoans(
                token = authToken,
                dateTime = AppSetting.activeloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = globalOrderBy, assignedToMe = globalAssignToMe
            )
        }
    }

    override fun getCardIndex(position: Int) {
        val borrowerBottomSheet = SheetBottomBorrowerCardFragment.newInstance()
        val bundle = Bundle()
        bundle.putParcelable(AppConstant.borrowerParcelObject, activeLoansList[position])
        borrowerBottomSheet.arguments = bundle
        borrowerBottomSheet.show(childFragmentManager, SheetBottomBorrowerCardFragment::class.java.canonicalName)
    }

    override fun navigateCardToDetailActivity(position: Int) {
        startActivity(Intent(requireActivity(), DetailActivity::class.java))
        //requireActivity().finish()
    }


    override fun onResume() {
        super.onResume()
        rowLoading?.visibility = View.INVISIBLE
        activeLoansList.clear()
        activeAdapter.notifyDataSetChanged()
        pageNumber = 1
        loadActiveApplications()
    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onErrorReceived(event: WebServiceErrorEvent) {
        rowLoading?.visibility = View.INVISIBLE
        shimmerContainer?.stopShimmer()
        shimmerContainer?.isVisible = false
        if(event.isInternetError)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )
        else
        if(event.errorResult!=null)
            SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )
    }

    override fun setOrderId(passedOrderBy: Int) {
        activeLoansList.clear()
        activeAdapter.notifyDataSetChanged()
        //orderBy = passedOrderBy
        globalOrderBy = passedOrderBy
        pageNumber = 1
        loadActiveApplications()
    }

    override fun setAssignToMe(passedAssignToMe: Boolean) {
        Log.e("setAssignToMe = ", passedAssignToMe.toString())
        activeLoansList.clear()
        activeAdapter.notifyDataSetChanged()
        globalAssignToMe = passedAssignToMe
        //assignedToMe = passedAssignToMe
        pageNumber = 1
        loadActiveApplications()
    }


}