package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import com.rnsoft.colabademo.databinding.RealEstateActivityLayoutBinding
import javax.inject.Inject

/**
 * Created by Anita Kiran on 9/16/2021.
 */
class RealEstateActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: RealEstateActivityLayoutBinding

    var loanApplicationId: Int? = null
    var loanPurpose: String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = RealEstateActivityLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)

        /*val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            loanPurpose = it.getString(AppConstant.loanPurpose)
        } */
    }
}