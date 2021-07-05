package com.rnsoft.colabademo

import android.view.View

interface NotificationClickListener {
    fun onItemClick( view: View)
    fun getNotificationIndex(position: Int)
}
