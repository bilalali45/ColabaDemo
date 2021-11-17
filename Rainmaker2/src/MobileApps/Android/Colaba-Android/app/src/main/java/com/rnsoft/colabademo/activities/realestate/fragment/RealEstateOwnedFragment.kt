package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.activities.addresses.info.fragment.DeleteCurrentResidenceDialogFragment
import com.rnsoft.colabademo.activities.addresses.info.fragment.SwipeToDeleteEvent
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.RealEstateOwnedLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.util.*
import javax.inject.Inject


/**
 * Created by Anita Kiran on 9/16/2021.
 */
@AndroidEntryPoint
class RealEstateOwnedFragment : BaseFragment(), View.OnClickListener {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: RealEstateOwnedLayoutBinding
    private lateinit var toolbar: AppHeaderWithCrossDeleteBinding
    private val viewModel : RealEstateViewModel by activityViewModels()
    private var savedViewInstance: View? = null
    private var propertyTypeId : Int = 0
    private var occupancyTypeId : Int = 0
    //var addressList : ArrayList<RealEstateAddress> = ArrayList()
    var realEstateAddress = AddressData()
    var addressHeading: String? = null
    var firstMortgageModel = FirstMortgageModel()
    var secondMortgageModel = SecondMortgageModel()
    private var propertyTypeList: ArrayList<DropDownResponse> = arrayListOf()
    private var occupancyTypeList:ArrayList<DropDownResponse> = arrayListOf()
    private var propertyStatusList:ArrayList<DropDownResponse> = arrayListOf()
    var propertyInfoId: Int? = null
    var borrowerId: Int? = null
    var borrowerPropertyId :Int? = null
    private var loanApplicationId: Int? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = RealEstateOwnedLayoutBinding.inflate(inflater, container, false)
        toolbar = binding.headerRealestate
         // savedViewInstance = binding.root
        super.addListeners(binding.root)

        // set Header title
        toolbar.toolbarTitle.setText(getString(R.string.real_estate_owned))


        val activity = (activity as? RealEstateActivity)

        activity?.loanApplicationId?.let { loanId -> loanApplicationId = loanId }

        activity?.borrowerPropertyId?.let { borrowerPropertyId = it }

        activity?.borrowerId?.let { borrowerId = it }

        activity?.propertyInfoId?.let { propertyInfoId = it }

        Log.e("realEstateIds-onCreate","Loan Application Id" + loanApplicationId + " borrowerPropertyId" + borrowerPropertyId+ " borrowerId" + borrowerId + " propertyInfoID " + propertyInfoId)


        if(borrowerPropertyId ==null || borrowerPropertyId ==0){
            Log.e("borrowerPropertyId", ""+ borrowerPropertyId)
            toolbar.btnTopDelete.visibility = View.GONE
        }

        else if (loanApplicationId != null && borrowerId != null) {
            Log.e("loanApplicationId: ", ""+ loanApplicationId + " borrwerId:" + borrowerId)
            toolbar.btnTopDelete.visibility = View.VISIBLE
            toolbar.btnTopDelete.setOnClickListener {
                DeleteIncomeDialogFragment.newInstance(AppConstant.income_delete_text).show(childFragmentManager,
                DeleteCurrentResidenceDialogFragment::class.java.canonicalName)
            }
        }

            initViews()
            getRealEstateDetails()

            findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<FirstMortgageModel>(AppConstant.firstMortgage)?.observe(
                viewLifecycleOwner) { result ->
                firstMortgageModel = result
                Log.e("first mor receivied",""+ result)
            }

            findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<SecondMortgageModel>(AppConstant.secMortgage)?.observe(
                viewLifecycleOwner) { result ->
                secondMortgageModel = result
                Log.e("sec mor receivied",""+ result)
            }

            findNavController().currentBackStackEntry?.savedStateHandle?.getLiveData<AddressData>(AppConstant.address)?.observe(
                viewLifecycleOwner) { result ->
                realEstateAddress = result
                Log.e("address receivied",""+ result)
            }

           return binding.root // savedViewInstance

    }

    private fun getRealEstateDetails() {
            viewModel.realEstateDetails.observe(viewLifecycleOwner, {
                if(it != null) {
                    it.data?.address?.let {
                        binding.tvPropertyAddress.text = it.street+" "+it.unit+"\n"+it.city+" "+it.stateName+" "+it.zipCode+" "+it.countryName
                        addressHeading = it.street
                        realEstateAddress = it

                        } ?: run {}

                    it.data?.rentalIncome?.let{
                            binding.edRentalIncome.setText(it.toString())
                            binding.layoutRentalIncome.visibility = View.VISIBLE
                            CustomMaterialFields.setColor(binding.layoutRentalIncome,R.color.grey_color_two,requireActivity())
                        }
                        // property id
                        it.data?.propertyTypeId?.let { id ->
                            propertyTypeId = id
                        }
                        // occupancy id
                        it.data?.occupancyTypeId?.let { id ->
                            occupancyTypeId = id
                        }
                        // property Status
                        it.data?.propertyStatus?.let { id ->
                            for(item in propertyStatusList) {
                                if (item.id == id) {
                                    binding.tvPropertyStatus.setText(item.name, false)
                                    CustomMaterialFields.setColor(binding.layoutPropertyStatus,R.color.grey_color_two,requireActivity())
                                    break
                                }
                            }
                        }

                        // hoa dues
                        it.data?.hoaDues?.let { value ->
                            binding.edAssociationDues.setText(Math.round(value).toString())
                            CustomMaterialFields.setColor(binding.layoutAssociationDues,R.color.grey_color_two,requireActivity())
                        }
                        // property value
                        it.data?.propertyValue?.let { value ->
                            binding.edPropertyValue.setText(Math.round(value).toString())
                            CustomMaterialFields.setColor(binding.layoutPropertyValue,R.color.grey_color_two,requireActivity())
                        }
                        // property tax
                        it.data?.annualPropertyTax?.let { value ->
                            binding.edPropertyTax.setText(Math.round(value).toString())
                            CustomMaterialFields.setColor(binding.layoutPropertyTaxes,R.color.grey_color_two,requireActivity())
                        }
                        // home owner insurance
                        it.data?.annualHomeInsurance?.let { value ->
                            binding.edHomeownerInsurance.setText(Math.round(value).toString())
                            CustomMaterialFields.setColor(binding.layoutHomeownerInsurance, R.color.grey_color_two, requireActivity())
                        }
                        //  flood insurance
                        it.data?.annualFloodInsurance?.let { value ->
                            binding.edFloodInsurance.setText(Math.round(value).toString())
                            CustomMaterialFields.setColor(binding.layoutFloodInsurance,R.color.grey_color_two,requireActivity())
                        }

                    Log.e("firstMortage",""+ it.data?.firstMortgageModel)
                        // has first mortgage 'yes'
                        if(it.data?.hasFirstMortgage !=null){
                            if(it.data.hasFirstMortgage){
                                binding.rbFirstMortgageYes.isChecked = true
                                binding.layoutFirstMortgageDetail.visibility =View.VISIBLE
                                binding.layoutSecondMortgage.visibility = View.VISIBLE

                                it.data.firstMortgageModel?.let{ model->
                                    firstMortgageModel = model
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
                            }
                        } else {
                            binding.rbFirstMortgageNo.isChecked = true }


                        // has second mortgage 'yes'
                        if(it.data?.hasSecondMortgage !=null){
                            if(it.data.hasSecondMortgage) {
                                binding.rbSecMortgageYes.isChecked = true
                                binding.layoutSecMortgageDetail.visibility = View.VISIBLE

                                it.data.secondMortgageModel?.let { model ->
                                    secondMortgageModel = model
                                    model.secondMortgagePayment?.let { payment->
                                        binding.secMortgagePayment.setText("$" + Math.round(payment))
                                    } ?: run { binding.secMortgagePayment.setText("$0") }

                                    model.unpaidSecondMortgagePayment?.let { balance->
                                        binding.secMortgageBalance.setText("$" + Math.round(balance))
                                    } ?: run {binding.secMortgageBalance.setText("$0)")}
                                }
                            }
                        } else {
                            binding.rbSecMortgageNo.isChecked = true
                        }

                    setDropDownData()
                    if(it.code.equals(AppConstant.RESPONSE_CODE_SUCCESS)){
                        hideLoader()
                    }
                }
                hideLoader()
            })





    }

    private fun hideLoader(){
        val  activity = (activity as? RealEstateActivity)
        activity?.binding?.loaderRealEstate?.visibility = View.GONE
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
            R.id.realestate_mainlayout -> {
                HideSoftkeyboard.hide(requireActivity(),binding.realestateMainlayout)
                super.removeFocusFromAllFields(binding.realestateMainlayout)
            }
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
        binding.edPropertyTax.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edPropertyTax, binding.layoutPropertyTaxes, requireContext()))
        binding.edHomeownerInsurance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edHomeownerInsurance, binding.layoutHomeownerInsurance, requireContext()))
        binding.edFloodInsurance.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edFloodInsurance, binding.layoutFloodInsurance, requireContext()))

        // set input format
        binding.edRentalIncome.addTextChangedListener(NumberTextFormat(binding.edRentalIncome))
        binding.edAssociationDues.addTextChangedListener(NumberTextFormat(binding.edAssociationDues))
        binding.edPropertyValue.addTextChangedListener(NumberTextFormat(binding.edPropertyValue))
        binding.edPropertyTax.addTextChangedListener(NumberTextFormat(binding.edPropertyTax))
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

    private fun setDropDownData(){
             viewModel.propertyType.observe(viewLifecycleOwner, {
                 val itemList: ArrayList<String> = arrayListOf()
                 for (item in it) {
                     itemList.add(item.name)
                     if (propertyTypeId == item.id) {
                         binding.tvPropertyType.setText(item.name)
                         propertyTypeList.add(item)
                         CustomMaterialFields.setColor(binding.layoutPropertyType, R.color.grey_color_two, requireActivity())
                     }
                 }

                 val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,itemList)
                 binding.tvPropertyType.setAdapter(adapter)
                 binding.tvPropertyType.setOnClickListener {
                     binding.tvPropertyType.showDropDown()
                 }
                 binding.tvPropertyType.onItemClickListener = object :
                     AdapterView.OnItemClickListener {
                     override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                         showHideRental()
                     }
                 }
             })


        // occupancy Type spinner
             viewModel.occupancyType.observe(viewLifecycleOwner, { occupancyList->

                if(occupancyList != null && occupancyList.size > 0) {
                    val itemList: ArrayList<String> = arrayListOf()
                    for (item in occupancyList) {
                        itemList.add(item.name)
                        occupancyTypeList.add(item)
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



        viewModel.propertyStatus.observe(viewLifecycleOwner, {
            if(it != null && it.size > 0) {
                val itemList: ArrayList<String> = arrayListOf()
                for (item in it) {
                    itemList.add(item.name)
                    propertyStatusList.add(item)

                    /*if(occupancyTypeId > 0 && occupancyTypeId == item.id){
                        binding.tvOccupancyType.setText(item.name)
                        CustomMaterialFields.setColor(binding.layoutOccupancyType,R.color.grey_color_two,requireActivity())
                    } */
                }

                val adapterPropertyStatus = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,itemList)
                binding.tvPropertyStatus.setAdapter(adapterPropertyStatus)
                binding.tvPropertyStatus.setOnFocusChangeListener { _, _ ->
                    binding.tvPropertyStatus.showDropDown()
                }
                binding.tvPropertyStatus.setOnClickListener {
                    binding.tvPropertyStatus.showDropDown()
                }
                binding.tvPropertyStatus.onItemClickListener = object :
                    AdapterView.OnItemClickListener {
                    override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                        CustomMaterialFields.setColor(binding.layoutPropertyStatus,R.color.grey_color_two,requireActivity())
                        showHideRental()
                    }
                }
            }
        })
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
        val addressFragment = RealEstateAddressFragment()
        val bundle = Bundle()
        bundle.putParcelable(AppConstant.address, realEstateAddress)
        addressFragment.arguments = bundle
        findNavController().navigate(R.id.action_realestate_address, addressFragment.arguments)
    }

    private fun onFirstMortgageYes(){
        if(binding.rbFirstMortgageYes.isChecked) {
            binding.layoutFirstMortgageDetail.visibility = View.VISIBLE
            binding.layoutSecondMortgage.visibility = View.VISIBLE
            binding.rbFirstMortgageYes.setTypeface(null, Typeface.BOLD)
            binding.rbFirstMortgageNo.setTypeface(null, Typeface.NORMAL)

            val fragment = RealEstateFirstMortgage()
            val bundle = Bundle()
            bundle.putString(AppConstant.address,addressHeading)
            bundle.putParcelable(AppConstant.firstMortgage,firstMortgageModel)
            fragment.arguments = bundle
            findNavController().navigate(R.id.action_realestate_first_mortgage,bundle)

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

        val fragment = RealEstateSecondMortgage()
        val bundle = Bundle()
        bundle.putString(AppConstant.address, addressHeading)
        bundle.putParcelable(AppConstant.secMortgage,secondMortgageModel)
        fragment.arguments = bundle
        findNavController().navigate(R.id.action_realestate_second_mortgage,bundle)
    }

    private fun onSecMortgegeNoClick(){
        binding.layoutSecondMortgage.visibility = View.VISIBLE
        binding.layoutSecMortgageDetail.visibility = View.GONE
        binding.rbSecMortgageNo.setTypeface(null, Typeface.BOLD)
        binding.rbSecMortgageYes.setTypeface(null, Typeface.NORMAL)
    }

    private fun checkValidations(){

        //findNavController().popBackStack()

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

        // get property id
        val property : String = binding.tvPropertyType.getText().toString().trim()
        val matchedList1 =  propertyTypeList.filter { p -> p.name.equals(property,true)}
        //Log.e("matchedList",""+matchedList1)
        val propertyId = if(matchedList1.size > 0) matchedList1.map { matchedList1.get(0).id }.single() else null
        //Log.e("propertyId",""+propertyId)

        // get occupancy id
        val occupancy : String = binding.tvOccupancyType.getText().toString().trim()
        val matchedList =  occupancyTypeList.filter { s -> s.name.equals(occupancy,true)}
        val occupancyId = if(matchedList.size>0) matchedList.map { matchedList.get(0).id }.single() else null
        //Log.e("occcupancyId",""+occupancyId)

        // get property status
        val propertyStatus : String = binding.tvPropertyStatus.getText().toString()
        val matchedStatus =  propertyStatusList.filter { s -> s.name.equals(propertyStatus,true)}
        val propertyStatusId = if(matchedStatus.size>0) matchedStatus.map { matchedStatus.get(0).id }.single() else null
        //Log.e("occcupancyId",""+occupancyId)

        // property value
        val propertyValue = binding.edPropertyValue.text.toString().trim()
        val newPropertyValue = if(propertyValue.length > 0) propertyValue.replace(",".toRegex(), "") else null

        // home insurance
        val homeInsurance = binding.edHomeownerInsurance.text.toString().trim()
        val newHomeInsurance = if(homeInsurance.length > 0) homeInsurance.replace(",".toRegex(), "") else null

        val hoa = binding.edAssociationDues.text.toString().trim()
        val newHoaDues = if(hoa.length > 0) hoa.replace(",".toRegex(), "") else null

        val propertyTax = binding.edPropertyTax.text.toString().trim()
        val newPropertyTax = if(propertyTax.length > 0) propertyTax.replace(",".toRegex(), "") else null

        val rentalIncome = binding.edRentalIncome.text.toString().trim()
        val newRentalIncome = if(rentalIncome.length > 0) rentalIncome.replace(",".toRegex(), "") else null

        // flood insurance
        val floodInsurance = binding.edFloodInsurance.text.toString().trim()
        val newFloodInsurance = if(floodInsurance.length >0 ) floodInsurance.replace(",".toRegex(), "") else null

        val hasFirstMortgage = if(binding.rbFirstMortgageYes.isChecked) true else false
        val hasSecondMortgage = if(binding.rbSecMortgageYes.isChecked) true else false

        lifecycleScope.launchWhenStarted{
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                val activity = (activity as? RealEstateActivity)

                activity?.loanApplicationId?.let { loanId -> loanApplicationId = loanId }

                activity?.borrowerPropertyId?.let { borrowerPropertyId = it }

                activity?.borrowerId?.let { borrowerId = it }

                activity?.propertyInfoId?.let { propertyInfoId = it }
                Log.e("realEstateIds","Loan Application Id" + loanApplicationId + " borrowerPropertyId" + borrowerPropertyId+ " borrowerId" + borrowerId + " propertyInfoID " + propertyInfoId)
                //Log.e("first Mortgage model before add api", ""+ firstMortgageModel)
               // Log.e("sec Mortgage model before add api", ""+ secondMortgageModel)

                val data = AddRealEstateResponse(loanApplicationId = loanApplicationId,propertyTypeId = propertyId,occupancyTypeId = occupancyId,propertyStatus=propertyStatusId,
                    appraisedPropertyValue = newPropertyValue?.toDouble(),propertyTax=newPropertyTax?.toDouble(),homeOwnerInsurance=newHomeInsurance?.toDouble(), floodInsurance = newFloodInsurance?.toDouble(), hoaDues=newHoaDues?.toDouble(), hasFirstMortgage = hasFirstMortgage,
                    hasSecondMortgage = hasSecondMortgage, address = realEstateAddress, firstMortgageModel = firstMortgageModel,secondMortgageModel=secondMortgageModel,
                    rentalIncome = newRentalIncome?.toDouble(),borrowerPropertyId = borrowerPropertyId,borrowerId = borrowerId,propertyInfoId = propertyInfoId)
                Log.e("RealEstateDataApi", ""+data)
                binding.loaderRealEstate.visibility=View.VISIBLE
                viewModel.sendRealEstate(authToken,data)

            }
        }
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

        else if(event.addUpdateDataResponse.code == AppConstant.INTERNET_ERR_CODE)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )

        else
            if(event.addUpdateDataResponse.message != null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )

        binding.loaderRealEstate.visibility = View.GONE
        findNavController().popBackStack()
    }

}