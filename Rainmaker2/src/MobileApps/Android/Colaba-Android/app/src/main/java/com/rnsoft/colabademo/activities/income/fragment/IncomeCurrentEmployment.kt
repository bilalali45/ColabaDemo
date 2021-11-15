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
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList


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
    private var loanApplicationId: Int? = null
    private var incomeInfoId :Int? = null
    private var borrowerId :Int? = null
    //var addressList :  ArrayList<AddressData> = ArrayList()
    private var employerAddress = AddressData()
    //val otherIncomeList : ArrayList<EmploymentOtherIncome> = ArrayList()
    val incomeListForApi : ArrayList<AddEmploymentOtherIncome> = ArrayList()



    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = IncomeCurrentEmploymentBinding.inflate(inflater, container, false)
        toolbar = binding.headerIncome

        super.addListeners(binding.root)
        // set Header title
            toolbar.toolbarTitle.text = getString(R.string.current_employment)


            arguments?.let { arguments ->
                loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
                borrowerId = arguments.getInt(AppConstant.borrowerId)
                incomeInfoId = arguments.getInt(AppConstant.incomeId)
                //incomeCategoryId = arguments.getInt(AppConstant.incomeCategoryId)
                //incomeTypeID = arguments.getInt(AppConstant.incomeTypeID)
            }
            initViews()
            setInputFields()
            getEmploymentData()

        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<AddressData>(AppConstant.address)?.observe(
            viewLifecycleOwner) { result ->
            employerAddress = result
            displayAddress(result)
        }

         return binding.root
    }

    private fun getEmploymentData(){

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (loanApplicationId != null && incomeInfoId != null && borrowerId!=null) {
                    Log.e("getting-current-details", "" +loanApplicationId + " borrowerId:  " + borrowerId+ " incomeInfoId: " + incomeInfoId)
                    binding.loaderEmployment.visibility = View.VISIBLE
                    viewModel.getEmploymentDetail(authToken, loanApplicationId!!,borrowerId!!,incomeInfoId!!)
                }
            }

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
                        CustomMaterialFields.setColor(
                            binding.layoutYearsProfession,
                            R.color.grey_color_two,
                            requireContext()
                        )
                    }

                    info?.employedByFamilyOrParty?.let {
                        if (it == true)
                            binding.rbEmployedByFamilyYes.isChecked = true
                        else {
                            binding.rbEmployedByFamilyNo.isChecked = true
                        }
                    }

                    info?.hasOwnershipInterest?.let {
                        if (it == true)
                            binding.rbOwnershipYes.isChecked = true
                        else
                            binding.rbOwnershipNo.isChecked = true
                    }

                    data.employmentData?.employerAddress?.let {
                        employerAddress = it
                        displayAddress(it)
                    }

                    data?.employmentData?.wayOfIncome?.let { salary ->
                        salary.isPaidByMonthlySalary?.let {
                            if(it==true){
                                binding.paytypeSalary.isChecked = true
                                payTypeClicked()
                                salary.employerAnnualSalary?.let {
                                    binding.edAnnualSalary.setText(Math.round(it).toString())
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

                    data?.employmentData?.employmentOtherIncome?.let {
                        for (i in 0 until it.size) {
                           // otherIncomeList.add(EmploymentOtherIncome(
                            //        it.get(i).incomeTypeId, // maintainging list for matching ids for sending data to api
                              //      it.get(i).name, it.get(i).displayName, it.get(i).annualIncome))

                            it.get(i).displayName?.let { name ->
                                if (name.equals(AppConstant.INCOME_BONUS, true)){
                                    it.get(i).annualIncome?.let {
                                        binding.editTextBonusIncome.setText(Math.round(it).toString())
                                        binding.cbBonus.performClick()
                                    }
                                }
                                if (name.equals(AppConstant.INCOME_COMMISSION, true)) {
                                    it.get(i).annualIncome?.let {
                                        binding.editTextCommission.setText(
                                            Math.round(it).toString()
                                        )
                                        binding.cbCommission.performClick()
                                    }
                                }
                                if (name.equals(AppConstant.INCOME_OVERTIME, true)) {
                                    it.get(i).annualIncome?.let {
                                        binding.editTextOvertimeIncome.setText(Math.round(it).toString())
                                        binding.cbOvertime.performClick()
                                    }
                                }
                            }
                        }
                    }
                }
                binding.loaderEmployment.visibility = View.GONE
            })
        }
    }


    private fun processSendData(){

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

            lifecycleScope.launchWhenStarted{
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    if(loanApplicationId != null && borrowerId !=null && incomeInfoId != null) {
                        Log.e("Loan Application Id", "" +loanApplicationId + " borrowerId:  " + borrowerId)

                        val phoneNum = if(binding.editTextEmpPhnum.text.toString().length > 0) binding.editTextEmpPhnum.text.toString() else null
                        var isOwnershipInterest : Boolean ? = null
                        if(binding.rbOwnershipYes.isChecked)
                           isOwnershipInterest = true

                        if(binding.rbOwnershipNo.isChecked)
                            isOwnershipInterest = false

                        var isEmployedByFamily : Boolean ? = null
                        if(binding.rbEmployedByFamilyYes.isChecked)
                            isEmployedByFamily = true

                        if(binding.rbEmployedByFamilyNo.isChecked)
                            isEmployedByFamily = false

                        val ownershipPercentage = if(binding.edOwnershipPercent.text.toString().length > 0) binding.edOwnershipPercent.text.toString() else null

                        val employerInfo = EmploymentInfo(
                            borrowerId = borrowerId,incomeInfoId= incomeInfoId, employerName=empName, employerPhoneNumber=phoneNum, jobTitle=jobTitle,startDate=startDate,endDate =null, yearsInProfession = profYears.toInt(),
                            hasOwnershipInterest = isOwnershipInterest, ownershipInterest = ownershipPercentage?.toDouble(),employedByFamilyOrParty = isEmployedByFamily)

                        // check values for wasys of income
                        var isPaidByMonthlySalary : Boolean? = null
                        var annualSalary : String = "0"
                        var hourlyRate : String = "0"
                        var avgHourWeeks : String= "0"

                           if(binding.paytypeSalary.isChecked) {
                               isPaidByMonthlySalary = true
                               val salary = binding.edAnnualSalary.text.toString().trim()
                               annualSalary = if(salary.length > 0) salary.replace(",".toRegex(), "") else "0"
                           }

                        if(binding.paytypeHourly.isChecked){
                            isPaidByMonthlySalary = false
                            val value = binding.edHourlyRate.text.toString().trim()
                            hourlyRate = if(value.length > 0) value.replace(",".toRegex(), "") else "0"
                            avgHourWeeks = if(binding.editTextWeeklyHours.text.toString().trim().length >0) binding.editTextWeeklyHours.text.toString() else "0"
                        }

                        val wayOfIncome = WayOfIncome(isPaidByMonthlySalary=isPaidByMonthlySalary,employerAnnualSalary=annualSalary?.toDouble(),hourlyRate= hourlyRate?.toDouble(),hoursPerWeek=avgHourWeeks.toInt())

                        // get other income types
                        if (binding.cbBonus.isChecked) {
                            val bonus = binding.editTextBonusIncome.text.toString().trim()
                            val incomeBonus = if (bonus.length > 0) bonus.replace(",".toRegex(), "") else null
                            incomeListForApi.add(AddEmploymentOtherIncome(incomeTypeId = 2, annualIncome = incomeBonus?.toDouble()))
                        }
                        if (binding.cbCommission.isChecked) {
                            val commission = binding.editTextCommission.text.toString().trim()
                            val incomeCommission = if (commission.length > 0) commission.replace(",".toRegex(), "") else null
                            incomeListForApi.add(AddEmploymentOtherIncome(incomeTypeId = 3, annualIncome = incomeCommission?.toDouble()))
                        }
                        if (binding.cbOvertime.isChecked) {
                            val overtime = binding.editTextOvertimeIncome.text.toString().trim()
                            val incomeOvertime = if (overtime.length > 0) overtime.replace(",".toRegex(), "") else null
                            incomeListForApi.add(AddEmploymentOtherIncome(incomeTypeId = 1, annualIncome = incomeOvertime?.toDouble()))
                        }


                        val employmentData = AddCurrentEmploymentModel(
                            loanApplicationId = loanApplicationId,borrowerId= borrowerId, employmentInfo=employerInfo, employerAddress= employerAddress,wayOfIncome = wayOfIncome,employmentOtherIncome = incomeListForApi)
                        Log.e("incomeOther", "" + incomeListForApi)
                        Log.e("employmentData-snding to API", "" + employmentData)

                        binding.loaderEmployment.visibility = View.VISIBLE
                        viewModel.sendCurrentEmploymentData(authToken, employmentData)
                    }
                }
            }
        }
    }

    private fun openAddressFragment(){
        val addressFragment = AddressCurrentEmployment()
        val bundle = Bundle()
        bundle.putString(AppConstant.TOOLBAR_TITLE, getString(R.string.current_employer_address))
        bundle.putParcelable(AppConstant.address,employerAddress)
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_current_employment_address, addressFragment.arguments)
    }

    private fun displayAddress(it: AddressData){
        val builder = StringBuilder()
        it.street?.let { builder.append(it).append(" ") }
        it.unit?.let { builder.append(it).append("\n") }
        it.city?.let { builder.append(it).append(" ") }
        it.stateName?.let{ builder.append(it).append(" ")}
        it.zipCode?.let { builder.append(it) }
        it.countryName?.let { builder.append(" ").append(it)}
        binding.textviewEmployerAddress.text = builder
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

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> processSendData()
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

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onSentData(event: SendDataEvent) {
        if(event.addUpdateDataResponse.code == AppConstant.RESPONSE_CODE_SUCCESS){
            binding.loaderEmployment.visibility = View.GONE
        }

        else if(event.addUpdateDataResponse.code == AppConstant.INTERNET_ERR_CODE)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG)

        else
            if(event.addUpdateDataResponse.message != null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG)

        findNavController().popBackStack()
    }

    private fun setInputFields() {

        // set lable focus
        binding.editTextEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextEmpName, binding.layoutEmpName, requireContext()))
        binding.editTextEmpPhnum.setOnFocusChangeListener(FocusListenerForPhoneNumber(binding.editTextEmpPhnum, binding.layoutEmpPhnum,requireContext()))
        binding.editTextJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextJobTitle, binding.layoutJobTitle, requireContext()))
        binding.editTextProfYears.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextProfYears, binding.layoutYearsProfession, requireContext()))
        binding.edOwnershipPercent.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOwnershipPercent, binding.layoutOwnershipPercentage, requireContext()))
        binding.edAnnualSalary.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edAnnualSalary, binding.layoutBaseSalary, requireContext()))
        binding.editTextBonusIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextBonusIncome, binding.layoutBonusIncome, requireContext()))
        binding.editTextOvertimeIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextOvertimeIncome, binding.layoutOvertimeIncome, requireContext()))
        binding.editTextCommission.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextCommission, binding.layoutCommIncome, requireContext()))
        binding.edHourlyRate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edHourlyRate, binding.layoutHourlyRate, requireContext()))
        binding.editTextWeeklyHours.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextWeeklyHours, binding.layoutWeeklyHours, requireContext()))

        // set input format
        binding.edAnnualSalary.addTextChangedListener(NumberTextFormat(binding.edAnnualSalary))
        binding.edHourlyRate.addTextChangedListener(NumberTextFormat(binding.edHourlyRate))
        binding.editTextBonusIncome.addTextChangedListener(NumberTextFormat(binding.editTextBonusIncome))
        binding.editTextCommission.addTextChangedListener(NumberTextFormat(binding.editTextCommission))
        binding.editTextOvertimeIncome.addTextChangedListener(NumberTextFormat(binding.editTextOvertimeIncome))
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

    override fun onDestroy() {
        super.onDestroy()
        //viewModel.employmentDetail.value = null

    }
}