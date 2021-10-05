package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.inputmethod.EditorInfo
import android.view.inputmethod.InputMethodManager
import android.widget.TextView
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.rnsoft.colabademo.databinding.RequestDocsSearchLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.assets_top_cell.view.*
import kotlinx.android.synthetic.main.docs_type_header_cell.view.*
import kotlinx.android.synthetic.main.docs_type_middle_cell.view.*
import java.util.HashMap
import javax.inject.Inject

private val docsTypeTabArray = arrayOf(
    "Document Templates",
    "Document List"
)

@AndroidEntryPoint
class RequestDocSearchFragment : DocsTypesBaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private var _binding: RequestDocsSearchLayoutBinding? = null
    private val binding get() = _binding!!


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {

        _binding = RequestDocsSearchLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root


        val linearLayoutManager = LinearLayoutManager(activity)


        binding.searchEditTextField.setOnEditorActionListener(TextView.OnEditorActionListener { v, actionId, event ->
            if (actionId == EditorInfo.IME_ACTION_SEARCH) {
                binding.searchEditTextField.clearFocus()
                binding.searchEditTextField.hideKeyboard()
                performLocalSearch( binding.searchEditTextField.text.toString())
                return@OnEditorActionListener true
            }
            false
        })

        binding.searchEditTextField.addTextChangedListener(object : TextWatcher {
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
            override fun afterTextChanged(s: Editable) {
                val str: String = binding.searchEditTextField.text.toString()
                if(str.isNotEmpty())
                    binding.searchcrossImageView.visibility = View.VISIBLE
                else
                    binding.searchcrossImageView.visibility = View.INVISIBLE
            }
        })

        setFocusToSearchField()

        binding.searchcrossImageView.setOnClickListener{
            binding.searchEditTextField.setText("")
            binding.searchEditTextField.clearFocus()
            binding.searchEditTextField.hideKeyboard()
            binding.searchcrossImageView.visibility = View.INVISIBLE



        }

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }


        return root
    }

    private fun performLocalSearch(searchKeyWord:String){
        val sampleDocs = getSampleDocsFiles()
        var title:String = ""
        val searchMap: HashMap<String, ArrayList<String>> = HashMap()
        for (i in 0 until sampleDocs.size) {
            val matchedList:ArrayList<String> = arrayListOf()
            val modelData = sampleDocs[i]
            for (j in 0 until modelData.contentCell.size) {
                val contentData = modelData.contentCell[j]
                if (contentData.checkboxContent.contains(searchKeyWord,true)) {
                    matchedList.add(contentData.checkboxContent)
                    title = modelData.headerTitle
                }
            }
            if(matchedList.size>0)
                searchMap.put(title, matchedList)
        }

        if(binding.searchItemsParentLayout.childCount >0)
            binding.searchItemsParentLayout.removeAllViewsInLayout()

        if(searchMap.size ==0) {
            //binding.searchResultFoundLayout.visibility = View.INVISIBLE
            binding.noResultLayout.visibility = View.VISIBLE
        }
        else {
            //binding.searchResultFoundLayout.visibility = View.VISIBLE
            binding.noResultLayout.visibility = View.GONE
            for ((key, value) in searchMap) {
                val modelData = value
                val mainCell: LinearLayoutCompat = layoutInflater.inflate(
                    R.layout.docs_type_top_main_cell,
                    null
                ) as LinearLayoutCompat
                val topCell: View = layoutInflater.inflate(R.layout.docs_type_header_cell, null)
                topCell.cell_header_title.text = key
                topCell.total_selected.visibility = View.GONE
                // always hide this...
                topCell.total_selected.visibility = View.GONE
                topCell.items_selected_imageview.visibility = View.GONE
                topCell.docs_arrow_down.visibility = View.GONE
                topCell.tag = R.string.docs_top_cell
                mainCell.addView(topCell)

                val emptyCellStart: View =
                    layoutInflater.inflate(R.layout.docs_type_empty_space_cell, null)
                //emptyCell.visibility = View.GONE
                mainCell.addView(emptyCellStart)
                for (j in 0 until modelData.size) {
                    val contentCell: View =
                        layoutInflater.inflate(R.layout.docs_type_middle_cell, null)
                    contentCell.checkbox.text = modelData[j]
                    contentCell.info_imageview.visibility = View.INVISIBLE
                    //contentCell.info_imageview.setOnClickListener(modelData.contentListenerAttached)
                    mainCell.addView(contentCell)
                }
                val emptyCellEnd: View =
                    layoutInflater.inflate(R.layout.docs_type_empty_space_cell, null)
                //emptyCell.visibility = View.GONE
                mainCell.addView(emptyCellEnd)


                binding.searchItemsParentLayout.addView(mainCell)

            }
        }

    }

    private fun setFocusToSearchField(){
        binding.searchEditTextField.setFocusableInTouchMode(true);
        binding.searchEditTextField.requestFocus();
        //val inputMethodManager =  binding.searchEditTextField.context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        //inputMethodManager.showSoftInput(binding.searchEditTextField, InputMethodManager.SHOW_IMPLICIT)
        showSoftKeyboard(binding.searchEditTextField)
    }

    private fun showSoftKeyboard(view: View) {
        if (view.requestFocus()) {
            val imm = view.context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager?

            // here is one more tricky issue
            // imm.showSoftInputMethod doesn't work well
            // and imm.toggleSoftInput(InputMethodManager.SHOW_IMPLICIT, 0) doesn't work well for all cases too
            imm?.toggleSoftInput(InputMethodManager.SHOW_FORCED, 0)
        }
    }

    private fun View.hideKeyboard() {
        val imm = context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        imm.hideSoftInputFromWindow(windowToken, 0)
    }


}

