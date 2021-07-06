package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.appcompat.widget.SwitchCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.postDelayed
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import androidx.viewpager2.widget.ViewPager2.OnPageChangeCallback
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator
import com.rnsoft.colabademo.databinding.FragmentHomeBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import java.text.SimpleDateFormat
import java.util.*
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
    private val loanViewModel: LoanViewModel by activityViewModels()

    private lateinit var homeViewModel: HomeViewModel
    private var _binding: FragmentHomeBinding? = null



    private lateinit  var searchImageView: ImageView
    private lateinit  var filterImageView: ImageView
    private lateinit  var greetingMessage: TextView
    private lateinit var assignToMeSwitch:SwitchCompat

    private var selectedText:String = tabArray[0]
    private var selectedPosition:Int = 0
    //private  lateinit var selectedTab:TabLayout.Tab

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
        greetingMessage = root.findViewById(R.id.greetingMessage)
        filterImageView = root.findViewById(R.id.filter_imageview)
        searchImageView = root.findViewById(R.id.searchIconImageView)
        assignToMeSwitch = root.findViewById(R.id.assignToMeSwitch)
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
                Log.e("Selected_Page", position.toString())
                selectedText = tabArray[position]
                selectedPosition = position
            }

            override fun onPageScrollStateChanged(state: Int) {
                super.onPageScrollStateChanged(state)
            }
        })



        tabLayout.addOnTabSelectedListener(object : TabLayout.OnTabSelectedListener {
            override fun onTabSelected(tab: TabLayout.Tab?) {
                tab?.let {
                    selectedText = it.text as String
                    Log.e("tab.position==",tab.position.toString())
                   // loanFilterInterface = adapter.fragmentHashMap[tab.position] as LoanFilterInterface

                    viewPager.adapter
                    viewPager.currentItem
                }
            }

            override fun onTabUnselected(tab: TabLayout.Tab?) {}

            override fun onTabReselected(tab: TabLayout.Tab?) {}
        })

        assignToMeSwitch.setOnCheckedChangeListener { buttonView, isChecked ->
            assignToMeSwitch.isClickable = false
            Log.e("selectedText-", selectedText)
            loanFilterInterface = adapter.fragmentHashMap[selectedPosition] as LoanFilterInterface


                if(selectedText == tabArray[0]) {
                    loanFilterInterface?.setAssignToMe(100)
                    loadLoanApplications(isChecked)
                }
                else if(selectedText == tabArray[1]){
                    loanFilterInterface?.setAssignToMe(200)
                    loadActiveApplications(isChecked)
                }
                else{
                    loanFilterInterface?.setAssignToMe(300)
                    loadNonActiveApplications(isChecked)
                }

            assignToMeSwitch.postDelayed(1500) {
                assignToMeSwitch.isClickable = true
            }


        }

        setGreetingMessageOnTop()

        return root
    }

    private var loanFilterInterface:LoanFilterInterface?=null

    private fun setGreetingMessageOnTop(){
        var greetingString = AppSetting.returnGreetingString()
        sharedPreferences.getString(AppConstant.firstName,"")?.let {
            greetingString = "$greetingString $it"
        }
        greetingMessage.text = greetingString
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //private var stringDateTime: String = ""
    private var pageNumber: Int = 1
    private var pageSize: Int = 20
    private var loanFilter: Int = 0
    private var orderBy: Int = 0
    //private var assignedToMe: Boolean = false


    private fun loadLoanApplications(assignedToMe:Boolean) {
        EventBus.getDefault().post(AllLoansLoadedEvent(ArrayList()))
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.loanApiDateTime.isEmpty())
                AppSetting.loanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(
                    Date()
                )
            Log.e("Why-", AppSetting.loanApiDateTime)
            Log.e("pageNumber-", pageNumber.toString() +" and page size = "+pageSize)
            loanViewModel.getAllLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.loanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = 0,
                orderBy = orderBy, assignedToMe = assignedToMe,
                optionalClear = true
            )
        }
    }

    private fun loadActiveApplications(assignedToMe:Boolean) {
        EventBus.getDefault().post(ActiveLoansEvent(ArrayList()))
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.activeloanApiDateTime.isEmpty())
                AppSetting.activeloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())

            loanViewModel.getActiveLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.activeloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = 1,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
        }
    }

    private fun loadNonActiveApplications(assignedToMe:Boolean){
        EventBus.getDefault().post(NonActiveLoansEvent(ArrayList()))
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.nonActiveloanApiDateTime.isEmpty())
                AppSetting.nonActiveloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())

            loanViewModel.getNonActiveLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.nonActiveloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = 2,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
        }
    }

}

