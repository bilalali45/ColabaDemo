package com.rnsoft.colabademo.activities.govtquestions

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class Detailmodel {
    @SerializedName("detailId")
    @Expose
    var detailID: Int = 0


    @SerializedName("name")
    @Expose
    var name: String = ""


//    @SerializedName("otherRace")
//    @Expose
//    val otherRace: String = ""

    @SerializedName("isOther")
    @Expose
    var isOther: Boolean = false

}