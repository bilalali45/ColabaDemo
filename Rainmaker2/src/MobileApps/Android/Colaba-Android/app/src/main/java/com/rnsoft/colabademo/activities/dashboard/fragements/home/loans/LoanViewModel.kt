package com.rnsoft.colabademo

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.activities.signinflow.phone.events.OtpSentEvent
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

@HiltViewModel
class LoanViewModel @Inject constructor(private val loanRepo: LoanRepo) :
    ViewModel() {

    private val _allLoansArrayList = MutableLiveData<ArrayList<LoanItem>>()
    val allLoansArrayList: LiveData<ArrayList<LoanItem>> get() = _allLoansArrayList

    //val list: MutableList<AllLoansArrayList> = ArrayList()

    init {
        _allLoansArrayList.value = ArrayList()
    }

    fun getAllLoans(token:String, dateTime:String,
                    pageNumber:Int, pageSize:Int,
                    loanFilter:Int, orderBy:Int,
                    assignedToMe:Boolean)
    {
        viewModelScope.launch {
            val result = loanRepo.getAllLoans(token = token , dateTime = dateTime,
                pageNumber = pageNumber, pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe)

            if (result is Result.Success) {
                 _allLoansArrayList.value = result.data
            }
            else if(result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG)
                EventBus.getDefault().post(AllLoansLoadedEvent(null, true))
            else
                EventBus.getDefault().post(AllLoansLoadedEvent(null))
        }
    }

}