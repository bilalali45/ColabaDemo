package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class OtpSentResponse(
    val code: String?,
    @SerializedName("data") val otpData: OtpData?,
    val message: String?,
    val status: String?
)

data class OtpData(
    val attemptsCount: Int,
    val phoneNumber: String,
    val hasCompleted: Boolean?,
    val remainingTimeoutInSeconds: Int?,
    val lastSendAt: String
)