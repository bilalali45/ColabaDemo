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
    var loanApplicationId: Int = -1
    var propertyId : Int = -1

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = RealEstateActivityLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_up, R.anim.hold)

        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
        }

        loanApplicationId = 5
        propertyId = 1003

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if (loanApplicationId != -1 && propertyId != -1 ){
                    binding.loaderRealEstate.visibility = View.VISIBLE
                    delay(2000)
                    viewModel.getRealEstateDetails(authToken, loanApplicationId, propertyId)
                    //viewModel.getFirstMortgageDetails(authToken,loanApplicationId,propertyId)
                    //viewModel.getPropertyTypes(authToken)
                    //viewModel.getOccupancyType(authToken)
                    //viewModel.getPropertyStatus(authToken)
                }
            }
        }
    }
}