package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.facebook.shimmer.ShimmerFrameLayout
import com.google.gson.Gson
import com.rnsoft.colabademo.databinding.BorrowerDocLayoutBinding


import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerDocumentFragment : Fragment() , AdapterClickListener {

    private var _binding: BorrowerDocLayoutBinding? = null
    private val binding get() = _binding!!

    private lateinit var docsRecycler: RecyclerView
    private var docsArrayList: ArrayList<BorrowerDocsModel> = ArrayList()
    private lateinit var borrowerDocumentAdapter: BorrowerDocumentAdapter
    private var shimmerContainer: ShimmerFrameLayout?=null
    lateinit var btnAll :Button

    private val detailViewModel: DetailViewModel by activityViewModels()

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

        borrowerDocumentAdapter = BorrowerDocumentAdapter(docsArrayList, this@BorrowerDocumentFragment)
        docsRecycler.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            this.adapter = borrowerDocumentAdapter
        }

        detailViewModel.borrowerDocsModelList.observe(viewLifecycleOwner, {
            if (it != null && it.size>0) {
                docsArrayList = it
                borrowerDocumentAdapter = BorrowerDocumentAdapter(docsArrayList, this@BorrowerDocumentFragment)
                docsRecycler.adapter = borrowerDocumentAdapter
                borrowerDocumentAdapter.notifyDataSetChanged()

                Log.e("list", "coming-$it")
            } else
                Log.e("else-stop", " borrowerDocsModelList not available....")
        })

        return view

    }

    override fun getCardIndex(position: Int) {

    }

    override fun navigateTo(position: Int) {
        val selectedDocumentType = docsArrayList[position]
        if (selectedDocumentType.subFiles.size > 0) {
            val listFragment = DocumentListFragment()
            val bundle = Bundle()
            Log.e("subFiles","- "+selectedDocumentType.subFiles.toString())
            val fileNames = Gson().toJson(selectedDocumentType.subFiles)
            bundle.putString(AppConstant.innerFilesName, fileNames)
            bundle.putString(AppConstant.download_id, selectedDocumentType.id)
            bundle.putString(AppConstant.download_requestId, selectedDocumentType.requestId)
            bundle.putString(AppConstant.download_docId, selectedDocumentType.docId)
            listFragment.arguments = bundle
            findNavController().navigate(R.id.docs_list_inner_fragment, bundle)


        }
    }


}