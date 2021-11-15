package com.rnsoft.colabademo

import android.os.Parcelable
import kotlinx.android.parcel.Parcelize

@Parcelize
data class BankruptcyAnswerData(
    var value1: Boolean = false,
    var value2: Boolean = false,
    var value3: Boolean = false,
    var value4: Boolean = false
):Parcelable