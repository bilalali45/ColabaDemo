package com.rnsoft.colabademo

import android.content.SharedPreferences
import javax.inject.Inject

class LoanRepo @Inject constructor(
    private val loanDataSource: LoanDataSource, private val preferenceEditor: SharedPreferences.Editor
) {

    suspend fun getAllLoans(token: String, dateTime:String,
                            pageNumber:Int, pageSize:Int,
                            loanFilter:Int, orderBy:Int,
                            assignedToMe:Boolean)
    :Result<ArrayList<LoanItem>>
    {
        val loansResult = loanDataSource.loadAllLoans(token = token ,  dateTime = dateTime,
            pageNumber = pageNumber, pageSize = pageSize, loanFilter = loanFilter,
            orderBy = orderBy, assignedToMe = assignedToMe)

        if(loansResult is Result.Success)
            storeLoansResultToRoom()

        return loansResult
    }

    private fun storeLoansResultToRoom(){

    }



}