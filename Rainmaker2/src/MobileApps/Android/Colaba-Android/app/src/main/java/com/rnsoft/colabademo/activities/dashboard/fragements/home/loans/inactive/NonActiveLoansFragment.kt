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
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
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
class NonActiveLoansFragment : Fragment() , LoanItemClickListener , LoanFilterInterface {
    private var _binding: NonActiveFragmentBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences


    private val loanViewModel: LoanViewModel by activityViewModels()
    private lateinit var nonActiveRecycler: RecyclerView
    private var nonActiveLoansList: ArrayList<LoanItem> = ArrayList()
    private lateinit var nonActiveAdapter: LoansAdapter
    private lateinit var loading: ProgressBar
    ////////////////////////////////////////////////////////////////////////////
    //private var stringDateTime: String = ""
    private var pageNumber: Int = 1
    private var pageSize: Int = 20
    private var loanFilter: Int = 2
    private var orderBy: Int = 0
    private var assignedToMe: Boolean = false

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        _binding = NonActiveFragmentBinding.inflate(inflater, container, false)
        val view = binding.root
        loading = view.findViewById(R.id.non_active_loan_loader)
        nonActiveRecycler = view.findViewById(R.id.inactive_loan_recycler_view)
        val linearLayoutManager = LinearLayoutManager(activity)
        nonActiveAdapter = LoansAdapter(nonActiveLoansList, this@NonActiveLoansFragment)
        nonActiveRecycler.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            this.adapter =nonActiveAdapter
        }

        loading.visibility = View.VISIBLE
        loanViewModel.nonActiveLoansArrayList.observe(viewLifecycleOwner, Observer {
            loading.visibility = View.INVISIBLE
            if(it.size>0) {

                val lastSize = nonActiveLoansList.size
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
                pageNumber++
                loadNonActiveApplications()
            }
        }
        nonActiveRecycler.addOnScrollListener(scrollListener)

        loadNonActiveApplications()

        return view
    }

    private fun loadNonActiveApplications(){
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.nonActiveloanApiDateTime.isEmpty())
                AppSetting.nonActiveloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())

            loanViewModel.getNonActiveLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.nonActiveloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
        }
    }

    override fun getCardIndex(position: Int){}

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
                nonActiveLoansList.clear()
                nonActiveAdapter.notifyDataSetChanged()
            }
        }
    }

    override fun setOrderId(orderId: Int) {

    }

    override fun setAssignToMe(assignToMe: Int) {
        Log.e("setAssignToMe = ", assignToMe.toString())
        nonActiveLoansList.clear()
        nonActiveAdapter.notifyDataSetChanged()
        loading.visibility = View.VISIBLE
    }
}