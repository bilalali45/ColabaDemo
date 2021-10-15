package com.rnsoft.colabademo.activities.realestate.model

import com.google.gson.annotations.SerializedName

/**
 * Created by Anita Kiran on 10/15/2021.
 */
data class RealEstateResponse(
    val code: String,
    @SerializedName("data") val data : RealEstateData?,
    val message: String?,
    val status: String
)

data class RealEstateData(
    val address: RealEstateAddress?,
    val annualFloodInsurance: Double?,
    val annualHomeInsurance: Double?,
    val annualPropertyTax: Double?,
    val firstMortgageBalance: Double?,
    val firstMortgagePayment: Double?,
    val hasFirstMortgage: Boolean?,
    val hasSecondMortgage: Boolean?,
    val homeOwnerDues: Double?,
    val occupancyTypeId: Int?,
    val propertyInfoId: Int?,
    val propertyStatus: String?,
    val propertyTypeId: Int?,
    val propertyValue: Double?,
    val rentalIncome: Double?,
    val secondMortgageBalance: Double?,
    val secondMortgagePayment: Double?
)

data class RealEstateAddress(
    val city: String,
    val countryId: Int,
    val countryName: String,
    val countyId: Int,
    val countyName: Int,
    val stateId: Int,
    val stateName: String,
    val street: String,
    val unit: String,
    val zipCode: String
)

