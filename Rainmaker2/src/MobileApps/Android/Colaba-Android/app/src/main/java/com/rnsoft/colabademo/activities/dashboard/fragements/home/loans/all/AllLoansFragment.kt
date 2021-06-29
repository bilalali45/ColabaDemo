package com.rnsoft.colabademo.activities.dashboard.fragements.home.loans.all


import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.activities.dashboard.fragements.home.loans.LoanItemClickListener
import com.rnsoft.colabademo.activities.dashboard.fragements.home.loans.LoansAdapter
import com.rnsoft.colabademo.activities.dashboard.fragements.home.loans.SheetBottomBorrowerCardFragment
import com.rnsoft.colabademo.databinding.FragmentLoanBinding
import org.json.JSONArray


class AllLoansFragment : Fragment(), LoanItemClickListener {
    private var _binding: FragmentLoanBinding? = null
    private val binding get() = _binding!!

    private var loanRecycleView: RecyclerView? = null
    private var covidData: JSONArray? = null
    private lateinit var  borrowList: List<Borrower>

    //private val recyclerView: RecyclerView? = null

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        _binding = FragmentLoanBinding.inflate(inflater, container, false)
        val view = binding.root

        loanRecycleView = view.findViewById<RecyclerView>(R.id.loan_recycler_view)
        loanRecycleView?.apply {
            // set a LinearLayoutManager to handle Android
            // RecyclerView behavior
            this.layoutManager = LinearLayoutManager(activity)
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            // set the custom adapter to the RecyclerView
            borrowList = Borrower.customersList(requireContext())
            this.adapter = LoansAdapter(borrowList, this@AllLoansFragment)

        }




        //val mAdapter: RecyclerView.Adapter<LoansAdapter.LoanViewHolder> = LoansAdapter(covidData)
        //loanRecycleView?.setAdapter(mAdapter)

        return view
    }


      override fun onViewCreated(itemView: View, savedInstanceState: Bundle?) {
        super.onViewCreated(itemView, savedInstanceState)

    }


    override fun getCardIndex(position: Int) {
        val borrowerBottomSheet = SheetBottomBorrowerCardFragment.newInstance()
        val bundle = Bundle()
        bundle.putParcelable(AppConstant.borrowerParcelObject, borrowList.get(position))
        borrowerBottomSheet.arguments = bundle
        borrowerBottomSheet.show(childFragmentManager, SheetBottomBorrowerCardFragment::class.java.canonicalName)
    }

    //fun StatsFragment(data: JSONArray?) { covidData = data}





}