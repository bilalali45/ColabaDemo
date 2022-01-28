package com.rnsoft.colabademo

import android.os.Parcelable
import kotlinx.android.parcel.Parcelize

/**
 * Created by Anita Kiran on 1/14/2022.
 */
@Parcelize
data class HourlyModel(
    var hourlyRate: String? = null,
    var avgWeeks:String? =  null
) : Parcelable
