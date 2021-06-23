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




    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = DashboardLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)




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