package com.rnsoft.colabademo

/**
 * Created by Anita Kiran on 11/15/2021.
 */
data class AddCurrentEmploymentModel(
    val loanApplicationId: Int?,
    val borrowerId: Int?,
    val employerAddress: AddressData?,
    val employmentInfo: EmploymentInfo?,
    val employmentOtherIncome: List<AddEmploymentOtherIncome>?,
    val wayOfIncome: WayOfIncome?
)

data class AddEmploymentOtherIncome(
    val incomeTypeId: Int?,
    val annualIncome: Double?
)


