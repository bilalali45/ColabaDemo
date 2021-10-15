package com.rnsoft.colabademo

import com.rnsoft.colabademo.activities.loan.model.LoanGoalModel
import com.rnsoft.colabademo.activities.model.*
import com.rnsoft.colabademo.activities.realestate.model.RealEstateResponse
import okhttp3.ResponseBody
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

    // subject property
    @GET("api/mcu/mobile/loanapplication/Loan/GetAllPropertyTypeDropDown")
    suspend fun getPropertyTypes(@Header("Authorization" )Authorization:String) : ArrayList<DropDownResponse>

    @GET("api/mcu/mobile/loanapplication/Loan/GetPropertyUsageDropDown")
    suspend fun getOccupancyType(@Header("Authorization" )Authorization:String) : ArrayList<DropDownResponse>

    @GET("api/mcu/mobile/loanapplication/Loan/GetAllPropertyStatusDropDown")
    suspend fun getPropertyStauts(@Header("Authorization" )Authorization:String) : ArrayList<DropDownResponse>


    @GET("api/mcu/mobile/loanapplication/Loan/GetCountries")
    suspend fun getCountries(@Header("Authorization" )Authorization:String) : ArrayList<CountriesModel>

    @GET("api/mcu/mobile/loanapplication/Loan/GetStates")
    suspend fun getStates(@Header("Authorization" )Authorization:String) : ArrayList<StatesModel>

    @GET("api/mcu/mobile/loanapplication/Loan/GetCounties")
    suspend fun getCounties(@Header("Authorization" )Authorization:String) : ArrayList<CountiesModel>

    @GET("api/mcu/mobile/loanapplication/SubjectProperty/GetSubjectPropertyDetails")
    suspend fun getSubjectPropertyDetails(
        @Header("Authorization" )  Authorization:String,
        @Query("loanApplicationId")  loanApplicationId:Int) : SubjectPropertyDetails

    @GET("api/mcu/mobile/loanapplication/SubjectProperty/GetRefinanceSubjectPropertyDetail")
    suspend fun getSubjectPropertyRefinance(
        @Header("Authorization" )  Authorization:String,
        @Query("loanApplicationId")  loanApplicationId:Int) : SubjectPropertyRefinanceDetails

    @GET("api/mcu/mobile/loanapplication/SubjectProperty/GetCoBorrowersOccupancyStatus")
    suspend fun getCoBorrowerOccupancyStatus(
        @Header("Authorization" )  Authorization:String,
        @Query("loanApplicationId")  loanApplicationId:Int) : CoBorrowerOccupancyStatus

    @GET("api/mcu/mobile/loanapplication/Loan/GetLoanInfoDetails")
    suspend fun getLoanInfoDetails(
        @Header("Authorization")  Authorization:String,
        @Query("loanApplicationId")  loanApplicationId:Int) : LoanInfoDetailsModel

    @GET("api/mcu/mobile/loanapplication/Loan/GetAllLoanGoals")
    suspend fun getLoanGoals(
        @Header("Authorization" )  Authorization:String,
        @Query("loanpurposeid")  loanPurpuseId:Int) : ArrayList<LoanGoalModel>

    @GET("api/mcu/mobile/loanapplication/RealEstate/GetRealEstateDetails")
    suspend fun getRealEstateDetails(
        @Header("Authorization") Authorization:String,
        @Query("loanApplicationId") loanPurpuseId:Int,
        @Query("borrowerPropertyId") borrowerPropertyId:Int)
    : RealEstateResponse

    @GET("api/mcu/mobile/loanapplication/RealEstate/GetFirstMortgageDetails")
    suspend fun getFirstMortgageDetails(
        @Header("Authorization") Authorization:String,
        @Query("loanApplicationId") loanPurpuseId:Int,
        @Query("borrowerPropertyId") borrowerPropertyId:Int) : RealEstateFirstMortgageModel


    @GET("api/mcu/mobile/loanapplication/RealEstate/GetSecondMortgageDetails")
    suspend fun getSecMortgageDetails(
        @Header("Authorization") Authorization:String,
        @Query("loanApplicationId") loanPurpuseId:Int,
        @Query("borrowerPropertyId") borrowerPropertyId:Int) : RealEstateSecondMortgageModel


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
    suspend fun readNotifications(@Header("Authorization" )  Authorization:String, @Body putParams:PutParameters):Response<Any>

    @PUT("api/mcu/mobile/Notification/notification/Seen")
    suspend fun seenNotifications(@Header("Authorization" )  Authorization:String, @Body putParams:PutParameters):Any

    @PUT("api/mcu/mobile/Notification/notification/Delete")
    suspend fun deleteNotifications(@Header("Authorization" )  Authorization:String, @Body putParams:PutParameters):Response<Any>


    @GET("api/mcu/mobile/loanapplication/loan/getloaninfo")
    suspend fun getLoanInfo(
        @Header("Authorization" )  Authorization:String,
        @Query("loanApplicationId")  loanApplicationId:Int):BorrowerOverviewModel


    @GET("api/mcu/mobile/documentmanagement/mcudocument/getdocuments")
    suspend fun getBorrowerDocuments(
        @Header("Authorization" )  Authorization:String,
        @Query("loanApplicationId")  loanApplicationId:Int):ArrayList<BorrowerDocsModel>


    @Streaming
    @GET("api/mcu/mobile/documentmanagement/mcudocument/View")
    suspend fun downloadFile(
        @Header("Authorization" )  Authorization:String,
        @Query("id")  id:String,
        @Query("requestId")  requestId:String,
        @Query("docId")  docId:String,
        @Query("fileId")  fileId:String):Response<ResponseBody>


    @GET("api/mcu/mobile/loanapplication/Loan/GetBorrowerLoanApplication")
    suspend fun getBorrowerApplicationTabData(
        @Header("Authorization" )  Authorization:String,
        @Query("loanApplicationId")  loanApplicationId:Int):BorrowerApplicationTabModel


}