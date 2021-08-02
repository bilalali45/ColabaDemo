package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class BorrowerApplicationTabModel(
    val code: String,
    @SerializedName("data") val borrowerAppData : BorrowerAppData?,
    val message: String?,
    val status: String
)

data class BorrowerAppData(
    val assetAndIncome: AssetAndIncome?,
    val borrowerInformation: BorrowerInformation?,
    val loanInformation: LoanInformation?,
    val subjectProperty: SubjectProperty?
)


data class AssetAndIncome(
    val totalAsset: Double,
    val totalMonthyIncome: Double
)

data class BorrowerInformation(
    val borrowerId: Int,
    val firstName: String?,
    val lastName: String?
)

data class LoanInformation(
    val deposit: Double,
    val depositPercent: Double,
    val loanAmount: Double,
    val loanPurposeDescription: String?,
    val loanPurposeId: Int
)

data class SubjectProperty(
    @SerializedName("address") val borrowerAddress: BorrowerAddress?,
    val propertyInfoId: Int,
    val propertyTypeId: Int,
    val propertyTypeName: String?,
    val propertyUsageId: Int,
    val propertyUsageName: String?
)

data class BorrowerAddress(
    val city: String,
    val countryId: Int,
    val countryName: String,
    val stateId: Int,
    val stateName: String,
    val street: String,
    val unit: String,
    val zipCode: String
)