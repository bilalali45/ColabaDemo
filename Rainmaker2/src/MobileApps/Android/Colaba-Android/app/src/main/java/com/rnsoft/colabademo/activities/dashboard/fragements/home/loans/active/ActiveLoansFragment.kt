package com.rnsoft.colabademo.activities.dashboard.fragements.home.loans.active

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.Borrower
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.activities.dashboard.fragements.home.loans.LoanItemClickListener
import com.rnsoft.colabademo.activities.dashboard.fragements.home.loans.LoansAdapter
import com.rnsoft.colabademo.databinding.FragmentDogBinding

class ActiveLoansFragment : Fragment() , LoanItemClickListener {
    private var _binding: FragmentDogBinding? = null
    private val binding get() = _binding!!
    private var activeRecycler: RecyclerView? = null
    private lateinit var  borrowList: List<Borrower>

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        _binding = FragmentDogBinding.inflate(inflater, container, false)
        val view = binding.root

        activeRecycler = view.findViewById<RecyclerView>(R.id.active_recycler)
        activeRecycler?.apply {
            // set a LinearLayoutManager to handle Android
            // RecyclerView behavior
            this.layoutManager = LinearLayoutManager(activity)
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            // set the custom adapter to the RecyclerView
            borrowList = Borrower.customersList(requireContext())
            this.adapter = LoansAdapter(borrowList, this@ActiveLoansFragment)

        }

        return view
    }

    override fun getCardIndex(position: Int) {

    }
}