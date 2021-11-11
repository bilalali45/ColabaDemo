package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.SelfEmpolymentContLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import java.util.*
import javax.inject.Inject

/**
 * Created by Anita Kiran on 9/15/2021.
 */
@AndroidEntryPoint
class SelfEmploymentContractor : BaseFragment(),View.OnClickListener {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private val viewModel : IncomeViewModel by activityViewModels()
    private lateinit var binding: SelfEmpolymentContLayoutBinding
    private lateinit var toolbarBinding: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null
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
            binding = SelfEmpolymentContLayoutBinding.inflate(inflater, container, false)
            toolbarBinding = binding.headerIncome
            savedViewInstance = binding.root
            super.addListeners(binding.root)
            // set Header title
            toolbarBinding.toolbarTitle.setText(getString(R.string.self_employment_contractor))

            arguments?.let { arguments ->
                loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
                borrowerId = arguments.getInt(AppConstant.borrowerId)
                incomeInfoId = arguments.getInt(AppConstant.incomeId)
                //incomeCategoryId = arguments.getInt(AppConstant.incomeCategoryId)
                //incomeTypeID = arguments.getInt(AppConstant.incomeTypeID)
            }

            initViews()
            getData()
            savedViewInstance

        }
    }

    private fun initViews() {
        binding.layoutAddress.setOnClickListener(this)
        toolbarBinding.btnClose.setOnClickListener(this)
        binding.mainLayoutBusinessCont.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)

        setInputFields()

    }

    private fun getData(){

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (borrowerId != null && incomeInfoId != null) {
                    binding.loaderSelfEmployment.visibility = View.VISIBLE
                    viewModel.getSelfEmploymentDetail(authToken,borrowerId!!,incomeInfoId!!)

                    viewModel.selfEmploymentDetail.observe(viewLifecycleOwner, { data ->
                        data?.selfEmploymentData?.let { info ->

                            info.businessName?.let {
                                binding.editTextBusinessName.setText(it)
                                CustomMaterialFields.setColor(binding.layoutBusinessName, R.color.grey_color_two, requireContext())
                            }
                            info.businessPhone?.let {
                                binding.editTextBusPhnum.setText(it)
                                CustomMaterialFields.setColor(binding.layoutBusPhnum, R.color.grey_color_two, requireContext())
                            }
                            info.startDate?.let {
                                binding.editTextBstartDate.setText(AppSetting.getFullDate1(it))
                            }
                            info.jobTitle?.let {
                                binding.edJobTitle.setText(it)
                                CustomMaterialFields.setColor(binding.layoutJobTitle, R.color.grey_color_two, requireContext())
                            }
                            info.annualIncome?.let {
                                binding.edNetIncome.setText(Math.round(it).toString())
                                CustomMaterialFields.setColor(binding.layoutNetIncome, R.color.grey_color_two, requireContext())
                            }

                        info.address?.let {
                            val builder = StringBuilder()
                            it.street?.let { builder.append(it).append(" ") }
                            it.unit?.let { builder.append(it).append("\n") }
                            it.city?.let { builder.append(it).append(" ") }
                            it.stateName?.let{ builder.append(it).append(" ")}
                            it.zipCode?.let { builder.append(it) }
                            binding.textviewBusinessAddress.text = builder
                        }

                        }
                        binding.loaderSelfEmployment.visibility = View.GONE
                    })

                }
            }
        }

    }

    private fun openAddressFragment(){
        val addressFragment = AddressBusiness()
        val bundle = Bundle()
        bundle.putString(AppConstant.address, getString(R.string.business_main_address))
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_business_address, addressFragment.arguments)
    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> checkValidations()
            R.id.layout_address -> openAddressFragment()
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_businessCont -> {
                HideSoftkeyboard.hide(requireActivity(), binding.mainLayoutBusinessCont)
                super.removeFocusFromAllFields(binding.mainLayoutBusinessCont)
            }
        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.editTextBusinessName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextBusinessName, binding.layoutBusinessName, requireContext()))
        binding.editTextBusPhnum.setOnFocusChangeListener(FocusListenerForPhoneNumber(binding.editTextBusPhnum, binding.layoutBusPhnum,requireContext()))
        binding.edJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edJobTitle, binding.layoutJobTitle, requireContext()))
        binding.editTextBstartDate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.editTextBstartDate, binding.layoutBStartDate, requireContext()))
        binding.edNetIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edNetIncome, binding.layoutNetIncome, requireContext()))

        // set input format
        binding.edNetIncome.addTextChangedListener(NumberTextFormat(binding.edNetIncome))
        binding.editTextBusPhnum.addTextChangedListener(PhoneTextFormatter(binding.editTextBusPhnum, "(###) ###-####"))


        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutNetIncome, requireContext())

        binding.editTextBstartDate.showSoftInputOnFocus = false
        binding.editTextBstartDate.setOnClickListener { openCalendar() }
        binding.editTextBstartDate.doAfterTextChanged {
            if (binding.editTextBstartDate.text?.length == 0) {
                CustomMaterialFields.setColor(binding.layoutBStartDate,R.color.grey_color_three,requireActivity())
            } else {
                CustomMaterialFields.setColor(binding.layoutBStartDate,R.color.grey_color_two,requireActivity())
                CustomMaterialFields.clearError(binding.layoutBStartDate,requireActivity())
            }
        }
    }

    private fun checkValidations(){

        val businessName: String = binding.editTextBusinessName.text.toString()
        val jobTitle: String = binding.edJobTitle.text.toString()
        val startDate: String = binding.editTextBstartDate.text.toString()
        val netIncome: String = binding.edNetIncome.text.toString()

        if (businessName.isEmpty() || businessName.length == 0) {
            CustomMaterialFields.setError(binding.layoutBusinessName, getString(R.string.error_field_required),requireActivity())
        }
        if (jobTitle.isEmpty() || jobTitle.length == 0) {
            CustomMaterialFields.setError(binding.layoutJobTitle, getString(R.string.error_field_required),requireActivity())
        }
        if (startDate.isEmpty() || startDate.length == 0) {
            CustomMaterialFields.setError(binding.layoutBStartDate, getString(R.string.error_field_required),requireActivity())
        }
        if (netIncome.isEmpty() || netIncome.length == 0) {
            CustomMaterialFields.setError(binding.layoutNetIncome, getString(R.string.error_field_required),requireActivity())
        }
        if (businessName.isNotEmpty() || businessName.length > 0) {
            CustomMaterialFields.clearError(binding.layoutBusinessName,requireActivity())
        }
        if (jobTitle.isNotEmpty() || jobTitle.length > 0) {
            CustomMaterialFields.clearError(binding.layoutJobTitle,requireActivity())
        }
        if (startDate.isNotEmpty() || startDate.length > 0) {
            CustomMaterialFields.clearError(binding.layoutBStartDate,requireActivity())
        }
        if (netIncome.isNotEmpty() || netIncome.length > 0) {
            CustomMaterialFields.clearError(binding.layoutNetIncome,requireActivity())
        }
        if (businessName.length > 0 && jobTitle.length > 0 &&  startDate.length > 0 && netIncome.length > 0 ){
            findNavController().popBackStack()
        }

        if (businessName.length > 0 && jobTitle.length > 0 &&  startDate.length > 0 && netIncome.length > 0 ){
            //findNavController().popBackStack()
            Log.e("btnClick","sendData")
            val address = SelfEmploymentAddress(city = null,countryId = null,countryName = null,stateId = null,stateName = null,unit = null,
                zipCode = null,street = null,countyId = 1,countyName = "kk")
            val selfEmploymentData = SelfEmploymentData(loanApplicationId=loanApplicationId,borrowerId=borrowerId,id= incomeInfoId,
                businessName = "Colaba", businessPhone = "123456",jobTitle = "Developer",
                startDate = "12-01-2020",address = address,annualIncome= 120000.0)

            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    viewModel.sendSelfEmploymentData(authToken,selfEmploymentData)
                }
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
            { view, year, monthOfYear, dayOfMonth -> binding.editTextBstartDate.setText("" + newMonth + "/" + dayOfMonth + "/" + year) },
            year,
            month,
            day
        )
        dpd.show()
    }
}