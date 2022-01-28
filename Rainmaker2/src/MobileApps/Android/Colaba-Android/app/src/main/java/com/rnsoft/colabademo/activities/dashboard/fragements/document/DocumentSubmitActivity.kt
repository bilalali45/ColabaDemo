package com.rnsoft.colabademo

import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.rnsoft.colabademo.databinding.DocSubmitNotificationBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.radioUnSelectColor
import com.rnsoft.colabademo.utils.CustomMaterialFields.Companion.setRadioColor
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class DocumentSubmitActivity : AppCompatActivity() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: DocSubmitNotificationBinding
    var selection : String = ""
    var off : String = "Off"
    var immediate: String = "immediate"
    var twenty_min : String = "twenty_mins"
    var thirty_mins: String = "thirty_mins"
    var ten_mins :  String = "ten_mins"


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = DocSubmitNotificationBinding.inflate(layoutInflater)
        setContentView(binding.root)

        clickEvents()

        sharedPreferences.getString(AppConstant.NOTIFICATION_SETTING, "")?.let { it ->

            if (it.isNotEmpty() && it.length > 0) {

                if (it.equals(off))
                    binding.notOff.isChecked = true

                if (it.equals(immediate))
                    binding.notImmediate.isChecked = true

                if (it.equals(ten_mins))
                    binding.notTenMins.isChecked = true

                if (it.equals(twenty_min))
                    binding.notTwentyMins.isChecked = true

                if (it.equals(thirty_mins))
                    binding.notThirtyMins.isChecked = true
            }
        }
    }

    private fun clickEvents(){
        binding.backButton.setOnClickListener {
            finish()
        }

        binding.notOff.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                setRadioColor(binding.notOff, this)
                sharedPreferences.edit().putString(AppConstant.NOTIFICATION_SETTING,off ).apply()
            } else
                radioUnSelectColor(binding.notOff, this)
        }

        binding.notImmediate.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                setRadioColor(binding.notImmediate, this)
                sharedPreferences.edit().putString(AppConstant.NOTIFICATION_SETTING,immediate ).apply()
            } else
                radioUnSelectColor(binding.notImmediate, this)
        }

        binding.notTenMins.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                setRadioColor(binding.notTenMins, this)
                sharedPreferences.edit().putString(AppConstant.NOTIFICATION_SETTING,ten_mins ).apply()
            } else
                radioUnSelectColor(binding.notTenMins, this)
        }

        binding.notTwentyMins.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                setRadioColor(binding.notTwentyMins, this)
                sharedPreferences.edit().putString(AppConstant.NOTIFICATION_SETTING,twenty_min).apply()
            } else
                radioUnSelectColor(binding.notTwentyMins, this)
        }

        binding.notThirtyMins.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked) {
                setRadioColor(binding.notThirtyMins, this)
                sharedPreferences.edit().putString(AppConstant.NOTIFICATION_SETTING,thirty_mins).apply()
            } else
                radioUnSelectColor(binding.notThirtyMins, this)
        }
    }

}
