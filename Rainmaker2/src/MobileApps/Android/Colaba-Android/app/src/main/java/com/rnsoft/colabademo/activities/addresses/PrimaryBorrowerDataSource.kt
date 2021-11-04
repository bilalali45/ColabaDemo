package com.rnsoft.colabademo

import android.util.Log
import timber.log.Timber
import java.io.IOException
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/29/2021.
 */
class PrimaryBorrowerDataSource @Inject constructor(private val serverApi: ServerApi) {

    suspend fun getPrimaryBorrowerDetails(
        token : String,
        loanApplicationId : Int,
        borrowerId : Int
    ): Result<PrimaryBorrowerResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getPrimaryBorrowerDetail(newToken, loanApplicationId,borrowerId)
            Log.e("Pri-Borrower-Details------", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getHousingStatus(token: String) : Result<ArrayList<OptionsResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getHousingStatus(newToken)
            Timber.e("Housing-Status-Response - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getRelationshipTypes(token: String) : Result<ArrayList<RelationTypesResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getRelationshipTypes(newToken)
            Timber.e("relationship-types-Response - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getCitizenship(token: String): Result<ArrayList<OptionsResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getCitizenship(newToken)
            Timber.e("Citizenship-Response - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getMilitaryAffiliation(token: String): Result<ArrayList<OptionsResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getMilitaryAffiliation(newToken)
            Timber.e("military-affiliation-Response - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getVisaStatus(token : String, residencyTypeId : Int) : Result<ArrayList<OptionsResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getVisaStatus(newToken,residencyTypeId)
            Timber.e("visa-status-Response - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }
}