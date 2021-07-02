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
            Log.e("NotificationListItem-", response.toString())
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
            Log.e("TotalNotificationCount-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

}