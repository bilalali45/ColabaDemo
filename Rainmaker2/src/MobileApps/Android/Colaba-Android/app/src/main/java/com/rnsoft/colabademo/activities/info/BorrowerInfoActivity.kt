package com.rnsoft.colabademo.activities.info

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.rnsoft.colabademo.databinding.ActivityBorrowerInformationBinding

/**
 * Created by Anita Kiran on 8/11/2021.
 */

class BorrowerInfoActivity : AppCompatActivity(){
    lateinit var binding : ActivityBorrowerInformationBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBorrowerInformationBinding.inflate(layoutInflater)
        setContentView(binding.root)
    }


}