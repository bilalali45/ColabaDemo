package com.rnsoft.colabademo

import android.os.Parcel
import android.os.Parcelable
import kotlinx.android.parcel.Parcelize

data class ChildAnswerParent(
    val childAnswerDataList: ArrayList<ChildAnswerData>
)

@Parcelize
data class ChildAnswerData(
    val liabilityName: String,
    val liabilityTypeId: String,
    val monthlyPayment: String,
    val name: String,
    val remainingMonth: String
):Parcelable