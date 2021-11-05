package com.rnsoft.colabademo

import javax.inject.Inject

/**
 * Created by Anita Kiran on 11/1/2021.
 */

class IncomeRepo @Inject constructor(private val dataSource: IncomeDataSource) {

    suspend fun getEmploymentDetails(token: String, loanApplicationId: Int, borrowerId: Int, incomeInfoId: Int): Result<EmploymentDetailResponse> {
        return dataSource.getEmploymentDetails(token = token, loanApplicationId, borrowerId, incomeInfoId)
    }

    suspend fun getSelfEmploymentData(token: String,borrowerId: Int, incomeInfoId: Int): Result<SelfEmploymentResponse> {
        return dataSource.getSelfEmploymentDetails(token = token,borrowerId, incomeInfoId)
    }

    suspend fun getBusinessIncome(token: String,borrowerId: Int, incomeInfoId: Int): Result<BusinessIncomeResponse> {
        return dataSource.getBusinessIncome(token = token,borrowerId, incomeInfoId)
    }

    suspend fun getMilitaryIncome(token: String,borrowerId: Int, incomeInfoId: Int): Result<MilitaryIncomeResponse> {
        return dataSource.getMilitaryIncome(token = token,borrowerId, incomeInfoId)
    }

    suspend fun getRetirementIncome(token: String,borrowerId: Int, incomeInfoId: Int): Result<RetirementIncomeResponse> {
        return dataSource.getRetirementIncome(token = token,borrowerId, incomeInfoId)
    }

    suspend fun getRetirementIncomeTypes(token: String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getRetirementIncomeTypes(token = token)
    }

    suspend fun getOtherIncome(token: String, incomeInfoId: Int): Result<OtherIncomeResponse> {
        return dataSource.getOtherIncome(token = token, incomeInfoId)
    }

    suspend fun getOtherIncomeIncomeTypes(token: String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getOtherIncomeTypes(token = token)
    }

    suspend fun getBusinessTypes(token: String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getBusinessTypes(token = token)
    }
}