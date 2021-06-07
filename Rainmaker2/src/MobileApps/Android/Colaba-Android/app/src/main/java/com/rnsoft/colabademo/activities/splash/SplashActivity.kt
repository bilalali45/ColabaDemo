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
    //init {
        //MyApp.appComponent.inject(this)
    //}
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.splash_layout)
        Log.e("Splash", "loaded")
        activityScope.launch {
            /*if (sharedPreferences.getBoolean(MyAppConfigConstant.IS_LOGGED_IN, false)) {
                delay(700)
                startActivity(Intent(this@SplashActivity, ProductActivity::class.java))
                finish()

            } else { */
                delay(300)
                startActivity(Intent(this@SplashActivity, SignUpFlowActivity::class.java))
                finish()
            //}
        }
    }
}