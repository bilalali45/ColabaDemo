package com.rnsoft.colabademo.activities.dashboard.fragements.search

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.rnsoft.colabademo.databinding.DashboardLayoutBinding
import org.greenrobot.eventbus.EventBus

class SearchActivity: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)





    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

}