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
import com.rnsoft.colabademo.databinding.OwnershipInterestInPropertyLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import dagger.hilt.android.AndroidEntryPoint
import java.util.ArrayList
import javax.inject.Inject


@AndroidEntryPoint
class OwnershipInterestInPropertyFragment : BaseFragment() {

    private var _binding: OwnershipInterestInPropertyLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = OwnershipInterestInPropertyLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI(){

        val dataArray: ArrayList<String> = arrayListOf("Primary Residence", "Second Home", "Investment Property")
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
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                binding.transactionTextInputLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.transactionTextInputLayout, requireContext())
                }

                removeErrorFromFields()
                clearFocusFromFields()

            }
        }





        val dataArray2: ArrayList<String> = arrayListOf("By Yourself", "Jointly with your spouse", "Jointly with another person")
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
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                binding.whichAssetInputLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.whichAssetInputLayout, requireContext())
                }
                if(position==dataArray2.size-1) {

                }
                else{

                }
            }
        }



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



    }





    private fun checkEmptyFields():Boolean{
        var bool =  true

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
       // CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())
        CustomMaterialFields.clearError(binding.transactionTextInputLayout,  requireContext())
        CustomMaterialFields.clearError(binding.whichAssetInputLayout,  requireContext())
    }

    private fun clearFocusFromFields(){
        //binding.annualBaseLayout.clearFocus()
        binding.whichAssetInputLayout.clearFocus()
        binding.transactionTextInputLayout.clearFocus()
    }


}
