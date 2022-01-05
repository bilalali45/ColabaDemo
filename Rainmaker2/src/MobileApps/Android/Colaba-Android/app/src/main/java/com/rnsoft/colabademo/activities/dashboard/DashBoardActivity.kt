package com.rnsoft.colabademo


import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.preference.PreferenceManager
import android.util.Log
import android.view.Gravity
import android.view.MenuItem
import android.view.View
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.drawerlayout.widget.DrawerLayout
import androidx.lifecycle.lifecycleScope
import androidx.navigation.NavController
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupWithNavController
import com.google.android.material.bottomnavigation.BottomNavigationView
import com.rnsoft.colabademo.databinding.DashboardLayoutBinding
import com.rnsoft.colabademo.databinding.NavHeaderBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.async
import kotlinx.coroutines.coroutineScope
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject
import javax.security.auth.callback.Callback


@AndroidEntryPoint
class DashBoardActivity : BaseActivity() {

    private val dashBoardViewModel: DashBoardViewModel by viewModels()
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: DashboardLayoutBinding
    private lateinit var actionBarToggle: ActionBarDrawerToggle
    private lateinit var navController: NavController
    private lateinit var appBarConfiguration: AppBarConfiguration
    private lateinit var headerBinding: NavHeaderBinding
    //val homeFragment = HomeFragment()

    //private var notificationArrayList: ArrayList<NotificationItem> = ArrayList()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = DashboardLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        AppSetting.userHasLoggedIn = true


        val navBottomView: BottomNavigationView = binding.navBottomView
        navController = findNavController(R.id.nav_host_fragment_activity_main)
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        val appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.navigation_home,
                R.id.navigation_profile,
                R.id.navigation_notifications
            )
        )
        //setupActionBarWithNavController(navController, appBarConfiguration)
        navBottomView.setupWithNavController(navController)

        dashBoardViewModel.notificationCount.observe(this@DashBoardActivity, { count ->
            when {
                count == -1 -> SandbarUtils.showRegular(
                    this@DashBoardActivity,
                    AppConstant.INTERNET_ERR_MSG
                )
                //count == -100 -> SandbarUtils.showRegular(this@DashBoardActivity, "Webservice not responding...")
                count > 0 -> {
                    val badge =
                        navBottomView.getOrCreateBadge(R.id.navigation_notifications) // previously showBadge
                    badge.number = count
                    badge.backgroundColor = getColor(R.color.colaba_red_color)
                    badge.badgeTextColor = getColor(R.color.white)
                }
                count == 0 -> {
                    val badge =
                        navBottomView.getOrCreateBadge(R.id.navigation_notifications) // previously showBadge
                    badge.isVisible = false
                }
                else -> {
                    SandbarUtils.showRegular(
                        this@DashBoardActivity,
                        "Webservice count not responding..."
                    )
                }
            }
        })

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let {
                val count =
                    dashBoardViewModel.getNotificationCountT(it)

                // Also run service for notifications get....
                /*
                dashBoardViewModel.getNotificationListing(
                    token ="it,
                    pageSize = pageSize, lastId = lastId, mediumId = mediumId
                )

                 */
            }
        }

        binding.centerFab.setOnClickListener{
            val startNewApplicationActivity = Intent(this@DashBoardActivity, StartNewApplicationActivity::class.java)
            startActivity(startNewApplicationActivity)
        }

        initViews()

    }

    private fun initViews(){
        //val navView: NavigationView = binding.navigationView
        // Pass the ActionBarToggle action into the drawerListener
        actionBarToggle = ActionBarDrawerToggle(this, binding.drawerLayout, 0, 0)
        binding.drawerLayout.addDrawerListener(actionBarToggle)
        binding.navigationView.setupWithNavController(navController)
        appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.nav_invite_agent, R.id.share_app_link,
                R.id.nav_settings, R.id.nav_logout
            ), binding.drawerLayout
        )

        val headerView = binding.navigationView.getHeaderView(0)
        headerBinding = NavHeaderBinding.bind(headerView)

        headerBinding.navHeaderBack.setOnClickListener {
            binding.drawerLayout.closeDrawer(Gravity.LEFT)
        }


        // set name
        val builder = StringBuilder()
        sharedPreferences.getString(AppConstant.firstName, "")?.let { firstName ->
            builder.append(firstName)
        }

        sharedPreferences.getString(AppConstant.lastName, "")?.let { lastname ->
            builder.append(" ".plus(lastname))
        }
        headerBinding.tvUserName.text = builder.toString()

        //binding.drawerLayout.setDrawerLockMode(DrawerLayout.LOCK_MODE_LOCKED_CLOSED)
        // Display the hamburger icon to launch the drawer
        //supportActionBar?.setDisplayHomeAsUpEnabled(true)

        // Call syncState() on the action bar so it'll automatically change to the back button when the drawer layout is open
        actionBarToggle.syncState()

        binding.navigationView.menu.findItem(R.id.nav_logout)
            .setOnMenuItemClickListener { menuItem: MenuItem? ->
                //loader.show()
                binding.drawerLayout.closeDrawer(Gravity.LEFT)

                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        binding.loaderHome.visibility = View.VISIBLE
                        val call = async { dashBoardViewModel.logoutUser() }
                        call.await()
                    }
                }

                dashBoardViewModel.logoutResponse.observe(this, { response ->
                    binding.loaderHome.visibility = View.GONE
                    val codeString = response.code.toString()
                    if (codeString == AppConstant.RESPONSE_CODE_SUCCESS){

                        val sharedPrefs = PreferenceManager.getDefaultSharedPreferences(this)
                        val editor = sharedPrefs.edit()
                        editor.clear()
                        editor.apply()
                        startActivity(Intent(this, SignUpFlowActivity::class.java))
                    }
                    else {
                        SandbarUtils.showError(this, AppConstant.WEB_SERVICE_ERR_MSG)
                    }
                })
                true
            }
    }


    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onDrawerClick(evt: DrawerMenuClickEvent){
        if(evt.onMenuClick){
            binding.drawerLayout.openDrawer(Gravity.LEFT)
        }
    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }
}