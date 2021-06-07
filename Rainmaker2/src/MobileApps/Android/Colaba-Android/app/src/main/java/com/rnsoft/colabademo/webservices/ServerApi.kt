package com.rnsoft.colabademo

import retrofit2.Response
import retrofit2.http.*

interface ServerApi{


    @Headers(
        "Accept-Encoding: gzip,deflate,br",
        "Content-Type: application/json",
        "Connection:keep-alive",
        "Cache-Control: no-cache",
        "Accept: */*",
        "User-Agent: Retrofit 2.9.0"
     )
    @POST("api/mobile/identity/mcuaccount/signin")
    suspend fun login(@Body loginRequest: LoginRequest): Response<LoginResponse>

    @POST("api/mobile/identity/mcuaccount/ForgotPasswordRequest")
    suspend fun forgotPasswordRequest(@Body forgotPasswordEmail: ForgotRequest): ForgotPasswordResponse


    @GET("api/mobile/identity/mcuaccount/GetMcuTenantTwoFaValues")
    suspend fun getMcuTenantTwoFaValuesService( @Header("IntermediateToken")  IntermediateToken:String): TenantConfigurationResponse

    @POST("api/mobile/identity/mcuaccount/SendTwoFa")
    suspend fun sendTwoFa( @Header("IntermediateToken")  IntermediateToken:String): SendTwoFaResponse


    ///////////////////////////////////////////////////////////////////////////////////
    // Phone Number screen API..

    @POST("api/mobile/identity/mcuaccount/SkipTwoFa")
    suspend fun skipTwoFactorApi(@Header("IntermediateToken")  IntermediateToken:String): SkipTwoFactorResponse

    @FormUrlEncoded
    @POST("api/mobile/identity/mcuaccount/SendTwoFaToNumber")
    suspend fun sendTwoFaToNumber(@Header("IntermediateToken")  IntermediateToken:String,
        @Query("PhoneNumber")  PhoneNumber: String
    ) : OtpToNumberResponse


    // @POST("item-list")
   // suspend fun fetchItemList( @Header("Authorization")  authHeader:String): ItemListResult


    //@POST("authenticate")
    //fun loginWithCallBack(@Body loginRequest: LoginRequest): Call<LoginResponse>

}