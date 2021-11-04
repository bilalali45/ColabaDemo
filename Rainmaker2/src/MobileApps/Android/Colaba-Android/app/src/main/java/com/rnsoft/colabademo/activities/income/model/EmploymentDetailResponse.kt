package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class EmploymentDetailResponse(
    val code: String?,
    @SerializedName("data")
    val employmentData: EmploymentData?,
    val message: String?,
    val status: String?
)


data class EmploymentData(
    val borrowerId: Int?,
    val employerAddress: EmployerAddress?,
    val employmentInfo: EmploymentInfo?,
    val employmentOtherIncome: List<EmploymentOtherIncome>?,
    val errorMessage: String?,
    val loanApplicationId: Int?,
    val wayOfIncome: WayOfIncome?
)

data class EmployerAddress(
    val borrowerId: Int?,
    val cityId: Int?,
    val cityName: String?,
    val countryId: Int?,
    val countryName: String?,
    val incomeInfoId: Int?,
    val loanApplicationId: Int?,
    val stateId: Int?,
    val stateName: String?,
    val streetAddress: String?,
    val unitNo: String?,
    val zipCode: String?
)

data class EmploymentInfo(
    val borrowerId: Int?,
    val employedByFamilyOrParty: Boolean?,
    val employerName: String?,
    val employerPhoneNumber: String?,
    val employmentCategory: Any?,
    val endDate: String?,
    val hasOwnershipInterest: Boolean?,
    val incomeInfoId: Int?,
    val isCurrentIncome: Boolean?,
    val jobTitle: String?,
    val ownershipInterest: Boolean?,
    val startDate: String?,
    val yearsInProfession: Int?
)

data class EmploymentOtherIncome(
    val annualIncome: Double?,
    val displayName: Any?,
    val incomeTypeId: Int?,
    val name: Any?
)


data class WayOfIncome(
    val employerAnnualSalary: Double?,
    val hourlyRate: Double?,
    val hoursPerWeek: Any?,
    val isPaidByMonthlySalary: Boolean?
)