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

   suspend fun validateLoginCredentials(
        userEmail: String,
        password: String,
        enableBiometric:Boolean,
        dontAskTwoFaIdentifier:String=""

    ): Result<LoginResponse> {
        if(enableBiometric)
            sharedPref.putBoolean(ColabaConstant.isbiometricEnabled, true).apply()

        val genericResult = dataSource.login(userEmail, password , dontAskTwoFaIdentifier )
        if (genericResult is Result.Success) {
            genericResult.data.data?.let { storeLoggedInUserInfo(it) }
        }
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

            settingData.twoFaResendCoolTimeInMinutes?.let {
                sharedPref.putInt(ColabaConstant.twoFaResendCoolTimeInMinutes, it).apply()
            }
        }
    }


    private fun storeLoggedInUserInfo(data: Data) {

        sharedPref.putString(ColabaConstant.token, data.token).apply()

        sharedPref.putString(ColabaConstant.refreshToken, data.refreshToken)
            .apply()
        sharedPref.putInt(ColabaConstant.userProfileId, data.userProfileId)
            .apply()
        sharedPref.putString(ColabaConstant.userName, data.userName).apply()
        sharedPref.putString(ColabaConstant.validFrom, data.validFrom).apply()
        sharedPref.putString(ColabaConstant.validTo, data.validTo).apply()
        sharedPref.putInt(ColabaConstant.tokenType, data.tokenType).apply()
        sharedPref.putString(ColabaConstant.tokenTypeName, data.tokenTypeName)
            .apply()
        sharedPref.putString(
            ColabaConstant.refreshTokenValidTo,
            data.refreshTokenValidTo
        ).apply()

        if(data.tokenTypeName == ColabaConstant.AccessToken)
            sharedPref.putBoolean(ColabaConstant.IS_LOGGED_IN, true).apply() // mark user as logged in completely...
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