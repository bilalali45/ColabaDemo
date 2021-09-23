package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.RadioGroup
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.GiftsAssetLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject

@AndroidEntryPoint
class GiftsAssetsFragment:Fragment() {

    private var _binding: GiftsAssetLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = GiftsAssetLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        return root
    }


    private fun setUpUI(){
        val dataArray: ArrayList<String> = arrayListOf("Relative", "Unmarried Partner", "Federal Agency", "State Agency", "Local Agency", "Community Non Profit", "Employer", "Religious Non Profit", "Lender")
        val giftAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray)
        binding.giftSourceAutoCompeleteView.setAdapter(giftAdapter)
        binding.giftSourceAutoCompeleteView.setOnFocusChangeListener { _, _ ->
            HideSoftkeyboard.hide(requireContext(),  binding.giftSourceAutoCompeleteView)
            binding.giftSourceAutoCompeleteView.showDropDown()
        }
        binding.giftSourceAutoCompeleteView.setOnClickListener{
            HideSoftkeyboard.hide(requireContext(),  binding.giftSourceAutoCompeleteView)
            binding.giftSourceAutoCompeleteView.showDropDown()
        }

        binding.giftSourceAutoCompeleteView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.giftSourceInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two ))
                binding.giftSourceInputLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.giftSourceInputLayout, requireContext())
                }

                binding.giftTypeConstraintlayout.visibility = View.VISIBLE

                removeErrorFromFields()
                clearFocusFromFields()

                if(position <=1) {
                    binding.giftOfEquity.text = "Grant"
                }
                else{
                    binding.giftOfEquity.text = "Gift Of Equity"
                }
            }
        }
        //set prefix and format
        CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())
        binding.annualBaseEditText.addTextChangedListener(NumberTextFormat(binding.annualBaseEditText))



        binding.radioGroup.setOnCheckedChangeListener(RadioGroup.OnCheckedChangeListener { _, checkedId ->
            when (checkedId) {
                R.id.cash_gift -> {
                    HideSoftkeyboard.hide(requireContext(),binding.radioGroup)
                    clearFocusFromFields()
                    binding.layoutTransferDate.visibility = View.GONE
                    binding.giftDepositGroup.setOnCheckedChangeListener (null);
                    binding.giftDepositGroup.clearCheck()
                    binding.giftDepositGroup.setOnCheckedChangeListener(onGiftDateCheckListener)
                    binding.giftTransferConstraintlayout.visibility = View.VISIBLE
                    binding.annualBaseLayout.hint = "Cash Value"
                }
                R.id.gift_of_equity -> {
                    HideSoftkeyboard.hide(requireContext(),binding.radioGroup)
                    clearFocusFromFields()
                    binding.layoutTransferDate.visibility = View.GONE
                    binding.giftDepositGroup.setOnCheckedChangeListener (null);
                    binding.giftDepositGroup.clearCheck()
                    binding.giftDepositGroup.setOnCheckedChangeListener(onGiftDateCheckListener)
                    binding.giftTransferConstraintlayout.visibility = View.GONE
                    binding.annualBaseLayout.hint = "Market Value"
                }
                else -> {
                }
            }
        })

        binding.dateOfTransferEditText.showSoftInputOnFocus = false
        binding.dateOfTransferEditText.setOnClickListener { openCalendar() }
        binding.dateOfTransferEditText.setOnFocusChangeListener{ _ , _ ->  openCalendar() }


        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.phoneFab.setOnClickListener {
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                clearFocusFromFields()
                findNavController().popBackStack()
            }
        }

        addFocusOutListenerToFields()


    }

    private val onGiftDateCheckListener =
        RadioGroup.OnCheckedChangeListener { p0, checkedId ->
            when (checkedId) {
                R.id.yes_deposited -> {
                    HideSoftkeyboard.hide(requireContext(),binding.radioGroup)
                    clearFocusFromFields()
                    binding.layoutTransferDate.visibility = View.VISIBLE
                    binding.layoutTransferDate.hint = resources.getString(R.string.date_of_transfer)
                }
                R.id.no_deposited -> {
                    HideSoftkeyboard.hide(requireContext(),binding.radioGroup)
                    clearFocusFromFields()
                    binding.layoutTransferDate.visibility = View.VISIBLE
                    binding.layoutTransferDate.hint = resources.getString(R.string.expected_date_of_transfer)
                }
                else -> {
                }
            }
        }

    private fun clearFocusFromFields(){
        binding.giftSourceInputLayout.clearFocus()
        binding.radioGroup.clearFocus()
        binding.annualBaseLayout.clearFocus()
        binding.giftDepositGroup.clearFocus()
        binding.dateOfTransferEditText.clearFocus()
    }

    private fun checkEmptyFields():Boolean{
        var bool =  true

        if(binding.annualBaseEditText.text?.isEmpty() == true || binding.annualBaseEditText.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.annualBaseLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())


        if(binding.giftSourceAutoCompeleteView.text?.isEmpty() == true || binding.giftSourceAutoCompeleteView.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.giftSourceInputLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.giftSourceInputLayout,  requireContext())

        if(binding.dateOfTransferEditText.text?.isEmpty() == true || binding.dateOfTransferEditText.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.layoutTransferDate, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.layoutTransferDate,  requireContext())




        return bool
    }

    private fun removeErrorFromFields(){
        CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())
        CustomMaterialFields.clearError(binding.giftSourceInputLayout,  requireContext())
        CustomMaterialFields.clearError(binding.layoutTransferDate,  requireContext())
        //CustomMaterialFields.clearError(binding.giftDepositGroup,  requireContext())
    }

    private  fun addFocusOutListenerToFields() {
        binding.annualBaseEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.annualBaseEditText,
                binding.annualBaseLayout,
                requireContext()
            )
        )
        binding.dateOfTransferEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.dateOfTransferEditText,
                binding.layoutTransferDate,
                requireContext()
            )
        )


    }

    private fun openCalendar(){
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val newMonth = month + 1
        //  datePicker.findViewById(Resources.getSystem().getIdentifier("day", "id", "android")).setVisibility(View.GONE);

        val dpd = DatePickerDialog(requireActivity(), { view, year, monthOfYear, dayOfMonth -> binding.dateOfTransferEditText.setText("" + newMonth + "-" + dayOfMonth + "-" + year) },
            year,
            month,
            day
        )

        dpd.show()

    }
}