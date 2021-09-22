package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.SelfEmpolymentContLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import java.util.*

/**
 * Created by Anita Kiran on 9/15/2021.
 */
class SelfEmploymentContractor : Fragment(),View.OnClickListener {

    private lateinit var binding: SelfEmpolymentContLayoutBinding
    private lateinit var toolbarBinding: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null

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

            // set Header title
            toolbarBinding.toolbarTitle.setText(getString(R.string.self_employment_contractor))

            initViews()
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

    private fun openAddressFragment(){
        val addressFragment = IncomeAddress()
        val bundle = Bundle()
        bundle.putString("address", "Business Main Address")
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_address, addressFragment.arguments)
    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> checkValidations()
            R.id.layout_address -> openAddressFragment()
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_businessCont -> HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutBusinessCont)

        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.edBusinessName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBusinessName, binding.layoutBusinessName, requireContext()))
        binding.edBusPhnum.setOnFocusChangeListener(FocusListenerForPhoneNumber(binding.edBusPhnum, binding.layoutBusPhnum,requireContext()))
        binding.edJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edJobTitle, binding.layoutJobTitle, requireContext()))
        binding.edBstartDate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBstartDate, binding.layoutBStartDate, requireContext()))
        binding.edNetIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edNetIncome, binding.layoutNetIncome, requireContext()))

        // set input format
        binding.edNetIncome.addTextChangedListener(NumberTextFormat(binding.edNetIncome))
        binding.edBusPhnum.addTextChangedListener(PhoneTextFormatter(binding.edBusPhnum, "(###) ###-####"))


        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutNetIncome, requireContext())

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

    private fun checkValidations(){

        val businessName: String = binding.edBusinessName.text.toString()
        val jobTitle: String = binding.edJobTitle.text.toString()
        val startDate: String = binding.edBstartDate.text.toString()
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