package com.rnsoft.colabademo

import com.rnsoft.colabademo.activities.loan.LoanInfoDataSource
import com.rnsoft.colabademo.activities.model.LoanInfoPurchase
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/13/2021.
 */
class LoanInfoRepo @Inject constructor(private val datasource : LoanInfoDataSource){

    suspend fun getLoanInfo(token:String ,loanApplicationId:Int): Result<LoanInfoPurchase> {
        return datasource.getLoanInfoDetails(token = token , loanApplicationId = loanApplicationId)
    }
}