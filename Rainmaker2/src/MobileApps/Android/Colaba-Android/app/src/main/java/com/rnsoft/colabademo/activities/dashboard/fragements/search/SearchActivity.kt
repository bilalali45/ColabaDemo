package com.rnsoft.colabademo

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.View
import android.view.inputmethod.EditorInfo
import android.view.inputmethod.InputMethodManager
import android.widget.ProgressBar
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.isVisible
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.facebook.shimmer.ShimmerFrameLayout
import com.rnsoft.colabademo.databinding.FragmentSearchBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject
import android.widget.TextView.OnEditorActionListener
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode


/**
 * Created by Anita Kiran on 1/5/2022.
 */

@AndroidEntryPoint
class SearchActivity : AppCompatActivity() , SearchAdapter.SearchClickListener {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private val searchViewModel: SearchViewModel by viewModels()
    private var pageNumber: Int = 1
    private var pageSize: Int = 20
    private var searchRecyclerView: RecyclerView? = null
    private lateinit var searchAdapter: SearchAdapter
    private lateinit var searchRowLoader: ProgressBar
    private var searchArrayList: ArrayList<SearchItem> = ArrayList()
    private var shimmerContainer: ShimmerFrameLayout? = null
    private lateinit var binding: FragmentSearchBinding
    private var hasPerformedSearchOnce = false


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = FragmentSearchBinding.inflate(layoutInflater)
        setContentView(binding.root)

        shimmerContainer = findViewById(R.id.shimmer_view_container) as ShimmerFrameLayout
        searchRowLoader = findViewById(R.id.search_row_loader)
        searchRecyclerView = findViewById(R.id.search_recycle_view)

        searchViewModel.resetSearchData()

        searchAdapter = SearchAdapter(searchArrayList, this)
        val linearLayoutManager = LinearLayoutManager(this)

        searchRecyclerView?.apply {
            this.layoutManager = linearLayoutManager
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            this.adapter = searchAdapter
        }


        searchViewModel.searchArrayList.observe(this, {
            searchRowLoader.visibility = View.INVISIBLE
            shimmerContainer?.stopShimmer()
            shimmerContainer?.isVisible = false

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
                shimmerContainer?.isVisible = true
                shimmerContainer?.startShimmer()
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

        setFocusToSearchField()

        binding.searchcrossImageView.setOnClickListener{
            binding.searchEditTextField.setText("")
            binding.searchEditTextField.clearFocus()
            binding.searchEditTextField.hideKeyboard()
            binding.searchcrossImageView.visibility = View.INVISIBLE
            binding.searchResultCountTextView.visibility = View.INVISIBLE
            binding.searchResultTitleTextView.visibility = View.INVISIBLE
            searchViewModel.resetSearchData()
            searchAdapter.clearData()
            searchRowLoader.visibility = View.INVISIBLE
            searchRowLoader.visibility = View.INVISIBLE
            shimmerContainer?.stopShimmer()
            shimmerContainer?.isVisible = false
            //binding.searchResultCountTextView.visibility = View.VISIBLE
            //binding.searchResultTitleTextView.visibility = View.VISIBLE
        }

        binding.searchBackButton.setOnClickListener {
            binding.searchBackButton.hideKeyboard()
            finish()
        }

        val scrollListener = object : EndlessRecyclerViewScrollListener(linearLayoutManager) {
            override fun onLoadMore(page: Int, totalItemsCount: Int, view: RecyclerView?) {
                // Triggered only when new data needs to be appended to the list
                // Add whatever code is needed to append new items to the bottom of the list
                searchRowLoader.visibility = View.VISIBLE
                searchRowLoader.bringToFront()
                pageNumber++
                performSearch()
            }
        }

        searchRecyclerView?.addOnScrollListener(scrollListener)


    }

    private fun performSearch() {
        hasPerformedSearchOnce = true
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

    private fun setFocusToSearchField(){
        binding.searchEditTextField.setFocusableInTouchMode(true);
        binding.searchEditTextField.requestFocus();
        //val inputMethodManager =  binding.searchEditTextField.context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        //inputMethodManager.showSoftInput(binding.searchEditTextField, InputMethodManager.SHOW_IMPLICIT)
        showSoftKeyboard(binding.searchEditTextField)
    }

    override fun onSearchItemClick(view: View) {

    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        searchViewModel.resetSearchData()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onErrorReceived(event: WebServiceErrorEvent) {
        shimmerContainer?.stopShimmer()
        shimmerContainer?.isVisible = false
        searchRowLoader.visibility = View.INVISIBLE
        if(event.isInternetError)
            SandbarUtils.showError(this, AppConstant.INTERNET_ERR_MSG )
        else
            if(event.errorResult!=null)
                SandbarUtils.showError(this, AppConstant.WEB_SERVICE_ERR_MSG )
    }

    override fun navigateToBorrowerScreen(position: Int) {
        val borrowerDetailIntent= Intent(this, DetailActivity::class.java)
        val test = searchArrayList[position]
        Log.e("Before" , test.id.toString())
        //borrowerDetailIntent.putExtra(AppConstant.borrowerParcelObject, allLoansArrayList[position])
        borrowerDetailIntent.putExtra(AppConstant.loanApplicationId,  test.id)
        //borrowerDetailIntent.putExtra(AppConstant.loanPurpose,  test.loanPurpose)
        borrowerDetailIntent.putExtra(AppConstant.firstName,  test.firstName)
        borrowerDetailIntent.putExtra(AppConstant.lastName,  test.lastName)
        //borrowerDetailIntent.putExtra(AppConstant.bPhoneNumber,  test.cellNumber)
        //borrowerDetailIntent.putExtra(AppConstant.bEmail,  test.email)
        startActivity(borrowerDetailIntent)
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