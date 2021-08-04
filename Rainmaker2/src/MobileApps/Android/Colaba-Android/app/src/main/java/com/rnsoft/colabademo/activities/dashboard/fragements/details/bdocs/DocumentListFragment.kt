package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.TextureView
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
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
    private var doc_name: String? = null
    private var doc_message: String? = null
    lateinit var tvDocName: TextView
    lateinit var tvDocMsg: TextView
    lateinit var doc_msg_layout: ConstraintLayout
    lateinit var layoutNoDocUploaded: ConstraintLayout


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


        tvDocName = view.findViewById(R.id.doc_type_name)
        tvDocMsg = view.findViewById(R.id.doc_msg)
        doc_msg_layout = view.findViewById(R.id.layout_doc_msg)
        layoutNoDocUploaded = view.findViewById(R.id.layout_no_doc_upload)
        docsRecycler = view.findViewById(R.id.docs_detail_list_recycle_view)


        lifecycleScope.launchWhenStarted {

            doc_name = arguments?.getString(AppConstant.docName)
            doc_message = arguments?.getString(AppConstant.docMessage)
            docsArrayList = arguments?.getParcelableArrayList(AppConstant.docObject)!!
            val filesNames = arguments?.getString(AppConstant.innerFilesName)
            download_id = arguments?.getString(AppConstant.download_id).toString()
            download_requestId = arguments?.getString(AppConstant.download_requestId).toString()
            download_docId = arguments?.getString(AppConstant.download_docId).toString()

            filesNames?.let {
                val token: TypeToken<ArrayList<SubFiles>> =
                    object : TypeToken<ArrayList<SubFiles>>() {}
                docsArrayList.clear()
                docsArrayList = Gson().fromJson(it, token.type)

                // set doc and msg
                tvDocName.text = doc_name

                if (doc_message?.isNotEmpty() == true) {
                    tvDocMsg.text = doc_message
                    doc_msg_layout.visibility = View.VISIBLE
                } else {
                    doc_msg_layout.visibility = View.GONE
                }
            }
        }
        populateRecyclerview()

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }
        return view
    }

    private fun populateRecyclerview() {
        val linearLayoutManager = LinearLayoutManager(activity)
        if (docsArrayList.size > 0) {
            Log.e("size","" + docsArrayList.size)
            documentListAdapter =
                DocumentListAdapter(docsArrayList, this@DocumentListFragment)
            docsRecycler.apply {
                this.layoutManager = linearLayoutManager
                this.setHasFixedSize(true)
                this.adapter = documentListAdapter
                documentListAdapter.notifyDataSetChanged()

                //layoutNoDocUploaded.visibility = View.GONE
                //docsRecycler.visibility = View.VISIBLE
            }
        } else {
            //layoutNoDocUploaded.visibility = View.VISIBLE
            //docsRecycler.visibility = View.GONE
        }
    }



    private fun showHideLayout(noDoc: Boolean, docList: Boolean, msg: Boolean) {
        if (docList && !msg) {
            layoutNoDocUploaded.visibility = View.GONE
            doc_msg_layout.visibility = View.GONE
            docsRecycler.visibility = View.VISIBLE
        }
        else if(noDoc && !docList && !msg){
            layoutNoDocUploaded.visibility = View.VISIBLE
            doc_msg_layout.visibility = View.GONE
            docsRecycler.visibility = View.GONE
        }
        else if(docList && msg){
            layoutNoDocUploaded.visibility = View.GONE
            doc_msg_layout.visibility = View.VISIBLE
            docsRecycler.visibility = View.VISIBLE
        }
    }

    override fun getCardIndex(position: Int) {

    }

    override fun navigateTo(position: Int) {
        //Log.e("param", " downloadId: " + download_id + " downRequeId: " + download_requestId + " downDocId: " + download_docId)

        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            val selectedFile = docsArrayList[position]
            if (download_docId != null && download_requestId != null && download_id != null)
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