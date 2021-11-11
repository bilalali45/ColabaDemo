package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.text.format.DateFormat
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomePreviousEmploymentBinding
import com.rnsoft.colabademo.databinding.StockBondsLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject

/**
 * Created by Anita Kiran on 9/13/2021.
 */
@AndroidEntryPoint
class IncomePreviousEmployment : BaseFragment(),View.OnClickListener {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: IncomePreviousEmploymentBinding
    private lateinit var toolbar: AppHeaderWithCrossDeleteBinding
    //private var savedViewInstance: View? = null
    private val viewModel : IncomeViewModel by activityViewModels()
    private var loanApplicationId: Int? = null
    private var incomeInfoId :Int? = null
    private var borrowerId :Int? = null
    var addressList :  ArrayList<EmployerAddress> = ArrayList()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = IncomePreviousEmploymentBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)
        // set Header title
        toolbar =  binding.headerIncome
        toolbar.toolbarTitle.setText(getString(R.string.previous_employment))

        arguments?.let { arguments ->
            loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
            borrowerId = arguments.getInt(AppConstant.borrowerId)
            incomeInfoId = arguments.getInt(AppConstant.incomeId)
        }

        initViews()
        getEmploymentData()
        return binding.root


        /*return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = IncomePreviousEmploymentBinding.inflate(inflater, container, false)
            toolbar = binding.headerIncome
            savedViewInstance = binding.root
            super.addListeners(binding.root)
            // set Header title
            toolbar.toolbarTitle.setText(getString(R.string.previous_employment))

            arguments?.let { arguments ->
                loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
                borrowerId = arguments.getInt(AppConstant.borrowerId)
                incomeInfoId = arguments.getInt(AppConstant.incomeId)
            }

            initViews()
            getEmploymentData()
            savedViewInstance
        } */
    }

    private fun getEmploymentData(){

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (loanApplicationId != null && incomeInfoId != null && borrowerId != null) {
                    binding.loaderEmployment.visibility = View.VISIBLE
                    viewModel.getPrevEmploymentDetail(authToken, loanApplicationId!!, borrowerId!!, incomeInfoId!!)
                }
                else{

                    Timber.e(" some id is null")
                }
            }
        }

        viewModel.prevEmploymentDetail.observe(viewLifecycleOwner, { data ->

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
                info?.endDate?.let {
                    binding.editTextEndDate.setText(AppSetting.getFullDate1(it))
                }
                info?.yearsInProfession?.let {
                    binding.editTextProfYears.setText(it.toString())
                    CustomMaterialFields.setColor(binding.layoutYearsProfession, R.color.grey_color_two, requireContext())
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
                addressList.add(EmployerAddress(
                    streetAddress = it.streetAddress,
                    unitNo = it.unitNo,
                    cityName = it.cityName,
                    cityId = it.cityId,
                    stateName = it.stateName,
                    countryName = it.countryName,
                    //countyName = it.,
                    //countyId = it.countyId,
                    stateId = it.stateId,
                    countryId = it.countryId,
                    zipCode = it.zipCode,
                    borrowerId = it.borrowerId,
                    loanApplicationId = it.loanApplicationId,
                    incomeInfoId = it.incomeInfoId
                ))

                val builder = StringBuilder()
                it.streetAddress?.let { builder.append(it).append(" ") }
                it.unitNo?.let { builder.append(it).append("\n") }
                it.cityName?.let { builder.append(it).append(" ") }
                it.stateName?.let{ builder.append(it).append(" ")}
                it.zipCode?.let { builder.append(it) }
                binding.tvPrevEmploymentAddress.text = builder
            }

            binding.loaderEmployment.visibility = View.GONE
        })
    }

    private fun initViews() {
        binding.rbOwnershipYes.setOnClickListener(this)
        binding.rbOwnershipNo.setOnClickListener(this)
        binding.layoutAddress.setOnClickListener(this)
        toolbar.btnClose.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)
        binding.mainLayoutPrevEmployment.setOnClickListener(this)

        setInputFields()
    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> checkValidations()
            R.id.rb_ownership_yes -> ownershipInterest()
            R.id.rb_ownership_no -> ownershipInterest()
            R.id.layout_address -> openAddressFragment() //findNavController().navigate(R.id.action_address)
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_prev_employment ->  {
                HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutPrevEmployment)
                super.removeFocusFromAllFields(binding.mainLayoutPrevEmployment)
            }
        }
    }

    private fun checkValidations(){
        val empName: String = binding.editTextEmpName.text.toString()
        val jobTitle: String = binding.editTextJobTitle.text.toString()
        val startDate: String = binding.editTextStartDate.text.toString()
        val endDate: String = binding.editTextEndDate.text.toString()
        val profYears: String = binding.editTextProfYears.text.toString()
        val netIncome: String = binding.edNetIncome.text.toString()

        if (empName.isEmpty() || empName.length == 0) {
            CustomMaterialFields.setError(binding.layoutEmpName, getString(R.string.error_field_required),requireActivity())
        }
        if (jobTitle.isEmpty() || jobTitle.length == 0) {
            CustomMaterialFields.setError(binding.layoutJobTitle, getString(R.string.error_field_required),requireActivity())
        }
        if (startDate.isEmpty() || startDate.length == 0) {
            CustomMaterialFields.setError(binding.layoutStartDate, getString(R.string.error_field_required),requireActivity())
        }
        if (endDate.isEmpty() || endDate.length == 0) {
            CustomMaterialFields.setError(binding.layoutEndDate, getString(R.string.error_field_required),requireActivity())
        }
        if (profYears.isEmpty() || profYears.length == 0) {
            CustomMaterialFields.setError(binding.layoutYearsProfession, getString(R.string.error_field_required),requireActivity())
        }
        if (netIncome.isEmpty() || netIncome.length == 0) {
            CustomMaterialFields.setError(binding.layoutNetIncome, getString(R.string.error_field_required),requireActivity())
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
        if (endDate.isNotEmpty() || endDate.length > 0) {
            CustomMaterialFields.clearError(binding.layoutEndDate,requireActivity())
        }
        if (netIncome.isNotEmpty() || netIncome.length > 0) {
            CustomMaterialFields.clearError(binding.layoutYearsProfession,requireActivity())
        }
        if (profYears.isNotEmpty() || profYears.length > 0) {
            CustomMaterialFields.clearError(binding.layoutYearsProfession,requireActivity())
        }
        if (empName.length > 0 && jobTitle.length > 0 &&  startDate.length > 0 && endDate.length > 0 && profYears.length > 0 && netIncome.length > 0){
            findNavController().popBackStack()
        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.editTextEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextEmpName, binding.layoutEmpName, requireContext()))
        binding.editTextEmpPhnum.setOnFocusChangeListener(FocusListenerForPhoneNumber(binding.editTextEmpPhnum, binding.layoutEmpPhnum,requireContext()))
        binding.editTextJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextJobTitle, binding.layoutJobTitle, requireContext()))
        binding.editTextProfYears.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextProfYears, binding.layoutYearsProfession, requireContext()))
        binding.edOwnershipPercent.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOwnershipPercent, binding.layoutOwnershipPercentage, requireContext()))
        binding.edNetIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edNetIncome, binding.layoutNetIncome, requireContext()))

        // set input format
        binding.edNetIncome.addTextChangedListener(NumberTextFormat(binding.edNetIncome))
        binding.editTextEmpPhnum.addTextChangedListener(PhoneTextFormatter(binding.editTextEmpPhnum, "(###) ###-####"))


        // set Dollar prifix
        CustomMaterialFields.setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutNetIncome, requireContext())
        CustomMaterialFields.setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())

        // start date
        binding.editTextStartDate.showSoftInputOnFocus = false
        binding.editTextStartDate.setOnClickListener { openCalendar() }
        binding.editTextStartDate.doAfterTextChanged {
            if (binding.editTextStartDate.text?.length == 0) {
                CustomMaterialFields.setColor(binding.layoutStartDate,R.color.grey_color_three,requireActivity())
            } else {
                CustomMaterialFields.setColor(binding.layoutStartDate,R.color.grey_color_two,requireActivity())
                CustomMaterialFields.clearError(binding.layoutStartDate,requireActivity())
            }
        }

        // end date
        binding.editTextEndDate.showSoftInputOnFocus = false
        binding.editTextEndDate.setOnClickListener { endDateCalendar() }
        binding.editTextEndDate.doAfterTextChanged {
            if (binding.editTextEndDate.text?.length == 0) {
                CustomMaterialFields.setColor(binding.layoutEndDate,R.color.grey_color_three,requireActivity())
            } else {
                CustomMaterialFields.setColor(binding.layoutEndDate,R.color.grey_color_two,requireActivity())
                CustomMaterialFields.clearError(binding.layoutEndDate,requireActivity())
            }
        }
    }

    private fun ownershipInterest(){
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

    private fun openAddressFragment(){
        val addressFragment = AddressPrevEmployment()
        val bundle = Bundle()
        bundle.putString(AppConstant.TOOLBAR_TITLE, getString(R.string.previous_employer_address))
        bundle.putParcelableArrayList(AppConstant.address,addressList)
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_prev_employment_address, addressFragment.arguments)
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

        //Date().time = cal.time
        // dpd.show()
    }

    private fun convertDateToLong(date: String): Long {
        val df = SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH)
        return df.parse(date).time
    }

    private fun endDateCalendar() {
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        //val newMonth = month + 1

        /*
        val dpd = DatePickerDialog(
            requireActivity(),
            { view, year, monthOfYear, dayOfMonth -> binding.edEndDate.setText("" + newMonth + "/" + dayOfMonth + "/" + year) },
            year,
            month,
            day
        )
        dpd.show()
         */

        val datePickerDialog = DatePickerDialog(
            requireActivity(), R.style.MySpinnerDatePickerStyle,
            {
                    view, selectedYear, monthOfYear, dayOfMonth ->
                binding.editTextEndDate.setText("" + (monthOfYear+1) + "/" + dayOfMonth + "/" + selectedYear)
                val cal = Calendar.getInstance()
                cal.set(selectedYear, monthOfYear, dayOfMonth)
                val date = DateFormat.format("dd-MM-yyyy", cal).toString()
                minDate = convertDateToLong(date)
            }
            , year, month, day
        )
        if(maxDate!=0L)
            datePickerDialog.datePicker.minDate = maxDate
        datePickerDialog.show()


    }

}