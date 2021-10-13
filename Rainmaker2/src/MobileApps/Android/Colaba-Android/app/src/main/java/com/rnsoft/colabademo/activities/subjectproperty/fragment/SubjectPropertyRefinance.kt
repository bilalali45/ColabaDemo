package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.DatePicker
import androidx.activity.addCallback
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.SubPropertyRefinanceBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import com.rnsoft.colabademo.utils.NumberTextFormat


/**
 * Created by Anita Kiran on 9/9/2021.
 */
class SubjectPropertyRefinance : BaseFragment(), DatePickerDialog.OnDateSetListener, View.OnClickListener {

    private val viewModel : SubjectPropertyViewModel by activityViewModels()
    private lateinit var binding: SubPropertyRefinanceBinding
    //private val propertyTypeArray = listOf("Single Family Property", "Condominium", "Townhouse", "Cooperative", "Manufactured Home", "Duplex (2 Unit)", "Triplex (3 Unit)", "Quadplex (4 Unit)")
    //private val occupancyTypeArray = listOf("Primary Residence", "Second Home", "Investment Property")
    private var savedViewInstance: View? = null
    private var propertyTypeId : Int = 0
    private var occupancyTypeId : Int = 0
    val token : String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhZGlxQHJhaW5zb2Z0Zm4uY29tIiwiRmlyc3ROYW1lIjoiU2FkaXEiLCJMYXN0TmFtZSI6Ik1hY2tub2ppYSIsIlRlbmFudENvZGUiOiJhaGNsZW5kaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTUNVIiwiZXhwIjoxNjM0MTc0Njg2LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.2E5FSNrooM9Fi7weXMOUj2WaRNEk2NNHfqINYndapBA"



    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = SubPropertyRefinanceBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root


            //binding.rbSubProperty.isChecked = false
            //binding.rbSubPropertyAddress.isChecked = false
            //binding.rbSubProperty.setOnClickListener(this)
            //binding.rbSubPropertyAddress.setOnClickListener(this)
            //binding.rbMixedPropertyNo.setOnClickListener(this)
            //binding.radioMixedPropertyYes.setOnClickListener(this)
            //binding.layoutDetails.setOnClickListener(this)
            binding.backButton.setOnClickListener(this)
            //binding.radioHasFirstMortgage.setOnClickListener(this)
            //binding.rbFirstMortgageNo.setOnClickListener(this)
            //binding.rbSecMortgageYes.setOnClickListener(this)
            //binding.rbSecMortgageNo.setOnClickListener(this)
            binding.layoutFirstMortgageDetail.setOnClickListener(this)
            binding.layoutSecMortgageDetail.setOnClickListener(this)
            //binding.rbOccupying.setOnClickListener(this)
            //binding.rbNonOccupying.setOnClickListener(this)
            binding.refinanceParentLayout.setOnClickListener(this)
            binding.btnSave.setOnClickListener(this)
            binding.layoutAddress.setOnClickListener(this)

            binding.edDateOfHomePurchase.showSoftInputOnFocus = false
            binding.edDateOfHomePurchase.setOnClickListener { createCustomDialog() }
            //binding.edDateOfHomePurchase.setOnFocusChangeListener { _, _ -> createCustomDialog() }

            setUpUI()
            setInputFields()
            getRefinanceDetails()
            getCoBorrowerOccupancyStatus()
            super.addListeners(binding.root)

            requireActivity().onBackPressedDispatcher.addCallback {
                requireActivity().finish()
                requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
            }
            savedViewInstance
        }
    }

    private fun setUpUI(){

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
        }

        binding.layoutAddress.setOnClickListener {
            findNavController().navigate(R.id.action_address)
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
        binding.rbOccupying.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.rbOccupying.setTypeface(null, Typeface.BOLD)
                binding.rbNonOccupying.setTypeface(null, Typeface.NORMAL)
            }
        }
        // radio non occupying
        binding.rbNonOccupying.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.rbNonOccupying.setTypeface(null, Typeface.BOLD)
                binding.rbOccupying.setTypeface(null, Typeface.NORMAL)
            }
        }

        // first mortgage yes
        binding.radioHasFirstMortgageYes.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.radioHasFirstMortgageYes.setTypeface(null, Typeface.BOLD)
                binding.radioHasFirstMortgageNo.setTypeface(null, Typeface.NORMAL)
                //binding.layoutFirstMortgageDetail.visibility = View.VISIBLE
            }
        }

        // first mortgage no
        binding.radioHasFirstMortgageNo.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.radioHasFirstMortgageNo.setTypeface(null, Typeface.BOLD)
                binding.radioHasFirstMortgageYes.setTypeface(null, Typeface.NORMAL)
                //binding.layoutFirstMortgageDetail.visibility = View.GONE
            }
        }

    }


    private fun getRefinanceDetails(){
        lifecycleScope.launchWhenStarted {
            viewModel.getRefinanceDetails(token, 1010)
            viewModel.refinanceDetails.observe(viewLifecycleOwner, { details->

                if(details != null){
                    details.subPropertyData?.address?.let {
                        binding.radioSubPropertyAddress.isChecked = true
                        binding.radioTxtPropertyAdd.setTypeface(null,Typeface.BOLD)
                        binding.tvSubPropertyAddress.visibility = View.VISIBLE
                        binding.tvSubPropertyAddress.text = it.street+" "+it.unit+"\n"+it.city+" "+it.stateName+" "+it.zipCode+" "+it.countryName
                    } ?: run {
                        binding.radioSubPropertyTbd.isChecked = true
                        binding.radioSubPropertyTbd.setTypeface(null,Typeface.BOLD)
                    }
                    // property id
                    details.subPropertyData?.propertyTypeId?.let { id ->
                        propertyTypeId = id
                    }
                    // occupancy id
                    details.subPropertyData?.propertyUsageId?.let { id ->
                        occupancyTypeId = id
                    }
                    // appraised value
                    details.subPropertyData?.propertyValue?.let { value ->
                        binding.edPropertyValue.setText(value.toString())
                    }
                    // hoa dues
                    details.subPropertyData?.hoaDues?.let { value ->
                        binding.edAssociation.setText(value.toString())
                    }
                    // property tax
                    details.subPropertyData?.propertyTax?.let { value ->
                        binding.edPropertyTaxes.setText(value.toString())
                    }
                    // home insurance
                    details.subPropertyData?.homeOwnerInsurance?.let { value ->
                        binding.edHomeownerInsurance.setText(value.toString())
                    }
                    // flood insurance
                    details.subPropertyData?.floodInsurance?.let { value ->

                        binding.edFloodInsurance.setText(value.toString())
                    }

                    details.subPropertyData?.isMixedUseProperty?.let { value ->
                        if(value){
                            binding.radioMixedPropertyYes.isChecked = true
                            details.subPropertyData.mixedUsePropertyExplanation?.let { desc ->
                                binding.mixedPropertyExplanation.setText(desc)
                            }
                        } else
                            binding.radioMixedPropertyNo.isChecked = true
                    } ?: run {
                        binding.radioMixedPropertyNo.isChecked = true
                    }

                    // has first mortgage
                    details.subPropertyData?.hasFirstMortgage?.let{ hasFirstMortgage->
                        if(hasFirstMortgage){
                            binding.radioHasFirstMortgageYes.isChecked = true
                            details.subPropertyData.firstMortgageModel?.let {

                            }
                        }
                    }

                    // has second mortgage
                    details.subPropertyData?.hasSecondMortgage?.let{ hasSecondMortgage->
                        if(hasSecondMortgage){
                            binding.rbSecMortgageYes.isChecked = true
                        }
                    }


                    getDropDownData()

                    }

            })
        }
    }

    private fun getCoBorrowerOccupancyStatus(){
        lifecycleScope.launchWhenStarted {
            viewModel.getCoBorrowerOccupancyStatus(token, 1010)
            viewModel.coBorrowerOccupancyStatus.observe(viewLifecycleOwner, {
                if(it.occupancyData != null  && it.occupancyData.size > 0){
                    binding.rbOccupying.isChecked = true
                    binding.rbOccupying.setTypeface(null, Typeface.BOLD)
                    binding.coBorrowerName.setText(it.occupancyData.get(0).borrowerFirstName + " " + it.occupancyData.get(0).borrowerLastName)
                }
                else {
                    binding.rbNonOccupying.isChecked = true
                    binding.rbNonOccupying.setTypeface(null, Typeface.BOLD)
                    binding.coBorrowerName.visibility = View.GONE
                }
            })
        }
    }

    private fun getDropDownData(){
        lifecycleScope.launchWhenStarted {
            viewModel.getPropertyTypes(token)
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
                    //binding.tvPropertyType.setOnFocusChangeListener { _, _ ->
                     //   binding.tvPropertyType.showDropDown()
                    //}
                    binding.tvPropertyType.setOnClickListener {
                        binding.tvPropertyType.showDropDown()
                    }
                    binding.tvPropertyType.onItemClickListener = object :
                        AdapterView.OnItemClickListener {
                        override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                            CustomMaterialFields.setColor(binding.layoutPropertyType,R.color.grey_color_two,requireActivity())
                        }
                    }
                }
            })
        }

        // occupancy Type spinner
        lifecycleScope.launchWhenStarted {
            viewModel.getOccupancyType(token)
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

                    val adapterOccupanycyType =
                        ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,itemList)
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
                            CustomMaterialFields.setColor(binding.layoutOccupancyType,R.color.grey_color_two,requireActivity())
                        }
                    }
                }

            })
        }
    }

    private fun setInputFields() {

        /*binding.edDateOfHomePurchase.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                //createCustomDialog()
                CustomMaterialFields.setColor(binding.layoutDateOfHomepurchase,R.color.grey_color_two,requireActivity())
            } else {
                if (binding.edDateOfHomePurchase.text?.length == 0)
                    CustomMaterialFields.setColor(binding.layoutDateOfHomepurchase,R.color.grey_color_three,requireActivity())
                else
                    CustomMaterialFields.setColor(binding.layoutDateOfHomepurchase,R.color.grey_color_two,requireActivity())
            }
        } */

        // set lable focus
        binding.edRentalIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edRentalIncome, binding.layoutRentalIncome, requireContext()))
        binding.edPropertyValue.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edPropertyValue, binding.layoutPropertyValue, requireContext()))
        binding.edAssociation.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edAssociation,
                binding.layoutAssociationDues,
                requireContext()
            )
        )
        binding.edPropertyTaxes.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edPropertyTaxes,
                binding.layoutPropertyTaxes,
                requireContext()
            )
        )
        binding.edHomeownerInsurance.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edHomeownerInsurance,
                binding.layoutHomeownerInsurance,
                requireContext()
            )
        )
        binding.edFloodInsurance.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edFloodInsurance,
                binding.layoutFloodInsurance,
                requireContext()
            )
        )

        // set input format
        binding.edRentalIncome.addTextChangedListener(NumberTextFormat(binding.edRentalIncome))
        binding.edPropertyValue.addTextChangedListener(NumberTextFormat(binding.edPropertyValue))
        binding.edAssociation.addTextChangedListener(NumberTextFormat(binding.edAssociation))
        binding.edPropertyTaxes.addTextChangedListener(NumberTextFormat(binding.edPropertyTaxes))
        binding.edHomeownerInsurance.addTextChangedListener(NumberTextFormat(binding.edHomeownerInsurance))
        binding.edFloodInsurance.addTextChangedListener(NumberTextFormat(binding.edFloodInsurance))
        CustomMaterialFields.onTextChangedLableColor(requireActivity(), binding.edDateOfHomePurchase, binding.layoutDateOfHomepurchase)


        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutRentalIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutPropertyValue, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutAssociationDues, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutPropertyTaxes, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutHomeownerInsurance, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutFloodInsurance, requireContext())

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.rb_sec_mortgage_yes -> onSecMortgageYesClick()
            R.id.rb_sec_mortgage_no -> onSecMortgegeNoClick()
            R.id.layout_first_mortgage_detail -> onFirstMortgageYes()
            R.id.layout_sec_mortgage_detail -> onSecMortgageYesClick()
            R.id.backButton -> {
                requireActivity().finish()
                requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
            }
            R.id.btn_save -> requireActivity().finish()
            //R.id.rb_sub_property -> radioSubPropertyClick()
            //R.id.rb_sub_property_address -> radioAddressClick()
            //R.id.layout_address -> radioAddressClick()
            R.id.radio_mixed_property_yes -> mixedPropertyDetailClick()
            R.id.layout_details -> mixedPropertyDetailClick()
            R.id.rb_first_mortgage_yes -> onFirstMortgageYes()
            R.id.refinance_parent_layout -> {
                HideSoftkeyboard.hide(requireActivity(), binding.refinanceParentLayout)
                super.removeFocusFromAllFields(binding.refinanceParentLayout)
            }

//            R.id.rb_first_mortgage_no -> {
//                binding.layoutFirstMortgageDetail.visibility = View.GONE
//                binding.layoutSecondMortgage.visibility = View.GONE
//                binding.rbFirstMortgageNo.setTypeface(null, Typeface.BOLD)
//                binding.radioHasFirstMortgage.setTypeface(null, Typeface.NORMAL)
//            }

//            R.id.rb_mixed_property_no ->
//                if (binding.rbMixedPropertyNo.isChecked) {
//                    binding.layoutDetails.visibility = View.GONE
//                    binding.rbMixedPropertyNo.setTypeface(null, Typeface.BOLD)
//                    binding.radioMixedPropertyYes.setTypeface(null, Typeface.NORMAL)
//                } else {
//                    binding.rbMixedPropertyNo.setTypeface(null, Typeface.NORMAL)
//                }

//            R.id.rb_occupying ->
//                if (binding.rbOccupying.isChecked) {
//                    binding.rbOccupying.setTypeface(null, Typeface.BOLD)
//                    binding.rbNonOccupying.setTypeface(null, Typeface.NORMAL)
//                } else {
//                    binding.rbOccupying.setTypeface(null, Typeface.NORMAL)
//                }
//
//            R.id.rb_non_occupying ->
//                if (binding.rbNonOccupying.isChecked) {
//                    binding.rbNonOccupying.setTypeface(null, Typeface.BOLD)
//                    binding.rbOccupying.setTypeface(null, Typeface.NORMAL)
//                } else {
//                    binding.rbNonOccupying.setTypeface(null, Typeface.NORMAL)
//                }
        }
    }


    private fun showHideRental() {
        if (binding.tvOccupancyType.text.toString().equals("Investment Property")) {
            binding.layoutRentalIncome.visibility = View.VISIBLE

        } else if (binding.tvOccupancyType.text.toString().equals("Primary Residence")) {
            var propertyType = binding.tvPropertyType.text.toString()
            if (propertyType.equals("Duplex (2 Unit)") || propertyType.equals("Triplex (3 Unit)") || propertyType.equals(
                    "Quadplex (4 Unit)"))
                binding.layoutRentalIncome.visibility = View.VISIBLE
            else
                binding.layoutRentalIncome.visibility = View.GONE

        } else if (binding.tvOccupancyType.text.toString().equals("Second Home")) {
            binding.layoutRentalIncome.visibility = View.GONE
        }
    }

    private fun createCustomDialog() {
        val pd = MonthYearPickerDialog()
        pd.setListener(this)
        pd.show(requireActivity().supportFragmentManager, "MonthYearPickerDialog")
    }

    override fun onDateSet(p0: DatePicker?, p1: Int, p2: Int, p3: Int) {
        var stringMonth = p2.toString()
        if (p2 < 10)
            stringMonth = "0$p2"

        val sampleDate = "$stringMonth / $p1"
        binding.edDateOfHomePurchase.setText(sampleDate)
    }

    private fun mixedPropertyDetailClick() {
        findNavController().navigate(R.id.action_mixed_property)
        //binding.layoutDetails.visibility = View.VISIBLE
        binding.radioMixedPropertyYes.setTypeface(null, Typeface.BOLD)
        //binding.rbMixedPropertyNo.setTypeface(null, Typeface.NORMAL)
    }

    private fun radioSubPropertyClick() {
        //binding.rbSubProperty.isChecked = true
        //binding.rbSubPropertyAddress.isChecked = false
        binding.tvSubPropertyAddress.visibility = View.GONE
        //bold text
        //binding.rbSubProperty.setTypeface(null, Typeface.BOLD)
        binding.radioTxtPropertyAdd.setTypeface(null, Typeface.NORMAL)
    }

    private fun radioAddressClick() {
        //binding.rbSubProperty.isChecked = false
        //binding.rbSubPropertyAddress.isChecked = true
        binding.tvSubPropertyAddress.visibility = View.VISIBLE
        //bold text
        binding.radioTxtPropertyAdd.setTypeface(null, Typeface.BOLD)
        //binding.rbSubProperty.setTypeface(null, Typeface.NORMAL)
        findNavController().navigate(R.id.action_address)
    }

    private fun onFirstMortgageYes() {
        if (binding.radioHasFirstMortgageYes.isChecked) {
            binding.layoutFirstMortgageDetail.visibility = View.VISIBLE
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            //findNavController().navigate(R.id.nav_first_mortage)
            //binding.radioHasFirstMortgage.setTypeface(null, Typeface.BOLD)
            //binding.rbFirstMortgageNo.setTypeface(null, Typeface.NORMAL)

            val fragment = FirstMortgageFragment()
            val bundle = Bundle()
            bundle.putString(AppConstant.address, getString(R.string.subject_property))
            fragment.arguments = bundle
            findNavController().navigate(R.id.action_refinance_first_mortgage, fragment.arguments)

        }
    }

    private fun onSecMortgageYesClick() {
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        binding.layoutSecMortgageDetail.visibility = View.VISIBLE
        findNavController().navigate(R.id.action_refinance_sec_mortgage)
        binding.rbSecMortgageYes.setTypeface(null, Typeface.BOLD)
        binding.rbSecMortgageNo.setTypeface(null, Typeface.NORMAL)
    }

    private fun onSecMortgegeNoClick() {
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        binding.layoutSecMortgageDetail.visibility = View.GONE
        binding.rbSecMortgageNo.setTypeface(null, Typeface.BOLD)
        binding.rbSecMortgageYes.setTypeface(null, Typeface.NORMAL)
    }

/*
    private fun setSpinnerData() {
        val adapter =
            ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, propertyTypeArray)
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
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two)
                )
                showHideRental()
            }
        }

        // set occupancy type spinner

        val adapterOccupanycyType =
            ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, occupancyTypeArray)
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
                binding.layoutOccupancyType.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(),
                        R.color.grey_color_two
                    )
                )
                showHideRental()

            }
        }
    } */

}