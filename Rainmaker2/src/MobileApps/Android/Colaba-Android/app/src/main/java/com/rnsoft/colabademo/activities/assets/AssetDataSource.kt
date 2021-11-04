package com.rnsoft.colabademo

import android.util.Log
import com.rnsoft.colabademo.activities.assets.fragment.model.*
import timber.log.Timber
import java.io.IOException
import javax.inject.Inject

/**
 * Created by Anita Kiran on 11/1/2021.
 */

class AssetDataSource @Inject constructor(private val serverApi: ServerApi) {


    suspend fun fetchAssetTypesByCategoryItemList(token: String , categoryId:Int, loanPurposeId:Int) : Result<ArrayList<GetAssetTypesByCategoryItem>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.fetchAssetTypesByCategoryItemList(newToken , categoryId = categoryId, loanPurposeId = loanPurposeId)
            Timber.e("GetAssetTypesByCategoryItem:  - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }


    suspend fun getBankAccountDetails(
        token : String,
        loanApplicationId : Int,
        borrowerId : Int,
        borrowerAssetId : Int
    ): Result<BankAccountResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBankAccountDetails(newToken, loanApplicationId,borrowerId,borrowerAssetId)
            Log.e("BankAccount-Details----", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getBankAccountType(token: String) : Result<ArrayList<DropDownResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getBankAccountType(newToken)
            Timber.e("BankAccountTyppes:  - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getRetirementAccountDetails(
        token : String,
        loanApplicationId : Int,
        borrowerId : Int,
        borrowerAssetId : Int
    ): Result<RetirementAccountResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getRetirementAccountDetails(newToken, loanApplicationId,borrowerId,borrowerAssetId)
            Log.e("BankAccount-Details----", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getFinancialAssetDetails(
        token : String,
        loanApplicationId : Int,
        borrowerId : Int,
        borrowerAssetId : Int
    ): Result<FinancialAssetResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getFinancialAssetDetails(newToken, loanApplicationId,borrowerId,borrowerAssetId)
            Log.e("Financial Asset Response----", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getAllFinancialAssets(token: String) : Result<ArrayList<DropDownResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getFinancialAsset(newToken)
            Timber.e("BankAccountTyppes:  - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getFromLoanNonRealEstateDetail(
        token : String, loanApplicationId : Int, borrowerId : Int, AssetTypeId:Int, borrowerAssetId : Int): Result<AssetsRealEstateResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getFromLoanNonRealEstateDetail(newToken, loanApplicationId,borrowerId, AssetTypeId,borrowerAssetId)
            Log.e("Financial Asset Response----", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getFromLoanRealEstateDetail(
        token : String, loanApplicationId : Int, borrowerId : Int, AssetTypeId:Int, borrowerAssetId : Int): Result<AssetsRealEstateResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getFromLoanRealEstateDetail(newToken, loanApplicationId,borrowerId, AssetTypeId,borrowerAssetId)
            Log.e("Asset LoanReal Estate--", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getAssetByCategory(token: String, categoryId: Int, loanPurposeId:Int) : Result<ArrayList<AssetTypesByCategory>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getAssetTransactionType(newToken, categoryId, loanPurposeId)
            Timber.e("Assets by category:  - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }


    suspend fun getProceedFromLoan(
        token: String, loanApplicationId : Int, borrowerId : Int, AssetTypeId:Int, borrowerAssetId : Int): Result<ProceedFromLoanResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getProceedFromLoan(newToken, loanApplicationId,borrowerId, AssetTypeId,borrowerAssetId)
            Log.e("Proceed from loan--", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getGiftAsset(
        token: String, loanApplicationId: Int, borrowerId: Int, borrowerAssetId : Int): Result<GiftAssetResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getGiftAssetDetail(newToken, loanApplicationId,borrowerId,borrowerAssetId)
            Log.e("Gift Asset Detail--", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getAllGiftSources(token: String): Result<ArrayList<GiftSourcesResponse>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getAllGiftSources(newToken)
            Timber.e("Gift sources: - $response")
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e.cause))
        }
    }

    suspend fun getOtherAssetDetails(
        token: String, loanApplicationId: Int, borrowerId: Int, borrowerAssetId : Int): Result<OtherAssetResponse> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getOtherAssetDetails(newToken, loanApplicationId,borrowerId,borrowerAssetId)
            Log.e("OtherAssetResponse--", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

}