package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.activities.assets.model.MyAssetBorrowerDataClass
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.*
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject
import kotlin.coroutines.CoroutineContext

@HiltViewModel
class BorrowerApplicationViewModel @Inject constructor(private val bAppRepo: BorrowerApplicationRepo) : ViewModel() {

    private var _assetsModelDataClass : MutableLiveData<ArrayList<MyAssetBorrowerDataClass>> =   MutableLiveData()
    val assetsModelDataClass: LiveData<ArrayList<MyAssetBorrowerDataClass>> get() = _assetsModelDataClass

    private var _governmentQuestionsModelClass : MutableLiveData<GovernmentQuestionsModelClass> =   MutableLiveData()
    val governmentQuestionsModelClass: LiveData<GovernmentQuestionsModelClass> get() = _governmentQuestionsModelClass

    suspend fun getBorrowerAssetsDetail(token:String, loanApplicationId:Int, borrowerId: ArrayList<Int>?): Boolean {

        /*
        viewModelScope.launch (Dispatchers.IO) {
            val responseResult = bAppRepo.getBorrowerAssetsDetail(token = token, loanApplicationId = loanApplicationId , borrowerId = borrowerId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _assetsModelDataClass.value = ((responseResult as Result.Success<AssetsModelDataClass>).data)
                else if (responseResult is Result.Error && (responseResult as Result.Error).exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult as Result.Error))
            }
        }

         */

        return true

    }

    private val job = Job()
    val coroutineContext: CoroutineContext
        get() = job + Dispatchers.Main

    suspend fun getBorrowerAssetsDetail2(token:String, loanApplicationId:Int , borrowerIds:ArrayList<Int>): Boolean {
        /*
        viewModelScope.launch(Dispatchers.IO) {
            withContext(Dispatchers.Default) {
                //val content = arrayListOf<Int>()
                val content = arrayListOf<Result<AssetsModelDataClass>>()
                coroutineScope {
                    borrowerIds.forEach { id ->
                        launch { // this will allow us to run multiple tasks in parallel
                            val responseResult = bAppRepo.getBorrowerAssetsDetail(token = token, loanApplicationId = loanApplicationId , borrowerId = id)
                            if (responseResult is Result.Success) {

                            }
                        }
                    }
                }  // coroutineScope block will wait here until all child tasks are completed



                val runningTasks = borrowerIds.map { id ->
                    async { // this will allow us to run multiple tasks in parallel
                        val responseResult = bAppRepo.getBorrowerAssetsDetail(token = token, loanApplicationId = loanApplicationId , borrowerId = id)
                        if (responseResult is Result.Success) {

                        }
                    }
                }

                val responses = runningTasks.awaitAll()



                responses.forEach { (id, response) ->
                    if (response.isSuccessful()) {
                       // content.find { it.id == id }.enable = true
                    }
                }


            }


        }

         */

        return true
    }



    suspend fun getBorrowerWithAssets(token:String, loanApplicationId:Int , borrowerIds:ArrayList<Int>) {
        val borrowerAssetList: ArrayList<MyAssetBorrowerDataClass> = ArrayList()
        viewModelScope.launch(Dispatchers.IO) {
            coroutineScope {
                borrowerIds.forEach { id ->
                    launch { // this will allow us to run multiple tasks in parallel
                        val responseResult = bAppRepo.getBorrowerAssetsDetail(
                            token = token,
                            loanApplicationId = loanApplicationId,
                            borrowerId = id
                        )
                        if (responseResult is Result.Success)
                            borrowerAssetList.add(responseResult.data)
                    }
                }
            }  // coroutineScope block will wait here until all child tasks are completed
            withContext(Dispatchers.Main) {
                _assetsModelDataClass.value = borrowerAssetList
            }
        }

    }

    fun resetAssetModelClass(){
        _assetsModelDataClass  =   MutableLiveData()
    }


    suspend fun getGovernmentQuestions(token:String, loanApplicationId:Int, ownTypeId:Int, borrowerId:Int ): Boolean {
        var bool = false
        viewModelScope.launch (Dispatchers.IO) {
            //delay(2000)
            val responseResult = bAppRepo.getGovernmentQuestions(token = token, loanApplicationId = loanApplicationId , ownTypeId = ownTypeId, borrowerId = borrowerId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                     bool = true
                    _governmentQuestionsModelClass.value = responseResult.data
                }
                else if (responseResult is Result.Error && (responseResult as Result.Error).exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult as Result.Error))
            }
        }
        return bool
    }


}