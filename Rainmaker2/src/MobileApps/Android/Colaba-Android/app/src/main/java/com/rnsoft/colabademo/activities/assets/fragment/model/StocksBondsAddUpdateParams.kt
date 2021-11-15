package com.rnsoft.colabademo.activities.assets.fragment.model

data class StocksBondsAddUpdateParams(
    val AssetTypeId: Int,
    val LoanApplicationId: Int,
    val BorrowerId: Int,
    val AccountNumber: String,
    val Balance: Int,
    val Id: Int? = null,
    val InstitutionName: String,
)