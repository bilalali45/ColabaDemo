package com.rnsoft.colabademo

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
class RequestDocsViewModel @Inject constructor(private val requestDocsRepo: RequestDocsRepo) : ViewModel() {

    private val _getTemplatesResponse : MutableLiveData<GetTemplatesResponse?> =   MutableLiveData()
    val getTemplatesResponse: MutableLiveData<GetTemplatesResponse?> get() = _getTemplatesResponse

    private val _anyResponse : MutableLiveData<Any?> =   MutableLiveData()
    val anyResponse: MutableLiveData<Any?> get() = _anyResponse

    private var webServiceRunning:Boolean = false


    suspend fun getEmailTemplates(token:String) {
        viewModelScope.launch (Dispatchers.IO) {
            val responseResult = requestDocsRepo.getEmailTemplates(token = token)
            withContext(Dispatchers.Main) {
                webServiceRunning = false
                if (responseResult is Result.Success)
                    _anyResponse.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getCategoryDocumentMcu(token:String) {
        viewModelScope.launch (Dispatchers.IO) {
            val responseResult = requestDocsRepo.getCategoryDocumentMcu(token = token)
            withContext(Dispatchers.Main) {
                webServiceRunning = false
                if (responseResult is Result.Success)
                    _anyResponse.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getTemplates(token:String) {
        viewModelScope.launch (Dispatchers.IO) {
            val responseResult = requestDocsRepo.getTemplates(token = token)
            withContext(Dispatchers.Main) {
                webServiceRunning = false
                if (responseResult is Result.Success)
                    _getTemplatesResponse.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }


}