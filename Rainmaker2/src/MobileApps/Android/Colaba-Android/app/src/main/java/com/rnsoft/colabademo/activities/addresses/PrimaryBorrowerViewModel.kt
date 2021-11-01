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

    private val _borrowerDetail : MutableLiveData<PrimaryBorrowerResponse> = MutableLiveData()
    val borrowerDetail : LiveData<PrimaryBorrowerResponse> get() = _borrowerDetail

    suspend fun getBasicBorrowerDetail(token : String, loanApplicationId : Int, borrowerId : Int) {
        //Timber.e("viewModel-getBorrowerDetails")
        viewModelScope.launch(Dispatchers.IO) {
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
}