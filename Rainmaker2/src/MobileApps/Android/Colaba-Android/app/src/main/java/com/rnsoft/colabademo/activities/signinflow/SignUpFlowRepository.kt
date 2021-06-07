package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject


class SignUpFlowRepository @Inject
constructor(
    private val dataSource: SignUpFlowDataSource, private val spEditor: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
)
{

    /*
    private var user: LoginResponse? = null
    val isLoggedIn: Boolean
        get() = user != null


    init {
        //MyApp.appComponent.inject(this)
        user = null
    }

    suspend fun fetchUserList(): Result<UserListResult> {
        return dataSource.fetchUserList()
    }

    suspend fun login(username: String, password: String): Result<LoginResponse> {
        val result = dataSource.login(username, password)
        if (result is Result.Success)
            setLoggedInUser(result.data)
        return result
    }

    private fun setLoggedInUser(loginResponse: LoginResponse) {
        this.user = loginResponse
        spEditor.putBoolean(MyAppConfigConstant.IS_LOGGED_IN, true).apply()
        spEditor.putString(MyAppConfigConstant.TOKEN, loginResponse.token).apply()
    }

    suspend fun logout() {
        user = null
        spEditor.clear()
        spEditor.putBoolean(MyAppConfigConstant.IS_LOGGED_IN, false).apply()
        spEditor.putString(MyAppConfigConstant.TOKEN, "").apply()
        //ProductsDatabase.getDatabase(applicationContext) .clearAllTables()
        //dataSource.logout()
    }

     */

}