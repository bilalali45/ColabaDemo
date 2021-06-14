package com.rnsoft.colabademo

import retrofit2.Response
import retrofit2.http.*

interface ServerApi{

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // LOGIN SCREEEN API...........
    /*
    @Headers(
        "Content-Type: application/json;charset=utf-8",
        "Accept: application/json;charset=utf-8",
        "Cache-Control: max-age=640000"
    )
     */

    @POST("api/mcu/mobile/identity/mcuaccount/signin")
    suspend fun login(@Body loginRequest: LoginRequest): Response<LoginResponse>

    suspend fun loginTwo(@Query("Email") Email:String, @Query("Password") Password:String): Response<LoginResponse>

    @GET("api/mcu/mobile/identity/mcuaccount/GetMcuTenantTwoFaValues")
    suspend fun getMcuTenantTwoFaValuesService( @Header("IntermediateToken")  IntermediateToken:String): TenantConfigurationResponse

    @POST("api/mobile/identity/mcuaccount/SendTwoFa")
    suspend fun sendTwoFa( @Header("IntermediateToken")  IntermediateToken:String): SendTwoFaResponse

    @GET("api/mcu/mobile/identity/mcuaccount/GetTwoFaSettings")
    suspend fun getOtpSetting(@Header("IntermediateToken")  IntermediateToken:String) :OtpSettingResponse

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // FORGOT SCREEN API....
    @POST("api/mcu/mobile/identity/mcuaccount/ForgotPasswordRequest")
    suspend fun forgotPasswordRequest(@Body forgotPasswordEmail: ForgotRequest): ForgotPasswordResponse


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // PHONE NUMBER SCREEN API................
    @POST("api/mcu/mobile/identity/mcuaccount/SkipTwoFa")
    suspend fun skipTwoFactorApi(@Header("IntermediateToken")  IntermediateToken:String): SkipTwoFactorResponse

    @POST("api/mcu/mobile/identity/mcuaccount/SendTwoFaToNumber") // Also used in OTP Screen and Phone Screen....
    suspend fun sendTwoFaToNumber(@Header("IntermediateToken")  IntermediateToken:String,
        @Query("PhoneNumber")  PhoneNumber: String
    ) : OtpSentResponse


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // OTP SCREEN API....

    @POST("api/mcu/mobile/identity/mcuaccount/VerifyTwoFa")
    suspend fun verifyOtpCode(@Header("IntermediateToken")  IntermediateToken:String,
                              @Body otpRequest: OtpRequest
    ) :OtpVerificationResponse

    @POST("api/mcu/mobile/identity/mcuaccount/DontAskTwoFa")
    suspend fun notAskForOtpAgain(@Header("Authorization")  Authorization:String) :NotAskForOtpResponse


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // OTP DASHBOARD API....
    @POST("api/mcu/mobile/identity/mcuaccount/Logout")
    suspend fun logoutUser(@Header("Authorization")  Authorization:String) :LogoutResponse



    @POST(" api/mcu/mobile/identity/mcuaccount/RefreshAccessToken")
    suspend fun refreshToken(@Body refreshTokenRequest: RefreshTokenRequest) :LogoutResponse


    // @POST("item-list")
   // suspend fun fetchItemList( @Header("Authorization")  authHeader:String): ItemListResult


    //@POST("authenticate")
    //fun loginWithCallBack(@Body loginRequest: LoginRequest): Call<LoginResponse>

}