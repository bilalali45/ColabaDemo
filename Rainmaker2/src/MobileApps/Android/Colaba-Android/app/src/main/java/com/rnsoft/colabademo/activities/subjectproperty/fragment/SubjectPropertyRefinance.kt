package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.res.ColorStateList
import android.graphics.Typeface
import android.os.Bundle
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
import com.rnsoft.colabademo.databinding.SubjectPropertyPurchaseBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import com.rnsoft.colabademo.utils.NumberTextFormat
import java.util.*


/**
 * Created by Anita Kiran on 9/9/2021.
 */
class SubjectPropertyRefinance : Fragment(), DatePickerDialog.OnDateSetListener, View.OnClickListener {

    private lateinit var binding: SubPropertyRefinanceBinding
    private val propertyTypeArray = listOf(
        "Single Family Property",
        "Condominium",
        "Townhouse",
        "Cooperative",
        "Manufactured Home",
        "Duplex (2 Unit)",
        "Triplex (3 Unit)",
        "Quadplex (4 Unit)"
    )
    private val occupancyTypeArray =
        listOf("Primary Residence", "Second Home", "Investment Property")

    private var savedViewInstance: View? = null

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
            binding.rbSecMortgageYes.setOnClickListener(this)
            binding.rbSecMortgageNo.setOnClickListener(this)
            binding.layoutFirstMortgageDetail.setOnClickListener(this)
            binding.layoutSecMortgageDetail.setOnClickListener(this)
            binding.rbOccupying.setOnClickListener(this)
            binding.rbNonOccupying.setOnClickListener(this)
            binding.refinanceParentLayout.setOnClickListener(this)
            binding.btnSave.setOnClickListener(this)

            binding.edDateOfHomePurchase.showSoftInputOnFocus = false
            binding.edDateOfHomePurchase.setOnClickListener { createCustomDialog() }
            binding.edDateOfHomePurchase.setOnFocusChangeListener { _, _ -> createCustomDialog() }

            setSpinnerData()
            setInputFields()

            savedViewInstance
        }

    }

    private fun setInputFields() {

        // set lable focus
        binding.edRentalIncome.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edRentalIncome,
                binding.layoutRentalIncome,
                requireContext()
            )
        )
        binding.edPropertyValue.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edPropertyValue,
                binding.layoutPropertyValue,
                requireContext()
            )
        )
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
            R.id.backButton -> requireActivity().finish()
            R.id.btn_save -> requireActivity().finish()
            R.id.rb_sub_property -> radioSubPropertyClick()
            R.id.rb_sub_property_address -> radioAddressClick()
            R.id.layout_address -> radioAddressClick()
            R.id.rb_mixed_property_yes -> mixedPropertyDetailClick()
            R.id.layout_details -> mixedPropertyDetailClick()
            R.id.rb_first_mortgage_yes -> onFirstMortgageYes()
            R.id.refinance_parent_layout -> HideSoftkeyboard.hide(
                requireActivity(),
                binding.refinanceParentLayout
            )

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
                } else {
                    binding.rbMixedPropertyNo.setTypeface(null, Typeface.NORMAL)
                }

            R.id.rb_occupying ->
                if (binding.rbOccupying.isChecked) {
                    binding.rbOccupying.setTypeface(null, Typeface.BOLD)
                    binding.rbNonOccupying.setTypeface(null, Typeface.NORMAL)
                } else {
                    binding.rbOccupying.setTypeface(null, Typeface.NORMAL)
                }

            R.id.rb_non_occupying ->
                if (binding.rbNonOccupying.isChecked) {
                    binding.rbNonOccupying.setTypeface(null, Typeface.BOLD)
                    binding.rbOccupying.setTypeface(null, Typeface.NORMAL)
                } else {
                    binding.rbNonOccupying.setTypeface(null, Typeface.NORMAL)
                }
        }
    }

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
    }

    private fun showHideRental() {
        if (binding.tvOccupancyType.text.toString().equals("Investment Property")) {
            binding.layoutRentalIncome.visibility = View.VISIBLE

        } else if (binding.tvOccupancyType.text.toString().equals("Primary Residence")) {
            var propertyType = binding.tvPropertyType.text.toString()
            if (propertyType.equals("Duplex (2 Unit)") || propertyType.equals("Triplex (3 Unit)") || propertyType.equals(
                    "Quadplex (4 Unit)"
                )
            )
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
        findNavController().navigate(R.id.nav_mixed_use_property)
        binding.layoutDetails.visibility = View.VISIBLE
        binding.rbMixedPropertyYes.setTypeface(null, Typeface.BOLD)
        binding.rbMixedPropertyNo.setTypeface(null, Typeface.NORMAL)
    }

    private fun radioSubPropertyClick() {
        binding.rbSubProperty.isChecked = true
        binding.rbSubPropertyAddress.isChecked = false
        binding.tvSubPropertyAddress.visibility = View.GONE
        //bold text
        binding.rbSubProperty.setTypeface(null, Typeface.BOLD)
        binding.radioTxtPropertyAdd.setTypeface(null, Typeface.NORMAL)
    }

    private fun radioAddressClick() {
        binding.rbSubProperty.isChecked = false
        binding.rbSubPropertyAddress.isChecked = true
        binding.tvSubPropertyAddress.visibility = View.VISIBLE
        //bold text
        binding.radioTxtPropertyAdd.setTypeface(null, Typeface.BOLD)
        binding.rbSubProperty.setTypeface(null, Typeface.NORMAL)
        findNavController().navigate(R.id.nav_sub_property_address)
    }

    private fun onFirstMortgageYes() {
        if (binding.rbFirstMortgageYes.isChecked) {
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            //findNavController().navigate(R.id.nav_first_mortage)
            binding.rbFirstMortgageYes.setTypeface(null, Typeface.BOLD)
            binding.rbFirstMortgageNo.setTypeface(null, Typeface.NORMAL)

            val fragment = FirstMortgageFragment()
            val bundle = Bundle()
            bundle.putString(AppConstant.address, getString(R.string.subject_property))
            fragment.arguments = bundle
            findNavController().navigate(R.id.nav_first_mortage, fragment.arguments)

        }
    }

        private fun onSecMortgageYesClick() {
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            binding.layoutSecMortgageDetail.visibility = View.VISIBLE
            findNavController().navigate(R.id.nav_sec_mortage)
            binding.rbSecMortgageYes.setTypeface(null, Typeface.BOLD)
            binding.rbSecMortgageNo.setTypeface(null, Typeface.NORMAL)
        }

        private fun onSecMortgegeNoClick() {
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            binding.layoutSecMortgageDetail.visibility = View.GONE
            binding.rbSecMortgageNo.setTypeface(null, Typeface.BOLD)
            binding.rbSecMortgageYes.setTypeface(null, Typeface.NORMAL)
        }

}