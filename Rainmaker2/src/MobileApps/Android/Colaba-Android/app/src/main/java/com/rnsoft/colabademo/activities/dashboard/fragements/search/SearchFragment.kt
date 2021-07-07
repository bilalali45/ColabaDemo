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
import android.widget.ProgressBar
import android.widget.TextView.OnEditorActionListener
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.databinding.FragmentSearchBinding
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

@AndroidEntryPoint
class SearchFragment : Fragment() , SearchAdapter.SearchClickListener {



    private var _binding: FragmentSearchBinding? = null

    private val searchViewModel: SearchViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    /////////////////////////////////////////////////////////////////////////////////////////////////
    private var pageNumber: Int = 1
    private var pageSize: Int = 20

    private var searchRecyclerView: RecyclerView? = null
    private lateinit var searchAdapter: SearchAdapter
    private lateinit var loading: ProgressBar
    private var searchArrayList: ArrayList<SearchItem> = ArrayList()


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = FragmentSearchBinding.inflate(inflater, container, false)
        val root: View = binding.root

        searchViewModel.resetSearchData()

        loading = root.findViewById(R.id.search_loader)
        searchRecyclerView = root.findViewById(R.id.search_recycle_view)
        searchAdapter = SearchAdapter(searchArrayList, this@SearchFragment)
        val linearLayoutManager = LinearLayoutManager(activity)
        searchRecyclerView?.apply {
            this.layoutManager = linearLayoutManager
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            this.adapter = searchAdapter
        }



        searchViewModel.searchArrayList.observe(viewLifecycleOwner, {
            //val result = it ?: return@Observer
            loading.visibility = View.INVISIBLE

            if(it.size<=1)
                binding.searchResultCountTextView.text = searchArrayList.size.toString() +" result found"
            else {
                searchArrayList.addAll(it)
                searchAdapter.notifyDataSetChanged()
                binding.searchResultCountTextView.text =
                    searchArrayList.size.toString() + " results found"

            }
            if(hasPerformedSearchOnce) {
                binding.searchResultCountTextView.visibility = View.VISIBLE
                binding.searchResultTitleTextView.visibility = View.VISIBLE
            }


        })

        binding.searchEditTextField.setOnEditorActionListener(OnEditorActionListener { v, actionId, event ->
            if (actionId == EditorInfo.IME_ACTION_SEARCH) {
                binding.searchEditTextField.clearFocus()
                binding.searchEditTextField.hideKeyboard()
                searchViewModel.resetSearchData()
                performSearch()
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


        binding.searchcrossImageView.setOnClickListener{
            binding.searchEditTextField.setText("")
            binding.searchEditTextField.clearFocus()
            binding.searchEditTextField.hideKeyboard()
            binding.searchcrossImageView.visibility = View.INVISIBLE
            binding.searchResultCountTextView.visibility = View.INVISIBLE
            binding.searchResultTitleTextView.visibility = View.INVISIBLE
            searchViewModel.resetSearchData()
            searchAdapter.clearData()
            //binding.searchResultCountTextView.visibility = View.VISIBLE
            //binding.searchResultTitleTextView.visibility = View.VISIBLE
        }

        binding.searchBackButton.setOnClickListener {
                findNavController().popBackStack()
        }

        val scrollListener = object : EndlessRecyclerViewScrollListener(linearLayoutManager) {
            override fun onLoadMore(page: Int, totalItemsCount: Int, view: RecyclerView?) {
                // Triggered only when new data needs to be appended to the list
                // Add whatever code is needed to append new items to the bottom of the list
                pageNumber++
                performSearch()
            }
        }
        searchRecyclerView?.addOnScrollListener(scrollListener)

        return root
    }

    private var hasPerformedSearchOnce = false

    private fun performSearch() {
        //...perform search
        hasPerformedSearchOnce = true
        loading.visibility = View.VISIBLE
        loading.bringToFront()
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            val searchTerm = binding.searchEditTextField.text.toString()
            if(searchTerm.isNotEmpty()) {
                searchViewModel.getSearchResult(
                    token = authToken,
                    pageNumber = pageNumber,
                    pageSize = pageSize,
                    searchTerm = searchTerm
                    )
            }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    override fun onSearchItemClick(view: View) {

    }

    private fun View.hideKeyboard() {
        val imm = context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        imm.hideSoftInputFromWindow(windowToken, 0)
    }

    override fun onStop() {
        super.onStop()
        searchViewModel.resetSearchData()
    }

}