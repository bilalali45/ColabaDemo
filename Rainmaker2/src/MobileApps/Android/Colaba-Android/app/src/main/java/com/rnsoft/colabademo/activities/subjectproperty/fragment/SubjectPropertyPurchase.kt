package com.rnsoft.colabademo

import android.content.SharedPreferences
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
import kotlinx.android.synthetic.main.realstate_horizontal.*
import kotlinx.coroutines.coroutineScope
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject

/**
 * Created by Anita Kiran on 9/8/2021.
 */
@AndroidEntryPoint
class SubjectPropertyPurchase : BaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: SubjectPropertyPurchaseBinding
    private var savedViewInstance: View? = null
    private val viewModel : BorrowerApplicationViewModel by activityViewModels()
    private val viewModelSubProperty : SubjectPropertyViewModel by activityViewModels()
    var addressList :  ArrayList<AddressData> = ArrayList()
    private var loanApplicationId: Int? = null
    private var propertyTypeList: ArrayList<DropDownResponse> = arrayListOf()
    private var occupancyTypeList:ArrayList<DropDownResponse> = arrayListOf()


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

            ApplicationClass.globalAddressList.clear()

            arguments?.let { arguments ->
                loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
            }

            setupUI()
            setInputFields()
            setDropDownData()

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
            checkValidations()
        }

        binding.subpropertyParentLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(),binding.subpropertyParentLayout)
            super.removeFocusFromAllFields(binding.subpropertyParentLayout)
        }
    }

    override fun onResume() {
        super.onResume()
        viewModelSubProperty.updatedAddress.observe(viewLifecycleOwner, {
            //addressList.clear()
            /*addressList.add(AddressData(street = it.street, unit = it.unit, city = it.city,
                stateName = it.stateName,
                countryName = it.countryName,
                countyName = it.countyName,
                countyId = it.countyId,
                stateId = it.stateId,
                countryId = it.countryId,
                zipCode = it.zipCode)) */

            binding.tvSubPropertyAddress.text =
                it.street + " " + it.unit + "\n" + it.city + " " + it.stateName + " " + it.zipCode + " " + it.countryName

            EventBus.getDefault().post(AddressUpdateEvent(it))
        })

        viewModelSubProperty.mixedPropertyDesc.observe(viewLifecycleOwner,{
            binding.radioMixedPropertyYes.isChecked = true
            binding.mixedPropertyExplanation.setText(it)
            binding.layoutMixedPropertyDetail.visibility = View.VISIBLE
        })
    }

    private fun getPurchaseDetails(){
        viewModel.subjectPropertyDetails.observe(viewLifecycleOwner, { details ->
            if(details != null){
                details.subPropertyData?.addressData?.let {
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
                    details.subPropertyData?.propertyTypeId?.let { selectedId ->
                        for(item in propertyTypeList) {
                            if (item.id == selectedId) {
                                binding.tvPropertyType.setText(item.name, false)
                                CustomMaterialFields.setColor(binding.layoutPropertyType, R.color.grey_color_two, requireActivity())
                                break
                            }
                        }
                    }
                    // occupancy id
                    details.subPropertyData?.occupancyTypeId?.let { selectedId ->
                        for(item in occupancyTypeList) {
                            if (item.id == selectedId) {
                                binding.tvOccupancyType.setText(item.name,false)
                                CustomMaterialFields.setColor(binding.layoutOccupancyType, R.color.grey_color_two, requireActivity())
                                break
                            }
                        }
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
                                binding.layoutMixedPropertyDetail.visibility = View.VISIBLE
                            }
                        }
                        else
                            binding.radioMixedPropertyNo.isChecked = true
                    } ?: run {
                        //binding.radioMixedPropertyNo.isChecked = true
                    }
                    if(details.code.equals(AppConstant.RESPONSE_CODE_SUCCESS)){
                       hideLoader() }
                }
                hideLoader()
            })
    }

    private fun setDropDownData(){
        lifecycleScope.launchWhenStarted {
            coroutineScope {
                viewModel.propertyType.observe(viewLifecycleOwner, { properties->
                    if (properties != null && properties.size > 0) {
                        val itemList: ArrayList<String> = arrayListOf()
                        propertyTypeList = arrayListOf()
                        for (item in properties) {
                            itemList.add(item.name)
                            propertyTypeList.add(item)
                        }


                        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, itemList)
                        binding.tvPropertyType.setAdapter(adapter)
                        //adapter.setNotifyOnChange(true)

//                    binding.tvPropertyType.setOnFocusChangeListener { _, _ ->
//                        binding.tvPropertyType.showDropDown()
//                    }
                        binding.tvPropertyType.setOnClickListener {
                            binding.tvPropertyType.showDropDown()
                        }

                        binding.tvPropertyType.onItemClickListener = object :
                            AdapterView.OnItemClickListener {
                            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                                CustomMaterialFields.setColor(binding.layoutPropertyType, R.color.grey_color_two, requireActivity())
                            }
                        }
                    }
                })

                // occupancy Type spinner
                viewModel.occupancyType.observe(viewLifecycleOwner, { occupancies ->

                    if (occupancies != null && occupancies.size > 0) {
                        val itemList: ArrayList<String> = arrayListOf()
                        for (item in occupancies) {
                            itemList.add(item.name)
                            occupancyTypeList.add(item)

                            /*if(occupancyTypeId > 0 && occupancyTypeId == item.id){
                            binding.tvOccupancyType.setText(item.name)
                            CustomMaterialFields.setColor(binding.layoutOccupancyType,R.color.grey_color_two,requireActivity())
                        } */
                        }

                        val adapterOccupanycyType = ArrayAdapter(
                            requireContext(),
                            android.R.layout.simple_list_item_1,
                            itemList
                        )
                        binding.tvOccupancyType.setAdapter(adapterOccupanycyType)
//                    binding.tvOccupancyType.setOnFocusChangeListener { _, _ ->
//                        binding.tvOccupancyType.showDropDown()
//                    }
                        binding.tvOccupancyType.setOnClickListener {
                            binding.tvOccupancyType.showDropDown()
                        }

                        binding.tvOccupancyType.onItemClickListener =
                            object : AdapterView.OnItemClickListener {
                                override fun onItemClick(
                                    p0: AdapterView<*>?,
                                    p1: View?,
                                    position: Int,
                                    id: Long
                                ) {
                                    CustomMaterialFields.setColor(
                                        binding.layoutOccupancyType,
                                        R.color.grey_color_two,
                                        requireActivity()
                                    )
                                }
                            }
                    }
                })

                setCoBorrowerOccupancyStatus()
            }
        }
    }

    private fun setCoBorrowerOccupancyStatus(){
        //Log.e("coBorrower","true")
        lifecycleScope.launchWhenStarted {
            viewModel.coBorrowerOccupancyStatus.observe(viewLifecycleOwner, {
                if (it.occupancyData != null && it.occupancyData.size > 0) {
                    binding.radioOccupying.isChecked = true
                    binding.coBorrowerName.setText(
                        it.occupancyData.get(0).borrowerFirstName + " " + it.occupancyData.get(0).borrowerLastName
                    )
                } else {
                    //binding.radioNonOccupying.isChecked = true
                    binding.coBorrowerName.visibility = View.GONE
                }
            })
        }
        getPurchaseDetails()

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

    private fun checkValidations(){
        // TBD
        val tbd = if(binding.radioSubPropertyTbd.isChecked) true else false
        if(!binding.radioSubPropertyAddress.isChecked){
            addressList.clear()
        }

        // get property id
        val property : String = binding.tvPropertyType.getText().toString().trim()
        val matchedList1 =  occupancyTypeList.filter { s -> s.name == property}
        val propertyId = if(matchedList1.size > 0) matchedList1.map { matchedList1.get(0).id }.single() else null
        // get occupancy id
        val occupancy : String = binding.tvOccupancyType.getText().toString().trim()
        val matchedList =  occupancyTypeList.filter { s -> s.name == occupancy}
        val occupancyId = if(matchedList.size>0) matchedList.map { matchedList.get(0).id }.single() else null

        // mixed use property
        val isMixedUseProperty = if(binding.radioMixedPropertyYes.isChecked) true else false
        // desc
        val mixedUsePropertyDesc = if(binding.mixedPropertyExplanation.text.toString().trim().length > 0) binding.mixedPropertyExplanation.text.toString() else null
        // appraised value
        val appraisedValue = binding.edAppraisedPropertyValue.text.toString().trim()
        var newAppraisedValue = if(appraisedValue.length > 0) appraisedValue.replace(",".toRegex(), "") else null

        // property tax
        val propertyTax = binding.edPropertyTax.text.toString().trim()    //.length > 0)  binding.edPropertyTax.text.toString() else null
        var newPropertyTax = if(propertyTax.length > 0) appraisedValue.replace(",".toRegex(), "") else null
        // home insurance
        val homeInsurance = binding.edHomeownerInsurance.text.toString().trim()
        var newHomeInsurance = if(homeInsurance.length > 0) homeInsurance.replace(",".toRegex(), "") else null
        // flood insurance
        val floodInsurance = binding.edFloodInsurance.text.toString().trim()
        val newFloodInsurance = if(floodInsurance.length >0 ) floodInsurance.replace(",".toRegex(), "") else null


        lifecycleScope.launchWhenStarted{
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                val activity = (activity as? SubjectPropertyActivity)
                loanApplicationId = activity?.loanApplicationId
                loanApplicationId?.let {
                    Log.e("Loan Application Id", ""+ it)
                    if(addressList !=null && addressList.size > 0){
                        val address = AddressData(
                            city = addressList.get(0).city,
                            countryName = addressList.get(0).countryName,
                            stateId = addressList.get(0).stateId,
                            stateName = addressList.get(0).stateName,
                            unit = addressList.get(0).unit,
                            zipCode = addressList.get(0).zipCode,
                            street = addressList.get(0).street,
                            countyId = addressList.get(0).countyId,
                            countyName = addressList.get(0).countyName,
                            countryId = addressList.get(0).countryId)

                        Log.e("address list before add api", ""+ address)

                        val propertyData = SubPropertyData(loanApplicationId = it,propertyTypeId = 1,occupancyTypeId = occupancyId,
                            appraisedPropertyValue = newAppraisedValue?.toDouble(),propertyTax = newPropertyTax?.toDouble(),homeOwnerInsurance=newHomeInsurance?.toDouble(),
                            floodInsurance = newFloodInsurance?.toDouble(),
                            addressData = address ,isMixedUseProperty= isMixedUseProperty,mixedUsePropertyExplanation=mixedUsePropertyDesc,subjectPropertyTbd = tbd)
                        showLoader()
                        viewModelSubProperty.sendSubjectPropertyDetail(authToken,propertyData)
                    }
                }
            }
        }
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

    private fun showLoader(){
        val  activity = (activity as? SubjectPropertyActivity)
        activity?.binding?.loaderSubjectProperty?.visibility = View.VISIBLE
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
    fun onSentData(event: SendDataEvent) {
        if(event.addUpdateDataResponse.code == AppConstant.RESPONSE_CODE_SUCCESS)
            dismissActivity()

        else if(event.addUpdateDataResponse.code == AppConstant.INTERNET_ERR_CODE)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )

        else
            if(event.addUpdateDataResponse.message != null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )
        hideLoader()

    }

    private fun dismissActivity(){
        requireActivity().finish()
        requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
    }

}
/*
// filter map syntax
// one line
var propertyId =  propertyTypeList.filter { s -> s.name == property}.map{it.id}.single()
// 2 lines
val matchedList =  occupancyTypeList.filter { s -> s.name == occupancy}
val id = matchedList.map { matchedList.get(0).id }.single()
*/
