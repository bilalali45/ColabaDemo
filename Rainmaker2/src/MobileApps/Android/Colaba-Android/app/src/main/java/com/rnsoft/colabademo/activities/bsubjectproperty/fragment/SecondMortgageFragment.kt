package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.SubPropertySecondMortgageBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/9/2021.
 */
class SecondMortgageFragment : Fragment(), View.OnClickListener {

    private lateinit var binding : SubPropertySecondMortgageBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = SubPropertySecondMortgageBinding.inflate(inflater, container, false)

        binding.backButton.setOnClickListener(this)
        binding.btnSave.setOnClickListener(this)
        binding.parentLayout.setOnClickListener(this)
        binding.rbQuesOneYes.setOnClickListener(this)
        binding.rbQuesOneNo.setOnClickListener(this)
        binding.rbQuesTwoYes.setOnClickListener(this)
        binding.rbQuesTwoNo.setOnClickListener(this)
        binding.switchCreditLimit.setOnClickListener(this)

        setInputFields()

        return binding.root

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.backButton ->  requireActivity().onBackPressed()
            R.id.btn_save ->  checkValidations()
            R.id.parentLayout-> HideSoftkeyboard.hide(requireActivity(), binding.parentLayout)
            R.id.rb_ques_one_yes ->
                if (binding.rbQuesOneYes.isChecked) {
                    binding.rbQuesOneYes.setTypeface(null, Typeface.BOLD)
                    binding.rbQuesOneNo.setTypeface(null, Typeface.NORMAL)
                }else{
                    binding.rbQuesOneYes.setTypeface(null, Typeface.NORMAL)
                }

            R.id.rb_ques_one_no ->
                if (binding.rbQuesOneNo.isChecked) {
                    binding.rbQuesOneNo.setTypeface(null, Typeface.BOLD)
                    binding.rbQuesOneYes.setTypeface(null, Typeface.NORMAL)

                }else{
                    binding.rbQuesOneNo.setTypeface(null, Typeface.NORMAL)
                }

            R.id.rb_ques_two_yes ->
                if (binding.rbQuesTwoYes.isChecked) {
                    binding.rbQuesTwoYes.setTypeface(null, Typeface.BOLD)
                    binding.rbQuesTwoNo.setTypeface(null, Typeface.NORMAL)
                }else{
                    binding.rbQuesTwoYes.setTypeface(null, Typeface.NORMAL)
                }

            R.id.rb_ques_two_no ->
                if (binding.rbQuesTwoNo.isChecked) {
                    binding.rbQuesTwoNo.setTypeface(null, Typeface.BOLD)
                    binding.rbQuesTwoYes.setTypeface(null, Typeface.NORMAL)
                }else{
                    binding.rbQuesTwoNo.setTypeface(null, Typeface.NORMAL)
                }

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
        binding.edSecMortgagePayment.setOnFocusChangeListener(MyCustomFocusListener(binding.edSecMortgagePayment, binding.layoutSecPayment, requireContext()))
        binding.edUnpaidBalance.setOnFocusChangeListener(MyCustomFocusListener(binding.edUnpaidBalance, binding.layoutUnpaidBalance, requireContext()))
        binding.edCreditLimit.setOnFocusChangeListener(MyCustomFocusListener(binding.edCreditLimit, binding.layoutCreditLimit, requireContext()))

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