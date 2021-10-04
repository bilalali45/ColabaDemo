package com.rnsoft.colabademo.activities.requestdocs.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.rnsoft.colabademo.AppConstant
import com.rnsoft.colabademo.BaseFragment
import com.rnsoft.colabademo.databinding.BankStatementLayoutBinding

/**
 * Created by Anita Kiran on 10/4/2021.
 */
class BorrowerStatementFragment : BaseFragment() {

    private lateinit var binding : BankStatementLayoutBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
           binding = BankStatementLayoutBinding.inflate(inflater, container, false)

        val title = arguments?.getString(AppConstant.heading).toString()
        binding.toolbarTitle.setText(title)

        return binding.root

        }
}