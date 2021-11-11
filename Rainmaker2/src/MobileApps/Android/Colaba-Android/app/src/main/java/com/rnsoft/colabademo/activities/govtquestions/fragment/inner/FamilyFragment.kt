package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.FamilyLayoutBinding
import com.rnsoft.colabademo.databinding.OutstandingLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class FamilyFragment:BaseFragment() {

    private var _binding: FamilyLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = FamilyLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI() {
        binding.backButton.setOnClickListener { findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener {
            findNavController().popBackStack()
            /*
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                findNavController().popBackStack()
            }
             */
        }
    }

    private fun checkEmptyFields():Boolean{
        var bool = true
        if(binding.edDetails.text?.isEmpty() == true || binding.edDetails.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.layoutDetail, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.layoutDetail,  requireContext())

        return bool
    }
}