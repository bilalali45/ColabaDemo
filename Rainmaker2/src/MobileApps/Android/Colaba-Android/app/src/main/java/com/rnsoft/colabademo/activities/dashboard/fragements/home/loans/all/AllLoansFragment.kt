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
import com.rnsoft.colabademo.databinding.FragmentLoanBinding
import com.rnsoft.colabademo.databinding.UnmarriedLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList


@AndroidEntryPoint
class AllLoansFragment : LoanBaseFragment(), AdapterClickListener, LoanFilterInterface {
    private var _binding: FragmentLoanBinding? = null
    private val binding get() = _binding!!
    private lateinit var loansAdapter: LoansAdapter
    private var shimmerContainer: ShimmerFrameLayout? = null
    private var rowLoading: ProgressBar? = null
    //private var loanRecycleView: RecyclerView? = null
    private val linearLayoutManager = LinearLayoutManager(activity)
    private var allLoansArrayList: ArrayList<LoanItem> = ArrayList()
    private var oldListDisplaying: Boolean = false
    private val pageSize: Int = 20
    private val loanFilter: Int = 0
    private var pageNumber: Int = 1
    @Inject
    lateinit var sharedPreferences: SharedPreferences


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        //super.onCreateView(inflater, container, savedInstanceState)
        _binding = FragmentLoanBinding.inflate(inflater, container, false)
        val view: View = binding.root

        rowLoading = view.findViewById(R.id.loan_row_loader)
        //loanRecycleView = view.findViewById(R.id.loan_recycler_view)
        loansAdapter = LoansAdapter(allLoansArrayList, this@AllLoansFragment)
        binding.loanRecyclerView?.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            //loansAdapter = LoansAdapter(allLoansArrayList, this@AllLoansFragment)
            this.adapter = loansAdapter
        }

        shimmerContainer =
            view.findViewById(R.id.shimmer_view_container) as ShimmerFrameLayout
        shimmerContainer?.startShimmer()


        loanViewModel.allLoansArrayList.observe(viewLifecycleOwner, {
            //val result = it ?: return@Observer
            //container.stopShimmer()
            //container.isVisible = false
            //loading.visibility = View.INVISIBLE
            rowLoading?.visibility = View.INVISIBLE
            if (it.size > 0) {
                shimmerContainer?.stopShimmer()
                shimmerContainer?.isVisible = false
                if (oldListDisplaying) {
                    oldListDisplaying = false
                    allLoansArrayList.clear()
                    Log.e("oldListDisplaying = ", "set to false")

                    //scrollListener.resetState()
                    //loanRecycleView?.removeOnScrollListener(scrollListener)
                    //loansAdapter.notifyDataSetChanged()
                }
                //loanRecycleView?.adapter = loansAdapter
                val lastSize = allLoansArrayList.size
                allLoansArrayList.addAll(it)

                binding.loanRecyclerView.addOnScrollListener(scrollListener)
                loansAdapter.notifyDataSetChanged()
                //loansAdapter.notifyItemRangeInserted(lastSize,lastSize+allLoansArrayList.size-1 )
            } else {
                Log.e("no-record", " found....")

                shimmerContainer?.stopShimmer()
                shimmerContainer?.isVisible = false
                if (pageNumber == 1) {
                    allLoansArrayList.clear()
                    binding.loanRecyclerView.addOnScrollListener(scrollListener)
                    loansAdapter.notifyDataSetChanged()
                }
            }
        })


        // Adds the scroll listener to RecyclerView
        binding.loanRecyclerView.addOnScrollListener(scrollListener)

        return view
    }


    /*
    private var loading = true
    private  var pastVisiblesItems:Int = 0
    private  var visibleItemCount :Int = 0
    private  var totalItemCount:Int = 0
    val scrollListenerSecond = object : RecyclerView.OnScrollListener() {
        override fun onScrolled(recyclerView: RecyclerView, dx: Int, dy: Int) {
            if (dy > 0) { //check for scroll down
                visibleItemCount = linearLayoutManager.getChildCount()
                totalItemCount = linearLayoutManager.getItemCount()
                pastVisiblesItems = linearLayoutManager.findFirstVisibleItemPosition();
                if (loading) {
                    if ((visibleItemCount + pastVisiblesItems) >= totalItemCount) {
                        loading = false;
                        Log.e("keep-loading--", "Last Item Wow !");
                        // Do pagination.. i.e. fetch new data
                        loading = true;
                    }
                }
            }
        }
    }
    */


    //private val scrollLinearLayout = LinearLayoutManager(activity, LinearLayoutManager.VERTICAL, false)
    val scrollListener = object : EndlessRecyclerViewScrollListener(linearLayoutManager) {
        override fun onLoadMore(page: Int, totalItemsCount: Int, view: RecyclerView?) {
            // Triggered only when new data needs to be appended to the list
            // Add whatever code is needed to append new items to the bottom of the list
            Log.e("calling--", "scroller--")
            rowLoading?.visibility = View.VISIBLE
            pageNumber++
            loadLoanApplications()
        }
    }

    override fun getSingleItemIndex(position: Int) {
        val borrowerBottomSheet = SheetBottomBorrowerCardFragment.newInstance()
        val bundle = Bundle()
        bundle.putParcelable(AppConstant.borrowerParcelObject, allLoansArrayList[position])
        borrowerBottomSheet.arguments = bundle
        borrowerBottomSheet.show(
            childFragmentManager,
            SheetBottomBorrowerCardFragment::class.java.canonicalName
        )
    }

    override fun navigateTo(position: Int) {
        Log.e("LoansFragment", "navigateTo")
        if (allLoansArrayList.size >= position) {
            val borrowerDetailIntent = Intent(requireActivity(), DetailActivity::class.java)
            val test = allLoansArrayList[position]
            Log.e("Before", test.loanApplicationId.toString())
            //borrowerDetailIntent.putExtra(AppConstant.borrowerParcelObject, allLoansArrayList[position])
            borrowerDetailIntent.putExtra(AppConstant.loanApplicationId, test.loanApplicationId)
            borrowerDetailIntent.putExtra(AppConstant.loanPurpose, test.loanPurpose)
            borrowerDetailIntent.putExtra(AppConstant.firstName, test.firstName)
            //borrowerDetailIntent.putExtra(AppConstant.firstName, test.)
            borrowerDetailIntent.putExtra(AppConstant.lastName, test.lastName)
            borrowerDetailIntent.putExtra(AppConstant.bPhoneNumber, test.cellNumber)
            borrowerDetailIntent.putExtra(AppConstant.bEmail, test.email)

            startActivity(borrowerDetailIntent)
        }
    }

    override fun onResume() {
        super.onResume()
        tabSwitched()
    }

    private fun tabSwitched() {
        rowLoading?.visibility = View.INVISIBLE
        //Log.e("onResume - ALL LOAN ", "$oldListDisplaying")
        allLoansArrayList.clear()
        //loansAdapter.notifyDataSetChanged()
        pageNumber = 1
        //loadDataFromCache()
        loadLoanApplications()
    }

    private fun loadLoanApplications() {
        // now load data from internet if connected...
        if (NetworkSetting.isNetworkAvailable(requireContext())) {
            //Log.e("ALL-LOAN--", "globalAssignToMe = $globalAssignToMe")
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (AppSetting.loanApiDateTime.isEmpty())
                    AppSetting.loanApiDateTime =
                        SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(
                            Date()
                        )
                //Log.e("Why-", AppSetting.loanApiDateTime)
                //Log.e("pageNumber-", "$pageNumber and page size = $pageSize")

                loanViewModel.getAllLoans(
                    token = authToken,
                    dateTime = AppSetting.loanApiDateTime, pageNumber = pageNumber,
                    pageSize = pageSize, loanFilter = loanFilter,
                    orderBy = globalOrderBy, assignedToMe = globalAssignToMe
                )
            }
        } else {
            rowLoading?.visibility = View.INVISIBLE
        }
    }

    private fun loadDataFromCache() {
        // load data from cache first...
        if (!NetworkSetting.isNetworkAvailable(requireContext())) {
            val token: TypeToken<ArrayList<LoanItem>> = object : TypeToken<ArrayList<LoanItem>>() {}
            if (sharedPreferences.contains(AppConstant.oldLoans)) {
                sharedPreferences.getString(AppConstant.oldLoans, "")?.let { oldLoans ->
                    val oldJsonList: ArrayList<LoanItem> = Gson().fromJson(oldLoans, token.type)
                    if (oldJsonList.size > 1) oldJsonList.removeAt(oldJsonList.size - 1)
                    if (oldJsonList.size > 1) oldJsonList.removeAt(oldJsonList.size - 1)
                    if (oldJsonList.size > 1) oldJsonList.removeAt(oldJsonList.size - 1)
                    //Log.e("oldJsonList-", oldJsonList.toString())
                    shimmerContainer?.stopShimmer()
                    shimmerContainer?.isVisible = false
                    oldListDisplaying = true
                    allLoansArrayList.clear()
                    allLoansArrayList.addAll(oldJsonList)
                    loansAdapter.notifyDataSetChanged()
                    //loanRecycleView?.removeOnScrollListener(scrollListener)
                }
            }
        }
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
        if (event.isInternetError)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG)
        else
            if (event.errorResult != null) {
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG)
            }
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onTabSwitched(event: OnTabSwitchedEvent) {
        if (event.loanFilter == loanFilter) {
            Timber.e(" Running from ALL-Loans fragment...")
            tabSwitched()
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
        //Log.e("setAssignToMe = ", passedAssignToMe.toString())
        allLoansArrayList.clear()
        if(loansAdapter != null) {
            loansAdapter.notifyDataSetChanged()
        } else {
            Log.e("LoanAdapter in NULL", "true")
            loansAdapter = LoansAdapter(allLoansArrayList, this@AllLoansFragment)
            loansAdapter.notifyDataSetChanged()
        }
        globalAssignToMe = passedAssignToMe
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
