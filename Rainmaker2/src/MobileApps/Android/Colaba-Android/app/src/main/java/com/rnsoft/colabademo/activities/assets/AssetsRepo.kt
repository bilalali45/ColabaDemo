package com.rnsoft.colabademo

import javax.inject.Inject

/**
 * Created by Anita Kiran on 11/1/2021.
 */

class AssetsRepo @Inject constructor(private val dataSource : AssetDataSource){

    suspend fun getBankAccountDetails(token: String, loanApplicationId : Int, borrowerId : Int, borrowerAssetId : Int): Result<BankAccountResponse> {
        return dataSource.getBankAccountDetails(token = token, loanApplicationId,borrowerId,borrowerAssetId)
    }

    suspend fun getBankAccountType(token: String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getBankAccountType(token = token)
    }

    suspend fun getRetirementAccountDetails(token: String, loanApplicationId : Int, borrowerId : Int, borrowerAssetId : Int): Result<RetirementAccountResponse> {
        return dataSource.getRetirementAccountDetails(token = token, loanApplicationId,borrowerId,borrowerAssetId)
    }

    suspend fun getFinancialAssetDetail(token: String, loanApplicationId : Int, borrowerId : Int, borrowerAssetId : Int): Result<FinancialAssetResponse> {
        return dataSource.getFinancialAssetDetails(token = token, loanApplicationId,borrowerId,borrowerAssetId)
    }

    suspend fun getAllFinancialAsset(token: String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getAllFinancialAssets(token = token)
    }

    suspend fun getFromLoanNonRealEstateDetail(token: String, loanApplicationId : Int, borrowerId : Int, assetTypeId:Int, borrowerAssetId : Int): Result<AssetsRealEstateResponse> {
        return dataSource.getFromLoanNonRealEstateDetail(token = token, loanApplicationId,borrowerId,assetTypeId,borrowerAssetId)
    }

    suspend fun getFromLoanRealEstateDetail(token: String, loanApplicationId : Int, borrowerId : Int, assetTypeId: Int, borrowerAssetId : Int): Result<AssetsRealEstateResponse> {
        return dataSource.getFromLoanRealEstateDetail(token = token, loanApplicationId,borrowerId,assetTypeId, borrowerAssetId)
    }

    suspend fun getAssetByCategory(token: String, categoryId: Int, loanPurposeId: Int): Result<ArrayList<AssetTypesByCategory>> {
        return dataSource.getAssetByCategory(token,categoryId,loanPurposeId)
    }

    suspend fun getProceedFromLoan(token: String, loanApplicationId : Int, borrowerId : Int, assetTypeId: Int, borrowerAssetId : Int): Result<ProceedFromLoanResponse> {
        return dataSource.getProceedFromLoan(token = token, loanApplicationId,borrowerId,assetTypeId, borrowerAssetId)
    }

    suspend fun getGiftAsset(token: String, loanApplicationId : Int, borrowerId : Int, borrowerAssetId : Int): Result<GiftAssetResponse> {
        return dataSource.getGiftAsset(token = token, loanApplicationId,borrowerId, borrowerAssetId)
    }

    suspend fun getAllGiftSources(token: String): Result<ArrayList<GiftSourcesResponse>> {
        return dataSource.getAllGiftSources(token = token)
    }

    suspend fun getOtherAsset(token: String, loanApplicationId : Int, borrowerId : Int, borrowerAssetId : Int): Result<OtherAssetResponse> {
        return dataSource.getOtherAssetDetails(token = token, loanApplicationId,borrowerId, borrowerAssetId)
    }




}