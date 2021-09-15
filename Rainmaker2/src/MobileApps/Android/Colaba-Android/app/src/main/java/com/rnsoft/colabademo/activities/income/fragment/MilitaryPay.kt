package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeMilitaryPayBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat
import java.util.*

/**
 * Created by Anita Kiran on 9/15/2021.
 */
class MilitaryPay : Fragment(), View.OnClickListener {

    private lateinit var binding: IncomeMilitaryPayBinding
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
            binding = IncomeMilitaryPayBinding.inflate(inflater, container, false)
            toolbarBinding = binding.headerIncome
            savedViewInstance = binding.root

            // set Header title
            toolbarBinding.toolbarTitle.setText(getString(R.string.military_pay))

            initViews()
            savedViewInstance

        }
    }


    private fun initViews() {
        binding.edStartDate.setOnClickListener(this)
        binding.edStartDate.showSoftInputOnFocus = false
        binding.edStartDate.setOnClickListener { openCalendar() }
        binding.edStartDate.setOnFocusChangeListener { _, _ -> openCalendar() }

        binding.layoutAddress.setOnClickListener(this)
        toolbarBinding.btnClose.setOnClickListener(this)
        binding.mainLayoutMilitaryPay.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)


        setInputFields()

    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> findNavController().popBackStack()
            R.id.layout_address -> openAddressFragment()
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_military_pay -> HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutMilitaryPay)

        }
    }

    private fun openAddressFragment(){
        val addressFragment = IncomeAddress()
        val bundle = Bundle()
        bundle.putString("address", "Service Location Address")
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_address, addressFragment.arguments)
    }


    private fun setInputFields() {

        // set lable focus
        binding.edEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEmpName, binding.layoutEmpName, requireContext()))
        binding.edJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edJobTitle, binding.layoutJobTitle, requireContext()))
        binding.edProfYears.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edProfYears, binding.layoutYearsProfession, requireContext()))
        binding.edBaseSalary.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBaseSalary, binding.layoutBaseSalary, requireContext()))
        binding.edEntitlement.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEntitlement, binding.layoutEntitlement, requireContext()))


        // set input format
        binding.edBaseSalary.addTextChangedListener(NumberTextFormat(binding.edBaseSalary))
        binding.edEntitlement.addTextChangedListener(NumberTextFormat(binding.edEntitlement))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutBaseSalary, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutEntitlement, requireContext())

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