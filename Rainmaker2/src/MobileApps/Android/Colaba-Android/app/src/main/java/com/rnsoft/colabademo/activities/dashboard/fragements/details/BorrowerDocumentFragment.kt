package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.facebook.shimmer.ShimmerFrameLayout
import com.rnsoft.colabademo.databinding.BorrowerDocLayoutBinding

import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerDocumentFragment : Fragment() , LoanItemClickListener  {

    private var _binding: BorrowerDocLayoutBinding? = null
    private val binding get() = _binding!!

    private lateinit var docsRecycler: RecyclerView
    private var docsArrayList: ArrayList<DocItem> = ArrayList()
    private lateinit var docsAdapter: DocsAdapter
    private var shimmerContainer: ShimmerFrameLayout?=null

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = BorrowerDocLayoutBinding.inflate(inflater, container, false)
        val view: View = binding.root

        shimmerContainer = view.findViewById(R.id.shimmer_view_container) as ShimmerFrameLayout
        shimmerContainer?.startShimmer()

        docsRecycler = view.findViewById(R.id.docs_recycle_view)

        val linearLayoutManager = LinearLayoutManager(activity)
        docsArrayList = DocItem.testDocList()
        docsAdapter = DocsAdapter(docsArrayList, this@BorrowerDocumentFragment)
        docsRecycler.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            this.adapter = docsAdapter
        }

        return view

    }

    override fun getCardIndex(position: Int) {

    }

    override fun navigateCardToDetailActivity(position: Int) {

    }

}