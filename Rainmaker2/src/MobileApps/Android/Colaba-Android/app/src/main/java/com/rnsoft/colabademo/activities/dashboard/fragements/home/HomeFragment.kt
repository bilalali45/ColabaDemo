package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import androidx.viewpager2.widget.ViewPager2.OnPageChangeCallback
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator
import com.rnsoft.colabademo.databinding.FragmentHomeBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


val animalsArray = arrayOf(
    "All Loans",
    "Active Loans",
    "Inactive Loans"
)


@AndroidEntryPoint
class HomeFragment : Fragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    private val dashBoardViewModel: DashBoardViewModel by activityViewModels()

    private lateinit var homeViewModel: HomeViewModel
    private var _binding: FragmentHomeBinding? = null


    private lateinit  var searchImageView: ImageView
    private lateinit  var filterImageView: ImageView

    private lateinit var  homeProfileLayout:ConstraintLayout

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        homeViewModel =
            ViewModelProvider(this).get(HomeViewModel::class.java)

        _binding = FragmentHomeBinding.inflate(inflater, container, false)



        val root: View = binding.root

        homeProfileLayout = root.findViewById(R.id.home_profile_layout)

        filterImageView = root.findViewById(R.id.filter_imageview)
        searchImageView = root.findViewById(R.id.searchIconImageView)
        searchImageView.setOnClickListener{
            findNavController().navigate(R.id.navigation_search, null)
            // NavHostFragment.findNavController(context).navigate(R.id.navigation_search)
            //Navigation.findNavController(context,R.id.nav_host_fragment_activity_main).navigate(R.id.navigation_search)
        }

        filterImageView.setOnClickListener{
            //
            FilterBottomSheetDialogFragment.newInstance().show(childFragmentManager, FilterBottomSheetDialogFragment::class.java.canonicalName)
        }




        val viewPager = binding.viewPager
        val tabLayout = binding.tabLayout

        val adapter = ViewPagerAdapter(requireActivity().supportFragmentManager, lifecycle)
        viewPager.adapter = adapter

        TabLayoutMediator(tabLayout, viewPager) { tab, position ->
            tab.text = animalsArray[position]
        }.attach()

        //viewPager.orientation = ViewPager2.ORIENTATION_HORIZONTAL


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
            }

            override fun onPageScrollStateChanged(state: Int) {
                super.onPageScrollStateChanged(state)
            }
        })

        tabLayout.addOnTabSelectedListener(object : TabLayout.OnTabSelectedListener {
            override fun onTabSelected(tab: TabLayout.Tab?) {
                //Toast.makeText(requireContext(), "Tab ${tab?.text} selected", Toast.LENGTH_SHORT).show()
            }

            override fun onTabUnselected(tab: TabLayout.Tab?) {
                //Toast.makeText(requireContext(), "Tab ${tab?.text} unselected", Toast.LENGTH_SHORT).show()
            }

            override fun onTabReselected(tab: TabLayout.Tab?) {
               // Toast.makeText(requireContext(), "Tab ${tab?.text} reselected", Toast.LENGTH_SHORT).show()
            }
        })


        return root
    }


    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }


}

