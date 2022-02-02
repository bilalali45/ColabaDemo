package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.CompoundButton
import android.widget.ImageView
import android.widget.TextView
import androidx.appcompat.widget.SwitchCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import androidx.viewpager2.widget.ViewPager2.OnPageChangeCallback
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator
import com.rnsoft.colabademo.activities.details.SelectLetteRecipientsrFragment
import com.rnsoft.colabademo.databinding.FragmentHomeBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.fragment_home.*
import org.greenrobot.eventbus.EventBus
import timber.log.Timber
import javax.inject.Inject


val tabArray = arrayOf(
    "All Loans",
    "Active Loans",
    "Inactive Loans"
)

@AndroidEntryPoint
class HomeFragment : BaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var homeViewModel: HomeViewModel
    private var _binding: FragmentHomeBinding? = null
    private lateinit var searchImageView: ImageView
    private lateinit var filterImageView: ImageView
    private lateinit var greetingMessage: TextView
    private lateinit var assignToMeSwitch: SwitchCompat
    private var selectedText: String = tabArray[0]
    private var selectedPosition: Int = 0
    private lateinit var pageAdapter: ViewPagerAdapter
    private lateinit var homeProfileLayout: ConstraintLayout
    //var listener: (()->Unit)? = null

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

        ViewPagerAdapter.initialize()






        homeProfileLayout = root.findViewById(R.id.assets_top_container)
        greetingMessage = root.findViewById(R.id.greetingMessage)
        filterImageView = root.findViewById(R.id.filter_imageview)
        searchImageView = root.findViewById(R.id.searchIconImageView)
        assignToMeSwitch = root.findViewById(R.id.assignToMeSwitch)

        searchImageView.setOnClickListener {
            //findNavController().navigate(R.id.navigation_search)
           // NavHostFragment.findNavController(context).navigate(R.id.navigation_search)
           // Navigation.findNavController(context,R.id.nav_host_fragment_activity_main).navigate(R.id.navigation_search)

            val intent = Intent(requireActivity(), SearchActivity::class.java)
            startActivity(intent)

//            val intent = Intent(requireActivity(), SelectLetteRecipientsrFragment::class.java)
//            startActivity(intent)
        }

        assignToMeSwitch.setOnCheckedChangeListener(assignToMeChangeListener)

        if (sharedPreferences.contains(AppConstant.ASSIGN_TO_ME)) {
            assignToMeSwitch.isChecked = sharedPreferences.getBoolean(AppConstant.ASSIGN_TO_ME, false)
            LoanBaseFragment.globalAssignToMe = assignToMeSwitch.isChecked
        }

        val viewPager = binding.viewPager
        val tabLayout = binding.tabLayout

        pageAdapter = ViewPagerAdapter(requireActivity().supportFragmentManager, lifecycle)
        viewPager.adapter = pageAdapter
        viewPager.isUserInputEnabled = false
        viewPager.setPageTransformer(null)
        TabLayoutMediator(tabLayout, viewPager) { tab, position ->
            tab.text = tabArray[position]
        }.attach()

        //viewPager.orientation = ViewPager2.ORIENTATION_HORIZONTAL

        viewPager.registerOnPageChangeCallback(object : OnPageChangeCallback(){
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



                /*
                val test2: Fragment? = requireActivity().supportFragmentManager.findFragmentByTag("f${position}")
                if (test2 != null) {
                    loanBaseFragment = test2 as LoanBaseFragment
                }

                 */
            }

            override fun onPageScrollStateChanged(state: Int) {
                super.onPageScrollStateChanged(state)
            }
        })

        tabLayout.addOnTabSelectedListener(object : TabLayout.OnTabSelectedListener {
            override fun onTabSelected(tab: TabLayout.Tab?) {
                tab?.let {
                    //Log.e("onTabSelected -", viewPager.currentItem.toString())
                    //Timber.e("onPageSelected - working... "+viewPager.currentItem)
                    //EventBus.getDefault().post(OnTabSwitchedEvent( viewPager.currentItem))

                    selectedText = it.text as String

                    when(selectedText){
                        tabArray[0] -> { loanBaseFragment = ViewPagerAdapter.hashMap.getValue(0) as AllLoansFragment }
                        tabArray[1] -> { loanBaseFragment = ViewPagerAdapter.hashMap.getValue(1) as ActiveLoansFragment }
                        tabArray[2] -> { loanBaseFragment = ViewPagerAdapter.hashMap.getValue(2) as NonActiveLoansFragment }
                    }

                    viewPager.adapter
                    viewPager.currentItem
                }
                binding.appbar.setExpanded(true,true)

            }

            override fun onTabUnselected(tab: TabLayout.Tab?) {}

            override fun onTabReselected(tab: TabLayout.Tab?) {}
        })



        filterImageView.setOnClickListener {
            loanBaseFragment?.let {
                CustomFilterBottomSheetDialogFragment.newInstance(it).show(
                    childFragmentManager,
                    CustomFilterBottomSheetDialogFragment::class.java.canonicalName
                )
            }

            //FilterBottomSheetDialogFragment.newInstance().show(childFragmentManager, FilterBottomSheetDialogFragment::class.java.canonicalName)
            //FilterBottomSheetDialogFragment.newInstance(loanFilterInterface!!).show(childFragmentManager, FilterBottomSheetDialogFragment::class.java.canonicalName)
        }

        loanBaseFragment = ViewPagerAdapter.hashMap.getValue(0) as AllLoansFragment

        setGreetingMessageOnTop()
        super.addListeners(binding.root)

        binding.imageView18.setOnClickListener {
            EventBus.getDefault().post(DrawerMenuClickEvent(true))
        }

        return root
    }


    val assignToMeChangeListener =
        CompoundButton.OnCheckedChangeListener { buttonView, isChecked ->
            //assignToMeSwitch.setOnClickListener(null)
            //Log.e("Checked change",""+isChecked)
            //Log.e("selectedText-", selectedText)
            loanBaseFragment?.setAssignToMe(isChecked)
            sharedPreferences.edit().putBoolean(AppConstant.ASSIGN_TO_ME, isChecked).apply()
        }

    //private var loanFilterInterface:LoanFilterInterface?=null
    private var loanBaseFragment: LoanBaseFragment? = null

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


