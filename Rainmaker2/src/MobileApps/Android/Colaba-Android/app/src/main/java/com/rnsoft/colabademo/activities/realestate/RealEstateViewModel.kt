package com.rnsoft.colabademo.activities.realestate

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.activities.realestate.model.RealEstateResponse
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/15/2021.
 */
@HiltViewModel
class RealEstateViewModel @Inject constructor(private val repo: RealEstateRepo) : ViewModel() {

    private val _realEstateDetails : MutableLiveData<RealEstateResponse> =   MutableLiveData()
    val realEstateDetails: LiveData<RealEstateResponse> get() = _realEstateDetails

    private val _propertyType : MutableLiveData<ArrayList<DropDownResponse>> =   MutableLiveData()
    val propertyType: LiveData<ArrayList<DropDownResponse>> get() = _propertyType

    private val _occupancyType : MutableLiveData<ArrayList<DropDownResponse>> =   MutableLiveData()
    val occupancyType: LiveData<ArrayList<DropDownResponse>> get() = _occupancyType



    suspend fun getRealEstateDetails(token:String, loanApplicationId:Int,borrowerPropertyId:Int) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repo.getRealEstateDetails(token = token, loanApplicationId = loanApplicationId, borrowerPropertyId = borrowerPropertyId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success){
                    _realEstateDetails.value = (responseResult.data)
                }
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG) {
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                }
                else if (responseResult is Result.Error){
                    //Timber.e(WebServiceErrorEvent(responseResult))
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
               }
            }
        }
    }

    suspend fun getPropertyTypes(token:String) {
        viewModelScope.launch() {
            val responseResult = repo.getPropertyType(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _propertyType.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getOccupancyType(token:String) {
        viewModelScope.launch() {
            val responseResult = repo.getOccupancyType(token = token )
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _occupancyType.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

}