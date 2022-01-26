package com.rnsoft.colabademo.activities

import com.google.gson.annotations.Expose

import com.google.gson.annotations.SerializedName
import com.rnsoft.colabademo.EthnicityModel


class DemoGraphicModel {

    @SerializedName("loanApplicationId")
    @Expose
    var loanApplicationId: Int? = 0

    @SerializedName("borrowerId")
    @Expose
    var borrowerId: Int? = 0


    @SerializedName("genderId")
    @Expose
    var genderId: Int? = 1


    @SerializedName("race")
    @Expose
    var race: ArrayList<RaceModel>? = null


    @SerializedName("ethnicity")
    @Expose
    var ethnicity: ArrayList<EthnicityModel>? = null



}