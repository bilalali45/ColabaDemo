package com.rnsoft.colabademo

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.AppHeaderWithBackNavBinding
import com.rnsoft.colabademo.databinding.ConditionalQualificationLetterBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint

/**
 * Created by Anita Kiran on 1/31/2022.
 */

@AndroidEntryPoint
class QualificationLetterFragment : BaseFragment() {
    private var savedViewInstance: View? = null
    private lateinit var binding: ConditionalQualificationLetterBinding
    private val detailViewModel: DetailViewModel by activityViewModels()
    private lateinit var toolbar: AppHeaderWithBackNavBinding


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return if (savedViewInstance != null){
            savedViewInstance
        } else {
            binding = ConditionalQualificationLetterBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root

            toolbar = binding.headerQualificationLetter
            super.addListeners(binding.root)

            clicks()
            setInputFields()

            savedViewInstance
        }
    }

    private fun setInputFields() {

        toolbar.headerTitle.text = getString(R.string.send_pre_qual_letter)

        // set lable focus
        binding.edLetterLoanAmount.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edLetterLoanAmount, binding.layoutLetterLoanAmount, requireContext()))
        binding.edLetterDownPayment.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edLetterDownPayment, binding.layoutLetterDownPayment, requireContext()))
        binding.edInterestRate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edInterestRate, binding.layoutInterestRate, requireContext()))

        // set prefix
        CustomMaterialFields.setDollarPrefix(binding.layoutLetterLoanAmount,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutLetterDownPayment,requireContext())
        CustomMaterialFields.setPercentagePrefix(binding.layoutInterestRate,requireContext())

        // input formats
        binding.edLetterLoanAmount.addTextChangedListener(NumberTextFormat(binding.edLetterLoanAmount))
        binding.edLetterDownPayment.addTextChangedListener(NumberTextFormat(binding.edLetterDownPayment))

    }

    private fun clicks(){

        toolbar.headerTitle.text = getString(R.string.send_conditional_letter)

        // clicks
        toolbar.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.qualificationLayout.setOnClickListener{
            HideSoftkeyboard.hide(requireActivity(), binding.qualificationLayout)
            super.removeFocusFromAllFields(binding.qualificationLayout)
        }
    }
}