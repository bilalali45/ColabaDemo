package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.*
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject
import kotlin.coroutines.CoroutineContext

@HiltViewModel
class BorrowerApplicationViewModel @Inject constructor(private val bAppRepo: BorrowerApplicationRepo) : ViewModel() {

    private var _assetsModelDataClass : MutableLiveData<AssetsModelDataClass> =   MutableLiveData()
    val assetsModelDataClass: LiveData<AssetsModelDataClass> get() = _assetsModelDataClass

    suspend fun getBorrowerAssetsDetail(token:String, loanApplicationId:Int , borrowerId:Int): Boolean {

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

        return true

    }

    private val job = Job()
    val coroutineContext: CoroutineContext
        get() = job + Dispatchers.Main

    suspend fun getBorrowerAssetsDetail2(token:String, loanApplicationId:Int , borrowerIds:ArrayList<Int>): Boolean {
        viewModelScope.launch(Dispatchers.IO) {
            withContext(Dispatchers.Default) {
                //val content = arrayListOf<Int>()
                val content = arrayListOf<Result<AssetsModelDataClass>>()
                coroutineScope {
                    borrowerIds.forEach { id ->
                        launch { // this will allow us to run multiple tasks in parallel
                            val responseResult = bAppRepo.getBorrowerAssetsDetail(token = token, loanApplicationId = loanApplicationId , borrowerId = id)
                            if (responseResult is Result.Success) {
                                //content.find {
                                  //  it.id == id
                                //}
                            }
                        }
                    }
                }  // coroutineScope block will wait here until all child tasks are completed

                /*

                val runningTasks = multipleIds.map { id ->
                    async { // this will allow us to run multiple tasks in parallel
                        val responseResult = bAppRepo.getBorrowerAssetsDetail(token = token, loanApplicationId = loanApplicationId , borrowerId = id)
                        if (responseResult is Result.Success) {
                            content.find {
                               // it.id == id
                            }
                        }
                    }
                }

                val responses = runningTasks.awaitAll()

                responses.forEach { (id, response) ->
                    if (response.isSuccessful()) {
                       // content.find { it.id == id }.enable = true
                    }
                }


                 */

            }


        }

        return true
    }


            

    fun resetAssetModelClass(){
        _assetsModelDataClass  =   MutableLiveData()
    }



}