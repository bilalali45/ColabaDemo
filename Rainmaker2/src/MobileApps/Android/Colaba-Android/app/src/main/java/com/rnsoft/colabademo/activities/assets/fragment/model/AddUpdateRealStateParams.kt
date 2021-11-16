package com.rnsoft.colabademo

data class AddUpdateRealStateParams(
    val AssetCategoryId: Int,
    val AssetTypeId: Int,
    val AssetValue: Int,
    val BorrowerId: Int,
    val Description: String,
    val LoanApplicationId: Int
)