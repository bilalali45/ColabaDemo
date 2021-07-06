package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
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

    private val loanViewModel: LoanViewModel by activityViewModels()
    private lateinit var activeRecycler: RecyclerView
    private var activeLoansList: ArrayList<LoanItem> = ArrayList()
    private lateinit var activeAdapter: LoansAdapter
    private lateinit var loading: ProgressBar

    ////////////////////////////////////////////////////////////////////////////
    private var pageNumber: Int = 1
    private var pageSize: Int = 20
    private var loanFilter: Int = 1
    private var orderBy: Int = 0
    private var assignedToMe: Boolean = false

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        _binding = ActiveLoanFragmentBinding.inflate(inflater, container, false)
        val view = binding.root

        loading = view.findViewById(R.id.active_loan_loader)
        activeRecycler = view.findViewById(R.id.active_recycler)
        val linearLayoutManager = LinearLayoutManager(activity)
        activeAdapter = LoansAdapter(activeLoansList, this@ActiveLoansFragment)
        activeRecycler.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            activeAdapter = LoansAdapter(activeLoansList, this@ActiveLoansFragment)
            this.adapter = activeAdapter

        }



        val scrollListener = object : EndlessRecyclerViewScrollListener(linearLayoutManager) {
            override fun onLoadMore(page: Int, totalItemsCount: Int, view: RecyclerView?) {
                // Triggered only when new data needs to be appended to the list
                // Add whatever code is needed to append new items to the bottom of the list
                //pageNumber++
                //loadActiveApplications()
            }
        }
        activeRecycler.addOnScrollListener(scrollListener)

        lifecycleScope.launchWhenResumed {
            loadActiveApplications()
        }


        loading.visibility = View.VISIBLE
        loanViewModel.activeLoansArrayList.observe(viewLifecycleOwner, Observer {
            //val result = it ?: return@Observer
            loading.visibility = View.INVISIBLE
            if(it.size>0) {
                //activeLoansList = it
                //val lastSize = activeLoansList.size
                activeLoansList.addAll(it)
                activeAdapter.notifyDataSetChanged()
            }
            else
                Log.e("should-stop"," here....")
        })


        return view
    }

    private fun loadActiveApplications() {
        loading.visibility = View.VISIBLE
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.activeloanApiDateTime.isEmpty())
                AppSetting.activeloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())

            loanViewModel.getActiveLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.activeloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = globalAssignToMe
            )
        }
    }

    override fun getCardIndex(position: Int) {}

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        //unregisterReceiver()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onClearEvent(event: AllLoansLoadedEvent) {
        loading.visibility = View.VISIBLE
        event.allLoansArrayList?.let {
            if (it.size == 0) {
                activeLoansList.clear()
                activeAdapter.notifyDataSetChanged()
            }
        }
    }

    override fun setOrderId(passedOrderBy: Int) {
        activeLoansList.clear()
        activeAdapter.notifyDataSetChanged()
        orderBy = passedOrderBy
        pageNumber = 1
        loadActiveApplications()
    }

    override fun setAssignToMe(passedAssignToMe: Boolean) {
        Log.e("setAssignToMe = ", passedAssignToMe.toString())
        activeLoansList.clear()
        activeAdapter.notifyDataSetChanged()
        globalAssignToMe = passedAssignToMe
        assignedToMe = passedAssignToMe
        pageNumber = 1
        loadActiveApplications()
    }


}