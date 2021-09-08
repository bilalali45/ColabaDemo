package com.rnsoft.colabademo

import android.content.res.ColorStateList
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
import com.rnsoft.colabademo.databinding.SubjectPropertyBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat

/**
 * Created by Anita Kiran on 9/8/2021.
 */
class SubjectPropertyFragment : Fragment(), View.OnClickListener {

    lateinit var binding: SubjectPropertyBinding
    private val propertyTypeArray = listOf("Single Family Property","Condominium","Townhouse", "Cooperative", "Manufactured Home", "Duplex (2 Unit)", "Triplex (3 Unit)", "Quadplex (4 Unit)")
    private val occupancyTypeArray = listOf("Primary Residence", "Second Home", "Investment Property")
    var isPropertyAddress : Boolean = false
    var isMixedProperty : Boolean = false


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = SubjectPropertyBinding.inflate(inflater, container, false)


        binding.rbSubProperty.isChecked = false
        binding.rbSubPropertyAddress.isChecked = false
        binding.rbSubProperty.setOnClickListener(this)
        binding.rbSubPropertyAddress.setOnClickListener(this)
        binding.rbMixedPropertyNo.setOnClickListener(this)
        binding.rbMixedPropertyYes.setOnClickListener(this)
        binding.layoutDetails.setOnClickListener(this)
        binding.backButton.setOnClickListener(this)

        setSpinnerData()
        setInputFields()

        if(isPropertyAddress){
            binding.tvSubPropertyAddress.visibility = View.VISIBLE
        }

        if(isMixedProperty){
            binding.layoutDetails.visibility = View.VISIBLE
        }



        return binding.root

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.rb_sub_property -> radioSubPropertyClick()
            R.id.rb_sub_property_address -> radioSubPropertyAddressClick()
            R.id.rb_mixed_property_yes -> mixedPropertyDetail()
            R.id.rb_mixed_property_no -> binding.layoutDetails.visibility = View.GONE
            R.id.layout_details -> mixedPropertyDetail()
            R.id.backButton -> requireActivity().onBackPressed()


        }
     }

    private fun setInputFields(){

        // set lable focus
        binding.edAppraisedPropertyValue.setOnFocusChangeListener(MyCustomFocusListener(binding.edAppraisedPropertyValue, binding.layoutAppraisedProperty, requireContext()))
        binding.edPropertyTaxes.setOnFocusChangeListener(MyCustomFocusListener(binding.edPropertyTaxes, binding.layoutPropertyTaxes, requireContext()))
        binding.edHomeownerInsurance.setOnFocusChangeListener(MyCustomFocusListener(binding.edHomeownerInsurance, binding.layoutHomeownerInsurance, requireContext()))
        binding.edFloodInsurance.setOnFocusChangeListener(MyCustomFocusListener(binding.edFloodInsurance, binding.layoutFloodInsurance, requireContext()))

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

    }

    private fun radioSubPropertyAddressClick(){

        binding.rbSubProperty.isChecked = false
        binding.rbSubPropertyAddress.isChecked = true
        binding.tvSubPropertyAddress.visibility = View.VISIBLE
        isPropertyAddress = true
        findNavController().navigate(R.id.nav_sub_property_address)
    }

    private fun mixedPropertyDetail(){
        isMixedProperty = true
        findNavController().navigate(R.id.nav_mixed_use_property)
        binding.layoutDetails.visibility = View.VISIBLE
    }

     private fun checkValidations(){

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

                if(binding.tvPropertyType.text.isNotEmpty() && binding.tvPropertyType.text.isNotBlank()) {
                    //clearError(binding.layoutLoanStage)
                }
                /*
                if (position == propertyTypeArray.size - 1)
                    binding.layoutPropertyType.visibility = View.VISIBLE
                else
                    binding.layoutPropertyType.visibility = View.GONE */
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
                        requireContext(), R.color.grey_color_two
                    )
                )

                if (binding.tvOccupancyType.text.isNotEmpty() && binding.tvOccupancyType.text.isNotBlank()) {
                    //clearError(binding.layoutLoanStage)
                }
                /*
                if (position == occupancyTypeArray.size - 1)
                    binding.layoutOccupancyType.visibility = View.VISIBLE
                else
                    binding.layoutOccupancyType.visibility = View.GONE */
            }
        }
    }






}