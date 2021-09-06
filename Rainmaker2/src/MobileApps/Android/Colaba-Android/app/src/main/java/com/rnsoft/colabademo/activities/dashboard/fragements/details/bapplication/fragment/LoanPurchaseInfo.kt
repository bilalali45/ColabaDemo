package com.rnsoft.colabademo.activities.dashboard.fragements.details.bapplication.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.LoanPurchaseInfoBinding
import com.rnsoft.colabademo.databinding.PrimaryBorrowerInfoLayoutBinding

/**
 * Created by Anita Kiran on 9/3/2021.
 */
class LoanPurchaseInfo : Fragment() {

    private lateinit var binding : LoanPurchaseInfoBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = LoanPurchaseInfoBinding.inflate(inflater, container, false)








        return binding.root

    }

}
