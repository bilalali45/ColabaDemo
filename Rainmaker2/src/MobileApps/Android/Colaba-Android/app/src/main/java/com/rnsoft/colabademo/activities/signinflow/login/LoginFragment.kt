package com.rnsoft.colabademo

import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.annotation.StringRes
import androidx.appcompat.widget.AppCompatButton
import androidx.appcompat.widget.AppCompatEditText
import androidx.appcompat.widget.AppCompatTextView
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.Navigation
import androidx.navigation.fragment.findNavController
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.flow.collect
import kotlinx.coroutines.launch
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode


@AndroidEntryPoint
class LoginFragment : Fragment() {

    private lateinit var root: View

    private val loginViewModel: LoginViewModel by activityViewModels()

    private lateinit var emailError: AppCompatTextView
    private lateinit var passwordError: AppCompatTextView
    private lateinit var userEmailField: AppCompatEditText
    private lateinit var passwordField: AppCompatEditText
    private lateinit var loading: ProgressBar

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        root = inflater.inflate(R.layout.login_layout, container, false)
        setupFragment()
        return root
    }

    private fun setupFragment() {
        userEmailField = root.findViewById<AppCompatEditText>(R.id.editTextEmail)
        passwordField = root.findViewById<AppCompatEditText>(R.id.editTextPassword)
        emailError = root.findViewById<AppCompatTextView>(R.id.emailErrorTextView)
        passwordError = root.findViewById<AppCompatTextView>(R.id.passwordErrorTextView)
        val forgotPasswordLink = root.findViewById<AppCompatTextView>(R.id.forgotPasswordLink)
        val loginButton = root.findViewById<AppCompatButton>(R.id.loginBtn)
        loading = root.findViewById<ProgressBar>(R.id.loader_login_screen)
        //resetToInitialPosition()
        loginButton.setOnClickListener {
            loading.visibility = View.VISIBLE
            resetToInitialPosition()
            loginViewModel.login(userEmailField.text.toString(), passwordField.text.toString())
        }

        forgotPasswordLink.setOnClickListener {
            Navigation.findNavController(it).navigate(R.id.forgot_password_id, null)
        }
    }

    private fun resetToInitialPosition() {
        emailError.text = ""
        passwordError.text = ""
        passwordError.visibility = View.GONE
        emailError.visibility = View.GONE
    }

    private fun navigateToDashBoard(model: LoginResponse) {
        //val displayName = model.token
        val intent = Intent(requireActivity(), DashBoardActivity::class.java)
        startActivity(intent)
        requireActivity().finish()
    }

    private fun navigateToOtpScreen() =
        findNavController().navigate(R.id.otp_verification_id, null)

    private fun navigateToPhoneScreen() =
        findNavController().navigate(R.id.phone_number_id, null)


    private fun showToast(@StringRes errorString: Int) {
        Toast.makeText(requireActivity().applicationContext, errorString, Toast.LENGTH_LONG).show()
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
    fun onLoginEventReceived(event: LoginEvent) {
        event.loginResponseResult.let {
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
                when (it.screenNumber) {
                    1 -> navigateToDashBoard(it.success)
                    2 -> navigateToPhoneScreen()
                    3 -> navigateToOtpScreen()
                    else -> navigateToDashBoard(it.success)
                }
            }
        }
    }


}


////////////////////////////////////////////////////////////////////////////////////////////
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