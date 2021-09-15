package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.AppHeaderWithBackNavBinding
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeCurrentEmploymentBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat
import java.util.*



/**
 * Created by Anita Kiran on 9/13/2021.
 */
class IncomeCurrentEmployment : Fragment() , View.OnClickListener {

    private lateinit var binding: IncomeCurrentEmploymentBinding
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
            binding = IncomeCurrentEmploymentBinding.inflate(inflater, container, false)
            toolbar = binding.headerIncome
            savedViewInstance = binding.root


            // set Header title
            toolbar.toolbarTitle.setText(getString(R.string.current_employment))

            initViews()
            savedViewInstance

        }
    }

    private fun initViews() {
        binding.edStartDate.setOnClickListener(this)
        binding.edStartDate.showSoftInputOnFocus = false
        binding.edStartDate.setOnClickListener { openCalendar() }
        binding.edStartDate.setOnFocusChangeListener { _, _ -> openCalendar() }

        binding.rbQues1Yes.setOnClickListener(this)
        binding.rbQues1No.setOnClickListener(this)
        binding.rbQues2Yes.setOnClickListener(this)
        binding.rbQues2No.setOnClickListener(this)
        binding.paytypeHourly.setOnClickListener(this)
        binding.paytypeSalary.setOnClickListener(this)
        binding.cbBonus.setOnClickListener(this)
        binding.cbOvertime.setOnClickListener(this)
        binding.cbCommission.setOnClickListener(this)
        binding.layoutAddress.setOnClickListener(this)
        toolbar.btnClose.setOnClickListener(this)


        setInputFields()

    }

    private fun setInputFields() {

        // set lable focus
        binding.edEmpName.setOnFocusChangeListener(MyCustomFocusListener(binding.edEmpName, binding.layoutEmpName, requireContext()))
        binding.edEmpPhnum.setOnFocusChangeListener(MyCustomFocusListener(binding.edEmpPhnum, binding.layoutEmpPhnum,requireContext()))
        binding.edJobTitle.setOnFocusChangeListener(MyCustomFocusListener(binding.edJobTitle, binding.layoutJobTitle, requireContext()))
        binding.edStartDate.setOnFocusChangeListener(MyCustomFocusListener(binding.edStartDate, binding.layoutStartDate, requireContext()))
        binding.edProfYears.setOnFocusChangeListener(MyCustomFocusListener(binding.edProfYears, binding.layoutYearsProfession, requireContext()))
        binding.edOwnershipPercent.setOnFocusChangeListener(MyCustomFocusListener(binding.edOwnershipPercent, binding.layoutOwnershipPercentage, requireContext()))
        binding.edBaseSalary.setOnFocusChangeListener(MyCustomFocusListener(binding.edBaseSalary, binding.layoutBaseSalary, requireContext()))
        binding.edBonusIncome.setOnFocusChangeListener(MyCustomFocusListener(binding.edBonusIncome, binding.layoutBonusIncome, requireContext()))
        binding.edOvertimeIncome.setOnFocusChangeListener(MyCustomFocusListener(binding.edOvertimeIncome, binding.layoutOvertimeIncome, requireContext()))
        binding.edCommIncome.setOnFocusChangeListener(MyCustomFocusListener(binding.edCommIncome, binding.layoutCommIncome, requireContext()))

        // set input format
        binding.edBaseSalary.addTextChangedListener(NumberTextFormat(binding.edBaseSalary))
        binding.edBonusIncome.addTextChangedListener(NumberTextFormat(binding.edBonusIncome))
        binding.edCommIncome.addTextChangedListener(NumberTextFormat(binding.edCommIncome))
        binding.edOvertimeIncome.addTextChangedListener(NumberTextFormat(binding.edOvertimeIncome))
        binding.edEmpPhnum.addTextChangedListener(PhoneTextFormatter(binding.edEmpPhnum, "(###) ###-####"))


        // set Dollar prifix
        CustomMaterialFields.setPercentagePrefix(binding.layoutOwnershipPercentage, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutBaseSalary, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutCommIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutOvertimeIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutBonusIncome, requireContext())

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.rb_ques1_yes -> quesOneClicked()
            R.id.rb_ques1_no -> quesOneClicked()
            R.id.rb_ques2_yes -> quesTwoClicked()
            R.id.rb_ques2_no -> quesTwoClicked()
            R.id.paytype_hourly -> payTypeClicked()
            R.id.paytype_salary ->payTypeClicked()
            R.id.cb_bonus -> bonusClicked()
            R.id.cb_overtime -> overtimeClicked()
            R.id.cb_commission -> commissionClicked()
            R.id.layout_address -> findNavController().navigate(R.id.action_address)
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainBorrowerName -> HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutCurrEmployment)

        }
    }

    private fun quesOneClicked(){
        if(binding.rbQues1Yes.isChecked) {
            binding.rbQues1Yes.setTypeface(null, Typeface.BOLD)
            binding.rbQues1No.setTypeface(null, Typeface.NORMAL)
        }
        else {
            binding.rbQues1No.setTypeface(null, Typeface.BOLD)
            binding.rbQues1Yes.setTypeface(null, Typeface.NORMAL)
        }
    }

    private fun quesTwoClicked(){
        if(binding.rbQues2Yes.isChecked) {
            binding.rbQues2Yes.setTypeface(null, Typeface.BOLD)
            binding.rbQues2No.setTypeface(null, Typeface.NORMAL)
        }
        else {
            binding.rbQues2No.setTypeface(null, Typeface.BOLD)
            binding.rbQues2Yes.setTypeface(null, Typeface.NORMAL)
        }
    }

    private fun payTypeClicked(){
        if(binding.paytypeSalary.isChecked) {
            binding.paytypeSalary.setTypeface(null, Typeface.BOLD)
            binding.paytypeHourly.setTypeface(null, Typeface.NORMAL)
        }
        else {
            binding.paytypeSalary.setTypeface(null, Typeface.BOLD)
            binding.paytypeHourly.setTypeface(null, Typeface.NORMAL)
        }
    }

    private fun bonusClicked(){
        if(binding.cbBonus.isChecked) {
            binding.cbBonus.setTypeface(null, Typeface.BOLD)
            binding.layoutBonusIncome.visibility = View.VISIBLE

        }
        else {
            binding.cbBonus.setTypeface(null, Typeface.NORMAL)
            binding.layoutBonusIncome.visibility = View.GONE
        }
    }

    private fun overtimeClicked(){
        if(binding.cbOvertime.isChecked) {
            binding.cbOvertime.setTypeface(null, Typeface.BOLD)
            binding.layoutOvertimeIncome.visibility = View.VISIBLE

        }
        else {
            binding.cbOvertime.setTypeface(null, Typeface.NORMAL)
            binding.layoutOvertimeIncome.visibility = View.GONE
        }
    }

    private fun commissionClicked(){
        if(binding.cbCommission.isChecked) {
            binding.cbCommission.setTypeface(null, Typeface.BOLD)
            binding.layoutCommIncome.visibility = View.VISIBLE
        }
        else {
            binding.cbCommission.setTypeface(null, Typeface.NORMAL)
            binding.layoutCommIncome.visibility = View.GONE

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
            { view, year, monthOfYear, dayOfMonth -> binding.edStartDate.setText("" + newMonth + "-" + dayOfMonth + "-" + year) },
            year,
            month,
            day
        )
        dpd.show()
    }
}