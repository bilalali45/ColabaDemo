package com.rnsoft.colabademo

import android.os.Parcelable
import kotlinx.android.parcel.Parcelize
import kotlinx.android.parcel.RawValue

@Parcelize
data class UpdateGovernmentQuestions(
    val BorrowerId: Int = 0,
    val LoanApplicationId: String = "5",
    val Questions: @RawValue ArrayList<QuestionData> = arrayListOf()
):Parcelable