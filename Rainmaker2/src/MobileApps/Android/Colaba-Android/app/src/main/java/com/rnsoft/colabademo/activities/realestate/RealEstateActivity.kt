package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.View
import androidx.activity.viewModels
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.RealEstateActivityLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.delay
import javax.inject.Inject

/**
 * Created by Anita Kiran on 9/16/2021.
 */
@AndroidEntryPoint
class RealEstateActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    lateinit var binding: RealEstateActivityLayoutBinding
    private val viewModel : RealEstateViewModel by viewModels()

    var loanApplicationId: Int? = null
    var loanPurpose: String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = RealEstateActivityLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_up, R.anim.hold)

        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            loanPurpose = it.getString(AppConstant.loanPurpose)
        }

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                binding.loaderRealEstate.visibility = View.VISIBLE
                delay(2000)
                viewModel.getRealEstateDetails(authToken, 5, 1003)
                viewModel.getPropertyTypes(authToken)
                viewModel.getOccupancyType(authToken)
                viewModel.getPropertyStatus(authToken)
                //if (loanApplicationId != null)
                //    viewModel.getRealEstateDetails(authToken, 5, 1003)
            }
        }
    }
}