package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import androidx.navigation.findNavController
import com.rnsoft.colabademo.databinding.BorrowerLoanLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerLoanActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: BorrowerLoanLayoutBinding

    var loanApplicationId:Int? = null
    var loanPurpose:String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = BorrowerLoanLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_left, R.anim.hold)

        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            loanPurpose = it.getString(AppConstant.loanPurpose)
        }

        val navController = findNavController(R.id.nav_host_borrower_loan)

        if(loanPurpose.equals(AppConstant.purchase, ignoreCase = true))
             navController.navigate(R.id.navigation_loan_refinance)
         else if(loanPurpose.equals(AppConstant.refinance, ignoreCase = true)) {
             navController.navigate(R.id.navigation_loan_refinance)
         }

      /*val appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.navigation_loan_purchase,
                R.id.navigation_loan_refinance
            )
        ) */
    }
}