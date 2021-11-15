package com.rnsoft.colabademo.utils

import java.text.DecimalFormat

/**
 * Created by Anita Kiran on 10/28/2021.
 */
class Common {
    companion object{

         fun addNumberFormat(value: Double) : String {
            val convertedString: String = DecimalFormat("#,###,###").format(value)
            return convertedString
        }

        fun removeCommas(value:String):String{
            return value.replace(",","")
        }

    }
}