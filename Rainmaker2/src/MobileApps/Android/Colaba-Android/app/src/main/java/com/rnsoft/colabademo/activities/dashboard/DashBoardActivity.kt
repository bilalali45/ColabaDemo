package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.widget.Button
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.activityViewModels
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject

@AndroidEntryPoint
class DashBoardActivity : AppCompatActivity() {

    private val dashBoardViewModel: DashBoardViewModel by viewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.dashboard_layout)
        val logoutButton = findViewById<Button>(R.id.logoutButton)
        logoutButton.setOnClickListener{
            sharedPreferences.getString(ColabaConstant.token,"")?.let {
                dashBoardViewModel.logoutUser(it)
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