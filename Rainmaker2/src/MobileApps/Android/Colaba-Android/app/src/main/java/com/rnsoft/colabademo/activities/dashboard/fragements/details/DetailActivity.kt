package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.DetailTopLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.util.*
import javax.inject.Inject


@AndroidEntryPoint
class DetailActivity : AppCompatActivity() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: DetailTopLayoutBinding
    private val detailViewModel: DetailViewModel by viewModels()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = DetailTopLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)

        lifecycleScope.launchWhenStarted {
            val extras = intent.extras
            extras?.let {
                val testLoanId = it.getInt(AppConstant.loanApplicationId)
                testLoanId.let { testLoanId->
                if(testLoanId!=null) {
                        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                            detailViewModel.getLoanInfo(token = authToken, loanApplicationId = testLoanId)
                        }
                    }
                }
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
        startActivity(Intent(this@DetailActivity, SignUpFlowActivity::class.java))
        finish()
    }
}