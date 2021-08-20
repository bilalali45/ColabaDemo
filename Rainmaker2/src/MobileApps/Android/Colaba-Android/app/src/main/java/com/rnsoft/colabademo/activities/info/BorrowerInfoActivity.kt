package com.rnsoft.colabademo.activities.info

import android.annotation.SuppressLint
import android.app.DatePickerDialog
import android.graphics.Typeface
import android.os.Bundle
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.view.LayoutInflater
import android.view.MotionEvent
import android.view.View
import android.view.View.OnTouchListener
import android.widget.LinearLayout
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.content.res.AppCompatResources
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.PhoneTextFormatter
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.activities.info.model.Address
import com.rnsoft.colabademo.databinding.ActivityBorrowerInformationBinding
import com.rnsoft.colabademo.databinding.SubLayoutMilitaryBinding
import com.rnsoft.colabademo.databinding.SublayoutCitizenshipBinding
import com.rnsoft.colabademo.databinding.SublayoutMaritalStatusBinding
import com.rnsoft.colabademo.test.MyCustomFocusListener
import com.rnsoft.colabademo.utils.RecyclerTouchListener
import kotlinx.android.synthetic.main.activity_borrower_information.*
import java.util.*
import java.util.regex.Pattern


/**
 * Created by Anita Kiran on 8/11/2021.
 */

class BorrowerInfoActivity : AppCompatActivity(), View.OnClickListener {

    lateinit var binding: ActivityBorrowerInformationBinding
    lateinit var msBinding: SublayoutMaritalStatusBinding
    lateinit var citizenshipBinding: SublayoutCitizenshipBinding
    lateinit var bindingMilitary: SubLayoutMilitaryBinding
    lateinit var linear: LinearLayout
    var list: ArrayList<Address> = ArrayList()
    private var touchListener: RecyclerTouchListener? = null


    @SuppressLint("ClickableViewAccessibility")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBorrowerInformationBinding.inflate(layoutInflater)
        setContentView(binding.root)
        msBinding = binding.layoutMaritalStatus
        citizenshipBinding = binding.layoutCitizenship
        bindingMilitary = binding.layoutMilitaryService

        initViews()
        setEndIconClicks()

        /* val til = TextInputLayout(this)
         val et = EditText(this)
         til.addView(et)
         til.hint = "Dynamic View"
         binding.layoutAddDependents.addView(til) */

        // TextInputLayout textInputLayout = (TextInputLayout) LayoutInflator.from(this).inflate(R.layout.text_input_layout, null);

        //val linearLayout = findViewById<View>(R.id.linearlayout) as LinearLayout
        linear =
            LayoutInflater.from(this).inflate(R.layout.dynamic_inputfield_two, null) as LinearLayout
        //linear.layoutParams.width = LinearLayout.LayoutParams.MATCH_PARENT;
        //linear.layoutParams.height = LinearLayout.LayoutParams.WRAP_CONTENT
        binding.layoutAddDependents.addView(linear)

        binding.scrollPrimaryInfo.isNestedScrollingEnabled = false

        setResidence()
        //setFocusforError()

       /* binding.edDateOfBirth.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                openCalendar()

          //      binding.layoutLastName.boxStrokeColor = ContextCompat.getColor(this, R.color.colaba_red_color)

                binding.layoutDateOfBirth.set(AppCompatResources.getColorStateList(
                        this,
                        R.color.primary_info_label_color
                    )
                )
            }
        } */

/*
        binding.edDateOfBirth.addTextChangedListener(object : TextWatcher {
            override fun afterTextChanged(s: Editable?) {
            }
            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) {
                if(binding.edDateOfBirth.isCursorVisible) {
                    openCalendar()
                }
            }
            override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {
            }
        }) */

        binding.recyclerview.setOnTouchListener(object : View.OnTouchListener {
            @SuppressLint("ClickableViewAccessibility")
            override fun onTouch(v: View, m: MotionEvent): Boolean {
                binding.scrollPrimaryInfo.requestDisallowInterceptTouchEvent(true)
                return false
            }
        })
    }

    private fun setResidence() {

        list.add(Address("5919 Trussvile ", "West Road"))
        list.add(Address("5919 Trussvile,", "West Road"))
        binding.recyclerview.layoutManager = LinearLayoutManager(this, RecyclerView.VERTICAL, false)
        binding.recyclerview.hasFixedSize()
       // binding.recyclerview.isNestedScrollingEnabled = false

        val adapter = ResidenceAdapter(this@BorrowerInfoActivity)
        adapter.setTaskList(list)
        binding.recyclerview.setAdapter(adapter)

        touchListener = RecyclerTouchListener(this, binding.recyclerview)
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
        binding.recyclerview.addOnItemTouchListener(touchListener!!)

    }

    private fun initViews() {
        binding.backButton.setOnClickListener(this)
        binding.btnSaveInfo.setOnClickListener(this)

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

        binding.edFirstName.setOnFocusChangeListener(MyCustomFocusListener(binding.edFirstName, binding.layoutFirstName, this))
        binding.edMiddleName.setOnFocusChangeListener(MyCustomFocusListener(binding.edMiddleName, binding.layoutMiddleName, this))
        binding.edLastName.setOnFocusChangeListener(MyCustomFocusListener(binding.edLastName, binding.layoutLastName, this))
        binding.edSuffix.setOnFocusChangeListener(MyCustomFocusListener(binding.edSuffix, binding.layoutSuffix, this))
        binding.edEmail.setOnFocusChangeListener(MyCustomFocusListener(binding.edEmail, binding.layoutEmail, this))
        binding.edHomeNumber.setOnFocusChangeListener(MyCustomFocusListener(binding.edHomeNumber, binding.layoutHomeNum, this))
        binding.edWorkNum.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edWorkNum,
                binding.layoutWorkNum, this))
        binding.edExtNum.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edExtNum,
                binding.layoutExtNum,
                this
            )
        )
        binding.edCellNum.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edCellNum,
                binding.layoutCellNum,
                this
            )
        )
        binding.edSecurityNum.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edSecurityNum,
                binding.layoutSecurityNum,
                this
            )
        )
        binding.edDependents.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edDependents,
                binding.layoutDependants,
                this
            )
        )
        binding.edDateOfBirth.setOnFocusChangeListener(
            MyCustomFocusListener(
                binding.edDateOfBirth,
                binding.layoutDateOfBirth,
                this
            )
        )

        binding.edHomeNumber.addTextChangedListener(
            PhoneTextFormatter(
                binding.edHomeNumber,
                "(###) ###-####"))
        binding.edWorkNum.addTextChangedListener(PhoneTextFormatter(binding.edWorkNum, "(###) ###-####"))
        binding.edCellNum.addTextChangedListener(PhoneTextFormatter(binding.edCellNum, "(###) ###-####"))

        binding.edDateOfBirth.setOnClickListener(this)
    }

    private fun setEndIconClicks() {

        binding.layoutDateOfBirth.setEndIconOnClickListener(View.OnClickListener {
           openCalendar()
        })

        // click for security number
        binding.layoutSecurityNum.setEndIconOnClickListener(View.OnClickListener {
            if (binding.edSecurityNum.getTransformationMethod()
                    .equals(PasswordTransformationMethod.getInstance())
            ) { //  hide password
                binding.edSecurityNum.setTransformationMethod(HideReturnsTransformationMethod.getInstance())
                binding.layoutSecurityNum.setEndIconDrawable(R.drawable.ic_eye_hide)
            } else {
                binding.edSecurityNum.setTransformationMethod(PasswordTransformationMethod.getInstance())
                binding.layoutSecurityNum.setEndIconDrawable(R.drawable.ic_eye_icon_svg)
            }
        })
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
            R.id.backButton -> finish()
            //R.id.ed_dateOfBirth ->   openCalendar()
        }
    }




    private fun checkValidations() {
        val firstName: String = binding.edFirstName.text.toString()
        val lastName: String = binding.edLastName.text.toString()
        val email: String = binding.edEmail.text.toString()
        val homeNum: String = binding.edHomeNumber.text.toString()

        if (firstName.isEmpty() || firstName.length == 0) {
            setError(binding.layoutFirstName, getString(R.string.error_field_required))
        }
        if (lastName.isEmpty() || lastName.length == 0) {
            setError(binding.layoutLastName, getString(R.string.error_field_required))
        }
        if (email.isEmpty() || email.length == 0) {
            setError(binding.layoutEmail, getString(R.string.error_field_required))
        }
        if (homeNum.isEmpty() || homeNum.length == 0) {
            setError(binding.layoutHomeNum, getString(R.string.error_field_required))
        }

        if (firstName.isNotEmpty() && firstName.length > 0) {
            clearError(binding.layoutFirstName)
        }

        if (lastName.isNotEmpty() && lastName.length > 0) {
            clearError(binding.layoutLastName)
        }
        if (email.isNotEmpty() && email.length > 0) {
            if (!isValidEmailAddress(email)) {
                setError(binding.layoutEmail, getString(R.string.invalid_email))
            } else {
                clearError(binding.layoutEmail)
            }
        }
        if (homeNum.isNotEmpty() && homeNum.isNotBlank()) {
            if (homeNum.length < 14) {
                setError(binding.layoutHomeNum, getString(R.string.invalid_phone_num))
            } else {
                clearError(binding.layoutHomeNum)
            }
        }
    }

    private fun setError(textInputlayout: TextInputLayout, errorMsg: String) {
        textInputlayout.helperText = errorMsg
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(
                this, R.color.primary_info_stroke_error_color
            )
        )
        //binding.layoutLastName.boxStrokeColor = ContextCompat.getColor(this, R.color.colaba_red_color)
    }

    private fun clearError(textInputlayout: TextInputLayout) {
        textInputlayout.helperText = ""
        textInputlayout.setBoxStrokeColorStateList(
            AppCompatResources.getColorStateList(
                this,
                R.color.primary_info_line_color
            )
        )
    }

    private fun setFocusforError() {

        binding.edEmail.setOnFocusChangeListener { view, hasFocus ->
            if (!hasFocus) {
                if (binding.edEmail.text.toString().length > 0) {
                    if (!isValidEmailAddress(binding.edEmail.text.toString())) {
                        setError(binding.layoutEmail, getString(R.string.invalid_email))
                    } else {
                        clearError(binding.layoutEmail)
                    }
                }
            }
        }

        binding.edHomeNumber.setOnFocusChangeListener { view, hasFocus ->
            if (!hasFocus) {
                if (binding.edHomeNumber.text.toString().length > 0) {
                    if (binding.edHomeNumber.text.toString().length < 14) {
                        setError(binding.layoutHomeNum, getString(R.string.invalid_phone_num))
                    } else {
                        clearError(binding.layoutHomeNum)
                    }
                }
            }
        }

        binding.edFirstName.setOnFocusChangeListener { view, hasFocus ->
            if (!hasFocus) {
                if (binding.edFirstName.text.toString().length > 0) {
                    clearError(binding.layoutFirstName)
                }
            }
        }

        binding.edLastName.setOnFocusChangeListener { view, hasFocus ->
            if (!hasFocus) {
                if (binding.edLastName.text.toString().length > 0) {
                    clearError(binding.layoutLastName)
                }
            }
        }

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
        touchListener?.let { binding.recyclerview.addOnItemTouchListener(it) }
    }

    private fun openCalendar(){

        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val newMonth = month + 1
        val dpd = DatePickerDialog(
            this, { view, year, monthOfYear, dayOfMonth ->
                binding.edDateOfBirth.setText("" + dayOfMonth + "-" + newMonth + "-" + year)
            },
            year,
            month,
            day
        )
        dpd.show()

    }

}