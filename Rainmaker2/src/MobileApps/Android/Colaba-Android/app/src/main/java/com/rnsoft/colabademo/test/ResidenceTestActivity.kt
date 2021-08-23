package com.rnsoft.colabademo.test

import android.content.Context
import android.content.res.ColorStateList
import android.graphics.Color
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.View
import android.view.View.OnFocusChangeListener
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.R
import android.widget.EditText
import androidx.annotation.ColorRes
import androidx.core.content.ContextCompat
import com.google.android.material.textfield.TextInputEditText
import com.rnsoft.colabademo.CustomTextWatcher
import org.w3c.dom.Text
import java.lang.Exception
import java.lang.reflect.Field
import java.lang.reflect.Method


class ResidenceTestActivity: AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.layout_active_duty)
        /*
        val textInputEditText                =   findViewById<TextInputEditText>(R.id.filledTextField)
        val textInputLayout                =   findViewById<TextInputLayout>(R.id.filledTextInputLayout)

        textInputEditText.setOnFocusChangeListener( MyCustomFocusListener(textInputEditText, textInputLayout , this@Residence))

        val textInputEditText2                =   findViewById<TextInputEditText>(R.id.filledTextField2)
        val textInputLayout2                =   findViewById<TextInputLayout>(R.id.filledTextInputLayout2)
        textInputEditText2.setOnFocusChangeListener(MyCustomFocusListener(textInputEditText2, textInputLayout2 , this@Residence))

         */

        Log.e("Activity", "onCreate")
    }




}





