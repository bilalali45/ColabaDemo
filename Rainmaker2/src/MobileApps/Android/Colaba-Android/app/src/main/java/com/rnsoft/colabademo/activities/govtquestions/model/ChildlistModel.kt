package com.rnsoft.colabademo.activities.govtquestions.model

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class ChildlistModel {

    @SerializedName("liabilityName")
    @Expose
    var liabilityName: String? = null


    @SerializedName("liabilityTypeId")
    @Expose
    var liabilityTypeId: Int? = null

    @SerializedName("monthlyPayment")
    @Expose
    var monthlyPayment: Int? = null

    @SerializedName("name")
    @Expose
    var name: String? = null


    @SerializedName("remainingMonth")
    @Expose
    var remainingMonth: Int? = null


}