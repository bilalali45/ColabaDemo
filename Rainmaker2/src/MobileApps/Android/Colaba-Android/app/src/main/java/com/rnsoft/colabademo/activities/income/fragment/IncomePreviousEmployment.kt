package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.AssetFragmentLayoutBinding
import com.rnsoft.colabademo.databinding.IncomePreviousEmploymentBinding

/**
 * Created by Anita Kiran on 9/13/2021.
 */
class IncomePreviousEmployment : Fragment() {

    private lateinit var binding: IncomePreviousEmploymentBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = IncomePreviousEmploymentBinding.inflate(inflater, container, false)

        return binding.root
    }
}