package com.rnsoft.colabademo.activities.details

import com.google.gson.annotations.SerializedName
import java.io.Serializable

class WebResponse<T> : Serializable {

    @SerializedName("message")
    internal var message: String? = null


    @SerializedName("status")
    internal var status : String? = null


    @SerializedName("data")
    internal var data: Boolean? = null


    @SerializedName("code")
    internal var code: String? = null


//
//
//
//    fun getToken(): String? {
//        return token
//    }
//
//    fun setToken(token: String?) {
//        this.token = token
//    }

    fun getMessage(): String? {
        return message
    }

    fun setMessage(message: String?) {
        this.message = message
    }

//    fun getData(): T? {
//        return data
//    }
//
//    fun setData(data: T?) {
//        this.data = data
//    }




}