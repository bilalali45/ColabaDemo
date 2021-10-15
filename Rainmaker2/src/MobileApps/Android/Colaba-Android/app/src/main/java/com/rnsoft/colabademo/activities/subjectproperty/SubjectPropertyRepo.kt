package com.rnsoft.colabademo


import com.rnsoft.colabademo.activities.model.*
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/11/2021.
 */
class SubjectPropertyRepo @Inject constructor(private val dataSource: SubjectPropertyDataSource) {

    suspend fun getPropertyType(token:String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getPropertyTypes(token = token)
    }

    suspend fun getOccupancyType(token:String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getOccupancyType(token = token)
    }

    suspend fun getCountries(token:String): Result<ArrayList<CountriesModel>> {
        return dataSource.getCountries(token = token)
    }

    suspend fun getCounties(token:String): Result<ArrayList<CountiesModel>> {
        return dataSource.getCounties(token = token)
    }

    suspend fun getStates(token:String): Result<ArrayList<StatesModel>> {
        return dataSource.getStates(token = token)
    }

    suspend fun getSubjectProptyDetails(token:String ,loanApplicationId:Int):Result<SubjectPropertyDetails>{
        return dataSource.getSubjectPropertyDetails(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getSubjectProptyRefinance(token:String ,loanApplicationId:Int):Result<SubjectPropertyRefinanceDetails>{
        return dataSource.getSubjectPropertyRefinance(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getCoBorrowerOccupancyStatus(token:String ,loanApplicationId:Int):Result<CoBorrowerOccupancyStatus>{
        return dataSource.getCoBorrowerOccupancyStatus(token = token , loanApplicationId = loanApplicationId)
    }

}