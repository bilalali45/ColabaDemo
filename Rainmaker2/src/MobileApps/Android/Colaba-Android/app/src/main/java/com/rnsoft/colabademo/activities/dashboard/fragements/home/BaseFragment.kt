package com.rnsoft.colabademo.activities.dashboard.fragements.home

import androidx.fragment.app.Fragment

 open class BaseFragment: Fragment() {
    protected var globalAssignToMe: Boolean = false
    open fun setOrderId(orderBy: Int) {}
    open fun setAssignToMe(assignToMe:Boolean) {}
 }