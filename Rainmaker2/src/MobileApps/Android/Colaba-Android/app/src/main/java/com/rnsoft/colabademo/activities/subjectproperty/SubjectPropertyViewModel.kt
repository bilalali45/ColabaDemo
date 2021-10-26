package com.rnsoft.colabademo

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.activities.model.*
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.greenrobot.eventbus.EventBus
import timber.log.Timber
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/11/2021.
 */

@HiltViewModel
class SubjectPropertyViewModel @Inject constructor(private val repository: SubjectPropertyRepo ) : ViewModel() {

    private val _propertyType: MutableLiveData<ArrayList<DropDownResponse>> = MutableLiveData()
    val propertyType: LiveData<ArrayList<DropDownResponse>> get() = _propertyType

    private val _occupancyType: MutableLiveData<ArrayList<DropDownResponse>> = MutableLiveData()
    val occupancyType: LiveData<ArrayList<DropDownResponse>> get() = _occupancyType

    private val _countries: MutableLiveData<ArrayList<CountriesModel>> = MutableLiveData()
    val countries: LiveData<ArrayList<CountriesModel>> get() = _countries

    private val _counties: MutableLiveData<ArrayList<CountiesModel>> = MutableLiveData()
    val counties: LiveData<ArrayList<CountiesModel>> get() = _counties

    private val _states: MutableLiveData<ArrayList<StatesModel>> = MutableLiveData()
    val states: LiveData<ArrayList<StatesModel>> get() = _states

    private val _subjectPropertyDetails: MutableLiveData<SubjectPropertyDetails> = MutableLiveData()
    val subjectPropertyDetails: LiveData<SubjectPropertyDetails> get() = _subjectPropertyDetails

    private val _refinanceDetails: MutableLiveData<SubjectPropertyRefinanceDetails> =
        MutableLiveData()
    val refinanceDetails: LiveData<SubjectPropertyRefinanceDetails> get() = _refinanceDetails

    private val _coBorrowerOccupancyStatus: MutableLiveData<CoBorrowerOccupancyStatus> =
        MutableLiveData()
    val coBorrowerOccupancyStatus: LiveData<CoBorrowerOccupancyStatus> get() = _coBorrowerOccupancyStatus


    suspend fun getCoBorrowerOccupancyStatus(token: String, loanApplicationId: Int) {
        //Timber.e("CoBorrower: " + loanApplicationId + "Auth Token: " + token)
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repository.getCoBorrowerOccupancyStatus(
                token = token,
                loanApplicationId = loanApplicationId
            )
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _coBorrowerOccupancyStatus.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getSubjectPropertyDetails(token: String, loanApplicationId: Int) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repository.getSubjectPropertyDetails(
                token = token,
                loanApplicationId = loanApplicationId
            )
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _subjectPropertyDetails.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getRefinanceDetails(token: String, loanApplicationId: Int) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repository.getSubjectPropertyRefinance(
                token = token,
                loanApplicationId = loanApplicationId
            )
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                    _refinanceDetails.value = (responseResult.data)
                } else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getPropertyTypes(token: String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repository.getPropertyType(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _propertyType.value = (responseResult.data)
                /*else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                     else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                    */

            }
        }
    }

    suspend fun getOccupancyType(token: String) {
            viewModelScope.launch(Dispatchers.IO) {
                val responseResult = repository.getOccupancyType(token = token)
                withContext(Dispatchers.Main) {
                    if (responseResult is Result.Success)
                        _occupancyType.value = (responseResult.data)
                    /*else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                   EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                   EventBus.getDefault().post(WebServiceErrorEvent(responseResult))

                 */

                }
            }
        }

    suspend fun getStates(token: String) {
            viewModelScope.launch(Dispatchers.IO) {
                val response = repository.getStates(token = token)
                withContext(Dispatchers.Main) {
                    if (response is Result.Success)
                        _states.value = (response.data)
                    else if (response is Result.Error && response.exception.message == AppConstant.INTERNET_ERR_MSG) {
                        //Timber.e(" GetStates " + response.toString())
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                    } else if (response is Result.Error) {
                        //Timber.e(" GetStates " + response.toString())
                        EventBus.getDefault().post(WebServiceErrorEvent(response))
                    }
                }
            }
        }

    suspend fun getCounty(token: String) {
            viewModelScope.launch(Dispatchers.IO) {
                val responseResult = repository.getCounties(token = token)
                withContext(Dispatchers.Main) {
                    if (responseResult is Result.Success) {
                        //Timber.e("Counties-Success")
                        _counties.value = (responseResult.data)
                    } else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                    else if (responseResult is Result.Error) {
                        //Timber.e(" Counties-Error " + responseResult.toString())
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                    }
                }
            }
        }

    suspend fun getCountries(token: String) {
            viewModelScope.launch(Dispatchers.IO) {
                val responseResult = repository.getCountries(token = token)
                withContext(Dispatchers.Main) {
                    if (responseResult is Result.Success)
                        _countries.value = (responseResult.data)
                    else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                    else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                }
            }
        }

}