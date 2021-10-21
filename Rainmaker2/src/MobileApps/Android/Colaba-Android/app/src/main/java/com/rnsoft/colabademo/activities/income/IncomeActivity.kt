package com.rnsoft.colabademo


import android.content.SharedPreferences

import android.os.Bundle
import androidx.activity.viewModels
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.IncomeActivityLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import javax.inject.Inject

@AndroidEntryPoint
class IncomeActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: IncomeActivityLayoutBinding
    private val viewModel: BorrowerApplicationViewModel by viewModels()
    private var bList:ArrayList<Int>? = null
    var loanApplicationId:Int? = null
    var loanPurpose:String? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = IncomeActivityLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_in_right, R.anim.hold)

        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            loanPurpose = it.getString(AppConstant.loanPurpose)
            bList = it.getIntegerArrayList(AppConstant.incomeBorrowerList) as ArrayList<Int>
            Timber.d("borrowerTabList size " + bList!!.size)
            for (item in bList!!) {
                Timber.d("item size " + item)
            }
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    if (loanApplicationId != null && bList != null && loanApplicationId!=null) {
                        viewModel.getBorrowerWithIncome(
                            authToken, loanApplicationId!!, bList!!
                        )
                    }
                }
            }
        }

        /*
        val host: NavHostFragment = supportFragmentManager
            .findFragmentById(R.id.nav_host_asset) as NavHostFragment? ?: return
        // Set up Action Bar
        val navController = host.navController
        appBarConfiguration = AppBarConfiguration(navController.graph)
        navController.addOnDestinationChangedListener { _, destination, _ ->
            val dest: String = try {
                resources.getResourceName(destination.id)
            } catch (e: Resources.NotFoundException) {
                destination.id.toString()
            }
            //Log.d("NavigationActivity", "Navigated to $dest")
        }
        */


    }
}