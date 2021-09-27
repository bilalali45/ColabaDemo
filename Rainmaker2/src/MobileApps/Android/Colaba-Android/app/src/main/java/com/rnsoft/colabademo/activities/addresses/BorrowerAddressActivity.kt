package com.rnsoft.colabademo

import android.app.Activity
import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import com.rnsoft.colabademo.databinding.BorrowerAddressLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerAddressActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: BorrowerAddressLayoutBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = BorrowerAddressLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)

        overridePendingTransition(R.anim.slide_left, R.anim.hold)

        val navController = findNavController(R.id.nav_host_borrower_address_main)
        val appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.navigation_primary_borrower_info,
                R.id.navigation_current_address,
                R.id.navigation_mailing_address,
                R.id.navigation_active_duty,
                R.id.navigation_reserve,
                R.id.navigation_unmarried,
                R.id.navigation_test_fields,
                R.id.navigation_non_permanent,
            )
        )
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
        startActivity(Intent(this@BorrowerAddressActivity, SignUpFlowActivity::class.java))
        finish()
    }

    fun Activity.hideSoftKeyboard() {

    }
}