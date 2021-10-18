package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.content.res.AppCompatResources
import com.google.android.material.textfield.TextInputLayout

import com.rnsoft.colabademo.databinding.FirstMortgageLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/9/2021.
 */
class FirstMortgageFragment : BaseFragment() {

    private lateinit var binding : FirstMortgageLayoutBinding
    private var list : ArrayList<FirstMortgageModel> = ArrayList()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FirstMortgageLayoutBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)


        binding.borrowerPurpose.setText(getString(R.string.subject_property))

        //HideSoftkeyboard.hide(requireActivity(), binding.firstMorgtageParentLayout)
        //super.removeFocusFromAllFields(binding.firstMorgtageParentLayout)

        setUpUI()
        setData()
        setInputFields()

        return binding.root
    }

    private fun setUpUI(){
        binding.backButton.setOnClickListener { requireActivity().onBackPressed() }

        binding.btnSave.setOnClickListener { checkValidations() }

        binding.cbPropertyTaxes.setOnClickListener {
            binding.cbPropertyTaxes.setTypeface(null, Typeface.BOLD)
            binding.cbPropertyTaxes.setTypeface(null, Typeface.NORMAL)
        }

        binding.cbFloodInsurance.setOnClickListener {
            binding.cbFloodInsurance.setTypeface(null, Typeface.BOLD)
            binding.cbFloodInsurance.setTypeface(null, Typeface.NORMAL)
        }

        binding.cbHomeownwerInsurance.setOnClickListener {
            binding.cbHomeownwerInsurance.setTypeface(null, Typeface.BOLD)
            binding.cbHomeownwerInsurance.setTypeface(null, Typeface.NORMAL)
        }

        binding.switchCreditLimit.setOnClickListener {
            if(binding.switchCreditLimit.isChecked) {
                binding.layoutCreditLimit.visibility = View.VISIBLE
                binding.tvHeloc.setTypeface(null, Typeface.BOLD)
            } else {
                binding.layoutCreditLimit.visibility = View.GONE
                binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
            }
        }

        binding.rbPaidClosingYes.setOnClickListener {
            if (binding.rbPaidClosingYes.isChecked) {
                binding.rbPaidClosingYes.setTypeface(null, Typeface.BOLD)
                binding.rbPaidClosingNo.setTypeface(null, Typeface.NORMAL)
            } else {
                binding.rbPaidClosingYes.setTypeface(null, Typeface.NORMAL)
            }
        }

        binding.rbPaidClosingNo.setOnClickListener {
            if (binding.rbPaidClosingNo.isChecked) {
                binding.rbPaidClosingNo.setTypeface(null, Typeface.BOLD)
                binding.rbPaidClosingYes.setTypeface(null, Typeface.NORMAL)
            }else{
                binding.rbPaidClosingNo.setTypeface(null, Typeface.NORMAL)
            }
        }
    }

    private fun setData(){

        list = arguments?.getParcelableArrayList(AppConstant.firstMortgage)!!
        if(list.size > 0) {
            list[0].firstMortgagePayment?.let {
                binding.edFirstMortgagePayment.setText(Math.round(it).toString())
            }
            list[0].unpaidFirstMortgagePayment?.let {
                binding.edUnpaidBalance.setText(Math.round(it).toString())
            }
            list[0].helocCreditLimit?.let {
                binding.edCreditLimit.setText(Math.round(it).toString())
                binding.layoutCreditLimit.visibility = View.VISIBLE
            }
            list[0].isHeloc?.let {
                if (it) {
                    binding.switchCreditLimit.isChecked = true
                    binding.tvHeloc.setTypeface(null, Typeface.BOLD)
                } else {
                    binding.switchCreditLimit.isChecked = false
                    binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
                }
            }
            list[0].propertyTaxesIncludeinPayment?.let { taxIncluded ->
                if (taxIncluded) {
                    binding.cbPropertyTaxes.isChecked = true
                    binding.cbPropertyTaxes.setTypeface(null, Typeface.BOLD)
                } else
                    binding.cbPropertyTaxes.isChecked = false
            }
            list[0].homeOwnerInsuranceIncludeinPayment?.let { insuranceIncluded ->
                if (insuranceIncluded) {
                    binding.cbHomeownwerInsurance.isChecked = true
                    binding.cbHomeownwerInsurance.setTypeface(null, Typeface.BOLD)
                } else
                    binding.cbHomeownwerInsurance.isChecked = false
            }
            list[0].floodInsuranceIncludeinPayment?.let { insuranceIncluded ->
                if (insuranceIncluded) {
                    binding.cbFloodInsurance.isChecked = true
                    binding.cbFloodInsurance.setTypeface(null, Typeface.BOLD)
                } else
                    binding.cbFloodInsurance.isChecked = false
            }
            list[0].paidAtClosing?.let {
                if (it == true) {
                    binding.rbPaidClosingYes.isChecked = true
                    binding.rbPaidClosingYes.setTypeface(null, Typeface.BOLD)
                } else {
                    binding.rbPaidClosingNo.isChecked = true
                    binding.rbPaidClosingNo.setTypeface(null, Typeface.BOLD)
                }
            }
        }

    }

//    override fun onClick(view: View?) {
//        when (view?.getId()) {
//            //R.id.backButton ->  requireActivity().onBackPressed()
            //R.id.btn_save ->  checkValidations()
//            R.id.first_morgtage_parentLayout-> {
//                HideSoftkeyboard.hide(requireActivity(), binding.firstMorgtageParentLayout)
//                super.removeFocusFromAllFields(binding.firstMorgtageParentLayout)
//            }
//            R.id.cb_flood_insurance ->
//                if (binding.cbFloodInsurance.isChecked) {
//                    binding.cbFloodInsurance.setTypeface(null, Typeface.BOLD)
//                }else{
//                    binding.cbFloodInsurance.setTypeface(null, Typeface.NORMAL)
//                }

//            R.id.cb_property_taxes ->
//                if (binding.cbPropertyTaxes.isChecked) {
//                    binding.cbPropertyTaxes.setTypeface(null, Typeface.BOLD)
//                }else{
//                    binding.cbPropertyTaxes.setTypeface(null, Typeface.NORMAL)
//                }
//
//            R.id.cb_homeownwer_insurance ->
//                if (binding.cbHomeownwerInsurance.isChecked) {
//                    binding.cbHomeownwerInsurance.setTypeface(null, Typeface.BOLD)
//                }else{
//                    binding.cbHomeownwerInsurance.setTypeface(null, Typeface.NORMAL)
//                }

//            R.id.switch_credit_limit ->
//                if(binding.switchCreditLimit.isChecked) {
//                    binding.layoutCreditLimit.visibility = View.VISIBLE
//                    binding.tvHeloc.setTypeface(null, Typeface.BOLD)
//                } else {
//                    binding.layoutCreditLimit.visibility = View.GONE
//                    binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
//                }
//
//            R.id.rb_ques_yes ->
//                if (binding.rbQuesYes.isChecked) {
//                    binding.rbQuesYes.setTypeface(null, Typeface.BOLD)
//                    binding.rbQuesNo.setTypeface(null, Typeface.NORMAL)
//                }else{
//                    binding.rbQuesYes.setTypeface(null, Typeface.NORMAL)
//                }
//
//            R.id.rb_ques_no ->
//                if (binding.rbQuesNo.isChecked) {
//                    binding.rbQuesNo.setTypeface(null, Typeface.BOLD)
//                    binding.rbQuesYes.setTypeface(null, Typeface.NORMAL)
//                }else{
//                    binding.rbQuesNo.setTypeface(null, Typeface.NORMAL)
//                }
//        }
//    }

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

    private fun checkValidations() {
        requireActivity().onBackPressed()
        /*val firstMortgagePayment: String = binding.edFirstMortgagePayment.text.toString()
        val unpaidBalance: String = binding.edUnpaidBalance.text.toString()
        val creditLimit: String = binding.edCreditLimit.text.toString()

        if (firstMortgagePayment.isEmpty() || firstMortgagePayment.length == 0) {
            setError(binding.layoutFirstPayment, getString(R.string.error_field_required))
        }
        if (unpaidBalance.isEmpty() || unpaidBalance.length == 0) {
            setError(binding.layoutUnpaidBalance, getString(R.string.error_field_required))
        }
        if (creditLimit.isEmpty() || creditLimit.length == 0) {
            setError(binding.layoutCreditLimit, getString(R.string.error_field_required))
        }
        if (firstMortgagePayment.isNotEmpty() && firstMortgagePayment.length > 0) {
            clearError(binding.layoutFirstPayment)
        }
        if (unpaidBalance.isNotEmpty() && unpaidBalance.length > 0) {
            clearError(binding.layoutUnpaidBalance)
        }
        if (creditLimit.isNotEmpty() && creditLimit.length > 0) {
            clearError(binding.layoutCreditLimit)
        } */
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