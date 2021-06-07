package com.rnsoft.colabademo

import java.io.IOException
import javax.inject.Inject

class PhoneNumberDataSource @Inject constructor(private val serverApi: ServerApi) {
    suspend fun skipTwoFactorService(intermediateToken: String): Result<SkipTwoFactorResponse> {
        return try {
            val skipTwoFactorResponse = serverApi.skipTwoFactorApi(intermediateToken)
            Result.Success(skipTwoFactorResponse)
        } catch (e: Throwable) {
            Result.Error(IOException("Error logging in", e))
        }
    }

    suspend fun sendOtpService(intermediateToken: String , phoneNumber:String): Result<OtpToNumberResponse> {
        return try {
            val otpResponse = serverApi.sendTwoFaToNumber(IntermediateToken = intermediateToken, PhoneNumber = phoneNumber)
            Result.Success(otpResponse)
        } catch (e: Throwable) {
            Result.Error(IOException("Error logging in", e))
        }
    }

}