package com.rnsoft.colabademo

import android.util.Log
import java.io.IOException
import javax.inject.Inject

class DashBoardDataSource  @Inject constructor(private val serverApi: ServerApi){

    suspend fun logoutUser(token: String): Result<LogoutResponse> {
        return try {
            val newToken = "Bearer "+token
            val response = serverApi.logoutUser(Authorization = newToken)
            Log.e("otp-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            Result.Success(LogoutResponse("500", null, "invalid-token ", "OK" ))
            //Result.Error(IOException("Error logging in", e))
        }
    }

}