package com.rnsoft.colabademo.activities.dashboard.fragements.details.model

data class BorrowerDocsModel(
    val createdOn: String,
    val docId: String,
    val docName: String,
    val files: List<File>,
    val id: String,
    val requestId: String,
    val status: String,
    val typeId: String,
    val userName: String
)