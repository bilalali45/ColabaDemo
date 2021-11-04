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
        delay(2000)
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

    suspend fun getSelfEmploymentDetail(token: String, borrowerId: Int, incomeInfoId:Int) {
        delay(2000)
        viewModelScope.launch(Dispatchers.IO) {
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

    suspend fun getIncomeBusiness(token: String, borrowerId: Int, incomeInfoId:Int) {
        delay(2000)
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repo.getBusinessIncome(
                token = token,
                borrowerId,
                incomeInfoId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _businessIncomeData.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

}