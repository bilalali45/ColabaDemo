package com.rnsoft.colabademo


import com.rnsoft.colabademo.activities.model.*
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/11/2021.
 */
class SubjectPropertyRepo @Inject constructor(private val dataSource: SubjectPropertyDataSource) {

    suspend fun getSubjectPropertyDetails(token:String, loanApplicationId:Int):Result<SubjectPropertyDetails>{
        return dataSource.getSubjectPropertyDetails(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getSubjectPropertyRefinance(token:String, loanApplicationId:Int):Result<SubjectPropertyRefinanceDetails>{
        return dataSource.getSubjectPropertyRefinance(token = token , loanApplicationId = loanApplicationId)
    }

}