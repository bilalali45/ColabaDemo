package com.rnsoft.colabademo

import com.rnsoft.colabademo.activities.realestate.model.RealEstateResponse
import timber.log.Timber
import java.io.IOException
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/15/2021.
 */
class RealEstateDataSource @Inject constructor(private val serverApi: ServerApi) {

    suspend fun getRealEstateDetails(
        token: String,
        loanApplicationId: Int,
        borrowerPropertyId: Int
    ): Result<RealEstateResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getRealEstateDetails(newToken,loanApplicationId,borrowerPropertyId)
            //Log.e("RealEstate-Reponse", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            Timber.e(e.message + e.cause)
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else {
                Result.Error(IOException("Error notification -", e))
            }
        }
    }


    suspend fun getPropertyTypes(token: String): Result<ArrayList<DropDownResponse>> {
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

    suspend fun getOccupancyType(token: String): Result<ArrayList<DropDownResponse>> {
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

    suspend fun getPropertyStatus(token: String): Result<ArrayList<DropDownResponse>> {
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
}