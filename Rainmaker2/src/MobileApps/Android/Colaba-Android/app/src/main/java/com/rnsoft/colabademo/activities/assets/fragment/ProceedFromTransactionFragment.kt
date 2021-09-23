package com.rnsoft.colabademo
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

import com.rnsoft.colabademo.databinding.ProceedFromTransLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import dagger.hilt.android.AndroidEntryPoint
import java.util.ArrayList
import javax.inject.Inject


    @AndroidEntryPoint
    class ProceedFromTransactionFragment : BaseFragment() {

        private var _binding: ProceedFromTransLayoutBinding? = null
        private val binding get() = _binding!!

        @Inject
        lateinit var sharedPreferences: SharedPreferences
        override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
        ): View {

            _binding = ProceedFromTransLayoutBinding.inflate(inflater, container, false)
            val root: View = binding.root
            setUpUI()
            super.addListeners(binding.root)
            return root
        }

        private fun setUpUI(){



            val dataArray: ArrayList<String> = arrayListOf("Proceeds From A Loan", "Proceeds From Selling Non-Real Estate Assets", "Proceeds From Selling Real Estate Assets")
            val stateNamesAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray)
            binding.transactionAutoCompleteTextView.setAdapter(stateNamesAdapter)
            binding.transactionAutoCompleteTextView.setOnFocusChangeListener { _, _ ->
                HideSoftkeyboard.hide(requireContext(),  binding.transactionAutoCompleteTextView)
                binding.transactionAutoCompleteTextView.showDropDown()
            }
            binding.transactionAutoCompleteTextView.setOnClickListener{
                binding.transactionAutoCompleteTextView.showDropDown()
            }

            binding.transactionAutoCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
                override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                    binding.transactionTextInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                        ContextCompat.getColor(requireContext(), R.color.grey_color_two ))

                    binding.transactionTextInputLayout.helperText?.let{
                        if(it.isNotEmpty())
                            CustomMaterialFields.clearError(binding.transactionTextInputLayout, requireContext())
                    }

                    removeErrorFromFields()
                    clearFocusFromFields()

                    if(position ==0) {
                        binding.whichAssetInputLayout.visibility = View.GONE
                        binding.layoutDetail.visibility = View.GONE

                        binding.whichAssetsCompleteView.setText("")
                        binding.annualBaseLayout.visibility = View.VISIBLE
                        binding.radioGroup.clearCheck()
                        binding.radioLabelTextView.visibility = View.VISIBLE
                        binding.radioGroup.visibility = View.VISIBLE

                    }
                    else{
                        binding.whichAssetsCompleteView.setText("")
                        binding.whichAssetInputLayout.visibility = View.GONE
                        binding.radioLabelTextView.visibility = View.GONE
                        binding.radioGroup.visibility = View.GONE
                        binding.annualBaseLayout.visibility = View.VISIBLE
                        binding.layoutDetail.visibility = View.VISIBLE
                    }
                }
            }


            binding.radioGroup.setOnCheckedChangeListener(RadioGroup.OnCheckedChangeListener { _, checkedId ->
                when (checkedId) {
                    R.id.radioButton -> {
                        binding.whichAssetInputLayout.visibility = View.VISIBLE
                    }
                    R.id.radioButton2 -> {
                        binding.whichAssetInputLayout.visibility = View.GONE
                    }
                    else -> {
                    }
                }
            })


            val dataArray2: ArrayList<String> = arrayListOf("House", "Automobile", "Financial Account", "Other")
            val dataArrayAdapter2 = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray2)
            binding.whichAssetsCompleteView.setAdapter(dataArrayAdapter2)
            binding.whichAssetsCompleteView.setOnFocusChangeListener { _, _ ->
                HideSoftkeyboard.hide(requireContext(),  binding.whichAssetsCompleteView)
                binding.whichAssetsCompleteView.showDropDown()
            }
            binding.whichAssetsCompleteView.setOnClickListener{
                binding.whichAssetsCompleteView.showDropDown()
            }

            binding.whichAssetsCompleteView.onItemClickListener = object: AdapterView.OnItemClickListener {
                override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                    binding.whichAssetInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                        ContextCompat.getColor(requireContext(), R.color.grey_color_two ))

                    binding.whichAssetInputLayout.helperText?.let{
                        if(it.isNotEmpty())
                            CustomMaterialFields.clearError(binding.whichAssetInputLayout, requireContext())
                    }
                    if(position==dataArray2.size-1) {
                        binding.layoutDetail.visibility = View.VISIBLE
                    }
                    else{
                        binding.layoutDetail.visibility = View.INVISIBLE
                    }
                }
            }

            CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())

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



        private fun clearFocusFromFields(){
            binding.annualBaseLayout.clearFocus()
            binding.whichAssetInputLayout.clearFocus()
            binding.transactionTextInputLayout.clearFocus()
            binding.radioGroup.clearFocus()
            binding.layoutDetail.clearFocus()
        }

        private fun checkEmptyFields():Boolean{
            var bool =  true

            if(binding.annualBaseEditText.text?.isEmpty() == true || binding.annualBaseEditText.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.annualBaseLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())


            if(binding.edDetails.text?.isEmpty() == true || binding.edDetails.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.layoutDetail, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.layoutDetail,  requireContext())

            if(binding.transactionAutoCompleteTextView.text?.isEmpty() == true || binding.transactionAutoCompleteTextView.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.transactionTextInputLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.transactionTextInputLayout,  requireContext())


            if(binding.whichAssetsCompleteView.text?.isEmpty() == true || binding.whichAssetsCompleteView.text?.isBlank() == true) {
                CustomMaterialFields.setError(binding.whichAssetInputLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.whichAssetInputLayout,  requireContext())

            return bool
        }

        private fun removeErrorFromFields(){
            CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())
            CustomMaterialFields.clearError(binding.layoutDetail,  requireContext())
            CustomMaterialFields.clearError(binding.transactionTextInputLayout,  requireContext())
            CustomMaterialFields.clearError(binding.whichAssetInputLayout,  requireContext())
        }

        private  fun addFocusOutListenerToFields() {
            binding.annualBaseEditText.setOnFocusChangeListener(
                CustomFocusListenerForEditText(
                    binding.annualBaseEditText,
                    binding.annualBaseLayout,
                    requireContext()
                )
            )
            binding.edDetails.setOnFocusChangeListener(
                CustomFocusListenerForEditText(
                    binding.edDetails,
                    binding.layoutDetail,
                    requireContext()
                )
            )
        }

}
