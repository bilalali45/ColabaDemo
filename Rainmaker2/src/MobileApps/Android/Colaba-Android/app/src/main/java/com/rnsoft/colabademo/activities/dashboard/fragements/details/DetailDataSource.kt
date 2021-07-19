package com.rnsoft.colabademo

import android.util.Log
import com.rnsoft.colabademo.activities.dashboard.fragements.details.model.BorrowerDocsModel
import retrofit2.Response
import java.io.IOException
import javax.inject.Inject

class DetailDataSource  @Inject constructor(private val serverApi: ServerApi){

    suspend fun getLoanInfo(token:String,loanApplicationId:Int):Result<BorrowerOverviewModel>{
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getLoanInfo(newToken, loanApplicationId)
            Log.e("getLoanInfo-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getBorrowerDocuments(token:String,loanApplicationId:Int):Result<ArrayList<BorrowerDocsModel>>{
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBorrowerDocuments(newToken, loanApplicationId)
            Log.e("BorrowerDocsModel-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

}