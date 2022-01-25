package com.rnsoft.colabademo.activities

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName
import com.rnsoft.colabademo.Detailmodel

class RaceModel {

    @SerializedName("raceId")
    @Expose
    val raceId: Int = 0


    @SerializedName("raceDetails")
    @Expose
    val raceDetails: ArrayList<Detailmodel>? = null
}