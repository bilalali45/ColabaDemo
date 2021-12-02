package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.activities.model.StatesModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.greenrobot.eventbus.EventBus
import timber.log.Timber
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/29/2021.
 */

@HiltViewModel
class PrimaryBorrowerViewModel @Inject constructor(
    private val repo : PrimaryBorrowerRepo,
    private val commonRepo : CommonRepo
) : ViewModel() {

    private val _borrowerDetail : MutableLiveData<PrimaryBorrowerResponse?> = MutableLiveData()
    val borrowerDetail : LiveData<PrimaryBorrowerResponse?> get() = _borrowerDetail

    private val _countries : MutableLiveData<ArrayList<CountriesModel>> =   MutableLiveData()
    val countries: LiveData<ArrayList<CountriesModel>> get() = _countries

    private val _counties : MutableLiveData<ArrayList<CountiesModel>> =   MutableLiveData()
    val counties: LiveData<ArrayList<CountiesModel>> get() = _counties

    private val _states : MutableLiveData<ArrayList<StatesModel>> =   MutableLiveData()
    val states: LiveData<ArrayList<StatesModel>> get() = _states

    private val _housingStatus: MutableLiveData<ArrayList<OptionsResponse>> = MutableLiveData()
    val housingStatus: LiveData<ArrayList<OptionsResponse>> get() = _housingStatus

    suspend fun getBasicBorrowerDetail(token : String, loanApplicationId : Int, borrowerId : Int) {
        viewModelScope.launch(Dispatchers.IO){
            val responseResult = repo.getPrimaryBorrowerDetails(token = token, loanApplicationId = loanApplicationId,borrowerId = borrowerId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                    _borrowerDetail.value = (responseResult.data)
                } else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    fun refreshBorrowerInfo(){
        _borrowerDetail.value = null
        _borrowerDetail.postValue(null)
    }

    suspend fun getHousingStatus(token: String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repo.getHousingStatus(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _housingStatus.value = (responseResult.data)
                /*else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                     else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                    */
            }
        }
    }

    suspend fun getStates(token:String) {
        viewModelScope.launch(Dispatchers.IO) {
            val response = commonRepo.getStates(token = token )
            withContext(Dispatchers.Main) {
                if (response is Result.Success)
                    _states.value = (response.data)
                else if (response is Result.Error && response.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (response is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(response))
            }
        }
    }

    suspend fun getCounty(token:String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = commonRepo.getCounties(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _counties.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getCountries(token:String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = commonRepo.getCountries(token = token )
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