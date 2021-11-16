package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.RetirementLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class RetirementFragment:BaseFragment() {

    private var _binding: RetirementLayoutBinding? = null
    private val binding get() = _binding!!

    private var loanApplicationId:Int? = null
    private var loanPurpose:String? = null
    private var borrowerId:Int? = null
    private var borrowerAssetId:Int = -1
    private var assetTypeID:Int? = null
    private var id:Int? = null

    private val viewModel: AssetViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = RetirementLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        arguments?.let { arguments->
            loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
            loanPurpose = arguments.getString(AppConstant.loanPurpose)
            borrowerId = arguments.getInt(AppConstant.borrowerId)
            borrowerAssetId = arguments.getInt(AppConstant.borrowerAssetId, -1)
            assetTypeID = arguments.getInt(AppConstant.assetTypeID)
            observeRetirementData()
        }
        return root
    }

    private fun setUpUI(){

        CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())
        binding.annualBaseEditText.addTextChangedListener(NumberTextFormat(binding.annualBaseEditText))

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.saveBtn.setOnClickListener { saveRetirement() }

        addFocusOutListenerToFields()

        setUpEndIcon()
    }

    private fun saveRetirement(){

        viewModel.genericAddUpdateAssetResponse.observe(viewLifecycleOwner, { genericAddUpdateAssetResponse ->
            if(genericAddUpdateAssetResponse.status == "OK"){
                val codeString = genericAddUpdateAssetResponse.code.toString()
                if(codeString == "200"){
                    lifecycleScope.launchWhenStarted {
                        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->

                        }
                    }
                }
            }
        })

        val fieldsValidated = checkEmptyFields()
        if(fieldsValidated) {
            clearFocusFromFields()
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->

                    loanApplicationId?.let { notNullLoanApplicationId->
                        borrowerId?.let { notNullBorrowerId ->
                            val retirementAddUpdateParams =
                                RetirementAddUpdateParams(
                                    BorrowerId = notNullBorrowerId,
                                    LoanApplicationId = notNullLoanApplicationId,
                                    AccountNumber = binding.accountNumberEdittext.text.toString(),
                                    InstitutionName = binding.financialEditText.text.toString(),
                                    Value = binding.annualBaseEditText.text.toString().toInt(),
                                    Id = id
                                )
                            viewModel.addUpdateRetirement(authToken , retirementAddUpdateParams)
                        }
                    }
                }
            }
        }

        /*
        BorrowerId = 5,
        LoanApplicationId = 5,
        AccountNumber = "ABC",
        InstitutionName = "XYZ",
        Value = 10011,
        Id = 1102

         */


    }

    private fun setUpEndIcon(){
        binding.accountNumberLayout.setEndIconOnClickListener(View.OnClickListener {
            if (binding.accountNumberEdittext.getTransformationMethod()
                    .equals(PasswordTransformationMethod.getInstance())
            ) { //  hide password
                binding.accountNumberEdittext.setTransformationMethod(HideReturnsTransformationMethod.getInstance())
                binding.accountNumberLayout.setEndIconDrawable(R.drawable.ic_eye_hide)
            } else {
                binding.accountNumberEdittext.setTransformationMethod(PasswordTransformationMethod.getInstance())
                binding.accountNumberLayout.setEndIconDrawable(R.drawable.ic_eye_icon_svg)
            }
        })
    }

    private fun clearFocusFromFields(){
        binding.accountNumberLayout.clearFocus()
        binding.annualBaseLayout.clearFocus()
        binding.financialLayout.clearFocus()
    }

    private fun checkEmptyFields():Boolean{
        var bool =  true
        if(binding.accountNumberEdittext.text?.isEmpty() == true || binding.accountNumberEdittext.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.accountNumberLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.accountNumberLayout,  requireContext())

        if(binding.annualBaseEditText.text?.isEmpty() == true || binding.annualBaseEditText.text?.isBlank() == true) {

            CustomMaterialFields.setError(binding.annualBaseLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.annualBaseLayout,  requireContext())

        if(binding.financialEditText.text?.isEmpty() == true || binding.financialEditText.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.financialLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.financialLayout,  requireContext())
        return bool
    }

    private  fun addFocusOutListenerToFields(){
        binding.accountNumberEdittext.onFocusChangeListener = CustomFocusListenerForEditText( binding.accountNumberEdittext , binding.accountNumberLayout , requireContext())
        binding.annualBaseEditText.onFocusChangeListener = CustomFocusListenerForEditText( binding.annualBaseEditText , binding.annualBaseLayout , requireContext())
        binding.financialEditText.onFocusChangeListener = CustomFocusListenerForEditText( binding.financialEditText , binding.financialLayout , requireContext())
    }

    private fun observeRetirementData(){
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if(loanApplicationId != null && borrowerId != null &&  borrowerAssetId>0) {
                    viewModel
                        .getRetirementAccountDetails(authToken, loanApplicationId!!, borrowerId!!, borrowerAssetId)
                }
            }
            viewModel.retirementAccountDetail.observe(viewLifecycleOwner, { observeRetirementData ->
                if(observeRetirementData.code == AppConstant.RESPONSE_CODE_SUCCESS) {
                    observeRetirementData.retirementAccountData?.let{ retirementAccountData->
                        retirementAccountData.id?.let { id = it }
                        binding.financialEditText.setText(retirementAccountData.institutionName)
                        binding.accountNumberEdittext.setText(retirementAccountData.accountNumber)
                        val value = retirementAccountData.value.toString()
                        binding.annualBaseEditText.setText(value)
                    }
                }
                else
                    findNavController().popBackStack()
            })
        }
    }


}