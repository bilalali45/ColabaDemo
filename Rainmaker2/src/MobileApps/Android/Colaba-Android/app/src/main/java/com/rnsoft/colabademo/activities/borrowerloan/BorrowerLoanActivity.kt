package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import com.rnsoft.colabademo.databinding.BorrowerLoanLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerLoanActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: BorrowerLoanLayoutBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = BorrowerLoanLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val navController = findNavController(R.id.nav_host_borrower_loan)
        val appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.navigation_loan_purchase,
                R.id.navigation_loan_refinance
            )
        )
    }
}