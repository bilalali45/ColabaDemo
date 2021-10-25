package com.rnsoft.colabademo

import android.util.Log
import com.rnsoft.colabademo.activities.assets.model.MyAssetBorrowerDataClass
import timber.log.Timber
import java.io.IOException
import javax.inject.Inject

class BorrowerApplicationDataSource  @Inject constructor(private val serverApi: ServerApi) {

   suspend fun getBorrowerAssetsDetail(
        token: String,
        loanApplicationId: Int,
        borrowerId:Int
    ): Result<MyAssetBorrowerDataClass> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBorrowerAssetsDetail(newToken, loanApplicationId = loanApplicationId , borrowerId = borrowerId)
            Timber.e("AssetsModelDataClass-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getBorrowerIncomeDetail(
        token: String,
        loanApplicationId: Int,
        borrowerId:Int
    ): Result<IncomeDetailsResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBorrowerIncomeDetail(newToken, loanApplicationId = loanApplicationId , borrowerId = borrowerId)
            Timber.e("IncomeResponse-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }



    suspend fun getGovernmentQuestions(
        token: String,
        loanApplicationId: Int,
        ownTypeId:Int, borrowerId:Int
    ): Result<GovernmentQuestionsModelClass> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getGovernmentQuestions( newToken, loanApplicationId = loanApplicationId , ownTypeId = ownTypeId,  borrowerId = borrowerId)
            Timber.e("GovernmentQuestionsModelClass - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }



}