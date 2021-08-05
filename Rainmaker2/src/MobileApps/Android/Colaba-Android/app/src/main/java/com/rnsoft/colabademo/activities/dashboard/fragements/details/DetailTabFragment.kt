package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.viewpager2.widget.ViewPager2.OnPageChangeCallback
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator
import com.rnsoft.colabademo.databinding.DetailTabLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

val detailTabArray = arrayOf(
    "Overview",
    "Application",
    "Documents"
)


@AndroidEntryPoint
class DetailTabFragment : Fragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    private var _binding: DetailTabLayoutBinding? = null
    private val binding get() = _binding!!
    private var selectedPosition:Int = 0
    private lateinit var pageAdapter:DetailPagerAdapter

    private val detailViewModel: DetailViewModel by activityViewModels()


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {


        _binding = DetailTabLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root


        val viewPager = binding.detailViewPager
        val tabLayout = binding.detailTabLayout

        pageAdapter = DetailPagerAdapter(requireActivity().supportFragmentManager, lifecycle)
        viewPager.adapter = pageAdapter

        TabLayoutMediator(tabLayout, viewPager) { tab, position ->
            tab.text = detailTabArray[position]
        }.attach()

        viewPager.registerOnPageChangeCallback(object : OnPageChangeCallback() {
            override fun onPageScrolled(
                position: Int,
                positionOffset: Float,
                positionOffsetPixels: Int
            ) {
                super.onPageScrolled(position, positionOffset, positionOffsetPixels)
            }

            override fun onPageSelected(position: Int) {
                super.onPageSelected(position)
                Log.e("Selected_Page", position.toString())

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

        binding.backButton.setOnClickListener{
            requireActivity().finish()
        }

        loadDetailWebservices()



        return root
    }


    private fun loadDetailWebservices(){
        lifecycleScope.launchWhenStarted {
            val detailActivity = (activity as? DetailActivity)
            detailActivity?.let {
                fillHeaderValues()
                val testLoanId = it.loanApplicationId
                testLoanId?.let { loanId->
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        detailViewModel.getLoanInfo(token = authToken, loanApplicationId = loanId)
                        detailViewModel.getBorrowerDocuments(token = authToken, loanApplicationId = loanId)
                        detailViewModel.getBorrowerApplicationTabData(token = authToken, loanApplicationId = loanId)
                    }
                }
            }
        }
    }

    private fun fillHeaderValues(){
        val getParentActivity =  activity as DetailActivity
        val borrowerCompleteName = getParentActivity.borrowerFirstName + " "+getParentActivity.borrowerLastName
        //var greetingString = AppSetting.returnGreetingString()
        //greetingString = "$greetingString, $borrowerCompleteName"
        binding.borrowerNameGreeting.text = borrowerCompleteName
        binding.borrowerPurpose.text = getParentActivity.borrowerLoanPurpose


    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }


    }

