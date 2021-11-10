package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class GovernmentQuestionsModelClass(
    val code: String?=null,
    @SerializedName("data") val questionData : ArrayList<QuestionData>?=null,
    val message: String?=null,
    val status: String?=null,
    var passedBorrowerId:Int?
)


data class QuestionData(
    val id: Int?=null,
    val parentQuestionId:Int? = null,
    var answer: String?,
    val answerDetail: String?= "",
    val headerText:String? = "title1",
    val answerData : Any? = null,

    val firstName: String?=null,

    val lastName: String?=null,
    val ownTypeId: Int?=null,
    val question: String?=null,
    val questionSectionId: Int?=null,
    val selectionOptionId: Any?=null
)

