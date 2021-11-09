package com.rnsoft.colabademo

import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import kotlinx.android.parcel.Parcelize

data class DemoGraphicResponseModel(
    val code: String?,
    @SerializedName("data") val demoGraphicData : DemoGraphicData?,
    val message: String?,
    val status: String?,
    var passedBorrowerId:Int?
)

data class DemoGraphicData(
    val borrowerId: Int?,
    val ethnicity: List<EthnicityDemoGraphic>?,
    val genderId: Int?,
    val loanApplicationId: Int?,
    val race: List<DemoGraphicRace>?,
    val state: String?
)

data class EthnicityDemoGraphic(
    val ethnicityDetails: List<EthnicityDetailDemoGraphic>?,
    val ethnicityId: Int?
)

@Parcelize
data class EthnicityDetailDemoGraphic(
    val detailId: Int?,
    val name: String?,
    val isOther: Boolean?,
    val otherEthnicity: String?
):Parcelable


data class DemoGraphicRace(
    val raceDetails : ArrayList<DemoGraphicRaceDetail>?,
    val raceId: Int?
)

@Parcelize
data class DemoGraphicRaceDetail(
    val detailId: Int?,
    val name: String?,
    val isOther: Boolean?,
    val otherRace: String?
):Parcelable


