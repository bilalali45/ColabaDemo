package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.rnsoft.colabademo.activities.model.LoanInfoPurchase
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.withContext
import org.greenrobot.eventbus.EventBus


import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/13/2021.
 */
@HiltViewModel
class LoanInfoViewModel @Inject constructor(private val repo: LoanInfoRepo) : ViewModel() {


    private val _loanInfoPurchase : MutableLiveData<LoanInfoPurchase> =   MutableLiveData()
    val loanInfoPurchase: LiveData<LoanInfoPurchase> get() = _loanInfoPurchase


    suspend fun getLoanInfoPurchase(token:String, loanApplicationId:Int) {
        viewModelScope.launch() {
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

}