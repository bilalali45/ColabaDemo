package com.rnsoft.colabademo.activities.assets.model

import android.view.View
import com.google.gson.annotations.SerializedName


data class MyAssetBorrowerDataClass(
    val code: String?,
    @SerializedName("data") val bAssetData : BAssetData?=null,
    val message: String?,
    val status: String?
)


data class BAssetData(
    val borrower: Borrower?
)

data class Borrower(
    val assetsTotal: Double?,
    val borrowerAssets: ArrayList<BorrowerAsset>?,
    val borrowerId: Int?,
    val borrowerName: String?,
    val ownTypeDisplayName: String?,
    val ownTypeId: Int?,
    val ownTypeName: String?
)

data class BorrowerAsset(
    val assets: ArrayList<Asset>?,
    val assetsCategory: String?,
    val assetsTotal: Double?,
    val listenerAttached: View.OnClickListener= View.OnClickListener {  }
)

data class Asset(
    val assetCategoryId: Int?,
    val assetCategoryName: String?,
    val assetId: Int?,
    val assetName: String?,
    val assetTypeID: Int?,
    val assetTypeName: String?,
    val assetValue: Double?,
    val isDisabledByTenant: Boolean?,
    val isEarnestMoney: Boolean?
)