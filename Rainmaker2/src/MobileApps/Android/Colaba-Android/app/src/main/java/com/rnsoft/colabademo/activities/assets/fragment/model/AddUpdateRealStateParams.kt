package com.rnsoft.colabademo

data class AddUpdateRealStateParams(
    val BorrowerId: Int,
    val LoanApplicationId: Int,
    val AssetCategoryId: Int,
    val AssetTypeId: Int,
    val AssetValue: Int,
    val Description: String?=null,
)