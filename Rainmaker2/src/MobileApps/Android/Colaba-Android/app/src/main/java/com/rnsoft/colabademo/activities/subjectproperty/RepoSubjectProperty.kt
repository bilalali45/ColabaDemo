package com.rnsoft.colabademo.activities.subjectproperty

import android.content.Context
import android.content.SharedPreferences
import com.rnsoft.colabademo.BorrowerOverviewModel
import com.rnsoft.colabademo.DetailDataSource
import com.rnsoft.colabademo.Result
import com.rnsoft.colabademo.activities.model.PropertyType
import dagger.hilt.android.qualifiers.ApplicationContext
import retrofit2.Response
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/11/2021.
 */
class RepoSubjectProperty @Inject constructor(private val dataSource: DataSourceSubjectProperty) {

    suspend fun getPropertyType(token:String): Response<List<PropertyType>> {
        return dataSource.getPropertyTypes(token = token)
    }
}