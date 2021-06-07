package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject

class LoginRepo @Inject
constructor(
    private val dataSource: LoginDataSource, private val sharedPref: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
) {

    private var user: LoginResponse? = null
    val isLoggedIn: Boolean
        get() = user != null

    init {
        user = null
    }

    suspend fun validateLoginCredentials(
        userEmail: String,
        password: String
    ): Result<LoginResponse> {
        val genericResult = dataSource.login(userEmail, password)
        if (genericResult is Result.Success)
            storeLoggedInUserInfo(genericResult.data)
        return genericResult
    }

    suspend fun fetchTenantConfiguration(authString: String): Result<TenantConfigurationResponse> {
        val genericResult = dataSource.tenantConfigurationSource(authString)
        if (genericResult is Result.Success)
            storeTenantInfo(genericResult.data)
        return genericResult
    }

    suspend fun getPhoneNumberDetail(authString: String): Result<SendTwoFaResponse> {
        val genericResult = dataSource.getPhoneDetail(authString)
        if (genericResult is Result.Success)
            storePhoneInfo(genericResult.data)
        return genericResult
    }

    private fun storeLoggedInUserInfo(loginResponse: LoginResponse) {
        // If user credentials will be cached in local storage, it is recommended it be encrypted
        // @see https://developer.android.com/training/articles/keystore
        this.user = loginResponse
        sharedPref.putBoolean(ColabaConstant.IS_LOGGED_IN, true).apply()

        if (loginResponse.data != null) {
                sharedPref.putString(ColabaConstant.token, loginResponse.data.token).apply()

            sharedPref.putString(ColabaConstant.refreshToken, loginResponse.data.refreshToken)
                .apply()
            sharedPref.putInt(ColabaConstant.userProfileId, loginResponse.data.userProfileId)
                .apply()
            sharedPref.putString(ColabaConstant.userName, loginResponse.data.userName).apply()
            sharedPref.putString(ColabaConstant.validFrom, loginResponse.data.validFrom).apply()
            sharedPref.putString(ColabaConstant.validTo, loginResponse.data.validTo).apply()
            sharedPref.putInt(ColabaConstant.tokenType, loginResponse.data.tokenType).apply()
            sharedPref.putString(ColabaConstant.tokenTypeName, loginResponse.data.tokenTypeName)
                .apply()
            sharedPref.putString(
                ColabaConstant.refreshTokenValidTo,
                loginResponse.data.refreshTokenValidTo
            ).apply()
        }




    }

    private fun storeTenantInfo(tenantConfigurationResponse: TenantConfigurationResponse) {

       ColabaConstant.userTwoFaSetting = tenantConfigurationResponse.tenantData.userTwoFaSetting
        sharedPref.putInt(
            ColabaConstant.tenantTwoFaSetting,
            tenantConfigurationResponse.tenantData.tenantTwoFaSetting
        ).apply()
    }

    private fun storePhoneInfo(sendTwoFaResponse: SendTwoFaResponse) {


    }

    suspend fun logout() {
        user = null
        sharedPref.clear()
        sharedPref.putBoolean(ColabaConstant.IS_LOGGED_IN, false).apply()
        sharedPref.putString(ColabaConstant.token, "").apply()
        //ProductsDatabase.getDatabase(applicationContext) .clearAllTables()
        //dataSource.logout()
    }

}