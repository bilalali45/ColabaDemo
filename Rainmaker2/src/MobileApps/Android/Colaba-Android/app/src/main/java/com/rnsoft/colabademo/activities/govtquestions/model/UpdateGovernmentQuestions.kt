package com.rnsoft.colabademo

data class UpdateGovernmentQuestions(
    val BorrowerId: Int = 0,
    val LoanApplicationId: String = "5",
    val Questions: ArrayList<QuestionData> = arrayListOf()
)
