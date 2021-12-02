package com.rnsoft.colabademo.activities.addresses.info

import android.annotation.SuppressLint
import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.graphics.Typeface
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.util.Log
import android.view.*
import android.view.inputmethod.InputMethodManager
import androidx.activity.addCallback
import androidx.annotation.ColorRes
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.activities.addresses.info.*
import com.rnsoft.colabademo.activities.addresses.info.adapter.DependentAdapter
import com.rnsoft.colabademo.activities.addresses.info.fragment.DeleteCurrentResidenceDialogFragment
import com.rnsoft.colabademo.activities.addresses.info.fragment.SwipeToDeleteEvent
import com.rnsoft.colabademo.activities.addresses.info.model.Dependent
import com.rnsoft.colabademo.databinding.*
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.RecyclerTouchListener
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.notification_view_holder.*
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import java.util.*
import java.util.regex.Pattern
import javax.inject.Inject
import kotlin.collections.ArrayList


/**
 * Created by Anita Kiran on 8/23/2021.
 */

// remove address click listener
@AndroidEntryPoint
class PrimaryBorrowerInfoFragment : BaseFragment(), RecyclerviewClickListener, View.OnClickListener, AddressClickListener {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private val viewModel : PrimaryBorrowerViewModel by activityViewModels()
    lateinit var bi: PrimaryBorrowerInfoLayoutBinding
    lateinit var msBinding: SublayoutMaritalStatusBinding
    lateinit var citizenshipBinding: SublayoutCitizenshipBinding
    lateinit var bindingMilitary: SubLayoutMilitaryBinding
    private var savedViewInstance: View? = null
    private var touchListener: RecyclerTouchListener? = null
    var count : Int = 0
    var selectedPosition : Int?=null
    val listItems = ArrayList<Dependent>()
    lateinit var adapter: BorrowerAddressAdapter
    lateinit var dependentAdapter: DependentAdapter
    var addressBtnText : String = "Add Previous Address"
    var reserveEverActivated : Boolean? = null
    var listAddress: ArrayList<PrimaryBorrowerAddress> = ArrayList()
    private var loanApplicationId: Int? = null
    private var borrowerId :Int? = null
    private var currentAddressModel = AddressModel()
    private var currentAddressDetail = CurrentAddress()


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return if (savedViewInstance != null) {
            savedViewInstance

        } else {
            bi = PrimaryBorrowerInfoLayoutBinding.inflate(inflater, container, false)
            savedViewInstance = bi.root
            super.addListeners(savedViewInstance as ViewGroup)
            msBinding = bi.layoutMaritalStatus
            citizenshipBinding = bi.layoutCitizenship
            bindingMilitary = bi.layoutMilitaryService

            setViews()
            bi.tvResidence.setText(requireContext().getString(R.string.add_previous_address))

            val activity = (activity as? BorrowerAddressActivity)
            activity?.loanApplicationId?.let {
                loanApplicationId = it
            }
            activity?.loanApplicationId?.let {
                borrowerId = it
            }

            listAddress.clear()
            setData()

            savedViewInstance
        }
    }

    private fun setData(){
        viewModel.borrowerDetail.observe(viewLifecycleOwner, { detail ->
            if (detail != null){
                detail.borrowerData?.currentAddress?.let { currentAddress->
                    try {
                        val fromDate = if (currentAddress.fromDate != null && currentAddress.fromDate.length > 0) currentAddress.fromDate else ""
                        currentAddress.addressModel?.let { address ->
                            displayAddress(address)
                            currentAddressModel = address
                            currentAddressDetail = currentAddress

                            bi.tvResidenceDate.text = "From ".plus(AppSetting.getMonthAndYearValue(fromDate))

                            currentAddress.monthlyRent?.let {
                                if (it > 0)
                                    bi.textviewRent.text = "$".plus(Math.round(it).toString())
                                else
                                    bi.textviewRent.text = "$0"
                            } ?: run { bi.textviewRent.text = "$0" }

                            bi.tvResidence.setText(requireContext().getString(R.string.add_previous_address))

                        } ?: run { bi.tvResidence.setText(requireContext().getString(R.string.add_current_address)) }

                    } catch (e : Exception){ }

                    } ?: run { bi.tvResidence.setText(requireContext().getString(R.string.add_current_address)) }

                detail.borrowerData?.previousAddresses?.let { prevAdd->
                    for(i in 0 until prevAdd.size){
                        val fromDate = if(prevAdd.get(i).fromDate != null) prevAdd.get(i).fromDate else ""
                        val toDate = if(prevAdd.get(i).toDate != null) prevAdd.get(i).toDate else ""
                        prevAdd.get(0).addressModel?.let { address ->
                            val desc = address.street + " " + address.unit + "\n" + address.city + " " + address.stateName + " " + address.zipCode + " " + address.countryName
                            listAddress.add(PrimaryBorrowerAddress(false,desc,fromDate,toDate,null))
                        }
                    }
                }

                if(listAddress.size > 0){
                    setResidence()
                }

                detail.borrowerData?.borrowerBasicDetails?.let {
                    it.firstName?.let {
                        bi.edFirstName.setText(it)
                        CustomMaterialFields.setColor(bi.layoutFirstName, R.color.grey_color_two, requireContext())
                    }

                    it.middleName?.let {
                        bi.edMiddleName.setText(it)
                        CustomMaterialFields.setColor(bi.layoutMiddleName, R.color.grey_color_two, requireContext())
                    }

                    it.lastName?.let {
                        bi.edLastName.setText(it)
                        CustomMaterialFields.setColor(bi.layoutLastName, R.color.grey_color_two, requireContext())
                    }

                    it.suffix?.let {
                        bi.edSuffix.setText(it)
                        CustomMaterialFields.setColor(bi.layoutSuffix, R.color.grey_color_two, requireContext())
                    }

                    it.emailAddress?.let {
                        bi.edEmail.setText(it)
                        CustomMaterialFields.setColor(bi.layoutEmail, R.color.grey_color_two, requireContext())
                    }

                    it.homePhone?.let {
                        bi.edHomeNumber.setText(it)
                        CustomMaterialFields.setColor(bi.layoutHomeNum, R.color.grey_color_two, requireContext())
                    }

                    it.workPhone?.let {
                        bi.edWorkNum.setText(it)
                        CustomMaterialFields.setColor(bi.layoutWorkNum, R.color.grey_color_two, requireContext())
                    }

                    it.workPhoneExt?.let {
                        bi.edExtNum.setText(it)
                        CustomMaterialFields.setColor(bi.layoutExtNum, R.color.grey_color_two, requireContext())
                    }

                    it.cellPhone?.let {
                        bi.edCellNum.setText(it)
                        CustomMaterialFields.setColor(bi.layoutCellNum, R.color.grey_color_two, requireContext())
                    }
                }

                detail.borrowerData?.maritalStatus?.let {
                    if (it.maritalStatusId == 1)
                        msBinding.rbMarried.isChecked = true
                    if (it.maritalStatusId == 2)
                        msBinding.rbSeparated.isChecked = true
                    if (it.maritalStatusId == 9)
                        msBinding.rbUnmarried.isChecked = true
                }

                detail.borrowerData?.borrowerCitizenship?.let {
                        //Timber.e("residency type id" + it.residencyTypeId)
                        it.ssn?.let { ssn->
                            bi.edSecurityNum.setText(ssn)
                        }
                        it.dobUtc?.let { dob->
                            bi.edDateOfBirth.setText(AppSetting.getFullDate(dob))
                        }
                        if(it.residencyTypeId == 1)
                            citizenshipBinding.rbUsCitizen.isChecked = true
                        if(it.residencyTypeId ==2)
                            citizenshipBinding.rbNonPrOther.isChecked =true
                        if(it.residencyTypeId ==3){
                            citizenshipBinding.rbNonPrOther.isChecked =true
                            citizenshipBinding.visaStatusDesc.text = it.residencyStatusExplanation
                            citizenshipBinding.layoutVisaStatusOther.visibility = View.VISIBLE
                        }

                        it.dependentCount?.let { count->
                            bi.tvDependentCount.setText(count.toString())

                        it.dependentAges?.let {
                            if (it.length > 0) {
                                val strs = it.split(",").toList()
                                //Timber.e("$strs")
                                for (i in 0 until strs.size) {
                                    var ordinal = getOrdinal(listItems.size + 1)
                                    listItems.add(
                                        Dependent(
                                            ordinal.plus(" Dependent Age (Years)"),
                                            strs.get(i).toInt()
                                        )
                                    )
                                }
                                bi.rvDependents.adapter = dependentAdapter
                                dependentAdapter.notifyDataSetChanged()
                            }
                        }
                        }
                    }

                // military
                detail.borrowerData?.militaryServiceDetails?.let {
                    it.details?.let {
                        for(i in 0 until it.size){
                            if(it.get(i).militaryAffiliationId == 4){
                                bindingMilitary.chbDutyPersonel.isChecked = true
                                bindingMilitary.serviceDate.text = it.get(i).expirationDateUtc?.let { it1 ->
                                    AppSetting.getMonthAndYearValue(it1)
                                }
                                    bindingMilitary.layoutActivePersonnel.visibility = View.VISIBLE
                                }
                                if(it.get(i).militaryAffiliationId==3) {
                                    bindingMilitary.chbResNationalGuard.isChecked = true
                                    it.get(i).reserveEverActivated?.let { it2->
                                      reserveEverActivated = it2
                                    }
                                }

                                if(it.get(i).militaryAffiliationId==1)
                                    bindingMilitary.chbVeteran.isChecked = true

                                if(it.get(i).militaryAffiliationId==2)
                                    bindingMilitary.chbSurvivingSpouse.isChecked = true
                            }
                        }
                    }
                   if(detail.code.equals(AppConstant.RESPONSE_CODE_SUCCESS)){
                      hideLoader() }
                }

            hideLoader()
            })
    }

    private fun sendBorrowerData(){
        //var currentAddressFromDate = bi.tvResidenceDate.text.toString().trim()
        //var newDate = AppSetting.reverseDateFormat(currentAddressFromDate)
        //Log.e("NewDate",newDate)
        val firstName: String = bi.edFirstName.text.toString()
        val lastName: String = bi.edLastName.text.toString()
        val email: String = bi.edEmail.text.toString()
        val homeNum: String = bi.edHomeNumber.text.toString()

        if (firstName.isEmpty() || firstName.length == 0) {
            setError(bi.layoutFirstName, getString(R.string.error_field_required))
        }
        if (lastName.isEmpty() || lastName.length == 0) {
            setError(bi.layoutLastName, getString(R.string.error_field_required))
        }
        if (email.isEmpty() || email.length == 0) {
            setError(bi.layoutEmail, getString(R.string.error_field_required))
        }
        if (homeNum.isEmpty() || homeNum.length == 0) {
            setError(bi.layoutHomeNum, getString(R.string.error_field_required))
        }
        if (firstName.isNotEmpty() && firstName.length > 0) {
            clearError(bi.layoutFirstName)
        }
        if (lastName.isNotEmpty() && lastName.length > 0) {
            clearError(bi.layoutLastName)
        }
        if (email.isNotEmpty() && email.length > 0){
            if (!LoginUtil.isValidEmailAddress(email.trim())) {
                setError(bi.layoutEmail, getString(R.string.invalid_email))
            } else {
                clearError(bi.layoutEmail)
            }
        }

        if (homeNum.isNotEmpty() && homeNum.isNotBlank()) {
            if (homeNum.length < 14) {
                setError(bi.layoutHomeNum, getString(R.string.invalid_phone_num))
            } else {
                clearError(bi.layoutHomeNum)
            }
        }
        if(!bi.tvDependentCount.text.equals("0")){
            checkDependentData()
        }

        if(firstName.length >0 && lastName.length > 0 && homeNum.length > 0 && LoginUtil.isValidEmailAddress(email.trim()) &&
            loanApplicationId !=null && borrowerId !=null )
            {
            // borrower basic details
            val middleName = if(bi.edMiddleName.text.toString().trim().length >0) bi.edMiddleName.text.toString() else null // get middle name
            val suffix = if(bi.edSuffix.text.toString().trim().length >0) bi.edSuffix.text.toString() else null  // suffix
            val workPhoneNumber = if(bi.edWorkNum.text.toString().trim().length >0) bi.edWorkNum.text.toString() else null // work number
            val workExt = if(bi.edExtNum.text.toString().trim().length >0) bi.edExtNum.text.toString() else null
            val cellPhone = if(bi.edCellNum.text.toString().trim().length >0) bi.edCellNum.text.toString() else null
            // add own type id


            // current address

            var currentAddressFromDate = bi.tvResidenceDate.text.toString().trim()
            var newDate = AppSetting.reverseDateFormat(currentAddressFromDate)
            Log.e("NewDate",newDate)
            val current = CurrentAddress(loanApplicationId=loanApplicationId!!,borrowerId = borrowerId!!, addressModel = currentAddressModel

            )

//            val id: Int?,
//            val housingStatusId: Int?,
//            val fromDate: String?,
//            val isMailingAddressDifferent: Boolean?,
//            val mailingAddressModel: Any?,
//            val monthlyRent: Double?



            lifecycleScope.launchWhenStarted{
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    val basicDetails = BorrowerBasicDetails(loanApplicationId=loanApplicationId!!,borrowerId = borrowerId!!,
                        firstName = firstName,lastName = lastName,middleName = middleName,suffix = suffix,emailAddress = email,homePhone = homeNum,
                        workPhone = workPhoneNumber,workPhoneExt = workExt,cellPhone = cellPhone,ownTypeId = 0
                    )
                }
            }
        }
    }

    private fun addEmptyDependentField(){
        if(listItems.size < 99) {
            if (listItems.size > 0) {
                var ordinal = getOrdinal(listItems.size + 1)
                listItems.add(Dependent(ordinal.plus(" Dependent Age (Years)"), 0))

            } else {
                listItems.clear()
                var ordinal = getOrdinal(1)
                listItems.add(Dependent(ordinal.plus(" Dependent Age (Years)"), 0))
            }
            bi.rvDependents.adapter = dependentAdapter
            dependentAdapter.notifyDataSetChanged()
            bi.tvDependentCount.setText(listItems.size.toString())
        }
    }

    override fun onItemClick(position: Int) {
        listItems.removeAt(position)
        //update list/ recreate again
        for(i in 0 until listItems.size){
            if (i + 1 <= listItems.size) {
                var ordinal = getOrdinal(i + 1)
                var age = listItems.get(i).age
                listItems[i]= Dependent((ordinal.plus(" Dependent Age (Years)")),age)
            }
        }
        bi.rvDependents.adapter = dependentAdapter
        dependentAdapter.notifyDataSetChanged()
        bi.tvDependentCount.setText(listItems.size.toString())
        hideSoftKeyboard()
    }

    override fun onClick(view: View?) {
        //val checked = (view as RadioButton).isChecked
        when (view?.getId()) {
            R.id.rb_unmarried -> if (msBinding.rbUnmarried.isChecked) setMaritalStatus(true, false, false)
            R.id.rb_married -> if (msBinding.rbMarried.isChecked) setMaritalStatus(false, true, false)
            R.id.rb_separated -> if (msBinding.rbSeparated.isChecked) setMaritalStatus(false, false, true)
            R.id.rb_us_citizen -> if (citizenshipBinding.rbUsCitizen.isChecked) setCitizenship(true, false, false)
            R.id.rb_pr -> if (citizenshipBinding.rbPr.isChecked) setCitizenship(false, true, false)
            R.id.rb_non_pr_other -> if (citizenshipBinding.rbNonPrOther.isChecked) setCitizenship(false, false, true)
            R.id.chb_duty_personel -> militaryActivePersonel()
            //R.id.chb_res_national_guard -> militaryNationalGuard()
            R.id.chb_veteran -> militaryVeteran()
            R.id.chb_surviving_spouse -> militarySurvivingSpouse()
        }
    }

    private fun setViews() {

        msBinding.rbUnmarried.setOnClickListener(this)
        msBinding.rbMarried.setOnClickListener(this)
        msBinding.rbSeparated.setOnClickListener(this)
        citizenshipBinding.rbUsCitizen.setOnClickListener(this)
        citizenshipBinding.rbNonPrOther.setOnClickListener(this)
        citizenshipBinding.rbPr.setOnClickListener(this)
        bindingMilitary.chbDutyPersonel.setOnClickListener(this)
        bindingMilitary.chbResNationalGuard.setOnClickListener(this)
        bindingMilitary.chbVeteran.setOnClickListener(this)
        bindingMilitary.chbSurvivingSpouse.setOnClickListener(this)
        bi.addDependentClick.setOnClickListener(this)
        bi.addPrevAddress.setOnClickListener(this)
        bi.edDateOfBirth.setOnClickListener(this)

        // initialize recyclerview
        dependentAdapter = DependentAdapter(requireActivity(),listItems,this@PrimaryBorrowerInfoFragment)
        bi.rvDependents.adapter = dependentAdapter

        setupUI()
        setSingleItemFocus()
        setEndIconClicks()
        setNumberFormts()

        msBinding.unmarriedAddendum.setOnClickListener { findNavController().navigate(R.id.action_info_unmarried_addendum) }
        bindingMilitary.layoutActivePersonnel.setOnClickListener { findNavController().navigate(R.id.action_info_active_duty)}
        //bindingMilitary.layoutNationalGuard.setOnClickListener { findNavController().navigate(R.id.action_info_reserve) }
        citizenshipBinding.layoutVisaStatusOther.setOnClickListener { findNavController().navigate(R.id.action_info_non_pr) }

    }

    private fun openCurrentAddress(){

    }

    private fun setupUI(){

        bi.currentAddressLayout.setOnClickListener {
            val bundle = Bundle()
            bundle.putParcelable(AppConstant.current_address,currentAddressDetail)
            findNavController().navigate(R.id.action_info_current_address,bundle)
        }

        bi.btnSaveInfo.setOnClickListener { sendBorrowerData() }

        bi.backButtonInfo.setOnClickListener {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold,R.anim.slide_out_left)
        }

        requireActivity().onBackPressedDispatcher.addCallback {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }

        bi.mainConstraintLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(),   bi.mainConstraintLayout)
            super.removeFocusFromAllFields(bi.mainConstraintLayout)
        }

        bi.addDependentClick.setOnClickListener{ addEmptyDependentField() }

        bi.addPrevAddress.setOnClickListener {
            if (bi.tvResidence.text.equals(getString(R.string.current_address))) {
                findNavController().navigate(R.id.action_info_current_address)
            } else {
                findNavController().navigate(R.id.action_info_previous_address)
            }
        }

        // check listeners
        msBinding.rbUnmarried.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                msBinding.rbUnmarried.setTypeface(null, Typeface.BOLD)
            else
                msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
        }
        //married
        msBinding.rbMarried.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                msBinding.rbMarried.setTypeface(null, Typeface.BOLD)
            else
                msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
        }
        //separated
        msBinding.rbSeparated.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                msBinding.rbSeparated.setTypeface(null, Typeface.BOLD)
            else
                msBinding.rbSeparated.setTypeface(null, Typeface.NORMAL)
        }
        //us citizen
        citizenshipBinding.rbUsCitizen.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.BOLD)
            else
                citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.NORMAL)
        }


        // non permanent residence
        citizenshipBinding.rbNonPrOther.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.BOLD)
            else
                citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.NORMAL)
        }
        //permanent residence
        citizenshipBinding.rbPr.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                citizenshipBinding.rbPr.setTypeface(null, Typeface.BOLD)
            else
                citizenshipBinding.rbPr.setTypeface(null, Typeface.NORMAL)
        }
        // active duty personel
        bindingMilitary.chbDutyPersonel.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                bindingMilitary.chbDutyPersonel.setTypeface(null, Typeface.BOLD)
            else
                bindingMilitary.chbDutyPersonel.setTypeface(null, Typeface.NORMAL)
        }
        //reserve or national guard
        bindingMilitary.chbResNationalGuard.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                bindingMilitary.chbResNationalGuard.setTypeface(null, Typeface.BOLD)
                bindingMilitary.layoutNationalGuard.visibility = View.VISIBLE
            }
            else {
                bindingMilitary.chbResNationalGuard.setTypeface(null, Typeface.NORMAL)
                bindingMilitary.layoutNationalGuard.visibility = View.GONE
            }
        }
        // veteran
        bindingMilitary.chbVeteran.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                bindingMilitary.chbVeteran.setTypeface(null, Typeface.BOLD)
            else
                bindingMilitary.chbVeteran.setTypeface(null, Typeface.NORMAL)
        }

        //surviving spouse
        bindingMilitary.chbSurvivingSpouse.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                bindingMilitary.chbSurvivingSpouse.setTypeface(null, Typeface.BOLD)
            else
                bindingMilitary.chbSurvivingSpouse.setTypeface(null, Typeface.NORMAL)
        }

        bindingMilitary.chbResNationalGuard.setOnClickListener {
            val bundle = Bundle()
            reserveEverActivated?.let { bundle.putBoolean(AppConstant.RESERVE_ACTIVATED, it) }
            findNavController().navigate(R.id.action_info_reserve,bundle)
            bindingMilitary.layoutNationalGuard.visibility = View.VISIBLE
        }

        bindingMilitary.layoutNationalGuard.setOnClickListener {
            val bundle = Bundle()
            reserveEverActivated?.let { bundle.putBoolean(AppConstant.RESERVE_ACTIVATED, it) }
            findNavController().navigate(R.id.action_info_reserve,bundle)
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
            AppCompatResources.getColorStateList(requireContext(), R.color.primary_info_line_color))
    }

    private fun setEndIconClicks() {
        bi.layoutDateOfBirth.setEndIconOnClickListener(View.OnClickListener {
            openCalendar()
        })
        // click for security number
        bi.layoutSecurityNum.setEndIconOnClickListener(View.OnClickListener {
            if (bi.edSecurityNum.getTransformationMethod()
                    .equals(PasswordTransformationMethod.getInstance())
            ) { //  hide password
                bi.edSecurityNum.setTransformationMethod(HideReturnsTransformationMethod.getInstance())
                bi.layoutSecurityNum.setEndIconDrawable(R.drawable.ic_eye_hide)
            } else {
                bi.edSecurityNum.setTransformationMethod(PasswordTransformationMethod.getInstance())
                bi.layoutSecurityNum.setEndIconDrawable(R.drawable.ic_eye_icon_svg)
            }
        })

    }

    @SuppressLint("ClickableViewAccessibility")
    private fun setResidence() {
        // set btn text

        //bi.recyclerview.layoutManager = LinearLayoutManager(requireContext(), RecyclerView.VERTICAL, false)
        //bi.recyclerview.hasFixedSize()
        adapter = BorrowerAddressAdapter(requireActivity())
        adapter.setTaskList(listAddress)
        bi.recyclerview.setAdapter(adapter)

        touchListener = RecyclerTouchListener(requireActivity(), bi.recyclerview)
        touchListener!!
            .setClickable(object : RecyclerTouchListener.OnRowClickListener {
                override fun onRowClicked(position: Int){
                    if(listAddress.get(position).isCurrentAddress){
                        addressBtnText = getString(R.string.previous_address)
                        val bundle = Bundle()
                        bundle.putString(AppConstant.showData,AppConstant.showData)
                        findNavController().navigate(R.id.action_info_current_address,bundle)
                    }
                    else {
                        if(listAddress.get(0).isCurrentAddress){
                            addressBtnText = getString(R.string.previous_address)
                        } else {
                            addressBtnText = getString(R.string.current_address)
                        }
                        val bundle = Bundle()
                        bundle.putInt(AppConstant.address,position)
                        findNavController().navigate(R.id.action_info_previous_address,bundle)
                    }
                }
                override fun onIndependentViewClicked(independentViewID: Int, position: Int) {}
            })
            .setSwipeOptionViews(R.id.delete_task)
            .setSwipeable(R.id.rowFG, R.id.rowBG, object :
                RecyclerTouchListener.OnSwipeOptionsClickListener {

                override fun onSwipeOptionClicked(viewID: Int, position: Int) {
                    var text = if(listAddress[position].isCurrentAddress) getString(R.string.delete_current_address) else getString(R.string.delete_prev_address)
                    selectedPosition = position
                    DeleteCurrentResidenceDialogFragment.newInstance(text).show(childFragmentManager, DeleteCurrentResidenceDialogFragment::class.java.canonicalName)
                }
            })
        bi.recyclerview.addOnItemTouchListener(touchListener!!)


        // disable touch from recyclerview
        bi.recyclerview.setOnTouchListener(object : View.OnTouchListener {
            override fun onTouch(v: View, m: MotionEvent): Boolean {
                bi.scrollPrimaryInfo.requestDisallowInterceptTouchEvent(true)
                return false
            }
        })
    }

    private fun setSingleItemFocus(){

        // check first name
        bi.edFirstName.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                setTextInputLayoutHintColor(bi.layoutFirstName, R.color.grey_color_two )
            } else {
                if (bi.edFirstName.text?.length == 0) {
                    setTextInputLayoutHintColor(bi.layoutFirstName,R.color.grey_color_three)
                } else {
                    setTextInputLayoutHintColor(bi.layoutFirstName,R.color.grey_color_two)
                    clearError(bi.layoutFirstName)
                }
            }
        }

        // last name
        bi.edLastName.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                setTextInputLayoutHintColor(bi.layoutLastName, R.color.grey_color_two )
            } else {
                if (bi.edLastName.text?.length == 0) {
                    setTextInputLayoutHintColor(bi.layoutLastName,R.color.grey_color_three)
                } else {
                    setTextInputLayoutHintColor(bi.layoutLastName,R.color.grey_color_two)
                    clearError(bi.layoutLastName)
                }
            }
        }

        // home number
        bi.edHomeNumber.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                setTextInputLayoutHintColor(bi.layoutHomeNum, R.color.grey_color_two )
            } else {
                if (bi.edHomeNumber.text?.length == 0) {
                    setTextInputLayoutHintColor(bi.layoutHomeNum,R.color.grey_color_three)
                } else {
                    setTextInputLayoutHintColor(bi.layoutHomeNum,R.color.grey_color_two)
                    if (bi.edHomeNumber.text?.length!! < 14) {
                        setError(bi.layoutHomeNum, getString(R.string.invalid_phone_num))
                    } else {
                        clearError(bi.layoutHomeNum)
                    }
                }
            }
        }

        // email
        bi.edEmail.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                setTextInputLayoutHintColor(bi.layoutEmail, R.color.grey_color_two)
            } else {
                if (bi.edEmail.text?.length == 0) {
                    setTextInputLayoutHintColor(bi.layoutEmail, R.color.grey_color_three)
                } else {
                    setTextInputLayoutHintColor(bi.layoutEmail, R.color.grey_color_two)

                    if (!isValidEmailAddress(bi.edEmail.text.toString())) {
                        setError(bi.layoutEmail, getString(R.string.invalid_email))
                    } else {
                        clearError(bi.layoutEmail)
                    }
                }
            }
        }

        bi.edDateOfBirth.showSoftInputOnFocus = false
        bi.edDateOfBirth.setOnClickListener { openCalendar() }
        bi.edDateOfBirth.setOnFocusChangeListener{ _ , _ ->  openCalendar() }

        bi.edMiddleName.setOnFocusChangeListener(CustomFocusListenerForEditText(bi.edMiddleName, bi.layoutMiddleName, requireContext()))
        bi.edSuffix.setOnFocusChangeListener(CustomFocusListenerForEditText(bi.edSuffix, bi.layoutSuffix, requireContext()))
        bi.edWorkNum.setOnFocusChangeListener(CustomFocusListenerForEditText(bi.edWorkNum,bi.layoutWorkNum, requireContext()))
        bi.edExtNum.setOnFocusChangeListener(CustomFocusListenerForEditText(bi.edExtNum, bi.layoutExtNum, requireContext()))
        bi.edCellNum.setOnFocusChangeListener(CustomFocusListenerForEditText(bi.edCellNum, bi.layoutCellNum, requireContext()))
        bi.edSecurityNum.setOnFocusChangeListener(CustomFocusListenerForEditText(bi.edSecurityNum,bi.layoutSecurityNum, requireContext()))
        bi.edDateOfBirth.setOnFocusChangeListener(CustomFocusListenerForEditText(bi.edDateOfBirth, bi.layoutDateOfBirth, requireContext()))

    }

    private fun checkDependentData(){

        var textInputLayout : TextInputLayout
        for(item in 0 until bi.rvDependents.childCount){

            textInputLayout = bi.rvDependents.layoutManager?.findViewByPosition(item)?.findViewById<TextInputLayout>(R.id.til_dependent)!!
            var text =  textInputLayout.editText?.text.toString()
            if(text.isEmpty() || text.isBlank()){
                textInputLayout.helperText = getString(R.string.error_field_required)
                textInputLayout.setBoxStrokeColorStateList(AppCompatResources.getColorStateList(requireContext(), R.color.primary_info_stroke_error_color))

            } else if(text.length>0){
                clearError(textInputLayout)
            }
        }
    }

    private fun setTextInputLayoutHintColor(textInputLayout: TextInputLayout, @ColorRes colorIdRes: Int) {
        textInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), colorIdRes))
    }

    private fun setMaritalStatus(isUnmarried: Boolean, isMarried: Boolean, isDivorced: Boolean) {
        if (isUnmarried) {
            findNavController().navigate(R.id.action_info_unmarried_addendum)
            msBinding.unmarriedAddendum.visibility = View.VISIBLE
            msBinding.rbUnmarried.setTypeface(null, Typeface.BOLD)
            msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbSeparated.setTypeface(null, Typeface.NORMAL)

        }
        if (isMarried) {
            msBinding.unmarriedAddendum.visibility = View.GONE
            msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbMarried.setTypeface(null, Typeface.BOLD)
            msBinding.rbSeparated.setTypeface(null, Typeface.NORMAL)
        }
        if (isDivorced) {
            msBinding.unmarriedAddendum.visibility = View.GONE
            msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbSeparated.setTypeface(null, Typeface.BOLD)
        }
    }

    private fun setCitizenship(usCitizen: Boolean, PR: Boolean, nonPR: Boolean) {
        if (usCitizen) {
            citizenshipBinding.layoutVisaStatusOther.visibility = View.GONE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.BOLD)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.NORMAL)
        }
        if (PR) {
            citizenshipBinding.layoutVisaStatusOther.visibility = View.GONE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.BOLD)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.NORMAL)
        }
        if (nonPR) {
            findNavController().navigate(R.id.action_info_non_pr)
            citizenshipBinding.layoutVisaStatusOther.visibility = View.VISIBLE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.BOLD)
        }
    }

    private fun militaryActivePersonel() {

        if (bindingMilitary.chbDutyPersonel.isChecked) {
            findNavController().navigate(R.id.action_info_active_duty)
            bindingMilitary.layoutActivePersonnel.visibility = View.VISIBLE
            bindingMilitary.chbDutyPersonel.setTypeface(null, Typeface.BOLD)

        } else {
            bindingMilitary.layoutActivePersonnel.visibility = View.GONE
            bindingMilitary.chbDutyPersonel.setTypeface(null, Typeface.NORMAL)
        }
    }

    /*private fun militaryNationalGuard() {
        if (bindingMilitary.chbResNationalGuard.isChecked) {
            val bundle = Bundle()
            reserveEverActivated?.let { bundle.putBoolean(AppConstant.RESERVE_ACTIVATED, it) }
            findNavController().navigate(R.id.action_info_reserve,bundle)
            bindingMilitary.layoutNationalGuard.visibility = View.VISIBLE
            bindingMilitary.chbResNationalGuard.setTypeface(null, Typeface.BOLD)

        } else {
            bindingMilitary.layoutNationalGuard.visibility = View.GONE
            bindingMilitary.chbResNationalGuard.setTypeface(null, Typeface.NORMAL)
        }
    } */

    private fun militaryVeteran() {
        if (bindingMilitary.chbVeteran.isChecked) {
            bindingMilitary.chbVeteran.setTypeface(null, Typeface.BOLD)

        } else {
            bindingMilitary.chbVeteran.setTypeface(null, Typeface.NORMAL)
        }
    }

    private fun militarySurvivingSpouse() {
        if (bindingMilitary.chbSurvivingSpouse.isChecked) {
            bindingMilitary.chbSurvivingSpouse.setTypeface(null, Typeface.BOLD)

        } else {
            bindingMilitary.chbSurvivingSpouse.setTypeface(null, Typeface.NORMAL)
        }
    }

    private fun setNumberFormts(){
        bi.edHomeNumber.addTextChangedListener(PhoneTextFormatter(bi.edHomeNumber, "(###) ###-####"))
        bi.edWorkNum.addTextChangedListener(PhoneTextFormatter(bi.edWorkNum, "(###) ###-####"))
        bi.edCellNum.addTextChangedListener(PhoneTextFormatter(bi.edCellNum, "(###) ###-####"))

        // security number format
        bi.edSecurityNum.addTextChangedListener(object : TextWatcher {
            var beforeLength = 0
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {
                beforeLength = bi.edSecurityNum.length()
            }

            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {
                val digits: Int = bi.edSecurityNum.getText().toString().length
                if (beforeLength < digits && (digits == 3 || digits == 6)) {
                    bi.edSecurityNum.append("-")
                }
            }

            override fun afterTextChanged(s: Editable) {}
        })

    }

    private fun displayAddress(address: AddressModel){
        val builder = StringBuilder()
        address.street?.let {
            if(it != "null")
                builder.append(it).append(" ")
        }
        address.unit?.let {
            if(it != "null")
                builder.append(it).append(",")
            else
                builder.append(",")

        } ?: run { builder.append(",") }

        address.city?.let {
            if(it != "null")
                builder.append("\n").append(it).append(",").append(" ")
        } ?: run { builder.append("\n") }

        address.stateName?.let {
            if(it !="null") builder.append(it).append(" ")
        }
        address.zipCode?.let {
            if(it != "null")
                builder.append(it)
        }
        bi.textviewCurrentAddress.text = builder

    }

    private fun isValidEmailAddress(email: String?): Boolean {
        val ePattern =
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,3}))$"
        val p = Pattern.compile(ePattern)
        val m = p.matcher(email)
        return m.matches()
    }

    private fun openCalendar(){
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)

        /*

        //  datePicker.findViewById(Resources.getSystem().getIdentifier("day", "id", "android")).setVisibility(View.GONE);

        val dpd = DatePickerDialog(requireActivity(), { view, selectedYear, monthOfYear, dayOfMonth -> bi.edDateOfBirth.setText("" + monthOfYear + "-" + dayOfMonth + "-" + selectedYear) },
            year,
            month,
            day
        )
        dpd.datePicker.spinnersShown = true
        dpd.getDatePicker().setCalendarViewShown(false);
        dpd.show()
        */

        // New Style Calendar Added....
        val datePickerDialog = DatePickerDialog(
            requireActivity(), R.style.MySpinnerDatePickerStyle,
            { view, selectedYear, monthOfYear, dayOfMonth -> bi.edDateOfBirth.setText("" + (monthOfYear+1) + "-" + dayOfMonth + "-" + selectedYear) }
            , year, month, day
        )
        datePickerDialog.show()
    }

    override fun onResume() {
        super.onResume()
        touchListener?.let { bi.recyclerview.addOnItemTouchListener(it) }
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
    fun onSwipeDeleteReceivedEvent(event: SwipeToDeleteEvent){
        if(event.boolean){
            selectedPosition?.let {
                bi.tvResidence.setText(getString(R.string.previous_address))
            /*if(listAddress.get(selectedPosition!!).isCurrentAddress) {
                    bi.tvResidence.setText(getString(R.string.current_address))
                } else {
                    if (listAddress.get(0).isCurrentAddress) {
                        bi.tvResidence.setText(getString(R.string.previous_address))
                    } else {
                        bi.tvResidence.setText(getString(R.string.current_address))
                    }
                } */
            }
            listAddress.removeAt(selectedPosition!!)
            adapter.setTaskList(listAddress)

        }
    }

    fun getOrdinal(i: Int): String? {
        val mod100 = i % 100
        val mod10 = i % 10
        if (mod10 == 1 && mod100 != 11) {
            return i.toString() + "st"
        } else if (mod10 == 2 && mod100 != 12) {
            return i.toString() + "nd"
        } else if (mod10 == 3 && mod100 != 13) {
            return i.toString() + "rd"
        } else {
            return  i.toString() + "th"
        }
    }

    private fun hideSoftKeyboard(){
        val imm = view?.let { ContextCompat.getSystemService(it.context, InputMethodManager::class.java) }
        imm?.hideSoftInputFromWindow(view?.windowToken, 0)
    }

    override fun onAddressClick(position: Int) {
       // Log.e("callback", "here")
    }

    private fun hideLoader(){
        val  activity = (activity as? BorrowerAddressActivity)
        activity?.binding?.loaderInfo?.visibility = View.GONE
    }


    // done button click of add dependents
    /*bi.edDependents.setOnEditorActionListener { _, actionId, _ ->
        if (actionId == EditorInfo.IME_ACTION_DONE) {
            addDependentField()
            requireActivity().getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN)
        }
        false
    } */



}