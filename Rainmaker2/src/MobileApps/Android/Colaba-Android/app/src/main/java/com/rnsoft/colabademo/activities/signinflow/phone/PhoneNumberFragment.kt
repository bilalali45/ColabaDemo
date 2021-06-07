package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.ProgressBar
import android.widget.Toast
import androidx.appcompat.widget.AppCompatTextView
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.navigation.fragment.findNavController
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class PhoneNumberFragment : Fragment() {

    private lateinit var root: View

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    private val phoneNumberViewModel: PhoneNumberViewModel by activityViewModels()
    private lateinit var continueButton:Button
    private lateinit var phoneNumber:EditText
    private var len = 0

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        setHasOptionsMenu(true)
        root = inflater.inflate(R.layout.phone_number, container, false)

        continueButton = root.findViewById<Button>(R.id.continueBtn)
        val skipLink = root.findViewById<AppCompatTextView>(R.id.skipTextLink)
        val loading = root.findViewById<ProgressBar>(R.id.loader_phone_screen)

        phoneNumber = root.findViewById<EditText>(R.id.editTextPhoneNumber)
        phoneNumber.addTextChangedListener(object : TextWatcher {
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {
                val str: String = phoneNumber.text.toString()
                len = str.length
            }
            override fun afterTextChanged(s: Editable) {
                val str: String = phoneNumber.text.toString()
                if (str.length == 3 && len < str.length) {
                    //phoneNumber.text.insert(0, "(");
                    //phoneNumber.append(")")
                    phoneNumber.setText("("+phoneNumber.text.toString() + ") ");
                    phoneNumber.setSelection(phoneNumber.text.length);
                }
                if (str.length == 9 && len < str.length) {
                    phoneNumber.append("-")
                }
                continueButton.isEnabled = str.length == 14
            }
        })


        if (sharedPreferences.getInt(ColabaConstant.tenantTwoFaSetting, 0) == 2 &&
            ColabaConstant.userTwoFaSetting == null
        )
            skipLink.visibility = View.VISIBLE


        phoneNumberViewModel.skipTwoFactorResponse.observe(requireActivity(), Observer {
            val skipTwoFactorResponse = it ?: return@Observer
            when (skipTwoFactorResponse.code) {
                "200" -> navigateToDashboardScreen()
                else -> {
                    skipTwoFactorResponse.message?.let { it1 -> showToast(it1) }
                }
            }
        })


        skipLink.setOnClickListener {
            phoneNumberViewModel.skipTwoFactor()
        }

        continueButton.setOnClickListener {
            //phoneNumberViewModel.sendOtpToPhone(phoneNumber = phoneNumber.text.toString() )
            findNavController().navigate(R.id.otp_verification_id, null)
        }

        return root
    }

    private fun showToast(toastMessage: String) {
        Toast.makeText(requireActivity().applicationContext, toastMessage, Toast.LENGTH_LONG).show()
    }

    private fun navigateToDashboardScreen() {
        val intent = Intent(requireActivity(), DashBoardActivity::class.java)
        startActivity(intent)
        requireActivity().finish()
    }
}
