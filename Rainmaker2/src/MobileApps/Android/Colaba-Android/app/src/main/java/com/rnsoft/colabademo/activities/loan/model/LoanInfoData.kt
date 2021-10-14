package com.rnsoft.colabademo

data class LoanInfoData(
    val cashOutAmount: Double?,
    val downPayment: Double?,
    val expectedClosingDate: String?,
    val loanApplicationId: Int?,
    val loanGoalId: Int?,
    val loanGoalName: String?,
    val loanPayment: Double?,
    val loanPurposeDescription: String?,
    val loanPurposeId: Int?,
    val propertyValue: Double?
)