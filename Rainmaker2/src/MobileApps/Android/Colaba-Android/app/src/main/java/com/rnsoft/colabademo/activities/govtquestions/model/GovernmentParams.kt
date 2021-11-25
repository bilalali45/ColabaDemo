package com.rnsoft.colabademo

import android.os.Parcelable
import kotlinx.android.parcel.Parcelize
import kotlinx.android.parcel.RawValue

@Parcelize
data class GovernmentParams(
    val BorrowerId: Int = 0,
    val LoanApplicationId: Int = 5,
    val Questions: ArrayList<QuestionData> = arrayListOf()
):Parcelable