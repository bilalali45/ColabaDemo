package com.rnsoft.colabademo.activities.details.model

data class SendInvitationEmailModel(
    val loanApplicationId: Int,
    val borrowerId: Int,
    val emailSubject: String,
    val emailBody: String
)