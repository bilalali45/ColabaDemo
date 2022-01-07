package com.rnsoft.colabademo.activities.details.model

data class SendInvitationEmailModel(
    val borrowerId: Int,
    val emailBody: String,
    val emailSubject: String,
    val loanApplicationId: Int
)