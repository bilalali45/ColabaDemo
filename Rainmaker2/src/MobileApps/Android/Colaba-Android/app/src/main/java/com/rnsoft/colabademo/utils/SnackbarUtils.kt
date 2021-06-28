package com.rnsoft.colabademo

import android.app.Activity
import android.graphics.Color
import android.view.Gravity
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import android.widget.TextView
import androidx.coordinatorlayout.widget.CoordinatorLayout
import androidx.core.content.ContextCompat
import androidx.core.view.marginBottom
import com.google.android.material.snackbar.BaseTransientBottomBar
import com.google.android.material.snackbar.Snackbar
import com.rnsoft.colabademo.R


object SnackbarUtils {
    fun showRegular(activity: Activity, textMessage: String) {
        show(activity, textMessage)
    }

    fun showSuccess(activity: Activity, textMessage: String) {
        show(activity, textMessage, Color.GREEN)
    }

    fun showError(activity: Activity, textMessage: String) {
        show(activity, textMessage, Color.RED)
    }

    fun showWarning(activity: Activity, textMessage: String) {
        show(activity, textMessage, Color.argb(255, 255, 165, 0))
    }

    private fun show(
        activity: Activity,
        textMessage: String,
        backgroundColor: Int = ContextCompat.getColor(activity, R.color.teal_200)
    ) {
        SimpleCustomSnackbar.make(activity, textMessage, backgroundColor)?.show()
    }
}

class SimpleCustomSnackbar(parent: ViewGroup, content: View) :
    BaseTransientBottomBar<SimpleCustomSnackbar>(
        parent,
        content,
        object : com.google.android.material.snackbar.ContentViewCallback {
            override fun animateContentIn(delay: Int, duration: Int) {}
            override fun animateContentOut(delay: Int, duration: Int) {}
        }) {

    init {
        getView().apply {
            setBackgroundColor(ContextCompat.getColor(view.context, android.R.color.transparent))
            setPadding(0, 0, 0, 0)
        }
    }

    companion object {
        private fun findSuitableParent(view: View?): ViewGroup? {
            if (view == null) {
                return null
            }

            var viewInProcess = view
            val viewGroup = viewInProcess
            var fallback: ViewGroup? = null
            do {
                if (viewInProcess is CoordinatorLayout) {
                    return viewGroup as ViewGroup
                } else if (viewInProcess is FrameLayout) {
                    if (viewGroup.id == R.id.content) {
                        return viewInProcess
                    }
                    fallback = viewGroup as ViewGroup?
                }

                if (viewInProcess != null) {
                    val parent = viewInProcess.parent
                    viewInProcess = if (parent is View) parent as ViewGroup else null
                }
            } while (viewInProcess != null)
            return fallback
        }

        fun make(
            activity: Activity,
            message: String,
            backgroundColor: Int = ContextCompat.getColor(activity, R.color.teal_200),
            duration: Int = Snackbar.LENGTH_LONG
        ): SimpleCustomSnackbar? {
            val parent =
                findSuitableParent(activity.findViewById(android.R.id.content)) ?: return null

            try {
                val toastView: View = LayoutInflater.from(activity)
                    .inflate(R.layout.custom_toast, parent, false)
                (toastView.findViewById<View>(R.id.text) as TextView).text = message
                toastView.setBackgroundColor(backgroundColor)
                val snackbar = SimpleCustomSnackbar(
                    parent,
                    toastView
                ).setDuration(duration)
                setCorrectAnimationAndPosition(snackbar)
                return snackbar
            } catch (e: Exception) {
                println(e.message)
            }
            return null
        }

        private fun setCorrectAnimationAndPosition(snackbar: SimpleCustomSnackbar) {
            val params = snackbar.view.layoutParams
            if (params is CoordinatorLayout.LayoutParams) {
                params.gravity = Gravity.BOTTOM
            } else if (params is FrameLayout.LayoutParams) {
                params.gravity = Gravity.BOTTOM
            }
            snackbar.view.layoutParams = params

            snackbar.animationMode = ANIMATION_MODE_FADE
        }
    }
}