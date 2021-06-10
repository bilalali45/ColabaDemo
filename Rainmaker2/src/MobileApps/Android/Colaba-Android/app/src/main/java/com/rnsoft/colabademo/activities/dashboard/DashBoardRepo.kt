package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject

class DashBoardRepo  @Inject constructor(
    private val dataSource: DashBoardDataSource, private val spEditor: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
) {

    suspend fun logoutUser(token: String): Result<LogoutResponse> {
        val logoutResult = dataSource.logoutUser(token = token)
        if(logoutResult is Result.Success)
            clearUserRecords()
        return logoutResult
    }

    private fun clearUserRecords(){
            //user = null
            spEditor.clear().commit()
            //spEditor.clear().apply()

            //spEditor.putString(ColabaConstant.token, "").apply()
            //dataSource.logout()

    }

}