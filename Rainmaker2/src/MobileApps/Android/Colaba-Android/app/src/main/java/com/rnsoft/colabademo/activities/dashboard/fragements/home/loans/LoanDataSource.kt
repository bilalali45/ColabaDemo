package com.rnsoft.colabademo

import android.util.Log
import java.io.IOException
import javax.inject.Inject

class LoanDataSource  @Inject constructor(private val serverApi: ServerApi){

    suspend fun loadAllLoans(token: String, dateTime:String,
                             pageNumber:Int, pageSize:Int,
                             loanFilter:Int, orderBy:Int,
                             assignedToMe:Boolean)
    :Result<ArrayList<LoanItem>>{
        return try {
            val newToken = "Bearer $token"
            //Log.e("LoanDataSource-API-Param", "DateTime= " + dateTime + " PageNum= " + pageNumber + " PageSize= " + pageSize + " LoanFilter= " + loanFilter + " OrderBy= " + orderBy +
              //      " Assigned to me=" + assignedToMe)

            val response = serverApi.loadAllLoansFromApi(

                dateTime = dateTime,
                pageNumber = pageNumber,
                pageSize = pageSize,
                loanFilter = loanFilter,
                orderBy = orderBy,
                assignedToMe = assignedToMe
            )
            Log.e("LoanDataSource-", response.toString())
            Result.Success(response)
        }
        catch (e: Throwable) {
            //Log.e("Exception",e.localizedMessage)
           if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error logging in", e))
        }
    }

}