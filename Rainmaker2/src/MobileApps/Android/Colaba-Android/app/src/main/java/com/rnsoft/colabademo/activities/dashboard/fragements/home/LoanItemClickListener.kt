package com.rnsoft.colabademo.activities.dashboard.fragements.home

import android.view.View
import androidx.constraintlayout.widget.ConstraintLayout

interface LoanItemClickListener {
    //fun onItemClick(testLayout: ConstraintLayout)
    fun getCardIndex(position: Int)
}

