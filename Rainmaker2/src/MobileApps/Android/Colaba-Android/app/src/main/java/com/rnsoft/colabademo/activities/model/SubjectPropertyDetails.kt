package com.rnsoft.colabademo

import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import kotlinx.android.parcel.Parcelize

/**
 * Created by Anita Kiran on 10/12/2021.
 */
data class SubjectPropertyDetails(
    val code: String,
    @SerializedName("data") val subPropertyData : SubPropertyData?,
    val message: String?,
    val status: String
)

data class SubPropertyData(
    var loanApplicationId : Int,
    var propertyTypeId : Int?,
    var occupancyTypeId : Int?,
    var appraisedPropertyValue : Double?,
    var propertyTax : Double?,
    var homeOwnerInsurance : Double?,
    var floodInsurance : Double?,
    val address : AddressData?,
    var isMixedUseProperty : Boolean?,
    var mixedUsePropertyExplanation : String?,
    var subjectPropertyTbd : Boolean?
)

@Parcelize
data class AddressData(
    var street : String?,
    var unit : String?,
    var city : String?,
    var stateId : Int?,
    var zipCode : String?,
    var countryId : Int?,
    var countryName : String?,
    var stateName : String?,
    var countyId: Int?,
    var countyName : String? = null
) : Parcelable



