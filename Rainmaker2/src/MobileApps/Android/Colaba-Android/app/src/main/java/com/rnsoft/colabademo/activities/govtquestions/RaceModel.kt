package com.rnsoft.colabademo

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class RaceModel {

    @SerializedName("raceId")
    @Expose
    var raceId: Int = 0


    @SerializedName("raceDetails")
    @Expose
    var raceDetails: ArrayList<Detailmodel>? = ArrayList()
}