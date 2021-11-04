package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

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


data class CurrentAddress(
    val loanApplicationId: Int,
    val borrowerId: Int,
    val id: Int?,
    val housingStatusId: Int?,
    val addressModel: AddressModel?,
    val fromDate: String?,
    val isMailingAddressDifferent: Boolean?,
    val mailingAddressModel: Any?,
    val monthlyRent: Double?
)

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

data class AddressModel(
    val city: String?,
    val countryId: Int?,
    val countryName: String?,
    val countyId: Int?,
    val countyName: String?,
    val stateId: Int?,
    val stateName: String?,
    val street: String?,
    val unit: String?,
    val zipCode: String?
)

data class MaritalStatus(
    val loanApplicationId: Int,
    val borrowerId: Int,
    val firstName: String?,
    val isInRelationship: Any?,
    val lastName: String?,
    val maritalStatusId: Int?,
    val middleName: String?,
    val otherRelationshipExplanation: Any?,
    val relationFormedStateId: Any?,
    val relationWithPrimaryId: Int?,
    val relationshipTypeId: Any?,
    val spouseBorrowerId: Int?,
    val spouseLoanContactId: Any?,
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
