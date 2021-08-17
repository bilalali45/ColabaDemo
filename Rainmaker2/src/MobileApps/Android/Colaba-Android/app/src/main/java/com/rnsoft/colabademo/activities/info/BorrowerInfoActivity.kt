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
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.ActivityBorrowerInformationBinding
import com.rnsoft.colabademo.databinding.SublayoutCitizenshipBinding
import com.rnsoft.colabademo.databinding.SublayoutMaritalStatusBinding
import java.util.*

/**
 * Created by Anita Kiran on 8/11/2021.
 */

class BorrowerInfoActivity : AppCompatActivity(),View.OnClickListener{

    lateinit var binding: ActivityBorrowerInformationBinding
    lateinit var msBinding: SublayoutMaritalStatusBinding
    lateinit var citizenshipBinding: SublayoutCitizenshipBinding


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBorrowerInformationBinding.inflate(layoutInflater)
        setContentView(binding.root)
        msBinding = binding.layoutMaritalStatus
        citizenshipBinding = binding.layoutCitizenship


        initViews()
        setEndIconClicks()

    }

    private fun initViews() {
        msBinding.rbUnmarried.setOnClickListener(this)
        msBinding.rbMarried.setOnClickListener(this)
        msBinding.rbDivorced.setOnClickListener(this)
        citizenshipBinding.rbUsCitizen.setOnClickListener(this)
        citizenshipBinding.rbNonPrOther.setOnClickListener(this)
        citizenshipBinding.rbPr.setOnClickListener(this)

    }

    private fun setEndIconClicks() {

        binding.layoutDob.setEndIconOnClickListener(View.OnClickListener {
            val c = Calendar.getInstance()
            val year = c.get(Calendar.YEAR)
            val month = c.get(Calendar.MONTH)
            val day = c.get(Calendar.DAY_OF_MONTH)
            val newMonth = month + 1
            val dpd = DatePickerDialog(
                this, { view, year, monthOfYear, dayOfMonth ->
                    binding.edDatePicker.setText("" + dayOfMonth + "-" + newMonth + "-" + year)
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

        val checked = (view as RadioButton).isChecked
        when (view.getId()) {
            R.id.rb_unmarried -> if (checked) setMaritalStatus(true, false,false)
            R.id.rb_married -> if (checked) setMaritalStatus(false, true,false)
            R.id.rb_divorced -> if (checked) setMaritalStatus(false, false,true)
            R.id.rb_us_citizen -> if (checked) setCitizenship(true, false,false)
            R.id.rb_pr -> if (checked)  setCitizenship(false, true,false)
            R.id.rb_non_pr_other -> if (checked)  setCitizenship(false, false,true)

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

}