package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import com.google.gson.Gson
import dagger.hilt.android.qualifiers.ApplicationContext
import javax.inject.Inject

class LoginRepo @Inject
constructor(
    private val dataSource: LoginDataSource, private val spEditor: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
) {

   suspend fun validateLoginCredentials(
        userEmail: String,
        password: String,
        enableBiometric:Boolean,
        dontAskTwoFaIdentifier:String=""

    ): Result<LoginResponse> {
        if(enableBiometric)
            spEditor.putBoolean(ColabaConstant.isbiometricEnabled, true).apply()

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
        spEditor.putInt(ColabaConstant.maxOtpSendAllowed, 5).apply() // Setting default value.........
        val result = dataSource.getOtpSetting(intermediateToken)
        if (result is Result.Success)
            storeOtpSetting(result.data)
        return result
    }

    private fun storeOtpSetting(otpSettingResponse: OtpSettingResponse){
        otpSettingResponse.otpSettingData?.let { settingData ->
            settingData.maxTwoFaSendAllowed?.let {
                spEditor.putInt(ColabaConstant.maxOtpSendAllowed, it).apply()
            }

            settingData.twoFaResendCoolTimeInMinutes?.let {
                spEditor.putInt(ColabaConstant.twoFaResendCoolTimeInMinutes, it).apply()
            }
        }
    }


    private fun storeLoggedInUserInfo(data: Data) {

        spEditor.putString(ColabaConstant.token, data.token).apply()

        spEditor.putString(ColabaConstant.refreshToken, data.refreshToken)
            .apply()
        spEditor.putInt(ColabaConstant.userProfileId, data.userProfileId)
            .apply()
        spEditor.putString(ColabaConstant.userName, data.userName).apply()
        spEditor.putString(ColabaConstant.validFrom, data.validFrom).apply()
        spEditor.putString(ColabaConstant.validTo, data.validTo).apply()
        spEditor.putInt(ColabaConstant.tokenType, data.tokenType).apply()
        spEditor.putString(ColabaConstant.tokenTypeName, data.tokenTypeName)
            .apply()
        spEditor.putString(
            ColabaConstant.refreshTokenValidTo,
            data.refreshTokenValidTo
        ).apply()

        if(data.tokenTypeName == ColabaConstant.AccessToken)
            spEditor.putBoolean(ColabaConstant.IS_LOGGED_IN, true).apply() // mark user as logged in completely...
    }

    private fun storeTenantInfo(tenantConfigurationResponse: TenantConfigurationResponse) {

       ColabaConstant.userTwoFaSetting = tenantConfigurationResponse.tenantData.userTwoFaSetting
        spEditor.putInt(
            ColabaConstant.tenantTwoFaSetting,
            tenantConfigurationResponse.tenantData.tenantTwoFaSetting
        ).apply()
    }

    private fun storePhoneInfo(sendTwoFaResponse: SendTwoFaResponse) {

        sendTwoFaResponse.twoFaData?.let { twoFaData->
            twoFaData.phoneNumber?.let {
                spEditor.putString(ColabaConstant.phoneNumber, it)
                    .apply()
            }
        }

        sendTwoFaResponse.message?.let {
            spEditor.putString(ColabaConstant.otp_message, sendTwoFaResponse.message)
                .apply()
        }


        sendTwoFaResponse.twoFaData?.let {
            val gson = Gson()
            val otpData = gson.toJson(sendTwoFaResponse.twoFaData)
            spEditor.putString(ColabaConstant.otpDataJson, otpData).apply()
            spEditor.putInt(ColabaConstant.secondsCount, 0).apply()
        }

    }


}