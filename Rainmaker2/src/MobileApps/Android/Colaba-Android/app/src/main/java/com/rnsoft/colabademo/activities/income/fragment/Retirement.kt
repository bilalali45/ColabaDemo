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
import com.rnsoft.colabademo.CustomFocusListenerForEditText
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeRetirementLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat
import java.util.*

/**
 * Created by Anita Kiran on 9/15/2021.
 */
class Retirement : Fragment(), View.OnClickListener {

    private lateinit var binding: IncomeRetirementLayoutBinding
    private lateinit var toolbarBinding: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null
    private val retirementArray = listOf("Pension")


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

        setInputFields()
        setRetirementType()


    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_retirement -> HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutRetirement)

        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.edEmpName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEmpName, binding.layoutEmpName, requireContext()))
        binding.edMonthlyIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edMonthlyIncome, binding.layoutMonthlyIncome, requireContext()))

        // set input format
        binding.edMonthlyIncome.addTextChangedListener(NumberTextFormat(binding.edMonthlyIncome))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutMonthlyIncome, requireContext())

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
                        requireContext(), R.color.grey_color_two
                    )
                )

                if (binding.tvRetirementType.text.isNotEmpty() && binding.tvRetirementType.text.isNotBlank()) {
                    //clearError(binding.layoutLoanStage)
                }
                /*
                if (position == occupancyTypeArray.size - 1)
                    binding.layoutOccupancyType.visibility = View.VISIBLE
                else
                    binding.layoutOccupancyType.visibility = View.GONE */
            }
        }
    }
}