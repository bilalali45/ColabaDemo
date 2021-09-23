package com.rnsoft.colabademo

import android.content.res.ColorStateList
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.activities.addresses.info.fragment.DeleteCurrentResidenceDialogFragment
import com.rnsoft.colabademo.activities.addresses.info.fragment.SwipeToDeleteEvent
import com.rnsoft.colabademo.activities.income.fragment.BottomDialogSelectEmployment
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.RealEstateOwnedLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.util.*


/**
 * Created by Anita Kiran on 9/16/2021.
 */
class RealEstateOwnedFragment : BaseFragment() , View.OnClickListener {

    private lateinit var binding: RealEstateOwnedLayoutBinding
    private lateinit var toolbar: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = RealEstateOwnedLayoutBinding.inflate(inflater, container, false)
            toolbar = binding.headerRealestate
            savedViewInstance = binding.root

            // set Header title
            toolbar.toolbarTitle.setText(getString(R.string.real_estate_owned))

            initViews()
            super.addListeners(binding.root)
            savedViewInstance

        }
    }


    private fun initViews(){
        binding.realestateMainlayout.setOnClickListener(this)
        toolbar.btnClose.setOnClickListener(this)
        binding.btnSave.setOnClickListener(this)
        toolbar.btnTopDelete.setOnClickListener(this)
        binding.rbFirstMortgageYes.setOnClickListener(this)
        binding.rbFirstMortgageNo.setOnClickListener(this)
        binding.rbSecMortgageYes.setOnClickListener(this)
        binding.rbSecMortgageNo.setOnClickListener(this)
        binding.layoutFirstMortgageDetail.setOnClickListener(this)
        binding.layoutSecMortgageDetail.setOnClickListener(this)
        binding.layoutAddress.setOnClickListener(this)

        setInputFields()
        setSpinnerData()

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.realestate_mainlayout -> HideSoftkeyboard.hide(requireActivity(),binding.realestateMainlayout)
            R.id.btn_close -> requireActivity().finish()
            R.id.btn_save -> checkValidations()
            R.id.btn_top_delete -> DeleteCurrentResidenceDialogFragment.newInstance(getString(R.string.txt_delete_property)).show(childFragmentManager, DeleteCurrentResidenceDialogFragment::class.java.canonicalName)
            R.id.rb_first_mortgage_yes -> onFirstMortgageYes()
            R.id.rb_first_mortgage_no -> onFirstMortgegeNoClick()
            R.id.rb_sec_mortgage_yes -> onSecMortgageYesClick()
            R.id.rb_sec_mortgage_no -> onSecMortgegeNoClick()
            R.id.layout_first_mortgage_detail -> onFirstMortgageYes()
            R.id.layout_sec_mortgage_detail -> onSecMortgageYesClick()
            R.id.layout_address-> openAddressFragment()
        }
    }


    private fun setInputFields(){

        // set lable focus
        binding.edRentalIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edRentalIncome, binding.layoutRentalIncome, requireContext()))
        binding.edAssociationDues.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edAssociationDues, binding.layoutAssociationDues, requireContext()))
        binding.edPropertyValue.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edPropertyValue, binding.layoutPropertyValue, requireContext()))
        binding.edPropertyTaxes.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edPropertyTaxes, binding.layoutPropertyTaxes, requireContext()))
        binding.edHomeownerInsurance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edHomeownerInsurance, binding.layoutHomeownerInsurance, requireContext()))
        binding.edFloodInsurance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edFloodInsurance, binding.layoutFloodInsurance, requireContext()))

        // set input format
        binding.edRentalIncome.addTextChangedListener(NumberTextFormat(binding.edRentalIncome))
        binding.edAssociationDues.addTextChangedListener(NumberTextFormat(binding.edAssociationDues))
        binding.edPropertyValue.addTextChangedListener(NumberTextFormat(binding.edPropertyValue))
        binding.edPropertyTaxes.addTextChangedListener(NumberTextFormat(binding.edPropertyTaxes))
        binding.edHomeownerInsurance.addTextChangedListener(NumberTextFormat(binding.edHomeownerInsurance))
        binding.edFloodInsurance.addTextChangedListener(NumberTextFormat(binding.edFloodInsurance))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutRentalIncome,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutAssociationDues,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutPropertyValue,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutPropertyTaxes,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutHomeownerInsurance,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutFloodInsurance,requireContext())

    }

    private fun setSpinnerData() {

        val propertyList = arrayListOf<String>(*resources.getStringArray(R.array.array_property_type))
        val occupancyList = arrayListOf<String>(*resources.getStringArray(R.array.array_occupancy_type))
        val statusList = arrayListOf<String>(*resources.getStringArray(R.array.array_property_status))

        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,propertyList )
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
            ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, occupancyList)
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
                    ContextCompat.getColor(requireContext(),R.color.grey_color_two))
                showHideRental()

            }
        }

        val adapterStatus =
            ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, statusList)
        binding.tvPropertyStatus.setAdapter(adapterStatus)
        binding.tvPropertyStatus.setOnFocusChangeListener { _, _ -> binding.tvPropertyStatus.showDropDown() }
        binding.tvPropertyStatus.setOnClickListener {
            binding.tvPropertyStatus.showDropDown()
        }
        binding.tvPropertyStatus.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutPropertyStatus.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(),R.color.grey_color_two))
                showHideRental()

            }
        }
    }

    private fun showHideRental(){
        if(binding.tvOccupancyType.text.toString().equals("Investment Property")) {
            binding.layoutRentalIncome.visibility = View.VISIBLE

        } else if (binding.tvOccupancyType.text.toString().equals("Primary Residence")) {
            val propertyType = binding.tvPropertyType.text.toString()
            if (propertyType.equals("Duplex (2 Unit)") || propertyType.equals("Triplex (3 Unit)") || propertyType.equals("Quadplex (4 Unit)"))
                binding.layoutRentalIncome.visibility = View.VISIBLE
            else
                binding.layoutRentalIncome.visibility = View.GONE

        } else if (binding.tvOccupancyType.text.toString().equals("Second Home")){
            binding.layoutRentalIncome.visibility = View.GONE
        }
    }


    private fun openAddressFragment(){
        val addressFragment = IncomeAddress()
        val bundle = Bundle()
        bundle.putString(AppConstant.address, getString(R.string.property_address))
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_realestate_address, addressFragment.arguments)
    }

    private fun onFirstMortgageYes(){
        if(binding.rbFirstMortgageYes.isChecked) {
            binding.layoutFirstMortgageDetail.visibility = View.VISIBLE
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            binding.rbFirstMortgageYes.setTypeface(null, Typeface.BOLD)
            binding.rbFirstMortgageNo.setTypeface(null, Typeface.NORMAL)

            val fragment = FirstMortgageFragment()
            val bundle = Bundle()
            bundle.putString(AppConstant.address, "5919 TRUSSVILLE CROSSINGS PKWY")
            fragment.arguments = bundle
            findNavController().navigate(R.id.action_realestate_first_mortgage,fragment.arguments)

        }
    }

    private fun onFirstMortgegeNoClick(){
        binding.layoutFirstMortgageDetail.visibility = View.GONE
        binding.layoutSecondMortgage.visibility = View.GONE
        binding.rbFirstMortgageNo.setTypeface(null, Typeface.BOLD)
        binding.rbFirstMortgageYes.setTypeface(null, Typeface.NORMAL)
    }

    private fun onSecMortgageYesClick(){
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        binding.layoutSecMortgageDetail.visibility = View.VISIBLE
        binding.rbSecMortgageYes.setTypeface(null, Typeface.BOLD)
        binding.rbSecMortgageNo.setTypeface(null, Typeface.NORMAL)

        val fragment = SecondMortgageFragment()
        val bundle = Bundle()
        bundle.putString(AppConstant.address, "5919 TRUSSVILLE CROSSINGS PKWY")
        fragment.arguments = bundle
        findNavController().navigate(R.id.action_realestate_second_mortgage,fragment.arguments)
    }

    private fun onSecMortgegeNoClick(){
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        binding.layoutSecMortgageDetail.visibility = View.GONE
        binding.rbSecMortgageNo.setTypeface(null, Typeface.BOLD)
        binding.rbSecMortgageYes.setTypeface(null, Typeface.NORMAL)
    }


    private fun checkValidations(){

        findNavController().popBackStack()

        /*if (binding.tvOccupancyType.text.toString().isEmpty() || binding.tvOccupancyType.text.toString().length == 0) {
            CustomMaterialFields.setError(binding.layoutOccupancyType, getString(R.string.error_field_required),requireActivity())
        }
        if (binding.tvPropertyType.text.toString().isEmpty() || binding.tvPropertyType.text.toString().length == 0) {
            CustomMaterialFields.setError(binding.layoutPropertyType, getString(R.string.error_field_required),requireActivity())
        }
        if (binding.edPropertyValue.text.toString().isEmpty() || binding.edPropertyValue.text.toString().length == 0) {
            CustomMaterialFields.setError(binding.layoutPropertyValue, getString(R.string.error_field_required),requireActivity())
        }
        if (binding.edAssociationDues.text.toString().isEmpty() || binding.edAssociationDues.text.toString().length == 0) {
            CustomMaterialFields.setError(binding.layoutAssociationDues, getString(R.string.error_field_required),requireActivity())
        }
        if (binding.edPropertyTaxes.text.toString().isEmpty() || binding.edPropertyTaxes.text.toString().length == 0) {
            CustomMaterialFields.setError(binding.layoutPropertyTaxes, getString(R.string.error_field_required),requireActivity())
        }
        if (binding.edHomeownerInsurance.text.toString().isEmpty() || binding.edHomeownerInsurance.text.toString().length == 0) {
            CustomMaterialFields.setError(binding.layoutHomeownerInsurance, getString(R.string.error_field_required),requireActivity())
        }
        if (binding.edFloodInsurance.text.toString().isEmpty() || binding.edFloodInsurance.text.toString().length == 0) {
            CustomMaterialFields.setError(binding.layoutFloodInsurance, getString(R.string.error_field_required),requireActivity())
        }

        if (binding.tvOccupancyType.text.toString().isNotEmpty() || binding.tvOccupancyType.text.toString().length > 0) {
            CustomMaterialFields.clearError(binding.layoutOccupancyType,requireActivity())
        }
        if (binding.tvPropertyType.text.toString().isNotEmpty() || binding.tvPropertyType.text.toString().length > 0) {
            CustomMaterialFields.clearError(binding.layoutPropertyType,requireActivity())
        }
        if (binding.edPropertyValue.text.toString().isNotEmpty() || binding.edPropertyValue.text.toString().length > 0) {
            CustomMaterialFields.clearError(binding.layoutPropertyValue,requireActivity())
        }
        if (binding.edAssociationDues.text.toString().isNotEmpty() || binding.edAssociationDues.text.toString().length > 0) {
            CustomMaterialFields.clearError(binding.layoutAssociationDues,requireActivity())
        }
        if (binding.edPropertyTaxes.text.toString().isNotEmpty() || binding.edPropertyTaxes.text.toString().length > 0) {
            CustomMaterialFields.clearError(binding.layoutPropertyTaxes,requireActivity())
        }
        if (binding.edHomeownerInsurance.text.toString().isNotEmpty() || binding.edHomeownerInsurance.text.toString().length > 0) {
            CustomMaterialFields.clearError(binding.layoutHomeownerInsurance,requireActivity())
        }
        if (binding.edFloodInsurance.text.toString().isNotEmpty() || binding.edFloodInsurance.text.toString().length > 0) {
            CustomMaterialFields.clearError(binding.layoutFloodInsurance,requireActivity())
        } */
    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onSwipeDeleteReceivedEvent(event: SwipeToDeleteEvent) {
        if(event.boolean){

        }
    }
}