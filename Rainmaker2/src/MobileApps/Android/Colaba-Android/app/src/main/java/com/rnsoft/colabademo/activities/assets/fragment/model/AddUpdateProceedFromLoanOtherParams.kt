package com.rnsoft.colabademo

data class AddUpdateProceedFromLoanOtherParams(
    val AssetCategoryId: Int,
    val AssetTypeId: Int,
    val AssetValue: Int,
    val BorrowerId: Int,
    val CollateralAssetDescription: String,
    val ColletralAssetTypeId: Int,
    val LoanApplicationId: Int
)