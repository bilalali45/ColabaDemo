package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.CustomFocusListenerForEditText
import com.rnsoft.colabademo.databinding.UndisclosedBorrowerFundLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class UndisclosedBorrowerFundFragment:Fragment() {

    private var _binding: UndisclosedBorrowerFundLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = UndisclosedBorrowerFundLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        return root
    }


    private fun setUpUI(){
        binding.backButton.setOnClickListener { findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener {
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                clearFocusFromFields()
                findNavController().popBackStack()
            }
        }
        addFocusOutListenerToFields()
        CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())
    }



    private fun clearFocusFromFields(){
        //binding.accountNumberLayout.clearFocus()
        //binding.accountTypeInputLayout.clearFocus()
        binding.annualBaseLayout.clearFocus()
        //binding.financialLayout.clearFocus()
    }

    private fun checkEmptyFields():Boolean{
        var bool =  true

        if(binding.annualBaseEditText.text?.isEmpty() == true || binding.annualBaseEditText.text?.isBlank() == true) {

            CustomMaterialFields.setError(binding.annualBaseLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())

        if(binding.edDetails.text?.isEmpty() == true || binding.edDetails.text?.isBlank() == true) {

            CustomMaterialFields.setError(binding.layoutDetail, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.layoutDetail,  requireContext())


        return bool
    }


    private  fun addFocusOutListenerToFields(){
        //binding.accountNumberEdittext.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.accountNumberEdittext , binding.accountNumberLayout , requireContext()))
        //binding.accountTypeCompleteView.setOnFocusChangeListener(CustomFocusListenerForAutoCompleteTextView( binding.accountTypeCompleteView , binding.accountTypeInputLayout , requireContext()))
        binding.annualBaseEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.annualBaseEditText , binding.annualBaseLayout , requireContext()))
        binding.edDetails.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.edDetails , binding.layoutDetail , requireContext()))
    }
}