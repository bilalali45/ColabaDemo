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
import com.rnsoft.colabademo.databinding.IncomeRetirementLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/15/2021.
 */
class RetirementIncomeFragment : Fragment(), View.OnClickListener {

    private lateinit var binding: IncomeRetirementLayoutBinding
    private lateinit var toolbarBinding: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null
    private val retirementArray = listOf("Social Security", "Pension","IRA / 401K" , "Other Retirement Source")


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = IncomeRetirementLayoutBinding.inflate(inflater, container, false)
            toolbarBinding = binding.headerIncome
            savedViewInstance = binding.root

            // set Header title
            toolbarBinding.toolbarTitle.setText(getString(R.string.retirement))

            initViews()
            savedViewInstance

        }
    }

    private fun initViews() {
        toolbarBinding.btnClose.setOnClickListener(this)
        binding.mainLayoutRetirement.setOnClickListener(this)
        binding.btnSaveChange.setOnClickListener(this)

        setInputFields()
        setRetirementType()

    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> checkValidations()
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_retirement -> HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutRetirement)

        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.edEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEmpName, binding.layoutEmpName, requireContext()))
        binding.edMonthlyIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edMonthlyIncome, binding.layoutMonthlyIncome, requireContext()))
        binding.edMonthlyWithdrawl.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edMonthlyWithdrawl, binding.layoutMonthlyWithdrawal, requireContext()))
        binding.edDesc.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edDesc, binding.layoutDesc, requireContext()))

        // set input format
        binding.edMonthlyIncome.addTextChangedListener(NumberTextFormat(binding.edMonthlyIncome))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutMonthlyIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutMonthlyWithdrawal, requireContext())
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
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                val type = binding.tvRetirementType.text.toString()
                if (type.equals("Pension")) {
                    binding.layoutEmpName.visibility = View.VISIBLE
                    binding.layoutMonthlyIncome.visibility = View.VISIBLE
                    binding.layoutMonthlyWithdrawal.visibility = View.GONE
                    binding.layoutDesc.visibility = View.GONE
                }

                else if (type.equals("Social Security")) {
                    binding.layoutEmpName.visibility = View.GONE
                    binding.layoutMonthlyIncome.visibility = View.VISIBLE

                    binding.layoutMonthlyWithdrawal.visibility = View.GONE
                    binding.layoutDesc.visibility = View.GONE
                }

                else if (type.equals("IRA / 401K")) {
                    binding.layoutEmpName.visibility = View.GONE
                    binding.layoutMonthlyIncome.visibility = View.GONE
                    binding.layoutDesc.visibility = View.GONE
                    binding.layoutMonthlyWithdrawal.visibility = View.VISIBLE
                }

                else if (type.equals("Other Retirement Source")) {
                    binding.layoutEmpName.visibility = View.GONE
                    binding.layoutMonthlyWithdrawal.visibility = View.GONE
                    binding.layoutDesc.visibility = View.VISIBLE
                    binding.layoutMonthlyIncome.visibility = View.VISIBLE
                }

                if (binding.tvRetirementType.text.isNotEmpty() && binding.tvRetirementType.text.isNotBlank()) {
                    CustomMaterialFields.clearError(binding.layoutRetirement,requireActivity())
                }
            }
        }
    }

    private fun checkValidations(){

        val retirementType: String = binding.tvRetirementType.text.toString()
        val empName: String = binding.edEmpName.text.toString()
        val desc: String = binding.edDesc.text.toString()
        val monthlyIncome: String = binding.edMonthlyIncome.text.toString()
        val mWithdrawal: String = binding.edMonthlyWithdrawl.text.toString()

        if (retirementType.isEmpty() || retirementType.length == 0) {
            CustomMaterialFields.setError(binding.layoutRetirement, getString(R.string.error_field_required),requireActivity())
        }
        if (empName.isEmpty() || empName.length == 0) {
            CustomMaterialFields.setError(binding.layoutEmpName, getString(R.string.error_field_required),requireActivity())
        }
        if (desc.isEmpty() || desc.length == 0) {
            CustomMaterialFields.setError(binding.layoutDesc, getString(R.string.error_field_required),requireActivity())
        }
        if (monthlyIncome.isEmpty() || monthlyIncome.length == 0) {
            CustomMaterialFields.setError(binding.layoutMonthlyIncome, getString(R.string.error_field_required),requireActivity())
        }
        if (mWithdrawal.isEmpty() || mWithdrawal.length == 0) {
            CustomMaterialFields.setError(binding.layoutMonthlyWithdrawal, getString(R.string.error_field_required),requireActivity())
        }
        if (retirementType.isNotEmpty() || retirementType.length > 0) {
            CustomMaterialFields.clearError(binding.layoutRetirement,requireActivity())
        }
        if (empName.isNotEmpty() || empName.length > 0) {
            CustomMaterialFields.clearError(binding.layoutEmpName,requireActivity())
        }
        if (desc.isNotEmpty() || desc.length > 0) {
            CustomMaterialFields.clearError(binding.layoutDesc,requireActivity())
        }
        if (monthlyIncome.isNotEmpty() || monthlyIncome.length > 0) {
            CustomMaterialFields.clearError(binding.layoutMonthlyIncome,requireActivity())
        }
        if (mWithdrawal.isNotEmpty() || mWithdrawal.length > 0) {
            CustomMaterialFields.clearError(binding.layoutMonthlyWithdrawal,requireActivity())
        }

    }
}