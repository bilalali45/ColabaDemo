package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.rnsoft.colabademo.databinding.DocRequestSentLayoutBinding

/**
 * Created by Anita Kiran on 10/6/2021.
 */
class RequestSentFragment : BaseFragment() {
     private lateinit var binding : DocRequestSentLayoutBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = DocRequestSentLayoutBinding.inflate(inflater, container, false)

        setupUI()


        return binding.root

    }


    private fun setupUI(){

        binding.btnBackToDoc.setOnClickListener {
            requireActivity().finish()
        }


    }
}