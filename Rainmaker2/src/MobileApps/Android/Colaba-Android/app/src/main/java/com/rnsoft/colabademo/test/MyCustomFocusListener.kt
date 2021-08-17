package com.rnsoft.colabademo.test

import android.content.Context
import android.content.res.ColorStateList
import android.util.Log
import android.view.View
import androidx.annotation.ColorRes
import androidx.core.content.ContextCompat
import com.google.android.material.textfield.TextInputEditText
import com.google.android.material.textfield.TextInputLayout
import com.rnsoft.colabademo.R

class MyCustomFocusListener(private val mEditText: TextInputEditText, private val mTextInputLayout: TextInputLayout , private val context: Context):View.OnFocusChangeListener {
    override fun onFocusChange(p0: View?, p1: Boolean) {
        if(p1) {
            Log.e("onFocus--","when on focus")

            //mTextInputLayout.error = "asdasd"
            //mTextInputLayout.helperText = "ADsfdasf"
            //mTextInputLayout.setHelperTextColor( ColorStateList.valueOf(ContextCompat.getColor(context, R.color.green_circle)))
            //mTextInputLayout.boxStrokeColor = ContextCompat.getColor(context, R.color.green_circle)

           // mTextInputLayout.setErrorTextAppearance(R.style.error_appearance)
            setTextInputLayoutHintColor(mTextInputLayout, context = context, R.color.grey_color_two )
           //mEditText.error = "ASdfadsf"
        }
        else{
            mTextInputLayout.error = null
            //mTextInputLayout.helperText = null
            Log.e("onFocus--","LOST")
            if (mEditText.text?.length == 0) {
                setTextInputLayoutHintColor(mTextInputLayout, context = context, R.color.grey_color_four )
            } else {
                setTextInputLayoutHintColor(mTextInputLayout, context = context, R.color.grey_color_two )
            }
        }
    }

    private fun setTextInputLayoutHintColor(textInputLayout: TextInputLayout, context: Context, @ColorRes colorIdRes: Int) {
        textInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(context, colorIdRes))
    }
}