package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.activities.assets.fragment.model.AssetTypesByCategory
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.greenrobot.eventbus.EventBus
import timber.log.Timber
import javax.inject.Inject

/**
 * Created by Anita Kiran on 11/1/2021.
 */

@HiltViewModel
class IncomeViewModel @Inject constructor(private val repo: IncomeRepo) : ViewModel(){

    private val _assetByCategory: MutableLiveData<ArrayList<AssetTypesByCategory>> = MutableLiveData()
    val assetByCategory: LiveData<ArrayList<AssetTypesByCategory>> get() = _assetByCategory

    private val _employmentDetail: MutableLiveData<EmploymentDetailResponse> = MutableLiveData()
    val employmentDetail: LiveData<EmploymentDetailResponse> get() = _employmentDetail

    private val _selfEmploymentDetail: MutableLiveData<SelfEmploymentResponse> = MutableLiveData()
    val selfEmploymentDetail: LiveData<SelfEmploymentResponse> get() = _selfEmploymentDetail

    private val _businessIncomeData: MutableLiveData<BusinessIncomeResponse> = MutableLiveData()
    val businessIncome: LiveData<BusinessIncomeResponse> get() = _businessIncomeData

    private val _militaryIncomeData: MutableLiveData<MilitaryIncomeResponse> = MutableLiveData()
    val militaryIncomeData: LiveData<MilitaryIncomeResponse> get() = _militaryIncomeData

    private val _retirementIncomeData: MutableLiveData<RetirementIncomeResponse> = MutableLiveData()
    val retirementIncomeData: LiveData<RetirementIncomeResponse> get() = _retirementIncomeData

    private val _retirementIncomeTypes: MutableLiveData<ArrayList<DropDownResponse>> = MutableLiveData()
    val retirementIncomeTypes: LiveData<ArrayList<DropDownResponse>> get() = _retirementIncomeTypes

    private val _otherIncomeData: MutableLiveData<OtherIncomeResponse> = MutableLiveData()
    val otherIncomeData: LiveData<OtherIncomeResponse> get() = _otherIncomeData



    /*suspend fun getBankAccountType(token: String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = assetsRepo.getBankAccountType(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _bankAccountType.value = (responseResult.data)
                /*else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                     else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                    */

            }
        }
    } */

    suspend fun getCurrentEmploymentDetail(token: String, loanApplicationId: Int, borrowerId: Int, incomeInfoId:Int) {
        delay(1000)
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repo.getEmploymentDetails(
                token = token,
                loanApplicationId = loanApplicationId,
                borrowerId, incomeInfoId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _employmentDetail.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getSelfEmploymentDetail(token: String, borrowerId: Int, incomeInfoId:Int){
        delay(1000)
        viewModelScope.launch(Dispatchers.IO){
            val responseResult = repo.getSelfEmploymentData(
                token = token,
                borrowerId,
                incomeInfoId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _selfEmploymentDetail.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getIncomeBusiness(token: String, borrowerId: Int, incomeInfoId:Int){
        delay(1000)
        viewModelScope.launch(Dispatchers.IO){
            val responseResult = repo.getBusinessIncome(
                token = token,
                borrowerId,
                incomeInfoId)
            withContext(Dispatchers.Main){
                if (responseResult is Result.Success)
                    _businessIncomeData.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))

            }
        }
    }

    suspend fun getMilitaryIncome(token: String, borrowerId: Int, incomeInfoId:Int){
        delay(1000)
        viewModelScope.launch(Dispatchers.IO){
            val responseResult = repo.getMilitaryIncome(
                token = token,
                borrowerId,
                incomeInfoId)
            withContext(Dispatchers.Main){
                if (responseResult is Result.Success)
                    _militaryIncomeData.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getRetirementIncome(token: String, borrowerId: Int, incomeInfoId:Int){
        //delay(1000)
        viewModelScope.launch(Dispatchers.IO){
            val responseResult = repo.getRetirementIncome(
                token = token,
                borrowerId,
                incomeInfoId)
            withContext(Dispatchers.Main){
                if (responseResult is Result.Success)
                    _retirementIncomeData.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getRetirementIncomeTypes(token: String){
        viewModelScope.launch(Dispatchers.IO){
            val responseResult = repo.getRetirementIncomeTypes(token = token)
            withContext(Dispatchers.Main){
                if (responseResult is Result.Success)
                    _retirementIncomeTypes.value = (responseResult.data)
                /*else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult)) */
            }
        }
    }

    suspend fun getOtherIncome(token: String,incomeInfoId:Int){
        delay(1000)
        viewModelScope.launch(Dispatchers.IO){
            val responseResult = repo.getOtherIncome(token = token,incomeInfoId)
            withContext(Dispatchers.Main){
                if (responseResult is Result.Success)
                    _otherIncomeData.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }


}