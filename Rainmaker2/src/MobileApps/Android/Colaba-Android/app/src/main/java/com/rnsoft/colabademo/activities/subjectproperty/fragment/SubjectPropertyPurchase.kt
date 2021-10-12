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
import androidx.activity.addCallback
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.SubjectPropertyPurchaseBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/8/2021.
 */
class SubjectPropertyPurchase : BaseFragment(), View.OnClickListener {

    private lateinit var binding: SubjectPropertyPurchaseBinding
    //private val propertyTypeArray = listOf("Single Family Property","Condominium","Townhouse", "Cooperative", "Manufactured Home", "Duplex (2 Unit)", "Triplex (3 Unit)", "Quadplex (4 Unit)")
    //private val occupancyTypeArray = listOf("Primary Residence", "Second Home", "Investment Property")
    private var savedViewInstance: View? = null
    private val viewModel : SubjectPropertyViewModel by activityViewModels()
    val token : String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhZGlxQHJhaW5zb2Z0Zm4uY29tIiwiRmlyc3ROYW1lIjoiU2FkaXEiLCJMYXN0TmFtZSI6Ik1hY2tub2ppYSIsIlRlbmFudENvZGUiOiJhaGNsZW5kaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTUNVIiwiZXhwIjoxNjM0MTc0Njg2LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.2E5FSNrooM9Fi7weXMOUj2WaRNEk2NNHfqINYndapBA"
    private var propertyTypeId : Int = 0
    private var occupancyTypeId : Int =0


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
            binding.subpropertyParentLayout.setOnClickListener(this)
            binding.btnSave.setOnClickListener(this)

            setInputFields()
            getPurchaseDetails()
            getCoBorrowerOccupancyStatus()
            super.addListeners(binding.root)

            requireActivity().onBackPressedDispatcher.addCallback {
                requireActivity().finish()
                requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
            }

            savedViewInstance
        }
    }

    private fun getPurchaseDetails(){
        lifecycleScope.launchWhenStarted {
            viewModel.getSubjectPropertyDetails(token, 5)
            viewModel.subjectPropertyDetails.observe(viewLifecycleOwner, { details ->
                if(details != null){
                    // property id
                    details.subPropertyData?.propertyTypeId?.let { id ->
                        propertyTypeId = id
                    }
                    // occupancy id
                    details.subPropertyData?.occupancyTypeId?.let { id ->
                        occupancyTypeId = id
                    }
                    // appraised value
                    details.subPropertyData?.appraisedPropertyValue?.let { value ->
                        binding.edAppraisedPropertyValue.setText(value.toString())
                    }
                    // property tax
                    details.subPropertyData?.propertyTax?.let { value ->
                        binding.edPropertyTax.setText(value.toString())
                    }
                    // home insurance
                    details.subPropertyData?.homeOwnerInsurance?.let { value ->
                        binding.edHomeownerInsurance.setText(value.toString())
                    }
                    // flood insurance
                    details.subPropertyData?.floodInsurance?.let { value ->
                        binding.edFloodInsurance.setText(value.toString())
                    }
                    // mixed use property
                    details.subPropertyData?.isMixedUseProperty?.let { value ->
                        if(value){
                            binding.rbMixedPropertyYes.isChecked = true
                            details.subPropertyData.mixedUsePropertyExplanation?.let { desc ->
                                binding.mixedPropertyExplanation.setText(desc)
                            }
                        }
                        else
                            binding.rbMixedPropertyNo.isChecked = true
                    }

                    getDropDownData()

                }
            })
        }
    }

    private fun getCoBorrowerOccupancyStatus(){
        lifecycleScope.launchWhenStarted {
            viewModel.getCoBorrowerOccupancyStatus(token, 5)
            viewModel.coBorrowerOccupancyStatus.observe(viewLifecycleOwner, {
            })
        }
    }

    private fun getDropDownData(){
        lifecycleScope.launchWhenStarted {
            viewModel.getPropertyTypes(token)
            viewModel.propertyType.observe(viewLifecycleOwner, {
                if(it != null && it.size > 0) {

                    Log.e("properyId2", ""+ propertyTypeId)
                    val itemList:ArrayList<String> = arrayListOf()
                    for(item in it){
                        itemList.add(item.name)
                        if(propertyTypeId > 0 && propertyTypeId == item.id){
                            binding.tvPropertyType.setText(item.name)
                        }
                    }
                    val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,itemList)
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
                        }
                    }
                }
            })
        }

        // occupancy Type spinner

        lifecycleScope.launchWhenStarted {
            viewModel.getOccupancyType("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhZGlxQHJhaW5zb2Z0Zm4uY29tIiwiRmlyc3ROYW1lIjoiU2FkaXEiLCJMYXN0TmFtZSI6Ik1hY2tub2ppYSIsIlRlbmFudENvZGUiOiJhaGNsZW5kaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTUNVIiwiZXhwIjoxNjM0MTc0Njg2LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.2E5FSNrooM9Fi7weXMOUj2WaRNEk2NNHfqINYndapBA")
            viewModel.occupancyType.observe(viewLifecycleOwner, {occupancyList->

                if(occupancyList != null && occupancyList.size > 0) {
                    val itemList: ArrayList<String> = arrayListOf()
                    for (item in occupancyList) {
                        itemList.add(item.name)
                        if(occupancyTypeId > 0 && occupancyTypeId == item.id){
                            binding.tvOccupancyType.setText(item.name)
                        }
                    }

                    val adapterOccupanycyType = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,itemList)
                    binding.tvOccupancyType.setAdapter(adapterOccupanycyType)
                    binding.tvOccupancyType.setOnFocusChangeListener { _, _ ->
                        binding.tvOccupancyType.showDropDown()
                    }
                    binding.tvOccupancyType.setOnClickListener {
                        binding.tvOccupancyType.showDropDown()
                    }

                    binding.tvOccupancyType.onItemClickListener = object :
                        AdapterView.OnItemClickListener {
                        override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                            binding.layoutOccupancyType.defaultHintTextColor =
                                ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                        }
                    }
                }

            })
        }
    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.rb_sub_property -> radioSubPropertyClick()
            R.id.rb_sub_property_address -> setAddressClick()
            R.id.rb_mixed_property_yes -> mixedPropertyDetail()
            R.id.layout_details -> mixedPropertyDetail()
            R.id.layout_address -> setAddressClick()
            R.id.backButton ->  {
                requireActivity().finish()
                requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
            }
            R.id.btn_save -> requireActivity().finish()
            R.id.subproperty_parent_layout -> {
                HideSoftkeyboard.hide(requireActivity(),binding.subpropertyParentLayout)
                super.removeFocusFromAllFields(binding.subpropertyParentLayout)
            }

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
        binding.edPropertyTax.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edPropertyTax, binding.layoutPropertyTaxes, requireContext()))
        binding.edHomeownerInsurance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edHomeownerInsurance, binding.layoutHomeownerInsurance, requireContext()))
        binding.edFloodInsurance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edFloodInsurance, binding.layoutFloodInsurance, requireContext()))

        // set input format
        binding.edAppraisedPropertyValue.addTextChangedListener(NumberTextFormat(binding.edAppraisedPropertyValue))
        binding.edPropertyTax.addTextChangedListener(NumberTextFormat(binding.edPropertyTax))
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

   /* private fun setSpinnerData() {
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

            }
        }


        // set occupancy type spinner

        val adapterOccupanycyType = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, occupancyTypeArray)
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

            }
        }
    } */






}