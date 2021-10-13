package com.rnsoft.colabademo

import android.util.Log
import java.io.IOException
import javax.inject.Inject

class BorrowerApplicationDataSource  @Inject constructor(private val serverApi: ServerApi) {

   suspend fun getBorrowerAssetsDetail(
        token: String,
        loanApplicationId: Int,
        borrowerId:Int
    ): Result<AssetsModelDataClass> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBorrowerAssetsDetail(newToken, loanApplicationId = loanApplicationId , borrowerId = borrowerId)
            Log.e("AssetsModelDataClass-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }



}