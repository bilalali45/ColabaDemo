package com.rnsoft.colabademo

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class EthnicityModel {

    @SerializedName("ethnicityId")
    @Expose
    val ethnicityId: Int = 0


    @SerializedName("ethnicityDetails")
    @Expose
    val ethnicityDetails: ArrayList<Detailmodel>? = null
}