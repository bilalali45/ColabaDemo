package com.rnsoft.colabademo

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.recyclerview.widget.LinearLayoutManager
import com.rnsoft.colabademo.activities.startapplication.adapter.ContactsAdapter
import com.rnsoft.colabademo.databinding.SearchedResultLayoutBinding
import dagger.hilt.android.AndroidEntryPoint

/**
 * Created by Anita Kiran on 1/19/2022.
 */

@AndroidEntryPoint
class SearchedContactsFragment : Fragment(), SearchItemClickListener   {

    private lateinit var binding: SearchedResultLayoutBinding
    private lateinit var searchAdapter: SearchContactAdapter
    private var savedViewInstance: View? = null
    private var searchArrayList: ArrayList<SearchResultResponseItem> = ArrayList()
    private val viewModel: StartNewAppViewModel by activityViewModels()

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = SearchedResultLayoutBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root

            val linearLayoutManager = LinearLayoutManager(activity)
            binding.searchRecycleView.apply {
                this.layoutManager = linearLayoutManager
                this.setHasFixedSize(true)
                searchAdapter = SearchContactAdapter(searchArrayList, this@SearchedContactsFragment)
                this.adapter = searchAdapter
            }

            populateRecyclerview()

            savedViewInstance
        }
    }

    private fun populateRecyclerview(){
        viewModel.searchResultResponse.observe(viewLifecycleOwner, {
            if(it.size > 0) {
                searchArrayList = it
                searchAdapter.showResult(it,SearchActivity.searchTerm)
                searchAdapter.notifyDataSetChanged()
            }
        })
    }

    override fun onPhoneClick(position: Int) {
        if(searchArrayList.get(position).mobileNumber !=  null){
            val intent = Intent(Intent.ACTION_DIAL)
            intent.data = Uri.parse("tel:"+searchArrayList.get(position).mobileNumber)
            startActivity(intent)
        }
    }

    override fun onEmailClick(position: Int) {
        val intent = Intent(Intent.ACTION_SEND)
        intent.type = "plain/text"
        intent.putExtra(Intent.EXTRA_EMAIL,arrayOf(searchArrayList.get(position).emailAddress))
        startActivity(Intent.createChooser(intent, ""))
    }


}