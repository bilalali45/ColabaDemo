package com.rnsoft.colabademo

import android.app.Activity
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
import kotlinx.android.synthetic.main.detail_list_layout.*
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber

/**
 * Created by Anita Kiran on 9/8/2021.
 */
@AndroidEntryPoint
class SubjectPropertyPurchase : BaseFragment() {

    private lateinit var binding: SubjectPropertyPurchaseBinding
    private var savedViewInstance: View? = null
    private val viewModel : BorrowerApplicationViewModel by activityViewModels()
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
            postponeEnterTransition()

            savedViewInstance = binding.root

            super.addListeners(binding.root)

            //setupUI()
            //setInputFields()
            //getPurchaseDetails()

            savedViewInstance
        }
    }

     fun setupUI(){

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
           dismissActivity()
        }
        // back
        requireActivity().onBackPressedDispatcher.addCallback {
            dismissActivity()
        }

        binding.btnSave.setOnClickListener {
            dismissActivity()
        }

        binding.subpropertyParentLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(),binding.subpropertyParentLayout)
            super.removeFocusFromAllFields(binding.subpropertyParentLayout)
        }
    }

    private fun getPurchaseDetails(){
            viewModel.subjectPropertyDetails.observe(viewLifecycleOwner, { details ->
                if(details != null){
                    details.subPropertyData?.address?.let {
                        if(it.street == null && it.unit==null && it.city==null && it.stateName==null && it.countryName==null){
                            binding.radioSubPropertyTbd.isChecked = true
                            binding.radioSubPropertyTbd.setTypeface(null,Typeface.BOLD)
                        } else {
                            binding.radioSubPropertyAddress.isChecked = true
                            binding.radioTxtPropertyAdd.setTypeface(null, Typeface.BOLD)
                            binding.tvSubPropertyAddress.visibility = View.VISIBLE
                            addressList.add(AddressData(
                                    street = it.street,
                                    unit = it.unit,
                                    city = it.city,
                                    stateName = it.stateName,
                                    countryName = it.countryName,
                                    countyName = it.countyName,
                                    countyId = it.countyId,
                                    stateId = it.stateId,
                                    countryId = it.countryId,
                                    zipCode = it.zipCode))
                            binding.tvSubPropertyAddress.text =
                                it.street + " " + it.unit + "\n" + it.city + " " + it.stateName + " " + it.zipCode + " " + it.countryName
                        }

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
                        //binding.radioMixedPropertyNo.isChecked = true
                    }
                    setDropDownData()
                    setCoBorrowerOccupancyStatus()
                    hideLoader()
                }
                hideLoader()
            })
    }

    private fun setDropDownData(){
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
                   // binding.tvPropertyType.freezesText = false
                    adapter.setNotifyOnChange(true)

//                    binding.tvPropertyType.setOnFocusChangeListener { _, _ ->
//                        binding.tvPropertyType.showDropDown()
//                    }
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
       // }

        // occupancy Type spinner
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
        viewModel.coBorrowerOccupancyStatus.observe(viewLifecycleOwner, {
            if(it.occupancyData != null  && it.occupancyData.size > 0){
                binding.radioOccupying.isChecked = true
                binding.coBorrowerName.setText(it.occupancyData.get(0).borrowerFirstName + " " + it.occupancyData.get(0).borrowerLastName)
            } else{
                //binding.radioNonOccupying.isChecked = true
                binding.coBorrowerName.visibility = View.GONE
            }
        })
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

    private fun hideLoader(){
        val  activity = (activity as? SubjectPropertyActivity)
        activity?.binding?.loaderSubjectProperty?.visibility = View.GONE
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
    fun onErrorReceived(event: WebServiceErrorEvent) {
        if(event.isInternetError)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )
        else
            if(event.errorResult!=null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )
        hideLoader()
    }

    private fun dismissActivity(){
        requireActivity().finish()
        requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
    }

}