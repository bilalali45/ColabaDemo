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
            //Timber.e("business-income-Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getMilitaryIncome(
        token: String, borrowerId: Int, incomeInfoId: Int): Result<MilitaryIncomeResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getMilitaryIncome(newToken,borrowerId,incomeInfoId)
            //Timber.e("military-income-Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getRetirementIncome(
        token: String, borrowerId: Int, incomeInfoId: Int): Result<RetirementIncomeResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getRetirementIncome(newToken,borrowerId,incomeInfoId)
            //Timber.e("retirement-income-Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getRetirementIncomeTypes(
        token: String): Result<ArrayList<DropDownResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getRetirementIncomeTypes(newToken)
            Timber.e("retirement-income-types-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getOtherIncome(
        token: String, incomeInfoId: Int): Result<OtherIncomeResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getOtherIncomeInfo(newToken,incomeInfoId)
            //Timber.e("other-income-Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getOtherIncomeTypes(
        token: String): Result<ArrayList<DropDownResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getOtherIncomeTypes(newToken)
            //Timber.e("other-income-Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }
    suspend fun getBusinessTypes(
        token: String): Result<ArrayList<DropDownResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getAllBusinessTypes(newToken)
            //Timber.e("other-income-Response-- $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }
}