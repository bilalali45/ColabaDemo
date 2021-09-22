package com.rnsoft.colabademo

import android.content.res.ColorStateList
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeOtherLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/15/2021.
 */
class IncomeOtherFragment : Fragment(), View.OnClickListener {

    private lateinit var binding: IncomeOtherLayoutBinding
    private lateinit var toolbarBinding: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null
    private val retirementArray = listOf("Alimony", "Child Support", "Separate Maintenance", "Foster Care", "Annuity", "Capital Gains", "Interest / Dividends", "Notes Receivable",
        "Trust", "Housing Or Parsonage", "Mortgage Credit Certificate", "Mortgage Differential Payments", "Public Assistance", "Unemployment Benefits", "VA Compensation", "Automobile" +
                " Allowance", "Boarder Income", "Royalty Payments", "Disability", "Other Income Source")


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = IncomeOtherLayoutBinding.inflate(inflater, container, false)
            toolbarBinding = binding.headerIncome
            savedViewInstance = binding.root

            // set Header title
            toolbarBinding.toolbarTitle.setText(getString(R.string.income_other))

            initViews()
            savedViewInstance

        }
    }

    private fun initViews() {
        toolbarBinding.btnClose.setOnClickListener(this)
        binding.mainLayoutOther.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)

        setInputFields()
        setRetirementType()

    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> checkValidations()
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_other -> HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutOther)

        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.edMonthlyIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edMonthlyIncome, binding.layoutMonthlyIncome, requireContext()))
        binding.edAnnualIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edAnnualIncome, binding.layoutAnnualIncome, requireContext()))

        // set input format
        binding.edMonthlyIncome.addTextChangedListener(NumberTextFormat(binding.edMonthlyIncome))
        binding.edAnnualIncome.addTextChangedListener(NumberTextFormat(binding.edAnnualIncome))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutMonthlyIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutAnnualIncome, requireContext())
    }

    private fun checkValidations(){

        val incomeType: String = binding.tvRetirementType.text.toString()
        val annualIncome: String = binding.edAnnualIncome.text.toString()
        val monthlyIncome: String = binding.edMonthlyIncome.text.toString()
        val desc: String = binding.edDesc.text.toString()

        if (incomeType.isEmpty() || incomeType.length == 0) {
            CustomMaterialFields.setError(binding.layoutRetirement, getString(R.string.error_field_required),requireActivity())
        }
        if (monthlyIncome.isEmpty() || monthlyIncome.length == 0) {
            CustomMaterialFields.setError(binding.layoutMonthlyIncome, getString(R.string.error_field_required),requireActivity())
        }
        if (annualIncome.isEmpty() || annualIncome.length == 0) {
            CustomMaterialFields.setError(binding.layoutAnnualIncome, getString(R.string.error_field_required),requireActivity())
        }
        if (desc.isEmpty() || desc.length == 0) {
            CustomMaterialFields.setError(binding.layoutDesc, getString(R.string.error_field_required),requireActivity())
        }
        if (incomeType.isNotEmpty() || incomeType.length > 0) {
            CustomMaterialFields.clearError(binding.layoutRetirement,requireActivity())
        }
        if (annualIncome.isNotEmpty() || annualIncome.length > 0) {
            CustomMaterialFields.clearError(binding.layoutAnnualIncome,requireActivity())
        }
        if (monthlyIncome.isNotEmpty() || monthlyIncome.length > 0) {
            CustomMaterialFields.clearError(binding.layoutMonthlyIncome,requireActivity())
        }
        if (desc.isNotEmpty() || desc.length > 0) {
            CustomMaterialFields.clearError(binding.layoutDesc,requireActivity())
        }

    }

    private fun setRetirementType(){
        val adapter =
            ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, retirementArray)
        binding.tvRetirementType.setAdapter(adapter)
        binding.tvRetirementType.setOnFocusChangeListener { _, _ ->
            binding.tvRetirementType.showDropDown()
        }
        binding.tvRetirementType.setOnClickListener {
            binding.tvRetirementType.showDropDown()
        }
        binding.tvRetirementType.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutRetirement.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(), R.color.grey_color_two))

                var item = binding.tvRetirementType.text.toString()
                if (item == "Capital Gains" || item == "Interest / Dividends" || item == "Other Income Source") {
                    binding.layoutAnnualIncome.visibility = View.VISIBLE
                    binding.layoutMonthlyIncome.visibility = View.GONE
                    binding.layoutDesc.visibility = View.GONE
                }
                else if (item == "Annuity") {
                    binding.layoutAnnualIncome.visibility = View.GONE
                    binding.layoutMonthlyIncome.visibility = View.VISIBLE
                    binding.layoutDesc.visibility = View.VISIBLE
                }
                else {
                    binding.layoutAnnualIncome.visibility = View.GONE
                    binding.layoutDesc.visibility = View.GONE
                    binding.layoutMonthlyIncome.visibility = View.VISIBLE
                }

                if (binding.tvRetirementType.text.isNotEmpty() && binding.tvRetirementType.text.isNotBlank()) {
                    CustomMaterialFields.clearError(binding.layoutRetirement,requireActivity())
                }

            }
        }
    }
}