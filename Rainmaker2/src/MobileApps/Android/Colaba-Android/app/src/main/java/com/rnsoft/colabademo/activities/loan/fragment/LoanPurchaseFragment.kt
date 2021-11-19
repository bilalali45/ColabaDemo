package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.DatePicker
import androidx.activity.addCallback
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.databinding.AppHeaderWithBackNavBinding
import com.rnsoft.colabademo.databinding.LoanPurchaseInfoBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import com.rnsoft.colabademo.utils.NumberTextFormat
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import java.text.DecimalFormat
import java.util.*
import javax.inject.Inject


/**
 * Created by Anita Kiran on 9/3/2021.
 */
@AndroidEntryPoint
class LoanPurchaseFragment : BaseFragment() , DatePickerDialog.OnDateSetListener{

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private var loanApplicationId: Int? = null
    private val loanViewModel : LoanInfoViewModel by activityViewModels()
    private lateinit var binding: LoanPurchaseInfoBinding
    private lateinit var bindingToolbar : AppHeaderWithBackNavBinding
    val format =  DecimalFormat("#,###,###")
    private lateinit var mTextWatcher : TextWatcher
    val stageList:ArrayList<String> = arrayListOf()
    private var goalFullList: ArrayList<LoanGoalModel> = arrayListOf()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = LoanPurchaseInfoBinding.inflate(inflater, container, false)
        bindingToolbar = binding.headerLoanPurchase

        arguments?.let { arguments ->
            loanApplicationId = arguments.getInt(AppConstant.loanApplicationId)
        }

        initViews()
        setCalulations()
        setLoanStages()

        super.addListeners(binding.root)
        return binding.root
    }

    private fun setLoanStages() {
        lifecycleScope.launchWhenStarted {
            loanViewModel.loanGoals.observe(viewLifecycleOwner, { goals ->
                if (goals != null && goals.size > 0) {
                    val itemList: ArrayList<String> = arrayListOf()
                    goalFullList = arrayListOf()
                    for (item in goals) {
                        itemList.add(item.description)
                        goalFullList.add(item)
                    }

                    val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, itemList)
                    binding.tvLoanStage.setAdapter(adapter)
                    //adapter.setNotifyOnChange(true)

                    binding.tvLoanStage.setOnFocusChangeListener { _, _ ->
                        binding.tvLoanStage.showDropDown()
                    }
                    binding.tvLoanStage.setOnClickListener {
                        binding.tvLoanStage.showDropDown()
                    }

                    binding.tvLoanStage.onItemClickListener = object :
                        AdapterView.OnItemClickListener {
                        override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                            CustomMaterialFields.setColor(binding.layoutLoanStage, R.color.grey_color_two, requireActivity())
                        }
                    }
                }
                hideLoader()
                getLoanInfoDetail()
            })

        }
    }

    private fun getLoanInfoDetail() {
        loanViewModel.loanInfoPurchase.observe(viewLifecycleOwner, { loanInfo ->
            if (loanInfo != null) {
                loanInfo.data?.loanGoalName?.let {
                    binding.tvLoanStage.setText(it)
                    CustomMaterialFields.setColor(binding.layoutLoanStage,R.color.grey_color_two,requireActivity())
                }
                loanInfo.data?.propertyValue?.let {
                    binding.edPurchasePrice.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutPurchasePrice,R.color.grey_color_two,requireActivity())
                }
                loanInfo.data?.loanPayment?.let {
                    binding.edLoanAmount.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutLoanAmount,R.color.grey_color_two,requireActivity())
                }
                loanInfo.data?.downPayment?.let {
                    binding.edDownPayment.setText(Math.round(it).toString())
                    CustomMaterialFields.setColor(binding.layoutDownPayment,R.color.grey_color_two,requireActivity())
                }
                loanInfo.data?.expectedClosingDate?.let {
                    val date = AppSetting.getMonthAndYear(it)
                    binding.edClosingDate.setText(date)
                    CustomMaterialFields.setColor(binding.layoutClosingDate,R.color.grey_color_two,requireActivity())
                }

                loanInfo.data?.loanGoalId?.let { goalId ->
                    //Log.e("loanGoalId- ", ""+ goalId)
                    for (item in goalFullList) {
                        if (item.id == goalId) {
                            binding.tvLoanStage.setText(item.description, false)
                            CustomMaterialFields.setColor(
                                binding.layoutLoanStage,
                                R.color.grey_color_two,
                                requireActivity())
                            break
                        }
                    }
                }
            }
            hideLoader()
        })
    }

    private fun initViews() {

        // set Header title
        bindingToolbar.headerTitle.setText(getString(R.string.loan_info_purchase))

        // set number format
        binding.edLoanAmount.addTextChangedListener(NumberTextFormat(binding.edLoanAmount))

        // set field $ prefixes
        CustomMaterialFields.setDollarPrefix(binding.layoutPurchasePrice,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutLoanAmount,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.layoutDownPayment,requireContext())
        CustomMaterialFields.setPercentagePrefix(binding.layoutPercent,requireContext())

        binding.edClosingDate.setOnClickListener {
            createCalendarDialog()
        }

        binding.edClosingDate.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edClosingDate,
                binding.layoutClosingDate,
                requireContext()
            )
        )

        binding.edPurchasePrice.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutPurchasePrice, R.color.grey_color_two, requireContext())
            } else {
                if (binding.edPurchasePrice.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutPurchasePrice, R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutPurchasePrice)
                    clearError(binding.layoutDownPayment)
                    clearError(binding.layoutPercent)
                    CustomMaterialFields.setColor(binding.layoutPurchasePrice, R.color.grey_color_two, requireContext())
                    val value = binding.edPurchasePrice.text.toString()
                    value.let {
                       validatePurchasePrice(value)
                    }
                }
            }
        }

        binding.edDownPayment.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                binding.edDownPayment.addTextChangedListener(mTextWatcher)
                CustomMaterialFields.setColor(binding.layoutDownPayment,R.color.grey_color_two, requireContext())
            } else {
                binding.edDownPayment.removeTextChangedListener(mTextWatcher)
                if (binding.edDownPayment.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutDownPayment, R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutDownPayment)
                    CustomMaterialFields.setColor(binding.layoutDownPayment, R.color.grey_color_two, requireContext())
                }
            }
        }

        binding.edPercent.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                binding.edPercent.addTextChangedListener(mTextWatcher)
            } else {
                binding.edPercent.removeTextChangedListener(mTextWatcher)
            }
        }

        binding.edLoanAmount.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutLoanAmount,R.color.grey_color_two, requireContext())
            } else {
                if (binding.edLoanAmount.text?.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutLoanAmount, R.color.grey_color_three, requireContext())
                } else {
                    clearError(binding.layoutLoanAmount)
                    CustomMaterialFields.setColor(binding.layoutLoanAmount, R.color.grey_color_two, requireContext())
                }
            }
        }

        // clicks
        bindingToolbar.backButton.setOnClickListener {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold,R.anim.slide_out_left)
        }

        requireActivity().onBackPressedDispatcher.addCallback {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }

        binding.btnSaveChange.setOnClickListener {
            checkValidations()
        }

        binding.loanPurchaseLayout.setOnClickListener{
            HideSoftkeyboard.hide(requireActivity(),binding.loanPurchaseLayout)
            super.removeFocusFromAllFields(binding.loanPurchaseLayout)
        }

    }

    private fun setCalulations(){

        mTextWatcher  = object : TextWatcher {
            override fun afterTextChanged(et: Editable?) {
                when {
                    et === binding.edPurchasePrice.editableText -> {
                        binding.edDownPayment.removeTextChangedListener(this)
                        binding.edPercent.removeTextChangedListener(this)
                        try {
                            var input = et.toString()
                            if(!input.isEmpty()) {
                                input = input.replace(",", "")
                                val newPrice = format.format(input.toLong())
                                binding.edPurchasePrice.removeTextChangedListener(this) //To Prevent from Infinite Loop
                                binding.edPurchasePrice.setText(newPrice)
                                binding.edPurchasePrice.setSelection(newPrice.length)
                                binding.edPurchasePrice.addTextChangedListener(this)
                                calculateInitialDownPayment(newPrice)
                            }
                        } catch (nfe: NumberFormatException) {
                            nfe.printStackTrace()
                        } catch (e: Exception) {
                            e.printStackTrace()
                        }
                    }

                    et === binding.edDownPayment.editableText -> {
                        binding.edPercent.removeTextChangedListener(this)
                        try {
                            var input = et.toString()
                            if(!input.isEmpty()){
                                input = input.replace(",", "")
                                val newPrice = format.format(input.toLong())
                                binding.edDownPayment.removeTextChangedListener(this)
                                binding.edDownPayment.setText(newPrice)
                                binding.edDownPayment.setSelection(newPrice.length)
                                binding.edDownPayment.addTextChangedListener(this)
                            }

                            val edValue = binding.edPurchasePrice.text.toString()
                            var purchasePrice = edValue.replace(",", "").toInt()
                            if (purchasePrice > 0 ) {
                                val downPayment = binding.edDownPayment.text.toString()
                                val newDownPayment = downPayment.replace(",", "").toInt()
                                downPayment.let {
                                    val result: Float = (newDownPayment.toFloat() / purchasePrice.toFloat()) * 100
                                    val convertResult: Int = Math.round(result)
                                    //Log.e(""+ purchasePrice, " Downpayment " + downPayment  +" Result " + result)
                                    //Log.e("ConvertResult", ""+ convertResult)
                                    binding.edPercent.setText(convertResult.toString())

                                    // calculate loan amount
                                    val newLoanAmount: Float = (purchasePrice.toFloat() - newDownPayment.toFloat())
                                    binding.edLoanAmount.setText(Math.round(newLoanAmount).toString())
                                    //Log.e(""+ purchasePrice, " Downpayment " + downPayment  +" Result " + result)
                                    //Log.e("LoanAmount", ""+ newLoanAmount)
                                }
                            }

                        } catch (nfe: NumberFormatException) {
                            nfe.printStackTrace()
                        } catch (e: Exception) {
                            e.printStackTrace()
                        }
                    }

                    et === binding.edPercent.editableText -> {

                        binding.edDownPayment.removeTextChangedListener(this)
                        try{
                            val edValue = binding.edPurchasePrice.text.toString()
                            val purchasePrice = edValue.replace(",", "").toInt()
                            if (purchasePrice > 0) {
                                val percent = binding.edPercent.text.toString()
                                edValue.let {
                                    val result = (purchasePrice * Integer.parseInt(percent)) / 100
                                    val newDownPayment = format.format(result)
                                    binding.edDownPayment.setText(newDownPayment.toString())
                                    // calculate loan amount
                                    val newLoanAmount: Float = (purchasePrice.toFloat() - result.toFloat())
                                    //Log.e("loanAmount",""+newDownPayment)
                                    binding.edLoanAmount.setText(Math.round(newLoanAmount).toString())
                                }
                            }
                        } catch (nfe: NumberFormatException) {
                            nfe.printStackTrace()
                        } catch (e: Exception) {
                            e.printStackTrace()
                        }
                    }
                }
            }

            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {}
            override fun onTextChanged(s: CharSequence?, p1: Int, p2: Int, p3: Int) {

                when {
                    s === binding.edPurchasePrice.editableText -> {
                        val value = binding.edPurchasePrice.text.toString()
                        if (value?.length == 0) {
                            binding.edPercent.setText("0")
                            binding.edDownPayment.setText("0")
                        }
                    }
                }
            }
        }
        binding.edPurchasePrice.addTextChangedListener(mTextWatcher)
        binding.edDownPayment.addTextChangedListener(mTextWatcher)
        binding.edPercent.addTextChangedListener(mTextWatcher)

    }

    private fun validatePurchasePrice(value:String){
        var purchasePrice = value.replace(",", "");
        if (purchasePrice.toInt() < 50000 || purchasePrice.toInt() > 100000000){
            CustomMaterialFields.setError(binding.layoutPurchasePrice,getString(R.string.invalid_purchase_price),requireContext())
        } else {
            CustomMaterialFields.clearError(binding.layoutPurchasePrice, requireContext())
        }
    }

    private fun calculateInitialDownPayment(value: String) {
        value.let {
            if (value.length > 0) {
                val purchasePrice = value.replace(",", "");
                val amount: Long = purchasePrice.toLong()
                val result = (amount * 20) / 100
                binding.edPercent.setText("20")
                val newPrice = format.format(result)
                binding.edDownPayment.setText(newPrice.toString())
            }
        }
    }

    private fun setLoanStageSpinner() {
        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,stageList)
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
            }
        }
    }

    private fun createCalendarDialog() {
        val pd = MonthYearPickerDialog()
        pd.setListener(this)
        pd.show(requireActivity().supportFragmentManager, "MonthYearPickerDialog")
    }

    private fun checkValidations(){

        /*val info = AddLoanInfoModel(loanApplicationId = 5,loanPurposeId = 4,loanGoalId = 4,expectedClosingDate = "2021-11-09",downPayment = 20000.0, cashOutAmount = 1,
            propertyValue = 2000.0)
        lifecycleScope.launchWhenStarted{
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                binding.loaderLoanPurchase.visibility = View.VISIBLE
                loanViewModel.addLoanInfo(authToken,info)
            }
        }
        binding.loaderLoanPurchase.visibility = View.GONE */

        val loanStage: String = binding.tvLoanStage.text.toString()
        val purchasePrice: String = binding.edPurchasePrice.text.toString()
        val loanAmount: String = binding.edLoanAmount.text.toString()
        val downPayment: String = binding.edDownPayment.text.toString()
        val percentage: String = binding.edPercent.text.toString()
        val closingDate: String = binding.edClosingDate.text.toString()

        if (loanStage.isEmpty() || loanStage.length == 0) {
            setError(binding.layoutLoanStage, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        if (purchasePrice.isEmpty() || purchasePrice.length == 0) {
            setError(binding.layoutPurchasePrice, getString(R.string.invalid_purchase_price))
        }
        if (loanAmount.isEmpty() || loanAmount.length == 0) {
            setError(binding.layoutLoanAmount, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        if (closingDate.isEmpty() || closingDate.length == 0) {
            setError(binding.layoutClosingDate, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        if (downPayment.isEmpty() || downPayment.length == 0) {
            setError(binding.layoutDownPayment, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        if (percentage.isEmpty() || percentage.length == 0) {
            setError(binding.layoutPercent, getString(com.rnsoft.colabademo.R.string.error_field_required))
        }
        // clear error
        if (loanStage.isNotEmpty() || loanStage.length > 0) {
            clearError(binding.layoutLoanStage)
        }
        if(purchasePrice.isNotEmpty() || purchasePrice.length > 0) {
            validatePurchasePrice(purchasePrice)
            clearError(binding.layoutDownPayment)
            clearError(binding.layoutPercent)
        }
        if(loanAmount.isNotEmpty() || loanAmount.length > 0) {
            clearError(binding.layoutLoanAmount)
        }
        if(downPayment.isNotEmpty() || downPayment.length > 0) {
            clearError(binding.layoutDownPayment)
        }
        if(percentage.isNotEmpty() || percentage.length > 0) {
            clearError(binding.layoutPercent)
        }
        if(closingDate.isNotEmpty() || closingDate.length > 0) {
            clearError(binding.layoutClosingDate)
        }
        else {
            if(loanStage.length > 0 && purchasePrice.length >0 && loanAmount.length >0 && downPayment.length > 0 && percentage.length > 0 && closingDate.length>0){
                loanApplicationId?.let { loanId ->
                    val info = AddLoanInfoModel(loanApplicationId = loanId, loanPurposeId = 4, loanGoalId = 4, expectedClosingDate = closingDate, downPayment = downPayment.toDouble(), cashOutAmount = 1, propertyValue = purchasePrice.toDouble())
                    lifecycleScope.launchWhenStarted {
                        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                            binding.loaderLoanPurchase.visibility = View.VISIBLE
                            loanViewModel.addLoanInfo(authToken, info)
                        }
                    }
                    binding.loaderLoanPurchase.visibility = View.GONE
                }
            }
        }
    }

    fun setError(textInputlayout: TextInputLayout, errorMsg: String) {
        textInputlayout.helperText = errorMsg
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(requireContext(), R.color.primary_info_stroke_error_color))
    }

    fun clearError(textInputlayout: TextInputLayout) {
        textInputlayout.helperText = ""
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(
                requireContext(), R.color.primary_info_line_color
            )
        )
    }

    override fun onDateSet(p0: DatePicker?, p1: Int, p2: Int, p3: Int) {
        var stringMonth = p2.toString()
        if (p2 < 10)
            stringMonth = "0$p2"
        val sampleDate = "$stringMonth / $p1"
        binding.edClosingDate.setText(sampleDate)
        CustomMaterialFields.clearError(binding.layoutClosingDate,requireActivity())
    }

    private fun hideLoader(){
        val  activity = (activity as? BorrowerLoanActivity)
        activity?.binding?.loaderLoanInfo?.visibility = View.GONE
    }

}
