package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeBusinessLayoutBinding
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
class BusinessIncomeFragment : BaseFragment(), View.OnClickListener {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: IncomeBusinessLayoutBinding
    private lateinit var toolbarBinding: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null
    //private val businessTypeArray = listOf("Partnership (e.g. LLC, LP, or GP","Corporation (e.g. C-Corp, S-Corp, or LLC")
    private val viewModel : IncomeViewModel by activityViewModels()
    var incomeInfoId :Int? = null
    var borrowerId :Int? = null
    private var businessTypes: ArrayList<DropDownResponse> = arrayListOf()
    var addressList :  ArrayList<BusinessIncomeAddress> = ArrayList()


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = IncomeBusinessLayoutBinding.inflate(inflater, container, false)
            toolbarBinding = binding.headerIncome
            savedViewInstance = binding.root

            // set Header title
            toolbarBinding.toolbarTitle.setText(getString(R.string.business))

            arguments?.let { arguments ->
                borrowerId = arguments.getInt(AppConstant.borrowerId)
                incomeInfoId = arguments.getInt(AppConstant.incomeId)
            }

            initViews()
            observeBusinesstIncomeTypes()
            super.addListeners(binding.root)
            savedViewInstance

        }
    }


    private fun initViews(){

        binding.layoutAddress.setOnClickListener(this)
        toolbarBinding.btnClose.setOnClickListener(this)
        binding.mainLayoutBusiness.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)

        setInputFields()
        setBusinessType()
        observeBusinesstIncomeTypes()

    }

    private fun observeBusinesstIncomeTypes(){
        lifecycleScope.launchWhenStarted {
            viewModel.businessTypes.observe(viewLifecycleOwner, { types ->
                if(types.size > 0) {
                    val itemList:ArrayList<String> = arrayListOf()
                    businessTypes = arrayListOf()
                    for (item in types) {
                        itemList.add(item.name)
                        businessTypes.add(item)
                    }
                    //Timber.e("itemList- $itemList")
                    //Timber.e("BusinessTypes- $businessTypes")
                    val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, itemList)
                    binding.tvBusinessType.setAdapter(adapter)
                }
                else
                    findNavController().popBackStack()
            })
        }

        getBusinessDetails()
    }

    private fun getBusinessDetails(){

        //Timber.e( "borrowerId:  " + borrowerId + "incomeInfoId: " + incomeInfoId )

        lifecycleScope.launchWhenStarted{
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (borrowerId != null && incomeInfoId != null) {
                    binding.loaderIncomeBusiness.visibility = View.VISIBLE
                    viewModel.getIncomeBusiness(authToken, borrowerId!!, incomeInfoId!!)
                }
            }
        }

        viewModel.businessIncome.observe(viewLifecycleOwner, { data ->
            data?.businessData?.let { info ->
                info.businessName?.let {
                    binding.edBusinessName.setText(it)
                    CustomMaterialFields.setColor(binding.layoutBusinessName, R.color.grey_color_two, requireContext())
                }
                info.businessPhone?.let {
                    binding.edBusPhnum.setText(it)
                    CustomMaterialFields.setColor(binding.layoutBusPhnum, R.color.grey_color_two, requireContext())
                }
                info.startDate?.let {
                    binding.edBstartDate.setText(AppSetting.getFullDate1(it))
                }
                info.jobTitle?.let {
                    binding.edJobTitle.setText(it)
                    CustomMaterialFields.setColor(binding.layoutJobTitle, R.color.grey_color_two, requireContext())
                }
                info.ownershipPercentage?.let {
                    binding.edOwnershipPercent.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutOwnershipPercentage, R.color.grey_color_two, requireContext())
                }
                info.annualIncome?.let {
                    binding.edNetIncome.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutNetIncome, R.color.grey_color_two, requireContext())
                }

                info.address?.let {
                    addressList.add(BusinessIncomeAddress(
                        street = it.street,
                        unit = it.unit,
                        city = it.city,
                        stateName = it.stateName,
                        countryName = it.countryName,
                        countyName = it.countyName,
                        countyId = it.countyId,
                        stateId = it.stateId,
                        countryId = it.countryId,
                        zipCode = it.zipCode
                    ))

                    val builder = StringBuilder()
                    it.street?.let { builder.append(it).append(" ") }
                    it.unit?.let { builder.append(it).append("\n") }
                    it.city?.let { builder.append(it).append(" ") }
                    it.stateName?.let{ builder.append(it).append(" ")}
                    it.zipCode?.let { builder.append(it) }
                    binding.textviewBusinessAddress.text = builder
                }

                info.incomeTypeId?.let { id ->
                    for(item in businessTypes)
                        if(id == item.id){
                            binding.tvBusinessType.setText(item.name, false)
                            break
                        }

                }
            }
            binding.loaderIncomeBusiness.visibility = View.GONE
        })

    }

    override fun onClick(view: View?){
        when (view?.getId()) {
            R.id.btn_save_change -> checkValidations()
            R.id.layout_address -> openAddressFragment()
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_business -> {
                HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutBusiness)
                super.removeFocusFromAllFields(binding.mainLayoutBusiness)
            }
        }
    }

    private fun checkValidations(){

        val businessType: String = binding.tvBusinessType.text.toString()
        val businessName: String = binding.edBusinessName.text.toString()
        val jobTitle: String = binding.edJobTitle.text.toString()
        val startDate: String = binding.edBstartDate.text.toString()
        val percentage: String = binding.edOwnershipPercent.text.toString()
        val netIncome: String = binding.edNetIncome.text.toString()


        if (businessType.isEmpty() || businessType.length == 0) {
            CustomMaterialFields.setError(binding.layoutBusinessType, getString(R.string.error_field_required),requireActivity())
        }
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
        if (percentage.isEmpty() || percentage.length == 0) {
            CustomMaterialFields.setError(binding.layoutOwnershipPercentage, getString(R.string.error_field_required),requireActivity())
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
        if (percentage.isNotEmpty() || percentage.length > 0) {
            CustomMaterialFields.clearError(binding.layoutOwnershipPercentage,requireActivity())
        }
        if (businessType.length > 0 && businessName.length > 0 && jobTitle.length > 0 &&  startDate.length > 0 && netIncome.length > 0  && percentage.length > 0){
            findNavController().popBackStack()
        }
    }

    private fun openAddressFragment(){
        val addressFragment = AddressBusiness()
        val bundle = Bundle()
        bundle.putString(AppConstant.TOOLBAR_TITLE, getString(R.string.business_main_address))
        bundle.putParcelableArrayList(AppConstant.address,addressList)
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_address, addressFragment.arguments)
    }

    private fun setInputFields() {

        binding.edBusinessName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBusinessName, binding.layoutBusinessName, requireContext()))
        binding.edBusPhnum.setOnFocusChangeListener(FocusListenerForPhoneNumber(binding.edBusPhnum, binding.layoutBusPhnum,requireContext()))
        binding.edBstartDate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBstartDate, binding.layoutBStartDate, requireContext()))
        binding.edJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edJobTitle, binding.layoutJobTitle, requireContext()))
        binding.edNetIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edNetIncome, binding.layoutNetIncome, requireContext()))
        binding.edOwnershipPercent.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOwnershipPercent, binding.layoutOwnershipPercentage, requireContext()))


        // set input format
        binding.edNetIncome.addTextChangedListener(NumberTextFormat(binding.edNetIncome))
        binding.edBusPhnum.addTextChangedListener(PhoneTextFormatter(binding.edBusPhnum, "(###) ###-####"))


        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutNetIncome, requireContext())
        CustomMaterialFields.setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())

       // start date
        binding.edBstartDate.showSoftInputOnFocus = false
        binding.edBstartDate.setOnClickListener { openCalendar() }

        binding.edBstartDate.doAfterTextChanged {
            if (binding.edBstartDate.text?.length == 0) {
                CustomMaterialFields.setColor(binding.layoutBStartDate,R.color.grey_color_three,requireActivity())
            } else {
                CustomMaterialFields.setColor(binding.layoutBStartDate,R.color.grey_color_two,requireActivity())
                CustomMaterialFields.clearError(binding.layoutBStartDate,requireActivity())
            }
        }

    }

    private fun setBusinessType(){
        //val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, businessTypeArray)
        //binding.tvBusinessType.setAdapter(adapter)
        binding.tvBusinessType.setOnFocusChangeListener { _, _ ->
            binding.tvBusinessType.showDropDown()
        }
        binding.tvBusinessType.setOnClickListener {
            binding.tvBusinessType.showDropDown()
        }
        binding.tvBusinessType.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutBusinessType.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(), R.color.grey_color_two
                    )
                )

                if (binding.tvBusinessType.text.isNotEmpty() && binding.tvBusinessType.text.isNotBlank()) {
                    CustomMaterialFields.clearError(binding.layoutBusinessType,requireActivity())
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
            { view, year, monthOfYear, dayOfMonth -> binding.edBstartDate.setText("" + newMonth + "/" + dayOfMonth + "/" + year) },
            year,
            month,
            day
        )
        dpd.show()
    }
}