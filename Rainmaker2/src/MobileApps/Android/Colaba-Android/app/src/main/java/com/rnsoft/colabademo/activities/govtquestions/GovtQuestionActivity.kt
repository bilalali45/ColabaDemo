package com.rnsoft.colabademo

import android.content.SharedPreferences

import android.os.Bundle
import android.view.View
import androidx.activity.viewModels
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import com.rnsoft.colabademo.databinding.GovtQuestionsActivityLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.delay
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
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
    var borrowerTabList:ArrayList<Int>? = null
    var borrowerOwnTypeList:ArrayList<Int>? = null
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
            borrowerTabList =
                it.getIntegerArrayList(AppConstant.borrowerList) as ArrayList<Int>
            Timber.d("borrowerTabList size " + borrowerTabList!!.size)

            borrowerOwnTypeList =
                it.getIntegerArrayList(AppConstant.borrowerOwnTypeList) as ArrayList<Int>

            for (item in borrowerTabList!!) {
                Timber.d("item size " + item)
            }
        }


        val navController = findNavController(R.id.nav_host_govt_question)


        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                Timber.e("loading govt service...")

                val borrowerId =  borrowerTabList?.get(0)
                val ownTypeId = borrowerOwnTypeList?.get(0)

                if(loanApplicationId!=null && borrowerTabList!=null && loanApplicationId!=null &&  borrowerId!=null && ownTypeId!=null ) {
                    val bool = borrowerApplicationViewModel.getGovernmentQuestions(
                        authToken,
                        loanApplicationId!!,
                        ownTypeId,
                        borrowerId
                    )
                    Timber.e("Government service loaded..." + bool)
                }
            }
        }

        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                Timber.e("DemoGraphic...")
                val borrowerId =  borrowerTabList?.get(0)

                if(loanApplicationId!=null && borrowerId!=null) {
                     borrowerApplicationViewModel.getDemoGraphicInfo(authToken,
                        5,//loanApplicationId!!,
                         5 )//borrowerId )
                    delay(2000)
                    //borrowerApplicationViewModel.getRaceList(authToken)
                    //borrowerApplicationViewModel.getGenderList(authToken)
                    //borrowerApplicationViewModel.getEthnicityList(authToken)
                    //delay(2000)
                    //navController.navigate(R.id.navigation_demographic)
                }
            }
        }
    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onErrorEvent(event: WebServiceErrorEvent) {
        binding.govtDataLoader.visibility = View.INVISIBLE
        finish()

    }
}