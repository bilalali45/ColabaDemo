package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope

import com.rnsoft.colabademo.databinding.RealEstateSecondMortgageBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat

class RealEstateSecondMortgage : BaseFragment(), View.OnClickListener {

    private lateinit var binding : RealEstateSecondMortgageBinding
    private val viewModel : RealEstateViewModel by activityViewModels()
    val token : String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhZGlxQHJhaW5zb2Z0Zm4uY29tIiwiRmlyc3ROYW1lIjoiU2FkaXEiLCJMYXN0TmFtZSI6Ik1hY2tub2ppYSIsIlRlbmFudENvZGUiOiJhaGNsZW5kaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTUNVIiwiZXhwIjoxNjM0NzUzMjYxLCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.bHZwTohB4toe2JGgKVNeaOoOh8HIaygh8WqmGpTPzO4"


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = RealEstateSecondMortgageBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)


        val title = arguments?.getString(AppConstant.address).toString()
        title.let {
            binding.borrowerPurpose.setText(title)
        }

        binding.backButton.setOnClickListener(this)
        binding.btnSave.setOnClickListener(this)
        binding.layoutRealestateSecMortgage.setOnClickListener(this)
        binding.rbPaidClosingYes.setOnClickListener(this)
        binding.rbPaidClosingNo.setOnClickListener(this)
        binding.switchCreditLimit.setOnClickListener(this)

        setInputFields()
        getSecondMortgageDetails()

        return binding.root

    }

    private fun getSecondMortgageDetails() {
        lifecycleScope.launchWhenStarted {
            viewModel.getSecondMortgageDetails(token, 5, 1003)
            viewModel.secondMortgageDetails.observe(viewLifecycleOwner, {
                if (it != null) {
                    it.data?.secondMortgagePayment?.let {
                        binding.edSecMortgagePayment.setText(Math.round(it).toString())
                        CustomMaterialFields.setColor(binding.layoutSecPayment,R.color.grey_color_two,requireActivity())

                    }
                    it.data?.unpaidSecondMortgagePayment?.let {
                        binding.edUnpaidBalance.setText(Math.round(it).toString())
                        CustomMaterialFields.setColor(binding.layoutUnpaidBalance,R.color.grey_color_two,requireActivity())
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
                            binding.rbPaidClosingNo.isChecked = false
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
            R.id.layout_realestate_sec_mortgage-> {
                HideSoftkeyboard.hide(requireActivity(), binding.layoutRealestateSecMortgage)
                super.removeFocusFromAllFields(binding.layoutRealestateSecMortgage)
            }

            R.id.rb_paid_closing_yes ->
                if (binding.rbPaidClosingYes.isChecked) {
                    binding.rbPaidClosingYes.setTypeface(null, Typeface.BOLD)
                    binding.rbPaidClosingNo.setTypeface(null, Typeface.NORMAL)
                } else
                    binding.rbPaidClosingYes.setTypeface(null, Typeface.NORMAL)


            R.id.rb_paid_closing_no ->
                if (binding.rbPaidClosingNo.isChecked) {
                    binding.rbPaidClosingNo.setTypeface(null, Typeface.BOLD)
                    binding.rbPaidClosingYes.setTypeface(null, Typeface.NORMAL)
                } else
                    binding.rbPaidClosingNo.setTypeface(null, Typeface.NORMAL)


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