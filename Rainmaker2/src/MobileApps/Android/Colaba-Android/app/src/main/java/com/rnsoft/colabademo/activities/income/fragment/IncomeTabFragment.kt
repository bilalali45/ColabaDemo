package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.viewpager2.widget.ViewPager2
import androidx.viewpager2.widget.ViewPager2.OnPageChangeCallback
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject
import com.rnsoft.colabademo.databinding.IncomeTabLayoutBinding


private val assetsTabArray = arrayOf(
    "Richard Glenn",
    "Maria Randall",
    "UnderTaker",
    "Adam Gilchrist"
)

@AndroidEntryPoint
class IncomeTabFragment : BaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private var _binding: IncomeTabLayoutBinding? = null
    private val binding get() = _binding!!
    private var selectedPosition:Int = 0
    private lateinit var pageAdapter: IncomePagerAdapter
    private lateinit var viewPager: ViewPager2
    private lateinit var tabLayout:TabLayout

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {

        _binding = IncomeTabLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        viewPager = binding.incomeViewPager
        tabLayout = binding.incomeTabLayout
        pageAdapter = IncomePagerAdapter(requireActivity().supportFragmentManager, lifecycle)
        viewPager.adapter = pageAdapter


        viewPager.setPageTransformer(null)
        viewPager.registerOnPageChangeCallback(object : OnPageChangeCallback() {
            override fun onPageScrolled(position: Int, positionOffset: Float, positionOffsetPixels: Int) {
                super.onPageScrolled(position, positionOffset, positionOffsetPixels)
            }
            override fun onPageSelected(position: Int) {
                super.onPageSelected(position)
                //Log.e("Selected_Page", position.toString())
                selectedPosition = position
            }
            override fun onPageScrollStateChanged(state: Int) {
                super.onPageScrollStateChanged(state)
            }
        })

        tabLayout.addOnTabSelectedListener(object : TabLayout.OnTabSelectedListener {
            override fun onTabSelected(tab: TabLayout.Tab?) {
                tab?.let {
                    viewPager.adapter
                    viewPager.currentItem
                }
            }
            override fun onTabUnselected(tab: TabLayout.Tab?) {}
            override fun onTabReselected(tab: TabLayout.Tab?) {}
        })
        TabLayoutMediator(tabLayout, viewPager) { tab, position -> tab.text = assetsTabArray[position] }.attach()



        binding.backButton.setOnClickListener {
            requireActivity().finish()
        }

        super.addListeners(binding.root)
        return root
    }


    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}

