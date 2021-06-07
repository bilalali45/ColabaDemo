package com.rnsoft.colabademo

data class LoginResponseResult(
    val success: LoginResponse? = null,
    val screenNumber:Int? = 0,
    val emailError: Int? = null,
    val passwordError: Int? = null,
    val responseError:Int? = null
)