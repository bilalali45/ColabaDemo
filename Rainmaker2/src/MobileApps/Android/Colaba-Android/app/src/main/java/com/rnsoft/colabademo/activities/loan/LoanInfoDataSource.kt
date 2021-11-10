package com.rnsoft.colabademo

import android.util.Log
import com.google.gson.Gson
import retrofit2.HttpException
import retrofit2.Response
import timber.log.Timber
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

    /*
    suspend fun addUpdateLoan(token: String, data:AddLoanInfoModel): Result<AddUpdateDataResponse> {
        val serverResponse: Response<AddUpdateDataResponse>
        return try {
            val newToken = "Bearer $token"
            serverResponse = serverApi.addUpdateLoanInfo(newToken, data)
            if (serverResponse.isSuccessful)
                Result.Success(serverResponse.body()!!)
            else {
                //Log.e("what-code ", ""+serverResponse.errorBody())
                // Log.e("what-code ", serverResponse.errorBody()?.charStream().toString())
                Result.Success(serverResponse.body()!!)
            }
        } catch (e: Throwable){
            if(e is HttpException){
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            }
            else {
                Result.Error(IOException("Error logging in", e))
            }
        }
    } */
    suspend fun addUpdateLoan(token: String, data:AddLoanInfoModel): Result<AddUpdateDataResponse> {
        val serverResponse: AddUpdateDataResponse
        return try {
            //Timber.e(" print correct json format = "+data)

            val g = Gson()
            val passJson = g.toJson(data)
            val newToken = "Bearer $token"
            serverResponse = serverApi.addUpdateLoanInfo(newToken, data)
            if(serverResponse.status.equals("OK", true) )
                Result.Success(serverResponse)
            else {
               // Log.e("what-code ", ""+serverResponse.errorBody())
                // Log.e("what-code ", serverResponse.errorBody()?.charStream().toString())
                Result.Success(serverResponse)
            }

        } catch (e: Throwable){
             Log.e("errorrr",e.localizedMessage)
            if(e is HttpException){
                 Log.e("network", "issues...")
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            }
            else {
                 Log.e("erorr",e.message ?:"Error")
                Result.Error(IOException("Error logging in", e))
            }
        }
    }

    suspend fun addUpdateLoanRefinance(token: String, data: UpdateLoanRefinanceModel): Result<Any> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.addUpdateLoanRefinance(newToken,data)
            Result.Success(response.isSuccessful)
        } catch (e: Throwable){
            if(e is HttpException){
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            }
            else {
                Result.Error(IOException("Error logging in", e))
            }
        }
    }

}