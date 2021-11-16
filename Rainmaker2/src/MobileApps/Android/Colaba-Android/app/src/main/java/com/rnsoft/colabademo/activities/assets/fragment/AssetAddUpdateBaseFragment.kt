package com.rnsoft.colabademo

import android.content.SharedPreferences
import androidx.activity.viewModels
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.addresses.info.fragment.DeleteCurrentResidenceDialogFragment
import com.rnsoft.colabademo.activities.addresses.info.fragment.SwipeToDeleteEvent
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject

@AndroidEntryPoint
open class AssetAddUpdateBaseFragment: BaseFragment() {
    protected var loanApplicationId:Int? = null
    protected var loanPurpose:String? = null
    protected var borrowerId:Int? = null
    protected var borrowerAssetId:Int = -1
    protected var assetCategoryId:Int = 4
    protected var assetTypeID:Int? = null
    protected var id:Int? = null

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    protected val viewModel: AssetViewModel by activityViewModels()

    private val borrowerApplicationViewModel: BorrowerApplicationViewModel by activityViewModels()

    protected fun showDeleteDialog(text:String ="Are you sure you want to remove this asset type?"){
        DeleteAssetBoxFragment.newInstance(text).show(childFragmentManager, DeleteCurrentResidenceDialogFragment::class.java.canonicalName)
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
    fun onAssetDeleteEventReceived(evt: AssetDeleteEvent) {
        if(evt.bool){
            if (loanApplicationId != null && borrowerId != null && borrowerAssetId >0) {
                viewModel.genericAddUpdateAssetResponse.observe(viewLifecycleOwner, { genericAddUpdateAssetResponse ->
                    val codeString = genericAddUpdateAssetResponse.code.toString()
                    if(codeString == "400"){
                        //updateMainAsset()
                        findNavController().popBackStack()
                    }
                })

                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        viewModel.deleteAsset(authToken, borrowerAssetId, borrowerId!!, loanApplicationId!!)
                    }
                }
            }
        }
    }

    protected fun observeAddUpdateResponse(){
        viewModel.genericAddUpdateAssetResponse.observe(viewLifecycleOwner, { genericAddUpdateAssetResponse ->
            if(genericAddUpdateAssetResponse.status == "OK"){
                val codeString = genericAddUpdateAssetResponse.code.toString()
                if(codeString == "200"){
                    lifecycleScope.launchWhenStarted {
                        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                            //updateMainAsset()
                            findNavController().popBackStack()
                        }
                    }
                }
            }
        })
    }


    private fun updateMainAsset(){
        borrowerApplicationViewModel.assetsModelDataClass.observe(viewLifecycleOwner, Observer { observableSampleContent ->
            findNavController().popBackStack()
        })
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                borrowerApplicationViewModel.getBorrowerWithAssets(
                    authToken, loanApplicationId!!,
                    arrayListOf(borrowerId!!)
                )

            }
        }
    }

}