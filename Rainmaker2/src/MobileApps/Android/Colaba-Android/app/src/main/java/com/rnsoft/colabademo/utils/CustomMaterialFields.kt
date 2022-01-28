package com.rnsoft.colabademo.utils

import android.content.Context
import android.content.res.ColorStateList
import android.graphics.Typeface
import android.text.Spannable
import android.text.SpannableString
import android.text.SpannableStringBuilder
import android.text.style.ForegroundColorSpan
import android.text.style.StyleSpan
import android.widget.LinearLayout
import androidx.appcompat.content.res.AppCompatResources
import androidx.appcompat.widget.AppCompatCheckBox
import androidx.appcompat.widget.AppCompatRadioButton
import androidx.core.content.ContextCompat
import androidx.core.view.setPadding
import androidx.core.view.updatePadding
import androidx.core.widget.doAfterTextChanged
import com.google.android.material.textfield.TextInputEditText
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.R

class CustomMaterialFields() {
    companion object {

        fun setColor(mTextInputLayout:TextInputLayout, color:Int, context: Context) {
            mTextInputLayout.defaultHintTextColor = ColorStateList.valueOf(
                ContextCompat.getColor(
                    context, color
                )
            )
        }

        fun setDollarPrefix(mTextInputLayout:TextInputLayout, context: Context){

            val text = SpannableString("$").apply {
                setSpan(ForegroundColorSpan(ContextCompat.getColor(context, R.color.grey_color_two)), 0, length, 0) }

            val text2 = SpannableString("  |  ").apply {
                setSpan(ForegroundColorSpan(ContextCompat.getColor(context, R.color.colaba_app_border_color)), 0, length, 0) }

            val spannable: Spannable = SpannableStringBuilder().apply {
                append(text, StyleSpan(Typeface.BOLD), Spannable.SPAN_EXCLUSIVE_EXCLUSIVE)
                append(text2, StyleSpan(Typeface.BOLD), Spannable.SPAN_EXCLUSIVE_EXCLUSIVE)
            }
            mTextInputLayout.prefixText= spannable
        }

        fun setPercentagePrefix(mTextInputLayout:TextInputLayout, context: Context){

            val text = SpannableString("%").apply {
                setSpan( ForegroundColorSpan(ContextCompat.getColor(context, R.color.grey_color_two)), 0, length, 0)
            }

            val text2 = SpannableString("  |  ").apply {
                setSpan(ForegroundColorSpan(ContextCompat.getColor(context, R.color.colaba_app_border_color)), 0, length, 0)
            }

            val spannable: Spannable = SpannableStringBuilder().apply {
                append(text ,StyleSpan(Typeface.BOLD), Spannable.SPAN_EXCLUSIVE_EXCLUSIVE)
                append(text2,StyleSpan(Typeface.BOLD), Spannable.SPAN_EXCLUSIVE_EXCLUSIVE )
            }
            mTextInputLayout.prefixText= spannable
        }


        fun setError(textInputlayout: TextInputLayout, errorMsg: String, context: Context) {
            textInputlayout.helperText = errorMsg
            textInputlayout.setBoxStrokeColorStateList(
                AppCompatResources.getColorStateList(context, R.color.primary_info_stroke_error_color))
        }

        fun clearError(textInputlayout: TextInputLayout, context: Context) {
            textInputlayout.helperText = ""
            textInputlayout.setBoxStrokeColorStateList(AppCompatResources.getColorStateList(
                    context,
                    R.color.primary_info_line_color))

        }

        fun onTextChangedLableColor(context: Context,mEditText: TextInputEditText, textInputLayout: TextInputLayout){
            mEditText.doAfterTextChanged {
                if (mEditText.text.toString().isNotEmpty()) {
                    textInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(context, R.color.grey_color_two))
                }
            }
        }

        fun setRadioColor(radio : AppCompatRadioButton, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
        }


        fun selectBoxWithShadow(radio : AppCompatRadioButton, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
            radio.setBackgroundResource(R.drawable.radio_background_with_shadow)
            //radio.setPadding(30,55,30,55)
            radio.updatePadding(30,57,30,57)
        }

        fun unselectBoxShadow(radio : AppCompatRadioButton, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_two))
            radio.setBackgroundResource(R.drawable.radio_background_simple)
            radio.updatePadding(30,50,30,50)
        }

        fun selectLayoutWithShadow(layout : LinearLayout, radio: AppCompatRadioButton, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
            layout.setBackgroundResource(R.drawable.radio_background_with_shadow)
        }

        fun unselectLayoutShadow(layout : LinearLayout, radio: AppCompatRadioButton, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_two))
            layout.setBackgroundResource(R.drawable.radio_background_simple)
            radio.updatePadding(30,2,30,2)
        }

        fun radioUnSelectColor(radio : AppCompatRadioButton, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_two))
        }


        // check boxes
        fun selectCheckBoxShadow(radio : AppCompatCheckBox, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
            radio.setBackgroundResource(R.drawable.radio_background_with_shadow)
            //radio.setPadding(30,55,30,55)
            radio.updatePadding(30,57,30,57)
        }

        fun unselectCheckBoxShadow(radio : AppCompatCheckBox, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_two))
            radio.setBackgroundResource(R.drawable.radio_background_simple)
            radio.updatePadding(30,50,30,50)
        }

        fun selectCheckBoxLayout(layout : LinearLayout, radio: AppCompatCheckBox, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
            layout.setBackgroundResource(R.drawable.radio_background_with_shadow)
        }

        fun unselectCheckBoxLayout(layout : LinearLayout, radio: AppCompatCheckBox, context: Context){
            radio.setTextColor(ContextCompat.getColor(context,R.color.grey_color_two))
            layout.setBackgroundResource(R.drawable.radio_background_simple)
            radio.updatePadding(28,2,30,2)
        }


        fun setCheckBoxTextColor(checkbox : AppCompatCheckBox, context: Context){
            checkbox.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
        }

        fun clearCheckBoxTextColor(checkbox : AppCompatCheckBox, context: Context){
            checkbox.setTextColor(ContextCompat.getColor(context,R.color.grey_color_two))
        }
    }
}