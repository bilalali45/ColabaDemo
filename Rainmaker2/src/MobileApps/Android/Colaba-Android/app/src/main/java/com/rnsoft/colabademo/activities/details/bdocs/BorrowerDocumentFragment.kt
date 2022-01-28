package com.rnsoft.colabademo


import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.os.Parcelable
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.google.gson.Gson
import com.rnsoft.colabademo.databinding.BorrowerDocLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.detail_list_layout.*
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerDocumentFragment : BaseFragment(), AdapterClickListener, DownloadClickListener {

    private lateinit var binding : BorrowerDocLayoutBinding
    private var savedViewInstance: View? = null
    //private val binding get() = _binding!!
    //private lateinit var docsRecycler: RecyclerView
    private var docsArrayList: ArrayList<BorrowerDocsModel> = ArrayList()
    private var filterDocsList :ArrayList<BorrowerDocsModel> = ArrayList()
    private lateinit var borrowerDocumentAdapter: BorrowerDocumentAdapter
    //private var shimmerContainer: ShimmerFrameLayout? = null
    var isStart: Boolean = true
    //lateinit var layout_noDocFound : ConstraintLayout
    //lateinit var layout_docData : ConstraintLayout
    var state: Parcelable? = null
    var filterSeletion: String = AppConstant.filter_all
    //private  var downloadLoader: ProgressBar? = null
    private val detailViewModel: DetailViewModel by activityViewModels()
    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = BorrowerDocLayoutBinding.inflate(inflater, container, false)
            //val view: View = binding.root
            savedViewInstance = binding.root
            //shimmerContainer = binding.findViewById(R.id.shimmer_view_container) as ShimmerFrameLayout
            binding.shimmerViewContainer.startShimmer()
            //docsRecycler = view.findViewById(R.id.docs_recycle_view)
            //layout_noDocFound = view.findViewById(R.id.layout_no_documents)
            //layout_docData = view.findViewById(R.id.layout_doc_data)
            val linearLayoutManager = LinearLayoutManager(activity)
            //downloadLoader = view.findViewById(R.id.doc_download_loader)


            borrowerDocumentAdapter = BorrowerDocumentAdapter(docsArrayList, this@BorrowerDocumentFragment, this@BorrowerDocumentFragment)

                binding.docsRecycleView.apply {
                this.layoutManager = linearLayoutManager
                this.setHasFixedSize(true)
                this.adapter = borrowerDocumentAdapter
            }

            detailViewModel.borrowerDocsModelList.observe(viewLifecycleOwner, {
                if (isStart) {
                    if (it != null && it.size > 0) {
                        docsArrayList = it
                        isStart = false
                        filterSeletion = AppConstant.filter_all
                        showHideLayout(true)
                        populateRecyclerview(docsArrayList)

                    } else
                        showHideLayout((false))
                }
            })

            (activity as DetailActivity).binding.requestDocFab.setOnClickListener {
                val intent = Intent(requireActivity(), RequestDocsActivity::class.java)
                requireActivity().startActivity(intent)
            }

            binding.btnDocFilter.setOnClickListener {
                DocListFilterDialogFragment.newInstance(filterSeletion).show(
                    childFragmentManager,
                    DocListFilterDialogFragment::class.java.canonicalName
                )
            }

            setDocDropDown()
            observeDownloadProgress()
            //super.addListeners(binding.root)
            savedViewInstance
        }

    }

    private fun populateRecyclerview(arrayList: ArrayList<BorrowerDocsModel>){
        if(arrayList.size >0) {
            //Log.e("populate Recycler",""+arrayList.size)
            borrowerDocumentAdapter = BorrowerDocumentAdapter(arrayList, this@BorrowerDocumentFragment, this@BorrowerDocumentFragment)
            binding.docsRecycleView.adapter = borrowerDocumentAdapter
            //borrowerDocumentAdapter.notifyDataSetChanged()
            showHideLayout(true)
        } else{
            showHideLayout(false)
        }
    }

    private fun setDocDropDown(){
        val docType = arrayListOf<String>(*resources.getStringArray(R.array.doc_list_types))

        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,docType)
        binding.tvDocType.setAdapter(adapter)
        binding.tvDocType.setOnFocusChangeListener { _, _ ->
            binding.tvDocType.showDropDown()
        }
        binding.tvDocType.setOnClickListener {
            binding.tvDocType.showDropDown()
        }
        binding.tvDocType.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutDocTypes.isHintEnabled = false
            }
        }
    }

    private fun observeDownloadProgress(){
        detailViewModel.progressGlobal.observe(viewLifecycleOwner, {
            if (it != null && it.size > 0) {
                var percentage = ((it[0]* 100) / it[1]).toInt()
                loader_percentage.text = "$percentage%"
            }
        })
    }

    override fun fileClicked(fileName: String , fileId:String, position: Int) {
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            val selected = docsArrayList[position]

            if (selected.docId != null && selected.requestId != null &&  selected.id != null) {
                binding.docDownloadLoader.visibility = View.VISIBLE
                loader_percentage.text = "0%"
                loader_percentage.visibility = View.VISIBLE

                detailViewModel.downloadFile(
                    token = authToken,
                    id = selected.id,
                    requestId = selected.requestId,
                    docId = selected.docId,
                    fileId = fileId,
                    fileName = fileName
                )
            }
            else
                SandbarUtils.showRegular(requireActivity(), "File can not be downloaded...")
        }
    }

    override fun onResume(){
        super.onResume()
        (activity as DetailActivity).showFabIcons()
    }

    private fun getFilteredDoc(docFilter: String){
        if(filterSeletion.equals(AppConstant.filter_all, true))
            populateRecyclerview(docsArrayList)
        else {
            filterDocsList = ArrayList()
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
            //Log.e("Filterlist size", ""+filterDocsList.size)
            if (filterDocsList.size > 0) {
                binding.layoutNoDocuments.visibility = View.GONE
                //(activity as DetailActivity).binding.requestDocFab.visibility = View.VISIBLE
                //docsRecycler.visibility = View.VISIBLE
                populateRecyclerview(filterDocsList)
            } else {
                showHideLayout(false)
                //docsRecycler.visibility = View.GONE
                //layout_noDocFound.visibility = View.VISIBLE
                //(activity as DetailActivity).binding.requestDocFab.visibility = View.VISIBLE
            }
        }
    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onFilterDoc(filter: onDocFilterEvent){
        filterSeletion = filter.selection
        getFilteredDoc(filterSeletion)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onErrorReceived(event: WebServiceErrorEvent){
        binding.docDownloadLoader.visibility = View.GONE
        loader_percentage.visibility = View.GONE
        //tvPercentage.visibility = View.GONE

        if(event.isInternetError)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )
        else
            if(event.errorResult!=null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onFileDownloadCompleted(event: FileDownloadEvent) {
        if (activity is DetailActivity) {
            (activity as DetailActivity).checkIfUnreadFileOpened()
        }
        binding.docDownloadLoader.visibility = View.GONE
        loader_percentage.visibility = View.GONE
        //tvPercentage.visibility = View.GONE
        event.docFileName?.let {
            if (!it.isNullOrBlank() && it.isNotEmpty()) {
                if(it.contains(".pdf"))
                    goToPdfFragment(it)
                else
                    goToImageViewFragment(it)
            }
        }
    }

    private fun goToPdfFragment(pdfFileName:String){
        val pdfViewFragment = PdfViewFragment()
        val bundle = Bundle()
        bundle.putString(AppConstant.downloadedFileName, pdfFileName)
        pdfViewFragment.arguments = bundle
        findNavController().navigate(R.id.pdf_view_fragment_id, pdfViewFragment.arguments)
    }

    private fun goToImageViewFragment(imageFileName:String){
        val imageViewFragment = ImageViewFragment()
        val bundle = Bundle()
        bundle.putString(AppConstant.downloadedFileName, imageFileName)
        imageViewFragment.arguments = bundle
        findNavController().navigate(R.id.image_view_fragment_id, imageViewFragment.arguments)
    }

    override fun navigateTo(position: Int) {
        val selectedDocumentType = if(filterSeletion.equals(AppConstant.filter_all)) docsArrayList[position] else filterDocsList[position]
        val listFragment = DocumentListFragment()
        val bundle = Bundle()
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

    private fun showHideLayout(dataLayout: Boolean){
        if(dataLayout){
            binding.docsRecycleView.visibility = View.VISIBLE
            binding.layoutNoDocuments .visibility = View.GONE
        }
        else {
            binding.docsRecycleView.visibility = View.GONE
            binding.layoutNoDocuments.visibility = View.VISIBLE
        }
    }

    override fun getSingleItemIndex(position: Int) {
    }

}