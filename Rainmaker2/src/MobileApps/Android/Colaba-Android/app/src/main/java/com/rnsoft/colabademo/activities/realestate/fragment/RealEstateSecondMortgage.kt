package com.rnsoft.colabademo

import android.content.SharedPreferences
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
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode

class RealEstateSecondMortgage : BaseFragment(), View.OnClickListener {

    private lateinit var binding : RealEstateSecondMortgageBinding
    private val viewModel : RealEstateViewModel by activityViewModels()
    lateinit var sharedPreferences : SharedPreferences

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
        viewModel.secondMortgageDetails.observe(viewLifecycleOwner, {
            if (it != null) {
                it.data?.secondMortgagePayment?.let {
                    binding.edSecMortgagePayment.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutSecPayment, R.color.grey_color_two, requireActivity()) }

                it.data?.unpaidSecondMortgagePayment?.let {
                    binding.edUnpaidBalance.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(
                        binding.layoutUnpaidBalance,
                        R.color.grey_color_two,
                        requireActivity()
                    )
                }
                it.data?.isHeloc?.let {
                    if (it == true) {
                        binding.switchCreditLimit.isChecked = true
                        binding.tvHeloc.setTypeface(null, Typeface.BOLD)
                    } else {
                        binding.switchCreditLimit.isChecked = false
                        binding.tvHeloc.setTypeface(null, Typeface.NORMAL)
                    }
                }
                it.data?.helocCreditLimit?.let {
                    binding.edCreditLimit.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(
                        binding.layoutCreditLimit,
                        R.color.grey_color_two, requireActivity())
                }
                it.data?.paidAtClosing?.let {
                    if (it == true) {
                        binding.rbPaidClosingYes.isChecked = true
                        binding.rbPaidClosingYes.setTypeface(null, Typeface.BOLD)
                    } else {
                        binding.rbPaidClosingNo.isChecked = true
                        binding.rbPaidClosingNo.setTypeface(null, Typeface.BOLD)
                    }
                }
                if(it.code.equals(AppConstant.RESPONSE_CODE_SUCCESS)){
                    hideLoader()
                }
            }
            hideLoader()
        })

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

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onErrorReceived(event: WebServiceErrorEvent) {
        if(event.isInternetError)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )
        else
            if(event.errorResult!=null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )
    }

    private fun hideLoader(){
        val  activity = (activity as? RealEstateActivity)
        activity?.binding?.loaderRealEstate?.visibility = View.GONE
    }

}