package com.rnsoft.colabademo



data class AssetsModelClass(
    val headerTitle: String = "Header Title",
    val headerAmount: String = "$0",
    val footerTitle: String  = "Footer Title",
    val contentCell:ArrayList<ContentCell>)

data class ContentCell(val title:String="Title", val description:String="detail", val contentAmount:String="0$" , val tenure:String= "")


