package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class MilitaryIncomeResponse(
    val code: String?,
    @SerializedName("data")
    val militaryIncomeData: MilitaryIncomeData?,
    val message: String?,
    val status: String?
)


data class MilitaryIncomeData(
    val address: CommonAddressModel?,
    val borrowerId: Int?,
    val employerName: String?,
    val id: Int?,
    val jobTitle: String?,
    val loanApplicationId: Int?,
    val militaryEntitlements: Double?,
    val monthlyBaseSalary: Double?,
    val startDate: String?,
    val yearsInProfession: Int?
)