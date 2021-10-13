package com.rnsoft.colabademo.activities.loan

import android.util.Log
import com.rnsoft.colabademo.AppConstant
import com.rnsoft.colabademo.NoConnectivityException
import com.rnsoft.colabademo.Result
import com.rnsoft.colabademo.ServerApi
import com.rnsoft.colabademo.activities.model.LoanInfoPurchase
import com.rnsoft.colabademo.activities.model.SubjectPropertyRefinanceDetails
import java.io.IOException
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/13/2021.
 */
class LoanInfoDataSource @Inject constructor(private val serverApi: ServerApi) {


    suspend fun getLoanInfoDetails(
        token: String,
        loanApplicationId: Int
    ): Result<LoanInfoPurchase> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getLoanInfoDetails(newToken, loanApplicationId)
            Log.e("Loan_info_Response", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }
}