package com.rnsoft.colabademo.activities.info

import android.app.DatePickerDialog
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
import kotlinx.android.synthetic.main.layout_primary_info_citizenship.*
import kotlinx.android.synthetic.main.layout_primary_info_marital_status.*
import kotlinx.android.synthetic.main.layout_primary_info_marital_status.view.*
import java.util.*

/**
 * Created by Anita Kiran on 8/11/2021.
 */

class BorrowerInfoActivity : AppCompatActivity(){
    lateinit var binding : ActivityBorrowerInformationBinding
    lateinit var maritalStatusLayout : View
    lateinit var rbUnmarried: RadioButton
    lateinit var unmarriedAddendum : LinearLayout
    val isPasswordVisible : Boolean = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBorrowerInformationBinding.inflate(layoutInflater)
        setContentView(binding.root)

        initViews()
        setMaritalStatus()
        setEndIconClicks()



    }



     fun setMaritalStatus(){

         if(rbUnmarried.isSelected==true){
             Log.e("here", "selected")
             unmarriedAddendum.visibility = View.GONE
         }

         if(rbUnmarried.isChecked==true){
             Log.e("here", "checked")
             unmarriedAddendum.visibility = View.GONE
         }

         if(rbUnmarried.isPressed==true){
             Log.e("here", "press")
             unmarriedAddendum.visibility = View.GONE
         }

         if(rbUnmarried.isActivated==true){
             Log.e("here", "activated")
             unmarriedAddendum.visibility = View.GONE
         }
    }

     fun initViews(){
         maritalStatusLayout = findViewById(R.id.layout_maritalStatus)
         rbUnmarried = findViewById(R.id.rb_unmarried)
         unmarriedAddendum= maritalStatusLayout.findViewById(R.id.layout_unmarried_addendum)
    }

    private fun setEndIconClicks(){

        binding.layoutDob.setEndIconOnClickListener(View.OnClickListener {
           // binding.dobDatePicker.visibility = View.VISIBLE
            val c = Calendar.getInstance()
            val year = c.get(Calendar.YEAR)
            val month = c.get(Calendar.MONTH)
            val day = c.get(Calendar.DAY_OF_MONTH)
            val newMonth = month+1
            val dpd = DatePickerDialog(this, DatePickerDialog.OnDateSetListener { view, year, monthOfYear, dayOfMonth ->
                binding.edDatePicker.setText("" + dayOfMonth + "-" + newMonth + "-" + year)
            }, year, month, day)
            dpd.show()

        })


        // click for security number
        binding.layoutSecurityNum.setEndIconOnClickListener(View.OnClickListener {
            if(binding.edSecurityNum.getTransformationMethod().equals(PasswordTransformationMethod.getInstance())){ //  hide password
                binding.edSecurityNum.setTransformationMethod(HideReturnsTransformationMethod.getInstance())
                binding.layoutSecurityNum.setEndIconDrawable(R.drawable.ic_eye_icon_svg)
            }
            else{
                binding.edSecurityNum.setTransformationMethod(PasswordTransformationMethod.getInstance())
                binding.layoutSecurityNum.setEndIconDrawable(R.drawable.ic_eye_hide)

            }
        })
    }

    /*private fun setDateOfBirth(){
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)

//        binding.dobDatePicker.setOnClickListener {
//            val dpd = DatePickerDialog(this, DatePickerDialog.OnDateSetListener { view, year, monthOfYear, dayOfMonth ->
//                binding.edDatePicker.setText("" + dayOfMonth + " " + month + ", " + year)
//            }, year, month, day)
//            dpd.show()
//        }

    } */
}