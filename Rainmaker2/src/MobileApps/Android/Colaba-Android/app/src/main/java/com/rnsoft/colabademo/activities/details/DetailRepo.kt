package com.rnsoft.colabademo

import com.rnsoft.colabademo.activities.details.boverview.model.BorrowerInvitationStatus
import com.rnsoft.colabademo.activities.details.model.SendInvitationEmailModel
import okhttp3.ResponseBody
import retrofit2.Response
import javax.inject.Inject


class DetailRepo @Inject constructor(private val detailDataSource: DetailDataSource) {

   suspend fun getLoanInfo(token:String ,loanApplicationId:Int):Result<BorrowerOverviewModel>{
        return detailDataSource.getLoanInfo(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getInvitationStatus(loanApplicationId:Int,borrowerId: Int):Result<BorrowerInvitationStatus>{
        return detailDataSource.getInvitationStatus(loanApplicationId = loanApplicationId,borrowerId)
    }

    suspend fun sendInvitationEmail(emailBody:SendInvitationEmailModel):Result<Any>{
        return detailDataSource.sendInvitationEmail(emailBody)
    }

    suspend fun resendInvitationEmail(emailBody:SendInvitationEmailModel):Result<Any>{
        return detailDataSource.resendInvitationEmail(emailBody)
    }


    suspend fun getInvitationEmail(loanApplicationId:Int,borrowerId: Int):Result<InvitatationEmailModel>{
        return detailDataSource.getInvitationEmail(loanApplicationId = loanApplicationId,borrowerId)
    }

    suspend fun getBorrowerDocuments(token:String ,loanApplicationId:Int):Result<ArrayList<BorrowerDocsModel>>{
        return detailDataSource.getBorrowerDocuments(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getBorrowerApplicationTabData(token:String ,loanApplicationId:Int):Result<BorrowerApplicationTabModel>{
        return detailDataSource.getBorrowerApplicationTabData(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun downloadFile(token:String , id:String, requestId:String, docId:String, fileId:String , fileName:String): Response<ResponseBody>? {
        return detailDataSource.downloadFile(token = token , id = id, requestId = requestId, docId = docId, fileId = fileId )
    }

    suspend fun getMilestoneForLoanCenter( loanApplicationId: Int): Result<AppMileStoneResponse> {
        return detailDataSource.getMilestoneForLoanCenter( loanApplicationId = loanApplicationId)
    }

}