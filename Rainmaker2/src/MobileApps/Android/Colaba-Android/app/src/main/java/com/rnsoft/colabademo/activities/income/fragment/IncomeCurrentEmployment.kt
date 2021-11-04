package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeCurrentEmploymentBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import java.util.*
import javax.inject.Inject


/**
 * Created by Anita Kiran on 9/13/2021.
 */
@AndroidEntryPoint
class IncomeCurrentEmployment : BaseFragment(), View.OnClickListener {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: IncomeCurrentEmploymentBinding
    private lateinit var toolbar: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null
    private val viewModel : IncomeViewModel by activityViewModels()
    var loanApplicationId: Int? = null
    var incomeInfoId :Int? = null
    var borrowerId :Int? = null

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
            setInputFields()
            getEmploymentData()

            savedViewInstance
        }
    }

    private fun getEmploymentData(){
        loanApplicationId = 5
        incomeInfoId = 1
        borrowerId = 5

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (loanApplicationId != null && incomeInfoId != null) {
                    binding.loaderEmployment.visibility = View.VISIBLE
                    viewModel.getCurrentEmploymentDetail(authToken, loanApplicationId!!,borrowerId!!,incomeInfoId!!)
                    viewModel.employmentDetail.observe(viewLifecycleOwner, { data ->

                        data?.employmentData?.employmentInfo.let { info ->
                            info?.employerName?.let {
                                binding.editTextEmpName.setText(it)
                                CustomMaterialFields.setColor(binding.layoutEmpName, R.color.grey_color_two, requireContext())
                            }
                            info?.employerPhoneNumber?.let {
                                binding.editTextEmpPhnum.setText(it)
                                CustomMaterialFields.setColor(binding.layoutEmpPhnum, R.color.grey_color_two, requireContext())
                            }
                            info?.jobTitle?.let {
                                binding.editTextJobTitle.setText(it)
                                CustomMaterialFields.setColor(binding.layoutJobTitle, R.color.grey_color_two, requireContext())
                            }
                            info?.startDate?.let {
                                binding.editTextStartDate.setText(AppSetting.getFullDate1(it))
                            }
                            info?.yearsInProfession?.let {
                                binding.editTextProfYears.setText(it.toString())
                                CustomMaterialFields.setColor(binding.layoutYearsProfession, R.color.grey_color_two, requireContext())
                            }
                            info?.employedByFamilyOrParty?.let {
                                if(it == true)
                                    binding.rbEmployedByFamilyYes.isChecked = true
                                else {
                                    binding.rbEmployedByFamilyNo.isChecked = true
                                }
                            }
                            info?.hasOwnershipInterest?.let {
                                if(it == true)
                                    binding.rbOwnershipYes.isChecked = true
                                else {
                                    binding.rbOwnershipNo.isChecked = true
                                }
                            }
                        }

                        data.employmentData?.employerAddress?.let {
                                /*addressList.add(AddressData(
                                    street = it.street,
                                    unit = it.unit,
                                    city = it.city,
                                    stateName = it.stateName,
                                    countryName = it.countryName,
                                    countyName = it.countyName,
                                    countyId = it.countyId,
                                    stateId = it.stateId,
                                    countryId = it.countryId,
                                    zipCode = it.zipCode)) */
                                binding.textviewEmployerAddress.text =
                                    it.streetAddress + " " + it.unitNo + "\n" + it.cityName + " " + it.stateName + " " + it.zipCode

                        }

                        data?.employmentData?.wayOfIncome?.let { salary ->
                            salary.isPaidByMonthlySalary?.let {
                                if(it==true) {
                                    binding.paytypeSalary.isChecked = true
                                    payTypeClicked()
                                    salary.employerAnnualSalary?.let {
                                        binding.edBaseSalary.setText(Math.round(it).toString())
                                        CustomMaterialFields.setColor(binding.layoutBaseSalary, R.color.grey_color_two, requireContext())
                                    }
                                } else {
                                    binding.paytypeHourly.isChecked = true
                                    payTypeClicked()
                                    salary.hourlyRate?.let {
                                        binding.edHourlyRate.setText(Math.round(it).toString())
                                        CustomMaterialFields.setColor(binding.layoutHourlyRate, R.color.grey_color_two, requireContext())
                                    }
                                    salary.hoursPerWeek?.let {
                                        binding.editTextWeeklyHours.setText(it.toString())
                                        CustomMaterialFields.setColor(binding.layoutWeeklyHours, R.color.grey_color_two, requireContext())
                                    }
                                }
                            }
                        }
                        binding.loaderEmployment.visibility = View.GONE
                    })
                }
            }
        }
    }

    private fun initViews() {
        binding.rbEmployedByFamilyYes.setOnClickListener(this)
        binding.rbEmployedByFamilyNo.setOnClickListener(this)
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


        binding.rbEmployedByFamilyYes.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                binding.rbEmployedByFamilyYes.setTypeface(null, Typeface.BOLD)
            else
                binding.rbEmployedByFamilyYes.setTypeface(null, Typeface.NORMAL)
        }

        binding.rbEmployedByFamilyNo.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                binding.rbEmployedByFamilyNo.setTypeface(null, Typeface.BOLD)
            else
                binding.rbEmployedByFamilyNo.setTypeface(null, Typeface.NORMAL)
        }

        binding.rbOwnershipYes.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                binding.rbOwnershipYes.setTypeface(null, Typeface.BOLD)
            else
                binding.rbOwnershipYes.setTypeface(null, Typeface.NORMAL)
        }

        binding.rbOwnershipNo.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                binding.rbOwnershipNo.setTypeface(null, Typeface.BOLD)
            else
                binding.rbOwnershipNo.setTypeface(null, Typeface.NORMAL)
        }

    }

    private fun setInputFields() {

        // set lable focus
        binding.editTextEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextEmpName, binding.layoutEmpName, requireContext()))
        binding.editTextEmpPhnum.setOnFocusChangeListener(FocusListenerForPhoneNumber(binding.editTextEmpPhnum, binding.layoutEmpPhnum,requireContext()))
        binding.editTextJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextJobTitle, binding.layoutJobTitle, requireContext()))
        binding.editTextProfYears.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextProfYears, binding.layoutYearsProfession, requireContext()))
        binding.edOwnershipPercent.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOwnershipPercent, binding.layoutOwnershipPercentage, requireContext()))
        binding.edBaseSalary.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBaseSalary, binding.layoutBaseSalary, requireContext()))
        binding.edBonusIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBonusIncome, binding.layoutBonusIncome, requireContext()))
        binding.edOvertimeIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOvertimeIncome, binding.layoutOvertimeIncome, requireContext()))
        binding.edCommIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edCommIncome, binding.layoutCommIncome, requireContext()))
        binding.edHourlyRate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edHourlyRate, binding.layoutHourlyRate, requireContext()))
        binding.editTextWeeklyHours.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextWeeklyHours, binding.layoutWeeklyHours, requireContext()))

        // set input format
        binding.edBaseSalary.addTextChangedListener(NumberTextFormat(binding.edBaseSalary))
        binding.edHourlyRate.addTextChangedListener(NumberTextFormat(binding.edHourlyRate))
        binding.edBonusIncome.addTextChangedListener(NumberTextFormat(binding.edBonusIncome))
        binding.edCommIncome.addTextChangedListener(NumberTextFormat(binding.edCommIncome))
        binding.edOvertimeIncome.addTextChangedListener(NumberTextFormat(binding.edOvertimeIncome))
        binding.editTextEmpPhnum.addTextChangedListener(PhoneTextFormatter(binding.editTextEmpPhnum, "(###) ###-####"))

        // set Dollar prefix
        CustomMaterialFields.setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutBaseSalary, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutCommIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutOvertimeIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutBonusIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutHourlyRate, requireContext())

        // calendar
        binding.editTextStartDate.showSoftInputOnFocus = false
        binding.editTextStartDate.setOnClickListener { openCalendar()}
        //binding.edStartDate.setOnFocusChangeListener { _, _ -> openCalendar() }

        binding.editTextStartDate.doAfterTextChanged {
            if (binding.editTextStartDate.text?.length == 0) {
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
            R.id.rb_employed_by_family_yes -> quesOneClicked()
            R.id.rb_employed_by_family_no -> quesOneClicked()
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

        val empName: String = binding.editTextEmpName.text.toString()
        val jobTitle: String = binding.editTextJobTitle.text.toString()
        val startDate: String = binding.editTextStartDate.text.toString()
        val profYears: String = binding.editTextProfYears.text.toString()

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
        val addressFragment = AddressCurrentEmployment()
        val bundle = Bundle()
        bundle.putString(AppConstant.address, getString(R.string.current_employer_address))
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_current_employment_address, addressFragment.arguments)
    }

    private fun quesOneClicked(){
        if(binding.rbEmployedByFamilyYes.isChecked) {
            binding.rbEmployedByFamilyYes.setTypeface(null, Typeface.BOLD)
            binding.rbEmployedByFamilyNo.setTypeface(null, Typeface.NORMAL)
        }
        else {
            binding.rbEmployedByFamilyNo.setTypeface(null, Typeface.BOLD)
            binding.rbEmployedByFamilyYes.setTypeface(null, Typeface.NORMAL)
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
            binding.layoutWeeklyHours.visibility = View.GONE
            binding.layoutBaseSalary.visibility = View.VISIBLE
        }
        else {
            binding.paytypeSalary.setTypeface(null, Typeface.NORMAL)
            binding.paytypeHourly.setTypeface(null, Typeface.BOLD)
            binding.layoutHourlyRate.visibility= View.VISIBLE
            binding.layoutWeeklyHours.visibility = View.VISIBLE
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
            { view, year, monthOfYear, dayOfMonth -> binding.editTextStartDate.setText("" + newMonth + "/" + dayOfMonth + "/" + year) },
            year,
            month,
            day
        )
        dpd.show()
    }
}