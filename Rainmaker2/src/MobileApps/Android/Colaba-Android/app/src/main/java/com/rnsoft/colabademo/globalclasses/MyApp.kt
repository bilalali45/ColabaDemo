package com.rnsoft.colabademo

import android.app.Application
import dagger.hilt.android.HiltAndroidApp

@HiltAndroidApp
open class MyApp : Application()
{
    //companion object{
        //lateinit var appComponent: AppComponent
    //}
    override fun onCreate() {
        super.onCreate()
        //appComponent = DaggerAppComponent.builder().appModule(AppModule(this)).build()
    }
}