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
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.facebook.shimmer.ShimmerFrameLayout
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import com.rnsoft.colabademo.activities.dashboard.fragements.home.BaseFragment
import com.rnsoft.colabademo.databinding.NonActiveFragmentBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

@AndroidEntryPoint
class NonActiveLoansFragment : BaseFragment() , AdapterClickListener , LoanFilterInterface {
    private var _binding: NonActiveFragmentBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences


    private var rowLoading: ProgressBar? = null
    private lateinit var nonActiveRecycler: RecyclerView
    private var nonActiveLoansList: ArrayList<LoanItem> = ArrayList()
    private lateinit var nonActiveAdapter: LoansAdapter
    private var shimmerContainer: ShimmerFrameLayout?=null
    //private lateinit var loading: ProgressBar
    ////////////////////////////////////////////////////////////////////////////
    //private var stringDateTime: String = ""

    private val pageSize: Int = 20
    private val loanFilter: Int = 2

    private var pageNumber: Int = 1
    //private var orderBy: Int = 0
    //private var assignedToMe: Boolean = false

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        _binding = NonActiveFragmentBinding.inflate(inflater, container, false)
        val view = binding.root
        shimmerContainer = view.findViewById(R.id.shimmer_view_container) as ShimmerFrameLayout
        shimmerContainer?.startShimmer()
        //loading = view.findViewById(R.id.non_active_loan_loader)

        rowLoading = view.findViewById(R.id.non_active_row_loader)

        nonActiveRecycler = view.findViewById(R.id.inactive_loan_recycler_view)
        val linearLayoutManager = LinearLayoutManager(activity)
        nonActiveAdapter = LoansAdapter(nonActiveLoansList, this@NonActiveLoansFragment)
        nonActiveRecycler.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            this.adapter =nonActiveAdapter
        }



        val token: TypeToken<ArrayList<LoanItem>> = object : TypeToken<ArrayList<LoanItem>>() {}
        val gson = Gson()
        if(sharedPreferences.contains(AppConstant.oldNonActiveLoans)) {
            sharedPreferences.getString(AppConstant.oldNonActiveLoans, "")?.let { oldLoans ->
                //val list: List<LoanItem> = gson.fromJson(oldLoans, ceptype)
                //Log.e("convered-", list.toString())
                val oldJsonList: ArrayList<LoanItem> = gson.fromJson(oldLoans, token.type)
                Log.e("oldJsonList-", oldJsonList.toString())
                val oldAdapter = LoansAdapter(oldJsonList , this@NonActiveLoansFragment)
                nonActiveRecycler.adapter = oldAdapter
                oldAdapter.notifyDataSetChanged()
                shimmerContainer?.stopShimmer()
                shimmerContainer?.isVisible = false
            }
        }



        //loading.visibility = View.VISIBLE
        loanViewModel.nonActiveLoansArrayList.observe(viewLifecycleOwner, {
            rowLoading?.visibility = View.INVISIBLE
            if(it.size>0) {
                shimmerContainer?.stopShimmer()
                shimmerContainer?.isVisible = false

                nonActiveLoansList.size
                nonActiveRecycler.adapter = nonActiveAdapter
                nonActiveLoansList.addAll(it)
                nonActiveAdapter.notifyDataSetChanged()
                // loansAdapter.notifyItemRangeInserted(lastSize,lastSize+allLoansArrayList.size-1 )
            }
            else
                Log.e("should-stop"," here....")
        })

        val scrollListener = object : EndlessRecyclerViewScrollListener(linearLayoutManager) {
            override fun onLoadMore(page: Int, totalItemsCount: Int, view: RecyclerView?) {
                // Triggered only when new data needs to be appended to the list
                // Add whatever code is needed to append new items to the bottom of the list
                rowLoading?.visibility = View.VISIBLE
                pageNumber++
                loadNonActiveApplications()
            }
        }
        nonActiveRecycler.addOnScrollListener(scrollListener)


        loadDataFromDatabase(loanFilter)

        //loadNonActiveApplications()

        return view
    }

    private fun loadNonActiveApplications(){
        //loading.visibility = View.VISIBLE
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.nonActiveloanApiDateTime.isEmpty())
                AppSetting.nonActiveloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())


            loanViewModel.getNonActiveLoans(
                token = authToken,
                dateTime = AppSetting.nonActiveloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = globalOrderBy, assignedToMe = globalAssignToMe
            )
        }
    }



    override fun getCardIndex(position: Int) {
        val borrowerBottomSheet = SheetBottomBorrowerCardFragment.newInstance()
        val bundle = Bundle()
        bundle.putParcelable(AppConstant.borrowerParcelObject, nonActiveLoansList[position])
        borrowerBottomSheet.arguments = bundle
        borrowerBottomSheet.show(childFragmentManager, SheetBottomBorrowerCardFragment::class.java.canonicalName)
    }

    override fun navigateTo(position: Int) {
        startActivity(Intent(requireActivity(), DetailActivity::class.java))
        //requireActivity().finish()
    }


    override fun onResume() {
        super.onResume()
        rowLoading?.visibility = View.INVISIBLE
        nonActiveLoansList.clear()
        nonActiveAdapter.notifyDataSetChanged()
        pageNumber = 1
        loadNonActiveApplications()
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
        nonActiveLoansList.clear()
        nonActiveAdapter.notifyDataSetChanged()
        //orderBy = passedOrderBy
        globalOrderBy = passedOrderBy
        pageNumber = 1
        loadNonActiveApplications()
    }

    override fun setAssignToMe(passedAssignToMe: Boolean) {
        Log.e("setAssignToMe = ", passedAssignToMe.toString())
        nonActiveLoansList.clear()
        nonActiveAdapter.notifyDataSetChanged()
        globalAssignToMe = passedAssignToMe
        //assignedToMe = passedAssignToMe
        pageNumber = 1
        loadNonActiveApplications()
    }
}