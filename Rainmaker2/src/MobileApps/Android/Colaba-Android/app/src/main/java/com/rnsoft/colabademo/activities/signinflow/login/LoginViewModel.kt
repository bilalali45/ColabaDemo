package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.util.Log
import android.util.Patterns
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject


enum class TOKEN_TYPES() {
    ACCESS_TOKEN ,
    IntermediateToken
}

@HiltViewModel
class LoginViewModel @Inject constructor(private val loginRepo: LoginRepo) :
    ViewModel() {

    //private val _loginResult = MutableStateFlow(LoginResponseResult())
    //val loginResponseResult: SharedFlow<LoginResponseResult> = _loginResult

    //private val _loginResult = MutableFl<LoginResponseResult>()
    //val loginResponseResult: Flow<LoginResponseResult> = Flow()

    @Inject
    lateinit var sharedPreferences: SharedPreferences



    fun login(userEmail: String, password: String , isBiometricActive:Boolean) {

        var dontAskTwoFaIdentifier = ""

        val emailError = isValidEmail(userEmail)
        val passwordLengthError = checkPasswordLength(password)
        if (emailError != null)
            EventBus.getDefault().post(LoginEvent(LoginResponseResult(emailError = emailError)))
        else if (passwordLengthError != null)
            EventBus.getDefault()
                .post(LoginEvent(LoginResponseResult(passwordError = passwordLengthError)))
        else {
            viewModelScope.launch {

                sharedPreferences.getString(ColabaConstant.dontAskTwoFaIdentifier ,"")?.let {
                    dontAskTwoFaIdentifier = it
                }

                val genericResult =
                    loginRepo.validateLoginCredentials(userEmail, password, isBiometricActive, dontAskTwoFaIdentifier)
                Log.e("login-result - ", genericResult.toString())

                if (genericResult is Result.Success) {
                    val loginResponse = genericResult.data

                    if (loginResponse.data?.tokenTypeName == ColabaConstant.AccessToken) {
                        EventBus.getDefault().post(LoginEvent(LoginResponseResult(success = loginResponse, screenNumber = 1)))
                        return@launch
                    } else if (loginResponse.data?.tokenTypeName == ColabaConstant.IntermediateToken) {
                        runOtpSettingService(loginResponse.data.token)
                        //loginRepo.getOtpSettingFromService(loginResponse.data.token)

                        val resultConfiguration =
                            loginRepo.fetchTenantConfiguration(loginResponse.data.token)
                        if (resultConfiguration is Result.Success) {
                            val tenantConfiguration = resultConfiguration.data
                            if (tenantConfiguration.tenantData.tenantTwoFaSetting == 1 ||
                                tenantConfiguration.tenantData.tenantTwoFaSetting == 3
                            ) {
                                val phoneInfoResult =
                                    loginRepo.getPhoneNumberDetail(loginResponse.data.token)
                                if (phoneInfoResult is Result.Success) {
                                    val phoneDetail = phoneInfoResult.data
                                    when (phoneDetail.code) {
                                        "404" -> EventBus.getDefault().post(LoginEvent(LoginResponseResult(success = loginResponse, screenNumber = 2)))
                                        "200" -> EventBus.getDefault().post(LoginEvent(LoginResponseResult(success = loginResponse, screenNumber = 3)))
                                        "400" -> EventBus.getDefault().post(LoginEvent(LoginResponseResult(success = loginResponse, screenNumber = 3)))
                                        else ->
                                            Log.e("Else", "WebService-error-go")
                                    }

                                }
                            }

                        }
                    }
                } else
                    EventBus.getDefault()
                        .post(LoginEvent(LoginResponseResult(responseError = R.string.user_data_does_not_exit)))

            }
        }
    }

    private suspend fun runOtpSettingService(intermediateToken:String){
        loginRepo.otpSettingFromService(intermediateToken)
    }

    private fun isValidEmail(userEmail: String): Int? {
        if (userEmail.isNotBlank()) {
            if (!Patterns.EMAIL_ADDRESS.matcher(userEmail).matches())
                return R.string.email_format_error
        } else
            return R.string.email_empty_error
        return null
    }


    private fun checkPasswordLength(password: String): Int? {
        if (password.isEmpty())
            return R.string.password_empty_error
        return null
    }
}

/*
    @ExperimentalCoroutinesApi
    fun newLoginFlow(userEmail: String, password: String) = callbackFlow {
        val genericResult =
            loginRepo.validateLoginCredentials(userEmail, password)
        if (genericResult is Result.Success) {
            val loginResponse = genericResult.data

            if (loginResponse.data?.tokenTypeName == "AccessToken")
                offer(LoginResponseResult(success = loginResponse, screenNumber = 1))
            else if (loginResponse.data?.tokenTypeName == "IntermediateToken") {
                val resultConfiguration =
                    loginRepo.fetchTenantConfiguration(loginResponse.data.token)
                if (resultConfiguration is Result.Success) {
                    val tenantConfiguration = resultConfiguration.data
                    if (tenantConfiguration.tenantData.tenantTwoFaSetting == 1 ||
                        tenantConfiguration.tenantData.tenantTwoFaSetting == 3
                    ) {
                        val phoneInfoResult =
                            loginRepo.getPhoneNumberDetail(loginResponse.data.token)
                        if (phoneInfoResult is Result.Success) {
                            val phoneDetail = phoneInfoResult.data
                            when (phoneDetail.code) {
                                "404" -> offer(
                                    LoginResponseResult(
                                        success = loginResponse,
                                        screenNumber = 2
                                    )
                                )
                                "200" -> offer(
                                    LoginResponseResult(
                                        success = loginResponse,
                                        screenNumber = 3
                                    )
                                )
                                "400" -> offer(
                                    LoginResponseResult(
                                        success = loginResponse,
                                        screenNumber = 3
                                    )
                                )
                                else -> Log.e("Else", "where-to-go")
                            }

                        } else
                            offer(LoginResponseResult(responseError = R.string.login_failed))
                    }

                } else
                    offer(LoginResponseResult(responseError = R.string.login_failed))
            }
        } else
            offer(LoginResponseResult(responseError = R.string.login_failed))
        //close()
        //awaitClose()
    }
 */