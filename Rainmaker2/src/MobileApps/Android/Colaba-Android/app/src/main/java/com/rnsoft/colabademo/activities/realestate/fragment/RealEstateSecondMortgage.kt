package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment

import com.rnsoft.colabademo.databinding.RealEstateSecondMortgageBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat

class RealEstateSecondMortgage : BaseFragment(), View.OnClickListener {

    private lateinit var binding : RealEstateSecondMortgageBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = RealEstateSecondMortgageBinding.inflate(inflater, container, false)

        val title = arguments?.getString(AppConstant.address).toString()
        title.let {
            binding.borrowerPurpose.setText(title)
        }

        binding.backButton.setOnClickListener(this)
        binding.btnSave.setOnClickListener(this)
        binding.layoutRealestateSecMortgage.setOnClickListener(this)
        binding.rbQuesYes.setOnClickListener(this)
        binding.rbQuesNo.setOnClickListener(this)
        binding.switchCreditLimit.setOnClickListener(this)

        setInputFields()
        super.addListeners(binding.root)
        return binding.root

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.backButton ->  requireActivity().onBackPressed()
            R.id.btn_save ->  checkValidations()
            R.id.layout_realestate_sec_mortgage-> {
                HideSoftkeyboard.hide(requireActivity(), binding.layoutRealestateSecMortgage)
                super.removeFocusFromAllFields(binding.layoutRealestateSecMortgage)
            }

            R.id.rb_ques_yes ->
                if (binding.rbQuesYes.isChecked) {
                    binding.rbQuesYes.setTypeface(null, Typeface.BOLD)
                    binding.rbQuesNo.setTypeface(null, Typeface.NORMAL)
                } else
                    binding.rbQuesYes.setTypeface(null, Typeface.NORMAL)


            R.id.rb_ques_no ->
                if (binding.rbQuesNo.isChecked) {
                    binding.rbQuesNo.setTypeface(null, Typeface.BOLD)
                    binding.rbQuesYes.setTypeface(null, Typeface.NORMAL)
                } else
                    binding.rbQuesNo.setTypeface(null, Typeface.NORMAL)


            R.id.switch_credit_limit ->
                if(binding.switchCreditLimit.isChecked) {
                    binding.layoutCreditLimit.visibility = View.VISIBLE
                    binding.tvHeloc.setTypeface(null, Typeface.BOLD)
                } else {
                    binding.layoutCreditLimit.visibility = View.GONE
                    binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
                }
        }
    }


    private fun setInputFields(){

        // set lable focus
        binding.edSecMortgagePayment.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edSecMortgagePayment, binding.layoutSecPayment, requireContext()))
        binding.edUnpaidBalance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edUnpaidBalance, binding.layoutUnpaidBalance, requireContext()))
        binding.edCreditLimit.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edCreditLimit, binding.layoutCreditLimit, requireContext()))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutSecPayment,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutUnpaidBalance,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutCreditLimit,requireContext())

        binding.edSecMortgagePayment.addTextChangedListener(NumberTextFormat(binding.edSecMortgagePayment))
        binding.edUnpaidBalance.addTextChangedListener(NumberTextFormat(binding.edUnpaidBalance))
        binding.edCreditLimit.addTextChangedListener(NumberTextFormat(binding.edCreditLimit))

    }


    private fun checkValidations(){
        requireActivity().onBackPressed()
    }
}