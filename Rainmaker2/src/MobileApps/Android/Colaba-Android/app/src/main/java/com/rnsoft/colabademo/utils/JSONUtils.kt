package com.rnsoft.colabademo.utils

import com.google.gson.Gson
import com.google.gson.JsonSyntaxException


class JSONUtils {



    private fun JSONUtils() {}

    companion object {
        private val gson = Gson()

        fun isJSONValid(jsonInString: String?): Boolean {
            return try {
                gson.fromJson(jsonInString, Any::class.java)
                true
            } catch (ex: JsonSyntaxException) {
                false
            }
        }
    }
}