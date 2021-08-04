package com.rnsoft.colabademo

import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import kotlinx.android.parcel.Parcelize
import kotlinx.android.parcel.RawValue


@Parcelize
data class BorrowerDocsModel(
    val createdOn: String?,
    val docId: String?,
    val docName: String?,
    @SerializedName("files")
    val subFiles : @RawValue ArrayList<SubFiles>,
    val id: String?,
    val requestId: String?,
    val status: String?,
    val typeId: String?,
    val userName: String?,
    val message:String?
    ) : Parcelable

@Parcelize
data class SubFiles(
    val byteProStatus: String,
    val clientName: String,
    val fileModifiedOn: String,
    val fileUploadedOn: String,
    val id: String,
    val isRead: Boolean,
    val mcuName: String,
    val status: @RawValue Any,
    val userId: @RawValue Any,
    val userName: @RawValue Any,

) : Parcelable