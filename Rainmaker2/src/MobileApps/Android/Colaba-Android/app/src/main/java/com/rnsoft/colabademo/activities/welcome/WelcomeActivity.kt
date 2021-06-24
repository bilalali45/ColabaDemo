package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import co.infinum.goldfinger.Goldfinger
import co.infinum.goldfinger.Goldfinger.PromptParams
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class WelcomeActivity : AppCompatActivity() {
    //private val activityScope = CoroutineScope(Dispatchers.Main)
    private lateinit var loginWithTextView: TextView
    private lateinit var withPasswordTextView: TextView
    private lateinit var username: TextView
    private lateinit var fingerPrintImageView: ImageView

    private lateinit var goldfinger: Goldfinger
    private lateinit var params: PromptParams

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.welcome_layout)
        username                =   findViewById(R.id.usernameTextView)
        loginWithTextView       =   findViewById(R.id.loginWithTextView)
        withPasswordTextView    =   findViewById(R.id.withPasswordTextView)
        fingerPrintImageView    =   findViewById(R.id.fingerPrintImage)

        sharedPreferences.getString(AppConstant.userName, "Default User")?.let {
            username.text = it
        }

        loginWithTextView.setOnClickListener {
            startActivity(Intent(this@WelcomeActivity, SignUpFlowActivity::class.java))
        }
        withPasswordTextView.setOnClickListener {
            startActivity(Intent(this@WelcomeActivity, SignUpFlowActivity::class.java))
        }

        goldfinger = Goldfinger.Builder(this)
            .logEnabled(true)
            .build()

        params = PromptParams.Builder(this)
            .title(resources.getString(R.string.authenticate_with_biometric))
            .negativeButtonText("Cancel")
            .description(resources.getString(R.string.user_your_biometric))
            //.subtitle("Subtitle")
            .build()

        fingerPrintImageView.setOnClickListener{
            if (goldfinger.canAuthenticate()) {
                goldfinger.authenticate(params,fingerPrintCallBack)
            }
           else
                showToast("Finger Print not available....")
        }
    }

    private fun showToast(toastMessage: String) = Toast.makeText(this, toastMessage, Toast.LENGTH_LONG).show()

    private val fingerPrintCallBack:Goldfinger.Callback = object : Goldfinger.Callback {
        override fun onError(e: Exception) {
           /* Critical error happened */
            showToast("Finger Print device error....")
        }

        override fun onResult(result: Goldfinger.Result) {
           /* Result received */
            if (result.type() == Goldfinger.Type.SUCCESS || result.type() == Goldfinger.Type.ERROR) {
                val formattedResult =
                    String.format("%s - %s", result.type().toString(), result.reason().toString())

                //showToast(formattedResult)
                startActivity(Intent(this@WelcomeActivity, DashBoardActivity::class.java))

            }
        }
    }








}