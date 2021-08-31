package com.rnsoft.colabademo

import android.app.Application
import dagger.hilt.android.HiltAndroidApp
import androidx.lifecycle.ProcessLifecycleOwner
import android.app.Activity
import androidx.appcompat.app.AppCompatActivity


@HiltAndroidApp
open class ApplicationClass : Application()
{

    override fun onCreate() {
        super.onCreate()
        //registerActivityLifecycleCallbacks(AppLifecycleTracker(applicationContext))
        val appLifecycleObserver = AppLifecycleObserver(applicationContext)
        ProcessLifecycleOwner.get().lifecycle.addObserver(appLifecycleObserver)
    }

}