package com.rnsoft.colabademo

import android.util.Log
import android.util.Patterns

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class SignUpFlowViewModel @Inject constructor(private val signUpFlowRepository: SignUpFlowRepository) : ViewModel() {

    private val _loginResult = MutableLiveData<LoginResponseResult>()
    val loginResponseResult: LiveData<LoginResponseResult> = _loginResult

}