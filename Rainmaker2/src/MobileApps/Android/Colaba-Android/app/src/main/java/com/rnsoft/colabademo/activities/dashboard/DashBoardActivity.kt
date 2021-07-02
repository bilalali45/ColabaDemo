package com.rnsoft.colabademo


import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.view.View
import android.widget.ImageView
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.DialogFragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.Navigation
import androidx.navigation.findNavController
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupWithNavController
import com.google.android.material.bottomnavigation.BottomNavigationView
import com.rnsoft.colabademo.databinding.DashboardLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.lang.invoke.ConstantCallSite
import javax.inject.Inject


@AndroidEntryPoint
class DashBoardActivity : AppCompatActivity() {

    private val dashBoardViewModel: DashBoardViewModel by viewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    private lateinit var binding: DashboardLayoutBinding

    private var pageSize = 20
    private var lastId = -1
    private var mediumId = 1

    private var notificationArrayList: ArrayList<NotificationItem> = ArrayList()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = DashboardLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)


        binding.fab.bringToFront()

        val navView: BottomNavigationView = binding.navView

        val navController = findNavController(R.id.nav_host_fragment_activity_main)
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        val appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.navigation_home, R.id.navigation_profile, R.id.navigation_notifications , R.id.navigation_search
            )
        )
        //setupActionBarWithNavController(navController, appBarConfiguration)
        navView.setupWithNavController(navController)


       lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let {
               val count = dashBoardViewModel.getNotificationCount("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIzODA2NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtb2JpbGV1c2VyMUBtYWlsaW5hdG9yLmNvbSIsIkZpcnN0TmFtZSI6Ik1vYmlsZSIsIkxhc3ROYW1lIjoiVXNlcjEiLCJUZW5hbnRDb2RlIjoibGVuZG92YSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJNQ1UiLCJMb2FuIE9mZmljZXIiXSwiZXhwIjoxNjI1NDYwNjM1LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.8YNU5I3L8ifDGQymy_tofEeR9TJp1Vlt8l77xC7AXqY")
                if(count == -1)
                    SandbarUtils.showRegular(this@DashBoardActivity ,AppConstant.INTERNET_ERR_MSG)
                else if(count == -100)
                    SandbarUtils.showRegular(this@DashBoardActivity ,"Webservice not responding...")
                else  if(count > 0)
                {
                    val badge = navView.getOrCreateBadge(R.id.navigation_notifications) // previously showBadge
                    badge.number = count
                    badge.backgroundColor = getColor(R.color.colaba_red_color)
                    badge.badgeTextColor = getColor(R.color.white)
                }
            }

           sharedPreferences.getString(AppConstant.token, "")?.let {
               val borrowerNotifications = dashBoardViewModel.getNotificationListing(
                   token=
                   "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIzODA2NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtb2JpbGV1c2VyMUBtYWlsaW5hdG9yLmNvbSIsIkZpcnN0TmFtZSI6Ik1vYmlsZSIsIkxhc3ROYW1lIjoiVXNlcjEiLCJUZW5hbnRDb2RlIjoibGVuZG92YSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJNQ1UiLCJMb2FuIE9mZmljZXIiXSwiZXhwIjoxNjI1NDYwNjM1LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.8YNU5I3L8ifDGQymy_tofEeR9TJp1Vlt8l77xC7AXqY",
                   pageSize = pageSize,lastId = lastId, mediumId = mediumId)
                if(borrowerNotifications.size>0)
                    notificationArrayList = borrowerNotifications
                else
                    SandbarUtils.showRegular(this@DashBoardActivity ,"Webservice not responding...")
           }
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

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onLogoutEventReceived(event: LogoutEvent) {
        startActivity(Intent(this@DashBoardActivity, SignUpFlowActivity::class.java))
        finish()
    }
}