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
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
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
    var loanApplicationId: Int? = null
    var incomeInfoId :Int? = null
    var borrowerId :Int? = null
    private var businessAddress = AddressData()
    private var savedViewInstance: View? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
         return if (savedViewInstance != null){
            savedViewInstance
        } else {
             binding = SelfEmpolymentContLayoutBinding.inflate(inflater, container, false)
             savedViewInstance = binding.root
             toolbarBinding = binding.headerIncome
             super.addListeners(binding.root)
             // set Header title
             toolbarBinding.toolbarTitle.setText(getString(R.string.self_employment_contractor))


             arguments?.let { arguments ->
                 loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
                 borrowerId = arguments.getInt(AppConstant.borrowerId)
                 arguments.getInt(AppConstant.incomeId).let {
                     if (it > 0)
                         incomeInfoId = it
                 }
             }

             findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<AddressData>(
                 AppConstant.address
             )?.observe(viewLifecycleOwner) { result ->
                 businessAddress = result
                 displayAddress(result)
             }

             initViews()
             getData()

             savedViewInstance
         }
    }

    private fun initViews(){
        binding.layoutAddress.setOnClickListener(this)
        toolbarBinding.btnClose.setOnClickListener(this)
        binding.mainLayoutBusinessCont.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)
        setInputFields()
    }

    private fun getData(){
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (borrowerId != null && incomeInfoId != null && incomeInfoId !=null) {
                    binding.loaderSelfEmployment.visibility = View.VISIBLE
                    viewModel.getSelfEmploymentDetail(authToken, borrowerId!!, incomeInfoId!!)

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
                                CustomMaterialFields.setColor(
                                    binding.layoutJobTitle,
                                    R.color.grey_color_two,
                                    requireContext()
                                )
                            }
                            info.annualIncome?.let {
                                binding.edNetIncome.setText(Math.round(it).toString())
                                CustomMaterialFields.setColor(
                                    binding.layoutNetIncome,
                                    R.color.grey_color_two,
                                    requireContext()
                                )
                            }

                            info.businessAddress?.let {
                               businessAddress = it
                                displayAddress(it)
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
        bundle.putString(AppConstant.TOOLBAR_TITLE, getString(R.string.business_main_address))
        bundle.putParcelable(AppConstant.address,businessAddress)
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
            val businessPhone = if( binding.editTextBusPhnum.text.toString().trim().length >0 ) binding.editTextBusPhnum.text.toString().trim() else null
            val newNetIncome = if(netIncome.length > 0) netIncome.replace(",".toRegex(), "") else null


            val selfEmploymentData = SelfEmploymentData(loanApplicationId=loanApplicationId,borrowerId=borrowerId,id= incomeInfoId,
                businessName = businessName, businessPhone = businessPhone,jobTitle = jobTitle,
                startDate = startDate,businessAddress = businessAddress,annualIncome= newNetIncome?.toDouble())

            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    if(loanApplicationId != null && borrowerId !=null) {
                        Log.e("Loan Application Id", "" + loanApplicationId + " borrowerId:  " + borrowerId + " income:  " + incomeInfoId)
                        Log.e("selfEmployment-snding to API", "" + selfEmploymentData)

                        binding.loaderSelfEmployment.visibility = View.VISIBLE
                        viewModel.sendSelfEmploymentData(authToken, selfEmploymentData)
                    }
                }
            }
        }
    }

    private fun displayAddress(it: AddressData){
        val builder = StringBuilder()
        it.street?.let { builder.append(it).append(" ") }
        it.unit?.let { builder.append(it).append("\n") }
        it.city?.let { builder.append(it).append(" ") }
        it.stateName?.let{ builder.append(it).append(" ")}
        it.zipCode?.let { builder.append(it) }
        it.countryName?.let { builder.append(" ").append(it)}
        binding.textviewBusinessAddress.text = builder
    }

    private fun openCalendar() {
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val newMonth = month + 1

        val dpd = DatePickerDialog(
            requireActivity(), { view, year, monthOfYear, dayOfMonth -> binding.editTextBstartDate.setText("" + newMonth + "/" + dayOfMonth + "/" + year) },
            year,
            month,
            day)
        dpd.show()
    }

    override fun onResume() {
        super.onResume()
        findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<AddressData>(
            AppConstant.address)?.observe(viewLifecycleOwner) { result ->
            businessAddress = result
            //binding.textviewCurrentEmployerAddress.text = result.street + " " + result.unit + "\n" + result.city + " " + result.stateName + " " + result.zipCode + " " + result.countryName
            displayAddress(result)
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
            binding.loaderSelfEmployment.visibility = View.GONE
        }

        else if(event.addUpdateDataResponse.code == AppConstant.INTERNET_ERR_CODE)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG)

        else
            if(event.addUpdateDataResponse.message != null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG)

        findNavController().popBackStack()
    }
}