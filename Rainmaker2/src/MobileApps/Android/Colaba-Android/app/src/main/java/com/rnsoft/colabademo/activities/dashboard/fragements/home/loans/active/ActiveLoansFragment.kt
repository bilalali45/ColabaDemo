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
import com.rnsoft.colabademo.activities.dashboard.fragements.home.loans.LoansAdapter
import com.rnsoft.colabademo.databinding.ActiveLoanFragmentBinding
import dagger.hilt.android.AndroidEntryPoint
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

@AndroidEntryPoint
class ActiveLoansFragment : Fragment() , LoanItemClickListener {
    private var _binding: ActiveLoanFragmentBinding? = null
    private val binding get() = _binding!!

    private val loanViewModel: LoanViewModel by activityViewModels()
    private var activeRecycler: RecyclerView? = null
    private lateinit var activeLoansList: ArrayList<LoanItem>
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

        activeRecycler?.apply {
            this.layoutManager = LinearLayoutManager(activity)
            this.setHasFixedSize(true)
           // this.adapter = LoansAdapter(activeLoansList, this@ActiveLoansFragment)
        }

        activeAdapter = LoansAdapter(ArrayList(), this@ActiveLoansFragment)
        activeRecycler?.apply {
            this.layoutManager = LinearLayoutManager(activity)
            this.setHasFixedSize(true)
            this.adapter = activeAdapter

        }

        loading.visibility = View.VISIBLE

        loanViewModel.allLoansArrayList.observe(viewLifecycleOwner, Observer {
            //val result = it ?: return@Observer
            loading.visibility = View.INVISIBLE
            activeLoansList = it
            activeAdapter = LoansAdapter(it, this@ActiveLoansFragment)
            activeRecycler?.adapter = activeAdapter
            activeAdapter.notifyDataSetChanged()
        })

        loadActiveApplications()


        return view
    }

    private fun loadActiveApplications() {
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            val stringDateTime =
                SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())
            loanViewModel.getAllLoans(
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIzODA2NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtb2JpbGV1c2VyMUBtYWlsaW5hdG9yLmNvbSIsIkZpcnN0TmFtZSI6Ik1vYmlsZSIsIkxhc3ROYW1lIjoiVXNlcjEiLCJUZW5hbnRDb2RlIjoibGVuZG92YSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJNQ1UiLCJMb2FuIE9mZmljZXIiXSwiZXhwIjoxNjI1Mjc3NDg4LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.nzloUYocTjEOWCpFEzX0uGrI3DHCwVIQLYbZjDDSBvI",
                dateTime = stringDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
        }
    }

    override fun getCardIndex(position: Int) {

    }
}