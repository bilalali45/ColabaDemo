package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.res.ColorStateList
import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.DatePicker
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.SubPropertyRefinanceBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/9/2021.
 */
class SubjectPropertyRefinance : Fragment(), DatePickerDialog.OnDateSetListener, View.OnClickListener {

    private lateinit var binding : SubPropertyRefinanceBinding
    private val propertyTypeArray = listOf("Single Family Property","Condominium","Townhouse", "Cooperative", "Manufactured Home", "Duplex (2 Unit)", "Triplex (3 Unit)", "Quadplex (4 Unit)")
    private val occupancyTypeArray = listOf("Primary Residence", "Second Home", "Investment Property")
    var isPropertyAddress : Boolean = false
    var isMixedProperty : Boolean = false
    var isFirstMortgage : Boolean = false
    var isSecMortgage : Boolean = false


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = SubPropertyRefinanceBinding.inflate(inflater, container, false)

        setInputFields()
        setSpinnerData()


        binding.rbSubProperty.isChecked = false
        binding.rbSubPropertyAddress.isChecked = false
        binding.rbSubProperty.setOnClickListener(this)
        binding.rbSubPropertyAddress.setOnClickListener(this)
        binding.rbMixedPropertyNo.setOnClickListener(this)
        binding.rbMixedPropertyYes.setOnClickListener(this)
        binding.layoutDetails.setOnClickListener(this)
        binding.backButton.setOnClickListener(this)
        binding.rbFirstMortgageYes.setOnClickListener(this)
        binding.rbFirstMortgageNo.setOnClickListener(this)
        binding.rbSecMortageYes.setOnClickListener(this)
        binding.rbSecMortageNo.setOnClickListener(this)
        binding.layoutFirstMortgageDetail.setOnClickListener(this)
        binding.layoutSecMortgageDetail.setOnClickListener(this)
        binding.rbOccupying.setOnClickListener(this)
        binding.rbNonOccupying.setOnClickListener(this)

        binding.edDateOfHomePurchase.showSoftInputOnFocus = false
        binding.edDateOfHomePurchase.setOnClickListener { createCustomDialog() }
        binding.edDateOfHomePurchase.setOnFocusChangeListener{ _ , _ ->  createCustomDialog() }

        if(isFirstMortgage){
            binding.layoutFirstMortgageDetail.visibility = View.VISIBLE
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            binding.rbFirstMortgageYes.setTypeface(null, Typeface.BOLD)
        }

        if(isSecMortgage){
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            binding.layoutSecMortgageDetail.visibility = View.VISIBLE
        }

        if(isPropertyAddress){
            binding.tvSubPropertyAddress.visibility = View.VISIBLE
        }

        if(isMixedProperty){
            binding.rbMixedPropertyYes.setTypeface(null, Typeface.BOLD)
            binding.layoutDetails.visibility = View.VISIBLE
        }

        return binding.root

    }

    private fun setInputFields(){

        // set lable focus
        binding.edRentalIncome.setOnFocusChangeListener(MyCustomFocusListener(binding.edRentalIncome, binding.layoutRentalIncome, requireContext()))
        binding.edPropertyValue.setOnFocusChangeListener(MyCustomFocusListener(binding.edPropertyValue, binding.layoutPropertyValue, requireContext()))
        binding.edAssociation.setOnFocusChangeListener(MyCustomFocusListener(binding.edAssociation, binding.layoutAssociationDues, requireContext()))
        binding.edPropertyTaxes.setOnFocusChangeListener(MyCustomFocusListener(binding.edPropertyTaxes, binding.layoutPropertyTaxes, requireContext()))
        binding.edHomeownerInsurance.setOnFocusChangeListener(MyCustomFocusListener(binding.edHomeownerInsurance, binding.layoutHomeownerInsurance, requireContext()))
        binding.edFloodInsurance.setOnFocusChangeListener(MyCustomFocusListener(binding.edFloodInsurance, binding.layoutFloodInsurance, requireContext()))

        // set input format
        binding.edRentalIncome.addTextChangedListener(NumberTextFormat(binding.edRentalIncome))
        binding.edPropertyValue.addTextChangedListener(NumberTextFormat(binding.edPropertyValue))
        binding.edAssociation.addTextChangedListener(NumberTextFormat(binding.edAssociation))
        binding.edPropertyTaxes.addTextChangedListener(NumberTextFormat(binding.edPropertyTaxes))
        binding.edHomeownerInsurance.addTextChangedListener(NumberTextFormat(binding.edHomeownerInsurance))
        binding.edFloodInsurance.addTextChangedListener(NumberTextFormat(binding.edFloodInsurance))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutRentalIncome,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutPropertyValue,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutAssociationDues,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutPropertyTaxes,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutHomeownerInsurance,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutFloodInsurance,requireContext())

    }


    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.rb_sec_mortage_yes -> onSecMortgageYesClick()
            R.id.rb_sec_mortage_no -> binding.layoutSecMortgageDetail.visibility = View.GONE
            R.id.layout_first_mortgage_detail -> onFirstMortgageYes()
            R.id.layout_sec_mortgage_detail -> onSecMortgageYesClick()
            R.id.backButton -> requireActivity().onBackPressed()
            R.id.rb_sub_property -> radioSubPropertyClick()
            R.id.rb_sub_property_address -> radioSubPropertyAddressClick()
            R.id.rb_mixed_property_yes -> mixedPropertyDetailClick()
            R.id.layout_details -> mixedPropertyDetailClick()
            R.id.rb_first_mortgage_yes -> onFirstMortgageYes()

            R.id.rb_first_mortgage_no -> {
                binding.layoutFirstMortgageDetail.visibility = View.GONE
                binding.layoutSecondMortgage.visibility = View.GONE
                binding.rbFirstMortgageNo.setTypeface(null, Typeface.BOLD)
                binding.rbFirstMortgageYes.setTypeface(null, Typeface.NORMAL)
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
                binding.layoutOccupancyType.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two))
                showHideRental()

            }
        }
    }

    private fun showHideRental(){
        if(binding.tvOccupancyType.text.toString().equals("Investment Property")) {
            binding.layoutRentalIncome.visibility = View.VISIBLE

        } else if (binding.tvOccupancyType.text.toString().equals("Primary Residence")) {
            var propertyType = binding.tvPropertyType.text.toString()
            if (propertyType.equals("Duplex (2 Unit)") || propertyType.equals("Triplex (3 Unit)") || propertyType.equals("Quadplex (4 Unit)"))
                binding.layoutRentalIncome.visibility = View.VISIBLE
            else
                binding.layoutRentalIncome.visibility = View.GONE

        } else if (binding.tvOccupancyType.text.toString().equals("Second Home")){
            binding.layoutRentalIncome.visibility = View.GONE
        }
    }

    private fun createCustomDialog(){
        val pd = MonthYearPickerDialog()
        pd.setListener(this)
        pd.show(requireActivity().supportFragmentManager, "MonthYearPickerDialog")
    }

    override fun onDateSet(p0: DatePicker?, p1: Int, p2: Int, p3: Int) {
        var stringMonth = p2.toString()
        if(p2<10)
            stringMonth = "0$p2"

        val sampleDate = "$stringMonth / $p1"
        binding.edDateOfHomePurchase.setText(sampleDate)
    }

    private fun mixedPropertyDetailClick(){
        isMixedProperty = true
        findNavController().navigate(R.id.nav_mixed_use_property)
        binding.layoutDetails.visibility = View.VISIBLE
        binding.rbMixedPropertyYes.setTypeface(null, Typeface.BOLD)
        binding.rbMixedPropertyNo.setTypeface(null, Typeface.NORMAL)
    }

    private fun radioSubPropertyClick(){
        binding.rbSubProperty.isChecked = true
        binding.rbSubPropertyAddress.isChecked = false
        binding.tvSubPropertyAddress.visibility = View.GONE

    }

    private fun radioSubPropertyAddressClick(){
        binding.rbSubProperty.isChecked = false
        binding.rbSubPropertyAddress.isChecked = true
        binding.tvSubPropertyAddress.visibility = View.VISIBLE
        isPropertyAddress = true
        findNavController().navigate(R.id.nav_sub_property_address)
    }

    private fun onFirstMortgageYes(){
        if(binding.rbFirstMortgageYes.isChecked) {
            isFirstMortgage = true
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            findNavController().navigate(R.id.nav_first_mortage)
            binding.rbFirstMortgageYes.setTypeface(null, Typeface.BOLD)
            binding.rbFirstMortgageNo.setTypeface(null, Typeface.NORMAL)

        }
    }

    private fun onSecMortgageYesClick(){
        isSecMortgage = true
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        binding.layoutSecMortgageDetail.visibility = View.VISIBLE
        findNavController().navigate(R.id.nav_sec_mortage)
    }

}