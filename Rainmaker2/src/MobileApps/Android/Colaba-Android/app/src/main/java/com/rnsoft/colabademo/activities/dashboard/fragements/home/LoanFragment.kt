package com.rnsoft.colabademo


import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.databinding.FragmentCatBinding
import org.json.JSONArray

class LoanFragment : Fragment(), LoansAdapter.OnItemClickListener  {
    private var _binding: FragmentCatBinding? = null
    private val binding get() = _binding!!

    private var loanRecycleView: RecyclerView? = null
    private var covidData: JSONArray? = null

    //private val recyclerView: RecyclerView? = null

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        _binding = FragmentCatBinding.inflate(inflater, container, false)
        val view = binding.root

        loanRecycleView = view.findViewById<RecyclerView>(R.id.loan_recycler_view)
        loanRecycleView?.apply {
            // set a LinearLayoutManager to handle Android
            // RecyclerView behavior
            this.layoutManager = LinearLayoutManager(activity)
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            // set the custom adapter to the RecyclerView
            val borrowList = Borrower.customersList(requireContext())
            this.adapter = LoansAdapter(borrowList, this@LoanFragment)

        }




        //val mAdapter: RecyclerView.Adapter<LoansAdapter.LoanViewHolder> = LoansAdapter(covidData)
        //loanRecycleView?.setAdapter(mAdapter)

        return view
    }


      override fun onViewCreated(itemView: View, savedInstanceState: Bundle?) {
        super.onViewCreated(itemView, savedInstanceState)

    }

    override fun onItemClick(testLayout: ConstraintLayout) {
       // testLayout.visibility = View.GONE
    }

    //fun StatsFragment(data: JSONArray?) { covidData = data}





}