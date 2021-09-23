package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.RetirementLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.login_layout.*
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*
import java.util.ArrayList
import javax.inject.Inject

@AndroidEntryPoint
class RetirementFragment:BaseFragment() {

    private var _binding: RetirementLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = RetirementLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI(){

        CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.phoneFab.setOnClickListener {
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                clearFocusFromFields()
                findNavController().popBackStack()
            }
        }

        addFocusOutListenerToFields()

        setUpEndIcon()
    }

    private fun setUpEndIcon(){
        binding.accountNumberLayout.setEndIconOnClickListener(View.OnClickListener {
            if (binding.accountNumberEdittext.getTransformationMethod()
                    .equals(PasswordTransformationMethod.getInstance())
            ) { //  hide password
                binding.accountNumberEdittext.setTransformationMethod(HideReturnsTransformationMethod.getInstance())
                binding.accountNumberLayout.setEndIconDrawable(R.drawable.ic_eye_hide)
            } else {
                binding.accountNumberEdittext.setTransformationMethod(PasswordTransformationMethod.getInstance())
                binding.accountNumberLayout.setEndIconDrawable(R.drawable.ic_eye_icon_svg)
            }
        })
    }

    private fun clearFocusFromFields(){
        binding.accountNumberLayout.clearFocus()
        binding.annualBaseLayout.clearFocus()
        binding.financialLayout.clearFocus()
    }

    private fun checkEmptyFields():Boolean{
        var bool =  true
        if(binding.accountNumberEdittext.text?.isEmpty() == true || binding.accountNumberEdittext.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.accountNumberLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.accountNumberLayout,  requireContext())

        if(binding.annualBaseEditText.text?.isEmpty() == true || binding.annualBaseEditText.text?.isBlank() == true) {

            CustomMaterialFields.setError(binding.annualBaseLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())

        if(binding.financialEditText.text?.isEmpty() == true || binding.financialEditText.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.financialLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.financialLayout,  requireContext())
        return bool
    }

    private  fun addFocusOutListenerToFields(){
        binding.accountNumberEdittext.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.accountNumberEdittext , binding.accountNumberLayout , requireContext()))
        binding.annualBaseEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.annualBaseEditText , binding.annualBaseLayout , requireContext()))
        binding.financialEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.financialEditText , binding.financialLayout , requireContext()))
    }
}