package com.rnsoft.colabademo.activities.model

import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import kotlinx.android.parcel.Parcelize

/**
 * Created by Anita Kiran on 10/13/2021.
 */
data class SubjectPropertyRefinanceDetails(
    val code: String,
    @SerializedName("data") val subPropertyData : SubPropertyRefinanceData?,
    val message: String?,
    val status: String
)

@Parcelize
data class RefinanceAddressData(
    var street : String?,
    var unit : String?,
    var city : String?,
    var stateId : Int?,
    var zipCode : String?,
    var countryId : Int?,
    var countryName : String?,
    var stateName : String?,
    var countyId: Int?,
    var countyName : String?
) : Parcelable


data class SubPropertyRefinanceData(
    var loanApplicationId : Int,
    var propertyInfoId : Int?,
    var propertyTypeId : Int?,
    var propertyUsageId : Int?,
    var rentalIncome : Float?,
    var isMixedUseProperty : Boolean?,
    var mixedUsePropertyExplanation : String?,
    var propertyValue: Float?,
    var hoaDues : Float?,
    var dateAcquired : String?,
    var isSameAsPropertyAddress: Boolean?,
    var loanGoalId : Int?,
    var loanAmount: Float?,
    var cashOutAmount: Float?,
    var hasFirstMortgage : Boolean?,
    var hasSecondMortgage: Boolean?,
    var propertyTax: Float?,
    var floodInsurance : Float?,
    var homeOwnerInsurance: Float?,
    var firstMortgageModel : FirstMortgageModel?,
    var secondMortgageModel : SecondMortgageModel?,
    val address : RefinanceAddressData?

)

data class FirstMortgageModel(
    var propertyTaxesIncludeinPayment : Boolean?,
    var homeOwnerInsuranceIncludeinPayment : Boolean?,
    var floodInsuranceIncludeinPayment : Boolean,
    var paidAtClosing : Boolean,
    var firstMortgagePayment: Float?,
    var unpaidFirstMortgagePayment : Float?,
    var helocCreditLimit : Float?,
    var isHeloc : Boolean?
)

data class SecondMortgageModel(
    var secondMortgagePayment: Float?,
    var unpaidSecondMortgagePayment : Float?,
    var paidAtClosing: Boolean?,
    var helocCreditLimit : Float?,
    var isHeloc: Boolean?,
    var state: String?,
    var combineWithNewFirstMortgage : Boolean?,
    var wasSmTaken: Boolean?
)
