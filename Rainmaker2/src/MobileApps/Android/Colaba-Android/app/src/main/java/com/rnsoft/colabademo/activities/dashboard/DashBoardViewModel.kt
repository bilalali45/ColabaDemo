package com.rnsoft.colabademo

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

@HiltViewModel
class DashBoardViewModel @Inject constructor(private val dashBoardRepo : DashBoardRepo) :
    ViewModel() {

    fun logoutUser(token:String) {
        viewModelScope.launch {
            val result = dashBoardRepo.logoutUser(token)
            if (result is Result.Success)
                EventBus.getDefault().post(LogoutEvent(result.data))
            else
                EventBus.getDefault().post(LogoutEvent(LogoutResponse("300", null, "Webservice error, can not logout...", null)))
        }
    }

    suspend fun getNotificationCount(token:String):Int {
        var count = 0
        viewModelScope.launch {
            val result = dashBoardRepo.getNotificationCount(token)
            if(result is Result.Success)
                count = result.data.count
            else if(result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG)
                count = -1
            else
                count = -100
        }

        return count
    }

    suspend fun getNotificationListing(token:String, pageSize:Int, lastId:Int, mediumId:Int):ArrayList<NotificationItem>{
        var notificationArrayList: ArrayList<NotificationItem> = ArrayList()
        viewModelScope.launch {
            val responseResult = dashBoardRepo.getNotificationListing(token = token, pageSize = pageSize, lastId = lastId, mediumId = mediumId)
            if(responseResult is Result.Success)
                notificationArrayList = responseResult.data
        }

        return notificationArrayList
    }
}