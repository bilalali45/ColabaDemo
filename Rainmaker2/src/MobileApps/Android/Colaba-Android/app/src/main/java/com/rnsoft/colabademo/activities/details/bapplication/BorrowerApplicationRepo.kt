package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject

class BorrowerApplicationRepo  @Inject constructor(
    private val borrowerApplicationDataSource: BorrowerApplicationDataSource, private val spEditor: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
) {

   suspend fun getBorrowerAssetsDetail(token:String, loanApplicationId:Int, borrowerId:Int):Result<AssetsModelDataClass>{
        return borrowerApplicationDataSource.getBorrowerAssetsDetail(token = token , loanApplicationId = loanApplicationId, borrowerId = borrowerId )
    }

}