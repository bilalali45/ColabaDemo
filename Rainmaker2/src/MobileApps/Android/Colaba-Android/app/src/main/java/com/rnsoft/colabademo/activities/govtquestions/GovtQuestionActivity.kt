package com.rnsoft.colabademo

import android.content.SharedPreferences

import android.os.Bundle
import androidx.navigation.ui.AppBarConfiguration
import com.rnsoft.colabademo.databinding.GovtQuestionsActivityLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import javax.inject.Inject

@AndroidEntryPoint
class GovtQuestionActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    lateinit var binding: GovtQuestionsActivityLayoutBinding
    //private lateinit var appBarConfiguration : AppBarConfiguration

    var loanApplicationId:Int? = null
    var loanPurpose:String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = GovtQuestionsActivityLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_left, R.anim.hold)


        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            loanPurpose = it.getString(AppConstant.loanPurpose)
        }

        Timber.e("Running on create function")


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

    override fun onStop() {
        super.onStop()
        Timber.e("onStop from Activity called....")
    }
}