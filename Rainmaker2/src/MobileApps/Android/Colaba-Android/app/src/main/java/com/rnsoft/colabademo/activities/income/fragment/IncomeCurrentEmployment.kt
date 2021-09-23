package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.res.ColorStateList
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.annotation.ColorRes
import androidx.core.content.ContextCompat
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.google.android.material.textfield.TextInputLayout

import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeCurrentEmploymentBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import java.util.*



/**
 * Created by Anita Kiran on 9/13/2021.
 */
class IncomeCurrentEmployment : BaseFragment(), View.OnClickListener {

    private lateinit var binding: IncomeCurrentEmploymentBinding
    private lateinit var toolbar: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = IncomeCurrentEmploymentBinding.inflate(inflater, container, false)
            toolbar = binding.headerIncome
            savedViewInstance = binding.root
            super.addListeners(binding.root)
            // set Header title
            toolbar.toolbarTitle.setText(getString(R.string.current_employment))

            initViews()
            savedViewInstance

        }
    }

    private fun initViews() {
        binding.rbQues1Yes.setOnClickListener(this)
        binding.rbQues1No.setOnClickListener(this)
        binding.rbOwnershipYes.setOnClickListener(this)
        binding.rbOwnershipNo.setOnClickListener(this)
        binding.paytypeHourly.setOnClickListener(this)
        binding.paytypeSalary.setOnClickListener(this)
        binding.cbBonus.setOnClickListener(this)
        binding.cbOvertime.setOnClickListener(this)
        binding.cbCommission.setOnClickListener(this)
        binding.layoutAddress.setOnClickListener(this)
        toolbar.btnClose.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)
        binding.currentEmpLayout.setOnClickListener(this)

        setInputFields()
    }


    private fun setInputFields() {

        // set lable focus
        binding.edEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEmpName, binding.layoutEmpName, requireContext()))
        binding.edEmpPhnum.setOnFocusChangeListener(FocusListenerForPhoneNumber(binding.edEmpPhnum, binding.layoutEmpPhnum,requireContext()))
        binding.edJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edJobTitle, binding.layoutJobTitle, requireContext()))
        binding.edProfYears.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edProfYears, binding.layoutYearsProfession, requireContext()))
        binding.edOwnershipPercent.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOwnershipPercent, binding.layoutOwnershipPercentage, requireContext()))
        binding.edBaseSalary.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBaseSalary, binding.layoutBaseSalary, requireContext()))
        binding.edBonusIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBonusIncome, binding.layoutBonusIncome, requireContext()))
        binding.edOvertimeIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOvertimeIncome, binding.layoutOvertimeIncome, requireContext()))
        binding.edCommIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edCommIncome, binding.layoutCommIncome, requireContext()))
        binding.edHourlyRate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edHourlyRate, binding.layoutHourlyRate, requireContext()))
        binding.edAvgHours.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edAvgHours, binding.layoutAvgHours, requireContext()))

        // set input format
        binding.edBaseSalary.addTextChangedListener(NumberTextFormat(binding.edBaseSalary))
        binding.edBonusIncome.addTextChangedListener(NumberTextFormat(binding.edBonusIncome))
        binding.edCommIncome.addTextChangedListener(NumberTextFormat(binding.edCommIncome))
        binding.edOvertimeIncome.addTextChangedListener(NumberTextFormat(binding.edOvertimeIncome))
        binding.edEmpPhnum.addTextChangedListener(PhoneTextFormatter(binding.edEmpPhnum, "(###) ###-####"))

        // set Dollar prefix
        CustomMaterialFields.setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutBaseSalary, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutCommIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutOvertimeIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutBonusIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutHourlyRate, requireContext())

        // calendar
        binding.edStartDate.showSoftInputOnFocus = false
        binding.edStartDate.setOnClickListener { openCalendar()}
        //binding.edStartDate.setOnFocusChangeListener { _, _ -> openCalendar() }

        binding.edStartDate.doAfterTextChanged {
            if (binding.edStartDate.text?.length == 0) {
                CustomMaterialFields.setColor(binding.layoutStartDate,R.color.grey_color_three,requireActivity())
            } else {
                CustomMaterialFields.setColor(binding.layoutStartDate,R.color.grey_color_two,requireActivity())
                CustomMaterialFields.clearError(binding.layoutStartDate,requireActivity())
            }
        }

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> checkValidations()
            R.id.rb_ques1_yes -> quesOneClicked()
            R.id.rb_ques1_no -> quesOneClicked()
            R.id.rb_ownership_yes -> quesTwoClicked()
            R.id.rb_ownership_no -> quesTwoClicked()
            R.id.paytype_hourly -> payTypeClicked()
            R.id.paytype_salary ->payTypeClicked()
            R.id.cb_bonus -> bonusClicked()
            R.id.cb_overtime -> overtimeClicked()
            R.id.cb_commission -> commissionClicked()
            R.id.layout_address -> openAddressFragment() //findNavController().navigate(R.id.action_address)
            R.id.btn_close -> findNavController().popBackStack()
            R.id.current_emp_layout -> {
                HideSoftkeyboard.hide(requireActivity(), binding.currentEmpLayout)
                super.removeFocusFromAllFields(binding.currentEmpLayout)
            }
        }
    }

    private fun checkValidations(){

        val empName: String = binding.edEmpName.text.toString()
        val jobTitle: String = binding.edJobTitle.text.toString()
        val startDate: String = binding.edStartDate.text.toString()
        val profYears: String = binding.edProfYears.text.toString()

        if (empName.isEmpty() || empName.length == 0) {
            CustomMaterialFields.setError(binding.layoutEmpName, getString(R.string.error_field_required),requireActivity())
        }
        if (jobTitle.isEmpty() || jobTitle.length == 0) {
            CustomMaterialFields.setError(binding.layoutJobTitle, getString(R.string.error_field_required),requireActivity())
        }
        if (startDate.isEmpty() || startDate.length == 0) {
            CustomMaterialFields.setError(binding.layoutStartDate, getString(R.string.error_field_required),requireActivity())
        }
        if (profYears.isEmpty() || profYears.length == 0) {
            CustomMaterialFields.setError(binding.layoutYearsProfession, getString(R.string.error_field_required),requireActivity())
        }
        if (empName.isNotEmpty() || empName.length > 0) {
            CustomMaterialFields.clearError(binding.layoutEmpName,requireActivity())
        }
        if (jobTitle.isNotEmpty() || jobTitle.length > 0) {
            CustomMaterialFields.clearError(binding.layoutJobTitle,requireActivity())
        }
        if (startDate.isNotEmpty() || startDate.length > 0) {
            CustomMaterialFields.clearError(binding.layoutStartDate,requireActivity())
        }
        if (profYears.isNotEmpty() || profYears.length > 0) {
            CustomMaterialFields.clearError(binding.layoutYearsProfession,requireActivity())
        }
        if (empName.length > 0 && jobTitle.length > 0 &&  startDate.length > 0 && profYears.length > 0 ){
            findNavController().popBackStack()
        }

    }

    private fun openAddressFragment(){
        val addressFragment = IncomeAddress()
        val bundle = Bundle()
        bundle.putString("address", "Current Employer Address")
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_current_employment_address, addressFragment.arguments)
    }

    private fun quesOneClicked(){
        if(binding.rbQues1Yes.isChecked) {
            binding.rbQues1Yes.setTypeface(null, Typeface.BOLD)
            binding.rbQues1No.setTypeface(null, Typeface.NORMAL)
        }
        else {
            binding.rbQues1No.setTypeface(null, Typeface.BOLD)
            binding.rbQues1Yes.setTypeface(null, Typeface.NORMAL)
        }
    }

    private fun quesTwoClicked(){
        if(binding.rbOwnershipYes.isChecked) {
            binding.rbOwnershipYes.setTypeface(null, Typeface.BOLD)
            binding.rbOwnershipNo.setTypeface(null, Typeface.NORMAL)
            binding.layoutOwnershipPercentage.visibility = View.VISIBLE
        }
        else {
            binding.rbOwnershipNo.setTypeface(null, Typeface.BOLD)
            binding.rbOwnershipYes.setTypeface(null, Typeface.NORMAL)
            binding.layoutOwnershipPercentage.visibility = View.GONE

        }
    }

    private fun payTypeClicked(){
        if(binding.paytypeSalary.isChecked) {
            binding.paytypeSalary.setTypeface(null, Typeface.BOLD)
            binding.paytypeHourly.setTypeface(null, Typeface.NORMAL)
            binding.layoutHourlyRate.visibility= View.GONE
            binding.layoutAvgHours.visibility = View.GONE
            binding.layoutBaseSalary.visibility = View.VISIBLE
        }
        else {
            binding.paytypeSalary.setTypeface(null, Typeface.NORMAL)
            binding.paytypeHourly.setTypeface(null, Typeface.BOLD)
            binding.layoutHourlyRate.visibility= View.VISIBLE
            binding.layoutAvgHours.visibility = View.VISIBLE
            binding.layoutBaseSalary.visibility = View.GONE

        }
    }

    private fun bonusClicked(){
        if(binding.cbBonus.isChecked) {
            binding.cbBonus.setTypeface(null, Typeface.BOLD)
            binding.layoutBonusIncome.visibility = View.VISIBLE

        }
        else {
            binding.cbBonus.setTypeface(null, Typeface.NORMAL)
            binding.layoutBonusIncome.visibility = View.GONE
        }
    }

    private fun overtimeClicked(){
        if(binding.cbOvertime.isChecked) {
            binding.cbOvertime.setTypeface(null, Typeface.BOLD)
            binding.layoutOvertimeIncome.visibility = View.VISIBLE

        }
        else {
            binding.cbOvertime.setTypeface(null, Typeface.NORMAL)
            binding.layoutOvertimeIncome.visibility = View.GONE
        }
    }

    private fun commissionClicked(){
        if(binding.cbCommission.isChecked) {
            binding.cbCommission.setTypeface(null, Typeface.BOLD)
            binding.layoutCommIncome.visibility = View.VISIBLE
        }
        else {
            binding.cbCommission.setTypeface(null, Typeface.NORMAL)
            binding.layoutCommIncome.visibility = View.GONE

        }
    }

    private fun openCalendar() {
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val newMonth = month + 1

        val dpd = DatePickerDialog(
            requireActivity(),
            { view, year, monthOfYear, dayOfMonth -> binding.edStartDate.setText("" + newMonth + "/" + dayOfMonth + "/" + year) },
            year,
            month,
            day
        )
        dpd.show()
    }
}