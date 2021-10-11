package com.rnsoft.colabademo.activities.subjectproperty

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.AppConstant
import com.rnsoft.colabademo.DetailRepo
import com.rnsoft.colabademo.Result
import com.rnsoft.colabademo.WebServiceErrorEvent
import com.rnsoft.colabademo.activities.model.PropertyType
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/11/2021.
 */

@HiltViewModel
class SubjectPropertyViewModel @Inject constructor(private val repository: RepoSubjectProperty ) : ViewModel() {

    private val _propertyType : MutableLiveData<PropertyType> =   MutableLiveData()
    val propertyType: LiveData<PropertyType> get() = _propertyType

    suspend fun getPropertyTypes(token:String) {
            viewModelScope.launch (Dispatchers.Main) {
                val responseResult = repository.getPropertyType(token = token)
                withContext(Dispatchers.Main) {
                    if (responseResult !=null)
                        _propertyType.value = responseResult.data
                    else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                    else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                }
            }
        }
    }

}