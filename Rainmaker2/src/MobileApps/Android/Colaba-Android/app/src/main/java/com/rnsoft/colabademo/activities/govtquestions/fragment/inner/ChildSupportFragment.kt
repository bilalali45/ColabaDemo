package com.rnsoft.colabademo

import android.content.SharedPreferences
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
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.ChildSupportLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import dagger.hilt.android.AndroidEntryPoint
import java.util.ArrayList
import javax.inject.Inject

@AndroidEntryPoint
class ChildSupportFragment:Fragment() {

    private var _binding: ChildSupportLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = ChildSupportLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        return root
    }

    private fun setUpUI() {
        val dataArray: ArrayList<String> = arrayListOf("1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "12+")
        val dataAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray)
        binding.paymentRemainingTextView.setAdapter(dataAdapter)
        binding.paymentRemainingTextView.setOnFocusChangeListener { _, _ ->
            HideSoftkeyboard.hide(requireContext(),  binding.paymentRemainingTextView)
            binding.paymentRemainingTextView.showDropDown()
        }
        binding.paymentRemainingTextView.setOnClickListener{
            binding.paymentRemainingTextView.showDropDown()
        }

        binding.paymentRemainingTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.paymentRemainingLayout.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                binding.paymentRemainingLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.paymentRemainingLayout, requireContext())
                }

                removeErrorFromFields()
                clearFocusFromFields()
            }
        }


        binding.alimonyPaymentRemainingTextView.setAdapter(dataAdapter)
        binding.alimonyPaymentRemainingTextView.setOnFocusChangeListener { _, _ ->
            HideSoftkeyboard.hide(requireContext(),  binding.alimonyPaymentRemainingTextView)
            binding.alimonyPaymentRemainingTextView.showDropDown()
        }
        binding.alimonyPaymentRemainingTextView.setOnClickListener{
            binding.alimonyPaymentRemainingTextView.showDropDown()
        }
        binding.alimonyPaymentRemainingTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.alimonyPaymentRemainingLayout.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two))
                binding.alimonyPaymentRemainingLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.alimonyPaymentRemainingLayout, requireContext())
                }
                removeErrorFromFields()
                clearFocusFromFields()
            }
        }

        binding.separateMaintenancePaymentRemainingTextView.setAdapter(dataAdapter)
        binding.separateMaintenancePaymentRemainingTextView.setOnFocusChangeListener { _, _ ->
            HideSoftkeyboard.hide(requireContext(),  binding.separateMaintenancePaymentRemainingTextView)
            binding.separateMaintenancePaymentRemainingTextView.showDropDown()
        }
        binding.separateMaintenancePaymentRemainingTextView.setOnClickListener{
            binding.separateMaintenancePaymentRemainingTextView.showDropDown()
        }

        binding.separateMaintenancePaymentRemainingTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.separateMaintenancePaymentRemainingLayout.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                binding.separateMaintenancePaymentRemainingLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.separateMaintenancePaymentRemainingLayout, requireContext())
                }

                removeErrorFromFields()
                clearFocusFromFields()
            }
        }


        binding.backButton.setOnClickListener{
            findNavController().popBackStack()
        }

        CustomMaterialFields.setDollarPrefix(binding.monthlyPaymentLayout, requireActivity())
        CustomMaterialFields.setDollarPrefix(binding.alimonyMonthlyPaymentLayout, requireActivity())
        CustomMaterialFields.setDollarPrefix(binding.separateMonthlyPaymentLayout, requireActivity())

        binding.childSupportCheckBox.setOnClickListener {
            if(binding.childSupportCheckBox.isChecked)
                binding.childSupportInnerFields.visibility = View.VISIBLE
            else
                binding.childSupportInnerFields.visibility = View.GONE
        }

        binding.alimonyCheckBox.setOnClickListener {
            if(binding.alimonyCheckBox.isChecked)
                binding.alimonyInnerFields.visibility = View.VISIBLE
            else
                binding.alimonyInnerFields.visibility = View.GONE
        }

        binding.separateMaintenanceCheckBox.setOnClickListener {
            if(binding.separateMaintenanceCheckBox.isChecked)
                binding.separateMaintenanceInnerFields.visibility = View.VISIBLE
            else
                binding.separateMaintenanceInnerFields.visibility = View.GONE
        }

        binding.saveBtn.setOnClickListener{
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                clearFocusFromFields()
                findNavController().popBackStack()
            }
        }

        binding.backButton.setOnClickListener{
            findNavController().popBackStack()
        }

        binding.separateMonthlyPaymentEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.separateMonthlyPaymentEditText, binding.separateMaintenancePaymentRemainingLayout, requireContext()))
        binding.separatePaymentReceiptEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.separatePaymentReceiptEditText, binding.separatePaymentReceiptLayout, requireContext()))

        binding.alimonyMonthlyPaymentEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.alimonyMonthlyPaymentEditText, binding.alimonyMonthlyPaymentLayout, requireContext()))
        binding.alimonyPaymentReceiptEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.alimonyPaymentReceiptEditText, binding.alimonyPaymentReceiptLayout, requireContext()))

        binding.monthlyPaymentEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.monthlyPaymentEditText, binding.monthlyPaymentLayout, requireContext()))
        binding.paymentReceiptEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.paymentReceiptEditText, binding.paymentReceiptLayout, requireContext()))
    }

    private fun checkEmptyFields():Boolean{
        var bool =  true
        if(binding.separateMaintenanceCheckBox.isChecked){


            if(binding.separateMonthlyPaymentEditText.text?.isEmpty() == true || binding.separateMonthlyPaymentEditText.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.separateMonthlyPaymentLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.separateMonthlyPaymentLayout,  requireContext())

            if(binding.separateMaintenancePaymentRemainingTextView.text?.isEmpty() == true || binding.separateMaintenancePaymentRemainingTextView.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.separateMaintenancePaymentRemainingLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.separateMaintenancePaymentRemainingLayout,  requireContext())

            if(binding.separatePaymentReceiptEditText.text?.isEmpty() == true || binding.separatePaymentReceiptEditText.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.separatePaymentReceiptLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.separatePaymentReceiptLayout,  requireContext())

        }

        if(binding.alimonyCheckBox.isChecked){

            if(binding.alimonyMonthlyPaymentEditText.text?.isEmpty() == true || binding.alimonyMonthlyPaymentEditText.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.alimonyMonthlyPaymentLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.alimonyMonthlyPaymentLayout,  requireContext())

            if(binding.alimonyPaymentReceiptEditText.text?.isEmpty() == true || binding.alimonyPaymentReceiptEditText.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.alimonyPaymentReceiptLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.alimonyPaymentReceiptLayout,  requireContext())

            if(binding.alimonyPaymentRemainingTextView.text?.isEmpty() == true || binding.alimonyPaymentRemainingTextView.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.alimonyPaymentRemainingLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.alimonyPaymentRemainingLayout,  requireContext())

        }

        if(binding.childSupportCheckBox.isChecked){


            if(binding.paymentRemainingTextView.text?.isEmpty() == true || binding.paymentRemainingTextView.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.paymentRemainingLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.paymentRemainingLayout,  requireContext())

            if(binding.monthlyPaymentEditText.text?.isEmpty() == true || binding.monthlyPaymentEditText.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.monthlyPaymentLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.monthlyPaymentLayout,  requireContext())

            if(binding.paymentReceiptEditText.text?.isEmpty() == true || binding.paymentReceiptEditText.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.paymentReceiptLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.paymentReceiptLayout,  requireContext())



        }

        return bool
    }

    private fun removeErrorFromFields(){
        CustomMaterialFields.clearError(binding.separateMonthlyPaymentLayout,  requireContext())
        CustomMaterialFields.clearError(binding.paymentRemainingLayout,  requireContext())
        CustomMaterialFields.clearError(binding.alimonyPaymentRemainingLayout,  requireContext())
    }

    private fun clearFocusFromFields(){
        binding.separateMonthlyPaymentLayout.clearFocus()
        binding.paymentRemainingLayout.clearFocus()
        binding.alimonyPaymentRemainingLayout.clearFocus()
    }

}