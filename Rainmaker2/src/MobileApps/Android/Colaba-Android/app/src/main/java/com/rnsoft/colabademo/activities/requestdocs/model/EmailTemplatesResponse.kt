package com.rnsoft.colabademo

/**
 * Created by Anita Kiran on 12/20/2021.
 */
data class EmailTemplatesResponse(
    val id: String,
    val tenantId: Int,
    val templateName: String,
    val templateDescription: String,
    val fromAddress: String,
    val toAddress: String?,
    val ccAddress: String?,
    val subject: String?,
    val emailBody: String?,
    val sortOrder: Int?
)


