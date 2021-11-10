package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.DatePicker
import androidx.activity.addCallback
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.SubPropertyRefinanceBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.primary_borrower_info_layout.*
import kotlinx.android.synthetic.main.sub_property_refinance.*
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.delay
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject


/**
 * Created by Anita Kiran on 9/9/2021.
 */
@AndroidEntryPoint
class SubjectPropertyRefinance : BaseFragment(), DatePickerDialog.OnDateSetListener {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private val viewModel : BorrowerApplicationViewModel by activityViewModels()
    private val viewModelSubProperty : SubjectPropertyViewModel by activityViewModels()
    private lateinit var binding: SubPropertyRefinanceBinding
    private var secondMortgageList : ArrayList<SecondMortgageModel> = ArrayList()
    private var firstMortgageList : ArrayList<FirstMortgageModel> = ArrayList()
    var addressDetailList :  ArrayList<AddressData> = ArrayList()
    var addressList :  ArrayList<AddressData> = ArrayList()
    private var loanApplicationId: Int? = null
    private var propertyTypeList: ArrayList<DropDownResponse> = arrayListOf()
    private var occupancyTypeList:ArrayList<DropDownResponse> = arrayListOf()
    var firstMortgageModel =  FirstMortgageModel()
    var secondMortgageModel = SecondMortgageModel()
    var refinanceAddressData = AddressData()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View{
        binding = SubPropertyRefinanceBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)

        clicks()
        setInputFields()
        setDropDownData()

        arguments?.let { arguments ->
            loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
        }
        return binding.root
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

    private fun clicks(){

        binding.edDateOfHomePurchase.showSoftInputOnFocus = false
        binding.edDateOfHomePurchase.setOnClickListener { createCustomDialog() }
        //binding.edDateOfHomePurchase.setOnFocusChangeListener { _, _ -> createCustomDialog() }

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
                binding.layoutFirstMortgageDetail.visibility = View.VISIBLE
                binding.layoutSecondMortgage.visibility = View.VISIBLE
            }
        }

        binding.radioHasFirstMortgageYes.setOnClickListener { onFirstMortgageYes() }

        binding.layoutFirstMortgageDetail.setOnClickListener { onFirstMortgageYes() }

        // first mortgage no
        binding.radioHasFirstMortgageNo.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.radioHasFirstMortgageNo.setTypeface(null, Typeface.BOLD)
                binding.radioHasFirstMortgageYes.setTypeface(null, Typeface.NORMAL)
                binding.layoutFirstMortgageDetail.visibility = View.GONE
                binding.layoutSecondMortgage.visibility = View.GONE
            }
        }

        // sec mortgage no
        binding.rbSecMortgageNo.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.rbSecMortgageNo.setTypeface(null, Typeface.BOLD)
                binding.rbSecMortgageYes.setTypeface(null, Typeface.NORMAL)
                binding.layoutSecMortgageDetail.visibility = View.GONE
            }
        }

        binding.rbSecMortgageYes.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.rbSecMortgageNo.setTypeface(null, Typeface.NORMAL)
                binding.rbSecMortgageYes.setTypeface(null, Typeface.BOLD)
                binding.layoutSecMortgageDetail.visibility = View.VISIBLE
            }
        }

        binding.rbSecMortgageYes.setOnClickListener{ onSecMortgageYesClick() }

        binding.layoutSecMortgageDetail.setOnClickListener { onSecMortgageYesClick() }

        binding.btnSave.setOnClickListener {
            sendData()
        }

        binding.backButton.setOnClickListener {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }

        requireActivity().onBackPressedDispatcher.addCallback {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }
    }

    private fun getRefinanceDetails(){
        viewModel.refinanceDetails.observe(viewLifecycleOwner, { details->
            if(details != null){
                details.subPropertyData?.addressRefinance?.let {
                     if(it.street == null && it.unit==null && it.city==null && it.stateName==null && it.countryName==null){
                         binding.radioSubPropertyTbd.isChecked = true
                         binding.radioSubPropertyTbd.setTypeface(null,Typeface.BOLD)
                     } else {
                         binding.radioSubPropertyAddress.isChecked = true
                         binding.radioTxtPropertyAdd.setTypeface(null, Typeface.BOLD)
                         binding.tvSubPropertyAddress.visibility = View.VISIBLE
                         binding.tvSubPropertyAddress.text =
                             it.street + " " + it.unit + "\n" + it.city + " " + it.stateName + " " + it.zipCode + " " + it.countryName
                         addressDetailList.add(
                             AddressData(
                                 street = it.street,
                                 unit = it.unit,
                                 city = it.city,
                                 stateName = it.stateName,
                                 countryName = it.countryName,
                                 countyName = it.countyName,
                                 countyId = it.countyId,
                                 stateId = it.stateId,
                                 countryId = it.countryId,
                                 zipCode = it.zipCode
                             )
                         )
                     }
                } ?: run {
                    binding.radioSubPropertyTbd.isChecked = true
                    binding.radioSubPropertyTbd.setTypeface(null,Typeface.BOLD)
                }

                details.subPropertyData?.rentalIncome?.let{
                    binding.edRentalIncome.setText(Math.round(it).toString())
                    binding.layoutRentalIncome.visibility = View.VISIBLE
                    CustomMaterialFields.setColor(binding.layoutRentalIncome,R.color.grey_color_two,requireActivity())
                }
                // property id
                details.subPropertyData?.propertyTypeId?.let { selectedId ->
                    for(item in propertyTypeList) {
                        if (item.id == selectedId) {
                            binding.tvPropertyType.setText(item.name)
                            CustomMaterialFields.setColor(binding.layoutPropertyType, R.color.grey_color_two, requireActivity())
                            break
                        }
                    }
                }
                // occupancy id
                details.subPropertyData?.propertyUsageId?.let { selectedId ->

                    for(item in occupancyTypeList) {
                        if (item.id == selectedId) {
                            binding.tvOccupancyType.setText(item.name)
                            CustomMaterialFields.setColor(binding.layoutOccupancyType, R.color.grey_color_two, requireActivity())
                            break
                        }
                    }
                }
                // date of home purchased
                details.subPropertyData?.dateAcquired?.let {
                    val date = AppSetting.getMonthAndYear(it)
                    binding.edDateOfHomePurchase.setText(date)
                    CustomMaterialFields.setColor(binding.layoutDateOfHomepurchase,R.color.grey_color_two,requireActivity())
                }
                // property value
                details.subPropertyData?.propertyValue?.let { value ->
                    binding.edPropertyValue.setText(Math.round(value).toString())
                    CustomMaterialFields.setColor(binding.layoutPropertyValue,R.color.grey_color_two,requireActivity())
                }
                // hoa dues
                details.subPropertyData?.hoaDues?.let { value ->
                    binding.edAssociation.setText(Math.round(value).toString())
                    CustomMaterialFields.setColor(binding.layoutAssociationDues,R.color.grey_color_two,requireActivity())
                }
                // property tax
                details.subPropertyData?.propertyTax?.let { value ->
                    binding.edPropertyTaxes.setText(Math.round(value).toString())
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
                            binding.mixedPropertyDesc.setText(desc)
                        }
                    } else
                        binding.radioMixedPropertyNo.isChecked = true
                } ?: run {
                    //binding.radioMixedPropertyNo.isChecked = true
                }

                // has first mortgage 'yes'
                details.subPropertyData?.hasFirstMortgage?.let{ yes->
                    if(yes){
                        binding.radioHasFirstMortgageYes.isChecked = true
                       // binding.layoutFirstMortgageDetail.visibility =View.VISIBLE
                       // binding.layoutSecondMortgage.visibility = View.VISIBLE

                        details.subPropertyData.firstMortgageModel?.let{ model->

                            firstMortgageList.add(FirstMortgageModel(model.propertyTaxesIncludeinPayment, model.homeOwnerInsuranceIncludeinPayment,model.floodInsuranceIncludeinPayment,
                                model.paidAtClosing,model.firstMortgagePayment,model.unpaidFirstMortgagePayment,model.helocCreditLimit,model.isHeloc))

                            model.firstMortgagePayment?.let { payment->
                                binding.firstMortgagePayment.setText("$" + Math.round(payment))
                            } ?: run {
                                binding.firstMortgagePayment.setText("$0")
                            }

                            model.unpaidFirstMortgagePayment?.let{ balance ->
                                binding.firstMortgageBalance.setText("$" + Math.round(balance))
                            } ?: run{
                                binding.firstMortgageBalance.setText("$0")
                            }
                        }
                    }  else binding.radioHasFirstMortgageNo.isChecked = true

                } ?: run{
                    //binding.radioHasFirstMortgageNo.isChecked = true
                }

                // has second mortgage 'yes'
                details.subPropertyData?.hasSecondMortgage?.let{ yes->
                    if(yes){
                        binding.rbSecMortgageYes.isChecked = true
                        binding.layoutSecMortgageDetail.visibility =View.VISIBLE

                        details.subPropertyData.secondMortgageModel?.let{ model->

                            secondMortgageList.add(SecondMortgageModel(model.secondMortgagePayment, model.unpaidSecondMortgagePayment,model.helocCreditLimit,model.isHeloc,
                                model.combineWithNewFirstMortgage,model.wasSmTaken))

                            model.secondMortgagePayment?.let { payment->
                                binding.textviewSecMortgagePayment.setText("$" + payment)
                            } ?: run {
                                binding.textviewSecMortgagePayment.setText("$0")
                            }

                            model.unpaidSecondMortgagePayment?.let{ balance ->
                                binding.textviewSecMortgageBalance.setText("$" + balance)
                            } ?: run{
                                binding.textviewSecMortgageBalance.setText("$0")
                            }
                        }
                    } else  binding.radioHasFirstMortgageNo.isChecked = true
                } ?: run{
                    //binding.radioHasFirstMortgageNo.isChecked = true
                }

                //getDropDownData()
                if(details.code.equals(AppConstant.RESPONSE_CODE_SUCCESS)){
                    hideLoader()
                }
            }
            hideLoader()
        })
    }

    private fun sendData(){
        // TBD
        val tbd = if(binding.radioSubPropertyTbd.isChecked) true else false
        // get property id
        val property1 : String = binding.tvPropertyType.getText().toString().trim()
        val matchedList1 =  occupancyTypeList.filter { s -> s.name == property1}
        //val propertyId = if(matchedList1.size > 0) matchedList1.map { matchedList1.get(0).id }.single() else null
        // get occupancy id
        val occupancy : String = binding.tvOccupancyType.getText().toString().trim()
        val matchedList =  occupancyTypeList.filter { s -> s.name == occupancy}
       // val occupancyId = if(matchedList.size>0) matchedList.map { matchedList.get(0).id }.single() else null

        // mixed use property
        val isMixedUseProperty = if(binding.radioMixedPropertyYes.isChecked) true else false
        // desc
        val mixedUsePropertyDesc = if(binding.mixedPropertyDesc.text.toString().trim().length > 0) binding.mixedPropertyDesc.text.toString() else null
        // property value
        val propertyValue = binding.edPropertyValue.text.toString().trim()
        var newPropertyValue = if(propertyValue.length > 0) propertyValue.replace(",".toRegex(), "") else null

        val hoa = binding.edAssociation.text.toString().trim()
        val newHoaDues = if(hoa.length > 0) hoa.replace(",".toRegex(), "") else null

        val propertyTax = binding.edPropertyTaxes.text.toString().trim()
        val newPropertyTax = if(propertyTax.length > 0) propertyTax.replace(",".toRegex(), "") else null

        val datePurchased = if(binding.edDateOfHomePurchase.text.toString().trim().length > 0) binding.edDateOfHomePurchase.text.toString() else null

        // home insurance
        val homeInsurance = binding.edHomeownerInsurance.text.toString().trim()
        var newHomeInsurance = if(homeInsurance.length > 0) homeInsurance.replace(",".toRegex(), "") else null
        // flood insurance
        val floodInsurance = binding.edFloodInsurance.text.toString().trim()
        val newFloodInsurance = if(floodInsurance.length >0 ) floodInsurance.replace(",".toRegex(), "") else null

        val hasFirstMortgage = if(binding.radioHasFirstMortgageYes.isChecked) true else false
        val hasSecondMortgage = if(binding.rbSecMortgageYes.isChecked) true else false

        lifecycleScope.launchWhenStarted{
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                val activity = (activity as? SubjectPropertyActivity)
                loanApplicationId = activity?.loanApplicationId
                loanApplicationId?.let { loanId->
                    //Log.e("Loan Application Id", ""+ loanId)

                        //Log.e("address list before add api", ""+ refinanceAddressData)
                        //Log.e("first Mortgage model before add api", ""+ firstMortgageModel)

                        //Log.e("sec Mortgage model before add api", ""+ secondMortgageModel)


                        val refinanceDetail = SubPropertyRefinanceData(loanApplicationId = loanId,propertyTypeId = 1,propertyUsageId = 4,
                            propertyValue = newPropertyValue?.toDouble(),propertyTax = newPropertyTax?.toDouble(),homeOwnerInsurance=newHomeInsurance?.toDouble(),
                            floodInsurance = newFloodInsurance?.toDouble(), hoaDues=newHoaDues?.toDouble(),dateAcquired = datePurchased, hasFirstMortgage = hasFirstMortgage,
                        hasSecondMortgage = hasSecondMortgage,isSameAsPropertyAddress = true,
                        addressRefinance = refinanceAddressData,isMixedUseProperty= isMixedUseProperty,mixedUsePropertyExplanation=mixedUsePropertyDesc,subjectPropertyTbd = tbd,
                        cashOutAmount = 0.0,firstMortgageModel = firstMortgageModel,secondMortgageModel =secondMortgageModel,loanAmount = 2000.0,loanGoalId = 1,rentalIncome = 400.0,propertyInfoId = null)

                        showLoader()
                        viewModelSubProperty.sendRefinanceDetail(authToken,refinanceDetail)

                }
            }
        }
    }

    private fun setCoBorrowerOccupancyStatus(){
        viewModel.coBorrowerOccupancyStatus.observe(viewLifecycleOwner, {
            if(it.occupancyData != null  && it.occupancyData.size > 0){
                binding.rbOccupying.isChecked = true
                binding.rbOccupying.setTypeface(null, Typeface.BOLD)
                binding.coBorrowerName.setText(it.occupancyData.get(0).borrowerFirstName + " " + it.occupancyData.get(0).borrowerLastName)
            }
            else {
                //binding.rbNonOccupying.isChecked = true
                //binding.rbNonOccupying.setTypeface(null, Typeface.BOLD)
                binding.coBorrowerName.visibility = View.GONE
            }
        })

        getRefinanceDetails()
    }

    private fun setInputFields() {

        // set lable focus
        binding.edRentalIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edRentalIncome, binding.layoutRentalIncome, requireContext()))
        binding.edPropertyValue.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edPropertyValue, binding.layoutPropertyValue, requireContext()))
        binding.edAssociation.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edAssociation, binding.layoutAssociationDues, requireContext()))
        binding.edPropertyTaxes.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edPropertyTaxes, binding.layoutPropertyTaxes, requireContext()))
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
                requireContext()))

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

    private fun openAddress(){
        val fragment = SubPropertyAddressFragment()
        val bundle = Bundle()
        bundle.putParcelableArrayList(AppConstant.address,addressDetailList)
        fragment.arguments = bundle
        findNavController().navigate(R.id.action_address, fragment.arguments)
    }

    override fun onResume() {
        super.onResume()
        viewModelSubProperty.updatedRefinanceAddress.observe(viewLifecycleOwner, {
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

            refinanceAddressData = it

            binding.tvSubPropertyAddress.text =
                it.street + " " + it.unit + "\n" + it.city + " " + it.stateName + " " + it.zipCode + " " + it.countryName
        })

        viewModelSubProperty.mixedPropertyRefinanceDesc.observe(viewLifecycleOwner,{
            binding.radioMixedPropertyYes.isChecked = true
            binding.mixedPropertyDesc.setText(it)
            binding.layoutMixedPropertyDetail.visibility = View.VISIBLE
        })


        viewModelSubProperty.firstMortgageDetail.observe(viewLifecycleOwner,{
            if(it !=null ) {
                binding.radioHasFirstMortgageYes.isChecked = true
                it.firstMortgagePayment?.let { payment ->
                    binding.firstMortgagePayment.setText("$" + Math.round(payment))
                } ?: run {
                    binding.firstMortgagePayment.setText("$0")
                }

                it.unpaidFirstMortgagePayment?.let { balance ->
                    binding.firstMortgageBalance.setText("$" + Math.round(balance))
                } ?: run {
                    binding.firstMortgageBalance.setText("$0")
                }
                firstMortgageModel = it
                Log.e("onResume", "First: --"+firstMortgageModel)
            }
           })

        binding.rbSecMortgageYes.isChecked = true
        binding.layoutSecMortgageDetail.visibility =View.VISIBLE

        viewModelSubProperty.secMortgageDetail.observe(viewLifecycleOwner,{
            it.secondMortgagePayment?.let { payment->
                binding.textviewSecMortgagePayment.setText("$" + payment)
            } ?: run {
                binding.textviewSecMortgagePayment.setText("$0")
            }

            it.unpaidSecondMortgagePayment?.let{ balance ->
                binding.textviewSecMortgageBalance.setText("$" + balance)
            } ?: run{
                binding.textviewSecMortgageBalance.setText("$0")
            }
            secondMortgageModel =it
            Log.e("onResume", "Sec:--"+secondMortgageModel)

        })
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

    private fun onFirstMortgageYes() {
        binding.layoutFirstMortgageDetail.visibility = View.VISIBLE
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        val fragment = FirstMortgageFragment()
        val bundle = Bundle()
        bundle.putParcelableArrayList(AppConstant.firstMortgage,firstMortgageList)
        fragment.arguments = bundle
        findNavController().navigate(R.id.action_refinance_first_mortgage, fragment.arguments)
    }

    private fun onSecMortgageYesClick() {
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        binding.layoutSecMortgageDetail.visibility = View.VISIBLE
        binding.rbSecMortgageYes.setTypeface(null, Typeface.BOLD)
        binding.rbSecMortgageNo.setTypeface(null, Typeface.NORMAL)
        val fragment = SecondMortgageFragment()
        val bundle = Bundle()
        bundle.putParcelableArrayList(AppConstant.secMortgage,secondMortgageList)
        fragment.arguments = bundle
        findNavController().navigate(R.id.action_refinance_sec_mortgage, fragment.arguments)
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
            SandbarUtils.showError(requireActivity(), "Data Sent Successfully" )

        else if(event.addUpdateDataResponse.code == AppConstant.INTERNET_ERR_CODE)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )

        else
            if(event.addUpdateDataResponse.message != null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )
        hideLoader()

    }

    /*
    private fun onSecMortgegeNoClick() {
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        binding.layoutSecMortgageDetail.visibility = View.GONE
        binding.rbSecMortgageNo.setTypeface(null, Typeface.BOLD)
        binding.rbSecMortgageYes.setTypeface(null, Typeface.NORMAL)
    } */

    /*private fun mixedPropertyDetailClick() {
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
  } */


    /*
      private fun getDropDownData(){
        //lifecycleScope.launchWhenStarted {
        //  viewModel.getPropertyTypes(token)
        viewModel.propertyType.observe(viewLifecycleOwner, {
            //if(it != null && it.size > 0) {

            val itemList:ArrayList<String> = arrayListOf()
            for(item in it){
                itemList.add(item.name)
                if(propertyTypeId == item.id){
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
                    showHideRental()
                }
            }
            //}
        })
        // }

        // occupancy Type spinner
        //lifecycleScope.launchWhenStarted {
        // viewModel.getOccupancyType(token)
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
                        showHideRental()
                    }
                }
            }

        })
        // }
    }
     */

}