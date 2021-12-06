package com.rnsoft.colabademo

import android.content.SharedPreferences
import javax.inject.Inject

class StartNewAppRepo  @Inject constructor(private val newAppDataSource: StartNewAppDataSource, private val spEditor: SharedPreferences.Editor) {

    suspend fun searchByBorrowerContact(token:String, searchKeyword:String):Result<SearchResultResponse>{
        return newAppDataSource.searchByBorrowerContact(token = token, searchKeyword = searchKeyword)
    }

    suspend fun lookUpBorrowerContact(token:String,  borrowerEmail:String, borrowerPhone:String):Result<LookUpBorrowerContactResponse>{
        return newAppDataSource.lookUpBorrowerContact(token = token, borrowerEmail = borrowerEmail, borrowerPhone = borrowerPhone)
    }

}