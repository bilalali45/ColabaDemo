package com.rnsoft.colabademo

import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import kotlinx.android.parcel.Parcelize

data class DemoGraphicResponseModel(
    val code: String?,
    @SerializedName("data") val demoGraphicData : DemoGraphicData?,
    val message: String?,
    val status: String?
)

data class DemoGraphicData(
    val borrowerId: Int?,
    val ethnicity: List<EthnicityDemoGraphic>?,
    val genderId: Int?,
    val loanApplicationId: Int?,
    val race: List<DemoGraphicRace>?,
    val state: Any?
)

data class EthnicityDemoGraphic(
    val ethnicityDetails: List<EthnicityDetailDemoGraphic>?,
    val ethnicityId: Int?
)

data class EthnicityDetailDemoGraphic(
    val detailId: Int?,
    val isOther: Boolean?,
    val otherEthnicity: String?
)

@Parcelize
data class DemoGraphicRace(
    val raceDetails: List<DemoGraphicRaceDetail>?,
    val raceId: Int?
) : Parcelable

data class DemoGraphicRaceDetail(
    val detailId: Int?,
    val isOther: Boolean?,
    val otherRace: String?
)


