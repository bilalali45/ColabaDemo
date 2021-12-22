package com.rnsoft.colabademo

data class SendDocRequestModel(
    val loanApplicationId: Int,
    val requests: List<DocRequestDataList>
)

data class DocRequestDataList(
    val documents: List<ReqestDocument>,
    val email: Email
)

data class Email(
    val ccAddress: Any,
    val emailBody: String,
    val emailTemplateId: String,
    val fromAddress: String,
    val subject: String,
    val toAddress: String
)

data class ReqestDocument(
    val docId: Int,
    val docMessage: String,
    val docType: String,
    val docTypeId: String,
    val requestId: Int
)