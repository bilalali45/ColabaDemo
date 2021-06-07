package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController

class OtpVerificationFragment: Fragment() {

    private lateinit var root:View


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        setHasOptionsMenu(true)
        root = inflater.inflate(R.layout.phone_number_verify, container, false)
        val verifyButton = root.findViewById<Button>(R.id.verifyBtn)
        verifyButton.setOnClickListener {
            findNavController().navigate(R.id.back_to_login_id, null)
        }

        return root
    }
}
