package com.rnsoft.colabademo

import android.util.Log
import java.io.IOException
import javax.inject.Inject

class DashBoardDataSource  @Inject constructor(private val serverApi: ServerApi){

    suspend fun logoutUser(token: String): Result<LogoutResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.logoutUser(Authorization = newToken)
            Log.e("LogoutResponse-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            Result.Success(LogoutResponse("500", null, "invalid-token ", "OK" ))
            //Result.Error(IOException("Error logging in", e))
        }
    }

    suspend fun getNotificationListing(token:String, pageSize:Int, lastId:Int, mediumId:Int):Result<ArrayList<NotificationItem>>{
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getNotificationListing(Authorization = newToken, pageSize = pageSize, lastId = lastId, mediumId = mediumId)
            Log.e("NotificationListItems- ", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getNotificationCount(token:String):Result<TotalNotificationCount>{
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getNotificationCount(newToken)
            Log.e("NotificationCount-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun readNotifications(token:String , ids:ArrayList<Int>):Result<Any>{
        return try {
            val newToken = "Bearer $token"
            val putParams = PutParameters(ids)
            val response = serverApi.readNotifications(newToken, putParams)
            Log.e("read-Notifications-", response.toString())
            if(response.isSuccessful)
                Result.Success(response)
            else
                Result.Error(IOException("unknown webservice error"))
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun seenNotifications(token:String,ids:ArrayList<Int>):Result<Any>{
        return try {
            val newToken = "Bearer $token"
            val putParams = PutParameters(ids)
            val response = serverApi.seenNotifications(newToken , putParams)
            Log.e("seen-Notifications-", response.toString())
            if(response.isSuccessful)
                Result.Success(response)
            else
                Result.Error(IOException("unknown webservice error"))
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun deleteNotifications(token:String,ids:ArrayList<Int>):Result<Any>{
        return try {
            val newToken = "Bearer $token"
            val putParams = PutParameters(ids)
            val response = serverApi.deleteNotifications(newToken, putParams)
            Log.e("delete-Notifications-", response.toString())
            if(response.isSuccessful)
                Result.Success(response)
            else
                Result.Error(IOException("unknown webservice error"))
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

}