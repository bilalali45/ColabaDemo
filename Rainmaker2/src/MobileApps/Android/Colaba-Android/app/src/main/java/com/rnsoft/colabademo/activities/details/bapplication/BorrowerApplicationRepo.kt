package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import com.rnsoft.colabademo.activities.assets.model.MyAssetBorrowerDataClass
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject

class BorrowerApplicationRepo  @Inject constructor(
    private val borrowerApplicationDataSource: BorrowerApplicationDataSource, private val spEditor: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
) {

   suspend fun getBorrowerAssetsDetail(token:String, loanApplicationId:Int, borrowerId:Int):Result<MyAssetBorrowerDataClass>{
        return borrowerApplicationDataSource.getBorrowerAssetsDetail(token = token , loanApplicationId = loanApplicationId, borrowerId = borrowerId )
    }

    suspend fun getBorrowerIncomeDetail(token:String, loanApplicationId:Int, borrowerId:Int):Result<IncomeDetailsResponse>{
        return borrowerApplicationDataSource.getBorrowerIncomeDetail(token = token , loanApplicationId = loanApplicationId, borrowerId = borrowerId )
    }

    suspend fun getGovernmentQuestions(token:String, loanApplicationId:Int, ownTypeId:Int, borrowerId:Int):Result<GovernmentQuestionsModelClass>{
        return borrowerApplicationDataSource.getGovernmentQuestions(
            token = token , loanApplicationId = loanApplicationId,
            ownTypeId = ownTypeId,
            borrowerId = borrowerId )
    }
}