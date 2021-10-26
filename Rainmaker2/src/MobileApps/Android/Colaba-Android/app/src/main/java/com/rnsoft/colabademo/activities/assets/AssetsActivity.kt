package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences

import android.os.Bundle
import android.view.View
import androidx.activity.viewModels
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.AssetsActivityLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import javax.inject.Inject

@AndroidEntryPoint
class AssetsActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    lateinit var binding: AssetsActivityLayoutBinding

    var loanApplicationId:Int? = null
    var loanPurpose:String? = null
    var borrowerTabList:ArrayList<Int>? = null
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
            borrowerTabList = it.getIntegerArrayList(AppConstant.assetBorrowerList) as ArrayList<Int>
            Timber.d("borrowerTabList size "+ borrowerTabList!!.size)
            for(item in borrowerTabList!!){
                Timber.d("item size "+ item)
            }

            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    if(loanApplicationId!=null && borrowerTabList!=null && loanApplicationId!=null ) {
                        borrowerApplicationViewModel.getBorrowerWithAssets(
                            authToken, loanApplicationId!!,
                            borrowerTabList!!
                        )
                    }
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
        binding.assetDataLoader.visibility = View.INVISIBLE
        finish()

    }
}