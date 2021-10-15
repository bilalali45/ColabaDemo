package com.rnsoft.colabademo

import android.content.SharedPreferences

import android.os.Bundle
import androidx.activity.viewModels
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
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

    private val borrowerApplicationViewModel: BorrowerApplicationViewModel by viewModels()



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = GovtQuestionsActivityLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_in_right, R.anim.hold)

        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            loanPurpose = it.getString(AppConstant.loanPurpose)
        }

        Timber.e("Running on create function")

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                Timber.e("loading govt service...")
                val bool = borrowerApplicationViewModel.getGovernmentQuestions(authToken, 5, 1, 5)
                Timber.e("Government service loaded..."+bool)
            }
        }


    }

    override fun onStop() {

        super.onStop()
        Timber.e("onStop from Activity called....")
    }
}