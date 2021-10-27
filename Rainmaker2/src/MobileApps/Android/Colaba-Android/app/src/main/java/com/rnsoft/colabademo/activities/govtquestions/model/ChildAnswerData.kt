package com.rnsoft.colabademo

data class ChildAnswerParent(
    val childAnswerDataList: ArrayList<ChildAnswerData>
)

data class ChildAnswerData(
    val liabilityName: String,
    val liabilityTypeId: Int,
    val monthlyPayment: Int,
    val name: String,
    val remainingMonth: Int
)