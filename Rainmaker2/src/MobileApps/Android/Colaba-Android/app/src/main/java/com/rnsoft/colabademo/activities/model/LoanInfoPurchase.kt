package com.rnsoft.colabademo.activities.model

import com.google.gson.annotations.SerializedName
import com.rnsoft.colabademo.SubPropertyData

/**
 * Created by Anita Kiran on 10/13/2021.
 */
data class LoanInfoPurchase(
    val code: String,
    @SerializedName("data") val loanDetails : LoanInfoDetails?,
    val message: String?,
    val status: String
)

data class LoanInfoDetails(
    var loanApplicationId : Int?,
    var loanPurposeId : Int?,
    var loanGoalId : Int?,
    var loanPurposeDescription: String?,
    var loanGoalName : String?,
    var propertyValue : Float?,
    var loanPayment : Float?,
    var downPayment: Float?,
    var expectedClosingDate : String?,
    var cashOutAmount : Float?
)
