package com.rnsoft.colabademo

import javax.inject.Inject

class SignUpFlowDataSource @Inject constructor(private val serverApi: ServerApi){

    /*
    suspend fun login(username: String, password: String): Result<LoginResponse> {
        return try {
            val loggedInUser = testAPI.login(LoginRequest(username, password))
            Result.Success(loggedInUser)
        } catch (e: Throwable) {
            Result.Error(IOException("Error logging in", e))
        }
    }

    suspend fun fetchUserList(): Result<UserListResult> {
        return try {
             Result.Success(testAPI.fetchUserList())
        } catch (e: Throwable) {
            Result.Error(IOException("Error logging in", e))
        }
    }

     */
}