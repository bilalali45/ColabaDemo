package com.rnsoft.colabademo

data class LoanItem(
    val activityTime: String,
    val cellNumber: String,
    val coBorrowerCount: Int,
    val detail: Detail,
    val documents: Int,
    val email: String,
    val firstName: String,
    val lastName: String,
    val loanApplicationId: Int,
    val loanPurpose: String,
    val milestone: String
)

data class Detail(
    val address: Address,
    val loanAmount: Int,
    val propertyValue: Int
)

data class Address(
    val city: String,
    val countryId: Int,
    val countryName: String,
    val stateId: Int,
    val stateName: String,
    val street: String,
    val unit: String,
    val zipCode: String
)