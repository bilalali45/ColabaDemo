package com.rnsoft.colabademo.activities.assets.fragment.model

data class BankAddUpdateParams(
    val AssetTypeId: Int,
    val LoanApplicationId: Int,
    val BorrowerId: Int,
    val id: Int? = null,
    val AccountNumber: String? = null,
    val Balance: Int? = null,
    val InstitutionName: String? = null


)