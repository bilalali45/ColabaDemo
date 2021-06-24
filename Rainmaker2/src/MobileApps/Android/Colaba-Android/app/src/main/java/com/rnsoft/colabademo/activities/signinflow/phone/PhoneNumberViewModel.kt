package com.rnsoft.colabademo

import android.content.SharedPreferences
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.activities.signinflow.phone.events.SkipEvent
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject


@HiltViewModel
class PhoneNumberViewModel @Inject constructor(private val phoneNumberRepo: PhoneNumberRepo) :
    ViewModel() {


    @Inject
    lateinit var sharedPreferences: SharedPreferences

    fun skipTwoFactor() {
        viewModelScope.launch {
            val genericResult = sharedPreferences.getString(AppConstant.token, "")?.let {
                phoneNumberRepo.skipTwoFactorAuthentication(
                    it
                )
            }
            genericResult?.let {
                if (genericResult is Result.Success) {
                        EventBus.getDefault().post(SkipEvent(genericResult.data))
                } else
                    EventBus.getDefault().post(SkipEvent(SkipTwoFactorResponse("300", null, "Webservice error, can not skip", null)))
            }

        }
    }




}