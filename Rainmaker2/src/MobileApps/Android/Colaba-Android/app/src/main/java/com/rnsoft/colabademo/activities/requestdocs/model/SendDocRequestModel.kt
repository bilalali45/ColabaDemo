package com.rnsoft.colabademo

data class SendDocRequestModel(
    val loanApplicationId: Int,
    val requests: List<DocRequestDataList>
)

data class DocRequestDataList(
    val documents: List<RequestDocument>,
    val email: Email
)

data class Email(
    val ccAddress: String? = null,
    val emailBody: String? = null,
    val emailTemplateId: String?=null,
    val fromAddress: String? = null,
    val subject: String,
    val toAddress: String
)

data class RequestDocument(
    val docId: String?= null,
    val docMessage: String,
    val docType: String,
    val docTypeId: String?=null,
    val requestId: Int? = null
)