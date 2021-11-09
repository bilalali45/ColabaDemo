package com.rnsoft.colabademo

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
    var rentalIncome : Double?,
    var isMixedUseProperty : Boolean?,
    var mixedUsePropertyExplanation : String?,
    var propertyValue: Double?,
    var hoaDues : Double?,
    var dateAcquired : String?,
    var isSameAsPropertyAddress: Boolean?,
    var loanGoalId : Int?,
    var loanAmount: Double?,
    var cashOutAmount: Double?,
    var hasFirstMortgage : Boolean?,
    var hasSecondMortgage: Boolean?,
    var propertyTax: Double?,
    var floodInsurance : Double?,
    var homeOwnerInsurance: Double?,
    var firstMortgageModel : FirstMortgageModel?,
    var secondMortgageModel : SecondMortgageModel?,
    val address : RefinanceAddressData?

)

@Parcelize
data class FirstMortgageModel(
    var propertyTaxesIncludeinPayment : Boolean?,
    var homeOwnerInsuranceIncludeinPayment : Boolean?,
    var floodInsuranceIncludeinPayment : Boolean?,
    var paidAtClosing : Boolean?,
    var firstMortgagePayment: Double?,
    var unpaidFirstMortgagePayment : Double?,
    var helocCreditLimit : Double?,
    var isHeloc : Boolean?
) : Parcelable

@Parcelize
data class SecondMortgageModel(
    var secondMortgagePayment: Double?,
    var unpaidSecondMortgagePayment : Double?,
    var paidAtClosing: Boolean?,
    var helocCreditLimit : Double?,
    var isHeloc: Boolean?,
    var state: String?,
    var combineWithNewFirstMortgage : Boolean?,
    var wasSmTaken: Boolean?
) : Parcelable
