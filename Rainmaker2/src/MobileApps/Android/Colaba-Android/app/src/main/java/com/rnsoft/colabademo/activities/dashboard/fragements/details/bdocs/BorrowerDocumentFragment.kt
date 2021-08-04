package com.rnsoft.colabademo


import android.content.SharedPreferences
import android.os.Bundle
import android.os.Parcelable
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatButton
import androidx.appcompat.widget.AppCompatTextView
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
class BorrowerDocumentFragment : Fragment(), AdapterClickListener, View.OnClickListener {

    private var _binding: BorrowerDocLayoutBinding? = null
    private val binding get() = _binding!!

    private lateinit var docsRecycler: RecyclerView
    private var docsArrayList: ArrayList<BorrowerDocsModel> = ArrayList()
    lateinit var filterDocsList :ArrayList<BorrowerDocsModel>
    private lateinit var borrowerDocumentAdapter: BorrowerDocumentAdapter
    private var shimmerContainer: ShimmerFrameLayout? = null
    lateinit var btnAll: AppCompatTextView
    lateinit var btnInDraft: AppCompatTextView
    lateinit var btnToDo: AppCompatTextView
    lateinit var btnFilterStarted: AppCompatTextView
    lateinit var btnFilterPending: AppCompatTextView
    lateinit var btnFilterCompleted: AppCompatTextView
    lateinit var btnFilterManullayAdded: AppCompatTextView
    var isStart: Boolean = true
    var filter : String = "All"


    var state: Parcelable? = null

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

        borrowerDocumentAdapter =
            BorrowerDocumentAdapter(docsArrayList, this@BorrowerDocumentFragment)

        docsRecycler.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            this.adapter = borrowerDocumentAdapter
        }

        detailViewModel.borrowerDocsModelList.observe(viewLifecycleOwner, {
            if (isStart) {
                if (it != null && it.size > 0) {
                    docsArrayList = it
                    isStart = false
                    filter = AppConstant.filter_all
                    populateRecyclerview(docsArrayList)
                    //Log.e("list", "coming-$it")
                } else
                    Log.e("else-stop", " borrowerDocsModelList not available....")
            }
        })


        btnAll = view.findViewById(R.id.btn_all)
        btnAll.setOnClickListener(this)
        btnAll.isActivated = true

        btnInDraft = view.findViewById(R.id.btn_filter_indraft)
        btnInDraft.setOnClickListener(this)

        btnToDo = view.findViewById(R.id.btn_filter_todo)
        btnToDo.setOnClickListener(this)

        btnFilterStarted = view.findViewById(R.id.btn_filter_started)
        btnFilterStarted.setOnClickListener(this)

        btnFilterPending = view.findViewById(R.id.btn_filter_pending)
        btnFilterPending.setOnClickListener(this)

        btnFilterCompleted = view.findViewById(R.id.btn_filter_completed)
        btnFilterCompleted.setOnClickListener(this)

        btnFilterManullayAdded = view.findViewById(R.id.btn_filter_manullayAdded)
        btnFilterManullayAdded.setOnClickListener(this)

        return view

    }

    private fun populateRecyclerview(arrayList: ArrayList<BorrowerDocsModel>) {
        borrowerDocumentAdapter =
            BorrowerDocumentAdapter(arrayList, this@BorrowerDocumentFragment)
        docsRecycler.adapter = borrowerDocumentAdapter
        borrowerDocumentAdapter.notifyDataSetChanged()
    }

    override fun onClick(v: View) {
        when (v.id) {
            R.id.btn_all -> {
                filter = AppConstant.filter_all
                btnAll.isActivated = true
                btnInDraft.isActivated = false
                btnToDo.isActivated = false
                btnFilterStarted.isActivated = false
                btnFilterPending.isActivated = false
                btnFilterCompleted.isActivated = false
                btnFilterManullayAdded.isActivated = false
                populateRecyclerview(docsArrayList)
            }
            R.id.btn_filter_indraft -> {
                filter = AppConstant.filter_inDraft
                selectStatusFilter(
                    false, true, false, false,
                    false, false, false
                )
                getDocItems(AppConstant.filter_inDraft)
            }
            R.id.btn_filter_todo -> {
                filter = AppConstant.filter_borrower_todo
                selectStatusFilter(
                    false, false, true,
                    false, false, false, false)
                getDocItems(AppConstant.filter_borrower_todo)
            }
            R.id.btn_filter_started -> {
                filter = AppConstant.filter_started

                selectStatusFilter(
                    false, false, false,
                    true, false, false, false
                )
                getDocItems(AppConstant.filter_started)
            }
            R.id.btn_filter_pending -> {
                filter = AppConstant.filter_pending_review
                selectStatusFilter(
                    false, false, false,

                    false, true, false, false
                )
                getDocItems(AppConstant.filter_pending_review)
            }
            R.id.btn_filter_completed -> {
                filter = AppConstant.filter_completed
                selectStatusFilter(
                    false, false, false,
                    false, false, true, false
                )
                getDocItems(AppConstant.filter_completed)
            }
            R.id.btn_filter_manullayAdded -> {
                filter = AppConstant.filter_manuallyAdded
                selectStatusFilter(
                    false, false, false,
                    false, false, false, true
                )
                getDocItems(AppConstant.filter_manuallyAdded)
            }
            else -> {
            }
        }
    }

    private fun selectStatusFilter(
        statusAll: Boolean,
        statusInDraft: Boolean,
        statusToDo: Boolean,
        statusStarted: Boolean,
        statusPending: Boolean,
        statusCompleted: Boolean,
        statusManuallyAdded: Boolean
    ) {
        if (statusAll) {
            btnAll.isActivated = true
            btnInDraft.isActivated = false
            btnToDo.isActivated = false
            btnFilterStarted.isActivated = false
            btnFilterPending.isActivated = false
            btnFilterCompleted.isActivated = false
            btnFilterManullayAdded.isActivated = false
        } else if (statusInDraft) {
            btnAll.isActivated = false
            btnInDraft.isActivated = true
            btnToDo.isActivated = false
            btnFilterStarted.isActivated = false
            btnFilterPending.isActivated = false
            btnFilterCompleted.isActivated = false
            btnFilterManullayAdded.isActivated = false
        } else if (statusToDo) {
            btnAll.isActivated = false
            btnInDraft.isActivated = false
            btnToDo.isActivated = true
            btnFilterStarted.isActivated = false
            btnFilterPending.isActivated = false
            btnFilterCompleted.isActivated = false
            btnFilterManullayAdded.isActivated = false
        } else if (statusStarted) {
            btnAll.isActivated = false
            btnInDraft.isActivated = false
            btnToDo.isActivated = false
            btnFilterStarted.isActivated = true
            btnFilterPending.isActivated = false
            btnFilterCompleted.isActivated = false
            btnFilterManullayAdded.isActivated = false
        } else if (statusPending) {
            btnAll.isActivated = false
            btnInDraft.isActivated = false
            btnToDo.isActivated = false
            btnFilterStarted.isActivated = false
            btnFilterPending.isActivated = true
            btnFilterCompleted.isActivated = false
            btnFilterManullayAdded.isActivated = false
        } else if (statusCompleted) {
            btnAll.isActivated = false
            btnInDraft.isActivated = false
            btnToDo.isActivated = false
            btnFilterStarted.isActivated = false
            btnFilterPending.isActivated = false
            btnFilterCompleted.isActivated = true
            btnFilterManullayAdded.isActivated = false
        } else if (statusManuallyAdded) {
            btnAll.isActivated = false
            btnInDraft.isActivated = false
            btnToDo.isActivated = false
            btnFilterStarted.isActivated = false
            btnFilterPending.isActivated = false
            btnFilterCompleted.isActivated = false
            btnFilterManullayAdded.isActivated = true
        }

    }

    private fun getDocItems(docFilter: String) {
        filterDocsList = ArrayList<BorrowerDocsModel>()
        for (i in docsArrayList.indices) {
            if (docFilter.equals(docsArrayList.get(i).status, ignoreCase = true)) {

                val doc = BorrowerDocsModel(
                    docsArrayList.get(i).createdOn,
                    docsArrayList.get(i).docId,
                    docsArrayList.get(i).docName,
                    docsArrayList.get(i).subFiles,
                    docsArrayList.get(i).id,
                    docsArrayList.get(i).requestId,
                    docsArrayList.get(i).status,
                    docsArrayList.get(i).typeId,
                    docsArrayList.get(i).userName,
                    docsArrayList.get(i).message
                )
                filterDocsList.add(doc)
            }
        }
        Log.e("Pending", "$filterDocsList")
        populateRecyclerview(filterDocsList)

    }

    override fun getCardIndex(position: Int) {
    }

    override fun navigateTo(position: Int) {
        //lateinit var selectedDocumentType : ArrayList<BorrowerDocsModel>
        /*if(filter.equals(AppConstant.filter_all)){
           val selectedDocumentType = docsArrayList[position]
        } else {
            val selectedDocumentType = filterDocsList[position]
        } */

        //Log.e(filter, "$filterDocsList")

        val selectedDocumentType = if(filter.equals(AppConstant.filter_all)) docsArrayList[position] else filterDocsList[position]

        val listFragment = DocumentListFragment()
        val bundle = Bundle()
        //Log.e("subFiles", "- " + selectedDocumentType.subFiles.toString())
        val fileNames = Gson().toJson(selectedDocumentType.subFiles)
        bundle.putString(AppConstant.docName, selectedDocumentType.docName)
        bundle.putString(AppConstant.docMessage, selectedDocumentType.message)
        bundle.putParcelableArrayList(AppConstant.docObject,selectedDocumentType.subFiles)
        bundle.putString(AppConstant.innerFilesName, fileNames)
        bundle.putString(AppConstant.download_id, selectedDocumentType.id)
        bundle.putString(AppConstant.download_requestId, selectedDocumentType.requestId)
        bundle.putString(AppConstant.download_docId, selectedDocumentType.docId)
        listFragment.arguments = bundle
        findNavController().navigate(R.id.docs_list_inner_fragment, listFragment.arguments)

    }


}