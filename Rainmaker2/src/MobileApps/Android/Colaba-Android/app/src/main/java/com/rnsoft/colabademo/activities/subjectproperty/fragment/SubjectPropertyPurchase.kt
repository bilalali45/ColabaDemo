package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.activity.addCallback
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.SubjectPropertyPurchaseBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint

/**
 * Created by Anita Kiran on 9/8/2021.
 */
@AndroidEntryPoint
class SubjectPropertyPurchase : BaseFragment() {

    private lateinit var binding: SubjectPropertyPurchaseBinding
    private var savedViewInstance: View? = null
    private val viewModel : SubjectPropertyViewModel by activityViewModels()
    val token : String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhZGlxQHJhaW5zb2Z0Zm4uY29tIiwiRmlyc3ROYW1lIjoiU2FkaXEiLCJMYXN0TmFtZSI6Ik1hY2tub2ppYSIsIlRlbmFudENvZGUiOiJhaGNsZW5kaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTUNVIiwiZXhwIjoxNjM0NzUzMjYxLCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.bHZwTohB4toe2JGgKVNeaOoOh8HIaygh8WqmGpTPzO4"
    private var propertyTypeId : Int = 0
    private var occupancyTypeId : Int = 0
    var addressList :  ArrayList<AddressData> = ArrayList()


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
            super.addListeners(binding.root)

            setupUI()
            setInputFields()
            getPurchaseDetails()

            savedViewInstance
        }
    }

    private fun setupUI(){

        // radio subject property TBD
        binding.radioSubPropertyTbd.setOnClickListener {
            binding.radioSubPropertyAddress.isChecked = false
            binding.radioSubPropertyTbd.setTypeface(null,Typeface.BOLD)
            binding.radioTxtPropertyAdd.setTypeface(null,Typeface.NORMAL)
            binding.tvSubPropertyAddress.visibility = View.GONE
        }

        // radio sub property address
        binding.radioSubPropertyAddress.setOnClickListener {
            binding.radioSubPropertyTbd.isChecked = false
            binding.tvSubPropertyAddress.visibility = View.VISIBLE
            binding.radioTxtPropertyAdd.setTypeface(null,Typeface.BOLD)
            binding.radioSubPropertyTbd.setTypeface(null,Typeface.NORMAL)
            openAddress()
        }

        binding.layoutAddress.setOnClickListener {
            //findNavController().navigate(R.id.action_address)
            openAddress()
        }

        // radio mixed use property click
        binding.radioMixedPropertyYes.setOnClickListener {
            findNavController().navigate(R.id.action_mixed_property)
            binding.layoutMixedPropertyDetail.visibility = View.VISIBLE
        }
        // mixed property detail
        binding.layoutMixedPropertyDetail.setOnClickListener{
            findNavController().navigate(R.id.action_mixed_property)
        }

        // radio btn mixed use property Yes
        binding.radioMixedPropertyYes.setOnCheckedChangeListener { _, isChecked ->
           if(isChecked){
               binding.radioMixedPropertyYes.setTypeface(null, Typeface.BOLD)
               binding.radioMixedPropertyNo.setTypeface(null, Typeface.NORMAL)
           }
        }
        // radio btn mixed use property No
        binding.radioMixedPropertyNo.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.radioMixedPropertyNo.setTypeface(null, Typeface.BOLD)
                binding.radioMixedPropertyYes.setTypeface(null, Typeface.NORMAL)
                binding.layoutMixedPropertyDetail.visibility = View.GONE
            }
        }
        // occupying
        binding.radioOccupying.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.radioOccupying.setTypeface(null, Typeface.BOLD)
                binding.radioNonOccupying.setTypeface(null, Typeface.NORMAL)
            }
        }
        // radio non occupying
        binding.radioNonOccupying.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.radioNonOccupying.setTypeface(null, Typeface.BOLD)
                binding.radioOccupying.setTypeface(null, Typeface.NORMAL)
            }
        }

        // close
        binding.backButton.setOnClickListener {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }
        // back
        requireActivity().onBackPressedDispatcher.addCallback {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }

        binding.btnSave.setOnClickListener {
            requireActivity().finish()
        }

        binding.subpropertyParentLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(),binding.subpropertyParentLayout)
            super.removeFocusFromAllFields(binding.subpropertyParentLayout)
        }
    }

    private fun getPurchaseDetails(){
       // lifecycleScope.launchWhenStarted {
         //   viewModel.getSubjectPropertyDetails(token, 1010)
            viewModel.subjectPropertyDetails.observe(viewLifecycleOwner, { details ->
                if(details != null){
                    details.subPropertyData?.address?.let {
                        binding.radioSubPropertyAddress.isChecked = true
                        binding.radioTxtPropertyAdd.setTypeface(null,Typeface.BOLD)
                        binding.tvSubPropertyAddress.visibility = View.VISIBLE
                        binding.tvSubPropertyAddress.text = it.street+" "+it.unit+"\n"+it.city+" "+it.stateName+" "+it.zipCode+" "+it.countryName
                        addressList.add(AddressData(street= it.street, unit=it.unit, city=it.city,stateName=it.stateName,countryName=it.countryName,countyName = it.countyName,
                            countyId = it.countyId, stateId = it.stateId, countryId = it.countryId, zipCode = it.zipCode ))
                    } ?: run {
                        binding.radioSubPropertyTbd.isChecked = true
                        binding.radioSubPropertyTbd.setTypeface(null,Typeface.BOLD)
                    }

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
                        binding.edAppraisedPropertyValue.setText(Math.round(value).toString())
                        CustomMaterialFields.setColor(binding.layoutAppraisedProperty,R.color.grey_color_two,requireActivity())

                    }
                    // property tax
                    details.subPropertyData?.propertyTax?.let { value ->
                        binding.edPropertyTax.setText(Math.round(value).toString())
                        CustomMaterialFields.setColor(binding.layoutPropertyTaxes,R.color.grey_color_two,requireActivity())
                    }
                    // home insurance
                    details.subPropertyData?.homeOwnerInsurance?.let { value ->
                        binding.edHomeownerInsurance.setText(Math.round(value).toString())
                        CustomMaterialFields.setColor(binding.layoutHomeownerInsurance,R.color.grey_color_two,requireActivity())
                    }
                    // flood insurance
                    details.subPropertyData?.floodInsurance?.let { value ->
                        binding.edFloodInsurance.setText(Math.round(value).toString())
                        CustomMaterialFields.setColor(binding.layoutFloodInsurance,R.color.grey_color_two,requireActivity())
                    }

                    details.subPropertyData?.isMixedUseProperty?.let { value ->
                        if(value){
                            binding.radioMixedPropertyYes.isChecked = true
                            details.subPropertyData.mixedUsePropertyExplanation?.let { desc ->
                                binding.mixedPropertyExplanation.setText(desc)
                            }
                        }
                        else
                            binding.radioMixedPropertyNo.isChecked = true
                    } ?: run {
                        binding.radioMixedPropertyNo.isChecked = true
                    }
                    setDropDownData()
                    setCoBorrowerOccupancyStatus()

                }
            })
       // }
    }

    private fun setDropDownData(){
        //lifecycleScope.launchWhenStarted {
          //  viewModel.getPropertyTypes(token)
            viewModel.propertyType.observe(viewLifecycleOwner, {
                if(it != null && it.size > 0) {

                    val itemList:ArrayList<String> = arrayListOf()
                    for(item in it){
                        itemList.add(item.name)
                        if(propertyTypeId > 0 && propertyTypeId == item.id){
                            binding.tvPropertyType.setText(item.name)
                            CustomMaterialFields.setColor(binding.layoutPropertyType,R.color.grey_color_two,requireActivity())
                        }
                    }
                    val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,itemList)
                    binding.tvPropertyType.setAdapter(adapter)
//                    binding.tvPropertyType.setOnFocusChangeListener { _, _ ->
//                        binding.tvPropertyType.showDropDown()
//                    }
                    binding.tvPropertyType.setOnClickListener {
                        binding.tvPropertyType.showDropDown()
                    }

                    binding.tvPropertyType.onItemClickListener = object :
                        AdapterView.OnItemClickListener {
                        override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                            //binding.layoutPropertyType.defaultHintTextColor = ColorStateList.valueOf(
                              //  ContextCompat.getColor(requireContext(), R.color.grey_color_two))
                            CustomMaterialFields.setColor(binding.layoutPropertyType,R.color.grey_color_two,requireActivity())
                        }
                    }
                }
            })
       // }

        // occupancy Type spinner
        //lifecycleScope.launchWhenStarted {
            //viewModel.getOccupancyType("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhZGlxQHJhaW5zb2Z0Zm4uY29tIiwiRmlyc3ROYW1lIjoiU2FkaXEiLCJMYXN0TmFtZSI6Ik1hY2tub2ppYSIsIlRlbmFudENvZGUiOiJhaGNsZW5kaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTUNVIiwiZXhwIjoxNjM0MTc0Njg2LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.2E5FSNrooM9Fi7weXMOUj2WaRNEk2NNHfqINYndapBA")
            viewModel.occupancyType.observe(viewLifecycleOwner, {occupancyList->

                if(occupancyList != null && occupancyList.size > 0) {
                    val itemList: ArrayList<String> = arrayListOf()
                    for (item in occupancyList) {
                        itemList.add(item.name)
                        if(occupancyTypeId > 0 && occupancyTypeId == item.id){
                            binding.tvOccupancyType.setText(item.name)
                            CustomMaterialFields.setColor(binding.layoutOccupancyType,R.color.grey_color_two,requireActivity())
                        }
                    }

                    val adapterOccupanycyType = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,itemList)
                    binding.tvOccupancyType.setAdapter(adapterOccupanycyType)
//                    binding.tvOccupancyType.setOnFocusChangeListener { _, _ ->
//                        binding.tvOccupancyType.showDropDown()
//                    }
                    binding.tvOccupancyType.setOnClickListener {
                        binding.tvOccupancyType.showDropDown()
                    }

                    binding.tvOccupancyType.onItemClickListener = object : AdapterView.OnItemClickListener {
                        override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                            CustomMaterialFields.setColor(binding.layoutOccupancyType,R.color.grey_color_two,requireActivity())
                        }
                    }
                }

            })
        //}
    }

    private fun setCoBorrowerOccupancyStatus(){
       // lifecycleScope.launchWhenStarted {
         //   viewModel.getCoBorrowerOccupancyStatus(token, 1010)
            viewModel.coBorrowerOccupancyStatus.observe(viewLifecycleOwner, {
                if(it.occupancyData != null  && it.occupancyData.size > 0){
                    binding.radioOccupying.isChecked = true
                    binding.coBorrowerName.setText(it.occupancyData.get(0).borrowerFirstName + " " + it.occupancyData.get(0).borrowerLastName)
                }
                else {
                    binding.radioNonOccupying.isChecked = true
                    binding.coBorrowerName.visibility = View.GONE
                }
            })
        //}
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

    private fun openAddress(){
        val fragment = SubPropertyAddressFragment()
        val bundle = Bundle()
        bundle.putParcelableArrayList(AppConstant.address,addressList)
        fragment.arguments = bundle
        findNavController().navigate(R.id.action_address, fragment.arguments)
    }

}