package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.text.format.DateFormat
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.view.isVisible
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.addresses.info.fragment.DeleteCurrentResidenceDialogFragment

import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeCurrentEmploymentBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.clearCheckBoxTextColor
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.radioUnSelectColor
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setCheckBoxTextColor
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setRadioColor

import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.previous_residence_layout.*
import kotlinx.coroutines.async
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import java.text.DecimalFormat
import java.text.SimpleDateFormat
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
    private var incomeInfoId : Int? = null
    private var borrowerId : Int? = null
    private var borrowerName: String? = null
    private var employerAddress = AddressData()
    private var hourlyModel = HourlyModel()
    val incomeListForApi : ArrayList<EmploymentOtherIncomes> = ArrayList()
    private var ownershipPercentageValue : String? = null
    private var salaryAmount : String = ""
    private var hourlyRate: String = ""
    private var averageHoursPerWeek : String = ""
    private var bonusAmount : String = ""
    private var overtimeAmount : String = ""
    private var commissionAmount : String = ""
    val formatter =  DecimalFormat("#,###,###")


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return if (savedViewInstance != null){
            savedViewInstance
        } else {
            binding = IncomeCurrentEmploymentBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root
            toolbar = binding.headerIncome
            super.addListeners(binding.root)
            // set Header title
            toolbar.toolbarTitle.text = getString(R.string.current_employment)
            arguments?.let { arguments ->
                loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
                borrowerId = arguments.getInt(AppConstant.borrowerId)
                incomeInfoId = arguments.getInt(AppConstant.incomeId)
                borrowerName = arguments.getString(AppConstant.borrowerName)
            }

            //Log.e("Current Employment-oncreate", "Loan Application Id " + loanApplicationId + " borrowerId:  " + borrowerId + " incomeInfoId" + incomeInfoId)

            borrowerName?.let {
                toolbar.borrowerPurpose.setText(it)
            }

            if (loanApplicationId != null && borrowerId != null){
                toolbar.btnTopDelete.visibility = View.VISIBLE
                toolbar.btnTopDelete.setOnClickListener {
                    DeleteIncomeDialogFragment.newInstance(AppConstant.income_delete_text).show(childFragmentManager, DeleteCurrentResidenceDialogFragment::class.java.canonicalName)
                }
            }
            if (incomeInfoId == null || incomeInfoId == 0) {
                toolbar.btnTopDelete.visibility = View.GONE
                showHideAddress(false,true)
            }

            initViews()
            setInputFields()
            getEmploymentData()

            savedViewInstance
        }
    }

    private fun getEmploymentData(){
        if (loanApplicationId != null && borrowerId!=null && incomeInfoId!! > 0) {

            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                   // Log.e("getting-current-details", "" +loanApplicationId + " borrowerId:  " + borrowerId+ " incomeInfoId: " + incomeInfoId)
                    binding.loaderEmployment.visibility = View.VISIBLE
                    viewModel.getEmploymentDetail(authToken, loanApplicationId!!, borrowerId!!, incomeInfoId!!)
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
                        CustomMaterialFields.setColor(
                            binding.layoutEmpPhnum,
                            R.color.grey_color_two,
                            requireContext()
                        )
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
                        if (it == true)
                            binding.rbEmployedByFamilyYes.isChecked = true
                        else {
                            binding.rbEmployedByFamilyNo.isChecked = true
                        }
                    }

                    info?.hasOwnershipInterest?.let {
                        if (it == true) {
                            binding.rbOwnershipYes.isChecked = true
                            setRadioColor(binding.rbOwnershipYes, requireContext())
                            binding.layoutOwnershipPercentage.visibility = View.VISIBLE
                            info.ownershipInterest?.let { percentage ->
                                binding.tvOwnershipPercentage.setText(Math.round(percentage).toString().plus("%"))
                                ownershipPercentageValue = Math.round(percentage).toString()
                            }
                        } else {
                            binding.rbOwnershipNo.isChecked = true
                            setRadioColor(binding.rbOwnershipNo, requireContext())
                            binding.layoutOwnershipPercentage.visibility = View.GONE

                        }
                    }

                    data?.employmentData?.employerAddress?.let {
                        employerAddress = it
                        displayAddress(it)
                        //binding.textviewCurrentEmployerAddress.text = it.street + " " + it.unit + "\n" + it.city + " " + it.stateName + " " + it.zipCode + " " + it.countryName
                    } ?:run{
                        showHideAddress(false,true)
                    }

                    data?.employmentData?.wayOfIncome?.let { salary ->
                        salary.isPaidByMonthlySalary?.let {
                            if (it == true) {
                                binding.paytypeSalary.isChecked = true
                                binding.layoutAnnualSalary.visibility = View.VISIBLE
                                setRadioColor(binding.paytypeSalary,requireContext())
                                //payTypeClicked()
                                salary.employerAnnualSalary?.let {
                                    val newSalary: String = formatter.format(Math.round(it))
                                    binding.tvAnnualSalary.text = "$".plus(newSalary)
                                    salaryAmount = Math.round(it).toString()
                                }
                            } else {
                                    binding.paytypeHourly.isChecked = true
                                    binding.layoutHourly.visibility = View.VISIBLE
                                    setRadioColor(binding.paytypeHourly, requireContext())
                                    salary.hourlyRate?.let {
                                        val newHourlyRate: String = formatter.format(Math.round(it))
                                        binding.tvHourlyRate.text = "$".plus(newHourlyRate)
                                        hourlyModel.hourlyRate = Math.round(it).toString()

                                    }
                                    salary.hoursPerWeek?.let {
                                        binding.tvWeeklyAverageHours.setText(it.toString())
                                        hourlyModel.avgWeeks = it.toString()
                                    }
                            }
                        }
                    }

                    data?.employmentData?.employmentOtherIncome?.let {
                        for (i in 0 until it.size) {

                            it.get(i).displayName?.let { name ->
                                if (name.equals(AppConstant.INCOME_BONUS, true)) {
                                    it.get(i).annualIncome?.let {
                                        val newBonus : String = formatter.format(Math.round(it))
                                        binding.tvBonusIncome.text = "$".plus(newBonus)
                                        bonusAmount = Math.round(it).toString()
                                        binding.cbBonus.isChecked = true
                                        binding.layoutBonusIncome.visibility = View.VISIBLE
                                        setCheckBoxTextColor(binding.cbBonus,requireContext())
                                    }
                                }
                                if (name.equals(AppConstant.INCOME_COMMISSION, true)) {
                                    it.get(i).annualIncome?.let {
                                        val commission: String = formatter.format(Math.round(it))
                                        binding.tvCommission.setText("$".plus(commission))
                                        commissionAmount = Math.round(it).toString() // for sending to next screen
                                        binding.cbCommission.isChecked = true
                                        binding.layoutCommission.visibility = View.VISIBLE
                                        setCheckBoxTextColor(binding.cbCommission,requireContext())
                                    }
                                }
                                if (name.equals(AppConstant.INCOME_OVERTIME, true)) {
                                    it.get(i).annualIncome?.let {
                                        val newOvertime : String = formatter.format(Math.round(it))
                                        binding.tvOvertimeIncome.setText("$".plus(newOvertime))
                                        overtimeAmount = Math.round(it).toString()
                                        binding.cbOvertime.isChecked = true
                                        binding.layoutOvertimeIncome.visibility = View.VISIBLE
                                        setCheckBoxTextColor(binding.cbOvertime,requireContext())

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

    private fun showHideAddress(isShowAddress: Boolean, isAddAddress: Boolean){
        if(isShowAddress){
            binding.layoutAddress.visibility = View.VISIBLE
            binding.addEmployerAddress.visibility = View.GONE
        }
        if(isAddAddress){
            binding.layoutAddress.visibility = View.GONE
            binding.addEmployerAddress.visibility = View.VISIBLE
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

    private fun displayAddress(it: AddressData) {
        if(it.street == null && it.city==null && it.zipCode==null)
            showHideAddress(false,true)
        else {
            val builder = StringBuilder()
            it.street?.let {
                if(it != "null") builder.append(it).append(" ") }
            it.unit?.let {
                if(it != "null") builder.append(it).append(",") } ?: run { builder.append(",") }
            it.city?.let {
                if(it != "null") builder.append("\n").append(it).append(",").append(" ") } ?: run { builder.append("\n") }
            it.stateName?.let {
                if(it !="null") builder.append(it).append(" ") }
            it.zipCode?.let {
                if(it != "null") builder.append(it) }

            binding.textviewCurrentEmployerAddress.text = builder
            showHideAddress(true,false)
        }
    }

    private fun initViews() {
        binding.addEmployerAddress.setOnClickListener {
            openAddressFragment()
        }

        binding.rbOwnershipYes.setOnClickListener(this)
        binding.rbOwnershipNo.setOnClickListener(this)
        binding.layoutOwnershipPercentage.setOnClickListener(this)
        binding.paytypeHourly.setOnClickListener(this)
        binding.paytypeSalary.setOnClickListener(this)
        binding.layoutAnnualSalary.setOnClickListener(this)
        binding.layoutHourly.setOnClickListener(this)
        binding.layoutAddress.setOnClickListener(this)
        toolbar.btnClose.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)
        binding.currentEmpLayout.setOnClickListener(this)
        binding.cbBonus.setOnClickListener(this)
        binding.cbOvertime.setOnClickListener(this)
        binding.cbCommission.setOnClickListener(this)
        binding.layoutBonusIncome.setOnClickListener(this)
        binding.layoutOvertimeIncome.setOnClickListener(this)
        binding.layoutCommission.setOnClickListener(this)


        binding.rbEmployedByFamilyYes.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                setRadioColor(binding.rbEmployedByFamilyYes, requireContext())
            else
                radioUnSelectColor(binding.rbEmployedByFamilyYes, requireContext())
        }

        binding.rbEmployedByFamilyNo.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                setRadioColor(binding.rbEmployedByFamilyNo, requireContext())
            else
                radioUnSelectColor(binding.rbEmployedByFamilyNo, requireContext())
        }

        binding.rbOwnershipNo.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                setRadioColor(binding.rbOwnershipNo, requireContext())
                binding.rbOwnershipYes.isChecked = false
                radioUnSelectColor(binding.rbOwnershipYes, requireContext())
                binding.layoutOwnershipPercentage.visibility = View.GONE
                binding.tvOwnershipPercentage.setText("")
                ownershipPercentageValue = null
            }
            else {
                setRadioColor(binding.rbOwnershipYes, requireContext())
                radioUnSelectColor(binding.rbOwnershipNo, requireContext())
            }
        }

/*        binding.paytypeSalary.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                //setRadioColor(binding.paytypeSalary, requireContext())
                binding.paytypeHourly.isChecked = false
                radioUnSelectColor(binding.paytypeHourly, requireContext())
            }
            else {
                radioUnSelectColor(binding.rbOwnershipYes, requireContext())
            }
        }

        binding.paytypeHourly.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                setRadioColor(binding.paytypeHourly, requireContext())
                binding.paytypeSalary.isChecked = false
                radioUnSelectColor(binding.paytypeSalary, requireContext())
            }
            else {
                radioUnSelectColor(binding.paytypeHourly, requireContext())
            }
        } */


        /*   binding.cbBonus.setOnCheckedChangeListener { _, isChecked ->
               if(isChecked){
                   binding.cbBonus.setTypeface(null, Typeface.BOLD)
                   binding.layoutBonusIncome.visibility = View.VISIBLE
               } else {
                   binding.cbBonus.setTypeface(null, Typeface.NORMAL)
                   binding.layoutBonusIncome.visibility = View.GONE
               }
           }

           binding.cbCommission.setOnCheckedChangeListener { _, isChecked ->
               if(isChecked){
                   binding.cbCommission.setTypeface(null, Typeface.BOLD)
                   binding.layoutCommission.visibility = View.VISIBLE
               } else {
                   binding.cbCommission.setTypeface(null, Typeface.NORMAL)
                   binding.layoutCommission.visibility = View.GONE
               }
           }

           binding.cbOvertime.setOnCheckedChangeListener { _, isChecked ->
               if (isChecked) {
                   binding.cbOvertime.setTypeface(null, Typeface.BOLD)
                   binding.layoutOvertimeIncome.visibility = View.VISIBLE
               } else {
                   binding.cbOvertime.setTypeface(null, Typeface.NORMAL)
                   binding.layoutOvertimeIncome.visibility = View.GONE
               }
           } */

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> processSendData()
            R.id.rb_ownership_yes ->{
                if(binding.rbOwnershipYes.isChecked) {
                    binding.rbOwnershipYes.isChecked = false
                    gotoOwnershipPercentage()
                }
            }
            R.id.layout_ownership_percentage -> gotoOwnershipPercentage()
            R.id.paytype_hourly -> onHourlyClicked()
            R.id.layout_hourly -> onHourlyClicked()
            R.id.paytype_salary -> onSalaryClicked()
            R.id.layout_annual_salary-> onSalaryClicked()
            R.id.layout_address -> openAddressFragment() //findNavController().navigate(R.id.action_address)
            R.id.cb_bonus-> onBonusClicked()
            R.id.layout_bonus_income-> onBonusClicked()
            R.id.cb_overtime-> onOvertimeClicked()
            R.id.layout_overtime_income ->onOvertimeClicked()
            R.id.cb_commission -> onCommissionClicked()
            R.id.layout_commission->onCommissionClicked()
            R.id.btn_close -> findNavController().popBackStack()
            R.id.current_emp_layout -> {
                HideSoftkeyboard.hide(requireActivity(), binding.currentEmpLayout)
                super.removeFocusFromAllFields(binding.currentEmpLayout)
            }
        }
    }

    private fun processSendData(){

        var isDataEntered : Boolean = false
        val empName: String = binding.editTextEmpName.text.toString()
        val startDate: String = binding.editTextStartDate.text.toString()

        if (empName.isEmpty() || empName.length == 0) {
            isDataEntered = false
            CustomMaterialFields.setError(binding.layoutEmpName, getString(R.string.error_field_required),requireActivity())
        }

        if (empName.isNotEmpty() || empName.length > 0) {
            isDataEntered = true
            CustomMaterialFields.clearError(binding.layoutEmpName,requireActivity())
        }
        if (startDate.isEmpty() || startDate.length == 0) {
            isDataEntered = false
            CustomMaterialFields.setError(binding.layoutStartDate, getString(R.string.error_field_required),requireActivity())
        }

        if (startDate.isNotEmpty() || startDate.length > 0) {
            isDataEntered = true
            CustomMaterialFields.clearError(binding.layoutStartDate,requireActivity())
        }

        if (empName.length > 0 && startDate.length > 0 ){
            //lifecycleScope.launchWhenStarted{
               // sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                   if(loanApplicationId != null && borrowerId !=null) {
                       //Log.e("Loan Application Id", "" +loanApplicationId + " borrowerId:  " + borrowerId)

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

                        val jobTitle = if(binding.editTextJobTitle.text.toString().length > 0) binding.editTextJobTitle.text.toString() else null
                        val profYears = if(binding.editTextProfYears.text.toString().length > 0) binding.editTextProfYears.text.toString() else null

                        val employerInfo = EmploymentInfo(
                            borrowerId = borrowerId,incomeInfoId= incomeInfoId, employerName=empName, employerPhoneNumber=phoneNum, jobTitle=jobTitle,startDate=startDate,endDate =null, yearsInProfession = profYears?.toInt(),
                            hasOwnershipInterest = isOwnershipInterest, ownershipInterest = ownershipPercentageValue?.toDoubleOrNull(),employedByFamilyOrParty = isEmployedByFamily)

                        // check values for wasys of income
                        var isPaidByMonthlySalary : Boolean? = null
                        var annualSalary : String = "0"
                        var hourlyRate : String = "0"
                        var avgHourWeeks : String= "0"

                        if(binding.paytypeSalary.isChecked) {
                            isPaidByMonthlySalary = true
                            //val salary = binding.tvAnnualSalary.text.toString().trim()
                            //annualSalary = if(salary.length > 0) salary.replace(",".toRegex(), "") else "0"
                            annualSalary = if(salaryAmount.length > 0) salaryAmount?.replace(",".toRegex(), "") else "0"
                        }

                        if(binding.paytypeHourly.isChecked){
                            isPaidByMonthlySalary = false
                            //val value = binding.tvHourlyRate.text.toString().trim()
                            //hourlyRate = if(value.length > 0) value.replace(",".toRegex(), "") else "0"
                            //avgHourWeeks = if(binding.tvWeeklyAverageHours.text.toString().trim().length >0) binding.tvWeeklyAverageHours.text.toString() else "0"
                            //hourlyRate = if(hourlyModel.hourlyRate!!.length > 0) hourlyModel.hourlyRate!! else "0"
                            //avgHourWeeks = if(hourlyModel.avgWeeks!!.isNotEmpty()) hourlyModel.avgWeeks!! else "0"

                            hourlyModel.hourlyRate?.let {
                             hourlyRate= if(it.length > 0) it.replace(",".toRegex(), "") else "0"
                            }

                            hourlyModel.avgWeeks?.let {
                                avgHourWeeks = if(it.length > 0) it else "0"
                            }

                        }

                        val wayOfIncome = WayOfIncome(isPaidByMonthlySalary=isPaidByMonthlySalary,employerAnnualSalary=annualSalary?.toDouble(),hourlyRate= hourlyRate?.toDouble(),hoursPerWeek=avgHourWeeks.toInt())

                        // get other income types
                        if (binding.cbBonus.isChecked) {
                            //val bonus = binding.tvBonusIncome.text.toString().trim()
                            //val newIncomeBonus = if (bonus.length > 0) bonus.replace(",".toRegex(), "") else null
                            val newIncomeBonus = if(bonusAmount.isNotEmpty()) bonusAmount?.replace(",".toRegex(), "") else null
                            incomeListForApi.add(EmploymentOtherIncomes(incomeTypeId = 2, annualIncome = newIncomeBonus?.toDouble()))
                        }
                        if (binding.cbOvertime.isChecked) {
                            //val overtime = binding.tvOvertimeIncome.text.toString().trim()
                            //val newIncomeOvertime = if (overtime.length > 0) overtime.replace(",".toRegex(), "") else null
                            val newIncomeOvertime = if(overtimeAmount.isNotEmpty()) overtimeAmount?.replace(",".toRegex(), "") else null
                            incomeListForApi.add(EmploymentOtherIncomes(incomeTypeId = 1, annualIncome = newIncomeOvertime?.toDouble()))
                        }
                        if (binding.cbCommission.isChecked){
                            //val commission = binding.tvCommission.text.toString().trim()
                            //val newIncomeCommission = if (commission.length > 0) commission.replace(",".toRegex(), "") else null
                            val newIncomeCommission = if(commissionAmount.isNotEmpty()) commissionAmount?.replace(",".toRegex(), "") else null
                            incomeListForApi.add(EmploymentOtherIncomes(incomeTypeId = 3, annualIncome = newIncomeCommission?.toDouble()))
                        }

                        val employmentData = AddCurrentEmploymentModel(
                            loanApplicationId = loanApplicationId,borrowerId= borrowerId, employmentInfo=employerInfo, employerAddress= employerAddress,wayOfIncome = wayOfIncome,employmentOtherIncomes = incomeListForApi)
                        //Log.e("incomeOther", "" + incomeListForApi)
                        //Log.e("employmentData-snding to API", "" + employmentData)

                        binding.loaderEmployment.visibility = View.VISIBLE
                        viewModel.sendCurrentEmploymentData(employmentData)
                    }
                //}
            //}
        }
    }

    override fun onResume() {
        super.onResume()
        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<AddressData>(
            AppConstant.address)?.observe(viewLifecycleOwner) { result -> employerAddress = result
            displayAddress(result)
        }

        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<String>(AppConstant.OWNERSHIP_INTEREST)?.observe(
            viewLifecycleOwner) { result ->
            result?.let {
                ownershipPercentageValue = result
                setOwnershipInterest(result)
                findNavController().currentBackStackEntry?.savedStateHandle?.remove<String>(AppConstant.OWNERSHIP_INTEREST)
            }
        }

        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<String>(AppConstant.PAY_TYPE_SALARY)?.observe(
            viewLifecycleOwner) { result ->
            result?.let {
                salaryAmount = result
                binding.tvAnnualSalary.setText("$"+result)
                binding.paytypeSalary.isChecked = true
                binding.layoutAnnualSalary.visibility = View.VISIBLE
                setRadioColor(binding.paytypeSalary, requireContext())
                binding.paytypeHourly.isChecked = false
                binding.layoutHourly.visibility = View.GONE
                hourlyRate = ""
                averageHoursPerWeek = ""
                binding.tvHourlyRate.setText("")
                binding.tvWeeklyAverageHours.setText("")
                radioUnSelectColor(binding.paytypeHourly,requireContext())

                findNavController().currentBackStackEntry?.savedStateHandle?.remove<String>(AppConstant.PAY_TYPE_SALARY)
            }
        }

        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<HourlyModel>(AppConstant.hourly)?.observe(
            viewLifecycleOwner) { result ->

            result?.let {
                hourlyModel = result
                binding.tvHourlyRate.setText("$"+hourlyModel.hourlyRate)
                binding.tvWeeklyAverageHours.setText(hourlyModel.avgWeeks)

                binding.paytypeHourly.isChecked = true
                setRadioColor(binding.paytypeHourly, requireContext())
                binding.paytypeSalary.isChecked = false
                radioUnSelectColor(binding.paytypeSalary,requireContext())
                binding.layoutHourly.visibility = View.VISIBLE
                binding.tvAnnualSalary.setText("")
                salaryAmount = ""
                binding.layoutAnnualSalary.visibility = View.GONE

                findNavController().currentBackStackEntry?.savedStateHandle?.remove<HourlyModel>(AppConstant.hourly)
            }
        }

        // bonus
        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<String>(AppConstant.INCOME_BONUS)?.observe(
            viewLifecycleOwner) { result ->
            result?.let {
                bonusAmount = result
                binding.tvBonusIncome.setText("$"+result)
                binding.cbBonus.isChecked = true
                binding.layoutBonusIncome.visibility = View.VISIBLE
                setCheckBoxTextColor(binding.cbBonus, requireContext())
                findNavController().currentBackStackEntry?.savedStateHandle?.remove<String>(AppConstant.INCOME_BONUS)


            }
        }
        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<String>(AppConstant.INCOME_OVERTIME)?.observe(
            viewLifecycleOwner) { result ->
            result?.let {
                overtimeAmount = result
                binding.tvOvertimeIncome.setText("$"+result)
                binding.cbOvertime.isChecked = true
                binding.layoutOvertimeIncome.visibility = View.VISIBLE
                setCheckBoxTextColor(binding.cbBonus, requireContext())
                findNavController().currentBackStackEntry?.savedStateHandle?.remove<String>(AppConstant.INCOME_OVERTIME)
            }
        }
        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<String>(AppConstant.INCOME_COMMISSION)?.observe(
            viewLifecycleOwner) { result ->
            result?.let {
                commissionAmount = result
                binding.tvCommission.setText("$"+result)
                binding.cbCommission.isChecked = true
                binding.layoutCommission.visibility = View.VISIBLE
                setCheckBoxTextColor(binding.cbCommission, requireContext())
                findNavController().currentBackStackEntry?.savedStateHandle?.remove<String>(AppConstant.INCOME_COMMISSION)

            }
        }
    }

    private fun setOwnershipInterest(result : String){
        binding.tvOwnershipPercentage.text = result.plus("%")
        binding.layoutOwnershipPercentage.visibility = View.VISIBLE
        binding.rbOwnershipYes.isChecked = true
        binding.rbOwnershipNo.isChecked = false
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

    private fun gotoOwnershipPercentage(){
        val bundle = Bundle()
        bundle.putString(AppConstant.TOOLBAR_TITLE, AppConstant.OWNERSHIP_INTEREST)
        bundle.putString(AppConstant.borrowerName, borrowerName)
        bundle.putString(AppConstant.OWNERSHIP_INTEREST_PERCENTAGE, ownershipPercentageValue)
        findNavController().navigate(R.id.action_income_sources, bundle)
    }

    private fun onSalaryClicked(){
        binding.paytypeSalary.isChecked = false
        //binding.layoutHourly.visibility = View.GONE
        //binding.paytypeHourly.isChecked = false
        //radioUnSelectColor(binding.paytypeHourly, requireContext())

        val bundle = Bundle()
        bundle.putString(AppConstant.TOOLBAR_TITLE, AppConstant.PAY_TYPE_SALARY)
        bundle.putString(AppConstant.borrowerName, borrowerName)
        bundle.putString(AppConstant.salary_value, salaryAmount)
        findNavController().navigate(R.id.action_income_sources, bundle)
    }

    private fun onHourlyClicked(){
        binding.paytypeHourly.isChecked = false
        //binding.layoutAnnualSalary.visibility = View.GONE
        //binding.paytypeSalary.isChecked = false
        //radioUnSelectColor(binding.paytypeSalary, requireContext())

        val bundle = Bundle()
        bundle.putString(AppConstant.TOOLBAR_TITLE, AppConstant.PAY_TYPE_HOURLY)
        bundle.putString(AppConstant.borrowerName, borrowerName)
        bundle.putParcelable(AppConstant.hourly,HourlyModel(hourlyRate, averageHoursPerWeek))
        findNavController().navigate(R.id.action_income_sources, bundle)
    }

    private fun onBonusClicked(){
        if(binding.cbBonus.isChecked) {
            binding.cbBonus.isChecked = false
            val bundle = Bundle()
            bundle.putString(AppConstant.TOOLBAR_TITLE, AppConstant.INCOME_BONUS)
            bundle.putString(AppConstant.borrowerName, borrowerName)
            bundle.putString(AppConstant.bonus_value, bonusAmount)
            findNavController().navigate(R.id.action_income_sources, bundle)

        } else {
            clearCheckBoxTextColor(binding.cbBonus,requireContext())
            binding.layoutBonusIncome.visibility = View.GONE
            bonusAmount = ""
        }
    }

    private fun onOvertimeClicked(){
        if(binding.cbOvertime.isChecked) {
            binding.cbOvertime.isChecked = false
            val bundle = Bundle()
            bundle.putString(AppConstant.TOOLBAR_TITLE, AppConstant.INCOME_OVERTIME)
            bundle.putString(AppConstant.borrowerName, borrowerName)
            bundle.putString(AppConstant.overtime_value, overtimeAmount)
            findNavController().navigate(R.id.action_income_sources, bundle)
        } else {
            clearCheckBoxTextColor(binding.cbOvertime,requireContext())
            binding.layoutOvertimeIncome.visibility = View.GONE
            overtimeAmount = ""
        }
    }

    private fun onCommissionClicked(){
        if(binding.cbCommission.isChecked) {
            binding.cbCommission.isChecked = false
            val bundle = Bundle()
            bundle.putString(AppConstant.TOOLBAR_TITLE, AppConstant.INCOME_COMMISSION)
            bundle.putString(AppConstant.borrowerName, borrowerName)
            bundle.putString(AppConstant.commission_value, commissionAmount)
            findNavController().navigate(R.id.action_income_sources, bundle)
        } else {
            clearCheckBoxTextColor(binding.cbCommission,requireContext())
            binding.layoutCommission.visibility = View.GONE
            commissionAmount = ""
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

    private val borrowerApplicationViewModel: BorrowerApplicationViewModel by activityViewModels()

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onSentData(event: SendDataEvent) {
        binding.loaderEmployment.visibility = View.GONE
        if(event.addUpdateDataResponse.code == AppConstant.RESPONSE_CODE_SUCCESS){
            viewModel.resetChildFragmentToNull()
            updateMainIncome()
        }
        else if(event.addUpdateDataResponse.code == AppConstant.INTERNET_ERR_CODE){
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG)
        } else {
            if (event.addUpdateDataResponse.message != null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG)
        }
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onIncomeDeleteReceived(evt: IncomeDeleteEvent){
        if(evt.isDeleteIncome){
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    val call = async{  viewModel.deleteIncome(authToken, incomeInfoId!!, borrowerId!!, loanApplicationId!!) }
                    call.await()
                }
                if (loanApplicationId != null && borrowerId != null && incomeInfoId!! > 0) {
                    viewModel.addUpdateIncomeResponse.observe(viewLifecycleOwner, { genericAddUpdateAssetResponse ->
                        val codeString = genericAddUpdateAssetResponse?.code.toString()
                        if(codeString == "400" || codeString == "200"){
                            updateMainIncome()
                            viewModel.resetChildFragmentToNull()
                        }
                    })
                }
            }
        }
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onMainIncomeUpdate(evt: OnUpdateMainIncomeReceived){
        if(evt.isMainIncomeUpdateReceived){
            IncomeTabFragment.isStartIncomeTab = false
            val incomeUpdate = IncomeUpdateInfo(AppConstant.income_employment,borrowerId!!)
            findNavController().previousBackStackEntry?.savedStateHandle?.set(AppConstant.income_update,incomeUpdate)
            findNavController().popBackStack()
        }
    }

    private fun updateMainIncome(){
        borrowerApplicationViewModel.resetSingleIncomeTab()
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                borrowerApplicationViewModel.getSingleTabIncomeDetail(authToken, loanApplicationId!!, borrowerId!!) }
        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.editTextEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextEmpName, binding.layoutEmpName, requireContext()))
        binding.editTextEmpPhnum.setOnFocusChangeListener(FocusListenerForPhoneNumber(binding.editTextEmpPhnum, binding.layoutEmpPhnum,requireContext()))
        binding.editTextJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextJobTitle, binding.layoutJobTitle, requireContext()))
        binding.editTextProfYears.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextProfYears, binding.layoutYearsProfession, requireContext()))

        // set input format
        binding.editTextEmpPhnum.addTextChangedListener(PhoneTextFormatter(binding.editTextEmpPhnum, "(###) ###-####"))

        // calendar
        binding.editTextStartDate.showSoftInputOnFocus = false
        binding.editTextStartDate.setOnClickListener { openCalendar() }
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

    var maxDate:Long = 0
    var minDate:Long = 0

    private fun openCalendar() {
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        //val newMonth = month + 1

        /*
        val dpd = DatePickerDialog(
            requireActivity(), {
                view, year, monthOfYear, dayOfMonth -> binding.edStartDate.setText("" + newMonth + "/" + dayOfMonth + "/" + year)
                val cal = Calendar.getInstance()
                cal.set(year, newMonth, dayOfMonth)
                val date = DateFormat.format("dd-MM-yyyy", cal).toString()
                maxDate = convertDateToLong(date)
            }, year, month, day)
         */


        val datePickerDialog = DatePickerDialog(
            requireActivity(), R.style.MySpinnerDatePickerStyle,
            {
                    view, selectedYear, monthOfYear, dayOfMonth ->
                binding.editTextStartDate.setText("" + (monthOfYear+1) + "/" + dayOfMonth + "/" + selectedYear)
                val cal = Calendar.getInstance()
                cal.set(selectedYear, (monthOfYear), dayOfMonth)
                val date = DateFormat.format("dd-MM-yyyy", cal).toString()
                maxDate = convertDateToLong(date)
            }
            , year, month, day
        )
        if(minDate!=0L)
            datePickerDialog.datePicker.maxDate = minDate
        datePickerDialog.show()

    }

    private fun convertDateToLong(date: String): Long {
        val df = SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH)
        return df.parse(date).time
    }

}