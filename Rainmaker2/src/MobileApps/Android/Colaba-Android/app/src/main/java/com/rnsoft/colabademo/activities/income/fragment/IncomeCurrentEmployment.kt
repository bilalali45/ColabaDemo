package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.IncomeCurrentEmploymentBinding

/**
 * Created by Anita Kiran on 9/13/2021.
 */
class IncomeCurrentEmployment : Fragment() {

    private lateinit var binding: IncomeCurrentEmploymentBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = IncomeCurrentEmploymentBinding.inflate(inflater, container, false)

        return binding.root
    }
}