package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
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
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

@AndroidEntryPoint
class NonActiveLoansFragment : Fragment() , LoanItemClickListener {
    private var _binding: NonActiveFragmentBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences


    private val loanViewModel: LoanViewModel by activityViewModels()
    private var nonActiveRecycler: RecyclerView? = null
    private lateinit var nonActiveLoansList: ArrayList<LoanItem>
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
        nonActiveRecycler?.apply {
            this.layoutManager = LinearLayoutManager(activity)
            this.setHasFixedSize(true)
            //this.adapter = LoansAdapter(nonActiveLoansList, this@NonActiveLoansFragment)
        }

        loading.visibility = View.VISIBLE
        loanViewModel.nonActiveLoansArrayList.observe(viewLifecycleOwner, Observer {
            loading.visibility = View.INVISIBLE
            nonActiveLoansList = it
            nonActiveAdapter = LoansAdapter(it, this@NonActiveLoansFragment)
            nonActiveRecycler?.adapter = nonActiveAdapter
            nonActiveAdapter.notifyDataSetChanged()
        })

        loadNonActiveApplications()

        return view
    }

    private fun loadNonActiveApplications(){
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.nonActiveloanApiDateTime.isEmpty())
                AppSetting.nonActiveloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())

            loanViewModel.getNonActiveLoans(
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIzODA2NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtb2JpbGV1c2VyMUBtYWlsaW5hdG9yLmNvbSIsIkZpcnN0TmFtZSI6Ik1vYmlsZSIsIkxhc3ROYW1lIjoiVXNlcjEiLCJUZW5hbnRDb2RlIjoibGVuZG92YSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJNQ1UiLCJMb2FuIE9mZmljZXIiXSwiZXhwIjoxNjI1Mjc3NDg4LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.nzloUYocTjEOWCpFEzX0uGrI3DHCwVIQLYbZjDDSBvI",
                dateTime = AppSetting.nonActiveloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
        }
    }

    override fun getCardIndex(position: Int){}
}