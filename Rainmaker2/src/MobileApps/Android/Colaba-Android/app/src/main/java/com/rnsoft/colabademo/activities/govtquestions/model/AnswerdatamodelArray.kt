package com.rnsoft.colabademo.activities.govtquestions.model

import com.google.gson.annotations.SerializedName

class AnswerdatamodelArray {

    @SerializedName("liabilityTypeId")
    var liabilityTypeId : Int? = 0

    @SerializedName("liabilityName")
    var liabilityName : String? = null

    @SerializedName("remainingMonth")
    var remainingMonth : Int? = 0

    @SerializedName("monthlyPayment")
    var monthlyPayment : Int? = 0


    @SerializedName("name")
    var name : String? = null

}