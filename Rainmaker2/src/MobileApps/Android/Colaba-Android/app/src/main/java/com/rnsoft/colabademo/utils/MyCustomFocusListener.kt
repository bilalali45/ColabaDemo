package com.rnsoft.colabademo

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
            setTextInputLayoutHintColor(mTextInputLayout, context = context, R.color.grey_color_two )
        }
        else{
            Log.e("onFocus--","LOST")
            if (mEditText.text?.length == 0) {
                setTextInputLayoutHintColor(mTextInputLayout, context = context, R.color.grey_color_three )
            } else {
                setTextInputLayoutHintColor(mTextInputLayout, context = context, R.color.grey_color_two )
            }
        }
    }

    private fun setTextInputLayoutHintColor(textInputLayout: TextInputLayout, context: Context, @ColorRes colorIdRes: Int) {
        textInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(context, colorIdRes))
    }
}