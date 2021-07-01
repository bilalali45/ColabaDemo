package com.rnsoft.colabademo

import android.os.Parcel
import android.os.Parcelable
import androidx.versionedparcelable.VersionedParcelize

//data class AllLoansArrayList(val loanItem:ArrayList<LoanItem>?)

@VersionedParcelize
data class LoanItem(
    val activityTime: String?,
    val cellNumber: String?,
    val coBorrowerCount: Int?,
    val detail: Detail?,
    val documents: Int?,
    val email: String?,
    val firstName: String?,
    val lastName: String?,
    val loanApplicationId: Int?,
    val loanPurpose: String?,
    val milestone: String?,
    var recycleCardState:Boolean? = false
): Parcelable {
    constructor(parcel: Parcel) : this(
        parcel.readString(),
        parcel.readString(),
        parcel.readInt(),
        parcel.readParcelable(Detail::class.java.classLoader),
        parcel.readInt(),
        parcel.readString(),
        parcel.readString(),
        parcel.readString(),
        parcel.readInt(),
        parcel.readString(),
        parcel.readString()
    ) {
    }

    override fun writeToParcel(parcel: Parcel, flags: Int) {
        parcel.writeString(activityTime)
        parcel.writeString(cellNumber)
        if (coBorrowerCount != null) {
            parcel.writeInt(coBorrowerCount)
        }
        parcel.writeParcelable(detail, flags)
        if (documents != null) {
            parcel.writeInt(documents)
        }
        parcel.writeString(email)
        parcel.writeString(firstName)
        parcel.writeString(lastName)
        if (loanApplicationId != null) {
            parcel.writeInt(loanApplicationId)
        }
        parcel.writeString(loanPurpose)
        parcel.writeString(milestone)
    }

    override fun describeContents(): Int {
        return 0
    }

    companion object CREATOR : Parcelable.Creator<LoanItem> {
        override fun createFromParcel(parcel: Parcel): LoanItem {
            return LoanItem(parcel)
        }

        override fun newArray(size: Int): Array<LoanItem?> {
            return arrayOfNulls(size)
        }
    }
}


@VersionedParcelize
data class Detail(
    val address: Address?,
    val loanAmount: Int?,
    val propertyValue: Int?
): Parcelable {
    constructor(parcel: Parcel) : this(
        parcel.readParcelable(Address::class.java.classLoader),
        parcel.readInt(),
        parcel.readInt()
    ) {
    }

    override fun writeToParcel(parcel: Parcel, flags: Int) {
        parcel.writeParcelable(address, flags)
        if (loanAmount != null) {
            parcel.writeInt(loanAmount)
        }
        if (propertyValue != null) {
            parcel.writeInt(propertyValue)
        }
    }

    override fun describeContents(): Int {
        return 0
    }

    companion object CREATOR : Parcelable.Creator<Detail> {
        override fun createFromParcel(parcel: Parcel): Detail {
            return Detail(parcel)
        }

        override fun newArray(size: Int): Array<Detail?> {
            return arrayOfNulls(size)
        }
    }
}


@VersionedParcelize
data class Address(
    val city: String?,
    val countryId: Int?,
    val countryName: String?,
    val stateId: Int?,
    val stateName: String?,
    val street: String?,
    val unit: String?,
    val zipCode: String?
): Parcelable {
    constructor(parcel: Parcel) : this(
        parcel.readString(),
        parcel.readInt(),
        parcel.readString(),
        parcel.readInt(),
        parcel.readString(),
        parcel.readString(),
        parcel.readString(),
        parcel.readString()
    ) {
    }

    override fun writeToParcel(parcel: Parcel, flags: Int) {
        parcel.writeString(city)
        if (countryId != null) {
            parcel.writeInt(countryId)
        }
        parcel.writeString(countryName)
        if (stateId != null) {
            parcel.writeInt(stateId)
        }
        parcel.writeString(stateName)
        parcel.writeString(street)
        parcel.writeString(unit)
        parcel.writeString(zipCode)
    }

    override fun describeContents(): Int {
        return 0
    }

    companion object CREATOR : Parcelable.Creator<Address> {
        override fun createFromParcel(parcel: Parcel): Address {
            return Address(parcel)
        }

        override fun newArray(size: Int): Array<Address?> {
            return arrayOfNulls(size)
        }
    }
}