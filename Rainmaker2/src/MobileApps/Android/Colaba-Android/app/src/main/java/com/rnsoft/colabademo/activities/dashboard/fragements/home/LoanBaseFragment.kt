package com.rnsoft.colabademo

import androidx.fragment.app.activityViewModels

open class LoanBaseFragment: BaseFragment() {

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