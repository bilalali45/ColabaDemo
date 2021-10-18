package com.rnsoft.colabademo

import android.util.Log
import java.io.IOException
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/13/2021.
 */
class LoanInfoDataSource @Inject constructor(private val serverApi: ServerApi) {


    suspend fun getLoanInfoDetails(
        token: String,
        loanApplicationId: Int
    ): Result<LoanInfoDetailsModel> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getLoanInfoDetails(newToken, loanApplicationId)
            //Log.e("Loan_info_Response", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getLoanGoals(
        token: String,
        loanPurposeId: Int
    ): Result<ArrayList<LoanGoalModel>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getLoanGoals(newToken, loanPurposeId)
            //Log.e("LoanGoals", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }
}