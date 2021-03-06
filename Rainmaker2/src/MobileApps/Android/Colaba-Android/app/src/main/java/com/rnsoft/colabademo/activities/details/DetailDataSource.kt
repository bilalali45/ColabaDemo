package com.rnsoft.colabademo

import android.util.Log
import com.rnsoft.colabademo.activities.details.boverview.model.BorrowerInvitationStatus
import com.rnsoft.colabademo.activities.details.model.SendInvitationEmailModel
import okhttp3.ResponseBody
import retrofit2.Response
import timber.log.Timber
import java.io.File
import java.io.IOException
import javax.inject.Inject

class DetailDataSource  @Inject constructor(private val serverApi: ServerApi) {

    suspend fun getInvitationStatus(loanApplicationId: Int, borrowerId: Int): Result<BorrowerInvitationStatus> {
        return try {
            val response = serverApi.getInvitationStatus(loanApplicationId,borrowerId)
            //Log.e("invitation-status-success", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getInvitationEmail(loanApplicationId: Int, borrowerId: Int): Result<InvitatationEmailModel> {
        return try {
            val response = serverApi.getInvitationRenderEmail(loanApplicationId,borrowerId)
            //Log.e("invitation-Email-success", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun sendInvitationEmail(emailBody: SendInvitationEmailModel): Result<Response<Unit>> {
        //Log.e("sendInvitationEmail", "DataSource " + " $emailBody")
        return try {
            val response = serverApi.sendBorrowerInvitation(emailBody)
            //Log.e("invitation-Email-success", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }


    suspend fun resendInvitationEmail(emailBody: SendInvitationEmailModel): Result<Response<Unit>> {
        return try {
            val response = serverApi.resendBorrowerInvitation(emailBody)
            //Log.e("resend-invitation-Email-success", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getLoanInfo(loanApplicationId: Int): Result<BorrowerOverviewModel> {
        return try {
            val response = serverApi.getLoanInfo(loanApplicationId)
            //Log.e("BorrowerOverview-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getBorrowerDocuments(
        token: String,
        loanApplicationId: Int
    ): Result<ArrayList<BorrowerDocsModel>> {
        return try {
            val response = serverApi.getBorrowerDocuments(loanApplicationId)
            //Log.e("BorrowerDocsModel-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun getBorrowerApplicationTabData(
        token: String,
        loanApplicationId: Int
    ): Result<BorrowerApplicationTabModel> {
        return try {
            val response = serverApi.getBorrowerApplicationTabData(loanApplicationId)
            //Timber.e(response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }

    suspend fun downloadFile(token:String, id:String, requestId:String, docId:String, fileId:String):Response<ResponseBody>?{
        try {
            val result = serverApi.downloadFile(
                id = id,
                requestId = requestId,
                docId = docId,
                fileId = fileId
            )
            Timber.e(result.body().toString())
            Timber.e(result.raw().toString())
            Timber.e(result.code().toString())
            Timber.e(result.errorBody().toString())
            Timber.e(result.errorBody()?.charStream().toString())
            Timber.e(result.errorBody()?.source().toString())


            return result
        } catch (e: Throwable) {
            /*
            if(e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
             */

            return  null

        }
    }

    suspend fun getMilestoneForLoanCenter( loanApplicationId: Int): Result<AppMileStoneResponse> {
        return try {

            val response = serverApi.getMilestoneForLoanCenter( loanApplicationId)
            //Log.e("getLoanInfo-", response.toString())
            Result.Success(response)
        } catch (e: Throwable) {
            if (e is NoConnectivityException)
                Result.Error(IOException(AppConstant.INTERNET_ERR_MSG))
            else
                Result.Error(IOException("Error notification -", e))
        }
    }



}