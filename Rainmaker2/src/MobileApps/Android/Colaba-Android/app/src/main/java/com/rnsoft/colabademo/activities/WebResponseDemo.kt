package com.rnsoft.colabademo.activities

import com.google.gson.annotations.SerializedName
import java.io.Serializable

class WebResponseDemo<T> : Serializable {

    @SerializedName("message")
    internal var message: String? = null


    @SerializedName("status")
    internal var status : String? = null


    @SerializedName("data")
    var result: T? = null


    @SerializedName("code")
    internal var code: String? = null




}