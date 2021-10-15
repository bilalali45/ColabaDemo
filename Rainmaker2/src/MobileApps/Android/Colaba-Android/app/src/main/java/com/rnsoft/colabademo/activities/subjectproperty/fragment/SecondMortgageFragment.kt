package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment

import com.rnsoft.colabademo.databinding.SubPropertySecondMortgageBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint

/**
 * Created by Anita Kiran on 9/9/2021.
 */
class SecondMortgageFragment : BaseFragment(), View.OnClickListener {

    private lateinit var binding : SubPropertySecondMortgageBinding
    private var list : ArrayList<SecondMortgageModel> = ArrayList()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = SubPropertySecondMortgageBinding.inflate(inflater, container, false)

        binding.backButton.setOnClickListener(this)
        binding.btnSave.setOnClickListener(this)
        binding.secMortgageParentLayout.setOnClickListener(this)
        binding.rbQuesOneYes.setOnClickListener(this)
        binding.rbQuesOneNo.setOnClickListener(this)
        binding.rbQuesTwoYes.setOnClickListener(this)
        binding.rbQuesTwoNo.setOnClickListener(this)
        binding.switchCreditLimit.setOnClickListener(this)

        setInputFields()
        super.addListeners(binding.root)


        list = arguments?.getParcelableArrayList(AppConstant.secMortgage)!!
        if(list.size > 0 ){
            list[0].secondMortgagePayment?.let { binding.edSecMortgagePayment.setText(it.toString()) }
            list[0].unpaidSecondMortgagePayment?.let { binding.edUnpaidBalance.setText(it.toString()) }
            list[0].helocCreditLimit?.let { binding.edCreditLimit.setText(it.toString()) }
            list[0].isHeloc?.let { binding.switchCreditLimit.isChecked = true }
            list[0].combineWithNewFirstMortgage?.let { isCombined->
                if(isCombined)
                    binding.rbQuesOneYes.isChecked = true
                else
                    binding.rbQuesOneNo.isChecked = false
            }
            list[0].paidAtClosing?.let { isPaid->
                if(isPaid)
                   binding.rbQuesOneYes.isChecked = true
                else
                    binding.rbQuesOneYes.isChecked = false
            }
            //it[0].wasSmTaken?.let {  }
        }

        return binding.root

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.backButton ->  requireActivity().onBackPressed()
            R.id.btn_save ->  checkValidations()
            R.id.sec_mortgage_parentLayout-> {
                HideSoftkeyboard.hide(requireActivity(), binding.secMortgageParentLayout)
                super.removeFocusFromAllFields(binding.secMortgageParentLayout)
            }
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