package com.rnsoft.colabademo

import com.google.gson.annotations.SerializedName

data class GovernmentQuestionsModelClass(
    val code: String?=null,
    @SerializedName("data") val questionData : ArrayList<QuestionData>?=null,
    val message: String?=null,
    val status: String?=null
)


data class QuestionData(
    val parentQuestionId:Int? = null,
    val answer: String?,
    val answerDetail: String?= "",
    val headerText:String? = "title1",
    val answerData : Any? = null,

    val firstName: String?=null,
    val id: Int?=null,
    val lastName: String?=null,
    val ownTypeId: Int?=null,
    val question: String?=null,
    val questionSectionId: Int?=null,
    val selectionOptionId: Any?=null



)