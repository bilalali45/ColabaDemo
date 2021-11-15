package com.rnsoft.colabademo.activities.assets.fragment.model

data class GiftAddUpdateParams(
    val AssetTypeId: Int,
    val BorrowerId: Int,
    val Description: String,
    val GiftSourceId: Int,
    val Id: Int,
    val IsDeposited: Boolean,
    val LoanApplicationId: Int,
    val Value: Int,
    val valueDate: String
)