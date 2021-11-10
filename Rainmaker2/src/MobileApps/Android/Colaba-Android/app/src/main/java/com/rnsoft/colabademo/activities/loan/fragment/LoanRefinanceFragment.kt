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
import androidx.activity.addCallback
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.databinding.AppHeaderWithBackNavBinding
import com.rnsoft.colabademo.databinding.LoanRefinanceInfoBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import java.util.ArrayList
import javax.inject.Inject

/**
 * Created by Anita Kiran on 9/6/2021.
 */
@AndroidEntryPoint
class LoanRefinanceFragment : BaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private var loanApplicationId: Int? = null
    private val loanViewModel : LoanInfoViewModel by activityViewModels()
    private lateinit var binding: LoanRefinanceInfoBinding
    private lateinit var bindingToolbar: AppHeaderWithBackNavBinding
    val stageList: ArrayList<String> = arrayListOf()
    //var loanInfoModel = LoanInfoData()
    var downPayment : Double?= null
    var propertyValue : Double ?= null

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        binding = LoanRefinanceInfoBinding.inflate(inflater, container, false)
        bindingToolbar = binding.headerLoanPurchase
        // set Header title
        bindingToolbar.headerTitle.setText(getString(R.string.loan_info_refinance))


        initViews()
        clicks()
        getLoanInfoDetail()

        super.addListeners(binding.root)
        return binding.root

    }

    private fun initViews(){

        // add prefix
        CustomMaterialFields.setDollarPrefix(binding.layoutLoanAmount,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutCashout,requireContext())

        // number formats
        binding.edCashoutAmount.addTextChangedListener(NumberTextFormat(binding.edCashoutAmount))
        binding.edLoanAmount.addTextChangedListener(NumberTextFormat(binding.edLoanAmount))

        // lable focus
        binding.edCashoutAmount.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutCashout, R.color.grey_color_two, requireContext())
            } else {
                if (binding.edCashoutAmount.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutCashout, R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutCashout)
                    CustomMaterialFields.setColor(binding.layoutCashout, R.color.grey_color_two, requireContext())
                }
            }
        }

        binding.edLoanAmount.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutLoanAmount, R.color.grey_color_two, requireContext())
            } else {
                if (binding.edLoanAmount.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutLoanAmount, R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutLoanAmount)
                    CustomMaterialFields.setColor(binding.layoutLoanAmount, R.color.grey_color_two, requireContext())
                }
            }
        }
    }

    private fun clicks(){
        binding.btnSaveChanges.setOnClickListener {
            processData()
        }

        bindingToolbar.backButton.setOnClickListener {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold,R.anim.slide_out_left)
        }

        requireActivity().onBackPressedDispatcher.addCallback {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }
    }

    private fun getLoanInfoDetail() {
        loanViewModel.loanInfoPurchase.observe(viewLifecycleOwner, { loanInfo ->

            if (loanInfo != null) {
                loanInfo.data?.downPayment?.let {
                    downPayment = it
                }
                loanInfo.data?.propertyValue?.let {
                    propertyValue = it
                }

                loanInfo.data?.loanGoalName?.let {
                    binding.tvLoanStage.setText(it,false)
                    CustomMaterialFields.setColor(binding.layoutLoanStage,R.color.grey_color_two,requireActivity())
                }

                loanInfo.data?.cashOutAmount?.let {
                    binding.edCashoutAmount.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutCashout,R.color.grey_color_two,requireActivity())
                }
                    loanInfo.data?.loanPayment?.let {
                        binding.edLoanAmount.setText(Math.round(it).toString())
                        CustomMaterialFields.setColor(binding.layoutLoanAmount,R.color.grey_color_two,requireActivity())
                    }

                loanInfo.data?.loanPurposeId?.let {
                    loanViewModel.getLoanGoals(AppConstant.authToken,it)
                    loanViewModel.loanGoals.observe(viewLifecycleOwner,{
                        for(item in it){
                            stageList.add(item.description)
                        }
                        setLoanStageSpinner()

                        })
                    }
                    if(loanInfo.code.equals(AppConstant.RESPONSE_CODE_SUCCESS)){
                        hideLoader()
                    }
                }
                hideLoader()
            })
    }

    private fun setLoanStageSpinner() {
        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, stageList)
        binding.tvLoanStage.setAdapter(adapter)
        binding.tvLoanStage.setOnFocusChangeListener { _, _ ->
            binding.tvLoanStage.showDropDown()
        }
        binding.tvLoanStage.setOnClickListener {
            binding.tvLoanStage.showDropDown()
        }
        binding.tvLoanStage.onItemClickListener = object :
            AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.layoutLoanStage.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                if(binding.tvLoanStage.text.isNotEmpty() && binding.tvLoanStage.text.isNotBlank()) {
                    clearError(binding.layoutLoanStage)
                }
                /*if (position == loanStageArray.size - 1)
                    binding.layoutLoanStage.visibility = View.VISIBLE
                else
                    binding.layoutLoanStage.visibility = View.GONE
                 */
            }
        }
    }

    private fun processData(){

        val loanStage: String = binding.tvLoanStage.text.toString().trim()
        val cashOutAmount: String = binding.edCashoutAmount.text.toString().trim()
        val loanAmount: String = binding.edLoanAmount.text.toString().trim()

        if (loanStage.isEmpty() || loanStage.length == 0) {
            setError(binding.layoutLoanStage, getString(R.string.error_field_required))
        }
        if (cashOutAmount.isEmpty() || cashOutAmount.length == 0) {
            setError(binding.layoutCashout, getString(R.string.error_field_required))
        }
        if (loanAmount.isEmpty() || loanAmount.length == 0) {
            setError(binding.layoutLoanAmount, getString(R.string.error_field_required))
        }
        // clear error
        if (loanStage.isNotEmpty() || loanStage.length > 0) {
            clearError(binding.layoutLoanStage)
        }
        if (cashOutAmount.isNotEmpty() || cashOutAmount.length > 0) {
            clearError(binding.layoutCashout)
        }
        if (loanAmount.isNotEmpty() || loanAmount.length > 0) {
            clearError(binding.layoutLoanAmount)
        }
            if(loanStage.length > 0 && loanAmount.length >0 && cashOutAmount.length > 0){
                var newCashoutAmount = cashOutAmount.replace(",".toRegex(), "")
                var newLoanAmount = loanAmount.replace(",".toRegex(), "")

                val activity = (activity as? BorrowerLoanActivity)
                activity?.loanApplicationId?.let { loanId->
                    val info = UpdateLoanRefinanceModel(
                        loanApplicationId = loanId,
                        loanPurposeId = 2,
                        loanGoalId = 4,
                        cashOutAmount = newCashoutAmount.toDouble(),
                        downPayment = downPayment!!,
                        propertyValue = propertyValue!!,
                        loanPayment = newLoanAmount.toDouble()
                    )

                    lifecycleScope.launchWhenStarted {
                        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                            binding.loaderLoanRefinance.visibility = View.VISIBLE
                            loanViewModel.addLoanRefinanceInfo(authToken, info)
                        }
                    }
                }
                binding.loaderLoanRefinance.visibility = View.GONE
            }


    }

    fun setError(textInputlayout: TextInputLayout, errorMsg: String) {
        textInputlayout.helperText = errorMsg
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(requireContext(),R.color.primary_info_stroke_error_color))
    }

    fun clearError(textInputlayout: TextInputLayout) {
        textInputlayout.helperText = ""
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(
                requireContext(),
                R.color.primary_info_line_color
            )
        )
    }

    private fun hideLoader(){
        val  activity = (activity as? BorrowerLoanActivity)
        activity?.binding?.loaderLoanInfo?.visibility = View.GONE
    }

}