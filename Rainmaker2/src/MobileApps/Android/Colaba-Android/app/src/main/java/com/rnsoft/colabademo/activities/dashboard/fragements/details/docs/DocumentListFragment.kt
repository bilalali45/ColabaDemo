package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.databinding.DetailListLayoutBinding

import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class DocumentListFragment : Fragment() , LoanItemClickListener {

    private var _binding: DetailListLayoutBinding? = null
    private val binding get() = _binding!!

    private lateinit var docsRecycler: RecyclerView
    private var docsArrayList: ArrayList<DocItem> = ArrayList()
    private lateinit var documentListAdapter: DocumentListAdapter

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = DetailListLayoutBinding.inflate(inflater, container, false)
        val view: View = binding.root

        val docNames =  arguments?.getParcelable<LoanItem>(AppConstant.docNames)
        docNames?.let {
            //binding.borrowerName.text = it.firstName
        }
        docsRecycler = view.findViewById(R.id.docs_detail_list_recycle_view)
        val linearLayoutManager = LinearLayoutManager(activity)
        docsArrayList = DocItem.testDocList()
        documentListAdapter = DocumentListAdapter(docsArrayList, this@DocumentListFragment)
        docsRecycler.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            this.adapter = documentListAdapter
        }

        return view

    }

    override fun getCardIndex(position: Int) {

    }

    override fun navigateCardToDetailActivity(position: Int) {

    }

}