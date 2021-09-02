package com.rnsoft.colabademo.activities.borroweraddresses.info

import android.annotation.SuppressLint
import android.app.DatePickerDialog
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
import androidx.annotation.ColorRes
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.activities.borroweraddresses.info.*
import com.rnsoft.colabademo.activities.borroweraddresses.info.adapter.DependentAdapter
import com.rnsoft.colabademo.activities.borroweraddresses.info.fragment.DeleteCurrentResidenceDialogFragment
import com.rnsoft.colabademo.activities.borroweraddresses.info.fragment.SwipeToDeleteEvent
import com.rnsoft.colabademo.activities.borroweraddresses.info.model.Address
import com.rnsoft.colabademo.activities.borroweraddresses.info.model.Dependent
import com.rnsoft.colabademo.databinding.*
import com.rnsoft.colabademo.utils.RecyclerTouchListener
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.util.*
import java.util.regex.Pattern
import kotlin.collections.ArrayList


/**
 * Created by Anita Kiran on 8/23/2021.
 */

class PrimaryBorrowerInfoFragment : Fragment(), RecyclerviewClickListener, View.OnClickListener, AddressClickListener {

    lateinit var bi: PrimaryBorrowerInfoLayoutBinding
    lateinit var msBinding: SublayoutMaritalStatusBinding
    lateinit var citizenshipBinding: SublayoutCitizenshipBinding
    lateinit var bindingMilitary: SubLayoutMilitaryBinding
    var list: ArrayList<Address> = ArrayList()
    private var touchListener: RecyclerTouchListener? = null
    var count : Int = 0
    var selectedPosition : Int?=null
    val listItems = ArrayList<Dependent>()
    lateinit var adapter:ResidenceAdapter
    lateinit var dependentAdapter: DependentAdapter
    var isMaritalStatusVisible : Boolean = false
    var isResActiveDuty : Boolean = false
    var isNationalGuard : Boolean = false
    var isVisaOther : Boolean = false
    var isAddressLoaded :Boolean = false
    var addressBtnText : String = "Add Previous Address"
    var isBtnSet : Boolean = false

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        bi = PrimaryBorrowerInfoLayoutBinding.inflate(inflater, container, false)
        msBinding = bi.layoutMaritalStatus
        citizenshipBinding = bi.layoutCitizenship
        bindingMilitary = bi.layoutMilitaryService

        setViews()
        setResidence()

        if(isMaritalStatusVisible){
            msBinding.unmarriedAddendum.visibility = View.VISIBLE
        }

        if(isResActiveDuty) {
            bindingMilitary.layoutActivePersonnel.visibility = View.VISIBLE
        }

        if(isNationalGuard){
            bindingMilitary.layoutNationalGuard.visibility = View.VISIBLE
        }
        if(isVisaOther){
            citizenshipBinding.layoutVisaStatusOther.visibility = View.VISIBLE
        }

        if(!isAddressLoaded){
            list.clear()
            list.add(Address(true,"5919 Trussvile Crossings Parkways, ZV Street, Birmingham AL 35235"))
            list.add(Address(false,"5919 Trussvile Crossings Pkwy, Birmingham AL 35235"))
        }

        bi.tvResidence.setText(addressBtnText)

//        if(!isBtnSet){
//            bi.tvResidence.setText(getString(R.string.previous_address))
//        }

        return bi.root
    }

    private fun setViews() {

        bi.backButton.setOnClickListener(this)
        bi.btnSaveInfo.setOnClickListener(this)
        bi.edDateOfBirth.setOnClickListener(this)
        msBinding.rbUnmarried.setOnClickListener(this)
        msBinding.rbMarried.setOnClickListener(this)
        msBinding.rbDivorced.setOnClickListener(this)
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

        setSingleItemFocus()
        setEndIconClicks()
        setNumberFormts()

        bi.mainConstraintLayout.setOnClickListener { hideSoftKeyboard() }
        msBinding.unmarriedAddendum.setOnClickListener { findNavController().navigate(R.id.navigation_unmarried) }
        bindingMilitary.layoutActivePersonnel.setOnClickListener { findNavController().navigate(R.id.navigation_active_duty)}
        bindingMilitary.layoutNationalGuard.setOnClickListener { findNavController().navigate(R.id.navigation_reserve) }
        citizenshipBinding.layoutVisaStatusOther.setOnClickListener { findNavController().navigate(R.id.navigation_non_permanent) }

    }

    private fun addEmptyDependentField() {
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

    override fun deleteDependentClick(position: Int) {
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
            R.id.rb_divorced -> if (msBinding.rbDivorced.isChecked) setMaritalStatus(false, false, true)
            R.id.rb_us_citizen -> if (citizenshipBinding.rbUsCitizen.isChecked) setCitizenship(true, false, false)
            R.id.rb_pr -> if (citizenshipBinding.rbPr.isChecked) setCitizenship(false, true, false)
            R.id.rb_non_pr_other -> if (citizenshipBinding.rbNonPrOther.isChecked) setCitizenship(false, false, true)
            R.id.chb_duty_personel -> militaryActivePersonel()
            R.id.chb_res_national_guard -> militaryNationalGuard()
            R.id.chb_veteran -> militaryVeteran()
            R.id.chb_surviving_spouse -> militarySurvivingSpouse()
            R.id.btn_save_info -> checkValidations()
            R.id.add_dependent_click -> addEmptyDependentField()
            R.id.add_prev_address ->  if(bi.tvResidence.text.equals(getString(R.string.current_address))){

                findNavController().navigate(R.id.navigation_current_address)
            } else {
                findNavController().navigate(R.id.navigation_mailing_address)
            }


            R.id.backButton -> requireActivity().finish()
            // R.id.ed_dateOfBirth -> openCalendar()
        }
    }

    private fun checkValidations() {
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
        if (email.isNotEmpty() && email.length > 0) {
            if (!isValidEmailAddress(email)) {
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

    }

    fun setError(textInputlayout: TextInputLayout, errorMsg: String) {
        textInputlayout.helperText = errorMsg
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(requireContext(), R.color.primary_info_stroke_error_color))
        //binding.layoutLastName.boxStrokeColor = ContextCompat.getColor(this, R.color.colaba_red_color)
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

        adapter = ResidenceAdapter(requireActivity(), this)
        adapter.setTaskList(list)
        bi.recyclerview.setAdapter(adapter)

        touchListener = RecyclerTouchListener(requireActivity(), bi.recyclerview)
        touchListener!!
            .setClickable(object : RecyclerTouchListener.OnRowClickListener {
                override fun onRowClicked(position: Int) {

                    if(list.get(position).isCurrentAddress){
                        addressBtnText = getString(R.string.previous_address)
                        findNavController().navigate(R.id.navigation_current_address)
                    }
                    else {
                        if(list.get(0).isCurrentAddress) {
                            addressBtnText = getString(R.string.previous_address)
                        } else {
                            addressBtnText = getString(R.string.current_address)
                        }
                        findNavController().navigate(R.id.navigation_mailing_address)
                    }
                }
                override fun onIndependentViewClicked(independentViewID: Int, position: Int) {}
            })
            .setSwipeOptionViews(R.id.delete_task)
            .setSwipeable(R.id.rowFG, R.id.rowBG, object :
                RecyclerTouchListener.OnSwipeOptionsClickListener {

                override fun onSwipeOptionClicked(viewID: Int, position: Int) {
                    var text = if(list[position].isCurrentAddress) getString(R.string.delete_current_address) else getString(R.string.delete_prev_address)
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

        // date of birth
        /*bi.edDateOfBirth.setOnTouchListener(object : View.OnTouchListener {
            override fun onTouch(v: View, m: MotionEvent): Boolean {
                when (m.getAction()) {
                    MotionEvent.ACTION_DOWN -> openCalendar()
                }
                return false
            }
        }) */

        bi.edDateOfBirth.showSoftInputOnFocus = false
        bi.edDateOfBirth.setOnClickListener { openCalendar() }
        bi.edDateOfBirth.setOnFocusChangeListener{ _ , _ ->  openCalendar() }

        bi.edMiddleName.setOnFocusChangeListener(MyCustomFocusListener(bi.edMiddleName, bi.layoutMiddleName, requireContext()))
        bi.edSuffix.setOnFocusChangeListener(MyCustomFocusListener(bi.edSuffix, bi.layoutSuffix, requireContext()))
        bi.edWorkNum.setOnFocusChangeListener(MyCustomFocusListener(bi.edWorkNum,bi.layoutWorkNum, requireContext()))
        bi.edExtNum.setOnFocusChangeListener(MyCustomFocusListener(bi.edExtNum, bi.layoutExtNum, requireContext()))
        bi.edCellNum.setOnFocusChangeListener(MyCustomFocusListener(bi.edCellNum, bi.layoutCellNum, requireContext()))
        bi.edSecurityNum.setOnFocusChangeListener(MyCustomFocusListener(bi.edSecurityNum,bi.layoutSecurityNum, requireContext()))
        bi.edDateOfBirth.setOnFocusChangeListener(MyCustomFocusListener(bi.edDateOfBirth, bi.layoutDateOfBirth, requireContext()))

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
            findNavController().navigate(R.id.navigation_unmarried)
            msBinding.unmarriedAddendum.visibility = View.VISIBLE
            isMaritalStatusVisible = true
            msBinding.rbUnmarried.setTypeface(null, Typeface.BOLD)
            msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbDivorced.setTypeface(null, Typeface.NORMAL)

        }
        if (isMarried) {
            isMaritalStatusVisible=false
            msBinding.unmarriedAddendum.visibility = View.GONE
            msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbMarried.setTypeface(null, Typeface.BOLD)
            msBinding.rbDivorced.setTypeface(null, Typeface.NORMAL)
        }
        if (isDivorced) {
            isMaritalStatusVisible=false
            msBinding.unmarriedAddendum.visibility = View.GONE
            msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbDivorced.setTypeface(null, Typeface.BOLD)
        }
    }

    private fun setCitizenship(usCitizen: Boolean, PR: Boolean, nonPR: Boolean) {
        if (usCitizen) {
            isVisaOther = false
            citizenshipBinding.layoutVisaStatusOther.visibility = View.GONE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.BOLD)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.NORMAL)
        }
        if (PR) {
            isVisaOther = false
            citizenshipBinding.layoutVisaStatusOther.visibility = View.GONE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.BOLD)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.NORMAL)
        }
        if (nonPR) {
            findNavController().navigate(R.id.navigation_non_permanent)
            isVisaOther = true
            citizenshipBinding.layoutVisaStatusOther.visibility = View.VISIBLE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.BOLD)
        }
    }

    private fun militaryActivePersonel() {

        if (bindingMilitary.chbDutyPersonel.isChecked) {
            findNavController().navigate(R.id.navigation_active_duty)
            isResActiveDuty = true
            bindingMilitary.layoutActivePersonnel.visibility = View.VISIBLE
            bindingMilitary.chbDutyPersonel.setTypeface(null, Typeface.BOLD)

        } else {
            isResActiveDuty = false
            bindingMilitary.layoutActivePersonnel.visibility = View.GONE
            bindingMilitary.chbDutyPersonel.setTypeface(null, Typeface.NORMAL)
        }
    }

    private fun militaryNationalGuard() {

        if (bindingMilitary.chbResNationalGuard.isChecked) {
            findNavController().navigate(R.id.navigation_reserve)
            isNationalGuard = true
            bindingMilitary.layoutNationalGuard.visibility = View.VISIBLE
            bindingMilitary.chbResNationalGuard.setTypeface(null, Typeface.BOLD)

        } else {
            isNationalGuard= false
            bindingMilitary.layoutNationalGuard.visibility = View.GONE
            bindingMilitary.chbResNationalGuard.setTypeface(null, Typeface.NORMAL)
        }
    }

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

    private fun isValidEmailAddress(email: String?): Boolean {
        val ePattern =
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$"
        val p = Pattern.compile(ePattern)
        val m = p.matcher(email)
        return m.matches()
    }

    private fun openCalendar(){
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val newMonth = month + 1
        //  datePicker.findViewById(Resources.getSystem().getIdentifier("day", "id", "android")).setVisibility(View.GONE);

        val dpd = DatePickerDialog(requireActivity(), { view, year, monthOfYear, dayOfMonth -> bi.edDateOfBirth.setText("" + newMonth + "-" + dayOfMonth + "-" + year) },
            year,
            month,
            day
        )

        dpd.show()

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
    fun onSwipeDeleteReceivedEvent(event: SwipeToDeleteEvent) {
        if(event.boolean){
            isAddressLoaded = true
            selectedPosition?.let {
                if (list.get(selectedPosition!!).isCurrentAddress) {
                    bi.tvResidence.setText(getString(R.string.current_address))
                } else {
                    if (list.get(0).isCurrentAddress) {
                        bi.tvResidence.setText(getString(R.string.previous_address))
                    } else {
                        bi.tvResidence.setText(getString(R.string.current_address))
                    }
                }
            }
            list.removeAt(selectedPosition!!)
            adapter.setTaskList(list)

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
        Log.e("callback", "here")



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