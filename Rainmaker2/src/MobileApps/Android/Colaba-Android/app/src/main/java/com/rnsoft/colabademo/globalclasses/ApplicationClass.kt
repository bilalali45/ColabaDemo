package com.rnsoft.colabademo

import android.app.Application
import dagger.hilt.android.HiltAndroidApp

@HiltAndroidApp
open class ApplicationClass : Application()
{
    override fun onCreate() {
        super.onCreate()
        //registerActivityLifecycleCallbacks(AppLifecycleTracker())
    }
}