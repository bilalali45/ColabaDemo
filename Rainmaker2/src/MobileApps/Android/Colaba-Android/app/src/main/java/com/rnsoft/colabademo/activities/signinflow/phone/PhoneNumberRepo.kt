package com.rnsoft.colabademo

import android.content.Context
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject

class PhoneNumberRepo @Inject
constructor(
    private val phoneNumberRepo : PhoneNumberDataSource,
    @ApplicationContext val applicationContext: Context
) {
    suspend fun skipTwoFactorAuthentication(intermediateToken: String): Result<SkipTwoFactorResponse> {
        return phoneNumberRepo.skipTwoFactorService(intermediateToken)
    }

    suspend fun sendOtpToPhone(intermediateToken: String, phoneNumber:String): Result<OtpToNumberResponse> {
        return phoneNumberRepo.sendOtpService(intermediateToken,phoneNumber)
    }


}