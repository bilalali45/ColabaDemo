package com.rnsoft.colabademo.activities

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName
import com.rnsoft.colabademo.Detailmodel

class DemoGetGovermentmodel {

    @SerializedName("BorrowerId")
    @Expose
    var BorrowerId: Int? = 0


    @SerializedName("LoanApplicationId")
    @Expose
    var LoanApplicationId: Int? = 0


    @SerializedName("Questions")
    @Expose
    var Questions: ArrayList<GetGovermentmodel>? = ArrayList()
}