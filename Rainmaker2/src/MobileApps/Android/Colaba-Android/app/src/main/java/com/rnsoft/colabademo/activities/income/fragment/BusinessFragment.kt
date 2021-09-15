package com.rnsoft.colabademo

import android.app.DatePickerDialog
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
import com.rnsoft.colabademo.databinding.IncomeBusinessLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat
import java.util.*

/**
 * Created by Anita Kiran on 9/15/2021.
 */
class BusinessFragment : Fragment(), View.OnClickListener {

    private lateinit var binding: IncomeBusinessLayoutBinding
    private lateinit var toolbarBinding: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null
    private val businessTypeArray = listOf("Business")


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

            initViews()
            savedViewInstance

        }
    }


    private fun initViews() {
        binding.edBstartDate.setOnClickListener(this)
        binding.edBstartDate.showSoftInputOnFocus = false
        binding.edBstartDate.setOnClickListener { openCalendar() }
        binding.edBstartDate.setOnFocusChangeListener { _, _ -> openCalendar() }

        binding.layoutAddress.setOnClickListener(this)
        toolbarBinding.btnClose.setOnClickListener(this)
        binding.mainLayoutBusiness.setOnClickListener(this)


        setInputFields()
        setBusinessType()

    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.layout_address -> findNavController().navigate(R.id.action_address)
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_business -> HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutBusiness)

        }
    }

    private fun setInputFields() {

        // set lable focus
        binding.edBusinessName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBusinessName, binding.layoutBusinessName, requireContext()))
        binding.edBusPhnum.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edBusPhnum, binding.layoutBusPhnum,requireContext()))
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

    }

    private fun setBusinessType(){
        val adapter =
            ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, businessTypeArray)
        binding.tvBusinessType.setAdapter(adapter)
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




    private fun openCalendar() {
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val newMonth = month + 1

        val dpd = DatePickerDialog(
            requireActivity(),
            { view, year, monthOfYear, dayOfMonth -> binding.edBstartDate.setText("" + newMonth + "-" + dayOfMonth + "-" + year) },
            year,
            month,
            day
        )
        dpd.show()
    }
}