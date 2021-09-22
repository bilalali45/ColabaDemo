package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.CompoundButton
import android.widget.ImageView
import android.widget.TextView
import androidx.appcompat.widget.SwitchCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import androidx.viewpager2.widget.ViewPager2.OnPageChangeCallback
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator
import com.rnsoft.colabademo.activities.dashboard.fragements.home.BaseFragment
import com.rnsoft.colabademo.databinding.FragmentHomeBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


val tabArray = arrayOf(
    "All Loans",
    "Active Loans",
    "Inactive Loans"
)


@AndroidEntryPoint
class HomeFragment : Fragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    //private val dashBoardViewModel: DashBoardViewModel by activityViewModels()
    //private val loanViewModel: LoanViewModel by activityViewModels()

    private lateinit var homeViewModel: HomeViewModel
    private var _binding: FragmentHomeBinding? = null


    private lateinit var searchImageView: ImageView
    private lateinit var filterImageView: ImageView
    private lateinit var greetingMessage: TextView
    private lateinit var assignToMeSwitch: SwitchCompat

    private var selectedText: String = tabArray[0]
    private var selectedPosition: Int = 0
    private lateinit var pageAdapter: ViewPagerAdapter
    //private  lateinit var selectedTab:TabLayout.Tab

    private lateinit var homeProfileLayout: ConstraintLayout

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

        homeProfileLayout = root.findViewById(R.id.assets_top_container)
        greetingMessage = root.findViewById(R.id.greetingMessage)
        filterImageView = root.findViewById(R.id.filter_imageview)
        searchImageView = root.findViewById(R.id.searchIconImageView)
        assignToMeSwitch = root.findViewById(R.id.assignToMeSwitch)
        searchImageView.setOnClickListener {
            findNavController().navigate(R.id.navigation_search, null)
            // NavHostFragment.findNavController(context).navigate(R.id.navigation_search)
            //Navigation.findNavController(context,R.id.nav_host_fragment_activity_main).navigate(R.id.navigation_search)
        }


        if (sharedPreferences.contains(AppConstant.ASSIGN_TO_ME)) {
            assignToMeSwitch.isChecked =
                sharedPreferences.getBoolean(AppConstant.ASSIGN_TO_ME, false)
            BaseFragment.globalAssignToMe = assignToMeSwitch.isChecked
        }

        val viewPager = binding.viewPager
        val tabLayout = binding.tabLayout

        pageAdapter = ViewPagerAdapter(requireActivity().supportFragmentManager, lifecycle)
        viewPager.adapter = pageAdapter

        TabLayoutMediator(tabLayout, viewPager) { tab, position ->
            tab.text = tabArray[position]
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
                //Log.e("onPageSelected -", position.toString())
                selectedText = tabArray[position]
                selectedPosition = position
                val test2: Fragment? =
                    requireActivity().supportFragmentManager.findFragmentByTag("f${position}")
                if (test2 != null) {
                    baseFragment = test2 as BaseFragment
                }
            }

            override fun onPageScrollStateChanged(state: Int) {
                super.onPageScrollStateChanged(state)
            }
        })



        tabLayout.addOnTabSelectedListener(object : TabLayout.OnTabSelectedListener {
            override fun onTabSelected(tab: TabLayout.Tab?) {
                tab?.let {
                    //Log.e("onTabSelected -", viewPager.currentItem.toString())
                    selectedText = it.text as String
                    viewPager.adapter
                    viewPager.currentItem
                }
                binding.appbar.setExpanded(true,true)

            }

            override fun onTabUnselected(tab: TabLayout.Tab?) {}

            override fun onTabReselected(tab: TabLayout.Tab?) {}
        })

        assignToMeSwitch.setOnCheckedChangeListener(assignToMeChangeListener)



        filterImageView.setOnClickListener {
            baseFragment?.let {
                CustomFilterBottomSheetDialogFragment.newInstance(it).show(
                    childFragmentManager,
                    CustomFilterBottomSheetDialogFragment::class.java.canonicalName
                )
            }

            //FilterBottomSheetDialogFragment.newInstance().show(childFragmentManager, FilterBottomSheetDialogFragment::class.java.canonicalName)
            //FilterBottomSheetDialogFragment.newInstance(loanFilterInterface!!).show(childFragmentManager, FilterBottomSheetDialogFragment::class.java.canonicalName)
        }

        setGreetingMessageOnTop()

        return root
    }

    val assignToMeChangeListener =
        CompoundButton.OnCheckedChangeListener { buttonView, isChecked ->
            //assignToMeSwitch.setOnClickListener(null)
            Log.e("selectedText-", selectedText)
            baseFragment?.setAssignToMe(isChecked)
            sharedPreferences.edit().putBoolean(AppConstant.ASSIGN_TO_ME, isChecked).apply()
        }

    //private var loanFilterInterface:LoanFilterInterface?=null
    private var baseFragment: BaseFragment? = null

    private fun setGreetingMessageOnTop() {
        var greetingString = AppSetting.returnGreetingString()
        sharedPreferences.getString(AppConstant.firstName, "")?.let {
            greetingString = "$greetingString $it"
        }
        greetingMessage.text = greetingString
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

}


