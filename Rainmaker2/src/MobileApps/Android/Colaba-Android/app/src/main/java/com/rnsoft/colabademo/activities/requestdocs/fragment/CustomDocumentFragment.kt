package com.rnsoft.colabademo.activities.requestdocs.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.rnsoft.colabademo.BaseFragment
import com.rnsoft.colabademo.databinding.CustomDocLayoutBinding

/**
 * Created by Anita Kiran on 10/4/2021.
 */
class CustomDocumentFragment : BaseFragment() {

    private lateinit var binding : CustomDocLayoutBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = CustomDocLayoutBinding.inflate(inflater, container, false)

        return binding.root

    }
}