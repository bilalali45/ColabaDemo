package com.rnsoft.colabademo.utils

import android.content.Context
import android.content.res.ColorStateList
import android.text.Spannable
import android.text.SpannableString
import android.text.SpannableStringBuilder
import android.text.style.ForegroundColorSpan
import androidx.core.content.ContextCompat
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.R

class CustomLableColor() {
    companion object {
        fun setColor(mTextInputLayout:TextInputLayout, color:Int, context: Context) {
            mTextInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                ContextCompat.getColor(
                    context, color
                )
            )
        }

        fun setPrefix(mTextInputLayout:TextInputLayout, context: Context){

            val text = SpannableString("$").apply {
                setSpan(
                    ForegroundColorSpan(ContextCompat.getColor(context,
                        R.color.grey_color_two)), 0, length, 0)
            }

            val text2 = SpannableString("  |  ").apply {
                setSpan(
                    ForegroundColorSpan(ContextCompat.getColor(context,
                        R.color.colaba_app_border_color)), 0, length, 0)
            }

            val spannable: Spannable = SpannableStringBuilder().apply {
                append(text)
                append(text2)
            }
            mTextInputLayout.prefixText= spannable

        }
    }

}