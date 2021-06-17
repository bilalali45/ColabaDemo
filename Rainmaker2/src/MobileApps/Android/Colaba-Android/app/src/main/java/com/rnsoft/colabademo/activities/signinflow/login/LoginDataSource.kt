package com.rnsoft.colabademo

import android.util.Log
import retrofit2.Response
import java.io.IOException
import javax.inject.Inject

class LoginDataSource @Inject constructor(private val serverApi: ServerApi){

    suspend fun login(userEmail: String, password: String , dontAskTwoFaIdentifier:String=""): Result<LoginResponse> {
        val serverResponse: Response<LoginResponse>
        return try {
            serverResponse = serverApi.login(LoginRequest(userEmail, password), dontAskTwoFaIdentifier )
            if(serverResponse.isSuccessful) {
                Log.e("Bingo- ", serverResponse.toString())
                Result.Success(serverResponse.body()!!)
            }
            else
            {
                Log.e("what-code ", serverResponse.toString())
                Log.e("what-code ", serverResponse.code().toString())
                Log.e("what-code ", serverResponse.errorBody().toString())
                Log.e("what-code ", serverResponse.errorBody()?.charStream().toString())
                Log.e("source- ",  serverResponse.errorBody()?.source().toString())
                val testError = serverResponse.errorBody()
                Result.Success(serverResponse.body()!!)
            }

        } catch (e: Throwable) {
            e.message?.let {
                Log.e("Error Message - ", it)
                //if(it.contains("HTTP 400", true))
                    //Result.Success(loggedInUser)
            }
            Result.Error(IOException("Error -", e ) )
        }
    }

    suspend fun tenantConfigurationSource(IntermediateToken:String): Result<TenantConfigurationResponse> {
        return try {
            val tenantConfiguration = serverApi.getMcuTenantTwoFaValuesService(IntermediateToken)
            Result.Success(tenantConfiguration)
        } catch (e: Throwable) {
            Result.Error(IOException("Error logging in", e))
        }
    }

    suspend fun getPhoneDetail(IntermediateToken:String): Result<SendTwoFaResponse> {
        return try {
            val phoneInfoDetail = serverApi.sendTwoFa(IntermediateToken)
            Result.Success(phoneInfoDetail)
        } catch (e: Throwable) {
            Result.Success(SendTwoFaResponse("404", null,"Verified mobile number not found.","OK"))
            //Result.Error(IOException("Error logging in", e))
        }
    }

    suspend fun getOtpSetting(IntermediateToken:String): Result<OtpSettingResponse> {
        return try {
            val response = serverApi.getOtpSetting(IntermediateToken)
            Result.Success(response)
        } catch (e: Throwable) {
            Result.Success(OtpSettingResponse("600", null,"Otp Setting Service error...","OK"))
        }
    }
}