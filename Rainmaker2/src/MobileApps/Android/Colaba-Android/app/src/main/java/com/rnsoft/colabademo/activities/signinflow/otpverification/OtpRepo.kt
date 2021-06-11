package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import android.util.Log
import com.google.gson.Gson
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject


class OtpRepo @Inject
constructor(
    private val otpDataSource : OtpDataSource, private val sharedPref: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
) {

    suspend fun startOtpVerification(intermediateToken: String, phoneNumber:String, otp:Int): Result<OtpVerificationResponse> {

        val otpResponseResult =  otpDataSource.verifyOtpService(intermediateToken, phoneNumber, otp)
        if(otpResponseResult is Result.Success)
            otpResponseResult.data.data?.let { storeLoggedInUserInfo(it) } // mark user as logged in completely...
        return otpResponseResult
    }

    private fun storeLoggedInUserInfo(data: Data) {

        sharedPref.putString(ColabaConstant.token, data.token).apply()

        sharedPref.putString(ColabaConstant.refreshToken, data.refreshToken)
            .apply()
        sharedPref.putInt(ColabaConstant.userProfileId, data.userProfileId)
            .apply()
        sharedPref.putString(ColabaConstant.userName, data.userName).apply()
        sharedPref.putString(ColabaConstant.validFrom, data.validFrom).apply()
        sharedPref.putString(ColabaConstant.validTo, data.validTo).apply()
        sharedPref.putInt(ColabaConstant.tokenType, data.tokenType).apply()
        sharedPref.putString(ColabaConstant.tokenTypeName, data.tokenTypeName)
            .apply()
        sharedPref.putString(
            ColabaConstant.refreshTokenValidTo,
            data.refreshTokenValidTo
        ).apply()

        if(data.tokenTypeName == ColabaConstant.AccessToken)
            sharedPref.putBoolean(ColabaConstant.IS_LOGGED_IN, true).apply() // mark user as logged in completely...
    }



    suspend fun notAskForOtpAgain(token: String): Result<NotAskForOtpResponse> {
        return otpDataSource.notAskForOtpAgain(token)
    }


}