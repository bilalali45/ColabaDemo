package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import dagger.hilt.android.qualifiers.ApplicationContext
import okhttp3.ResponseBody
import retrofit2.Response
import java.io.*
import javax.inject.Inject


class DetailRepo  @Inject constructor(
    private val detailDataSource: DetailDataSource, private val spEditor: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
) {


   suspend fun getLoanInfo(token:String ,loanApplicationId:Int):Result<BorrowerOverviewModel>{
        return detailDataSource.getLoanInfo(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getBorrowerDocuments(token:String ,loanApplicationId:Int):Result<ArrayList<BorrowerDocsModel>>{
        return detailDataSource.getBorrowerDocuments(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getBorrowerApplicationTabData(token:String ,loanApplicationId:Int):Result<BorrowerApplicationTabModel>{
        return detailDataSource.getBorrowerApplicationTabData(token = token , loanApplicationId = loanApplicationId)
    }

     suspend fun downloadFile(token:String , id:String, requestId:String, docId:String, fileId:String , fileName:String): Response<ResponseBody>? {
        return detailDataSource.downloadFile(token = token , id = id, requestId = requestId, docId = docId, fileId = fileId )
    }
}