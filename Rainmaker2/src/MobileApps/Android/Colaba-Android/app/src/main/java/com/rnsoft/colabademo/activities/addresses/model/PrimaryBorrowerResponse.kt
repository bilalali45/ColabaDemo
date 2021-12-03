package com.rnsoft.colabademo

import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import kotlinx.android.parcel.Parcelize

data class PrimaryBorrowerResponse(
    val code: String?,
    @SerializedName("data")
    val borrowerData: PrimaryBorrowerData?,
    val message: String?,
    val status: String?
)

data class PrimaryBorrowerData(
    val loanApplicationId: Int,
    val borrowerId: Int,
    val borrowerBasicDetails: BorrowerBasicDetails,
    val borrowerCitizenship: BorrowerCitizenship,
    val currentAddress: CurrentAddress?,
    val maritalStatus: MaritalStatus,
    val militaryServiceDetails: MilitaryServiceDetails,
    val previousAddresses: List<PreviousAddresses>?
)

data class BorrowerBasicDetails(
    val borrowerId: Int,
    val loanApplicationId: Int,
    val cellPhone: String?,
    val emailAddress: String?,
    val firstName: String?,
    val homePhone: String?,
    val lastName: String?,
    val middleName: String?,
    val ownTypeId: Int?,
    val suffix: String?,
    val workPhone: String?,
    val workPhoneExt: String?
)

data class BorrowerCitizenship(
    val borrowerId: Int,
    val loanApplicationId: Int,
    val dependentAges: String?,
    val dependentCount: Int?,
    val dobUtc: String?,
    val residencyStatusExplanation: String?,
    val residencyStatusId: Int?,
    val residencyTypeId: Int?,
    val ssn: String?
)

@Parcelize
data class CurrentAddress(
    val loanApplicationId: Int? = null,
    val borrowerId: Int? = null,
    val id: Int? = null,
    val housingStatusId: Int? = null,
    val addressModel: AddressModel? = null,
    val fromDate: String? = null,
    val isMailingAddressDifferent: Boolean? = null,
    val mailingAddressModel: AddressModel? = null,
    val monthlyRent: Double? = null
) : Parcelable

data class PreviousAddresses(
    val id: Int?,
    val housingStatusId: Int?,
    val monthlyRent: Double?,
    val fromDate: String?,
    val toDate: String?,
    val addressModel: AddressModel?,
    val isMailingAddressDifferent: Boolean?,
    val mailingAddressModel: Any?,
)

@Parcelize
data class AddressModel(
    val city: String? = null,
    val countryId: Int? = null,
    val countryName: String? = null,
    val countyId: Int? = null,
    val countyName: String? = null,
    val stateId: Int? = null,
    val stateName: String? = null,
    val street: String? = null,
    val unit: String? = null,
    val zipCode: String? = null
) : Parcelable

data class MaritalStatus(
    val loanApplicationId: Int,
    val borrowerId: Int,
    val maritalStatusId: Int?,
    val firstName: String?,
    val middleName: String?,
    val lastName: String?,
    val relationWithPrimaryId: Int?,
    val isInRelationship: Boolean?,
    val otherRelationshipExplanation: String?,
    val relationFormedStateId: Int?,
    val relationshipTypeId: Int?,
    val spouseBorrowerId: Int?,
    val spouseLoanContactId: Int?,
    val spouseMaritalStatusId: Int?
)


data class MilitaryServiceDetails(
    val details: List<MilitaryServiceDetail>?,
    val isVaEligible: Boolean?
)

data class MilitaryServiceDetail(
    val expirationDateUtc: String?,
    val militaryAffiliationId: Int?,
    val reserveEverActivated: Boolean?
)
