package com.rnsoft.colabademo.activities.model

import com.google.gson.annotations.SerializedName

/**
 * Created by Anita Kiran on 10/13/2021.
 */
data class SubjectPropertyRefinanceDetails(
    val code: String,
    @SerializedName("data") val subPropertyData : SubPropertyRefinanceData?,
    val message: String?,
    val status: String
)

data class RefinanceAddressData(
    var street : String,
    var unit : String,
    var city : String,
    var stateId : Int,
    var zipCode : String,
    var countryId : Int,
    var countryName : String,
    var stateName : String,
    var countyId: Int,
    var countyName : String? = null
)

data class SubPropertyRefinanceData(
    var loanApplicationId : Int,
    var propertyInfoId : Int,
    var propertyTypeId : Int,
    var propertyUsageId : Int,
    var rentalIncome : Int?,
    var isMixedUseProperty : Boolean,
    var mixedUsePropertyExplanation : String?,
    var propertyValue: Float,
    var hoaDues : String?,
    var dateAcquired : String?,
    var isSameAsPropertyAddress: Boolean,
    var loanGoalId : Int,
    var loanAmount: Float,
    var cashOutAmount: Long?,
    var hasFirstMortgage : Boolean?,
    var hasSecondMortgage: Boolean?,
    var propertyTax: Long?,
    var floodInsurance : Long?,
    var homeOwnerInsurance: Long?,
    var firstMortgageModel : String?,
    var secondMortgageModel: String?,
    val address : RefinanceAddressData?
)
