package com.rnsoft.colabademo

import timber.log.Timber
import java.io.IOException
import javax.inject.Inject

/**
 * Created by Anita Kiran on 11/1/2021.
 */
class IncomeDataSource @Inject constructor(private val serverApi: ServerApi) {

    suspend fun getEmploymentDetails(
        token: String, loanApplicationId: Int, borrowerId: Int, incomeInfoId: Int): Result<EmploymentDetailResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getEmploymentDetail(newToken,loanApplicationId,borrowerId,incomeInfoId)
            Timber.e("Employment Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getSelfEmploymentDetails(
        token: String, borrowerId: Int, incomeInfoId: Int): Result<SelfEmploymentResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getSelfEmploymentContractor(newToken,borrowerId,incomeInfoId)
            Timber.e("Self-Employment-Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getBusinessIncome(
        token: String, borrowerId: Int, incomeInfoId: Int): Result<BusinessIncomeResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBusinessIncome(newToken,borrowerId,incomeInfoId)
            Timber.e("business-income-Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }
}