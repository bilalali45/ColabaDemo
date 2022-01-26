package com.rnsoft.colabademo.activities

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName
import com.rnsoft.colabademo.EthnicityModel
import com.rnsoft.colabademo.activities.govtquestions.model.answerData

class GetGovermentmodel {


    @SerializedName("id")
    @Expose
    var id: Int? = 0

    @SerializedName("parentQuestionId")
    @Expose
    var parentQuestionId: Int? = 0


    @SerializedName("headerText")
    @Expose
    var headerText: String? = null



    @SerializedName("questionSectionId")
    @Expose
    var questionSectionId: Int = 0

    @SerializedName("ownTypeId")
    @Expose
    var ownTypeId: Int = 0


    @SerializedName("firstName")
    @Expose
    var firstName: String? = null

    @SerializedName("lastName")
    @Expose
    var lastName: String? = null


    @SerializedName("question")
    @Expose
    var question: String? = null



    @SerializedName("answer")
    @Expose
    var answer: String? = null




    @SerializedName("answerDetail")
    @Expose
    var answerDetail: String? = null




    @SerializedName("answerData")
    @Expose
   /// var answerData: ArrayList<EthnicityModel>? = null
     var answerData: answerData? = null
}