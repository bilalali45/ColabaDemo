package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

import com.rnsoft.colabademo.databinding.FragmentProfileBinding
import com.rnsoft.colabademo.databinding.FragmentSearchBinding


class SearchFragment : Fragment() , SearchAdapter.SearchClickListener {


    private lateinit var profileViewModel: ProfileViewModel
    private var _binding: FragmentSearchBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    private var searchRecyclerView: RecyclerView? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        profileViewModel =
            ViewModelProvider(this).get(ProfileViewModel::class.java)

        _binding = FragmentSearchBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val textView: TextView = binding.searchTitleTextView
        profileViewModel.text.observe(viewLifecycleOwner, Observer {
            textView.text = it
        })

        searchRecyclerView = root.findViewById<RecyclerView>(R.id.search_recycle_view)
        searchRecyclerView?.apply {
            // set a LinearLayoutManager to handle Android
            // RecyclerView behavior
            this.layoutManager = LinearLayoutManager(activity)
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            // set the custom adapter to the RecyclerView
            val searchList = SearchModel.sampleSearchList(requireContext())
            this.adapter = SearchAdapter(searchList, this@SearchFragment)

        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    override fun onSearchItemClick(view: View) {

    }


}