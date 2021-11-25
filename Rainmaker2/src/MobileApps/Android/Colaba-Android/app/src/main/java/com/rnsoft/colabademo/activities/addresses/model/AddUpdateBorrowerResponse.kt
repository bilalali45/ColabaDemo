package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class AddUpdateBorrowerResponse(
    val loanApplicationId: Int,
    val borrowerId: Int,
    val borrowerBasicDetails: BorrowerBasicDetails,
    val currentAddress: CurrentAddress?,
    val previousAddresses: List<PreviousAddresses>?,
    val borrowerCitizenship: BorrowerCitizenship,
    val maritalStatus: MaritalStatus,
    val militaryServiceDetails: MilitaryServiceDetails,
)

data class BorrowerBasicDetails(
    val borrowerId: Int,
    val loanApplicationId: Int,
    val firstName: String?,
    val lastName: String?,
    val middleName: String?,
    val suffix: String?,
    val emailAddress: String?,
    val homePhone: String?,
    val workPhone: String?,
    val workPhoneExt: String?,
    val cellPhone: String?,
    val ownTypeId: Int?
    )


data class CurrentAddress(
    val loanApplicationId: Int?,
    val borrowerId: Int?,
    val id: Int?,
    val housingStatusId: Int?,
    val monthlyRent: Int?,
    val fromDate: String?,
    @SerializedName("addressModel")
    val addressModel: AddressModel?,
    val isMailingAddressDifferent: Boolean?,
    val mailingAddressModel: Any?,
)

data class PreviousAddresses(
    val housingStatusId: Int?,
    val id: Int?,
    val monthlyRent: Double?,
    val fromDate: String?,
    val toDate: String?,
    @SerializedName("addressModel")
    val addressModel: AddressModel?,
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

data class BorrowerCitizenship(
    val borrowerId: Int?,
    val loanApplicationId: Int?,
    val residencyTypeId: Int?,
    val residencyStatusId: Int?,
    val residencyStatusExplanation: String?,
    val dependentCount: Int?,
    val dependentAges: String?,
    val dobUtc: String?,
    val ssn: String?
)

data class MaritalStatus(
    val borrowerId: Int?,
    val firstName: String?,
    val lastName: String?,
    val middleName: String?,
    val isInRelationship: Boolean?,
    val loanApplicationId: Int?,
    val maritalStatusId: Int?,
    val otherRelationshipExplanation: String?,
    val relationFormedStateId: Int?,
    val relationWithPrimaryId: Int?,
    val relationshipTypeId: Int?,
    val spouseBorrowerId: Int?,
    val spouseLoanContactId: Int?,
    val spouseMaritalStatusId: Int?
)

data class MilitaryServiceDetails(
    val details: List<MilitaryDetails>?,
    val isVaEligible: Boolean?
)


data class MilitaryDetails(
    val expirationDateUtc: String?,
    val militaryAffiliationId: Int?,
    val reserveEverActivated: Boolean?
)










