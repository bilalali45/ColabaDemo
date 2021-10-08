package com.rnsoft.colabademo

import java.util.regex.Pattern

object LoginUtil {

    val EMAIL_ADDRESS_PATTERN: Pattern = Pattern.compile(
        "[a-zA-Z0-9\\+\\.\\_\\%\\-\\+]{1,256}" +
                "\\@" +
                "[a-zA-Z0-9][a-zA-Z0-9\\-]{0,64}" +
                "(" +
                "\\." +
                "[a-zA-Z0-9][a-zA-Z0-9\\-]{0,25}" +
                ")+"
    )

    fun isValidEmail(userEmail: String): String? {
        return if (userEmail.isEmpty())
            "Empty Email, Please try again…"
        else if (!EMAIL_ADDRESS_PATTERN.matcher(userEmail).matches())
            "Invalid Email, Please try again…"
        else
            null
    }

    fun isValidEmailAddress(userEmail: String): Boolean {
        return !EMAIL_ADDRESS_PATTERN.matcher(userEmail).matches()
    }


    fun checkPasswordLength(password: String): String? {
        if (password.isEmpty())
            return "Password Empty, Please try again…"
        else
        if(password.isBlank())
            return "Password Empty, Please try again…"
        else
        if(password.isNotEmpty()){
            for (element in password) {
                if (Character.isWhitespace(element)) {
                    return "Password can not contain space…"
                }
            }
        }

        return null
    }

}