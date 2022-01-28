package com.rnsoft.colabademo

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.viewModels
import androidx.fragment.app.activityViewModels
import androidx.viewpager2.widget.ViewPager2
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator
import com.rnsoft.colabademo.databinding.SearchTabLayoutBinding
import dagger.hilt.android.AndroidEntryPoint

/**
 * Created by Anita Kiran on 1/18/2022.
 */
@AndroidEntryPoint
class SearchTabFragment : BaseFragment() {

    private lateinit var binding: SearchTabLayoutBinding
    private lateinit var pageAdapter: SearchPagerAdapter
    private lateinit var viewPager: ViewPager2
    private var selectedPosition:Int = 0
    private val searchViewModel: SearchViewModel by activityViewModels()
    private val viewModel: StartNewAppViewModel by activityViewModels()
    private val detailTabArray = arrayOf("0 Application Found", "0 Contacts Found")
    private var dataCounter : Int = 0

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = SearchTabLayoutBinding.inflate(inflater, container, false)

        setupUI()
        observeResult()

        return binding.root

    }

    private fun observeResult(){
        searchViewModel.searchArrayList.observe(viewLifecycleOwner, {

            detailTabArray[0] =  ""+ it.size +  " " + "Application Found"
            dataCounter++
            if(it.size > 0)
               // binding.searchResultCountTextView.text = searchArrayList.size.toString() +" result found"
            else {
                //Log.e("searchArrayList", ""+it.size)
                // searchArrayList.addAll(it)
                // searchAdapter.notifyDataSetChanged()
                //binding.searchResultCountTextView.text = searchArrayList.size.toString() + " results found"
            }
        })


        viewModel.searchResultResponse.observe(viewLifecycleOwner, {

            dataCounter++
            detailTabArray[1] =  ""+ it.size +  " " + "Contact Found"

            if(dataCounter > 1 ) {
                binding.searchLayout.visibility = View.VISIBLE
                val activity = (activity as? SearchActivity)
                activity?.stopShimmer()

                if (viewPager.visibility == View.INVISIBLE)
                    viewPager.postDelayed({
                        viewPager.visibility = View.VISIBLE
                        TabLayoutMediator(binding.searchTabLayout, viewPager) { tab, position -> tab.text = detailTabArray[position] }.attach()
                    }, 600)
                else
                    TabLayoutMediator(binding.searchTabLayout, viewPager) { tab, position -> tab.text = detailTabArray[position] }.attach()
            }
        })
    }

    private fun setupUI(){

        pageAdapter = SearchPagerAdapter(requireActivity().supportFragmentManager, lifecycle)
        viewPager = binding.searchViewPager
        viewPager.adapter = pageAdapter
        viewPager.setPageTransformer(null)

        viewPager.registerOnPageChangeCallback(object : ViewPager2.OnPageChangeCallback() {
            override fun onPageScrolled(
                position: Int,
                positionOffset: Float,
                positionOffsetPixels: Int
            ) {
                super.onPageScrolled(position, positionOffset, positionOffsetPixels)
            }

            override fun onPageSelected(position: Int) {
                super.onPageSelected(position)
                selectedPosition = position
            }

            override fun onPageScrollStateChanged(state: Int) {
                super.onPageScrollStateChanged(state)
            }
        })

        binding.searchTabLayout.addOnTabSelectedListener(object : TabLayout.OnTabSelectedListener {
            override fun onTabSelected(tab: TabLayout.Tab?) {
                tab?.let {
                    viewPager.adapter
                    viewPager.currentItem
                }
            }

            override fun onTabUnselected(tab: TabLayout.Tab?) {}

            override fun onTabReselected(tab: TabLayout.Tab?) {}
        })

    }


}