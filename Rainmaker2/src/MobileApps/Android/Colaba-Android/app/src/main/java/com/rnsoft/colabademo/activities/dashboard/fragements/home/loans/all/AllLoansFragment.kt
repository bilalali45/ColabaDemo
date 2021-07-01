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
import com.rnsoft.colabademo.databinding.FragmentLoanBinding
import dagger.hilt.android.AndroidEntryPoint
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList


@AndroidEntryPoint
class AllLoansFragment : Fragment(), LoanItemClickListener {
    private var _binding: FragmentLoanBinding? = null
    private val binding get() = _binding!!




    private val loanViewModel: LoanViewModel by activityViewModels()
    private lateinit var loansAdapter: LoansAdapter
    private lateinit var loading: ProgressBar
    private var loanRecycleView: RecyclerView? = null
    private  var allLoansArrayList: ArrayList<LoanItem> = ArrayList()

    ////////////////////////////////////////////////////////////////////////////
    //private var stringDateTime: String = ""
    private var pageNumber: Int = 1
    private var pageSize: Int = 10
    private var loanFilter: Int = 0
    private var orderBy: Int = 0
    private var assignedToMe: Boolean = false


    @Inject
    lateinit var sharedPreferences: SharedPreferences



    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentLoanBinding.inflate(inflater, container, false)
        val view = binding.root

        loading = view.findViewById(R.id.loader_all_loan)
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
            allLoansArrayList .addAll(it)
            loansAdapter = LoansAdapter(allLoansArrayList , this@AllLoansFragment)
            loanRecycleView?.adapter = loansAdapter
            loansAdapter.notifyDataSetChanged()
            //loansAdapter.notifyItemRangeInserted((allLoansArrayList.size/2),pageSize*pageNumber)
        })

        val scrollListener = object : EndlessRecyclerViewScrollListener(linearLayoutManager) {
            override fun onLoadMore(page: Int, totalItemsCount: Int, view: RecyclerView?) {
                // Triggered only when new data needs to be appended to the list
                // Add whatever code is needed to append new items to the bottom of the list
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
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIzODA2NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtb2JpbGV1c2VyMUBtYWlsaW5hdG9yLmNvbSIsIkZpcnN0TmFtZSI6Ik1vYmlsZSIsIkxhc3ROYW1lIjoiVXNlcjEiLCJUZW5hbnRDb2RlIjoibGVuZG92YSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJNQ1UiLCJMb2FuIE9mZmljZXIiXSwiZXhwIjoxNjI1MzY5MjYwLCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.gc9kQVAYwD1zelwUnFcKSDXsknJzdm_uv-RuEyEz6lI",
                dateTime = AppSetting.loanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
        }
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
