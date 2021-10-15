package com.rnsoft.colabademo

import com.rnsoft.colabademo.activities.realestate.model.RealEstateResponse
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/15/2021.
 */
class RealEstateRepo @Inject constructor(private val dataSource: RealEstateDataSource){

    suspend fun getRealEstateDetails(token:String ,loanApplicationId:Int,borrowerPropertyId:Int): Result<RealEstateResponse> {
        return dataSource.getRealEstateDetails(
            token = token,
            loanApplicationId = loanApplicationId,
            borrowerPropertyId = borrowerPropertyId
        )
    }
        suspend fun getPropertyType(token:String): Result<ArrayList<DropDownResponse>> {
            return dataSource.getPropertyTypes(token = token)
        }

        suspend fun getOccupancyType(token:String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getOccupancyType(token = token)
    }

    suspend fun getPropertyStatus(token:String): Result<ArrayList<DropDownResponse>> {
        return dataSource.getOccupancyType(token = token)
    }
}