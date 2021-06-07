package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class OtpToNumberResponse(
    val code: String?,
    @SerializedName("data") val otpData: OtpData?,
    val message: String?,
    val status: String?
)

data class OtpData(
    val attemptsCount: Int,
    val phoneNumber: String
)