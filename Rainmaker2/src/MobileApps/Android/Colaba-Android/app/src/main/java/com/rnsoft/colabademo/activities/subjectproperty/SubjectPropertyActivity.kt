package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import androidx.activity.viewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import com.rnsoft.colabademo.databinding.BorrowerSubjectPropertyLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class SubjectPropertyActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences : SharedPreferences
    private lateinit var binding : BorrowerSubjectPropertyLayoutBinding
    private val viewModel : SubjectPropertyViewModel by viewModels()
    var purpose : String? = null
    var loanApplicationId: Int? = null
    var loanPurpose:String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = BorrowerSubjectPropertyLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_in_right, R.anim.hold)

        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            purpose = it.getString(AppConstant.borrowerPurpose)
        }


        val navController = findNavController(R.id.nav_host_borrower_subject_property)
        /*if(purpose.equals(AppConstant.purchase, ignoreCase = true))
            navController.navigate(R.id.nav_sub_property_purchase)
        else if(purpose.equals(AppConstant.refinance, ignoreCase = true)) {
            navController.navigate(R.id.nav_sub_property_refinance)
        } */

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if(loanApplicationId != null) {
                    //coroutineScope {
                    Log.e("authToken", authToken)
                    Log.e("laon id", ""+loanApplicationId)
                    viewModel.getPropertyTypes(authToken)
                    viewModel.getOccupancyType(authToken)
                    viewModel.getCoBorrowerOccupancyStatus(authToken, 1010)
                    navController.navigate(R.id.nav_sub_property_refinance)

                    if(purpose.equals(AppConstant.purchase, ignoreCase = true)) {
                        viewModel.getSubjectPropertyDetails(authToken, 1010)
                        navController.navigate(R.id.nav_sub_property_purchase)
                    }
                    else if(purpose.equals(AppConstant.refinance, ignoreCase = true)) {
                        viewModel.getRefinanceDetails(authToken, 1010)
                        navController.navigate(R.id.nav_sub_property_refinance)
                    }
                }
            }
        }

    }
 }