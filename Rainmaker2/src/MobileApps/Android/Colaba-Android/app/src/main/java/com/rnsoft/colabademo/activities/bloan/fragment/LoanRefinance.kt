package com.rnsoft.colabademo

import android.content.res.ColorStateList
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.databinding.AppHeaderWithBackNavBinding
import com.rnsoft.colabademo.databinding.LoanRefinanceInfoBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/6/2021.
 */
class LoanRefinance : Fragment() {

    private lateinit var binding: LoanRefinanceInfoBinding
    private lateinit var bindingToolbar: AppHeaderWithBackNavBinding
    private val loanStageArray = listOf("Pre-Approval")


    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        binding = LoanRefinanceInfoBinding.inflate(inflater, container, false)
        bindingToolbar = binding.headerLoanPurchase

        // set Header title
        bindingToolbar.headerTitle.setText(getString(R.string.loan_info_refinance))

        setLoanStageSpinner()
        setFieldFocus()
        addAmountPrefix()
        setNumberFormats()

        binding.btnSaveChanges.setOnClickListener {
            checkValidations() }

        bindingToolbar.backButton.setOnClickListener {
            requireActivity().finish()
        }

        binding.loanRefinanceLayout.setOnClickListener{
            HideSoftkeyboard.hide(requireActivity(),binding.loanRefinanceLayout)
        }
        binding.parentLayout.setOnClickListener{
            HideSoftkeyboard.hide(requireActivity(),binding.parentLayout)
        }

       return binding.root

    }

    private fun setNumberFormats(){
        binding.edCashoutAmount.addTextChangedListener(NumberTextFormat(binding.edCashoutAmount))
        binding.edLoanAmount.addTextChangedListener(NumberTextFormat(binding.edLoanAmount))

    }

    private fun addAmountPrefix(){
        CustomMaterialFields.setDollarPrefix(binding.layoutLoanAmount,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutCashout,requireContext())
    }


    private fun setFieldFocus(){

        binding.edCashoutAmount.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutCashout, R.color.grey_color_two, requireContext())
            } else {
                if (binding.edCashoutAmount.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutCashout, R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutCashout)
                    CustomMaterialFields.setColor(binding.layoutCashout, R.color.grey_color_two, requireContext())
                }
            }
        }

        binding.edLoanAmount.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutLoanAmount, R.color.grey_color_two, requireContext())
            } else {
                if (binding.edLoanAmount.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutLoanAmount, R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutLoanAmount)
                    CustomMaterialFields.setColor(binding.layoutLoanAmount, R.color.grey_color_two, requireContext())
                }
            }
        }

    }


    private fun setLoanStageSpinner() {
        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, loanStageArray)
        binding.tvLoanStage.setAdapter(adapter)
        binding.tvLoanStage.setOnFocusChangeListener { _, _ ->
            binding.tvLoanStage.showDropDown()
        }
        binding.tvLoanStage.setOnClickListener {
            binding.tvLoanStage.showDropDown()
        }
        binding.tvLoanStage.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutLoanStage.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), com.rnsoft.colabademo.R.color.grey_color_two))

                if(binding.tvLoanStage.text.isNotEmpty() && binding.tvLoanStage.text.isNotBlank()) {
                    clearError(binding.layoutLoanStage)
                }
                if (position == loanStageArray.size - 1)
                    binding.layoutLoanStage.visibility = View.VISIBLE
                else
                    binding.layoutLoanStage.visibility = View.GONE
            }
        }
    }

    private fun checkValidations(){

        val loanStage: String = binding.tvLoanStage.text.toString()
        val cashOutAmount: String = binding.edCashoutAmount.text.toString()
        val loanAmount: String = binding.edLoanAmount.text.toString()

        if (loanStage.isEmpty() || loanStage.length == 0) {
            setError(binding.layoutLoanStage, getString(R.string.error_field_required))
        }
        if (cashOutAmount.isEmpty() || cashOutAmount.length == 0) {
            setError(binding.layoutCashout, getString(R.string.error_field_required))
        }
        if (loanAmount.isEmpty() || loanAmount.length == 0) {
            setError(binding.layoutLoanAmount, getString(R.string.error_field_required))
        }
        // clear error
        if (loanStage.isNotEmpty() || loanStage.length > 0) {
            clearError(binding.layoutLoanStage)
        }
        if (cashOutAmount.isNotEmpty() || cashOutAmount.length > 0) {
            clearError(binding.layoutCashout)
        }
        if (loanAmount.isNotEmpty() || loanAmount.length > 0) {
            clearError(binding.layoutLoanAmount)
        }
    }


    fun setError(textInputlayout: TextInputLayout, errorMsg: String) {
        textInputlayout.helperText = errorMsg
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(requireContext(),R.color.primary_info_stroke_error_color))
    }

    fun clearError(textInputlayout: TextInputLayout) {
        textInputlayout.helperText = ""
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(
                requireContext(),
                R.color.primary_info_line_color
            )
        )
    }

}