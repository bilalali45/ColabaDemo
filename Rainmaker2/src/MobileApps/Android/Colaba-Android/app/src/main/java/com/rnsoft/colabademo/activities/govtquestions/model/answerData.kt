package com.rnsoft.colabademo.activities.govtquestions.model

import com.google.gson.annotations.SerializedName

class answerData {

    @SerializedName("selectionOptionId")
     var selectionOptionId : Int? = 0

    @SerializedName("selectionOptionText")
    var selectionOptionText : String? = null
}