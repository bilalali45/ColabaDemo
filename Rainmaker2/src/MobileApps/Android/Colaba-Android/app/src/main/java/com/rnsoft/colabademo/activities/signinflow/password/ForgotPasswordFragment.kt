package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ProgressBar
import android.widget.Toast
import androidx.appcompat.widget.AppCompatEditText
import androidx.appcompat.widget.AppCompatTextView
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.navigation.fragment.findNavController
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.util.*

class ForgotPasswordFragment : Fragment() {
    private lateinit var root: View

    private val forgotPasswordViewModel: ForgotPasswordViewModel by activityViewModels()

    private lateinit var loading:ProgressBar

    private lateinit var errorTextView:AppCompatTextView

    private lateinit var resetButton:Button

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        setHasOptionsMenu(true)
        root = inflater.inflate(R.layout.password_forgot, container, false)

        resetButton  = root.findViewById<Button>(R.id.resetPasswordBtn)
        val userEmailField = root.findViewById<AppCompatEditText>(R.id.editTextEmail)
        errorTextView = root.findViewById(R.id.emailErrorTextView)
        loading = root.findViewById(R.id.loader_forgot_screen)

        resetButton.setOnClickListener {
            it.isEnabled = false
            errorTextView.text =""
            loading.visibility = View.VISIBLE
            forgotPasswordViewModel.forgotPassword(userEmailField.text.toString())
        }
        return root
    }



    private fun showToast(toastMessage: String) {
        Toast.makeText(requireActivity().applicationContext, toastMessage, Toast.LENGTH_LONG).show()
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
    fun onPasswordEventReceived(event: ForgotPasswordEvent) {
        loading.visibility = View.INVISIBLE
        resetButton.isEnabled = true
        val forgotPasswordResponse = event.forgotPasswordResponse
        when(event.forgotPasswordResponse.code)
        {
            "400" ->errorTextView.text = forgotPasswordResponse.message
            "300" ->errorTextView.text = forgotPasswordResponse.message
            "600" ->errorTextView.text = forgotPasswordResponse.message
            "200" -> {
                showToast("success")
                findNavController().navigate(R.id.back_to_login_id, null)
            }
            else -> {
                showToast("Failure Exception...")
            }
        }
    }


    /*
        override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
            forgotPasswordViewModel.forgotPasswordResponse?.observe(viewLifecycleOwner, Observer {
                val forgotPasswordResponse = it ?: return@Observer
                loading.visibility = View.INVISIBLE
                when(forgotPasswordResponse.code) {
                    "400" ->errorTextView.text = forgotPasswordResponse.message.let { it1 -> it1 }
                    "300" ->errorTextView.text = forgotPasswordResponse.message.let { it1 -> it1 }
                    "600" ->errorTextView.text = forgotPasswordResponse.message.let { it1 -> it1 }
                    "200" -> {
                        forgotPasswordViewModel.forgotPasswordResponse?.removeObservers(viewLifecycleOwner)
                        showToast("success")
                        findNavController().navigate(R.id.back_to_login_id, null)
                    }
                    else -> {
                        showToast("Failure Exception...")
                    }
                }
            })
    }

     */

}