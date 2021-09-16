package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.*
import javax.inject.Inject

@AndroidEntryPoint
class SplashActivity : AppCompatActivity() {
    private val activityScope = CoroutineScope(Dispatchers.Main)

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.splash_layout)



    activityScope.launch {

            //startActivity(Intent(this@SplashActivity, IncomeActivity::class.java))

            //startActivity(Intent(this@SplashActivity, BorrowerAddressActivity::class.java))


            if (sharedPreferences.getBoolean(AppConstant.IS_LOGGED_IN, false)
                && sharedPreferences.getBoolean(AppConstant.isbiometricEnabled, false)
            ) {
                delay(500)
                startActivity(Intent(this@SplashActivity, WelcomeActivity::class.java))
            } else if (sharedPreferences.getBoolean(AppConstant.IS_LOGGED_IN, false)) {
                delay(500)
                startActivity(Intent(this@SplashActivity, SignUpFlowActivity::class.java))
            } else {
                delay(500)
                startActivity(Intent(this@SplashActivity, SignUpFlowActivity::class.java))
            }

            finish()
        }


    }
}