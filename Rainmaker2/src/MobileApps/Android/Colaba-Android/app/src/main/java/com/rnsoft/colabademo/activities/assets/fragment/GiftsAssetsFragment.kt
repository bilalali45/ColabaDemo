package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.RadioGroup
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.GiftsAssetLayoutBinding
import com.rnsoft.colabademo.utils.Common
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject

@AndroidEntryPoint
class GiftsAssetsFragment:BaseFragment() {

    private var _binding: GiftsAssetLayoutBinding? = null
    private val binding get() = _binding!!

    private var loanApplicationId:Int? = null
    private var loanPurpose:String? = null
    private var borrowerId:Int? = null
    private var borrowerAssetId:Int = -1
    private var assetCategoryId:Int = 4
    private var assetTypeID:Int? = null
    private var id:Int? = null

    private val GIFT_OF_EQUITY = "Gift Of Equity"
    private val GRANT = "Grant"
    private val CASH_GIFT = "Cash Gift"

    private val viewModel: AssetViewModel by activityViewModels()

    private var dataArray: ArrayList<String> = arrayListOf("Relative", "Unmarried Partner", "Federal Agency", "State Agency", "Local Agency", "Community Non Profit", "Employer", "Religious Non Profit", "Lender")
    private lateinit var giftAdapter:ArrayAdapter<String>
    private var giftResources: ArrayList<GiftSourcesResponse> = arrayListOf()
    private var giftAssetList: ArrayList<GetAssetTypesByCategoryItem> = arrayListOf()

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = GiftsAssetLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        arguments?.let { arguments->
            loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
            loanPurpose = arguments.getString(AppConstant.loanPurpose)
            borrowerId = arguments.getInt(AppConstant.borrowerId)
            borrowerAssetId = arguments.getInt(AppConstant.borrowerAssetId , -1)
            assetCategoryId = arguments.getInt(AppConstant.assetCategoryId , 4)
            assetTypeID = arguments.getInt(AppConstant.assetTypeID)
            observeGiftData()
            getGiftCategory()
        }
        return root
    }

    private fun getGiftCategory() {
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (loanApplicationId != null)
                    viewModel.fetchAssetTypesByCategoryItemList(authToken, assetCategoryId, loanApplicationId!!)
            }
        }

        viewModel.assetTypesByCategoryItemList.observe(viewLifecycleOwner, { assetTypesByCategoryItemList ->
            if(assetTypesByCategoryItemList!=null && assetTypesByCategoryItemList.size>0){
                giftAssetList = assetTypesByCategoryItemList
            }
        })
    }

    private fun observeGiftData(){
        lifecycleScope.launchWhenStarted {
            viewModel.allGiftResources.observe(viewLifecycleOwner, { giftResourceTypes ->
                if(giftResourceTypes.size>0) {
                    dataArray = arrayListOf()
                    giftResources = arrayListOf()
                    for (item in giftResourceTypes) {
                        dataArray.add(item.name)
                        giftResources.add(item)
                    }
                    giftAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray)
                    binding.giftSourceAutoCompeleteView.setAdapter(giftAdapter)
                    fetchAndObserveGiftDetails()
                }
                else
                    findNavController().popBackStack()
            })
        }
    }

    private fun fetchAndObserveGiftDetails(){

        if (loanApplicationId != null && borrowerId != null && borrowerAssetId >0) {

            viewModel.giftAssetDetail.observe(viewLifecycleOwner, { giftAssetDetail ->
                if (giftAssetDetail.code == AppConstant.RESPONSE_CODE_SUCCESS) {
                    giftAssetDetail.giftAssetData?.let { giftAssetData ->

                       giftAssetData.id?.let { id = it }

                        giftAssetData.isDeposited?.let { isDeposited ->
                            if (isDeposited) {
                                binding.cashGift.isChecked = true
                                giftAssetData.valueDate?.let { valueDate ->
                                    binding.layoutTransferDate.visibility = View.VISIBLE
                                    val newDate = valueDate.substring(0, valueDate.indexOf("T"))
                                    val initDate: Date? = SimpleDateFormat("yyyy-MM-dd").parse(newDate)
                                    val formatter = SimpleDateFormat("MM-dd-yyyy")
                                    val parsedDate: String = formatter.format(initDate)
                                    binding.dateOfTransferEditText.setText(parsedDate)
                                }
                                binding.yesDeposited.isChecked = true
                            } else {
                                binding.giftOfEquity.isChecked = true
                                binding.noDeposited.isChecked = false
                            }
                        }

                        giftAssetData.assetTypeId?.let{ assetTypeId->
                            for(item in giftAssetList){
                                if(item.id == 10 && item.id == assetTypeId )
                                    binding.cashGift.isChecked = true
                                else
                                if(item.id == 26 && item.id == assetTypeId) {
                                    binding.giftOfEquity.isChecked = true
                                    binding.giftOfEquity.text = GIFT_OF_EQUITY
                                }
                                else
                                if(item.id == 11 && item.id == assetTypeId) {
                                    binding.giftOfEquity.isChecked = true
                                    binding.giftOfEquity.text = GRANT
                                }
                            }
                        }

                        giftAssetData.value?.let {
                            val newValue = it.toString()
                            binding.annualBaseEditText.setText(newValue)
                        }
                        giftAssetData.giftSourceId?.let { giftSourceId ->
                            for (item in giftResources) {
                                if (giftSourceId == item.id) {
                                    binding.giftSourceAutoCompeleteView.setText(item.name, false)
                                    break
                                }
                            }
                        }
                    }
                }
            })

            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    viewModel.getGiftAssetDetails(authToken, loanApplicationId!!, borrowerId!!, borrowerAssetId)
                }
            }
        }


    }

    private fun setUpUI(){
        giftAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray)
        binding.giftSourceAutoCompeleteView.setAdapter(giftAdapter)
        binding.giftSourceAutoCompeleteView.setOnFocusChangeListener { _, _ ->
            HideSoftkeyboard.hide(requireContext(),  binding.giftSourceAutoCompeleteView)
            binding.giftSourceAutoCompeleteView.showDropDown()
        }
        binding.giftSourceAutoCompeleteView.setOnClickListener{
            HideSoftkeyboard.hide(requireContext(),  binding.giftSourceAutoCompeleteView)
            binding.giftSourceAutoCompeleteView.showDropDown()
        }

        binding.giftSourceAutoCompeleteView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.giftSourceInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two ))
                binding.giftSourceInputLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.giftSourceInputLayout, requireContext())
                }

                binding.giftTypeConstraintlayout.visibility = View.VISIBLE

                removeErrorFromFields()
                clearFocusFromFields()

                if(position <=1) {
                    binding.giftOfEquity.text = GRANT
                }
                else{
                    binding.giftOfEquity.text = GIFT_OF_EQUITY
                }
            }
        }
        //set prefix and format
        CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())
        binding.annualBaseEditText.addTextChangedListener(NumberTextFormat(binding.annualBaseEditText))



        binding.radioGroup.setOnCheckedChangeListener(RadioGroup.OnCheckedChangeListener { _, checkedId ->
            when (checkedId) {
                R.id.cash_gift -> {
                    HideSoftkeyboard.hide(requireContext(),binding.radioGroup)
                    clearFocusFromFields()
                    binding.layoutTransferDate.visibility = View.GONE
                    binding.giftDepositGroup.setOnCheckedChangeListener (null);
                    binding.giftDepositGroup.clearCheck()
                    binding.giftDepositGroup.setOnCheckedChangeListener(onGiftDateCheckListener)
                    binding.giftTransferConstraintlayout.visibility = View.VISIBLE
                    binding.annualBaseLayout.hint = "Cash Value"
                }
                R.id.gift_of_equity -> {
                    HideSoftkeyboard.hide(requireContext(),binding.radioGroup)
                    clearFocusFromFields()
                    binding.layoutTransferDate.visibility = View.GONE
                    binding.giftDepositGroup.setOnCheckedChangeListener (null);
                    binding.giftDepositGroup.clearCheck()
                    binding.giftDepositGroup.setOnCheckedChangeListener(onGiftDateCheckListener)
                    binding.giftTransferConstraintlayout.visibility = View.GONE
                    binding.annualBaseLayout.hint = "Market Value"
                }
                else -> {
                }
            }
        })

        binding.dateOfTransferEditText.showSoftInputOnFocus = false
        binding.dateOfTransferEditText.setOnClickListener { openCalendar() }
        binding.dateOfTransferEditText.setOnFocusChangeListener{ _ , _ ->  openCalendar() }


        binding.backButton.setOnClickListener {
            viewModel.setGiftDetailToNull()
            findNavController().popBackStack()
        }

        binding.saveBtn.setOnClickListener {
            saveGift()
        }

        addFocusOutListenerToFields()


    }



    private fun saveGift(){
        val fieldsValidated = checkEmptyFields()
        if(fieldsValidated) {
            clearFocusFromFields()
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    var assetTypeId:Int?=null
                    for(item in giftAssetList){
                        if(binding.cashGift.isChecked && item.name ==binding.cashGift.text.toString() )
                            assetTypeId = item.id
                        if(binding.giftOfEquity.isChecked && item.name.equals(GIFT_OF_EQUITY, true) )
                            assetTypeId = item.id
                        if(binding.giftOfEquity.isChecked && item.name.equals(GRANT, true) )
                            assetTypeId = item.id
                    }

                    var giftSourceId:Int?=null
                    for(item in giftResources){
                        if(item.name == binding.giftSourceAutoCompeleteView.text.toString())
                            giftSourceId = item.id
                    }
                    giftSourceId?.let { notNullGiftSourceId ->
                            loanApplicationId?.let { notNullLoanApplicationId ->
                                borrowerId?.let { notNullBorrowerId ->

                                    var IsDeposited:Boolean? = null
                                    if(binding.yesDeposited.isChecked) IsDeposited = true
                                    else
                                        if(binding.noDeposited.isChecked) IsDeposited = false

                                    val giftAddUpdateParams =
                                        GiftAddUpdateParams(
                                            BorrowerId = notNullBorrowerId,
                                            LoanApplicationId = notNullLoanApplicationId,
                                            GiftSourceId = notNullGiftSourceId,
                                            Description = null,
                                            AssetTypeId = assetTypeId,
                                            Id = id,
                                            IsDeposited = IsDeposited,
                                            Value = Common.removeCommas(binding.annualBaseEditText.text.toString()).toInt(),
                                            valueDate = binding.dateOfTransferEditText.text.toString()
                                        )
                                    viewModel.addUpdateGift(authToken, giftAddUpdateParams)
                                }

                        }
                    }

                }
            }
        }

        viewModel.genericAddUpdateAssetResponse.observe(viewLifecycleOwner, { genericAddUpdateAssetResponse ->
            if(genericAddUpdateAssetResponse.status == "OK"){
                val codeString = genericAddUpdateAssetResponse.code.toString()
                if(codeString == "200"){
                    lifecycleScope.launchWhenStarted {
                        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                            viewModel.setGiftDetailToNull()
                            findNavController().popBackStack()
                        }
                    }
                }
            }
        })
    }


    private val onGiftDateCheckListener =
        RadioGroup.OnCheckedChangeListener { p0, checkedId ->
            when (checkedId) {
                R.id.yes_deposited -> {
                    HideSoftkeyboard.hide(requireContext(),binding.radioGroup)
                    clearFocusFromFields()
                    binding.layoutTransferDate.visibility = View.VISIBLE
                    binding.layoutTransferDate.hint = resources.getString(R.string.date_of_transfer)
                }
                R.id.no_deposited -> {
                    HideSoftkeyboard.hide(requireContext(),binding.radioGroup)
                    clearFocusFromFields()
                    binding.layoutTransferDate.visibility = View.VISIBLE
                    binding.layoutTransferDate.hint = resources.getString(R.string.expected_date_of_transfer)
                }
                else -> {
                }
            }
        }

    private fun clearFocusFromFields(){
        binding.giftSourceInputLayout.clearFocus()
        binding.radioGroup.clearFocus()
        binding.annualBaseLayout.clearFocus()
        binding.giftDepositGroup.clearFocus()
        binding.dateOfTransferEditText.clearFocus()
    }

    private fun checkEmptyFields():Boolean{
        var bool =  true

        if(binding.giftSourceAutoCompeleteView.text?.isEmpty() == true || binding.giftSourceAutoCompeleteView.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.giftSourceInputLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.giftSourceInputLayout,  requireContext())

        /*
        if(binding.annualBaseEditText.text?.isEmpty() == true || binding.annualBaseEditText.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.annualBaseLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())

        if(binding.dateOfTransferEditText.text?.isEmpty() == true || binding.dateOfTransferEditText.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.layoutTransferDate, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.layoutTransferDate,  requireContext())
         */
        return bool
    }

    private fun removeErrorFromFields(){
        CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())
        CustomMaterialFields.clearError(binding.giftSourceInputLayout,  requireContext())
        CustomMaterialFields.clearError(binding.layoutTransferDate,  requireContext())
        //CustomMaterialFields.clearError(binding.giftDepositGroup,  requireContext())
    }

    private  fun addFocusOutListenerToFields() {
        binding.annualBaseEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.annualBaseEditText,
                binding.annualBaseLayout,
                requireContext()
            )
        )
        binding.dateOfTransferEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.dateOfTransferEditText,
                binding.layoutTransferDate,
                requireContext()
            )
        )


    }

    private fun openCalendar(){
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val newMonth = month + 1
        //  datePicker.findViewById(Resources.getSystem().getIdentifier("day", "id", "android")).setVisibility(View.GONE);

        val dpd = DatePickerDialog(requireActivity(), { view, year, monthOfYear, dayOfMonth -> binding.dateOfTransferEditText.setText("" + newMonth + "-" + dayOfMonth + "-" + year) }, year, month, day)

        dpd.show()

    }
}