package com.rnsoft.colabademo.activities

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName
import com.rnsoft.colabademo.Detailmodel

class RaceModel {

    @SerializedName("raceId")
    @Expose
    var raceId: Int = 0


    @SerializedName("raceDetails")
    @Expose
    var raceDetails: ArrayList<Detailmodel>? = ArrayList()
}