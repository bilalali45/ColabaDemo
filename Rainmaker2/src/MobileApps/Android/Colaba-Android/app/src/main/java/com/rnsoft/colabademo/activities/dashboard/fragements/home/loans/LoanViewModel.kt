package com.rnsoft.colabademo

import android.util.Log
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

    private val _activeLoansArrayList = MutableLiveData<ArrayList<LoanItem>>()
    val activeLoansArrayList: LiveData<ArrayList<LoanItem>> get() = _activeLoansArrayList

    private val _nonActiveLoansArrayList = MutableLiveData<ArrayList<LoanItem>>()
    val nonActiveLoansArrayList: LiveData<ArrayList<LoanItem>> get() = _nonActiveLoansArrayList



    private val localLoansArrayList: ArrayList<LoanItem> = ArrayList<LoanItem>()
    private val localActiveLoansArrayList: ArrayList<LoanItem> = ArrayList<LoanItem>()
    private val localNonActiveLoansArrayList: ArrayList<LoanItem> = ArrayList<LoanItem>()


    init {
        _allLoansArrayList.value = ArrayList()
        _nonActiveLoansArrayList.value = ArrayList()
        _activeLoansArrayList.value = ArrayList()
    }

    fun getAllLoans(token:String, dateTime:String,
                    pageNumber:Int, pageSize:Int,
                    loanFilter:Int, orderBy:Int,
                    assignedToMe:Boolean)
    {
        viewModelScope.launch {
            Log.e("viewmodel-"," method is - getAllLoans")
            val result = loanRepo.getAllLoans(token = token , dateTime = dateTime,
                pageNumber = pageNumber, pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe)

            if (result is Result.Success) {
                localLoansArrayList.addAll(result.data)
                _allLoansArrayList.value = localLoansArrayList
            }
            else if(result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG)
                EventBus.getDefault().post(AllLoansLoadedEvent(null, true))
            else
                EventBus.getDefault().post(AllLoansLoadedEvent(null))
        }
    }


    fun getActiveLoans(token:String, dateTime:String,
                    pageNumber:Int, pageSize:Int,
                    loanFilter:Int, orderBy:Int,
                    assignedToMe:Boolean)
    {
        viewModelScope.launch {
            val result = loanRepo.getAllLoans(token = token , dateTime = dateTime,
                pageNumber = pageNumber, pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe)

            if (result is Result.Success) {
                localActiveLoansArrayList.addAll(result.data)
                _activeLoansArrayList.value = localActiveLoansArrayList
            }
            else if(result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG)
                EventBus.getDefault().post(AllLoansLoadedEvent(null, true))
            else
                EventBus.getDefault().post(AllLoansLoadedEvent(null))
        }
    }


    fun getNonActiveLoans(token:String, dateTime:String,
                    pageNumber:Int, pageSize:Int,
                    loanFilter:Int, orderBy:Int,
                    assignedToMe:Boolean)
    {
        viewModelScope.launch {
            val result = loanRepo.getAllLoans(token = token , dateTime = dateTime,
                pageNumber = pageNumber, pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe)

            if (result is Result.Success) {
                localNonActiveLoansArrayList.addAll(result.data)
                _nonActiveLoansArrayList.value = localNonActiveLoansArrayList
            }
            else if(result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG)
                EventBus.getDefault().post(AllLoansLoadedEvent(null, true))
            else
                EventBus.getDefault().post(AllLoansLoadedEvent(null))
        }
    }

}