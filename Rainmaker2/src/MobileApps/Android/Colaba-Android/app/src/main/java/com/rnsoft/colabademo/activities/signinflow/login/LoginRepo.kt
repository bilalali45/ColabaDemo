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

    private var isbiometricEnabled = false

    suspend fun validateLoginCredentials(
        userEmail: String,
        password: String,
        enableBiometric:Boolean
    ): Result<LoginResponse> {
        isbiometricEnabled = enableBiometric
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

    suspend fun otpSettingFromService(intermediateToken:String): Result<OtpSettingResponse>{
        sharedPref.putInt(ColabaConstant.maxOtpSendAllowed, 5).apply() // Setting default value.........
        val result = dataSource.getOtpSetting(intermediateToken)
        if (result is Result.Success)
            storeOtpSetting(result.data)
        return result
    }

    private fun storeOtpSetting(otpSettingResponse: OtpSettingResponse){
        otpSettingResponse.otpSettingData?.let { settingData ->
            settingData.maxTwoFaSendAllowed?.let {
                sharedPref.putInt(ColabaConstant.maxOtpSendAllowed, it).apply()
            }
        }
    }


    private fun storeLoggedInUserInfo(loginResponse: LoginResponse) {
        // If user credentials will be cached in local storage, it is recommended it be encrypted
        // @see https://developer.android.com/training/articles/keystore
        this.user = loginResponse


        if(isbiometricEnabled)
            sharedPref.putBoolean(ColabaConstant.isbiometricEnabled, true).apply()

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

            if(loginResponse.data.tokenTypeName == "AccessToken")
                sharedPref.putBoolean(ColabaConstant.IS_LOGGED_IN, true).apply() // mark user as logged in completely...
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


}