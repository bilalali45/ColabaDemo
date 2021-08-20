package com.rnsoft.colabademo.activities.info

import android.app.DatePickerDialog
import android.graphics.Typeface
import android.os.Bundle
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.util.Log
import android.view.View
import android.widget.LinearLayout
import android.widget.RadioButton
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.textfield.TextInputLayout
import com.google.gson.TypeAdapterFactory
import com.rnsoft.colabademo.MyCustomFocusListener
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.ActivityBorrowerInformationBinding
import com.rnsoft.colabademo.databinding.SubLayoutMilitaryBinding
import com.rnsoft.colabademo.databinding.SublayoutCitizenshipBinding
import com.rnsoft.colabademo.databinding.SublayoutMaritalStatusBinding

import java.lang.reflect.Type
import java.util.*

/**
 * Created by Anita Kiran on 8/11/2021.
 */

class BorrowerInfoActivity : AppCompatActivity(),View.OnClickListener{

    lateinit var binding: ActivityBorrowerInformationBinding
    lateinit var msBinding: SublayoutMaritalStatusBinding
    lateinit var citizenshipBinding: SublayoutCitizenshipBinding
    lateinit var bindingMilitary: SubLayoutMilitaryBinding


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBorrowerInformationBinding.inflate(layoutInflater)
        setContentView(binding.root)
        msBinding = binding.layoutMaritalStatus
        citizenshipBinding = binding.layoutCitizenship
        bindingMilitary = binding.layoutMilitaryService


        initViews()
        setEndIconClicks()

        //binding.layoutFirstName.helperText="This is required"

        //binding.layoutFirstName.setError("This is required")
      //  binding.edFirstName.setError("Please enter your name")





    }

    private fun initViews() {
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
        binding.edSuffix.setOnFocusChangeListener(MyCustomFocusListener(binding.edSuffix, binding.layoutSuffix , this))
        binding.edEmail.setOnFocusChangeListener(MyCustomFocusListener(binding.edEmail, binding.layoutEmail, this))
        binding.edHomeNumber.setOnFocusChangeListener(MyCustomFocusListener(binding.edHomeNumber, binding.layoutHomeNum, this))
        binding.edWorkNum.setOnFocusChangeListener(MyCustomFocusListener(binding.edWorkNum, binding.layoutWorkNum , this))
        binding.edExtNum.setOnFocusChangeListener(MyCustomFocusListener(binding.edExtNum, binding.layoutExtNum , this))
        binding.edCellNum.setOnFocusChangeListener(MyCustomFocusListener(binding.edCellNum, binding.layoutCellNum , this))
        binding.edSecurityNum.setOnFocusChangeListener(MyCustomFocusListener(binding.edSecurityNum, binding.layoutSecurityNum, this))
        binding.edDependents.setOnFocusChangeListener(MyCustomFocusListener(binding.edDependents, binding.layoutDependants, this))
        binding.edDateOfBirth.setOnFocusChangeListener(MyCustomFocusListener(binding.edDateOfBirth, binding.layoutDateOfBirth , this))

    }

    private fun setEndIconClicks() {

        binding.layoutDateOfBirth.setEndIconOnClickListener(View.OnClickListener {
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

        })

        // click for security number
        binding.layoutSecurityNum.setEndIconOnClickListener(View.OnClickListener {
            if (binding.edSecurityNum.getTransformationMethod()
                    .equals(PasswordTransformationMethod.getInstance())
            ) { //  hide password
                binding.edSecurityNum.setTransformationMethod(HideReturnsTransformationMethod.getInstance())
                binding.layoutSecurityNum.setEndIconDrawable(R.drawable.ic_eye_icon_svg)
            } else {
                binding.edSecurityNum.setTransformationMethod(PasswordTransformationMethod.getInstance())
                binding.layoutSecurityNum.setEndIconDrawable(R.drawable.ic_eye_hide)
            }
        })
    }

    override fun onClick(view: View?) {

        //val checked = (view as RadioButton).isChecked
        when (view?.getId()) {
            R.id.rb_unmarried -> if (msBinding.rbUnmarried.isChecked) setMaritalStatus(true, false,false)
            R.id.rb_married -> if (msBinding.rbMarried.isChecked) setMaritalStatus(false, true,false)
            R.id.rb_divorced -> if (msBinding.rbDivorced.isChecked) setMaritalStatus(false, false,true)
            R.id.rb_us_citizen -> if (citizenshipBinding.rbUsCitizen.isChecked) setCitizenship(true, false,false)
            R.id.rb_pr -> if (citizenshipBinding.rbPr.isChecked) setCitizenship(false, true,false)
            R.id.rb_non_pr_other -> if (citizenshipBinding.rbNonPrOther.isChecked) setCitizenship(false, false,true)

            R.id.chb_duty_personel-> militaryActivePersonel()
            R.id.chb_res_national_guard-> militaryNationalGuard()
            R.id.chb_veteran-> militaryVeteran()
            R.id.chb_surviving_spouse-> militarySurvivingSpouse()

        }
    }

    private fun setMaritalStatus(isUnmarried:Boolean, isMarried:Boolean, isDivorced:Boolean){

        if(isUnmarried){
            msBinding.unmarriedAddendum.visibility = View.VISIBLE
            msBinding.rbUnmarried.setTypeface(null, Typeface.BOLD)
            msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbDivorced.setTypeface(null, Typeface.NORMAL)
        }
        if(isMarried){
            msBinding.unmarriedAddendum.visibility = View.GONE
            msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbMarried.setTypeface(null, Typeface.BOLD)
            msBinding.rbDivorced.setTypeface(null, Typeface.NORMAL)
        }
        if(isDivorced){
            msBinding.unmarriedAddendum.visibility = View.GONE
            msBinding.rbUnmarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbMarried.setTypeface(null, Typeface.NORMAL)
            msBinding.rbDivorced.setTypeface(null, Typeface.BOLD)
        }
    }

    private fun setCitizenship(usCitizen:Boolean, PR:Boolean, nonPR:Boolean){
        if(usCitizen){
            citizenshipBinding.layoutVisaStatusOther.visibility = View.GONE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.BOLD)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.NORMAL)
        }
        if(PR){
            citizenshipBinding.layoutVisaStatusOther.visibility = View.GONE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.BOLD)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.NORMAL)
        }
        if(nonPR){
            citizenshipBinding.layoutVisaStatusOther.visibility = View.VISIBLE
            citizenshipBinding.rbUsCitizen.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbPr.setTypeface(null, Typeface.NORMAL)
            citizenshipBinding.rbNonPrOther.setTypeface(null, Typeface.BOLD)
        }
    }

    private fun militaryActivePersonel(){
        if(bindingMilitary.chbDutyPersonel.isChecked){
            bindingMilitary.layoutActivePersonnel.visibility = View.VISIBLE
            bindingMilitary.chbDutyPersonel.setTypeface(null,Typeface.BOLD)

        } else {
            bindingMilitary.layoutActivePersonnel.visibility = View.GONE
            bindingMilitary.chbDutyPersonel.setTypeface(null,Typeface.NORMAL)
        }
    }

    private fun militaryNationalGuard(){
        if(bindingMilitary.chbResNationalGuard.isChecked){
            bindingMilitary.layoutNationalGuard.visibility = View.VISIBLE
            bindingMilitary.chbResNationalGuard.setTypeface(null,Typeface.BOLD)

        } else {
            bindingMilitary.layoutNationalGuard.visibility = View.GONE
            bindingMilitary.chbResNationalGuard.setTypeface(null,Typeface.NORMAL)
        }
    }

    private fun militaryVeteran(){
        if(bindingMilitary.chbVeteran.isChecked){
            bindingMilitary.chbVeteran.setTypeface(null,Typeface.BOLD)

        } else {
            bindingMilitary.chbVeteran.setTypeface(null,Typeface.NORMAL)
        }
    }

    private fun militarySurvivingSpouse(){
        if(bindingMilitary.chbSurvivingSpouse.isChecked){
            bindingMilitary.chbSurvivingSpouse.setTypeface(null,Typeface.BOLD)

        } else {
            bindingMilitary.chbSurvivingSpouse.setTypeface(null,Typeface.NORMAL)
        }
    }

}