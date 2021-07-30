package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class BorrowerDocsModel(
    val createdOn: String?,
    val docId: String?,
    val docName: String?,
    @SerializedName("files") val subFiles : ArrayList<SubFiles>,
    val id: String?,
    val requestId: String?,
    val status: String?,
    val typeId: String?,
    val userName: String?,
    val isRead: Boolean?
)

data class SubFiles(
    val byteProStatus: String,
    val clientName: String,
    val fileModifiedOn: String,
    val fileUploadedOn: String,
    val id: String,
    val isRead: Boolean,
    val mcuName: String,
    val status: Any,
    val userId: Any,
    val userName: Any
)