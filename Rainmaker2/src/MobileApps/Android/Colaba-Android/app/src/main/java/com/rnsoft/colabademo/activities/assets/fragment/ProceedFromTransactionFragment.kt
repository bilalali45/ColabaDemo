package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.core.view.isVisible
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.ProceedFromTransLayoutBinding
import com.rnsoft.colabademo.utils.Common
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.proceed_from_trans_layout.*
import timber.log.Timber
import java.util.ArrayList
import javax.inject.Inject


    @AndroidEntryPoint
    class ProceedFromTransactionFragment : BaseFragment() {

        private var _binding: ProceedFromTransLayoutBinding? = null
        private val binding get() = _binding!!

        private var transactionArray: ArrayList<String> = arrayListOf("Proceeds From A Loan", "Proceeds from Selling Non-Real Estate Assets", "Processing from Selling Real Estate")
        private lateinit var transactionAdapter: ArrayAdapter<String>

        private val financialArray: ArrayList<String> = arrayListOf("House", "Automobile", "Financial Account", "Other")
        private lateinit var financialAdapter : ArrayAdapter<String>

        private var dropDownList: ArrayList<DropDownResponse> = arrayListOf()

        private val viewModel: AssetViewModel by activityViewModels()

        private var loanApplicationId:Int? = null
        private var loanPurpose:String? = null
        private var borrowerId:Int? = null
        private var borrowerAssetId:Int = -1
        private var assetCategoryId:Int = 6
        private var assetTypeID:Int? = null
        private var id:Int? = null

        private var categoryList: ArrayList<GetAssetTypesByCategoryItem> = arrayListOf()

        @Inject
        lateinit var sharedPreferences: SharedPreferences
        override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
            _binding = ProceedFromTransLayoutBinding.inflate(inflater, container, false)
            val root: View = binding.root
            setUpUI()
            super.addListeners(binding.root)
            arguments?.let { arguments ->
                loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
                loanPurpose = arguments.getString(AppConstant.loanPurpose)
                borrowerId = arguments.getInt(AppConstant.borrowerId)
                borrowerAssetId = arguments.getInt(AppConstant.borrowerAssetId , -1)
                assetCategoryId = arguments.getInt(AppConstant.assetCategoryId , 6)
                assetTypeID = arguments.getInt(AppConstant.assetTypeID)
            }
            getTransactionCategories()
            return root
        }

        private fun getTransactionCategories() {
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    if (loanApplicationId != null && assetCategoryId > 0)
                        viewModel.fetchAssetTypesByCategoryItemList(authToken, assetCategoryId, loanApplicationId!!)
                }
            }

            viewModel.assetTypesByCategoryItemList.observe(viewLifecycleOwner, { assetTypesByCategoryItemList ->
                if(assetTypesByCategoryItemList!=null && assetTypesByCategoryItemList.size>0){
                    categoryList = assetTypesByCategoryItemList
                    for(item in categoryList){
                        if(item.id == assetTypeID){
                            binding.transactionAutoCompleteTextView.setText(item.name , false)
                            if(item.id == 12){
                                getProceedsFromLoan(0)
                            }
                            else if(item.id == AppConstant.assetNonRealStateId){
                                getProceedsFromNonRealEstateDetail(1)
                            }
                            else if(item.id == AppConstant.assetRealStateId){
                                getProceedsFromRealEstateDetail(2)
                            }
                            break
                        }
                    }
                }
            })
        }

        private fun getProceedsFromLoan(position:Int) {
            if (loanApplicationId != null && borrowerId != null && assetTypeID != null && borrowerAssetId > 0){
                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        viewModel.getProceedsFromLoan(authToken, loanApplicationId!!, borrowerId!!, assetTypeID!!, borrowerAssetId)
                    }
                }
                observeChanges(position)
            }
        }

        private fun getProceedsFromNonRealEstateDetail(position:Int){
            if (loanApplicationId != null && borrowerId != null && assetTypeID != null && borrowerAssetId>0) {
                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->

                        viewModel.getProceedsFromNonRealEstateDetail(
                            authToken, loanApplicationId!!,
                            borrowerId!!, assetTypeID!!, borrowerAssetId
                        )
                    }
                }
                observeChanges(position)
            }
        }

        private fun getProceedsFromRealEstateDetail(position:Int){
            if (loanApplicationId != null && borrowerId != null && assetTypeID != null && borrowerAssetId>0) {
                observeChanges(position)

                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        viewModel.getProceedsFromRealEstateDetail(
                            authToken, loanApplicationId!!,
                            borrowerId!!, assetTypeID!!, borrowerAssetId
                        )
                    }
                }
            }

        }

        private fun observeChanges(position:Int){
            visibleOtherFields(position)
            viewModel.proceedFromLoanModel.observe(viewLifecycleOwner, { proceedFromLoanModel ->
                proceedFromLoanModel.proceedFromLoanData?.let{ proceedFromLoanData->
                    proceedFromLoanData.id?.let { id = it }
                    proceedFromLoanData.value?.let {
                        binding.annualBaseEditText.setText(it.toString())
                    }
                    proceedFromLoanData.description?.let {
                        binding.edDetails.setText(it)
                    }

                    proceedFromLoanData.securedByCollateral?.let {
                        if(it)
                            binding.radioButton.isChecked = true
                        else
                            binding.radioButton2.isChecked = true
                    }

                    proceedFromLoanData.collateralAssetOtherDescription?.let {
                        binding.edDetails.setText(it)
                    }

                    proceedFromLoanData.collateralAssetName?.let{
                        binding.whichAssetsCompleteView.setText(it, false)
                    }
                }
            })
        }

        private fun visibleOtherFields(position:Int){
            binding.transactionTextInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                ContextCompat.getColor(requireContext(), R.color.grey_color_two ))

            binding.transactionTextInputLayout.helperText?.let{
                if(it.isNotEmpty())
                    CustomMaterialFields.clearError(binding.transactionTextInputLayout, requireContext())
            }

            removeErrorFromFields()
            clearFocusFromFields()

            if(position ==0) {
                binding.whichAssetInputLayout.visibility = View.GONE
                binding.layoutDetail.visibility = View.GONE

                binding.whichAssetsCompleteView.setText("")
                binding.annualBaseLayout.visibility = View.VISIBLE
                binding.radioGroup.clearCheck()
                binding.radioLabelTextView.visibility = View.VISIBLE
                binding.radioGroup.visibility = View.VISIBLE

            }
            else{
                binding.whichAssetsCompleteView.setText("")
                binding.whichAssetInputLayout.visibility = View.GONE
                binding.radioLabelTextView.visibility = View.GONE
                binding.radioGroup.visibility = View.GONE
                binding.annualBaseLayout.visibility = View.VISIBLE
                binding.layoutDetail.visibility = View.VISIBLE
            }
        }

        private fun setUpUI(){
            transactionAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  transactionArray)
            binding.transactionAutoCompleteTextView.setAdapter(transactionAdapter)
            binding.transactionAutoCompleteTextView.setOnFocusChangeListener { _, _ ->
                HideSoftkeyboard.hide(requireContext(),  binding.transactionAutoCompleteTextView)
                binding.transactionAutoCompleteTextView.showDropDown()
            }
            binding.transactionAutoCompleteTextView.setOnClickListener { binding.transactionAutoCompleteTextView.showDropDown() }

            binding.transactionAutoCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
                override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                    visibleOtherFields(position)
                }
            }

            binding.radioGroup.setOnCheckedChangeListener { _, checkedId ->
                when (checkedId) {
                    R.id.radioButton -> {
                        binding.whichAssetInputLayout.visibility = View.VISIBLE
                    }
                    R.id.radioButton2 -> {
                        binding.whichAssetInputLayout.visibility = View.GONE
                    }
                    else -> {
                    }
                }
            }

            financialAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  financialArray)
            binding.whichAssetsCompleteView.setAdapter(financialAdapter)
            binding.whichAssetsCompleteView.setOnFocusChangeListener { _, _ ->
                HideSoftkeyboard.hide(requireContext(),  binding.whichAssetsCompleteView)
                binding.whichAssetsCompleteView.showDropDown()
            }
            binding.whichAssetsCompleteView.setOnClickListener{
                binding.whichAssetsCompleteView.showDropDown()
            }

            binding.whichAssetsCompleteView.onItemClickListener =
                AdapterView.OnItemClickListener { p0, p1, position, id ->
                    binding.whichAssetInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                        ContextCompat.getColor(requireContext(), R.color.grey_color_two ))

                    binding.whichAssetInputLayout.helperText?.let{
                        if(it.isNotEmpty())
                            CustomMaterialFields.clearError(binding.whichAssetInputLayout, requireContext())
                    }
                    if(position==financialArray.size-1) {
                        binding.layoutDetail.visibility = View.VISIBLE
                    } else{
                        binding.layoutDetail.visibility = View.INVISIBLE
                    }
                }

            CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())
            binding.annualBaseEditText.addTextChangedListener(NumberTextFormat(binding.annualBaseEditText))

            binding.backButton.setOnClickListener {
                findNavController().popBackStack()
            }

            binding.saveBtn.setOnClickListener {
               saveProceedTransaction()
            }

            addFocusOutListenerToFields()


        }

        private fun saveProceedTransaction(){
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                clearFocusFromFields()
                for(item in categoryList){
                    Timber.e("name = "+item.name +"  = "+binding.transactionAutoCompleteTextView.text.toString())
                    if(item.name.equals(binding.transactionAutoCompleteTextView.text.toString(),true)) {
                        if (item.id == 12)
                            addUpdateProceedFromLoan(item.id)
                        else if (item.id == AppConstant.assetRealStateId || item.id == AppConstant.assetNonRealStateId)
                            item.displayName?.let {
                                addUpdateAssetsRealStateOrNonRealState(item.id, it)
                            }
                        break
                    }
                }
            }
        }


        private fun addUpdateProceedFromLoan(assetTypeId:Int){
            observeDataUploaded()
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    var paramBorrowerAssetId:Int? = null
                    if(borrowerAssetId > 0)
                        paramBorrowerAssetId = borrowerAssetId

                    var securedByColletral:Boolean? = null
                    if(radioButton.isChecked)
                        securedByColletral = true
                    if(radioButton2.isChecked)
                        securedByColletral = false

                    var colletralAssetTypeId:Int? = null
                    for(item in financialArray) {
                        if (item == binding.whichAssetsCompleteView.text.toString()) {
                            colletralAssetTypeId = financialArray.indexOf(item)+1
                            break
                        }
                    }

                    loanApplicationId?.let { notNullLoanApplicationId ->
                        borrowerId?.let { notNullBorrowerId ->
                            val addUpdateProceedLoanParams =
                                AddUpdateProceedLoanParams(
                                    BorrowerId = notNullBorrowerId,
                                    LoanApplicationId = notNullLoanApplicationId,
                                    AssetCategoryId = assetCategoryId,
                                    AssetTypeId = assetTypeId,
                                    BorrowerAssetId = paramBorrowerAssetId,
                                    AssetValue = Common.removeCommas(binding.annualBaseEditText.text.toString()).toInt(),
                                    SecuredByColletral = securedByColletral,
                                    CollateralAssetDescription = binding.edDetails.text.toString(),
                                    ColletralAssetTypeId = colletralAssetTypeId,
                                )
                            viewModel.addUpdateProceedFromLoan(authToken, addUpdateProceedLoanParams)

                            colletralAssetTypeId?.let { notNullColletralAssetTypeId->
                                val addUpdateProceedFromLoanOtherParams =
                                    AddUpdateProceedFromLoanOtherParams(
                                        BorrowerId = notNullBorrowerId,
                                        LoanApplicationId = notNullLoanApplicationId,
                                        AssetCategoryId = assetCategoryId,
                                        AssetTypeId = assetTypeId,
                                        AssetValue = Common.removeCommas(binding.annualBaseEditText.text.toString()).toInt(),
                                        CollateralAssetDescription = binding.edDetails.text.toString(),
                                        ColletralAssetTypeId = notNullColletralAssetTypeId,
                                    )
                                viewModel.addUpdateProceedFromLoanOther(authToken, addUpdateProceedFromLoanOtherParams)
                            }
                        }
                    }
                }
            }
        }

        private fun  addUpdateAssetsRealStateOrNonRealState(assetTypeId:Int, assetTypeDisplayName:String){
            observeDataUploaded()
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    loanApplicationId?.let { notNullLoanApplicationId ->
                        borrowerId?.let { notNullBorrowerId ->
                            val addUpdateRealStateParams =
                                AddUpdateRealStateParams(
                                    BorrowerId = notNullBorrowerId,
                                    LoanApplicationId = notNullLoanApplicationId,
                                    AssetCategoryId = assetCategoryId,
                                    AssetTypeId = assetTypeId,
                                    AssetValue = Common.removeCommas(binding.annualBaseEditText.text.toString()).toInt(),
                                    Description = binding.edDetails.text.toString(),
                                )
                            viewModel.addUpdateAssetsRealStateOrNonRealState(
                                authToken,
                                addUpdateRealStateParams
                            )
                        }
                    }
                }
            }
        }

        private fun observeDataUploaded(){
            viewModel.genericAddUpdateAssetResponse.observe(viewLifecycleOwner, { genericAddUpdateAssetResponse ->
                if(genericAddUpdateAssetResponse.status == "OK"){
                    val codeString = genericAddUpdateAssetResponse.code.toString()
                    if(codeString == "200"){
                        lifecycleScope.launchWhenStarted {
                            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                                findNavController().popBackStack()
                            }
                        }
                    }
                }
            })
        }


        private fun clearFocusFromFields(){
            binding.annualBaseLayout.clearFocus()
            binding.whichAssetInputLayout.clearFocus()
            binding.transactionTextInputLayout.clearFocus()
            binding.radioGroup.clearFocus()
            binding.layoutDetail.clearFocus()
        }

        private fun checkEmptyFields():Boolean{
            var bool =  true

            if((binding.annualBaseEditText.text?.isEmpty() == true || binding.annualBaseEditText.text?.isBlank() == true)
                && binding.annualBaseLayout.isVisible) {
                CustomMaterialFields.setError(binding.annualBaseLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())


            if((binding.edDetails.text?.isEmpty() == true || binding.edDetails.text?.isBlank() == true)
                && binding.layoutDetail.isVisible) {
                CustomMaterialFields.setError(binding.layoutDetail, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.layoutDetail,  requireContext())

            if((binding.transactionAutoCompleteTextView.text?.isEmpty() == true || binding.transactionAutoCompleteTextView.text?.isBlank() == true)
                && binding.transactionTextInputLayout.isVisible) {
                CustomMaterialFields.setError(binding.transactionTextInputLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.transactionTextInputLayout,  requireContext())


            if((binding.whichAssetsCompleteView.text?.isEmpty() == true || binding.whichAssetsCompleteView.text?.isBlank() == true)
                &&  binding.whichAssetInputLayout.isVisible) {
                CustomMaterialFields.setError(binding.whichAssetInputLayout, "This field is required." , requireContext())
                bool = false
            }
            else
                CustomMaterialFields.clearError(binding.whichAssetInputLayout,  requireContext())

            return bool
        }

        private fun removeErrorFromFields(){
            CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())
            CustomMaterialFields.clearError(binding.layoutDetail,  requireContext())
            CustomMaterialFields.clearError(binding.transactionTextInputLayout,  requireContext())
            CustomMaterialFields.clearError(binding.whichAssetInputLayout,  requireContext())
        }

        private  fun addFocusOutListenerToFields() {
            binding.annualBaseEditText.setOnFocusChangeListener(
                CustomFocusListenerForEditText(
                    binding.annualBaseEditText,
                    binding.annualBaseLayout,
                    requireContext()
                )
            )
            binding.edDetails.setOnFocusChangeListener(
                CustomFocusListenerForEditText(
                    binding.edDetails,
                    binding.layoutDetail,
                    requireContext()
                )
            )
        }


}
