package com.rnsoft.colabademo

import android.os.Parcel
import android.os.Parcelable
import androidx.versionedparcelable.VersionedParcelize

//data class AllLoansArrayList(val loanItem:ArrayList<LoanItem>?)

@VersionedParcelize
data class DocItem(
    var docUploadedTime: String? = "",
    var docType: String? = "",
    var docOneName: String?  = null,
    var docOneImage: String?  = null,
    var docTwoName: String?  = null,
    var docTwoImage: String?  = null,
    var docSetId: Int?  = null,
    var totalDocs: Int?  = null
    ): Parcelable {


    constructor(parcel: Parcel) : this(
        parcel.readString(),
        parcel.readString(),
        parcel.readString(),
        parcel.readString(),
        parcel.readString(),
        parcel.readString(),
        parcel.readValue(Int::class.java.classLoader) as? Int,
        parcel.readValue(Int::class.java.classLoader) as? Int
    ) {
    }

    override fun writeToParcel(parcel: Parcel, flags: Int) {
        parcel.writeString(docUploadedTime)
        parcel.writeString(docType)
        parcel.writeString(docOneName)
        parcel.writeString(docOneImage)
        parcel.writeString(docTwoName)
        parcel.writeString(docTwoImage)
        parcel.writeValue(docSetId)
        parcel.writeValue(totalDocs)
    }

    override fun describeContents(): Int {
        return 0
    }

    companion object CREATOR : Parcelable.Creator<DocItem> {
        override fun createFromParcel(parcel: Parcel): DocItem {
            return DocItem(parcel)
        }

        override fun newArray(size: Int): Array<DocItem?> {
            return arrayOfNulls(size)
        }
    }


}

