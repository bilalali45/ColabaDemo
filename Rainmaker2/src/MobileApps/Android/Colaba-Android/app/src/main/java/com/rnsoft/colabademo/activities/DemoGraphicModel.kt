package com.rnsoft.colabademo.activities

import com.google.gson.annotations.Expose

import com.google.gson.annotations.SerializedName
import com.rnsoft.colabademo.EthnicityModel


class DemoGraphicModel {

    @SerializedName("loanApplicationId")
    @Expose
     val loanApplicationId: Int? = 0

    @SerializedName("borrowerId")
    @Expose
     val borrowerId: Int? = 0


    @SerializedName("genderId")
    @Expose
    val genderId: Int? = 0


    @SerializedName("race")
    @Expose
    val race: ArrayList<RaceModel>? = null


    @SerializedName("ethnicity")
    @Expose
    val ethnicity: ArrayList<EthnicityModel>? = null



}