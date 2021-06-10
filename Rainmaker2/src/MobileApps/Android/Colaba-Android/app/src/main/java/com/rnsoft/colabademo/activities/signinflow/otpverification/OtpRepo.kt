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
            sharedPref.putBoolean(ColabaConstant.IS_LOGGED_IN, true).apply() // mark user as logged in completely...
        return otpResponseResult
    }



    suspend fun notAskForOtpAgain(token: String): Result<NotAskForOtpResponse> {
        return otpDataSource.notAskForOtpAgain(token)
    }


}