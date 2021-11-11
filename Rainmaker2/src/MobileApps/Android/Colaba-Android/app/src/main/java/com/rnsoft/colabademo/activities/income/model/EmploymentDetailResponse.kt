package com.rnsoft.colabademo

import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import kotlinx.android.parcel.Parcelize

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

@Parcelize
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
) : Parcelable

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
    val ownershipInterest: Double?,
    val startDate: String?,
    val yearsInProfession: Int?
)

data class EmploymentOtherIncome(
    val annualIncome: Double?,
    val displayName: String?,
    val incomeTypeId: Int?,
    val name: String?
)


data class WayOfIncome(
    val employerAnnualSalary: Double?,
    val hourlyRate: Double?,
    val hoursPerWeek: Int?,
    val isPaidByMonthlySalary: Boolean?
)