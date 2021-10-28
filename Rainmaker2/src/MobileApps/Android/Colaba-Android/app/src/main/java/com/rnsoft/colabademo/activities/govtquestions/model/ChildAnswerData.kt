package com.rnsoft.colabademo

data class ChildAnswerParent(
    val childAnswerDataList: ArrayList<ChildAnswerData>
)

data class ChildAnswerData(
    val liabilityName: String,
    val liabilityTypeId: String,
    val monthlyPayment: String,
    val name: String,
    val remainingMonth: String
)