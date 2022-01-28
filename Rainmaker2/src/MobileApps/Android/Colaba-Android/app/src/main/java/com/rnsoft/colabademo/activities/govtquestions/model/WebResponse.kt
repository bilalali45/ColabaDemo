package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName
import java.io.Serializable


/**
 * Created by Anita Kiran on 1/27/2022.
 */
class WebResponse<T> : Serializable {

    @SerializedName("message")
    internal var message: String? = null
    @SerializedName("status")
    internal var status : String? = null
    @SerializedName("data")
    internal var data: Boolean? = null
    @SerializedName("code")
    internal var code: String? = null
}