package com.rnsoft.colabademo

import android.content.SharedPreferences
import androidx.activity.OnBackPressedCallback
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.addresses.info.fragment.DeleteCurrentResidenceDialogFragment
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import javax.inject.Inject

@AndroidEntryPoint
open class AssetBaseFragment: BaseFragment() {
    protected var loanApplicationId:Int? = null
    protected var loanPurpose:String? = null
    protected var borrowerId:Int? = null
    protected var assetUniqueId:Int = -1
    protected var assetCategoryId:Int = 4
    protected var assetTypeID:Int? = null
    protected var assetCategoryName:String? = null
    protected var listenerAttached:Int? = null


    @Inject
    lateinit var sharedPreferences: SharedPreferences

    protected val viewModel: AssetViewModel by activityViewModels()

    private val borrowerApplicationViewModel: BorrowerApplicationViewModel by activityViewModels()

    protected fun showDeleteDialog(params: AssetReturnParams, text:String ="Are you sure you want to remove this asset type?"){
        DeleteAssetBoxFragment.newInstance(params , text).show(childFragmentManager, DeleteCurrentResidenceDialogFragment::class.java.canonicalName)
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
            if (loanApplicationId != null && borrowerId != null && assetUniqueId >0) {
                viewModel.genericAddUpdateAssetResponse.observe(viewLifecycleOwner, { genericAddUpdateAssetResponse ->
                    val codeString = genericAddUpdateAssetResponse.code.toString()
                    if(codeString == "400"){
                        evt.assetReturnParams.assetAction = AppConstant.assetDeleted
                        Timber.e("catching unique new id = "+evt.assetReturnParams.assetUniqueId)
                        EventBus.getDefault()
                            .post(AssetUpdateEvent(evt.assetReturnParams))
                        findNavController().popBackStack()
                        //updateMainAsset()
                        //findNavController().popBackStack()
                    }
                })

                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        viewModel.deleteAsset(authToken, assetUniqueId, borrowerId!!, loanApplicationId!!)
                    }
                }
            }
        }
    }

    protected fun observeAddUpdateResponse(assetReturnParams: AssetReturnParams){
        viewModel.genericAddUpdateAssetResponse.observe(viewLifecycleOwner, { genericAddUpdateAssetResponse ->
            if(genericAddUpdateAssetResponse.status == "OK"){
                val codeString = genericAddUpdateAssetResponse.code.toString()
                if(codeString == "200"){
                    lifecycleScope.launchWhenStarted {
                        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                            genericAddUpdateAssetResponse.assetUniqueData?.let { nonNullAssetUniqueData->
                                assetReturnParams.assetUniqueId = nonNullAssetUniqueData
                                Timber.e("catching unique new id = "+nonNullAssetUniqueData)
                                EventBus.getDefault()
                                    .post(AssetUpdateEvent(assetReturnParams))
                                findNavController().popBackStack()
                            }
                            //updateMainAsset()
                            //findNavController().popBackStack()
                        }
                    }
                }
            }
        })
    }


    protected val backToAssetScreen: OnBackPressedCallback = object : OnBackPressedCallback(true) {
        override fun handleOnBackPressed() {
            findNavController().popBackStack()
        }
    }



    private fun updateMainAsset(){
        borrowerApplicationViewModel.assetsModelDataClass.observe(viewLifecycleOwner, { observableSampleContent ->
            findNavController().popBackStack()
        })
        val assetsActivity = (activity as? AssetsActivity)
        var mainBorrowerList:ArrayList<Int>? = null
        assetsActivity?.let { assetsActivity ->
            mainBorrowerList =  assetsActivity.borrowerTabList
        }
        mainBorrowerList?.let { notNullMainBorrowerList->
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    borrowerApplicationViewModel.getBorrowerWithAssets(
                        authToken, loanApplicationId!!, notNullMainBorrowerList , borrowerId!!
                    )
                }
            }
        }
    }

}