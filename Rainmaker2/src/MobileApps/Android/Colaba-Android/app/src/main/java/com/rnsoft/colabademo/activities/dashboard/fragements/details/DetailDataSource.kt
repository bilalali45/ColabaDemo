package com.rnsoft.colabademo

import android.util.Log
import okhttp3.ResponseBody
import retrofit2.Response
import java.io.IOException
import java.io.InputStream
import javax.inject.Inject

class DetailDataSource  @Inject constructor(private val serverApi: ServerApi) {

    suspend fun getLoanInfo(token: String, loanApplicationId: Int): Result<BorrowerOverviewModel> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getLoanInfo(newToken, loanApplicationId)
            Log.e("getLoanInfo-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getBorrowerDocuments(
        token: String,
        loanApplicationId: Int
    ): Result<ArrayList<BorrowerDocsModel>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBorrowerDocuments(newToken, loanApplicationId)
            Log.e("BorrowerDocsModel-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getBorrowerApplicationTabData(
        token: String,
        loanApplicationId: Int
    ): Result<BorrowerApplicationTabModel> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBorrowerApplicationTabData(newToken, loanApplicationId)
            Log.e("ApplicationTabData-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun downloadFile(token:String, id:String, requestId:String, docId:String, fileId:String):Response<ResponseBody>?{
        try {
            val newToken = "Bearer $token"
            val result = serverApi.downloadFile(Authorization = newToken, id = id, requestId = requestId, docId = docId, fileId = fileId)
            Log.e("downloadFile-", result.toString())
            return result
        } catch (e: Throwable) {
            /*
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
             */

            return  null

        }
    }


}