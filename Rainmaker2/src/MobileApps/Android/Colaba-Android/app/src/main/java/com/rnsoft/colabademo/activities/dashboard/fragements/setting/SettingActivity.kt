package com.rnsoft.colabademo

import android.content.Intent
import android.os.Bundle
import com.rnsoft.colabademo.databinding.SettingFragmentBinding

class SettingActivity : BaseActivity()  {

    private lateinit var binding : SettingFragmentBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = SettingFragmentBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_in_right, R.anim.hold)

        binding.btnDocSubmit.setOnClickListener{
            startActivity(Intent(this, DocumentSubmitActivity::class.java))
        }

        binding.backButton.setOnClickListener {
            finish()
        }
    }

}