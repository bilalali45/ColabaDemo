package com.rnsoft.colabademo

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class Detailmodel {
    @SerializedName("detailId")
    @Expose
    val detailID: Int = 0


    @SerializedName("name")
    @Expose
    val name: String = ""


//    @SerializedName("otherRace")
//    @Expose
//    val otherRace: String = ""

    @SerializedName("isOther")
    @Expose
    val isOther: Boolean = false

}