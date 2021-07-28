package com.rnsoft.colabademo

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.text.method.PasswordTransformationMethod
import android.util.DisplayMetrics
import android.util.Log
import android.view.Gravity
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.ProgressBar
import androidx.appcompat.widget.*
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.Navigation
import androidx.navigation.fragment.findNavController
import co.infinum.goldfinger.Goldfinger
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject


@AndroidEntryPoint
class LoginFragment : Fragment() {

    private lateinit var root: View

    private val loginViewModel: LoginViewModel by activityViewModels()

    private lateinit var emailError: AppCompatTextView
    private lateinit var passwordError: AppCompatTextView
    private lateinit var userEmailField: AppCompatEditText
    private lateinit var passwordField: AppCompatEditText
    private lateinit var loading: ProgressBar
    private lateinit var biometricSwitch: SwitchCompat
    private lateinit var forgotPasswordLink: AppCompatTextView
    private lateinit var loginButton: AppCompatButton
    private lateinit var imageView5: ImageView


    private lateinit var passwordImageView: AppCompatImageView
    private lateinit var passwordHideImageView: AppCompatImageView


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        root = inflater.inflate(R.layout.login_layout, container, false)
        setupFragment()

        //registerBroadcastReceiver()
        return root
    }

    private fun setupFragment() {
        userEmailField = root.findViewById<AppCompatEditText>(R.id.editTextEmail)
        passwordField = root.findViewById<AppCompatEditText>(R.id.editTextPassword)
        emailError = root.findViewById<AppCompatTextView>(R.id.emailErrorTextView)
        passwordError = root.findViewById<AppCompatTextView>(R.id.passwordErrorTextView)
        passwordImageView = root.findViewById<AppCompatImageView>(R.id.passwordImageShow)
        passwordHideImageView = root.findViewById<AppCompatImageView>(R.id.passwordHideImageShow)
        biometricSwitch = root.findViewById<SwitchCompat>(R.id.switch1)


        imageView5 =  root.findViewById<ImageView>(R.id.imageView5)
        imageView5.setOnClickListener{
            navigateToDashBoard(null)
        }

        forgotPasswordLink = root.findViewById<AppCompatTextView>(R.id.forgotPasswordLink)
        loginButton = root.findViewById<AppCompatButton>(R.id.loginBtn)
        loading = root.findViewById<ProgressBar>(R.id.loader_login_screen)
        //resetToInitialPosition()
        loginButton.setOnClickListener {
            loading.visibility = View.VISIBLE
            toggleButtonState(false)
            resetToInitialPosition()
            loginViewModel.login(userEmailField.text.toString(), passwordField.text.toString())
        }

        forgotPasswordLink.setOnClickListener {
            Navigation.findNavController(it).navigate(R.id.forgot_password_id, null)
        }

        passwordImageView.setOnClickListener {
            passwordField.transformationMethod = null
            passwordField.setSelection(passwordField.length());
            passwordImageView.visibility = View.INVISIBLE
            passwordHideImageView.visibility = View.VISIBLE
        }

        passwordHideImageView.setOnClickListener {
            passwordField.transformationMethod = PasswordTransformationMethod()
            passwordField.setSelection(passwordField.length());
            passwordHideImageView.visibility = View.INVISIBLE
            passwordImageView.visibility = View.VISIBLE
        }

        goldfinger = Goldfinger.Builder(requireActivity())
            .logEnabled(true)
            .build()

        biometricSwitch.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                if (goldfinger.canAuthenticate()) {
                    Log.e("Yes", "Let Toggle On...")

                }
                else {
                    biometricSwitch.isChecked = false
                    SandbarUtils.showRegular(requireActivity(), resources.getString((R.string.biometric_check_two)) )

                    /*
                    ToastUtils.init(requireActivity().application)
                    ToastUtils.setView(R.layout.toast_error_layout)
                    ToastUtils.setGravity(Gravity.BOTTOM, 0, 60)
                    ToastUtils.show(resources.getString((R.string.biometric_check)))
                    */
                }
            }
            AppSetting.biometricEnabled = biometricSwitch.isChecked
        }


    }

    private lateinit var goldfinger: Goldfinger

    private fun resetToInitialPosition() {
        emailError.text = ""
        passwordError.text = ""
        passwordError.visibility = View.GONE
        emailError.visibility = View.GONE
    }

    private fun navigateToDashBoard(model: LoginResponse?) {
        //val displayName = model.token
        val intent = Intent(requireActivity(), DashBoardActivity::class.java)
        startActivity(intent)
        requireActivity().finish()
    }

    private fun navigateToOtpScreen() =
        findNavController().navigate(R.id.otp_verification_id, null)

    private fun navigateToPhoneScreen() =
        findNavController().navigate(R.id.phone_number_id, null)




   override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        //unregisterReceiver()
        EventBus.getDefault().unregister(this)
    }


    @Inject
    lateinit var sharedPreferences: SharedPreferences

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onLoginEventReceived(event: LoginEvent) {
        toggleButtonState(true)
        event.loginResponseResult.let {
            loading.visibility = View.INVISIBLE

            if (it.emailError != null) {
                emailError.visibility = View.VISIBLE
                emailError.text = it.emailError
            } else if (it.passwordError != null) {
                passwordError.visibility = View.VISIBLE
                passwordError.text = it.passwordError
            } else if (it.responseError != null) {
                //showToast(it.responseError)
                if(it.responseError == AppConstant.INTERNET_ERR_MSG)
                    SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )
                 else
                    SandbarUtils.showError(requireActivity(), it.responseError )
            } else if (it.success != null) {
                emailError.visibility = View.GONE
                passwordError.visibility = View.GONE
                when (it.screenNumber) {
                    1 -> {
                        if(biometricSwitch.isChecked)
                            sharedPreferences.edit().putBoolean(AppConstant.isbiometricEnabled, true).apply()
                        navigateToDashBoard(it.success)

                    }
                    2 -> navigateToPhoneScreen()
                    3 -> navigateToOtpScreen()
                    else -> {
                        //showToast(R.string.we_have_send_you_email)
                        SandbarUtils.showRegular(requireActivity(),AppConstant.INTERNET_ERR_MSG)

                    }
                }
            }
        }
    }

    private fun toggleButtonState(bool: Boolean) {
        forgotPasswordLink.isEnabled = bool
        loginButton.isEnabled = bool
    }


}



//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Getting phone lock broadcast ......

    /*
    private var mPowerKeyReceiver: BroadcastReceiver? = null

    private fun registerBroadcastReceiver() {
        val theFilter = IntentFilter()
        /** System Defined Broadcast  */
        theFilter.addAction(Intent.ACTION_SCREEN_ON)
        theFilter.addAction(Intent.ACTION_SCREEN_OFF)
        mPowerKeyReceiver = object : BroadcastReceiver() {
            override fun onReceive(context: Context?, intent: Intent) {
                val strAction = intent.action
                if (strAction == Intent.ACTION_SCREEN_OFF || strAction == Intent.ACTION_SCREEN_ON) {
                    // > Your playground~!
                    Log.e("Screen-","ON-OFF LOCK---")
                }
            }
        }
        requireActivity().applicationContext
            .registerReceiver(mPowerKeyReceiver, theFilter)
    }

    private fun unregisterReceiver() {
            try {
                requireActivity().applicationContext
                    .unregisterReceiver(mPowerKeyReceiver)
            } catch (e: IllegalArgumentException) {
                mPowerKeyReceiver = null
            }
    }


     */


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //private fun showToast(@StringRes errorString: Int) {

            //SandbarUtils.showRegular(requireActivity(), "some error" )

            //ToastUtils.init(requireActivity().application)

            //ToastUtils.setView(R.layout.toast_error_layout)
            //ToastUtils.setGravity(Gravity.BOTTOM, 0, 60)



            //ToastUtils.show("Some Error coming from the case")




    // }



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// adding observer to login...

/*
        loginViewModel.loginResponseResult.observe(viewLifecycleOwner, Observer {
            val loginResult = it ?: return@Observer
            loading.visibility = View.INVISIBLE

            if (loginResult.emailError != null) {
                emailError.visibility = View.VISIBLE
                emailError.text = it.emailError?.let { it1 -> resources.getString(it1) }
            } else if (loginResult.passwordError != null) {
                passwordError.visibility = View.VISIBLE
                passwordError.text = it.passwordError?.let { it1 -> resources.getString(it1) }
            } else if (loginResult.responseError != null) {
                showToast(loginResult.responseError)
            } else if (loginResult.success != null) {
                emailError.visibility = View.GONE
                passwordError.visibility = View.GONE
                when(it.screenNumber)
                {
                    1 -> navigateToDashBoard(loginResult.success)
                    2 -> navigateToPhoneScreen(loginResult.success)
                    3 -> navigateToOtpScreen(loginResult.success)
                    else -> navigateToDashBoard(loginResult.success)
                }
            }
        })


        lifecycleScope.launchWhenStarted  {
            // Trigger the flow and start listening for values.
            // Note that this happens when lifecycle is STARTED and stops
            // collecting when the lifecycle is STOPPED
            loginViewModel.loginResponseResult.collect { it ->
                // New value received
                loading.visibility = View.INVISIBLE
                if (it.emailError != null) {
                    emailError.visibility = View.VISIBLE
                    emailError.text = it.emailError.let { it1 -> resources.getString(it1) }
                } else if (it.passwordError != null) {
                    passwordError.visibility = View.VISIBLE
                    passwordError.text = it.passwordError.let { it1 -> resources.getString(it1) }
                } else if (it.responseError != null) {
                    showToast(it.responseError)
                } else if (it.success != null) {
                    emailError.visibility = View.GONE
                    passwordError.visibility = View.GONE
                    when(it.screenNumber)
                    {
                        0 -> resetToInitialPosition()
                        1 -> navigateToDashBoard(it.success)
                        2 -> navigateToPhoneScreen(it.success)
                        3 -> navigateToOtpScreen(it.success)
                        else -> navigateToDashBoard(it.success)
                    }
                }
            }
        }


        @ExperimentalCoroutinesApi
    private fun runColdFlow(email:String, password:String){
        lifecycleScope.launch  {
            // Trigger the flow and start listening for values.
            // Note that this happens when lifecycle is STARTED and stops
            // collecting when the lifecycle is STOPPED
            loginViewModel.newLoginFlow(email, password).collect { it ->
                // New value received
                loading.visibility = View.INVISIBLE
                if (it.emailError != null) {
                    emailError.visibility = View.VISIBLE
                    emailError.text = it.emailError.let { it1 -> resources.getString(it1) }
                } else if (it.passwordError != null) {
                    passwordError.visibility = View.VISIBLE
                    passwordError.text = it.passwordError.let { it1 -> resources.getString(it1) }
                } else if (it.responseError != null) {
                    showToast(it.responseError)
                } else if (it.success != null) {
                    emailError.visibility = View.GONE
                    passwordError.visibility = View.GONE
                    when(it.screenNumber)
                    {
                        0 -> resetToInitialPosition()
                        1 -> navigateToDashBoard(it.success)
                        2 -> navigateToPhoneScreen()
                        3 -> navigateToOtpScreen()
                        else -> navigateToDashBoard(it.success)
                    }
                }
            }
        }
    }
*/