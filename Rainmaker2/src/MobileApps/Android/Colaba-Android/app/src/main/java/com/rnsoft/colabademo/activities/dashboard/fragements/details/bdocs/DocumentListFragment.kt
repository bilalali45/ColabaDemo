package com.rnsoft.colabademo

import android.Manifest
import android.content.Intent
import android.content.SharedPreferences
import android.content.pm.PackageManager
import android.net.Uri
import android.os.Build
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.app.ActivityCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.RecyclerView
import com.google.gson.Gson
import com.rnsoft.colabademo.activities.dashboard.DocViewerActivity
import com.rnsoft.colabademo.databinding.DetailListLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import java.io.File
import javax.inject.Inject


@AndroidEntryPoint
class DocumentListFragment : Fragment(), AdapterClickListener {
    private var _binding: DetailListLayoutBinding? = null
    private val binding get() = _binding!!
    private val detailViewModel: DetailViewModel by activityViewModels()
    private lateinit var docsRecycler: RecyclerView
    private var docsArrayList: ArrayList<SubFiles> = ArrayList()
    private lateinit var documentListAdapter: DocumentListAdapter
    private var download_id: String? = null
    private var download_requestId: String? = null
    private var download_docId: String? = null
    private var doc_name: String? = null
    private var doc_message: String? = null
    lateinit var tvDocName: TextView
    lateinit var tvDocMsg: TextView
    lateinit var doc_msg_layout: ConstraintLayout
    lateinit var layoutNoDocUploaded: ConstraintLayout

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
        tvDocName = view.findViewById(R.id.doc_type_name)
        tvDocMsg = view.findViewById(R.id.doc_msg)
        doc_msg_layout = view.findViewById(R.id.layout_doc_msg)
        layoutNoDocUploaded = view.findViewById(R.id.layout_no_doc_upload)

        lifecycleScope.launchWhenStarted {

            doc_name = arguments?.getString(AppConstant.docName)
            doc_message = arguments?.getString(AppConstant.docMessage)
            docsArrayList = arguments?.getParcelableArrayList(AppConstant.docObject)!!
            //val filesNames = arguments?.getString(AppConstant.innerFilesName)
            download_id = arguments?.getString(AppConstant.download_id).toString()
            download_requestId = arguments?.getString(AppConstant.download_requestId).toString()
            download_docId = arguments?.getString(AppConstant.download_docId).toString()

            // set name and message
            tvDocName.text = doc_name
            if (doc_message?.isNotEmpty() == true) {
                tvDocMsg.text = doc_message
                doc_msg_layout.visibility = View.VISIBLE
            } else {
                doc_msg_layout.visibility = View.GONE
            }

             // populate recyclerview
            if (docsArrayList.size > 0) {
                docsRecycler.visibility = View.VISIBLE
                layoutNoDocUploaded.visibility = View.GONE

                documentListAdapter =
                    DocumentListAdapter(docsArrayList, this@DocumentListFragment)
                docsRecycler.apply {
                    this.setHasFixedSize(true)
                    this.adapter = documentListAdapter
                    documentListAdapter.notifyDataSetChanged()
                }
            } else {
                docsRecycler.visibility = View.GONE
                layoutNoDocUploaded.visibility = View.VISIBLE
            }
        }

        detailViewModel.fileName.observe(viewLifecycleOwner, {
           if(!it.isNullOrBlank() && !it.isNullOrEmpty()){
               redirectToPdfFragment(it)
           }
        })

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }
        return view
    }

    override fun navigateTo(position: Int) {
        //Log.e("param", " downloadId: " + download_id + " downRequeId: " + download_requestId + " downDocId: " + download_docId)
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            val selectedFile = docsArrayList[position]
            if (download_docId != null && download_requestId != null && download_id != null) {
                detailViewModel.downloadFile(
                    token = authToken,
                    id = download_id!!,
                    requestId = download_requestId!!,
                    docId = download_docId!!,
                    fileId = selectedFile.id
                )
            }
        }
    }

    override fun getCardIndex(position: Int) {

    }

    private fun redirectToPdfFragment(pdfFileName:String){
        val pdfViewFragment = PdfViewFragment()
        val bundle = Bundle()
        bundle.putString(AppConstant.pdfFileName, pdfFileName)
        pdfViewFragment.arguments = bundle
        findNavController().navigate(R.id.pdf_view_fragment_id, pdfViewFragment.arguments)
    }

}