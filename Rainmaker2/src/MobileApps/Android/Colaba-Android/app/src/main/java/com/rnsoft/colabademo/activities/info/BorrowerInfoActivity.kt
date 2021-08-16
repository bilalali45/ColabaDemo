package com.rnsoft.colabademo.activities.info

import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.LinearLayout
import android.widget.RadioButton
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.AppCompatRadioButton
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.ActivityBorrowerInformationBinding
import kotlinx.android.synthetic.main.layout_primary_info_citizenship.*
import kotlinx.android.synthetic.main.layout_primary_info_marital_status.*
import kotlinx.android.synthetic.main.layout_primary_info_marital_status.view.*

/**
 * Created by Anita Kiran on 8/11/2021.
 */

class BorrowerInfoActivity : AppCompatActivity(){
    lateinit var binding : ActivityBorrowerInformationBinding
    lateinit var maritalStatusLayout : View
    lateinit var rbUnmarried: RadioButton
    lateinit var unmarriedAddendum : LinearLayout

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBorrowerInformationBinding.inflate(layoutInflater)
        setContentView(binding.root)
        Log.e("onCreate", "yes")

        initViews()
        setMaritalStatus()

    }


     fun setMaritalStatus(){

         if(rbUnmarried.isSelected){
             Log.e("here", "selected")
             unmarriedAddendum.visibility = View.GONE
         }

         if(rbUnmarried.isChecked){
             Log.e("here", "checked")
             unmarriedAddendum.visibility = View.GONE
         }

         if(rbUnmarried.isPressed){
             Log.e("here", "press")
             unmarriedAddendum.visibility = View.GONE
         }

         if(rbUnmarried.isActivated){
             Log.e("here", "activated")
             unmarriedAddendum.visibility = View.GONE
         }
    }

     fun initViews(){
         maritalStatusLayout = findViewById(R.id.layout_maritalStatus)
         rbUnmarried = findViewById(R.id.rb_unmarried)
         unmarriedAddendum= maritalStatusLayout.findViewById(R.id.layout_unmarried_addendum)


    }



}