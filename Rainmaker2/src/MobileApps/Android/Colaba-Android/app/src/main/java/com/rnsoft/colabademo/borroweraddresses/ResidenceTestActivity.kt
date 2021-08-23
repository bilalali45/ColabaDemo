package com.rnsoft.colabademo.borroweraddresses

import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import com.rnsoft.colabademo.R
import java.util.*


class ResidenceTestActivity: AppCompatActivity() {




    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.active_duty_layout)
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





