package com.rnsoft.colabademo

import android.content.res.ColorStateList
import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.SubjectPropertyPurchaseBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/8/2021.
 */
class SubjectPropertyPurchase : Fragment(), View.OnClickListener {

    lateinit var binding: SubjectPropertyPurchaseBinding
    private val propertyTypeArray = listOf("Single Family Property","Condominium","Townhouse", "Cooperative", "Manufactured Home", "Duplex (2 Unit)", "Triplex (3 Unit)", "Quadplex (4 Unit)")
    private val occupancyTypeArray = listOf("Primary Residence", "Second Home", "Investment Property")
    private var savedViewInstance: View? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = SubjectPropertyPurchaseBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root

            binding.rbSubProperty.isChecked = false
            binding.rbSubPropertyAddress.isChecked = false
            binding.rbSubProperty.setOnClickListener(this)
            binding.rbSubPropertyAddress.setOnClickListener(this)
            binding.rbMixedPropertyNo.setOnClickListener(this)
            binding.rbMixedPropertyYes.setOnClickListener(this)
            binding.layoutDetails.setOnClickListener(this)
            binding.backButton.setOnClickListener(this)
            binding.layoutAddress.setOnClickListener(this)
            binding.rbOccupying.setOnClickListener(this)
            binding.rbNonOccupying.setOnClickListener(this)
            binding.rbMixedPropertyYes.setOnClickListener(this)
            binding.rbMixedPropertyNo.setOnClickListener(this)
            binding.parentLayout.setOnClickListener(this)
            binding.btnSave.setOnClickListener(this)

            setSpinnerData()
            setInputFields()

            savedViewInstance
        }
    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.rb_sub_property -> radioSubPropertyClick()
            R.id.rb_sub_property_address -> setAddressClick()
            R.id.rb_mixed_property_yes -> mixedPropertyDetail()
            R.id.layout_details -> mixedPropertyDetail()
            R.id.layout_address -> setAddressClick()
            R.id.backButton -> requireActivity().finish()
            R.id.btn_save -> requireActivity().finish()
            R.id.parentLayout -> HideSoftkeyboard.hide(requireActivity(),binding.parentLayout)

            R.id.rb_mixed_property_no ->
                if (binding.rbMixedPropertyNo.isChecked) {
                    binding.layoutDetails.visibility = View.GONE
                    binding.rbMixedPropertyNo.setTypeface(null, Typeface.BOLD)
                    binding.rbMixedPropertyYes.setTypeface(null, Typeface.NORMAL)
                }else{
                    binding.rbMixedPropertyNo.setTypeface(null, Typeface.NORMAL)
                }
            R.id.rb_occupying ->
                if (binding.rbOccupying.isChecked) {
                    binding.rbOccupying.setTypeface(null, Typeface.BOLD)
                    binding.rbNonOccupying.setTypeface(null, Typeface.NORMAL)
                }else{
                    binding.rbOccupying.setTypeface(null, Typeface.NORMAL)
                }

            R.id.rb_non_occupying ->
                if (binding.rbNonOccupying.isChecked) {
                    binding.rbNonOccupying.setTypeface(null, Typeface.BOLD)
                    binding.rbOccupying.setTypeface(null, Typeface.NORMAL)
                }else{
                    binding.rbNonOccupying.setTypeface(null, Typeface.NORMAL)
                }
        }
     }

    private fun setInputFields(){

        // set lable focus
        binding.edAppraisedPropertyValue.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edAppraisedPropertyValue, binding.layoutAppraisedProperty, requireContext()))
        binding.edPropertyTaxes.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edPropertyTaxes, binding.layoutPropertyTaxes, requireContext()))
        binding.edHomeownerInsurance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edHomeownerInsurance, binding.layoutHomeownerInsurance, requireContext()))
        binding.edFloodInsurance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edFloodInsurance, binding.layoutFloodInsurance, requireContext()))

        // set input format
        binding.edAppraisedPropertyValue.addTextChangedListener(NumberTextFormat(binding.edAppraisedPropertyValue))
        binding.edPropertyTaxes.addTextChangedListener(NumberTextFormat(binding.edPropertyTaxes))
        binding.edHomeownerInsurance.addTextChangedListener(NumberTextFormat(binding.edHomeownerInsurance))
        binding.edFloodInsurance.addTextChangedListener(NumberTextFormat(binding.edFloodInsurance))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutAppraisedProperty,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutPropertyTaxes,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutHomeownerInsurance,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutFloodInsurance,requireContext())

    }

    private fun radioSubPropertyClick(){
        binding.rbSubProperty.isChecked = true
        binding.rbSubPropertyAddress.isChecked = false
        binding.tvSubPropertyAddress.visibility = View.GONE
        //bold text
        binding.rbSubProperty.setTypeface(null,Typeface.BOLD)
        binding.radioTxtPropertyAdd.setTypeface(null,Typeface.NORMAL)

    }

    private fun setAddressClick(){
        binding.rbSubProperty.isChecked = false
        binding.rbSubPropertyAddress.isChecked = true
        binding.tvSubPropertyAddress.visibility = View.VISIBLE
        //bold text
        binding.radioTxtPropertyAdd.setTypeface(null,Typeface.BOLD)
        binding.rbSubProperty.setTypeface(null,Typeface.NORMAL)

        findNavController().navigate(R.id.action_address)
    }

    private fun mixedPropertyDetail(){
        findNavController().navigate(R.id.action_mixed_property)
        binding.layoutDetails.visibility = View.VISIBLE
        binding.rbMixedPropertyYes.setTypeface(null, Typeface.BOLD)
        binding.rbMixedPropertyNo.setTypeface(null, Typeface.NORMAL)
    }

    private fun setSpinnerData() {
        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,propertyTypeArray )
        binding.tvPropertyType.setAdapter(adapter)
        binding.tvPropertyType.setOnFocusChangeListener { _, _ ->
            binding.tvPropertyType.showDropDown()
        }
        binding.tvPropertyType.setOnClickListener {
            binding.tvPropertyType.showDropDown()
        }
        binding.tvPropertyType.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutPropertyType.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                /*if(binding.tvPropertyType.text.isNotEmpty() && binding.tvPropertyType.text.isNotBlank()) {
                    //clearError(binding.layoutLoanStage)
                } */
            }
        }


        // set occupancy type spinner

        val adapterOccupanycyType =
            ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, occupancyTypeArray)
        binding.tvOccupancyType.setAdapter(adapterOccupanycyType)
        binding.tvOccupancyType.setOnFocusChangeListener { _, _ -> binding.tvOccupancyType.showDropDown()
        }
        binding.tvOccupancyType.setOnClickListener {
            binding.tvOccupancyType.showDropDown()
        }
        binding.tvOccupancyType.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutOccupancyType.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(), R.color.grey_color_two))

                /*if (binding.tvOccupancyType.text.isNotEmpty() && binding.tvOccupancyType.text.isNotBlank()) {
                    //clearError(binding.layoutLoanStage)
                } */
            }
        }
    }






}