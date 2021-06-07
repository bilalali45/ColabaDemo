package com.rnsoft.colabademo

data class LoginResponse(
    val code: String?,
    val data: Data?,
    val message: Any?,
    val status: String?
)

data class Data(
    val isLoggedIn: Boolean,
    val refreshToken: String,
    val refreshTokenValidTo: String,
    val token: String,
    val userName: String,
    val userProfileId: Int,
    val validFrom: String,
    val validTo: String,
    val tokenType: Int,
    val tokenTypeName: String
)