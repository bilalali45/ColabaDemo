package com.rnsoft.colabademo

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.withContext
import org.greenrobot.eventbus.EventBus


import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/13/2021.
 */
@HiltViewModel
class LoanInfoViewModel @Inject constructor(private val repo: LoanInfoRepo) : ViewModel() {

    private val _loanInfoPurchase : MutableLiveData<LoanInfoDetailsModel> =   MutableLiveData()
    val loanInfoPurchase: LiveData<LoanInfoDetailsModel> get() = _loanInfoPurchase

    private val _loanGoals : MutableLiveData<ArrayList<LoanGoalModel>> =   MutableLiveData()
    val loanGoals: LiveData<ArrayList<LoanGoalModel>> get() = _loanGoals


    suspend fun getLoanInfoPurchase(token:String, loanApplicationId:Int) {
        viewModelScope.launch() {
            delay(2000)
            val responseResult = repo.getLoanInfo(token = token, loanApplicationId = loanApplicationId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _loanInfoPurchase.value = (responseResult.data)

                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

     fun getLoanGoals(token:String, loanPurposeId:Int) {
        viewModelScope.launch() {
            val responseResult = repo.getLoanGoals(token = token, loanPurposeId = loanPurposeId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _loanGoals.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun addLoanInfo(token: String,data: AddLoanInfoModel) {
        //delay(2000)
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repo.addLoanInfo(token = token,data)
            withContext(Dispatchers.Main) {
                if(responseResult is Result.Success) {
                    Log.e("Viewmodel", "${responseResult.data}")
                    Log.e("Viewmodel", "$responseResult")
                    //EventBus.getDefault().post(SendDataEvent(responseResult))
                }
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun addLoanRefinanceInfo(token: String,data: UpdateLoanRefinanceModel) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = repo.addLoanRefinanceInfo(token = token,data)
            withContext(Dispatchers.Main) {
                if(responseResult is Result.Success) {
                    Log.e("Viewmodel", "${responseResult.data}")
                    Log.e("Viewmodel", "$responseResult")
                    //EventBus.getDefault().post(SendDataEvent(responseResult))
                }
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

}