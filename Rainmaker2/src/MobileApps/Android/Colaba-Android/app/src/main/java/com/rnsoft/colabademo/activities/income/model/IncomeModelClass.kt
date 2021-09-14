package com.rnsoft.colabademo



data class IncomeModelClass(
    val headerTitle: String = "Header Title",
    val headerAmount: String = "$0",
    val footerTitle: String  = "Footer Title",
    val incomeContentCell:ArrayList<IncomeContentCell>)

data class IncomeContentCell(val title:String="Title", val description:String="detail", val contentAmount:String="0$" , val tenure:String= "")


