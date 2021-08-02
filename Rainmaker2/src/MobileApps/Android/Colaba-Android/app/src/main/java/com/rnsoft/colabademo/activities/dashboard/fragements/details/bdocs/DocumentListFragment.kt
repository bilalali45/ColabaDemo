package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import com.rnsoft.colabademo.databinding.DetailListLayoutBinding

import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class DocumentListFragment : Fragment(), AdapterClickListener {

    private var _binding: DetailListLayoutBinding? = null
    private val binding get() = _binding!!

    private lateinit var docsRecycler: RecyclerView
    private var docsArrayList: ArrayList<SubFiles> = ArrayList()
    private lateinit var documentListAdapter: DocumentListAdapter

    private var download_id: String? = null
    private var download_requestId: String? = null
    private var download_docId: String? = null

    private val detailViewModel: DetailViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = DetailListLayoutBinding.inflate(inflater, container, false)
        val view: View = binding.root

        docsRecycler = view.findViewById(R.id.docs_detail_list_recycle_view)
        val linearLayoutManager = LinearLayoutManager(activity)

        lifecycleScope.launchWhenStarted {
            val filesNames = arguments?.getString(AppConstant.innerFilesName)
            download_id = arguments?.getString(AppConstant.download_id).toString()
            download_requestId = arguments?.getString(AppConstant.download_requestId).toString()
            download_docId = arguments?.getString(AppConstant.download_docId).toString()

            filesNames?.let {
                val token: TypeToken<ArrayList<SubFiles>> =
                    object : TypeToken<ArrayList<SubFiles>>() {}
                docsArrayList.clear()
                docsArrayList = Gson().fromJson(it, token.type)
            }
            documentListAdapter = DocumentListAdapter(docsArrayList, this@DocumentListFragment)
            docsRecycler.apply {
                this.layoutManager = linearLayoutManager
                this.setHasFixedSize(true)
                this.adapter = documentListAdapter
                documentListAdapter.notifyDataSetChanged()
            }
        }

        binding.backButton.setOnClickListener{
           findNavController().popBackStack()
        }
        return view
    }

    override fun getCardIndex(position: Int) {

    }

    override fun navigateTo(position: Int) {
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            val selectedFile = docsArrayList[position]
            /*
            if (download_docId != null && download_requestId != null && download_id != null)
                detailViewModel.downloadFile(
                    token = authToken,
                    id = download_id!!,
                    requestId = download_requestId!!,
                    docId = download_docId!!,
                    fileId = selectedFile.id
                )

             */
        }
    }

}