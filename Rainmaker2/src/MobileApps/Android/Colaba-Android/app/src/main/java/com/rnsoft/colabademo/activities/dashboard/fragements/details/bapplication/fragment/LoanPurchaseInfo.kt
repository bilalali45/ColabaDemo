package com.rnsoft.colabademo.activities.dashboard.fragements.details.bapplication.fragment

import android.R
import android.app.DatePickerDialog
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.DatePicker
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.core.widget.addTextChangedListener
import androidx.core.widget.doAfterTextChanged
import androidx.core.widget.doOnTextChanged
import androidx.fragment.app.Fragment
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.MyCustomFocusListener
import com.rnsoft.colabademo.PhoneTextFormatter
import com.rnsoft.colabademo.databinding.AppToolbarHeadingBinding
import com.rnsoft.colabademo.databinding.LoanPurchaseInfoBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import com.rnsoft.colabademo.utils.NumberTextFormat


/**
 * Created by Anita Kiran on 9/3/2021.
 */
class LoanPurchaseInfo : Fragment(), DatePickerDialog.OnDateSetListener {

    private lateinit var binding: LoanPurchaseInfoBinding
    private lateinit var bindingToolbar : AppToolbarHeadingBinding

    private val loanStageArray = listOf("Pre-Approval")


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = LoanPurchaseInfoBinding.inflate(inflater, container, false)
        bindingToolbar = binding.headerLoanPurchase

        setLoanStageSpinner()
        initViews()
        setNumberFormats()


        binding.edPurchasePrice.addTextChangedListener(object : TextWatcher {

            override fun afterTextChanged(s: Editable) {}

            override fun beforeTextChanged(s: CharSequence, start: Int,
                                           count: Int, after: Int) {
            }

            override fun onTextChanged(s: CharSequence, start: Int,
                                       before: Int, count: Int) {






                
            }
        })






        return binding.root

    }


    private fun initViews() {

        // set Header title
        bindingToolbar.headerTitle.setText(getString(com.rnsoft.colabademo.R.string.loan_info_purchase))

        // set field prefixes
        CustomMaterialFields.setDollarPrefix(binding.layoutDownPayment,requireContext())
        CustomMaterialFields.setPercentagePrefix(binding.layoutPercent,requireContext())

        binding.edClosingDate.setOnClickListener {
            createCalendarDialog()
        }

        binding.edClosingDate.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edClosingDate,
                binding.layoutClosingDate,
                requireContext()
            )
        )

        binding.edPurchasePrice.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutPurchasePrice, com.rnsoft.colabademo.R.color.grey_color_two, requireContext())
            } else {
                if (binding.edPurchasePrice.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutPurchasePrice, com.rnsoft.colabademo.R.color.grey_color_three, requireContext())
                } else {
                    CustomMaterialFields.setColor(binding.layoutPurchasePrice, com.rnsoft.colabademo.R.color.grey_color_two, requireContext())
                    val value = binding.edPurchasePrice.text.toString()
                    value.let {
                        calculatePercentage(value)
                    }
                }
            }
        }

        binding.edDownPayment.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edDownPayment,
                binding.layoutDownPayment,
                requireContext()
            )
        )
        binding.edPercent.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edPercent,
                binding.layoutPercent,
                requireContext()
            )
        )
        binding.edLoanAmount.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edLoanAmount,
                binding.layoutLoanAmount,
                requireContext()
            )
        )

        /*bindingToolbar.backButton.setOnClickListener {
            requireActivity().finish()
        } */


    }

    private fun calculatePercentage(value: String) {
        value.let {
            if (value.length > 0) {
                var purchasePrice = value.replace(",", "");
                val amount: Long = purchasePrice.toLong()
                val result = (amount * 20) / 100
                binding.edDownPayment.setText(result.toString())
                binding.edPercent.setText("20")

                if (amount < 50000 || amount > 100000000){
                    CustomMaterialFields.setError(binding.layoutPurchasePrice,getString(com.rnsoft.colabademo.R.string.invalid_purchase_price),requireContext())
                } else {
                    CustomMaterialFields.clearError(binding.layoutPurchasePrice, requireContext())
                }
            }
        }
    }


    private fun setLoanStageSpinner() {
        val adapter = ArrayAdapter(requireContext(), R.layout.simple_list_item_1, loanStageArray)
        binding.tvLoanStage.setAdapter(adapter)
        binding.tvLoanStage.setOnFocusChangeListener { _, _ ->
            binding.tvLoanStage.showDropDown()
        }
        binding.tvLoanStage.setOnClickListener {
            binding.tvLoanStage.showDropDown()
        }
        binding.tvLoanStage.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutLoanStage.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(),
                        com.rnsoft.colabademo.R.color.grey_color_two
                    )
                )
                if (position == loanStageArray.size - 1)
                    binding.layoutLoanStage.visibility = View.VISIBLE
                else
                    binding.layoutLoanStage.visibility = View.GONE
            }
        }
    }

    private fun createCalendarDialog() {
        val pd = MonthYearPickerDialog()
        pd.setListener(this)
        pd.show(requireActivity().supportFragmentManager, "MonthYearPickerDialog")
    }

    override fun onDateSet(p0: DatePicker?, p1: Int, p2: Int, p3: Int) {
        var stringMonth = p2.toString()
        if (p2 < 10)
            stringMonth = "0$p2"

        val sampleDate = "$stringMonth / $p1"
        binding.edClosingDate.setText(sampleDate)
    }

    private fun onBackClicked(view:View){
        requireActivity().finish()
    }

    private fun setNumberFormats(){
        //binding.edPurchasePrice.addTextChangedListener(NumberTextFormat(binding.edPurchasePrice))
        binding.edLoanAmount.addTextChangedListener(NumberTextFormat(binding.edLoanAmount))
        binding.edDownPayment.addTextChangedListener(NumberTextFormat(binding.edDownPayment))
    }

}
