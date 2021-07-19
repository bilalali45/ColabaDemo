package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import com.rnsoft.colabademo.activities.dashboard.fragements.details.model.BorrowerDocsModel
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject

class DetailRepo  @Inject constructor(
    private val detailDataSource: DetailDataSource, private val spEditor: SharedPreferences.Editor
) {


   suspend fun getLoanInfo(token:String ,loanApplicationId:Int):Result<BorrowerOverviewModel>{
        return detailDataSource.getLoanInfo(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getBorrowerDocuments(token:String ,loanApplicationId:Int):Result<ArrayList<BorrowerDocsModel>>{
        return detailDataSource.getBorrowerDocuments(token = token , loanApplicationId = loanApplicationId)
    }

}