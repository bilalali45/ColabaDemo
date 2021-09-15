package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomePreviousEmploymentBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat
import java.util.*

/**
 * Created by Anita Kiran on 9/13/2021.
 */
class IncomePreviousEmployment : Fragment(),View.OnClickListener {

    private lateinit var binding: IncomePreviousEmploymentBinding
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
            binding = IncomePreviousEmploymentBinding.inflate(inflater, container, false)
            toolbar = binding.headerIncome
            savedViewInstance = binding.root


            // set Header title
            toolbar.toolbarTitle.setText(getString(R.string.previous_employment))

            initViews()
            savedViewInstance

        }

    }


    private fun initViews() {
        /*binding.edStartDate.setOnClickListener(this)
        binding.edStartDate.showSoftInputOnFocus = false
        binding.edStartDate.setOnClickListener { openCalendar() }
        binding.edStartDate.setOnFocusChangeListener { _, _ -> openCalendar() } */

        binding.edEndDate.setOnClickListener(this)
        binding.edEndDate.showSoftInputOnFocus = false
        binding.edEndDate.setOnClickListener { openCalendar() }
        binding.edEndDate.setOnFocusChangeListener { _, _ -> openCalendar() }

        binding.rbOwnershipYes.setOnClickListener(this)
        binding.rbOwnershipNo.setOnClickListener(this)

        binding.layoutAddress.setOnClickListener(this)
        toolbar.btnClose.setOnClickListener(this)


        setInputFields()

    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.rb_ownership_yes -> ownershipInterest()
            R.id.rb_ownership_no -> ownershipInterest()
            R.id.layout_address -> openAddressFragment() //findNavController().navigate(R.id.action_address)
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_prev_employment -> HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutPrevEmployment)

        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.edEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEmpName, binding.layoutEmpName, requireContext()))
        binding.edEmpPhnum.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEmpPhnum, binding.layoutEmpPhnum,requireContext()))
        binding.edJobTitle.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edJobTitle, binding.layoutJobTitle, requireContext()))
        binding.edProfYears.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edProfYears, binding.layoutYearsProfession, requireContext()))
        binding.edStartDate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edStartDate, binding.layoutStartDate, requireContext()))
        //binding.edEndDate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEndDate, binding.layoutEndDate, requireContext()))
        binding.edOwnershipPercent.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edOwnershipPercent, binding.layoutOwnershipPercentage, requireContext()))
        binding.edNetIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edNetIncome, binding.layoutNetIncome, requireContext()))

        // set input format
        binding.edNetIncome.addTextChangedListener(NumberTextFormat(binding.edNetIncome))
        binding.edEmpPhnum.addTextChangedListener(PhoneTextFormatter(binding.edEmpPhnum, "(###) ###-####"))


        // set Dollar prifix
        CustomMaterialFields.setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())

        CustomMaterialFields.setDollarPrefix(binding.layoutNetIncome, requireContext())
        CustomMaterialFields.setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())

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
        val addressFragment = IncomeAddress()
        val bundle = Bundle()
        bundle.putString("address", "Employer Address")
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_prev_employment_address, addressFragment.arguments)
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