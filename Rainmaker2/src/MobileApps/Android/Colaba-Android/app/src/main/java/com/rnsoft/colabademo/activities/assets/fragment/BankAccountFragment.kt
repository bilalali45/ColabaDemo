package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.BankAccountLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject
import kotlin.collections.ArrayList

@AndroidEntryPoint
class BankAccountFragment : BaseFragment() {

    private var _binding: BankAccountLayoutBinding? = null
    private val binding get() = _binding!!
    private var bankAccounts: ArrayList<String> = arrayListOf("Checking Account", "Saving Account")
    private lateinit var bankAdapter:ArrayAdapter<String>
    private val viewModel: AssetViewModel by activityViewModels()

    private var loanApplicationId:Int? = null
    private var loanPurpose:String? = null
    private var borrowerId:Int? = null
    private var borrowerAssetId:Int = -1
    private var assetTypeID:Int? = null
    private var id:Int? = null

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = BankAccountLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()

        super.addListeners(binding.root)
        arguments?.let { arguments->
            loanApplicationId = arguments.getInt(AppConstant.loanApplicationId )
            loanPurpose = arguments.getString(AppConstant.loanPurpose , null)
            borrowerId = arguments.getInt(AppConstant.borrowerId)
            borrowerAssetId = arguments.getInt(AppConstant.borrowerAssetId , -1)
            assetTypeID = arguments.getInt(AppConstant.assetTypeID, -1)
            observeBankData()
        }
        return root
    }


    private fun setUpUI(){
        bankAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  bankAccounts)
        binding.accountTypeCompleteView.setAdapter(bankAdapter)
        binding.accountTypeCompleteView.setOnFocusChangeListener { _, _ ->
            HideSoftkeyboard.hide(requireContext(),  binding.accountTypeCompleteView)
            binding.accountTypeCompleteView.showDropDown()
        }
        binding.accountTypeCompleteView.setOnClickListener{
            binding.accountTypeCompleteView.showDropDown()
        }

        binding.accountTypeCompleteView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.accountTypeInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two ))
                binding.accountNumberLayout.helperText?.let{
                    if(it.isNotEmpty())
                        CustomMaterialFields.clearError(binding.accountTypeInputLayout, requireContext())
                }
                if(position ==bankAccounts.size-1) { }
                else{}
            }
        }

        CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())
        binding.annualBaseEditText.addTextChangedListener(NumberTextFormat(binding.annualBaseEditText))

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.saveBtn.setOnClickListener {
            saveBankDetails()
        }
        addFocusOutListenerToFields()
        setUpEndIcon()
    }

    private fun saveBankDetails(){
        val fieldsValidated = checkEmptyFields()
        if(fieldsValidated) {
            clearFocusFromFields()
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    var accountTypeId:Int?=null
                    for(item in classLevelBankAccountTypes){
                        if(item.name == binding.accountTypeCompleteView.text.toString())
                            accountTypeId = item.id
                    }
                    accountTypeId?.let { notNullAccountTypeId->
                        loanApplicationId?.let { notNullLoanApplicationId->
                            borrowerId?.let { notNullBorrowerId ->
                                val bankAddUpdateParams =
                                    BankAddUpdateParams(
                                        AssetTypeId = notNullAccountTypeId,
                                        LoanApplicationId = notNullLoanApplicationId,
                                        BorrowerId = notNullBorrowerId,
                                        id = id,
                                        AccountNumber = binding.accountNumberEdittext.text.toString(),
                                        Balance = binding.annualBaseEditText.text.toString().toInt(),
                                        InstitutionName = binding.financialEditText.text.toString()
                                    )
                                viewModel.addUpdateBankDetails(authToken , bankAddUpdateParams)
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
                                findNavController().popBackStack()
                            }
                        }
                    }
                }
         })

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
        binding.accountTypeInputLayout.clearFocus()
        binding.annualBaseLayout.clearFocus()
        binding.financialLayout.clearFocus()
    }

    private fun checkEmptyFields():Boolean{
        var bool =  true

        if(binding.accountTypeCompleteView.text?.isEmpty() == true || binding.accountTypeCompleteView.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.accountTypeInputLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.accountTypeInputLayout,  requireContext())

        /*
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

         */
        return bool
    }

    private fun addFocusOutListenerToFields(){
        binding.accountNumberEdittext.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.accountNumberEdittext , binding.accountNumberLayout , requireContext()))
        //binding.accountTypeCompleteView.setOnFocusChangeListener(CustomFocusListenerForAutoCompleteTextView( binding.accountTypeCompleteView , binding.accountTypeInputLayout , requireContext()))
        binding.annualBaseEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.annualBaseEditText , binding.annualBaseLayout , requireContext()))
        binding.financialEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.financialEditText , binding.financialLayout , requireContext()))
    }

    private var classLevelBankAccountTypes: ArrayList<DropDownResponse> = arrayListOf(DropDownResponse(1, "Checking Account"), DropDownResponse(2, "Savings Account"))


    private fun observeBankData(){
        lifecycleScope.launchWhenStarted {
            viewModel.bankAccountType.observe(viewLifecycleOwner, { bankAccountTypes ->
                if(bankAccountTypes.size>0) {
                    bankAccounts = arrayListOf()
                    classLevelBankAccountTypes = arrayListOf()
                    for (item in bankAccountTypes) {
                        bankAccounts.add(item.name)
                        classLevelBankAccountTypes.add(item)
                    }
                    bankAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  bankAccounts)
                    binding.accountTypeCompleteView.setAdapter(bankAdapter)
                    fetchAndObserveBankAccountDetails()
                }
                else
                    findNavController().popBackStack()
            })
        }

    }

    private fun fetchAndObserveBankAccountDetails(){
        if(loanApplicationId != null && borrowerId != null &&  borrowerAssetId >0) {

            viewModel.bankAccountDetails.observe(viewLifecycleOwner, { bankAccountDetails ->
                if(bankAccountDetails.code == AppConstant.RESPONSE_CODE_SUCCESS){
                    bankAccountDetails.bankAccountData?.let { bankAccountData ->
                        bankAccountData.institutionName?.let { binding.financialEditText.setText(it)  }
                        bankAccountData.accountNumber?.let{ binding.accountNumberEdittext.setText(it) }
                        bankAccountData.balance?.let{binding.annualBaseEditText.setText(it.toString())}
                        bankAccountData.id?.let { id = it }
                        bankAccountData.assetTypeId?.let { assetTypeId->
                            for(item in classLevelBankAccountTypes){
                                if(assetTypeId == item.id){
                                    binding.accountTypeCompleteView.setText(item.name, false)
                                    break
                                }
                            }
                        }
                    }
                }
            })

            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    viewModel.getBankAccountDetails(authToken, loanApplicationId!!, borrowerId!!, borrowerAssetId)
                }
            }
        }
    }

}