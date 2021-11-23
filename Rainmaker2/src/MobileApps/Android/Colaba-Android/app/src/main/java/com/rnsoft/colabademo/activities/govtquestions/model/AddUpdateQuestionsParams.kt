package com.rnsoft.colabademo

import android.os.Parcelable
import kotlinx.android.parcel.Parcelize
import kotlinx.android.parcel.RawValue


data class AddUpdateQuestionsParams(
    val BorrowerId: Int = 0,
    val LoanApplicationId: Int = 5,
    val Questions: ArrayList<QuestionData> = arrayListOf()
)