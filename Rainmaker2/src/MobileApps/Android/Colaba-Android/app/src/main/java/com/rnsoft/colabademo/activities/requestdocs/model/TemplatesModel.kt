package com.rnsoft.colabademo.activities.requestdocs.model

data class TemplatesModel(
    val docs: List<Doc>,
    val id: String,
    val name: String,
    val type: String
)

data class Doc(
    val docMessage: String,
    val docType: String,
    val docTypeId: String
)