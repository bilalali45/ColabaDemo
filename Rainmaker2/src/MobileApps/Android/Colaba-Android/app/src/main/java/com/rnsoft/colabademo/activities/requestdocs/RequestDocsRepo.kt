package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import dagger.hilt.android.qualifiers.ApplicationContext
import okhttp3.ResponseBody
import retrofit2.Response
import java.io.*
import javax.inject.Inject


class RequestDocsRepo  @Inject constructor(private val requestDocsDataSource: RequestDocsDataSource) {

    suspend fun getEmailTemplates(token: String): Result<Any> {
        return requestDocsDataSource.getEmailTemplates(token = token )
    }

    suspend fun getCategoryDocumentMcu(token: String): Result<Any> {
        return requestDocsDataSource.getCategoryDocumentMcu(token = token )
    }

    suspend fun getTemplates(token: String): Result<GetTemplatesResponse>{
        return requestDocsDataSource.getTemplates(token = token )
    }


}