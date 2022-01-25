package com.rnsoft.colabademo.activities

import com.google.gson.annotations.Expose

import com.google.gson.annotations.SerializedName
import com.rnsoft.colabademo.EthnicityModel


class DemoGraphicModel {

    @SerializedName("loanApplicationId")
    @Expose
    private val loanApplicationId: Int? = null

    @SerializedName("borrowerId")
    @Expose
    private val borrowerId: Int? = null


    @SerializedName("genderId")
    @Expose
    private val genderId: Int? = null


    @SerializedName("race")
    @Expose
    val race: ArrayList<RaceModel>? = null


    @SerializedName("ethnicity")
    @Expose
    val ethnicity: ArrayList<EthnicityModel>? = null



}