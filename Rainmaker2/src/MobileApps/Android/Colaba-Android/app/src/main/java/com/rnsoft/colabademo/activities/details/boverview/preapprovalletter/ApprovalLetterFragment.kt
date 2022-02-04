package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.os.Bundle
import android.text.format.DateFormat
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.BaseFragment
import com.rnsoft.colabademo.DetailViewModel
import com.rnsoft.colabademo.HideSoftkeyboard
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.AppHeaderWithBackNavBinding
import com.rnsoft.colabademo.databinding.ConditionalApprovalLetterBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import java.text.SimpleDateFormat
import java.util.*

/**
 * Created by Anita Kiran on 1/31/2022.
 */
class ApprovalLetterFragment : BaseFragment(){
    private var savedViewInstance: View? = null
    private lateinit var binding: ConditionalApprovalLetterBinding
    private lateinit var toolbar: AppHeaderWithBackNavBinding
    private val detailViewModel: DetailViewModel by activityViewModels()

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return if (savedViewInstance != null){
            savedViewInstance
        } else {
            binding = ConditionalApprovalLetterBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root
            toolbar = binding.headerApprovalLetter
            super.addListeners(binding.root)


            clicks()
            setInputFields()

            savedViewInstance
        }
    }

    private fun setInputFields() {

        toolbar.headerTitle.text = getString(R.string.send_conditional_letter)


        // set lable focus
        binding.edLetterLoanAmount.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edLetterLoanAmount, binding.layoutLetterLoanAmount, requireContext()))
        binding.edLetterDownPayment.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edLetterDownPayment, binding.layoutLetterDownPayment, requireContext()))
        binding.edInterestRate.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edInterestRate, binding.layoutInterestRate, requireContext()))

        // set prefix
        CustomMaterialFields.setDollarPrefix(binding.layoutLetterLoanAmount,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutLetterDownPayment,requireContext())
        CustomMaterialFields.setPercentagePrefix(binding.layoutInterestRate,requireContext())

        // input formats
        binding.edLetterLoanAmount.addTextChangedListener(NumberTextFormat(binding.edLetterLoanAmount))
        binding.edLetterDownPayment.addTextChangedListener(NumberTextFormat(binding.edLetterDownPayment))

    }

    private fun clicks(){

        // clicks
        toolbar.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.approvalLayout.setOnClickListener{
            HideSoftkeyboard.hide(requireActivity(), binding.approvalLayout)
            super.removeFocusFromAllFields(binding.approvalLayout)
        }

        // calendar
        binding.edLetterExpiryDate.showSoftInputOnFocus = false
        binding.edLetterExpiryDate.setOnClickListener { openCalendar() }

        binding.edLetterExpiryDate.doAfterTextChanged {
            if (binding.edLetterExpiryDate.text?.length == 0) {
                CustomMaterialFields.setColor(binding.layoutExpiryDate,R.color.grey_color_three,requireActivity())
            } else {
                CustomMaterialFields.setColor(binding.layoutExpiryDate,R.color.grey_color_two,requireActivity())
                CustomMaterialFields.clearError(binding.layoutExpiryDate,requireActivity())
            }
        }
    }



    var maxDate:Long = 0
    var minDate:Long = 0

    private fun openCalendar() {
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val datePickerDialog = DatePickerDialog(requireActivity(), R.style.MySpinnerDatePickerStyle, {
                    view, selectedYear, monthOfYear, dayOfMonth ->
                binding.edLetterExpiryDate.setText("" + (monthOfYear+1) + "/" + dayOfMonth + "/" + selectedYear)
                val cal = Calendar.getInstance()
                cal.set(selectedYear, (monthOfYear), dayOfMonth)
                val date = DateFormat.format("dd-MM-yyyy", cal).toString()
                maxDate = convertDateToLong(date)
            }
            , year, month, day
        )
        if(minDate!=0L)
            datePickerDialog.datePicker.maxDate = minDate
        datePickerDialog.show()

    }

    private fun convertDateToLong(date: String): Long {
        val df = SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH)
        return df.parse(date).time
    }

}