package com.rnsoft.colabademo

import android.content.Context
import android.content.Intent
import androidx.lifecycle.Lifecycle
import androidx.lifecycle.OnLifecycleEvent
import androidx.lifecycle.LifecycleObserver

class AppLifecycleObserver(val appContext: Context) : LifecycleObserver {
        @OnLifecycleEvent(Lifecycle.Event.ON_START)
        fun onEnterForeground() {
            //run the code we need
            if(AppSetting.userHasLoggedIn) {
                val intent = Intent(appContext, SignUpFlowActivity::class.java)
                intent.flags = Intent.FLAG_ACTIVITY_NEW_TASK
                appContext.startActivity(intent)
            }
        }

        @OnLifecycleEvent(Lifecycle.Event.ON_STOP)
        fun onEnterBackground() {
            //run the code we need

        }

        companion object {
            val TAG = AppLifecycleObserver::class.java.name
        }
    }
