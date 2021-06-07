package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class LoginRequest (
    @SerializedName("Email") var Email: String = "",
@SerializedName("Password")    var Password: String = "")