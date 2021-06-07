package com.rnsoft.colabademo

import android.content.SharedPreferences
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject


@HiltViewModel
class PhoneNumberViewModel @Inject constructor(private val phoneNumberRepo: PhoneNumberRepo) :
    ViewModel() {

    private val _skipTwoFactorResponse = MutableLiveData<SkipTwoFactorResponse>()
    val skipTwoFactorResponse: LiveData<SkipTwoFactorResponse> = _skipTwoFactorResponse

    private val _sendOtpToNumberResponse = MutableLiveData<OtpToNumberResponse>()
    val otpToNumberResponse: LiveData<OtpToNumberResponse> = _sendOtpToNumberResponse


    @Inject
    lateinit var sharedPreferences: SharedPreferences

    fun skipTwoFactor() {
        viewModelScope.launch {
            val genericResult = sharedPreferences.getString(ColabaConstant.token, "")?.let {
                phoneNumberRepo.skipTwoFactorAuthentication(
                    it
                )
            }
            genericResult?.let {
                if (genericResult is Result.Success) {
                    if(genericResult.data.code == "200")
                        _skipTwoFactorResponse.value = genericResult.data
                    else
                        _skipTwoFactorResponse.value =
                            SkipTwoFactorResponse("300", null, "Webservice error, can not skip", null)
                }
                else
                    _skipTwoFactorResponse.value =
                        SkipTwoFactorResponse("300", null, "Webservice error, can not skip", null)
            }

        }
    }


    fun sendOtpToPhone(phoneNumber:String) {

        var correctPhoneNumber = phoneNumber.replace(" ", "")
        correctPhoneNumber = phoneNumber.replace("-", "")
        correctPhoneNumber = phoneNumber.replace("(", "")
        correctPhoneNumber = phoneNumber.replace(")", "")

        viewModelScope.launch {
            val genericResult = sharedPreferences.getString(ColabaConstant.token, "")?.let {
                phoneNumberRepo.sendOtpToPhone(
                    it , correctPhoneNumber
                )
            }
            genericResult?.let {
                if (genericResult is Result.Success) {
                    if(genericResult.data.code == "200")
                        _sendOtpToNumberResponse.value = genericResult.data
                    else
                        _sendOtpToNumberResponse.value =
                            OtpToNumberResponse("300", null, "Webservice error, can not skip", null)
                }
                else
                    _sendOtpToNumberResponse.value =
                        OtpToNumberResponse("300", null, "Webservice error, can not skip", null)
            }

        }
    }

}