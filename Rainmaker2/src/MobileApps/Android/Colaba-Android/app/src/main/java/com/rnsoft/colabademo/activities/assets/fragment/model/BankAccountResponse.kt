package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

/**
 * Created by Anita Kiran on 11/1/2021.
 */
data class BankAccountResponse(
    val code: String?,
    @SerializedName("data") val bankAccountData: BankAccountData?,
    val message: String?,
    val status: String?
)

data class BankAccountData(

    @SerializedName("id") val assetUniqueId: Int?,
    val assetTypeId: Int?,
    val institutionName: String?,
    val accountNumber: String?,
    val balance: Double?,
    val loanApplicationId: Int?,
    val borrowerId: Int?
)


