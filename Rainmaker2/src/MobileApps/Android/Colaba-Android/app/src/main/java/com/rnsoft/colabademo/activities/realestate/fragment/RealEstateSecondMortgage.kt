package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.RealEstateSecondMortgageBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.radioUnSelectColor
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setRadioColor

import com.rnsoft.colabademo.utils.NumberTextFormat
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.lang.NullPointerException

class RealEstateSecondMortgage : BaseFragment(), View.OnClickListener {

    private lateinit var binding : RealEstateSecondMortgageBinding
    var secondMortgageModel = SecondMortgageModel()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = RealEstateSecondMortgageBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)


        val title = arguments?.getString(AppConstant.address).toString()
        title.let {
            if(it != "null")
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
        try {
            secondMortgageModel = arguments?.getParcelable(AppConstant.secMortgage)!!
            secondMortgageModel.let {
                it.secondMortgagePayment?.let {
                    binding.edSecMortgagePayment.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(
                        binding.layoutSecPayment,
                        R.color.grey_color_two,
                        requireActivity()
                    )
                }

                it.unpaidSecondMortgagePayment?.let {
                    binding.edUnpaidBalance.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(
                        binding.layoutUnpaidBalance,
                        R.color.grey_color_two,
                        requireActivity()
                    )
                }
                it.isHeloc?.let { isHeloc ->
                    if (isHeloc == true) {
                        binding.switchCreditLimit.isChecked = true
                        binding.tvHeloc.setTypeface(null, Typeface.BOLD)
                        binding.layoutCreditLimit.visibility = View.VISIBLE

                        it.helocCreditLimit?.let {
                            binding.edCreditLimit.setText(Math.round(it).toString())
                            CustomMaterialFields.setColor(
                                binding.layoutCreditLimit,
                                R.color.grey_color_two,
                                requireActivity()
                            )
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
        } catch (e :NullPointerException){

        }
    }

    private fun saveData() {

        // first mortgage
        val secMortgagePayment = binding.edSecMortgagePayment.text.toString().trim()
        var newSecMortgagePayment = if(secMortgagePayment.length > 0) secMortgagePayment.replace(",".toRegex(), "") else null

        // second mortgage
        val unpaidBalance = binding.edUnpaidBalance.text.toString().trim()
        var newUnpaidBalance = if(unpaidBalance.length > 0) unpaidBalance.replace(",".toRegex(), "") else null

        val creditLimit = binding.edCreditLimit.text.toString().trim()
        var newCreditLimit = if(creditLimit.length > 0) creditLimit.replace(",".toRegex(), "") else null

        val isHeloc = if(binding.switchCreditLimit.isChecked)true else false
        var isPaidAtClosing : Boolean? = null

        if(binding.rbPaidClosingYes.isChecked)
            isPaidAtClosing = true

        if(binding.rbPaidClosingNo.isChecked)
            isPaidAtClosing = false

        val secMortgageDetail = SecondMortgageModel(secondMortgagePayment = newSecMortgagePayment?.toDouble(),unpaidSecondMortgagePayment = newUnpaidBalance?.toDouble(),
            helocCreditLimit = newCreditLimit?.toDoubleOrNull(), isHeloc = isHeloc,paidAtClosing = isPaidAtClosing,wasSmTaken = null)

        findNavController().previousBackStackEntry?.savedStateHandle?.set(AppConstant.secMortgage,secMortgageDetail)
        findNavController().popBackStack()

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.backButton ->  requireActivity().onBackPressed()
            R.id.btn_save ->  saveData()
            R.id.layout_realestate_sec_mortgage-> {
                HideSoftkeyboard.hide(requireActivity(), binding.layoutRealestateSecMortgage)
                super.removeFocusFromAllFields(binding.layoutRealestateSecMortgage)
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


        binding.rbPaidClosingYes.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked)
                setRadioColor(binding.rbPaidClosingYes, requireContext())
            else
               radioUnSelectColor(binding.rbPaidClosingYes, requireContext())
        }

        binding.rbPaidClosingNo.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked)
                setRadioColor(binding.rbPaidClosingNo, requireContext())
            else
                radioUnSelectColor(binding.rbPaidClosingNo, requireContext())
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

}