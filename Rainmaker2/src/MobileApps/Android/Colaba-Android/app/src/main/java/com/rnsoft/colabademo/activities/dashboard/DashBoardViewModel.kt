package com.rnsoft.colabademo

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

@HiltViewModel
class DashBoardViewModel @Inject constructor(private val dashBoardRepo : DashBoardRepo) :
    ViewModel() {

    fun logoutUser(token:String) {
        viewModelScope.launch {
            val result = dashBoardRepo.logoutUser(token)
            if (result is Result.Success)
                EventBus.getDefault().post(LogoutEvent(result.data))
            else
                EventBus.getDefault().post(LogoutEvent(LogoutResponse("300", null, "Webservice error, can not logout...", null)))
        }
    }
}