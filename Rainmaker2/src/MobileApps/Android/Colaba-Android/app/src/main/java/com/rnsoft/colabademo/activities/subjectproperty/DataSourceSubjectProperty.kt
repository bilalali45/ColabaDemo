package com.rnsoft.colabademo.activities.subjectproperty

import android.util.Log
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.activities.model.PropertyType
import retrofit2.Response
import java.io.IOException
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/11/2021.
 */
class DataSourceSubjectProperty @Inject constructor(private val serverApi: ServerApi) {

    /*suspend fun getPropertyTypes(token: String): Response<List<PropertyType>> {
        return try {
            val newToken = "Bearer $token"
            val response = serverApi.getPropertyTypes(newToken)
            //Result.Success(response)



        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                //Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                //Result.Error(IOException("Error notification -", e))
        }
    } */


    suspend fun getPropertyTypes(token: String): Response<List<PropertyType>> = serverApi.getPropertyTypes(token)

}