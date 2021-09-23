package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.OtherAssetsLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import java.util.ArrayList
import javax.inject.Inject

@AndroidEntryPoint
class OtherAssetsFragment:Fragment() {

    private var _binding: OtherAssetsLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = OtherAssetsLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        return root
    }

    private fun setUpUI(){
        val dataArray: ArrayList<String> = arrayListOf("Trust Account", "Bridge Loan Proceeds", "Individual Development Account (IDA)", "Cash Value of Life Insurance", "Employer Assistance", "Relocation Funds", "Rent Credit", "Lot Equity", "Sweat Equity", "Trade Equity", "Other")
        val stateNamesAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray)
        binding.accountTypeCompleteView.setAdapter(stateNamesAdapter)
        binding.accountTypeCompleteView.setOnFocusChangeListener { _, _ ->
            HideSoftkeyboard.hide(requireContext(),  binding.accountTypeCompleteView)
            binding.accountTypeCompleteView.showDropDown()
        }
        binding.accountTypeCompleteView.setOnClickListener{
            binding.accountTypeCompleteView.showDropDown()
        }

        binding.accountTypeCompleteView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.accountTypeInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two ))
                binding.accountNumberLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.accountTypeInputLayout, requireContext())
                }
                binding.accountNumberLayout.visibility = View.GONE
                binding.financialLayout.visibility = View.GONE
                binding.annualBaseLayout.visibility = View.GONE
                binding.layoutDetail.visibility = View.GONE

                if(position<4) {
                    binding.accountNumberLayout.visibility = View.VISIBLE
                    binding.financialLayout.visibility = View.VISIBLE
                    binding.annualBaseLayout.visibility = View.VISIBLE
                    binding.layoutDetail.visibility = View.GONE
                }
                else
                if(position == 4 || position == 5) {
                    binding.annualBaseLayout.hint = "Cash Value"
                    binding.annualBaseLayout.visibility = View.VISIBLE
                    binding.layoutDetail.visibility = View.GONE
                }
                else
                if(position > 5 && position < dataArray.size-1) {
                    binding.annualBaseLayout.hint = "Market Value of Equity"
                    binding.annualBaseLayout.visibility = View.VISIBLE
                    binding.layoutDetail.visibility = View.GONE
                }
                else{
                    binding.annualBaseLayout.hint = "Cash or Market Value"
                    binding.annualBaseLayout.visibility = View.VISIBLE
                    binding.layoutDetail.visibility = View.VISIBLE
                }
            }
        }

        binding.backButton.setOnClickListener { findNavController().popBackStack() }

        binding.phoneFab.setOnClickListener {
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                clearFocusFromFields()
                findNavController().popBackStack()
            }
        }

        addFocusOutListenerToFields()

        CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())
        binding.annualBaseEditText.addTextChangedListener(NumberTextFormat(binding.annualBaseEditText))

        setUpEndIcon()
    }

    private fun setUpEndIcon(){
        binding.accountNumberLayout.setEndIconOnClickListener(View.OnClickListener {
            if (binding.accountNumberEdittext.getTransformationMethod()
                    .equals(PasswordTransformationMethod.getInstance())
            ) { //  hide password
                binding.accountNumberEdittext.setTransformationMethod(
                    HideReturnsTransformationMethod.getInstance())
                binding.accountNumberLayout.setEndIconDrawable(R.drawable.ic_eye_hide)
            } else {
                binding.accountNumberEdittext.setTransformationMethod(PasswordTransformationMethod.getInstance())
                binding.accountNumberLayout.setEndIconDrawable(R.drawable.ic_eye_icon_svg)
            }
        })
    }


    private fun clearFocusFromFields(){
        binding.accountNumberLayout.clearFocus()
        binding.accountTypeInputLayout.clearFocus()
        binding.annualBaseLayout.clearFocus()
        binding.financialLayout.clearFocus()
    }

    private fun checkEmptyFields():Boolean{
        var bool =  true
        if(binding.accountNumberEdittext.text?.isEmpty() == true || binding.accountNumberEdittext.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.accountNumberLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.accountNumberLayout,  requireContext())

        if(binding.accountTypeCompleteView.text?.isEmpty() == true || binding.accountTypeCompleteView.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.accountTypeInputLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.accountTypeInputLayout,  requireContext())

        if(binding.annualBaseEditText.text?.isEmpty() == true || binding.annualBaseEditText.text?.isBlank() == true) {

            CustomMaterialFields.setError(binding.annualBaseLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())

        if(binding.financialEditText.text?.isEmpty() == true || binding.financialEditText.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.financialLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.financialLayout,  requireContext())
        return bool
    }


    private  fun addFocusOutListenerToFields(){
        binding.accountNumberEdittext.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.accountNumberEdittext , binding.accountNumberLayout , requireContext()))
        //binding.accountTypeCompleteView.setOnFocusChangeListener(CustomFocusListenerForAutoCompleteTextView( binding.accountTypeCompleteView , binding.accountTypeInputLayout , requireContext()))
        binding.annualBaseEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.annualBaseEditText , binding.annualBaseLayout , requireContext()))
        binding.financialEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.financialEditText , binding.financialLayout , requireContext()))
    }
}