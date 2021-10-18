package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.content.res.AppCompatResources
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.google.android.material.textfield.TextInputLayout

import com.rnsoft.colabademo.databinding.FirstMortgageLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import timber.log.Timber

/**
 * Created by Anita Kiran on 9/9/2021.
 */
class RealEstateFirstMortgage : BaseFragment(),View.OnClickListener {

    private lateinit var binding : FirstMortgageLayoutBinding
    private val viewModel : RealEstateViewModel by activityViewModels()
    val token : String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhZGlxQHJhaW5zb2Z0Zm4uY29tIiwiRmlyc3ROYW1lIjoiU2FkaXEiLCJMYXN0TmFtZSI6Ik1hY2tub2ppYSIsIlRlbmFudENvZGUiOiJhaGNsZW5kaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTUNVIiwiZXhwIjoxNjM0NzUzMjYxLCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.bHZwTohB4toe2JGgKVNeaOoOh8HIaygh8WqmGpTPzO4"


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FirstMortgageLayoutBinding.inflate(inflater, container, false)

        val title = arguments?.getString(AppConstant.address).toString()
        title.let {
            binding.borrowerPurpose.setText(title)
        }

        binding.backButton.setOnClickListener(this)
        binding.btnSave.setOnClickListener(this)
        binding.firstMorgtageParentLayout.setOnClickListener(this)
        binding.cbFloodInsurance.setOnClickListener(this)
        binding.cbHomeownwerInsurance.setOnClickListener(this)
        binding.cbPropertyTaxes.setOnClickListener(this)
        binding.switchCreditLimit.setOnClickListener(this)
        binding.rbPaidClosingYes.setOnClickListener(this)
        binding.rbPaidClosingNo.setOnClickListener(this)

        setInputFields()
        getFirstMortgageDetails()
        super.addListeners(binding.root)

        return binding.root
    }

    private fun getFirstMortgageDetails() {
        lifecycleScope.launchWhenStarted {
            viewModel.getFirstMortgageDetails(token, 5, 1003)
            viewModel.firstMortgageDetails.observe(viewLifecycleOwner, {
                if (it != null) {
                    it.data?.firstMortgagePayment?.let {
                        binding.edFirstMortgagePayment.setText(Math.round(it).toString())
                        CustomMaterialFields.setColor(binding.layoutFirstPayment,R.color.grey_color_two,requireActivity())
                    }
                    it.data?.unpaidFirstMortgagePayment?.let {
                        binding.edUnpaidBalance.setText(Math.round(it).toString())
                        CustomMaterialFields.setColor(binding.layoutUnpaidBalance,R.color.grey_color_two,requireActivity())
                    }
                    it.data?.floodInsuranceIncludeinPayment?.let {
                        if(it==true) {
                            binding.cbFloodInsurance.isChecked = true
                            binding.cbFloodInsurance.setTypeface(null, Typeface.BOLD)
                        } else {
                            binding.cbFloodInsurance.isChecked = false
                            binding.cbFloodInsurance.setTypeface(null, Typeface.NORMAL)
                        }
                    }
                    it.data?.propertyTaxesIncludeinPayment?.let {
                        if(it==true) {
                            binding.cbPropertyTaxes.isChecked = true
                            binding.cbPropertyTaxes.setTypeface(null, Typeface.BOLD)
                        } else {
                            binding.cbPropertyTaxes.isChecked = false
                            binding.cbPropertyTaxes.setTypeface(null, Typeface.NORMAL)
                        }
                    }
                    it.data?.homeOwnerInsuranceIncludeinPayment?.let {
                        if(it==true) {
                            binding.cbHomeownwerInsurance.isChecked = true
                            binding.cbHomeownwerInsurance.setTypeface(null, Typeface.BOLD)
                        } else {
                            binding.cbHomeownwerInsurance.isChecked = false
                            binding.cbHomeownwerInsurance.setTypeface(null, Typeface.NORMAL)
                        }
                    }

                    it.data?.isHeloc?.let {
                        if(it==true) {
                            binding.switchCreditLimit.isChecked = true
                            binding.tvHeloc.setTypeface(null, Typeface.BOLD)
                        } else {
                            binding.switchCreditLimit.isChecked = false
                            binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
                        }
                    }
                    it.data?.helocCreditLimit?.let {
                        binding.edCreditLimit.setText(Math.round(it).toString())
                        CustomMaterialFields.setColor(binding.layoutCreditLimit,R.color.grey_color_two,requireActivity())
                    }
                    it.data?.paidAtClosing?.let {
                        if(it==true) {
                            binding.rbPaidClosingYes.isChecked = true
                            binding.rbPaidClosingYes.setTypeface(null, Typeface.BOLD)
                        } else {
                            binding.rbPaidClosingNo.isChecked = true
                            binding.rbPaidClosingNo.setTypeface(null, Typeface.BOLD)
                        }
                    }

                }
            })
        }
    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.backButton ->  requireActivity().onBackPressed()
            R.id.btn_save ->  checkValidations()
            R.id.first_morgtage_parentLayout-> {
                HideSoftkeyboard.hide(requireActivity(), binding.firstMorgtageParentLayout)
                super.removeFocusFromAllFields(binding.firstMorgtageParentLayout)
            }
            R.id.cb_flood_insurance ->
                if (binding.cbFloodInsurance.isChecked) {
                    binding.cbFloodInsurance.setTypeface(null, Typeface.BOLD)
                }else{
                    binding.cbFloodInsurance.setTypeface(null, Typeface.NORMAL)
                }

            R.id.cb_property_taxes ->
                if (binding.cbPropertyTaxes.isChecked) {
                    binding.cbPropertyTaxes.setTypeface(null, Typeface.BOLD)
                }else{
                    binding.cbPropertyTaxes.setTypeface(null, Typeface.NORMAL)
                }

            R.id.cb_homeownwer_insurance ->
                if (binding.cbHomeownwerInsurance.isChecked) {
                    binding.cbHomeownwerInsurance.setTypeface(null, Typeface.BOLD)
                }else{
                    binding.cbHomeownwerInsurance.setTypeface(null, Typeface.NORMAL)
                }

            R.id.switch_credit_limit ->
                if(binding.switchCreditLimit.isChecked) {
                    binding.layoutCreditLimit.visibility = View.VISIBLE
                    binding.tvHeloc.setTypeface(null, Typeface.BOLD)
                } else {
                    binding.layoutCreditLimit.visibility = View.GONE
                    binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
                }

            R.id.rb_paid_closing_yes ->
                if (binding.rbPaidClosingYes.isChecked) {
                    binding.rbPaidClosingYes.setTypeface(null, Typeface.BOLD)
                    binding.rbPaidClosingNo.setTypeface(null, Typeface.NORMAL)
                }else{
                    binding.rbPaidClosingYes.setTypeface(null, Typeface.NORMAL)
                }

            R.id.rb_paid_closing_no ->
                if (binding.rbPaidClosingNo.isChecked) {
                    binding.rbPaidClosingNo.setTypeface(null, Typeface.BOLD)
                    binding.rbPaidClosingYes.setTypeface(null, Typeface.NORMAL)
                }else{
                    binding.rbPaidClosingNo.setTypeface(null, Typeface.NORMAL)
                }
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