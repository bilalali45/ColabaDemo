package com.rnsoft.colabademo

import android.content.res.ColorStateList
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.core.view.isVisible
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.ProceedFromTransLayoutBinding
import com.rnsoft.colabademo.utils.Common
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import kotlinx.android.synthetic.main.proceed_from_trans_layout.*
import timber.log.Timber
import java.util.ArrayList



    class ProceedFromTransactionFragment : AssetBaseFragment() {

        private var _binding: ProceedFromTransLayoutBinding? = null
        private val binding get() = _binding!!

        private var transactionArray: ArrayList<String> = arrayListOf("Proceeds From A Loan", "Proceeds from Selling Non-Real Estate Assets", "Processing from Selling Real Estate")
        private lateinit var transactionAdapter: ArrayAdapter<String>

        private val financialArray: ArrayList<String> = arrayListOf("House", "Automobile", "Financial Account", "Other")
        private lateinit var financialAdapter : ArrayAdapter<String>

        private var categoryList: ArrayList<GetAssetTypesByCategoryItem> = arrayListOf()

        override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
            _binding = ProceedFromTransLayoutBinding.inflate(inflater, container, false)
            val root: View = binding.root
            setUpUI()
            super.addListeners(binding.root)
            arguments?.let { arguments ->
                loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
                loanPurpose = arguments.getString(AppConstant.loanPurpose)
                borrowerId = arguments.getInt(AppConstant.borrowerId)
                assetUniqueId = arguments.getInt(AppConstant.assetUniqueId , -1)
                assetCategoryId = arguments.getInt(AppConstant.assetCategoryId , 6)
                assetTypeID = arguments.getInt(AppConstant.assetTypeID)
                assetCategoryName = arguments.getString(AppConstant.assetCategoryName , null)
                listenerAttached = arguments.getInt(AppConstant.listenerAttached)
            }
            if(assetUniqueId>0) {
                binding.topDelImageview.visibility = View.VISIBLE
                binding.topDelImageview.setOnClickListener{ showDeleteDialog( returnUpdatedParams(true)) }
            }
            activity?.onBackPressedDispatcher?.addCallback(viewLifecycleOwner, backToAssetScreen )
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
            if (loanApplicationId != null && borrowerId != null && assetTypeID != null && assetUniqueId > 0){
                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        viewModel.getProceedsFromLoan(authToken, loanApplicationId!!, borrowerId!!, assetTypeID!!, assetUniqueId)
                    }
                }
                observeChanges(position)
            }
        }

        private fun getProceedsFromNonRealEstateDetail(position:Int){
            if (loanApplicationId != null && borrowerId != null && assetTypeID != null && assetUniqueId>0) {
                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->

                        viewModel.getProceedsFromNonRealEstateDetail(
                            authToken, loanApplicationId!!,
                            borrowerId!!, assetTypeID!!, assetUniqueId
                        )
                    }
                }
                observeChanges(position)
            }
        }

        private fun getProceedsFromRealEstateDetail(position:Int){
            if (loanApplicationId != null && borrowerId != null && assetTypeID != null && assetUniqueId>0) {
                observeChanges(position)

                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        viewModel.getProceedsFromRealEstateDetail(
                            authToken, loanApplicationId!!,
                            borrowerId!!, assetTypeID!!, assetUniqueId
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
            observeAddUpdateResponse(returnUpdatedParams())
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    var paramBorrowerAssetId:Int? = null
                    if(assetUniqueId > 0)
                        paramBorrowerAssetId = assetUniqueId

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
            observeAddUpdateResponse(returnUpdatedParams())
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

        private fun returnUpdatedParams(assetDeleteBoolean:Boolean = false): AssetReturnParams {
            var assetAction = AppConstant.assetAdded
            if(assetDeleteBoolean)
                assetAction = AppConstant.assetDeleted
            else
                if(assetUniqueId>0)
                    assetAction = AppConstant.assetUpdated

            Timber.e("catching unique id in returnUpdatedParams  = $assetUniqueId")

            return AssetReturnParams(
                assetName = binding.transactionAutoCompleteTextView.text.toString(),
                assetTypeName = binding.edDetails.text.toString(),
                assetTypeID = assetTypeID,
                assetUniqueId = assetUniqueId,
                assetCategoryId = assetCategoryId,
                assetCategoryName = assetCategoryName,
                listenerAttached = listenerAttached,
                assetAction = assetAction,
                assetValue = Common.removeCommas(binding.annualBaseEditText.text.toString()).toDouble()
            )
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
            binding.annualBaseEditText.onFocusChangeListener = CustomFocusListenerForEditText(
                binding.annualBaseEditText,
                binding.annualBaseLayout,
                requireContext()
            )
            binding.edDetails.onFocusChangeListener = CustomFocusListenerForEditText(
                binding.edDetails,
                binding.layoutDetail,
                requireContext()
            )
        }


}
