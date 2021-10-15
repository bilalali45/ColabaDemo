package com.rnsoft.colabademo

import com.rnsoft.colabademo.activities.loan.model.LoanGoalModel
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/13/2021.
 */
class LoanInfoRepo @Inject constructor(private val datasource : LoanInfoDataSource) {

    suspend fun getLoanInfo(token: String, loanApplicationId: Int): Result<LoanInfoDetailsModel> {
        return datasource.getLoanInfoDetails(token = token, loanApplicationId = loanApplicationId)
    }

    suspend fun getLoanGoals(token: String, loanPurposeId: Int): Result<ArrayList<LoanGoalModel>> {
        return datasource.getLoanGoals(token = token, loanPurposeId = loanPurposeId)
    }
}