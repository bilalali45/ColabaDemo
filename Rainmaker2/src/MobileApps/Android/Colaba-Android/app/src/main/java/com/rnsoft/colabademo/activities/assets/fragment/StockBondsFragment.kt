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
import com.rnsoft.colabademo.databinding.StockBondsLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import java.util.ArrayList
import javax.inject.Inject

@AndroidEntryPoint
class StockBondsFragment:BaseFragment() {

    private var _binding: StockBondsLayoutBinding? = null
    private val binding get() = _binding!!

    private var loanApplicationId:Int? = null
    private var loanPurpose:String? = null
    private var borrowerId:Int? = null
    private var borrowerAssetId:Int? = null

    private var dataArray: ArrayList<String> = arrayListOf("Checking Account", "Saving Account")

    private var bankAccounts: ArrayList<DropDownResponse> = arrayListOf()

    private lateinit var bankAdapter:ArrayAdapter<String>

    private val viewModel: AssetViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = StockBondsLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        arguments?.let { arguments->
            loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
            loanPurpose = arguments.getString(AppConstant.loanPurpose)
            borrowerId = arguments.getInt(AppConstant.borrowerId)
            borrowerAssetId = arguments.getInt(AppConstant.borrowerAssetId)
            observeStockBondsData()
        }
        return root
    }

    private fun setUpUI(){

        bankAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray)
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
                if(position ==dataArray.size-1) { }
                else{}
            }
        }

        binding.backButton.setOnClickListener { findNavController().popBackStack() }

        binding.phoneFab.setOnClickListener {
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                clearFocusFromFields()
                findNavController().popBackStack()
            }
        }

        addFocusOutListenerToFields()

        CustomMaterialFields.setDollarPrefix(binding.annualBaseLayout, requireActivity())
        binding.annualBaseEditText.addTextChangedListener(NumberTextFormat(binding.annualBaseEditText))

        setUpEndIcon()
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
        if(binding.accountNumberEdittext.text?.isEmpty() == true || binding.accountNumberEdittext.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.accountNumberLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.accountNumberLayout,  requireContext())

        if(binding.accountTypeCompleteView.text?.isEmpty() == true || binding.accountTypeCompleteView.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.accountTypeInputLayout, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.accountTypeInputLayout,  requireContext())

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
        binding.accountNumberEdittext.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.accountNumberEdittext , binding.accountNumberLayout , requireContext()))
        //binding.accountTypeCompleteView.setOnFocusChangeListener(CustomFocusListenerForAutoCompleteTextView( binding.accountTypeCompleteView , binding.accountTypeInputLayout , requireContext()))
        binding.annualBaseEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.annualBaseEditText , binding.annualBaseLayout , requireContext()))
        binding.financialEditText.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.financialEditText , binding.financialLayout , requireContext()))
    }

    private fun observeStockBondsData(){

        lifecycleScope.launchWhenStarted {
            viewModel.allFinancialAsset.observe(viewLifecycleOwner, { allFinancialAsset ->
                if(allFinancialAsset.size>0) {
                    dataArray = arrayListOf()
                    bankAccounts = arrayListOf()
                    for (item in allFinancialAsset) {
                        dataArray.add(item.name)
                        bankAccounts.add(item)
                    }
                    bankAdapter = ArrayAdapter(binding.root.context, android.R.layout.simple_list_item_1,  dataArray)
                    binding.accountTypeCompleteView.setAdapter(bankAdapter)
                }
                else
                    findNavController().popBackStack()
            })
        }

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if(loanApplicationId != null && borrowerId != null &&  borrowerAssetId!=null) {
                    viewModel
                        .getFinancialAssetDetails(authToken, loanApplicationId!!, borrowerId!!, borrowerAssetId!!)
                }
            }
            viewModel.financialAssetDetail.observe(viewLifecycleOwner, { financialAssetDetail ->
                if(financialAssetDetail.code == AppConstant.RESPONSE_CODE_SUCCESS) {
                    financialAssetDetail.financialAssetData?.let{ financialAssetData->
                        binding.financialEditText.setText(financialAssetData.institutionName)
                        binding.accountNumberEdittext.setText(financialAssetData.accountNumber)
                        binding.annualBaseEditText.setText(financialAssetData.balance.toString())
                        financialAssetData.assetTypeId?.let { assetTypeId->
                            for(item in bankAccounts){
                                if(assetTypeId == item.id){
                                    binding.accountTypeCompleteView.setText(item.name, false)
                                    break
                                }
                            }
                        }
                    }
                }
                else
                    findNavController().popBackStack()
            })
        }
    }
}