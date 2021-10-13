package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName


data class AssetsModelClass(
    val code: String,
    @SerializedName("data") val borrowerAssetData : BorrowerAssetData?,
    val message: Any,
    val status: String
)

data class BorrowerAssetData(
    @SerializedName("Borrower") val assetBorrowerList :ArrayList<AssetBorrower>,
    val totalAssetsValue: Double
)


data class AssetBorrower(
    val assetsValue: Double,
    val borrowerAssets: ArrayList<BorrowerAsset>,
    val borrowerId: Int,
    val borrowerName: String,
    val ownTypeDisplayName: String,
    val ownTypeId: Int,
    val ownTypeName: String
)


data class BorrowerAsset(
    val assetCategoryId: Int,
    val assetCategoryName: String,
    val assetId: Int,
    val assetName: String,
    val assetTypeID: Int,
    val assetTypeName: String,
    val assetValue: Double,
    val isDisabledByTenant: Boolean,
    val isEarnestMoney: Boolean
)