package com.rnsoft.colabademo.activities.dashboard.fragements.home.loans

import android.view.View
import androidx.constraintlayout.widget.ConstraintLayout

interface LoanItemClickListener {
    //fun onItemClick(testLayout: ConstraintLayout)
    fun getCardIndex(position: Int)
}

