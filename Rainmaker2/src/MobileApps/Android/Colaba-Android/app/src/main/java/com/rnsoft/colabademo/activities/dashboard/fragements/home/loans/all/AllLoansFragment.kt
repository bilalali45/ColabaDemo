package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.activities.dashboard.fragements.home.FilterBottomSheetInterface
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
class AllLoansFragment : Fragment(), LoanItemClickListener ,  FilterBottomSheetInterface {
    private var _binding: FragmentLoanBinding? = null
    private val binding get() = _binding!!
    private val loanViewModel: LoanViewModel by activityViewModels()
    private lateinit var loansAdapter: LoansAdapter
    private lateinit var loading: ProgressBar
    private lateinit var rowLoading: ProgressBar
    private var loanRecycleView: RecyclerView? = null
    private  var allLoansArrayList: ArrayList<LoanItem> = ArrayList()

    ////////////////////////////////////////////////////////////////////////////
    //private var stringDateTime: String = ""
    private var pageNumber: Int = 1
    private var pageSize: Int = 10
    private var loanFilter: Int = 0
    private var orderBy: Int = 0
    private var assignedToMe: Boolean = false

    //private var borrowerListEnded = false

    @Inject
    lateinit var sharedPreferences: SharedPreferences



    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentLoanBinding.inflate(inflater, container, false)
        val view = binding.root

        loading = view.findViewById(R.id.loader_all_loan)
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

            this.adapter = loansAdapter
           //loansAdapter = LoansAdapter(allLoansArrayList , this@AllLoansFragment)
        }

        /*
        viewLifecycleOwner.lifecycleScope.launchWhenStarted{
            val serviceCompleted = withContext(Dispatchers.IO) {
                sharedPreferences.getString(AppConstant.token,"")?.let{
                    loanViewModel.getAllLoans(it)
                }
            }
        }
        */


        loading.visibility = View.VISIBLE

        loanViewModel.allLoansArrayList.observe(viewLifecycleOwner, {
            //val result = it ?: return@Observer
            loading.visibility = View.INVISIBLE
            rowLoading.visibility = View.INVISIBLE
            if(it.size>0) {
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
                rowLoading.visibility = View.VISIBLE
                pageNumber++
                loadLoanApplications()
            }
        }

        /*
        val scrollListener2 = object : RecyclerView.OnScrollListener() {
            override fun onScrolled(view: RecyclerView, dx: Int, dy: Int) {
                // Triggered only when new data needs to be appended to the list
                // Add whatever code is needed to append new items to the bottom of the list
                loadLoanApplications()
            }
        }

         */



        // Adds the scroll listener to RecyclerView
        // Adds the scroll listener to RecyclerView
        loanRecycleView?.addOnScrollListener(scrollListener)

        loadLoanApplications()

        return view
    }

    override fun getCardIndex(position: Int) {
        val borrowerBottomSheet = SheetBottomBorrowerCardFragment.newInstance()
        val bundle = Bundle()
        bundle.putParcelable(AppConstant.borrowerParcelObject, allLoansArrayList[position])
        borrowerBottomSheet.arguments = bundle
        borrowerBottomSheet.show(childFragmentManager, SheetBottomBorrowerCardFragment::class.java.canonicalName)
    }

    private fun loadLoanApplications() {
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.loanApiDateTime.isEmpty())
                AppSetting.loanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())
            Log.e("Why-", AppSetting.loanApiDateTime)
            Log.e("pageNumber-", pageNumber.toString() +" and page size = "+pageSize)
            loanViewModel.getAllLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.loanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
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
    fun onClearEvent(event: AllLoansLoadedEvent) {
        loading.visibility = View.VISIBLE
        event.allLoansArrayList?.let {
            if (it.size == 0) {
                allLoansArrayList.clear()
                loansAdapter.notifyDataSetChanged()
            }
        }
    }

    override fun setOrderId(orderId: Int) {

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
