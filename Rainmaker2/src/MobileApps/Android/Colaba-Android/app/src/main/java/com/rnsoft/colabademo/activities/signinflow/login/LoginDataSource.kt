package com.rnsoft.colabademo

import android.util.Log
import com.google.gson.Gson
import org.json.JSONObject
import retrofit2.Response
import java.io.IOException
import javax.inject.Inject


class LoginDataSource @Inject constructor(private val serverApi: ServerApi){

    suspend fun login(userEmail: String, password: String , dontAskTwoFaIdentifier:String=""): Result<LoginResponse> {
        val serverResponse: Response<LoginResponse>
        return try {
            serverResponse = serverApi.login(LoginRequest(userEmail, password), dontAskTwoFaIdentifier)
            //Log.e("login-Response","$serverResponse")

            if(serverResponse.isSuccessful)
                Result.Success(serverResponse.body()!!)
            else {
                //Log.e("what-code ", serverResponse.errorBody().toString())
                //Log.e("what-code ", serverResponse.errorBody()?.charStream().toString())
                //Log.e("source- ",  serverResponse.errorBody()?.source().toString())
                //val testError = serverResponse.errorBody()

                val jsonObj = JSONObject(serverResponse.errorBody()!!.charStream().readText())
                //Result.Success(serverResponse.body()!!)
                Result.Failure(jsonObj.getString("code"),jsonObj.getString("message"))

            }

        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException(e.localizedMessage))
        }
    }

    suspend fun tenantConfigurationSource(IntermediateToken:String): Result<TenantConfigurationResponse> {
        return try {
            val tenantConfiguration = serverApi.getMcuTenantTwoFaValuesService(IntermediateToken)
            Log.e("tenantConfiguration- ", tenantConfiguration.toString())
            Result.Success(tenantConfiguration)
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
            Result.Error(IOException("Error logging in", e))
        }
    }

    suspend fun getPhoneDetail(IntermediateToken:String): Result<SendTwoFaResponse> {
        return try {
            val phoneInfoDetail = serverApi.sendTwoFa(IntermediateToken)
            Log.e("SendTwoFaResponse- ", phoneInfoDetail.toString())
            Result.Success(phoneInfoDetail)
        } catch (e: Throwable) {
            Log.e("SendTwoFaResponse- ", e.toString())
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
            Result.Success(SendTwoFaResponse("404", null,"Verified mobile number not found.","OK"))
        }
    }

    suspend fun getOtpSetting(IntermediateToken:String): Result<OtpSettingResponse> {
        return try {
            val response = serverApi.getOtpSetting(IntermediateToken)
            Result.Success(response)
        } catch (e: Throwable) {
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
            Result.Success(OtpSettingResponse("600", null,"Otp Setting Service error...","OK"))
        }
    }
}