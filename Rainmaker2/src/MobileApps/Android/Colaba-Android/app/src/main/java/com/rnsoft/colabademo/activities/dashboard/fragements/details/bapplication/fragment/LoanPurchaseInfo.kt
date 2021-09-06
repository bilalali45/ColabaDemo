package com.rnsoft.colabademo.activities.dashboard.fragements.details.bapplication.fragment

import android.R
import android.app.DatePickerDialog
import android.content.res.ColorStateList
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.DatePicker
import androidx.core.content.ContextCompat
import androidx.core.widget.doAfterTextChanged
import androidx.core.widget.doOnTextChanged
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.MyCustomFocusListener
import com.rnsoft.colabademo.activities.borroweraddresses.info.model.Dependent
import com.rnsoft.colabademo.databinding.LoanPurchaseInfoBinding
import com.rnsoft.colabademo.databinding.PrimaryBorrowerInfoLayoutBinding
import com.rnsoft.colabademo.utils.CustomLableColor
import com.rnsoft.colabademo.utils.MaterialTextviewFocusListener
import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import kotlinx.android.synthetic.main.dependent_input_field.view.*
import java.util.*
import kotlin.collections.ArrayList

/**
 * Created by Anita Kiran on 9/3/2021.
 */
class LoanPurchaseInfo : Fragment(), DatePickerDialog.OnDateSetListener {

    private lateinit var binding: LoanPurchaseInfoBinding
    private val loanStageArray = listOf("Pre-Approval")


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = LoanPurchaseInfoBinding.inflate(inflater, container, false)

        setLoanStageSpinner()
        setFocusAndClicks()

        return binding.root

    }


    private fun setFocusAndClicks() {

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
                CustomLableColor.setColor(
                    binding.layoutPurchasePrice,
                    com.rnsoft.colabademo.R.color.grey_color_two,
                    requireContext()
                )
            } else {
                if (binding.edPurchasePrice.text?.length == 0) {
                    CustomLableColor.setColor(
                        binding.layoutPurchasePrice,
                        com.rnsoft.colabademo.R.color.grey_color_three,
                        requireContext()
                    )
                } else {
                    CustomLableColor.setColor(
                        binding.layoutPurchasePrice,
                        com.rnsoft.colabademo.R.color.grey_color_two,
                        requireContext()
                    )
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

        binding.edDownPayment.getText()
            ?.insert(binding.edDownPayment.getSelectionStart(), "fizzbuzz")

        holder.itemView.ed_age.doAfterTextChanged {
            val age = Integer.parseInt(holder.itemView.ed_age.text.toString())
            if(age >0 ){
                items.set(position, Dependent(items.get(position).dependent, age))
            }
        }

    }









    }

    private fun calculatePercentage(purchasePrice : String ){
        purchasePrice.let {
            if (purchasePrice.length > 0) {
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


}
