package com.rnsoft.colabademo.activities.details.boverview.model

data class TestOverviewModel(
    val address: Address,
    val borrowers: List<Borrower>,
    val cellPhone: String,
    val downPayment: Double,
    val email: String,
    val loanAmount: Double,
    val loanGoal: String,
    val loanNumber: Any,
    val loanPurpose: String,
    val milestone: String,
    val milestoneId: Int,
    val postedOn: Any,
    val propertyType: String,
    val propertyUsage: String,
    val propertyValue: Double
)