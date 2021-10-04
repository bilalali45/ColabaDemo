package com.rnsoft.colabademo

import android.view.View

data class IncomeModelClass(
    val headerTitle: String = "Header Title",
    val headerAmount: String = "$0",
    val footerTitle: String  = "Footer Title",
    val incomeContentCell:ArrayList<IncomeContentCell>,
    val listenerAttached: View.OnClickListener= View.OnClickListener {  }
    )

data class IncomeContentCell(val title:String="Title", val description:String="detail", val contentAmount:String="0$" , val tenure:String= "",
                             val contentListenerAttached: View.OnClickListener?= null)


