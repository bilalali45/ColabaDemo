package com.rnsoft.colabademo.activities.info

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.databinding.*

/**
 * Created by Anita Kiran on 8/11/2021.
 */

class BorrowerInfoActivity : AppCompatActivity() {

    lateinit var binding: ActivityMainInfoBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainInfoBinding.inflate(layoutInflater)
        setContentView(binding.root)

        supportFragmentManager.beginTransaction().replace(R.id.main_info, MainInfoFragment())
            .commit()
    }
}
