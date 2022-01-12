package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.addCallback
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.SubPropertySecondMortgageBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.radioUnSelectColor
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setRadioColor

import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import java.lang.NullPointerException

/**
 * Created by Anita Kiran on 9/9/2021.
 */
@AndroidEntryPoint
class SecondMortgageFragment : BaseFragment(), View.OnClickListener {

    private lateinit var binding : SubPropertySecondMortgageBinding
    var secondMortgageModel: SecondMortgageModel? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = SubPropertySecondMortgageBinding.inflate(inflater, container, false)

        binding.backButton.setOnClickListener(this)
        binding.btnSave.setOnClickListener(this)
        binding.secMortgageParentLayout.setOnClickListener(this)

        setInputFields()
        setData()
        super.addListeners(binding.root)

        requireActivity().onBackPressedDispatcher.addCallback {
            findNavController().popBackStack()
        }

        return binding.root

    }

    private fun setData(){
        try{
            secondMortgageModel = arguments?.getParcelable(AppConstant.secMortgage)!!
            secondMortgageModel?.let {
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
                    }
                }

                it.wasSmTaken?.let { taken ->
                    if (taken) {
                        binding.radioWasMortgageTakenYes.isChecked = true
                    } else {
                        binding.radioWasMortgageTakenNo.isChecked = true
                    }
                }
                it.combineWithNewFirstMortgage?.let { isCombined ->
                    if (isCombined) {
                        binding.rbYesCombinedFirstMortgage.isChecked = true
                    } else {
                        binding.rbNoCombinedFirstMortgage.isChecked = true
                    }
                }
            }
        } catch (e: NullPointerException){
            Log.e("Exception", "catch")
        }

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.backButton -> findNavController().popBackStack()
            R.id.btn_save -> saveData()
            R.id.sec_mortgage_parentLayout -> {
                HideSoftkeyboard.hide(requireActivity(), binding.secMortgageParentLayout)
                super.removeFocusFromAllFields(binding.secMortgageParentLayout)
            }
        }
    }

    private fun saveData() {

        // first mortgage
        val secMortgagePayment = binding.edSecMortgagePayment.text.toString().trim()
        var newSecMortgagePayment = if(secMortgagePayment.length > 0) secMortgagePayment.replace(",".toRegex(), "") else "0"

        // second mortgage
        val unpaidBalance = binding.edUnpaidBalance.text.toString().trim()
        var newUnpaidBalance = if(unpaidBalance.length > 0) unpaidBalance.replace(",".toRegex(), "") else "0"

        val creditLimit = binding.edCreditLimit.text.toString().trim()
        var newCreditLimit = if(creditLimit.length > 0) creditLimit.replace(",".toRegex(), "") else "0"

        val isHeloc = if(binding.switchCreditLimit.isChecked)true else false
        var isCombinedWithFirstMortgage : Boolean? = null
        if(binding.rbYesCombinedFirstMortgage.isChecked)
            isCombinedWithFirstMortgage = true

        if(binding.rbNoCombinedFirstMortgage.isChecked)
            isCombinedWithFirstMortgage = false

        var isMortgageTaken : Boolean? = null
        if(binding.radioWasMortgageTakenYes.isChecked)
            isMortgageTaken = true

        if(binding.radioWasMortgageTakenNo.isChecked)
            isMortgageTaken = false


        val secMortgageDetail = SecondMortgageModel(secondMortgagePayment = newSecMortgagePayment.toDouble(),unpaidSecondMortgagePayment = newUnpaidBalance.toDouble(),
            helocCreditLimit = newCreditLimit.toDoubleOrNull(), isHeloc = isHeloc,combineWithNewFirstMortgage = isCombinedWithFirstMortgage,wasSmTaken = isMortgageTaken)

        //viewModel.addSecMortgageModel(secMortgageDetail)
        findNavController().previousBackStackEntry?.savedStateHandle?.set(AppConstant.secMortgage,secMortgageDetail)
        findNavController().popBackStack()

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


        binding.rbYesCombinedFirstMortgage.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                setRadioColor(binding.rbYesCombinedFirstMortgage, requireContext())
            else
                radioUnSelectColor(binding.rbYesCombinedFirstMortgage, requireContext())
        }

        binding.rbNoCombinedFirstMortgage.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked)
                setRadioColor(binding.rbNoCombinedFirstMortgage, requireContext())
            else
                radioUnSelectColor(binding.rbNoCombinedFirstMortgage, requireContext())
        }

        binding.radioWasMortgageTakenYes.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked)
                setRadioColor(binding.radioWasMortgageTakenYes, requireContext())
            else
                radioUnSelectColor(binding.radioWasMortgageTakenYes, requireContext())
        }

        binding.radioWasMortgageTakenNo.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked)
                setRadioColor(binding.radioWasMortgageTakenNo, requireContext())
            else
                radioUnSelectColor(binding.radioWasMortgageTakenNo, requireContext())
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
}