package com.rnsoft.colabademo

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

@HiltViewModel
class DashBoardViewModel @Inject constructor(private val dashBoardRepo : DashBoardRepo) :
    ViewModel() {

    private val _notificationItemList : MutableLiveData<ArrayList<NotificationItem>> =   MutableLiveData()
    val notificationItemList: LiveData<ArrayList<NotificationItem>> get() = _notificationItemList

    //private var lastNotificationItem:NotificationItem? = null

    private val _notificationCount: MutableLiveData<Int> =  MutableLiveData()
    val notificationCount:LiveData<Int> = _notificationCount

    fun setFakeCount(itemCount: Int) {
        _notificationCount.value = itemCount
    }


    fun logoutUser(token:String) {
        viewModelScope.launch {
            val result = dashBoardRepo.logoutUser(token)
            if (result is Result.Success)
                EventBus.getDefault().post(LogoutEvent(result.data))
            else
                EventBus.getDefault().post(LogoutEvent(LogoutResponse("300", null, "Webservice error, can not logout...", null)))
        }
    }

    /*
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
    */

     fun getNotificationCountT(token:String) {
         viewModelScope.launch {
             val result = dashBoardRepo.getNotificationCount(token)
             if (result is Result.Success)
                 _notificationCount.value = result.data.count
             else if (result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG)
                 _notificationCount.value = -1
             else
                 _notificationCount.value = -100
         }
     }

    suspend fun getNotificationListing(token:String, pageSize:Int, lastId:Int, mediumId:Int) {
            viewModelScope.launch {
                val responseResult = dashBoardRepo.getNotificationListing(
                    token = token,
                    pageSize = pageSize,
                    lastId = lastId,
                    mediumId = mediumId
                )
                if (responseResult is Result.Success) {
                    _notificationItemList.value = (responseResult.data)

                }

        }
    }


    fun getFurtherNotificationList(token:String, pageSize:Int, lastId:Int, mediumId:Int) {

            viewModelScope.launch {
                val responseResult = dashBoardRepo.getNotificationListing(
                    token = token, pageSize = pageSize,
                    lastId = lastId, mediumId = mediumId
                )
                if (responseResult is Result.Success) {
                    _notificationItemList.value = (responseResult.data)

                }


            }
    }




    fun seenNotifications(token:String , ids:ArrayList<Int>) {
        viewModelScope.launch {
            val result = dashBoardRepo.seenNotifications(token, ids)
            if (result is Result.Success) {

            }
            else if (result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG){

            }
            else{

            }
        }
    }

    fun readNotifications(token:String , ids:ArrayList<Int>) {
        viewModelScope.launch {
            val result = dashBoardRepo.readNotifications(token, ids)
            if (result is Result.Success) {
                Log.e("read-notify-", result.toString())
            }
            else if (result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG){
                Log.e("read-notify-", result.toString())
            }
            else{
                Log.e("read-notify-", result.toString())
            }
        }
    }

    fun deleteNotifications(token:String , ids:ArrayList<Int>) {
        viewModelScope.launch {
            val result = dashBoardRepo.deleteNotifications(token, ids)
            if (result is Result.Success) {
                Log.e("del-notify-", result.toString())
            }
            else if (result is Result.Error && result.exception.message == AppConstant.INTERNET_ERR_MSG){
                Log.e("del-notify-", result.toString())
            }
            else{
                Log.e("del-notify-", result.toString())
            }
        }
    }










}