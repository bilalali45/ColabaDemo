package com.rnsoft.colabademo.activities.assets.fragment.model

data class OtherAssetAddUpdateParams(
    val BorrowerId: Int,
    val GiftSourceId: Int,

    val AssetTypeId: Int,

    val AccountNumber: String?=null,
    val AssetId: Int?=null,
    val Description: String?=null,
    val InstitutionName: String?=null,
    val Value: Int?=null
)