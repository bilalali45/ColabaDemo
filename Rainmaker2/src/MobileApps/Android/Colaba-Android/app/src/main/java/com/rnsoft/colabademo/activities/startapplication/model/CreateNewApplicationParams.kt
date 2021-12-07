package com.rnsoft.colabademo

data class CreateNewApplicationParams(
    val EmailAddress: String,
    val FirstName: String,
    val LastName: String,
    val LoanGoal: Int,
    val LoanOfficerUserId: Int,
    val LoanPurpose: Int,
    val MobileNumber: String,
    val branchId: Int,
    val contactId: Int?= null
)