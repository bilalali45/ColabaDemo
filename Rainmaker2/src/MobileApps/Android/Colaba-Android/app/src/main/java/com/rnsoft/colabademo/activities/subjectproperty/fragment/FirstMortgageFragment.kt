package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.addCallback
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.google.android.material.textfield.TextInputLayout

import com.rnsoft.colabademo.databinding.FirstMortgageLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.clearCheckBoxTextColor
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setCheckBoxTextColor
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setRadioColor

import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import java.lang.NullPointerException

/**
 * Created by Anita Kiran on 9/9/2021.
 */

class FirstMortgageFragment : BaseFragment() {

    private lateinit var binding : FirstMortgageLayoutBinding
    var firstMortgageModel :  FirstMortgageModel? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FirstMortgageLayoutBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)
        binding.borrowerPurpose.setText(getString(R.string.subject_property))

        setUpUI()
        setInputFields()
        setData()

        return binding.root
    }

    private fun setUpUI(){
        binding.firstMorgtageParentLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(), binding.firstMorgtageParentLayout)
            super.removeFocusFromAllFields(binding.firstMorgtageParentLayout)
        }

        binding.backButton.setOnClickListener { findNavController().popBackStack() }

        requireActivity().onBackPressedDispatcher.addCallback {
            findNavController().popBackStack()
        }

        binding.btnSave.setOnClickListener { saveData() }

        binding.cbPropertyTaxes.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                setCheckBoxTextColor(binding.cbPropertyTaxes, requireContext())
            else
                clearCheckBoxTextColor(binding.cbPropertyTaxes, requireContext())

        }

        binding.cbFloodInsurance.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                setCheckBoxTextColor(binding.cbFloodInsurance, requireContext())
            else
                clearCheckBoxTextColor(binding.cbFloodInsurance, requireContext())
        }

        binding.cbHomeownwerInsurance.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                setCheckBoxTextColor(binding.cbHomeownwerInsurance, requireContext())
            else
                clearCheckBoxTextColor(binding.cbHomeownwerInsurance, requireContext())
        }

        binding.switchCreditLimit.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                binding.layoutCreditLimit.visibility = View.VISIBLE
                binding.tvHeloc.setTypeface(null, Typeface.BOLD)
                binding.tvHeloc.setTextColor(ContextCompat.getColor(requireContext(),R.color.grey_color_one))
            } else {
                binding.layoutCreditLimit.visibility = View.GONE
                binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
                binding.tvHeloc.setTextColor(ContextCompat.getColor(requireContext(),R.color.grey_color_two))
            }
        }

        binding.rbPaidClosingYes.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked)
                binding.rbPaidClosingYes.setTextColor(ContextCompat.getColor(requireContext(),R.color.grey_color_one))
             else
                binding.rbPaidClosingYes.setTextColor(ContextCompat.getColor(requireContext(),R.color.grey_color_two))
        }

        binding.rbPaidClosingNo.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked)
                binding.rbPaidClosingNo.setTextColor(ContextCompat.getColor(requireContext(),R.color.grey_color_one))
            else
                binding.rbPaidClosingNo.setTextColor(ContextCompat.getColor(requireContext(),R.color.grey_color_two))
        }
    }

    private fun setData(){
        try {
            firstMortgageModel = arguments?.getParcelable(AppConstant.firstMortgage)!!

            firstMortgageModel?.let {
                it.firstMortgagePayment?.let {
                    binding.edFirstMortgagePayment.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutFirstPayment, R.color.grey_color_two, requireActivity())
                }
                it.unpaidFirstMortgagePayment?.let {
                    binding.edUnpaidBalance.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutUnpaidBalance, R.color.grey_color_two, requireActivity())
                }
                it.floodInsuranceIncludeinPayment?.let {
                    if (it == true) {
                        binding.cbFloodInsurance.isChecked = true
                    }
                    else {
                        binding.cbFloodInsurance.isChecked = false }
                }
                it.propertyTaxesIncludeinPayment?.let {
                    if (it == true) {
                        binding.cbPropertyTaxes.isChecked = true
                    } else {
                        binding.cbPropertyTaxes.isChecked = false
                    }
                }
                it.homeOwnerInsuranceIncludeinPayment?.let {
                    if (it == true) {
                        binding.cbHomeownwerInsurance.isChecked = true

                    } else {
                        binding.cbHomeownwerInsurance.isChecked = false
                    }
                }

                it.isHeloc?.let { bool ->
                    if (bool == true) {
                        binding.switchCreditLimit.isChecked = true
                        binding.tvHeloc.setTypeface(null, Typeface.BOLD)
                        binding.layoutCreditLimit.visibility = View.VISIBLE

                        it.helocCreditLimit?.let {
                            binding.edCreditLimit.setText(Math.round(it).toString())
                            CustomMaterialFields.setColor(binding.layoutCreditLimit, R.color.grey_color_two, requireActivity())
                        }

                    } else {
                        binding.switchCreditLimit.isChecked = false
                        binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
                    }
                }

                it.paidAtClosing?.let {
                    if (it == true) {
                        binding.rbPaidClosingYes.isChecked = true
                    } else {
                        binding.rbPaidClosingNo.isChecked = true
                    }
                }
            }
        } catch (e: NullPointerException){
            //Log.e("Exception", "catch")
        }
    }


    private fun setInputFields(){

        // set lable focus
        binding.edFirstMortgagePayment.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edFirstMortgagePayment, binding.layoutFirstPayment, requireContext()))
        binding.edUnpaidBalance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edUnpaidBalance, binding.layoutUnpaidBalance, requireContext()))
        binding.edCreditLimit.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edCreditLimit, binding.layoutCreditLimit, requireContext()))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutFirstPayment,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutUnpaidBalance,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutCreditLimit,requireContext())

        binding.edFirstMortgagePayment.addTextChangedListener(NumberTextFormat(binding.edFirstMortgagePayment))
        binding.edUnpaidBalance.addTextChangedListener(NumberTextFormat(binding.edUnpaidBalance))
        binding.edCreditLimit.addTextChangedListener(NumberTextFormat(binding.edCreditLimit))

    }

    private fun saveData() {

        // first mortgage
        val firstMortgagePayment = binding.edFirstMortgagePayment.text.toString().trim()
        val newFirstMortgagePayment = if(firstMortgagePayment.length > 0) firstMortgagePayment.replace(",".toRegex(), "") else "0"

        // second mortgage
        val unpaidBalance = binding.edUnpaidBalance.text.toString().trim()
        val newUnpaidBalance = if(unpaidBalance.length > 0) unpaidBalance.replace(",".toRegex(), "") else "0"

        val creditLimit = binding.edCreditLimit.text.toString().trim()
        val newCreditLimit = if(creditLimit.length > 0) creditLimit.replace(",".toRegex(), "") else "0"

        val floodInsurance = if(binding.cbFloodInsurance.isChecked)  true else false

        val propertyTax = if(binding.cbPropertyTaxes.isChecked) true else false

        val homeownerInsurance = if(binding.cbHomeownwerInsurance.isChecked) true else false

        var isPaidAtClosing : Boolean? = null
        if(binding.rbPaidClosingYes.isChecked)
            isPaidAtClosing = true

        if(binding.rbPaidClosingNo.isChecked)
            isPaidAtClosing = false

        val isHeloc = if(binding.switchCreditLimit.isChecked)true else false

        val firstMortgageDetail = FirstMortgageModel(firstMortgagePayment = newFirstMortgagePayment?.toDouble(),unpaidFirstMortgagePayment = newUnpaidBalance?.toDouble(),
            helocCreditLimit = newCreditLimit?.toDoubleOrNull(), floodInsuranceIncludeinPayment = floodInsurance,propertyTaxesIncludeinPayment = propertyTax,homeOwnerInsuranceIncludeinPayment = homeownerInsurance,
            paidAtClosing = isPaidAtClosing,isHeloc = isHeloc)

        findNavController().previousBackStackEntry?.savedStateHandle?.set(AppConstant.firstMortgage, firstMortgageDetail)
        findNavController().popBackStack()

    }

    fun setError(textInputlayout: TextInputLayout, errorMsg: String) {
        textInputlayout.helperText = errorMsg
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(requireContext(), R.color.primary_info_stroke_error_color))
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