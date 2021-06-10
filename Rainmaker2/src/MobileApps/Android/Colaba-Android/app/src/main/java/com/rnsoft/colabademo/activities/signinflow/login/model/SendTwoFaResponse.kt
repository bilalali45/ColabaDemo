package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class SendTwoFaResponse(
    val code: String,
    @SerializedName("data") val twoFaData: TwoFaData?,
    val message: Any?,
    val status: String
)

data class TwoFaData(
    val attemptsCount: Int,
    val phoneNumber: String?,
    val hasCompleted: Boolean?,
    val remainingTimeoutInSeconds: Int?,
    val lastSendAt: String?
)