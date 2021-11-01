package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class OtherAssetResponse(
    val code: String?,
    @SerializedName("data")
    val data: OtherAssetData?,
    val message: String?,
    val status: String?
)

data class OtherAssetData(
    val accountNumber: Any?,
    val assetDescription: Any?,
    val assetId: Int?,
    val assetTypeId: Int?,
    val assetTypeName: String?,
    val assetValue: Double?,
    val institutionName: String?
)

