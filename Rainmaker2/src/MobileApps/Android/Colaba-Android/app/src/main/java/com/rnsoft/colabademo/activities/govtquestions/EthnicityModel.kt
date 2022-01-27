package com.rnsoft.colabademo.activities.govtquestions

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class EthnicityModel {

    @SerializedName("ethnicityId")
    @Expose
    var ethnicityId: Int = 0


    @SerializedName("ethnicityDetails")
    @Expose
    val ethnicityDetails: ArrayList<Detailmodel>? = ArrayList()
}