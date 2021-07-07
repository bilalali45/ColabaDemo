package com.rnsoft.colabademo.activities.dashboard.fragements.home

import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import com.rnsoft.colabademo.LoanViewModel

open class BaseFragment: Fragment() {

    protected var globalAssignToMe: Boolean = false
    open fun setOrderId(orderBy: Int) {}
    open fun setAssignToMe(assignToMe:Boolean) {}

    protected val loanViewModel: LoanViewModel by activityViewModels()

    protected fun loadDataFromDatabase(loanFilter:Int){

        loanViewModel.getLoanDataFromDatabase(loanFilter = loanFilter)

    }

}