package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class BusinessIncomeResponse(
    val code: String?,
    @SerializedName("data")
    val businessData: BusinessData?,
    val message: String?,
    val status: String?
)

data class BusinessData(
    val loanApplicationId: Int?,
    val id: Int?,
    val borrowerId: Int?,
    val incomeTypeId: Int?,
    val address: BusinessIncomeAddress?,
    val businessName: String?,
    val businessPhone: String?,
    val startDate: String?,
    val jobTitle: String?,
    val ownershipPercentage: Double?,
    val annualIncome: Double?,
    val hasOwnershipInterest: Boolean?
)

data class BusinessIncomeAddress(
    val city: String?,
    val countryId: Int?,
    val countryName: Int?,
    val countyId: Int?,
    val countyName: String?,
    val stateId: Int?,
    val stateName: String?,
    val street: String?,
    val unit: String?,
    val zipCode: String?
)
