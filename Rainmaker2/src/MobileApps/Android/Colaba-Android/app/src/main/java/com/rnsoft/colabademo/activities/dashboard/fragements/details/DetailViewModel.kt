package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

@HiltViewModel
class DetailViewModel @Inject constructor(private val detailRepo: DetailRepo ) : ViewModel() {

    private val _borrowerOverviewModel : MutableLiveData<BorrowerOverviewModel> =   MutableLiveData()
    val borrowerOverviewModel: LiveData<BorrowerOverviewModel> get() = _borrowerOverviewModel

    private val _borrowerDocsModelList : MutableLiveData<ArrayList<BorrowerDocsModel>> =   MutableLiveData()
    val borrowerDocsModelList: LiveData<ArrayList<BorrowerDocsModel>> get() = _borrowerDocsModelList

    private val _borrowerApplicationTabModel : MutableLiveData<BorrowerApplicationTabModel> =   MutableLiveData()
    val borrowerApplicationTabModel: LiveData<BorrowerApplicationTabModel> get() = _borrowerApplicationTabModel



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

    suspend fun getBorrowerApplicationTabData(token:String, borrowerId:Int) {
        viewModelScope.launch {
            val responseResult = detailRepo.getBorrowerApplicationTabData(token = token, borrowerId = borrowerId)
            if (responseResult is Result.Success)
                _borrowerApplicationTabModel.value = (responseResult.data)
            else if(responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                EventBus.getDefault().post(WebServiceErrorEvent(null, true))
            else if(responseResult is Result.Error)
                EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
        }
    }





}