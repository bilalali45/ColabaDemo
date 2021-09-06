package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.LoanRefinanceInfoBinding

/**
 * Created by Anita Kiran on 9/6/2021.
 */
class LoanRefinance : Fragment() {

    private lateinit var binding: LoanRefinanceInfoBinding
    private val loanStageArray = listOf("Pre-Approval")


    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        binding = LoanRefinanceInfoBinding.inflate(inflater, container, false)


       return binding.root

    }




}