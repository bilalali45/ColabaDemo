package com.rnsoft.colabademo.activities.dashboard.fragements.home.loans

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.Borrower
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.FragmentBirdBinding


class InActiveLoansFragment : Fragment() , LoanItemClickListener {
    private var _binding: FragmentBirdBinding? = null
    private val binding get() = _binding!!
    private var inActiveLoanRecyclerView: RecyclerView? = null
    private lateinit var  borrowList: List<Borrower>

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        _binding = FragmentBirdBinding.inflate(inflater, container, false)
        val view = binding.root

        inActiveLoanRecyclerView = view.findViewById<RecyclerView>(R.id.inactive_loan_recycler_view)
        inActiveLoanRecyclerView?.apply {
            // set a LinearLayoutManager to handle Android
            // RecyclerView behavior
            this.layoutManager = LinearLayoutManager(activity)
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            // set the custom adapter to the RecyclerView
            borrowList = Borrower.emptyCustomersList(requireContext())
            this.adapter = LoansAdapter(borrowList, this@InActiveLoansFragment)

        }

        return view
    }

    override fun getCardIndex(position: Int) {

    }
}