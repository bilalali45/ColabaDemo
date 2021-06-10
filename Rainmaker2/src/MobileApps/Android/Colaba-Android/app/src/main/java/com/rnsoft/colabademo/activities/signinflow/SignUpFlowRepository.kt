package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import com.google.gson.Gson
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject


class SignUpFlowRepository @Inject
constructor(
    private val signUpFlowDataSource: SignUpFlowDataSource, private val spEditor: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
)
{

    suspend fun sendOtpToPhone(intermediateToken: String, phoneNumber:String): Result<OtpSentResponse> {
        spEditor.putString(ColabaConstant.phoneNumber, phoneNumber).apply()
        val otpResponseResult = signUpFlowDataSource.sendOtpService(intermediateToken,phoneNumber)
        if(otpResponseResult is Result.Success)
            setUpOtpInfo(otpResponseResult.data)
        return otpResponseResult

    }

    private fun setUpOtpInfo(otpSentResponse: OtpSentResponse){
        otpSentResponse.message?.let {
            spEditor.putString(ColabaConstant.otp_message, otpSentResponse.message)
                .apply()
        }
        otpSentResponse.otpData?.let {
            val gson = Gson()
            val otpData = gson.toJson(otpSentResponse.otpData)
            spEditor.putString(ColabaConstant.otpDataJson, otpData).apply()
            spEditor.putInt(ColabaConstant.secondsCount, 0).apply()
        }
    }

    /*

    suspend fun logout() {
        user = null
        spEditor.clear()
        spEditor.putBoolean(MyAppConfigConstant.IS_LOGGED_IN, false).apply()
        spEditor.putString(MyAppConfigConstant.TOKEN, "").apply()
        //ProductsDatabase.getDatabase(applicationContext) .clearAllTables()
        //dataSource.logout()
    }

     */

}