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
import com.rnsoft.colabademo.databinding.FragmentLoanBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList


@AndroidEntryPoint
class AllLoansFragment : BaseFragment(), AdapterClickListener ,  LoanFilterInterface {
    private var _binding: FragmentLoanBinding? = null
    private val binding get() = _binding!!

    private lateinit var loansAdapter: LoansAdapter
    private var shimmerContainer: ShimmerFrameLayout? = null
    private var rowLoading: ProgressBar?=null
    private var loanRecycleView: RecyclerView? = null
    private  var allLoansArrayList: ArrayList<LoanItem> = ArrayList()

    ////////////////////////////////////////////////////////////////////////////
    private val pageSize: Int = 20
    private val loanFilter: Int = 0

    private var pageNumber: Int = 1
    //private var orderBy: Int = 0
    //private var assignedToMe: Boolean = false

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?): View {
        super.onCreateView(inflater, container, savedInstanceState)
        _binding = FragmentLoanBinding.inflate(inflater, container, false)
        val view = binding.root

        rowLoading = view.findViewById(R.id.loan_row_loader)
        loanRecycleView = view.findViewById(R.id.loan_recycler_view)
        val linearLayoutManager = LinearLayoutManager(activity)
        loansAdapter = LoansAdapter(allLoansArrayList , this@AllLoansFragment)
        loanRecycleView?.apply {
            // set a LinearLayoutManager to handle Android
            // RecyclerView behavior
            this.layoutManager =linearLayoutManager
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            // set the custom adapter to the RecyclerView
            //borrowList = Borrower.customersList(requireContext())

            //this.adapter = loansAdapter
           //loansAdapter = LoansAdapter(allLoansArrayList , this@AllLoansFragment)
        }

        shimmerContainer = view.findViewById(R.id.shimmer_view_container) as ShimmerFrameLayout
        shimmerContainer?.startShimmer()

        /*
        viewLifecycleOwner.lifecycleScope.launchWhenStarted{
            val serviceCompleted = withContext(Dispatchers.IO) {
                sharedPreferences.getString(AppConstant.token,"")?.let{
                    loanViewModel.getAllLoans(it)
                }
            }
        }
        */

        //val ceptype: Type = object : TypeToken<ArrayList<LoanItem?>?>() {}.type


        val token: TypeToken<ArrayList<LoanItem>> = object : TypeToken<ArrayList<LoanItem>>() {}
        val gson = Gson()
        if(sharedPreferences.contains(AppConstant.oldLoans)) {
            sharedPreferences.getString(AppConstant.oldLoans, "")?.let { oldLoans ->
                //val list: List<LoanItem> = gson.fromJson(oldLoans, ceptype)
                //Log.e("convered-", list.toString())
                val oldJsonList: ArrayList<LoanItem> = gson.fromJson(oldLoans, token.type)
                Log.e("oldJsonList-", oldJsonList.toString())
                val oldAdapter = LoansAdapter(oldJsonList , this@AllLoansFragment)
                loanRecycleView?.adapter = oldAdapter
                oldAdapter.notifyDataSetChanged()
                shimmerContainer?.stopShimmer()
                shimmerContainer?.isVisible = false
            }
        }




        //loading.visibility = View.VISIBLE

        loanViewModel.allLoansArrayList.observe(viewLifecycleOwner, {
            //val result = it ?: return@Observer
            //container.stopShimmer()
            //container.isVisible = false
            //loading.visibility = View.INVISIBLE
            rowLoading?.visibility = View.INVISIBLE
            if(it.size>0) {
                shimmerContainer?.stopShimmer()
                shimmerContainer?.isVisible = false
                loanRecycleView?.adapter = loansAdapter
                val lastSize = allLoansArrayList.size
                allLoansArrayList.addAll(it)
                loansAdapter.notifyDataSetChanged()
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
                loadLoanApplications()
            }
        }





        // Adds the scroll listener to RecyclerView
        // Adds the scroll listener to RecyclerView
        loanRecycleView?.addOnScrollListener(scrollListener)


        /*
        loanViewModel.databaseLoansArrayList.observe(viewLifecycleOwner, {
            rowLoading?.visibility = View.INVISIBLE
            shimmerContainer.stopShimmer()
            shimmerContainer.isVisible = false
            allLoansArrayList.addAll(it)
            loansAdapter.notifyDataSetChanged()
            loanViewModel.databaseLoansArrayList.removeObservers(this)
        })
        */

        //if(hasLoanApiDataLoaded)
        //loadDataFromDatabase(loanFilter)

        //loadLoanApplications()


        return view
    }

    override fun getCardIndex(position: Int) {
        val borrowerBottomSheet = SheetBottomBorrowerCardFragment.newInstance()
        val bundle = Bundle()
        bundle.putParcelable(AppConstant.borrowerParcelObject, allLoansArrayList[position])
        borrowerBottomSheet.arguments = bundle
        borrowerBottomSheet.show(childFragmentManager, SheetBottomBorrowerCardFragment::class.java.canonicalName)
    }

    override fun navigateTo(position: Int) {
        val borrowerDetailIntent= Intent(requireActivity(), DetailActivity::class.java)
        val test = allLoansArrayList[position]
        Log.e("Before" , test.loanApplicationId.toString())
        //borrowerDetailIntent.putExtra(AppConstant.borrowerParcelObject, allLoansArrayList[position])
        borrowerDetailIntent.putExtra(AppConstant.loanApplicationId,  test.loanApplicationId)
        borrowerDetailIntent.putExtra(AppConstant.loanPurpose,  test.loanPurpose)
        borrowerDetailIntent.putExtra(AppConstant.firstName,  test.firstName)
        borrowerDetailIntent.putExtra(AppConstant.lastName,  test.lastName)
        startActivity(borrowerDetailIntent)

    }

    private fun loadLoanApplications() {
        //loading.visibility = View.VISIBLE
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.loanApiDateTime.isEmpty())
                AppSetting.loanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())
            Log.e("Why-", AppSetting.loanApiDateTime)
            Log.e("pageNumber-", "$pageNumber and page size = $pageSize")

            loanViewModel.getAllLoans(
                token = authToken,
                dateTime = AppSetting.loanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = globalOrderBy, assignedToMe = globalAssignToMe
            )
        }
    }


    override fun onResume() {
        super.onResume()
        rowLoading?.visibility = View.INVISIBLE
        allLoansArrayList.clear()
        loansAdapter.notifyDataSetChanged()
        pageNumber = 1
        loadLoanApplications()
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
        if(event.errorResult!=null){
            SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )
        }
    }

    override fun setOrderId(passedOrderBy: Int) {
        allLoansArrayList.clear()
        loansAdapter.notifyDataSetChanged()
        // = passedOrderBy
        globalOrderBy = passedOrderBy
        pageNumber = 1
        loadLoanApplications()
    }

    override fun setAssignToMe(passedAssignToMe: Boolean) {
        Log.e("setAssignToMe = ", passedAssignToMe.toString())
        allLoansArrayList.clear()
        loansAdapter.notifyDataSetChanged()
        globalAssignToMe = passedAssignToMe
        //assignedToMe = passedAssignToMe
        pageNumber = 1
        loadLoanApplications()
    }


}

    /*
    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        viewLifecycleOwner.launch{
           val serviceCompleted = withContext(Dispatchers.IO) {
               sharedPreferences.getString(AppConstant.token,"")?.let{
                   loanViewModel.getAllLoans(it)
               }
           }
        }
    }
    */
