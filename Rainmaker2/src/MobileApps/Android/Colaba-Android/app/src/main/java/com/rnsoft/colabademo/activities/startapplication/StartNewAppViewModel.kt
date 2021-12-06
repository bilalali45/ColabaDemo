package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

@HiltViewModel
class StartNewAppViewModel @Inject constructor(private val startNewAppRepo: StartNewAppRepo ) :
    ViewModel() {

    private val _searchResultResponse : MutableLiveData<SearchResultResponse> =   MutableLiveData()
    val searchResultResponse: LiveData<SearchResultResponse> get() = _searchResultResponse

    private val _lookUpBorrowerContactResponse : MutableLiveData<LookUpBorrowerContactResponse> =   MutableLiveData()
    val lookUpBorrowerContactResponse: LiveData<LookUpBorrowerContactResponse> get() = _lookUpBorrowerContactResponse

    suspend fun searchByBorrowerContact(token:String, searchKeyword:String){
        viewModelScope.launch(Dispatchers.IO) {
            val result = startNewAppRepo.searchByBorrowerContact(token = token, searchKeyword = searchKeyword)
            withContext(Dispatchers.Main) {
                if (result is Result.Success)
                    _searchResultResponse.value = result.data
                else if (result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (result is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(result))
            }
        }
    }

    suspend fun lookUpBorrowerContact(token:String, borrowerEmail:String, borrowerPhone:String){
        viewModelScope.launch(Dispatchers.IO) {
            val result = startNewAppRepo.lookUpBorrowerContact(token = token, borrowerEmail = borrowerEmail, borrowerPhone = borrowerPhone)
            withContext(Dispatchers.Main) {
                if (result is Result.Success)
                    _lookUpBorrowerContactResponse.value = result.data
                else if (result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (result is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(result))
            }
        }
    }

}