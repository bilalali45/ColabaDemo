package com.rnsoft.colabademo

import android.content.SharedPreferences

import android.os.Bundle
import androidx.activity.viewModels
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.AssetsActivityLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import javax.inject.Inject

@AndroidEntryPoint
class AssetsActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    lateinit var binding: AssetsActivityLayoutBinding
    //private lateinit var appBarConfiguration : AppBarConfiguration

    var loanApplicationId:Int? = null
    var loanPurpose:String? = null

    private var bList:ArrayList<Int>? = null

    private val borrowerApplicationViewModel: BorrowerApplicationViewModel by viewModels()




    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = AssetsActivityLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_in_right, R.anim.hold)

        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            loanPurpose = it.getString(AppConstant.loanPurpose)
            bList = it.getIntegerArrayList(AppConstant.assetBorrowerList) as ArrayList<Int>
        }

        Timber.d("Running on create function")


        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                if(loanApplicationId!=null && bList!=null ) {
                    borrowerApplicationViewModel.getBorrowerWithAssets(
                        authToken, 5,
                        arrayListOf(5)
                    )
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

    override fun onStop() {
        super.onStop()
        Timber.e("onStop from Activity called....")
    }
}