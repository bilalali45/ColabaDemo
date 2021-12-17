package com.rnsoft.colabademo

class GetTemplatesResponse : ArrayList<GetTemplatesResponseItem>()

data class GetTemplatesResponseItem(
    val docs: ArrayList<Doc>,
    val id: String,
    val name: String,
    val type: String
)

data class Doc(
    val docType: String,
    val docMessage: String,
    val docTypeId: String
)