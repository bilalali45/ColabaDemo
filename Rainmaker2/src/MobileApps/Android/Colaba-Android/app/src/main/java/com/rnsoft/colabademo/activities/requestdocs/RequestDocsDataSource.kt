package com.rnsoft.colabademo

import android.util.Log
import okhttp3.ResponseBody
import retrofit2.Response
import java.io.File
import java.io.IOException
import java.io.InputStream
import javax.inject.Inject

class RequestDocsDataSource  @Inject constructor(private val serverApi: ServerApi) {

    suspend fun getEmailTemplates(token: String): Result<Any>{
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getEmailTemplates(newToken)
            //Log.e("getEmailTemplates-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getCategoryDocumentMcu(token: String): Result<Any>{
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getCategoryDocumentMcu(newToken)
            //Log.e("getCategoryDocumentMcu-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getTemplates(token: String): Result<GetTemplatesResponse>{
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getTemplates(newToken)
            //Log.e("getCategoryDocumentMcu-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

}