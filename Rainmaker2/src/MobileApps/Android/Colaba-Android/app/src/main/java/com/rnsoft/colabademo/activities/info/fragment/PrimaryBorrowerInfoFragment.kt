package com.rnsoft.colabademo.activities.info

import android.annotation.SuppressLint
import android.app.DatePickerDialog
import android.content.Context
import android.content.res.ColorStateList
import android.graphics.Typeface
import android.os.Bundle
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.view.*
import android.view.inputmethod.EditorInfo
import android.widget.LinearLayout
import androidx.annotation.ColorRes
import androidx.appcompat.content.res.AppCompatResources
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.MyCustomFocusListener
import com.rnsoft.colabademo.PhoneTextFormatter
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.activities.info.*
import com.rnsoft.colabademo.activities.info.model.Address
import com.rnsoft.colabademo.databinding.*
import com.rnsoft.colabademo.utils.RecyclerTouchListener
import java.util.*
import java.util.regex.Pattern
import kotlin.collections.ArrayList

/**
 * Created by Anita Kiran on 8/23/2021.
 */

class PrimaryBorrowerInfoFragment : Fragment(), View.OnClickListener {

    lateinit var bi: PrimaryBorrowerInfoLayoutBinding
    lateinit var msBinding: SublayoutMaritalStatusBinding
    lateinit var citizenshipBinding: SublayoutCitizenshipBinding
    lateinit var bindingMilitary: SubLayoutMilitaryBinding
    lateinit var bindingDependents: DependentInputFieldBinding
    var list: ArrayList<Address> = ArrayList()
    private var touchListener: RecyclerTouchListener? = null


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        bi = PrimaryBorrowerInfoLayoutBinding.inflate(inflater, container, false)
        msBinding = bi.layoutMaritalStatus
        citizenshipBinding = bi.layoutCitizenship
        bindingMilitary = bi.layoutMilitaryService
        bindingDependents = bi.layoutAddDependents

        initViews()
        setSingleItemFocus()
        setEndIconClicks()
        setResidence()


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

        // done button click of add dependents
        bi.edDependents.setOnEditorActionListener { _, actionId, _ ->
            if (actionId == EditorInfo.IME_ACTION_DONE) {
                addDependentField()
                requireActivity().getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN)
            }
            false
        }


        return bi.root

    }


    private fun initViews() {

        bi.edHomeNumber.addTextChangedListener(PhoneTextFormatter(bi.edHomeNumber, "(###) ###-####"))
        bi.edWorkNum.addTextChangedListener(PhoneTextFormatter(bi.edWorkNum, "(###) ###-####"))
        bi.edCellNum.addTextChangedListener(PhoneTextFormatter(bi.edCellNum, "(###) ###-####"))

        bi.edMiddleName.setOnFocusChangeListener(MyCustomFocusListener(bi.edMiddleName, bi.layoutMiddleName, requireContext()))
        bi.edSuffix.setOnFocusChangeListener(MyCustomFocusListener(bi.edSuffix, bi.layoutSuffix, requireContext()))
        bi.edWorkNum.setOnFocusChangeListener(MyCustomFocusListener(bi.edWorkNum,bi.layoutWorkNum, requireContext()))
        bi.edExtNum.setOnFocusChangeListener(MyCustomFocusListener(bi.edExtNum, bi.layoutExtNum, requireContext()))
        bi.edCellNum.setOnFocusChangeListener(MyCustomFocusListener(bi.edCellNum, bi.layoutCellNum, requireContext()))
        bi.edSecurityNum.setOnFocusChangeListener(MyCustomFocusListener(bi.edSecurityNum,bi.layoutSecurityNum, requireContext()))
        bi.edDependents.setOnFocusChangeListener(MyCustomFocusListener(bi.edDependents, bi.layoutDependants, requireContext()))
        bi.edDateOfBirth.setOnFocusChangeListener(MyCustomFocusListener(bi.edDateOfBirth, bi.layoutDateOfBirth, requireContext()))

        bindingDependents.edDependent1.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent1, bindingDependents.layoutDependent1, requireContext()))
        bindingDependents.edDependent2.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent2, bindingDependents.layoutDependent2, requireContext()))
        bindingDependents.edDependent3.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent3, bindingDependents.layoutDependent3, requireContext()))
        bindingDependents.edDependent4.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent4, bindingDependents.layoutDependent4, requireContext()))
        bindingDependents.edDependent5.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent5, bindingDependents.layoutDependent5, requireContext()))
        bindingDependents.edDependent6.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent6, bindingDependents.layoutDependent6, requireContext()))
        bindingDependents.edDependent7.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent7, bindingDependents.layoutDependent7, requireContext()))
        bindingDependents.edDependent8.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent8, bindingDependents.layoutDependent8, requireContext()))
        bindingDependents.edDependent9.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent9, bindingDependents.layoutDependent9, requireContext()))
        bindingDependents.edDependent10.setOnFocusChangeListener(MyCustomFocusListener(bindingDependents.edDependent10, bindingDependents.layoutDependent10, requireContext()))

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
            R.id.backButton -> requireActivity().finish()
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
    }
    fun setError(textInputlayout: TextInputLayout, errorMsg: String) {
        textInputlayout.helperText = errorMsg
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(requireContext(), R.color.primary_info_stroke_error_color
            )
        )
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

    private fun setResidence() {

        list.add(Address("5919 Trussvile Crossings Parkways, ZV Street, Birmingham AL 35235", "West Road"))
        list.add(Address("5919 Trussvile Crossings Pkwy, Birmingham AL 35235", "West Road"))
        bi.recyclerview.layoutManager = LinearLayoutManager(requireContext(), RecyclerView.VERTICAL, false)
        bi.recyclerview.hasFixedSize()

        val adapter = ResidenceAdapter(requireActivity())
        adapter.setTaskList(list)
        bi.recyclerview.setAdapter(adapter)

        touchListener = RecyclerTouchListener(requireActivity(), bi.recyclerview)
        touchListener!!
            .setClickable(object : RecyclerTouchListener.OnRowClickListener {
                override fun onRowClicked(position: Int) { }
                override fun onIndependentViewClicked(independentViewID: Int, position: Int) {}
            })
            .setSwipeOptionViews(R.id.delete_task)
            .setSwipeable(R.id.rowFG, R.id.rowBG, object :
                RecyclerTouchListener.OnSwipeOptionsClickListener {

                override fun onSwipeOptionClicked(viewID: Int, position: Int) {
                    list.removeAt(position)
                    adapter.setTaskList(list)
                }
            })
        bi.recyclerview.addOnItemTouchListener(touchListener!!)


        // disable touch from recyclerview
        bi.recyclerview.setOnTouchListener(object : View.OnTouchListener {
            @SuppressLint("ClickableViewAccessibility")
            override fun onTouch(v: View, m: MotionEvent): Boolean {
                bi.scrollPrimaryInfo.requestDisallowInterceptTouchEvent(true)
                return false
            }
        })
    }

    private fun addDependentField() {
        /* val til = TextInputLayout(this)
         val et = EditText(this)
         til.addView(et)
         til.hint = "Dynamic View"
         binding.layoutAddDependents.addView(til) */
        // TextInputLayout textInputLayout = (TextInputLayout) LayoutInflator.from(this).inflate(R.layout.text_input_layout, null);
        //val linearLayout = findViewById<View>(R.id.linearlayout) as LinearLayout
        //linear.layoutParams.width = LinearLayout.LayoutParams.MATCH_PARENT;
        //linear.layoutParams.height = LinearLayout.LayoutParams.WRAP_CONTENT
        /*linear = LayoutInflater.from(this).inflate(R.layout.dynamic_inputfield_two, null) as LinearLayout
        binding.layoutAddDependents.addView(linear)*/
        val count = Integer.parseInt(bi.edDependents.text.toString())
        if (count > 0) {
            bindingDependents.dynamicDependents.visibility = View.VISIBLE

            if (count == 1) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.GONE
                bindingDependents.row3.visibility = View.GONE
                bindingDependents.row4.visibility = View.GONE
                bindingDependents.row5.visibility = View.GONE

                bindingDependents.layoutDependent2.visibility = View.GONE
                bindingDependents.layoutDependent3.visibility = View.GONE
                bindingDependents.layoutDependent4.visibility = View.GONE
                bindingDependents.layoutDependent5.visibility = View.GONE
                bindingDependents.layoutDependent6.visibility = View.GONE
                bindingDependents.layoutDependent7.visibility = View.GONE
                bindingDependents.layoutDependent8.visibility = View.GONE
                bindingDependents.layoutDependent9.visibility = View.GONE
                bindingDependents.layoutDependent10.visibility = View.GONE


            } else if (count == 2) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.GONE
                bindingDependents.row3.visibility = View.GONE
                bindingDependents.row4.visibility = View.GONE
                bindingDependents.row5.visibility = View.GONE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.GONE
                bindingDependents.layoutDependent4.visibility = View.GONE
                bindingDependents.layoutDependent5.visibility = View.GONE
                bindingDependents.layoutDependent6.visibility = View.GONE
                bindingDependents.layoutDependent7.visibility = View.GONE
                bindingDependents.layoutDependent8.visibility = View.GONE
                bindingDependents.layoutDependent9.visibility = View.GONE
                bindingDependents.layoutDependent10.visibility = View.GONE

            } else if (count == 3) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.VISIBLE
                bindingDependents.row3.visibility = View.GONE
                bindingDependents.row4.visibility = View.GONE
                bindingDependents.row5.visibility = View.GONE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.VISIBLE
                bindingDependents.layoutDependent4.visibility = View.GONE
                bindingDependents.layoutDependent5.visibility = View.GONE
                bindingDependents.layoutDependent6.visibility = View.GONE
                bindingDependents.layoutDependent7.visibility = View.GONE
                bindingDependents.layoutDependent8.visibility = View.GONE
                bindingDependents.layoutDependent9.visibility = View.GONE
                bindingDependents.layoutDependent10.visibility = View.GONE

            } else if (count == 4) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.VISIBLE
                bindingDependents.row3.visibility = View.GONE
                bindingDependents.row4.visibility = View.GONE
                bindingDependents.row5.visibility = View.GONE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.VISIBLE
                bindingDependents.layoutDependent4.visibility = View.VISIBLE

                bindingDependents.layoutDependent5.visibility = View.GONE
                bindingDependents.layoutDependent6.visibility = View.GONE
                bindingDependents.layoutDependent7.visibility = View.GONE
                bindingDependents.layoutDependent8.visibility = View.GONE
                bindingDependents.layoutDependent9.visibility = View.GONE
                bindingDependents.layoutDependent10.visibility = View.GONE

            } else if (count == 5) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.VISIBLE
                bindingDependents.row3.visibility = View.VISIBLE
                bindingDependents.row4.visibility = View.GONE
                bindingDependents.row5.visibility = View.GONE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.VISIBLE
                bindingDependents.layoutDependent4.visibility = View.VISIBLE
                bindingDependents.layoutDependent5.visibility = View.VISIBLE

                bindingDependents.layoutDependent6.visibility = View.GONE
                bindingDependents.layoutDependent7.visibility = View.GONE
                bindingDependents.layoutDependent8.visibility = View.GONE
                bindingDependents.layoutDependent9.visibility = View.GONE
                bindingDependents.layoutDependent10.visibility = View.GONE

            } else if (count == 6) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.VISIBLE
                bindingDependents.row3.visibility = View.VISIBLE
                bindingDependents.row4.visibility = View.GONE
                bindingDependents.row5.visibility = View.GONE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.VISIBLE
                bindingDependents.layoutDependent4.visibility = View.VISIBLE
                bindingDependents.layoutDependent5.visibility = View.VISIBLE
                bindingDependents.layoutDependent6.visibility = View.VISIBLE

                bindingDependents.layoutDependent7.visibility = View.GONE
                bindingDependents.layoutDependent8.visibility = View.GONE
                bindingDependents.layoutDependent9.visibility = View.GONE
                bindingDependents.layoutDependent10.visibility = View.GONE

            } else if (count == 7) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.VISIBLE
                bindingDependents.row3.visibility = View.VISIBLE
                bindingDependents.row4.visibility = View.VISIBLE
                bindingDependents.row5.visibility = View.GONE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.VISIBLE
                bindingDependents.layoutDependent4.visibility = View.VISIBLE
                bindingDependents.layoutDependent5.visibility = View.VISIBLE
                bindingDependents.layoutDependent6.visibility = View.VISIBLE
                bindingDependents.layoutDependent7.visibility = View.VISIBLE

                bindingDependents.layoutDependent8.visibility = View.GONE
                bindingDependents.layoutDependent9.visibility = View.GONE
                bindingDependents.layoutDependent10.visibility = View.GONE

            } else if (count == 8) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.VISIBLE
                bindingDependents.row3.visibility = View.VISIBLE
                bindingDependents.row4.visibility = View.VISIBLE
                bindingDependents.row5.visibility = View.GONE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.VISIBLE
                bindingDependents.layoutDependent4.visibility = View.VISIBLE
                bindingDependents.layoutDependent5.visibility = View.VISIBLE
                bindingDependents.layoutDependent6.visibility = View.VISIBLE
                bindingDependents.layoutDependent7.visibility = View.VISIBLE
                bindingDependents.layoutDependent8.visibility = View.VISIBLE

                bindingDependents.layoutDependent9.visibility = View.GONE
                bindingDependents.layoutDependent10.visibility = View.GONE

            } else if (count == 9) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.VISIBLE
                bindingDependents.row3.visibility = View.VISIBLE
                bindingDependents.row4.visibility = View.VISIBLE
                bindingDependents.row5.visibility = View.VISIBLE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.VISIBLE
                bindingDependents.layoutDependent4.visibility = View.VISIBLE
                bindingDependents.layoutDependent5.visibility = View.VISIBLE
                bindingDependents.layoutDependent6.visibility = View.VISIBLE
                bindingDependents.layoutDependent7.visibility = View.VISIBLE
                bindingDependents.layoutDependent8.visibility = View.VISIBLE
                bindingDependents.layoutDependent9.visibility = View.VISIBLE

                bindingDependents.layoutDependent10.visibility = View.GONE

            } else if (count == 10) {
                bindingDependents.row1.visibility = View.VISIBLE
                bindingDependents.row2.visibility = View.VISIBLE
                bindingDependents.row3.visibility = View.VISIBLE
                bindingDependents.row4.visibility = View.VISIBLE
                bindingDependents.row5.visibility = View.VISIBLE

                bindingDependents.layoutDependent1.visibility = View.VISIBLE
                bindingDependents.layoutDependent2.visibility = View.VISIBLE
                bindingDependents.layoutDependent3.visibility = View.VISIBLE
                bindingDependents.layoutDependent4.visibility = View.VISIBLE
                bindingDependents.layoutDependent5.visibility = View.VISIBLE
                bindingDependents.layoutDependent6.visibility = View.VISIBLE
                bindingDependents.layoutDependent7.visibility = View.VISIBLE
                bindingDependents.layoutDependent8.visibility = View.VISIBLE
                bindingDependents.layoutDependent9.visibility = View.VISIBLE
                bindingDependents.layoutDependent10.visibility = View.VISIBLE

            }
        } else { // dependent 0
            bindingDependents.dynamicDependents.visibility = View.GONE
        }
    }

    private fun openCalendar(){
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val newMonth = month + 1
        val dpd = DatePickerDialog(
            requireActivity(), { view, year, monthOfYear, dayOfMonth ->
                bi.edDateOfBirth.setText("" + dayOfMonth + "/" + newMonth + "/" + year)
            },
            year,
            month,
            day
        )
        dpd.show()

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

        // dependents
        bi.edDependents.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                setTextInputLayoutHintColor(bi.layoutDependants, R.color.grey_color_two )
            } else {
                if (bi.edDependents.text?.length == 0) {
                    setTextInputLayoutHintColor(bi.layoutDependants,R.color.grey_color_three)
                } else {
                    setTextInputLayoutHintColor(bi.layoutDependants,R.color.grey_color_two)
                    addDependentField()
                }
            }
        }

        // date of birth
        bi.edDateOfBirth.setOnTouchListener(object : View.OnTouchListener {
            override fun onTouch(v: View, m: MotionEvent): Boolean {
                when (m.getAction()) {
                    MotionEvent.ACTION_DOWN -> openCalendar()
                }
                return false

            }
        })

    }

    private fun setTextInputLayoutHintColor(textInputLayout: TextInputLayout, @ColorRes colorIdRes: Int) {
        textInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), colorIdRes))
    }

    private fun setMaritalStatus(isUnmarried: Boolean, isMarried: Boolean, isDivorced: Boolean) {

        if (isUnmarried) {
            msBinding.unmarriedAddendum.visibility = View.VISIBLE
            msBinding.rbUnmarried.setTypeface(null, Typeface.BOLD)
            msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbDivorced.setTypeface(null, Typeface.NORMAL)
        }
        if (isMarried) {
            msBinding.unmarriedAddendum.visibility = View.GONE
            msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbMarried.setTypeface(null, Typeface.BOLD)
            msBinding.rbDivorced.setTypeface(null, Typeface.NORMAL)
        }
        if (isDivorced) {
            msBinding.unmarriedAddendum.visibility = View.GONE
            msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbDivorced.setTypeface(null, Typeface.BOLD)
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
            citizenshipBinding.layoutVisaStatusOther.visibility = View.VISIBLE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.BOLD)
        }
    }

    private fun militaryActivePersonel() {
        if (bindingMilitary.chbDutyPersonel.isChecked) {
            bindingMilitary.layoutActivePersonnel.visibility = View.VISIBLE
            bindingMilitary.chbDutyPersonel.setTypeface(null, Typeface.BOLD)

        } else {
            bindingMilitary.layoutActivePersonnel.visibility = View.GONE
            bindingMilitary.chbDutyPersonel.setTypeface(null, Typeface.NORMAL)
        }
    }

    private fun militaryNationalGuard() {
        if (bindingMilitary.chbResNationalGuard.isChecked) {
            bindingMilitary.layoutNationalGuard.visibility = View.VISIBLE
            bindingMilitary.chbResNationalGuard.setTypeface(null, Typeface.BOLD)

        } else {
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

    private fun isValidEmailAddress(email: String?): Boolean {
        val ePattern =
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$"
        val p = Pattern.compile(ePattern)
        val m = p.matcher(email)
        return m.matches()
    }

    override fun onResume() {
        super.onResume()
        touchListener?.let { bi.recyclerview.addOnItemTouchListener(it) }
    }

}