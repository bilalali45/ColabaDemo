package com.rnsoft.colabademo

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

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