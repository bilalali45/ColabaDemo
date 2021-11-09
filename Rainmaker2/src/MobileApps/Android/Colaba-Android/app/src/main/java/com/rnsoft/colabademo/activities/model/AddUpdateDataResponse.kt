package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class AddUpdateDataResponse(
    @SerializedName("code")
    val code: String?,
    @SerializedName("data")
    val data: Int?,
    @SerializedName("message")
    val message: String? = null,
    @SerializedName("status")
    val status: String?
)
