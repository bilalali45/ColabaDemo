package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomePreviousEmploymentBinding

/**
 * Created by Anita Kiran on 9/13/2021.
 */
class IncomePreviousEmployment : Fragment() {

    private lateinit var binding: IncomePreviousEmploymentBinding
    private lateinit var toolbar: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = IncomePreviousEmploymentBinding.inflate(inflater, container, false)
            //toolbar = binding.headerIncome
            savedViewInstance = binding.root


            // set Header title
            toolbar.toolbarTitle.setText(getString(R.string.previous_employment))

            //initViews()
            savedViewInstance

        }

    }
}