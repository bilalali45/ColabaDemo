package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import okhttp3.ResponseBody
import org.greenrobot.eventbus.EventBus
import retrofit2.Response
import javax.inject.Inject

@HiltViewModel
class DetailViewModel @Inject constructor(private val detailRepo: DetailRepo ) : ViewModel() {

    private val _borrowerOverviewModel : MutableLiveData<BorrowerOverviewModel> =   MutableLiveData()
    val borrowerOverviewModel: LiveData<BorrowerOverviewModel> get() = _borrowerOverviewModel

    private val _borrowerDocsModelList : MutableLiveData<ArrayList<BorrowerDocsModel>> =   MutableLiveData()
    val borrowerDocsModelList: LiveData<ArrayList<BorrowerDocsModel>> get() = _borrowerDocsModelList


    suspend fun getLoanInfo(token:String, loanApplicationId:Int) {
        viewModelScope.launch {
            val responseResult = detailRepo.getLoanInfo(token = token, loanApplicationId = loanApplicationId)
            if (responseResult is Result.Success)
                _borrowerOverviewModel.value = (responseResult.data)
            else if(responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                EventBus.getDefault().post(WebServiceErrorEvent(null, true))
            else if(responseResult is Result.Error)
                EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
        }
    }


    suspend fun getBorrowerDocuments(token:String, loanApplicationId:Int) {
        viewModelScope.launch {
            val responseResult = detailRepo.getBorrowerDocuments(token = token, loanApplicationId = loanApplicationId)
            if (responseResult is Result.Success)
                _borrowerDocsModelList.value = (responseResult.data)
            else if(responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                EventBus.getDefault().post(WebServiceErrorEvent(null, true))
            else if(responseResult is Result.Error)
                EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
        }
    }

     fun downloadFile(token:String,  id:String, requestId:String, docId:String, fileId:String){
        viewModelScope.launch {
            val responseResult = detailRepo.downloadFile(token = token, id = id, requestId = requestId, docId = docId, fileId = fileId )
            if (responseResult is Response<*>){

            }

        }
    }







}