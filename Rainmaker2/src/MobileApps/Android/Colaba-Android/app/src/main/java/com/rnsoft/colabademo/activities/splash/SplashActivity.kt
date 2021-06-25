package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
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
        Log.e("Splash", "loaded")
        activityScope.launch {

            //Toast.makeText(this@SplashActivity, BuildConfig.BASE_URL, Toast.LENGTH_LONG).show()

            if (sharedPreferences.getBoolean(AppConstant.IS_LOGGED_IN, false)
                && sharedPreferences.getBoolean(AppConstant.isbiometricEnabled, false)) {
                delay(500)
                //startActivity(Intent(this@SplashActivity, WelcomeActivity::class.java))
                startActivity(Intent(this@SplashActivity, DashBoardActivity::class.java))
            }
            else if(sharedPreferences.getBoolean(AppConstant.IS_LOGGED_IN, false)){
                delay(500)
                startActivity(Intent(this@SplashActivity, SignUpFlowActivity::class.java))
                //startActivity(Intent(this@SplashActivity, DashBoardActivity::class.java))
            }
            else

            {
                delay(500)
                startActivity(Intent(this@SplashActivity, SignUpFlowActivity::class.java))
                //startActivity(Intent(this@SplashActivity, DashBoardActivity::class.java))
            }
            finish()
        }
    }
}