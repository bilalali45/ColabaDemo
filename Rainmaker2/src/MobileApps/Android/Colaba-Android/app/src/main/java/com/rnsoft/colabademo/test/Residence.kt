package com.rnsoft.colabademo.test

import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import com.rnsoft.colabademo.R

class Residence: AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.current_residence_layout)

        Log.e("Activity", "onCreate")
    }
}