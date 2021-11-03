package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class GiftAssetResponse(
    val code: String?,
    @SerializedName("data")
    val data: GiftAssetData?,
    val message: String?,
    val status: String?
)


data class GiftAssetData(
    val assetTypeId: Int?,
    val borrowerId: Int?,
    val description: String?,
    val giftSourceId: Int?,
    val id: Int?,
    val isDeposited: Boolean?,
    val loanApplicationId: Int?,
    val value: Double?,
    val valueDate: Any?
    )