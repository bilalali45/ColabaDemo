package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController

import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.IncomeOtherLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import java.util.ArrayList
import javax.inject.Inject

/**
 * Created by Anita Kiran on 9/15/2021.
 */
@AndroidEntryPoint
class OtherIncomeFragment : BaseFragment() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private val viewModel : IncomeViewModel by activityViewModels()
    var incomeInfoId :Int? = null
    private lateinit var binding: IncomeOtherLayoutBinding
    private lateinit var toolbarBinding: AppHeaderWithCrossDeleteBinding
    private val retirementArray = listOf("Alimony", "Child Support", "Separate Maintenance", "Foster Care", "Annuity", "Capital Gains", "Interest / Dividends", "Notes Receivable",
        "Trust", "Housing Or Parsonage", "Mortgage Credit Certificate", "Mortgage Differential Payments", "Public Assistance", "Unemployment Benefits", "VA Compensation", "Automobile" +
                " Allowance", "Boarder Income", "Royalty Payments", "Disability", "Other Income Source")

    private var loanApplicationId:Int? = null
    private var borrowerId:Int? = null
    private var incomeId:Int? = null
    private var incomeCategoryId:Int? = null
    private var incomeTypeID:Int? = null
    private var incomeTypes: ArrayList<DropDownResponse> = arrayListOf()


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = IncomeOtherLayoutBinding.inflate(inflater, container, false)
        toolbarBinding = binding.headerIncome
        super.addListeners(binding.root)
        // set Header title
        toolbarBinding.toolbarTitle.setText(getString(R.string.income_other))

        arguments?.let { arguments ->
            loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
            borrowerId = arguments.getInt(AppConstant.borrowerId)
            incomeCategoryId = arguments.getInt(AppConstant.incomeCategoryId)
            incomeTypeID = arguments.getInt(AppConstant.incomeTypeID)
            arguments.getInt(AppConstant.incomeId).let {
                if(it > 0) {
                    incomeInfoId = it
                }
            }
        }

       // Log.e("onCreate", "" + loanApplicationId + " borrowerId:  " + borrowerId + " incomeInfoId: " + incomeId)

        initViews()
        //observeOtherIncomeTypes()
        getOtherIncomeDetails()
        return binding.root
    }

    private fun observeOtherIncomeTypes(){
        lifecycleScope.launchWhenStarted {
            viewModel.otherIncomeTypes.observe(viewLifecycleOwner, { types ->
                if(types.size > 0) {
                    val itemList: ArrayList<String> = arrayListOf()
                    incomeTypes = arrayListOf()
                    for (item in types) {
                        itemList.add(item.name)
                        incomeTypes.add(item)
                    }
                    Timber.e("itemList- $itemList")
                    Timber.e("RetirementTypes- $incomeTypes")
                    val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, itemList)
                    binding.tvIncomeType.setAdapter(adapter)

                    getOtherIncomeDetails()
                }
                else
                    findNavController().popBackStack()

            })
        }

    }

    private fun getOtherIncomeDetails(){
        //Timber.e( "borrowerId:  " + borrowerId + " incomeInfoId: " + incomeInfoId )

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if(incomeInfoId != null){
                    binding.loaderOtherIncome.visibility = View.VISIBLE
                    viewModel.getOtherIncome(authToken,incomeId!!)
//                    viewModel.otherIncomeData.observe(viewLifecycleOwner, { data ->
//                        binding.loaderOtherIncome.visibility = View.GONE
//                        data?.otherIncomeData?.let { info ->
//                            info.incomeTypeId?.let { incomeTypeId->
//                                for(item in incomeTypes)
//                                    if(incomeTypeId == item.id){
//                                        binding.tvIncomeType.setText(item.name, false)
//                                        toggleOtherFields()
//                                        break
//                                    }
//                            }
//                            //info.description?.let { binding.edDesc.setText(it) }
//                            //info.monthlyBaseIncome?.let {  binding.edMonthlyIncome.setText(it.toString()) }
//                            //info.annualBaseIncome?.let {  binding.edAnnualIncome.setText(it.toString()) }
//                        }
//                    })
                }
            }
        }
    }

    private fun toggleOtherFields(){
       // binding.layoutRetirement.defaultHintTextColor = ColorStateList.valueOf(
         // ContextCompat.getColor(requireContext(), R.color.grey_color_two))

        val item = binding.tvIncomeType.text.toString()
        if (item.equals("Capital Gains",true) || item.equals( "Interest / Dividends",true) || item.equals("Other Income Source",true)) {
            binding.layoutAnnualIncome.visibility = View.VISIBLE
            binding.layoutMonthlyIncome.visibility = View.GONE
            binding.layoutDesc.visibility = View.GONE
        }
        else if (item.equals("Annuity",true)) {
            binding.layoutAnnualIncome.visibility = View.GONE
            binding.layoutMonthlyIncome.visibility = View.VISIBLE
            binding.layoutDesc.visibility = View.VISIBLE
        }
        else {
            binding.layoutAnnualIncome.visibility = View.GONE
            binding.layoutDesc.visibility = View.GONE
            binding.layoutMonthlyIncome.visibility = View.VISIBLE
        }

        if (binding.tvIncomeType.text.isNotEmpty() && binding.tvIncomeType.text.isNotBlank()) {
            CustomMaterialFields.clearError(binding.layoutRetirement,requireActivity())
        }
    }

    private fun initViews() {
        toolbarBinding.btnClose.setOnClickListener{ findNavController().popBackStack()}
        binding.btnSaveChange.setOnClickListener{ sendData() }
        binding.mainLayoutOther.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutOther)
            super.removeFocusFromAllFields(binding.mainLayoutOther)
        }


        setInputFields()
        setRetirementType()

    }

    /*override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.btn_save_change -> checkValidations()
            R.id.btn_close -> findNavController().popBackStack()
            R.id.mainLayout_other -> {
                HideSoftkeyboard.hide(requireActivity(),binding.mainLayoutOther)
                super.removeFocusFromAllFields(binding.mainLayoutOther)
            }

        }
    }*/

    private fun setInputFields() {

        // set lable focus
        binding.edMonthlyIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edMonthlyIncome, binding.layoutMonthlyIncome, requireContext()))
        binding.edAnnualIncome.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edAnnualIncome, binding.layoutAnnualIncome, requireContext()))

        // set input format
        binding.edMonthlyIncome.addTextChangedListener(NumberTextFormat(binding.edMonthlyIncome))
        binding.edAnnualIncome.addTextChangedListener(NumberTextFormat(binding.edAnnualIncome))

        // set Dollar prifix
        CustomMaterialFields.setDollarPrefix(binding.layoutMonthlyIncome, requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutAnnualIncome, requireContext())
    }

    private fun sendData(){

        val incomeType: String = binding.tvIncomeType.text.toString()
        val annualIncome: String = binding.edAnnualIncome.text.toString()
        val monthlyIncome: String = binding.edMonthlyIncome.text.toString()
        val desc: String = binding.edDesc.text.toString()

        if (incomeType.isEmpty() || incomeType.length == 0) {
            CustomMaterialFields.setError(binding.layoutRetirement, getString(R.string.error_field_required),requireActivity())
        }
        if (monthlyIncome.isEmpty() || monthlyIncome.length == 0) {
            CustomMaterialFields.setError(binding.layoutMonthlyIncome, getString(R.string.error_field_required),requireActivity())
        }
        if (annualIncome.isEmpty() || annualIncome.length == 0) {
            CustomMaterialFields.setError(binding.layoutAnnualIncome, getString(R.string.error_field_required),requireActivity())
        }
        if (desc.isEmpty() || desc.length == 0) {
            CustomMaterialFields.setError(binding.layoutDesc, getString(R.string.error_field_required),requireActivity())
        }
        if (incomeType.isNotEmpty() || incomeType.length > 0) {
            CustomMaterialFields.clearError(binding.layoutRetirement,requireActivity())
        }
        if (annualIncome.isNotEmpty() || annualIncome.length > 0) {
            CustomMaterialFields.clearError(binding.layoutAnnualIncome,requireActivity())
        }
        if (monthlyIncome.isNotEmpty() || monthlyIncome.length > 0) {
            CustomMaterialFields.clearError(binding.layoutMonthlyIncome,requireActivity())
        }
        if (desc.isNotEmpty() || desc.length > 0) {
            CustomMaterialFields.clearError(binding.layoutDesc,requireActivity())
        }

        if(incomeType.length > 0) {
            if(monthlyIncome.length > 0 || annualIncome.length >0 || desc.length > 0){

                // get incomeType id
                val type : String = binding.tvIncomeType.getText().toString().trim()
                val matchedList =  incomeTypes.filter { p -> p.name.equals(type,true)}
                //Log.e("matchedList",""+matchedList1)
                val incomeTypeId = if(matchedList.size > 0) matchedList.map { matchedList.get(0).id }.single() else null
                //Log.e("propertyId",""+propertyId)

                val monthlyIncome : String = binding.edMonthlyIncome.text.toString().trim()
                val newMonthlyIncome = if (monthlyIncome.length > 0) monthlyIncome.replace(",".toRegex(), "") else null

                val annualIncome = binding.edAnnualIncome.text.toString().trim()
                val newAnnualIncome = if(annualIncome.length > 0) annualIncome.replace(",".toRegex(), "") else null
                val desc = if (binding.edDesc.text.toString().length > 0) binding.edDesc.text.toString() else null

                val data = AddOtherIncomeInfo(
                    loanApplicationId=loanApplicationId,incomeInfoId=incomeInfoId,borrowerId=borrowerId,monthlyBaseIncome = newMonthlyIncome?.toDouble(),annualBaseIncome = newAnnualIncome?.toDouble(),
                    description = desc,incomeTypeId = incomeTypeId)

                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        if (loanApplicationId != null && borrowerId != null) {
                            Log.e("sending", "" + loanApplicationId + " borrowerId:  " + borrowerId + " incomeInfoId: " + incomeId)
                            Log.e("employmentData-snding to API", "" + data)
                            binding.loaderOtherIncome.visibility = View.VISIBLE
                            viewModel.sendOtherIncome(authToken, data)
                        }
                    }
                }
            }
        }
    }

    private fun setRetirementType(){
        //val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, retirementArray)
        //binding.tvRetirementType.setAdapter(adapter)
        binding.tvIncomeType.setOnFocusChangeListener { _, _ ->
            binding.tvIncomeType.showDropDown()
        }
        binding.tvIncomeType.setOnClickListener {
            binding.tvIncomeType.showDropDown()
        }
        binding.tvIncomeType.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutRetirement.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(), R.color.grey_color_two))
                toggleOtherFields()

                /*var item = binding.tvRetirementType.text.toString()
                if (item == "Capital Gains" || item == "Interest / Dividends" || item == "Other Income Source") {
                    binding.layoutAnnualIncome.visibility = View.VISIBLE
                    binding.layoutMonthlyIncome.visibility = View.GONE
                    binding.layoutDesc.visibility = View.GONE
                }
                else if (item == "Annuity") {
                    binding.layoutAnnualIncome.visibility = View.GONE
                    binding.layoutMonthlyIncome.visibility = View.VISIBLE
                    binding.layoutDesc.visibility = View.VISIBLE
                }
                else {
                    binding.layoutAnnualIncome.visibility = View.GONE
                    binding.layoutDesc.visibility = View.GONE
                    binding.layoutMonthlyIncome.visibility = View.VISIBLE
                }

                if (binding.tvRetirementType.text.isNotEmpty() && binding.tvRetirementType.text.isNotBlank()) {
                    CustomMaterialFields.clearError(binding.layoutRetirement,requireActivity())
                } */

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
        binding.loaderOtherIncome.visibility = View.GONE
        if(event.addUpdateDataResponse.code == AppConstant.INTERNET_ERR_CODE)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG)
        else
            if(event.addUpdateDataResponse.message != null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG)

        findNavController().popBackStack()
    }
}