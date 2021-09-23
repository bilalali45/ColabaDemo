package com.rnsoft.colabademo.activities.dashboard.fragements.home

import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import com.rnsoft.colabademo.LoanViewModel

open class LoanBaseFragment: Fragment() {

    companion object {
        var globalAssignToMe: Boolean = false
        var globalOrderBy:Int = 1
    }

    open fun setOrderId(orderBy: Int) {}
    open fun setAssignToMe(assignToMe:Boolean) {}

    protected val loanViewModel: LoanViewModel by activityViewModels()

    protected fun loadDataFromDatabase(loanFilter:Int){
        loanViewModel.getLoanDataFromDatabase(loanFilter = loanFilter)
    }




}