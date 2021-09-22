package com.rnsoft.colabademo

import android.view.ViewGroup
import android.widget.FrameLayout
import android.widget.LinearLayout
import android.widget.RelativeLayout
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.iterator
import androidx.fragment.app.Fragment


import timber.log.Timber

open class AppBaseFragment:Fragment() {

    fun addListeners(rootView: ViewGroup) {
        HideSoftkeyboard.hide(requireContext(),rootView)
        for (item in rootView) {
            //if(item is ConstraintLayout || item is LinearLayoutCompat || item is LinearLayout || item is RelativeLayout || item is FrameLayout)
            if(item is ViewGroup)
                addListeners(item)
            item.clearFocus()
            Timber.e("clearing focus")
        }
        rootView.setOnClickListener{ addListeners(rootView) }
    }

}