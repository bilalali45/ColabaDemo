package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
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
    ): View {
        binding = CustomDocLayoutBinding.inflate(inflater, container, false)

        setupUI()

        return binding.root

    }


    private fun setupUI(){

        binding.btnTopDelete.setOnClickListener {
            DeleteDocumentDialogFragment.newInstance("Tax returns with schedules (Personals-Two Years").show(childFragmentManager, DeleteDocumentDialogFragment::class.java.canonicalName)
        }

        binding.btnClose.setOnClickListener {
            findNavController().popBackStack()
            requireActivity().finish()
        }

    }
}