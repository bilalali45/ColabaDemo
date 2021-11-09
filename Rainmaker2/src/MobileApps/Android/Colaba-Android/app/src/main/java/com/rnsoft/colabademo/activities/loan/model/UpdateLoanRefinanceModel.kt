package com.rnsoft.colabademo

/**
 * Created by Anita Kiran on 11/9/2021.
 */
data class UpdateLoanRefinanceModel(
    val cashOutAmount: Int?,
    val downPayment: Double,
    val loanApplicationId: Int,
    val loanGoalId: Int,
    val loanPurposeId: Int,
)
