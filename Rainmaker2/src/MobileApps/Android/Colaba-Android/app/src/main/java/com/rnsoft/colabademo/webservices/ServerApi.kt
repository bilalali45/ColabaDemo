package com.rnsoft.colabademo

import retrofit2.Call
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
    suspend fun login(@Body loginRequest: LoginRequest ,  @Header("dontAskTwoFaIdentifier")  dontAskTwoFaIdentifier:String=""): Response<LoginResponse>

    suspend fun loginTwo(@Query("Email") Email:String, @Query("Password") Password:String): Response<LoginResponse>

    @GET("api/mcu/mobile/identity/mcuaccount/GetMcuTenantTwoFaValues")
    suspend fun getMcuTenantTwoFaValuesService( @Header("IntermediateToken")  IntermediateToken:String): TenantConfigurationResponse

    @POST("api/mcu/mobile/identity/mcuaccount/SendTwoFa")
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



    @POST("api/mcu/mobile/identity/mcuaccount/RefreshAccessToken")
    fun refreshToken(@Body refreshTokenRequest: RefreshTokenRequest) : Call<LoginResponse>

    @GET("api/mcu/mobile/loanapplication/loan/GetListForPipeline")
    suspend fun loadAllLoansFromApi(
        @Header("Authorization" )  Authorization:String,
        @Query("dateTime" , encoded = true)  dateTime:String,
        @Query("pageNumber")  pageNumber:Int,
        @Query("pageSize")  pageSize:Int ,
        @Query("loanFilter") loanFilter:Int ,
        @Query("orderBy") orderBy:Int ,
        @Query("assignedToMe")  assignedToMe:Boolean)
    :ArrayList<LoanItem>


    @GET("api/mcu/mobile/loanapplication/loan/search")
    suspend fun searchByText(
        @Header("Authorization" )  Authorization:String,
        @Query("pageNumber")  pageNumber:Int,
        @Query("pageSize")  pageSize:Int ,
        @Query("searchTerm")  searchTerm:String)
            :ArrayList<SearchItem>



    @GET("api/mcu/mobile/Notification/notification/GetPaged")
    suspend fun getNotificationListing(
        @Header("Authorization" )  Authorization:String,
        @Query("pageSize")  pageSize:Int,
        @Query("lastId")  lastId:Int ,
        @Query("mediumId")  mediumId:Int
      ):ArrayList<NotificationItem>


    @GET("api/mcu/mobile/Notification/notification/getcount")
    suspend fun getNotificationCount(@Header("Authorization" )  Authorization:String):TotalNotificationCount


    @PUT("api/mcu/mobile/Notification/notification/Read")
    suspend fun readNotifications(@Header("Authorization" )  Authorization:String, @Body json:ArrayList<Int>):Response<Any>

    @PUT("api/mcu/mobile/Notification/notification/Seen")
    suspend fun seenNotifications(@Header("Authorization" )  Authorization:String, @Body ids:ArrayList<Int>):Response<Any>

    @PUT("api/mcu/mobile/Notification/notification/Delete")
    suspend fun deleteNotifications(@Header("Authorization" )  Authorization:String, @Body ids:ArrayList<Int>):Response<Any>


    //@POST("authenticate")
    //fun loginWithCallBack(@Body loginRequest: LoginRequest): Call<LoginResponse>

}