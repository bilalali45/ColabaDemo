package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class LoginRequest (
    val Email: String = "",
 val Password: String = "")