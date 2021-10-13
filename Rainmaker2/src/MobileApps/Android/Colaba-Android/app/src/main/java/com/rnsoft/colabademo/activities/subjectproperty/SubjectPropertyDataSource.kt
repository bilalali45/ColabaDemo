package com.rnsoft.colabademo

import android.util.Log
import com.rnsoft.colabademo.activities.model.*
import java.io.IOException
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/11/2021.
 */
class SubjectPropertyDataSource @Inject constructor(private val serverApi: ServerApi) {

    suspend fun getPropertyTypes(token: String): Result<ArrayList<PropertyType>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getPropertyTypes(newToken)
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getOccupancyType(token: String): Result<ArrayList<PropertyType>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getOccupancyType(newToken)
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }


    suspend fun getSubjectPropertyDetails(
        token: String,
        loanApplicationId: Int
    ): Result<SubjectPropertyDetails> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getSubjectPropertyDetails(newToken, loanApplicationId)
            Log.e("Sub-property-Details", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getSubjectPropertyRefinance(
        token: String,
        loanApplicationId: Int
    ): Result<SubjectPropertyRefinanceDetails> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getSubjectPropertyRefinance(newToken, loanApplicationId)
            Log.e("Sub-property-Refinance", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }


    suspend fun getCoBorrowerOccupancyStatus(
        token: String,
        loanApplicationId: Int
    ): Result<CoBorrowerOccupancyStatus> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getCoBorrowerOccupancyStatus(newToken, loanApplicationId)
            Log.e("Co-Borrow-Occupnacy", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }



    suspend fun getCountries(token: String): Result<ArrayList<CountriesModel>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getCountries(newToken)
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }


    suspend fun getCounties(token: String): Result<ArrayList<CountiesModel>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getCounties(newToken)
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getStates(token: String): Result<ArrayList<StatesModel>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getStates(newToken)
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }



}