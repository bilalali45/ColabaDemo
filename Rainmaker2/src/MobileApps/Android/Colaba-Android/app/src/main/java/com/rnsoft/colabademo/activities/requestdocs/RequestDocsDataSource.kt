package com.rnsoft.colabademo

import android.util.Log
import okhttp3.ResponseBody
import retrofit2.Response
import timber.log.Timber
import java.io.File
import java.io.IOException
import java.io.InputStream
import javax.inject.Inject

class RequestDocsDataSource  @Inject constructor(private val serverApi: ServerApi) {


    suspend fun getCategoryDocumentMcu(token: String): Result<CategoryDocsResponse>{
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

    suspend fun getEmailTemplates(token: String): Result<ArrayList<EmailTemplatesResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getEmailTemplates(newToken)
            //Timber.e(" EmailTemp-Response " + response.toString())
            Result.Success(response)

        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getEmailBody(token: String, loanApplicationId: Int, templateId: String): Result<EmailTemplatesResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getEmailBody(newToken,loanApplicationId,templateId)
            //Log.e("Email-Body", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }



}