package com.rnsoft.colabademo.activities.borrowerloan.fragment

import android.R
import android.app.DatePickerDialog
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.DatePicker
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.MyCustomFocusListener
import com.rnsoft.colabademo.databinding.AppToolbarHeadingBinding
import com.rnsoft.colabademo.databinding.LoanPurchaseInfoBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import com.rnsoft.colabademo.utils.NumberTextFormat
import java.text.DecimalFormat


/**
 * Created by Anita Kiran on 9/3/2021.
 */
class LoanPurchaseInfo : Fragment(), DatePickerDialog.OnDateSetListener {

    private lateinit var binding: LoanPurchaseInfoBinding
    private lateinit var bindingToolbar : AppToolbarHeadingBinding
    var isStart : Boolean = true
    var isCalculatePercentage= false
    var isCalculateDownPayment = false

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
        binding.edDownPayment.isEnabled = false
        binding.edPercent.isEnabled = false


        binding.edPurchasePrice.addTextChangedListener(object : TextWatcher {
            override fun afterTextChanged(s: Editable) {
                val value = binding.edPurchasePrice.text
                value.let {
                    binding.edDownPayment.isEnabled = true
                    binding.edPercent.isEnabled = true
                }
                if(value?.length == 0) {
                    binding.edDownPayment.isEnabled = false
                    binding.edPercent.isEnabled = false
                    binding.edPercent.setText("0")
                    binding.edDownPayment.setText("0")
                }
            }
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {
                try {
                    isStart = true
                    var input = s.toString();
                    if(!input.isEmpty()) {
                        input = input.replace(",", "")
                        val format =  DecimalFormat("#,###,###")
                        val newPrice = format.format(input.toLong())
                        binding.edPurchasePrice.removeTextChangedListener(this) //To Prevent from Infinite Loop
                        binding.edPurchasePrice.setText(newPrice)
                        binding.edPurchasePrice.setSelection(newPrice.length)
                        binding.edPurchasePrice.addTextChangedListener(this)
                        calculateInitialDownPayment(newPrice)
                    }
                } catch (nfe: NumberFormatException) {
                    nfe.printStackTrace()
                } catch (e: Exception) {
                    e.printStackTrace()
                }
            }
        })

        binding.edDownPayment.addTextChangedListener(object : TextWatcher {
            override fun afterTextChanged(s: Editable) {
                try {
                    val purchasePrice = Integer.parseInt(binding.edPurchasePrice.text.toString())
                    if (purchasePrice > 0 ) {
                        if (!isStart && isCalculateDownPayment) {
                            val downPayment = binding.edDownPayment.text.toString()
                            downPayment.let {
                                val result : Float = (downPayment.toFloat() / purchasePrice.toFloat())*100
                                val convertResult : Int =  Math.round(result)
                                //Log.e(""+ purchasePrice, " Downpayment " + downPayment  +" Result " + result)
                                //Log.e("ConvertResult", ""+ convertResult)
                                binding.edPercent.setText(convertResult.toString())
                            }
                        }
                    } else {
                    }
                } catch (nfe: NumberFormatException) {
                    nfe.printStackTrace()
                } catch (e: Exception) {
                    e.printStackTrace()
                }
            }
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {
            }
        })


        binding.edPercent.addTextChangedListener(object : TextWatcher {
            override fun afterTextChanged(s: Editable) {
                //val edValue = binding.edPercent.text.toString().length
                //if(edValue==0){
                // binding.edDownPayment.setText("0")
                //}
                try {
                    val purchasePrice = binding.edPurchasePrice.text.toString().length
                    if (purchasePrice > 0) {
                        if (!isStart && isCalculatePercentage) {
                            val edValue = binding.edPercent.text.toString()
                            val purchaseValue = binding.edPurchasePrice.text.toString()
                            edValue.let {
                                val result = (Integer.parseInt(purchaseValue) * Integer.parseInt(edValue)) / 100
                                binding.edDownPayment.setText(result.toString())
                            }
                        }
                    }
                } catch (nfe: NumberFormatException) {
                    nfe.printStackTrace()
                } catch (e: Exception) {
                    e.printStackTrace()
                }

            }
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
        })

        binding.btnSaveChange.setOnClickListener {
            checkValidations()
        }

        return binding.root
    }

    private fun checkValidations(){

        val loanStage: String = binding.tvLoanStage.text.toString()
        val purchasePrice: String = binding.edPurchasePrice.text.toString()
        val loanAmount: String = binding.edLoanAmount.text.toString()
        val downPayment: String = binding.edDownPayment.text.toString()
        val percentage: String = binding.edPercent.text.toString()
        val closingDate: String = binding.edClosingDate.text.toString()

        if (loanStage.isEmpty() || loanStage.length == 0) {
            setError(binding.layoutLoanStage, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        if (purchasePrice.isEmpty() || purchasePrice.length == 0) {
            setError(binding.layoutPurchasePrice, getString(com.rnsoft.colabademo.R.string.invalid_purchase_price))
        }
        if (loanAmount.isEmpty() || loanAmount.length == 0) {
            setError(binding.layoutLoanAmount, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        if (closingDate.isEmpty() || closingDate.length == 0) {
            setError(binding.layoutClosingDate, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        if (downPayment.isEmpty() || downPayment.length == 0) {
            setError(binding.layoutDownPayment, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        if (percentage.isEmpty() || percentage.length == 0) {
            setError(binding.layoutPercent, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        // clear error
        if (loanStage.isNotEmpty() || loanStage.length > 0) {
            clearError(binding.layoutLoanStage)
        }
        if (purchasePrice.isNotEmpty() || purchasePrice.length > 0) {
            clearError(binding.layoutPurchasePrice)
            clearError(binding.layoutDownPayment)
            clearError(binding.layoutPercent)
        }
        if (loanAmount.isNotEmpty() || loanAmount.length > 0) {
            clearError(binding.layoutLoanAmount)
        }
        if (downPayment.isNotEmpty() || downPayment.length > 0) {
            clearError(binding.layoutDownPayment)
        }
        if (percentage.isNotEmpty() || percentage.length > 0) {
            clearError(binding.layoutPercent)
        }
        if (closingDate.isNotEmpty() || closingDate.length > 0) {
            clearError(binding.layoutClosingDate)
        }

    }


    fun setError(textInputlayout: TextInputLayout, errorMsg: String) {
        textInputlayout.helperText = errorMsg
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(requireContext(), com.rnsoft.colabademo.R.color.primary_info_stroke_error_color))
    }

    fun clearError(textInputlayout: TextInputLayout) {
        textInputlayout.helperText = ""
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(
                requireContext(),
                com.rnsoft.colabademo.R.color.primary_info_line_color
            )
        )
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
                isStart = false
                if (binding.edPurchasePrice.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutPurchasePrice, com.rnsoft.colabademo.R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutPurchasePrice)
                    clearError(binding.layoutDownPayment)
                    clearError(binding.layoutPercent)
                    CustomMaterialFields.setColor(binding.layoutPurchasePrice, com.rnsoft.colabademo.R.color.grey_color_two, requireContext())

                    val value = binding.edPurchasePrice.text.toString()
                    value.let {
                        var purchasePrice = value.replace(",", "");
                        if (purchasePrice.toInt() < 50000 || purchasePrice.toInt() > 100000000){
                            CustomMaterialFields.setError(binding.layoutPurchasePrice,getString(com.rnsoft.colabademo.R.string.invalid_purchase_price),requireContext())
                        } else {
                            CustomMaterialFields.clearError(binding.layoutPurchasePrice, requireContext())
                        }
                    }
                }
            }
        }

        binding.edDownPayment.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                isCalculateDownPayment = true
                isCalculatePercentage = false
                CustomMaterialFields.setColor(binding.layoutDownPayment, com.rnsoft.colabademo.R.color.grey_color_two, requireContext())
            } else {
                if (binding.edDownPayment.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutDownPayment, com.rnsoft.colabademo.R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutDownPayment)
                    CustomMaterialFields.setColor(binding.layoutDownPayment, com.rnsoft.colabademo.R.color.grey_color_two, requireContext())
                }
            }
        }

        binding.edPercent.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                isCalculateDownPayment = false
                isCalculatePercentage = true
            }
        }

        binding.edLoanAmount.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutLoanAmount, com.rnsoft.colabademo.R.color.grey_color_two, requireContext())
            } else {
                if (binding.edLoanAmount.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutLoanAmount, com.rnsoft.colabademo.R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutLoanAmount)
                    CustomMaterialFields.setColor(binding.layoutLoanAmount, com.rnsoft.colabademo.R.color.grey_color_two, requireContext())
                }
            }
        }

    }

    private fun calculateInitialDownPayment(value: String) {
        value.let {
            if (value.length > 0) {
                var purchasePrice = value.replace(",", "");
                val amount: Long = purchasePrice.toLong()
                val result = (amount * 20) / 100
                binding.edDownPayment.setText(result.toString())
                binding.edPercent.setText("20")

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
                    ContextCompat.getColor(requireContext(), com.rnsoft.colabademo.R.color.grey_color_two))

                if(binding.tvLoanStage.text.isNotEmpty() && binding.tvLoanStage.text.isNotBlank()) {
                    clearError(binding.layoutLoanStage)
                }
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
        clearError(binding.layoutClosingDate)
    }

    private fun onBackClicked(view:View){
        requireActivity().finish()
    }

    private fun setNumberFormats(){
        //binding.edPurchasePrice.addTextChangedListener(NumberTextFormat(binding.edPurchasePrice))
        //binding.edLoanAmount.addTextChangedListener(NumberTextFormat(binding.edLoanAmount))
        //binding.edDownPayment.addTextChangedListener(NumberTextFormat(binding.edDownPayment))
    }


}
