package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.util.Log
import okhttp3.Authenticator
import okhttp3.Request
import okhttp3.Response
import okhttp3.Route
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class TokenAuthenticator ( private val sharedPreferences: SharedPreferences) : Authenticator {
    override fun authenticate(route: Route?, response: Response): Request? {
        return null
    }

    /*
        override fun authenticate(route: Route?, authResponse: Response?): Request? {
            Log.e("authenticate - ", "Generating Token---")
            val newTokenResponse : LoginResponse = getNewToken() ?: return null
            if(newTokenResponse.token.isNotBlank() && newTokenResponse.token.isNotEmpty()) {
                sharedPreferences.edit().putString(MyAppConfigConstant.TOKEN, newTokenResponse.token).apply()
                return authResponse?.request()!!.newBuilder()
                    .header("Authorization", "Bearer " + newTokenResponse.token)
                    .build()
            }
            return null
        }

        private  fun getNewToken(): LoginResponse?
        {
            // Refresh your access_token using a synchronous api request
            val retrofitInstance = Retrofit.Builder()
                .baseUrl(MyAppConfigConstant.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build()
            val testAPI = retrofitInstance.create(TestAPI::class.java)
            val retrofitResponse = testAPI.loginWithCallBack(LoginRequest(MyAppConfigConstant.LOGIN_NAME, MyAppConfigConstant.PASSWORD)).execute()
            if (retrofitResponse.body() != null)
                if (retrofitResponse.body()!!.token.isNotEmpty() && retrofitResponse.body()!!.token.isNotBlank()) {
                    return retrofitResponse.body()!!
                }
            return null
        }

     */
}