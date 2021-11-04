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

//    suspend fun getOtherAsset(token: String, loanApplicationId : Int, borrowerId : Int, borrowerAssetId : Int): Result<OtherAssetResponse> {
//        return dataSource.getOtherAssetDetails(token = token, loanApplicationId,borrowerId, borrowerAssetId)
//    }
}