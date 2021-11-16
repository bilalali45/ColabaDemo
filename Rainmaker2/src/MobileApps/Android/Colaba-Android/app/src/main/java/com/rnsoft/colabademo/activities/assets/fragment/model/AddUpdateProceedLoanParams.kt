package com.rnsoft.colabademo

data class AddUpdateProceedLoanParams(
    val AssetCategoryId: Int,
    val AssetTypeId: Int,
    val AssetValue: Int,
    val BorrowerAssetId: Int,
    val BorrowerId: Int,
    val CollateralAssetDescription: Any,
    val ColletralAssetTypeId: Int,
    val LoanApplicationId: Int,
    val SecuredByColletral: Boolean
)