package com.rnsoft.colabademo

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.view.isVisible
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.AppConstant.INCOME_BONUS
import com.rnsoft.colabademo.AppConstant.INCOME_COMMISSION
import com.rnsoft.colabademo.AppConstant.INCOME_OVERTIME
import com.rnsoft.colabademo.AppConstant.OWNERSHIP_INTEREST
import com.rnsoft.colabademo.AppConstant.PAY_TYPE_HOURLY
import com.rnsoft.colabademo.AppConstant.PAY_TYPE_SALARY
import com.rnsoft.colabademo.AppConstant.commission_value
import com.rnsoft.colabademo.AppConstant.overtime_value
import com.rnsoft.colabademo.databinding.IncomeSourcesLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.clearError
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setError
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setPercentagePrefix
import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 1/13/2022.
 */
class IncomeFiguresFragment : BaseFragment() {

    private lateinit var binding: IncomeSourcesLayoutBinding
    var title: String = ""
    private var hourlyModel = HourlyModel()

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = IncomeSourcesLayoutBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)

        setInputFeilds()
        setData()

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.btnSave.setOnClickListener {
            saveData()
        }

        binding.layoutParentFigures.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(), binding.layoutParentFigures)
            super.removeFocusFromAllFields(binding.layoutParentFigures)
        }

        return  binding.root
    }

    private fun setData(){
        arguments?.let { arguments ->
            title = arguments.getString(AppConstant.TOOLBAR_TITLE)!!
            binding.incomeTypeHeading.setText(title)
            binding.borrowerName.setText(arguments.getString(AppConstant.borrowerName))

            if(title == OWNERSHIP_INTEREST){
                binding.layoutOwnershipPercentage.visibility = View.VISIBLE
                val percentageValue = arguments.getString(AppConstant.OWNERSHIP_INTEREST_PERCENTAGE)
                percentageValue?.let {
                    if(it.isNotEmpty()) {
                        binding.edOwnershipPercent.setText(it)
                        CustomMaterialFields.setColor(binding.layoutOwnershipPercentage,R.color.grey_color_two, requireContext())
                    }
                }
            }

            if(title == PAY_TYPE_SALARY){
                binding.layoutBaseSalary.visibility = View.VISIBLE
                val value = arguments.getString(AppConstant.salary_value)
                value?.let {
                    if(it.isNotEmpty()) {
                        binding.edAnnualSalary.setText(it)
                        CustomMaterialFields.setColor(binding.layoutBaseSalary,R.color.grey_color_two, requireContext())
                    }
                }
            }

            if(title == PAY_TYPE_HOURLY){
                binding.layoutHourly.visibility = View.VISIBLE
                hourlyModel = arguments?.getParcelable(AppConstant.hourly)!!
                hourlyModel.hourlyRate?.let {
                    if(it.isNotEmpty()) {
                        binding.edHourlyRate.setText(it)
                        CustomMaterialFields.setColor(binding.layoutBaseSalary,R.color.grey_color_two, requireContext())
                    }
                }

                hourlyModel.avgWeeks?.let {
                    if(it.isNotEmpty()){
                        binding.editTextWeeklyHours.setText(it)
                        CustomMaterialFields.setColor(binding.layoutWeeklyHours,R.color.grey_color_two, requireContext())
                    }
                }
            }

            if(title == INCOME_BONUS){
                binding.layoutBonusIncome.visibility = View.VISIBLE
                val bonus = arguments?.getString(AppConstant.bonus_value)
                bonus?.let {
                    if(it.isNotEmpty()) {
                        binding.editTextBonusIncome.setText(it)
                        CustomMaterialFields.setColor(binding.layoutBonusIncome,R.color.grey_color_two, requireContext())
                    }
                }
            }

            if(title == INCOME_OVERTIME){
                binding.layoutOvertimeIncome.visibility = View.VISIBLE
                val bonus = arguments?.getString(overtime_value)
                bonus?.let {
                    if(it.isNotEmpty()) {
                        binding.editTextOvertimeIncome.setText(it)
                        CustomMaterialFields.setColor(binding.layoutOvertimeIncome,R.color.grey_color_two, requireContext())
                    }
                }
            }

            if(title == INCOME_COMMISSION){
                binding.layoutCommIncome.visibility = View.VISIBLE
                val bonus = arguments?.getString(commission_value)
                bonus?.let {
                    if(it.isNotEmpty()) {
                        binding.editTextCommission.setText(it)
                        CustomMaterialFields.setColor(binding.layoutCommIncome,R.color.grey_color_two, requireContext())
                    }
                }
            }
        }
    }

    private fun saveData(){

        if (binding.layoutOwnershipPercentage.isVisible) {
            val ownershipPercentage = binding.edOwnershipPercent.text.toString()
            if(ownershipPercentage.length == 0)
                setError(binding.layoutOwnershipPercentage, getString(R.string.error_field_required), requireActivity())

            if (ownershipPercentage.length > 0){
                clearError(binding.layoutOwnershipPercentage, requireActivity())
                findNavController().previousBackStackEntry?.savedStateHandle?.set(OWNERSHIP_INTEREST, ownershipPercentage)
                findNavController().popBackStack()
            }
        }

        if (binding.layoutBaseSalary.isVisible) {
            val salary = binding.edAnnualSalary.text.toString()
            if(salary.length == 0)
                setError(binding.layoutBaseSalary, getString(R.string.error_field_required), requireActivity())

            if (salary.length > 0){
                clearError(binding.layoutBaseSalary, requireActivity())

                findNavController().previousBackStackEntry?.savedStateHandle?.set(PAY_TYPE_SALARY, salary)
                findNavController().popBackStack()
            }
        }

        if(binding.layoutHourly.isVisible){
            if(binding.edHourlyRate.text.toString().trim().length == 0){
                setError(binding.layoutHourlyRate, getString(R.string.error_field_required), requireActivity())
            }
            if(binding.edHourlyRate.text.toString().trim().length >0){
                clearError(binding.layoutHourlyRate, requireActivity())
            }
            if(binding.editTextWeeklyHours.text.toString().trim().length == 0){
                setError(binding.layoutWeeklyHours, getString(R.string.error_field_required), requireActivity())
            }
            if(binding.editTextWeeklyHours.text.toString().trim().length >0){
                clearError(binding.layoutWeeklyHours, requireActivity())
            }

            if(binding.edHourlyRate.text.toString().trim().length >0 && binding.editTextWeeklyHours.text.toString().trim().length >0){
                val model = HourlyModel(binding.edHourlyRate.text.toString().trim(),binding.editTextWeeklyHours.text.toString().trim())
                findNavController().previousBackStackEntry?.savedStateHandle?.set(AppConstant.hourly,model)
                findNavController().popBackStack()
            }
        }


        if(binding.layoutBonusIncome.isVisible){
            val incomeBonus = binding.editTextBonusIncome.text.toString()
            if(incomeBonus.length ==0){
                setError(binding.layoutBonusIncome, getString(R.string.error_field_required), requireActivity())
            }
            if(incomeBonus.length > 0){
                clearError(binding.layoutBonusIncome, requireActivity())
                findNavController().previousBackStackEntry?.savedStateHandle?.set(INCOME_BONUS, incomeBonus)
                findNavController().popBackStack()
            }
        }


        if(binding.layoutCommIncome.isVisible){
            val incomeCommission = binding.editTextCommission.text.toString()
            if(incomeCommission.length ==0){
                setError(binding.layoutCommIncome, getString(R.string.error_field_required), requireActivity())
            }
            if(incomeCommission.length > 0){
                clearError(binding.layoutCommIncome, requireActivity())
                findNavController().previousBackStackEntry?.savedStateHandle?.set(INCOME_COMMISSION, incomeCommission)
                findNavController().popBackStack()
            }
        }


        if(binding.layoutOvertimeIncome.isVisible){
            val incomeOvertime = binding.editTextOvertimeIncome.text.toString()
            if(incomeOvertime.length ==0){
                setError(binding.layoutOvertimeIncome, getString(R.string.error_field_required), requireActivity())
            }
            if(incomeOvertime.length > 0){
                clearError(binding.layoutOvertimeIncome, requireActivity())
                findNavController().previousBackStackEntry?.savedStateHandle?.set(INCOME_OVERTIME, incomeOvertime)
                findNavController().popBackStack()
            }
        }



    }

    private fun setInputFeilds(){
        binding.edOwnershipPercent.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOwnershipPercent, binding.layoutOwnershipPercentage, requireContext()))
        binding.edAnnualSalary.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edAnnualSalary, binding.layoutBaseSalary, requireContext()))
        binding.edHourlyRate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edHourlyRate, binding.layoutHourlyRate, requireContext()))
        binding.editTextWeeklyHours.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextWeeklyHours, binding.layoutWeeklyHours, requireContext()))
        binding.editTextBonusIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextBonusIncome, binding.layoutBonusIncome, requireContext()))
        binding.editTextOvertimeIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextOvertimeIncome, binding.layoutOvertimeIncome, requireContext()))
        binding.editTextCommission.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextCommission, binding.layoutCommIncome, requireContext()))

        // comma format
        binding.edAnnualSalary.addTextChangedListener(NumberTextFormat(binding.edAnnualSalary))
        binding.edHourlyRate.addTextChangedListener(NumberTextFormat(binding.edHourlyRate))
        binding.editTextBonusIncome.addTextChangedListener(NumberTextFormat(binding.editTextBonusIncome))
        binding.editTextCommission.addTextChangedListener(NumberTextFormat(binding.editTextCommission))
        binding.editTextOvertimeIncome.addTextChangedListener(NumberTextFormat(binding.editTextOvertimeIncome))

        // prefix
        setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutBaseSalary, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutHourlyRate, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutCommIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutOvertimeIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutBonusIncome, requireContext())

    }



}