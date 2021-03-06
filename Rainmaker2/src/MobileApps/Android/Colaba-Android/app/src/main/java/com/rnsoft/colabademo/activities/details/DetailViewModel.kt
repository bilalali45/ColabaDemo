package com.rnsoft.colabademo

import android.content.Context
import android.util.Log
import android.widget.Toast
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.activities.details.boverview.model.BorrowerInvitationStatus
import com.rnsoft.colabademo.activities.details.model.SendInvitationEmailModel
import dagger.hilt.android.lifecycle.HiltViewModel
import dagger.hilt.android.qualifiers.ApplicationContext
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import okhttp3.ResponseBody
import org.greenrobot.eventbus.EventBus
import timber.log.Timber
import java.io.ByteArrayOutputStream
import java.io.File
import java.io.FileOutputStream
import java.io.InputStream
import javax.inject.Inject

@HiltViewModel
class DetailViewModel @Inject constructor(private val detailRepo: DetailRepo , @ApplicationContext val applicationContext: Context) : ViewModel() {

    private val _borrowerOverviewModel : MutableLiveData<BorrowerOverviewModel> =   MutableLiveData()
    val borrowerOverviewModel: LiveData<BorrowerOverviewModel> get() = _borrowerOverviewModel

    private val _borrowerDocsModelList : MutableLiveData<ArrayList<BorrowerDocsModel>> =   MutableLiveData()
    val borrowerDocsModelList: LiveData<ArrayList<BorrowerDocsModel>> get() = _borrowerDocsModelList

    private val _borrowerApplicationTabModel : MutableLiveData<BorrowerApplicationTabModel> =   MutableLiveData()
    val borrowerApplicationTabModel: MutableLiveData<BorrowerApplicationTabModel> get() = _borrowerApplicationTabModel

    private val _appMileStoneResponse : MutableLiveData<AppMileStoneResponse?> =   MutableLiveData()
    val appMileStoneResponse: LiveData<AppMileStoneResponse?> get() = _appMileStoneResponse

    private val _invitationStatus : MutableLiveData<BorrowerInvitationStatus> =   MutableLiveData()
    val invitationStatus: LiveData<BorrowerInvitationStatus> get() = _invitationStatus

    private val _invitationEmail : MutableLiveData<InvitatationEmailModel> = MutableLiveData()
    val invitationEmail: LiveData<InvitatationEmailModel> get() = _invitationEmail

    private var docsServiceRunning:Boolean = false
    private var overviewServiceRunning:Boolean = false
    private var applicationServiceRunning:Boolean = false

    fun getBorrowerInvitationStatus(loanApplicationId:Int, borrowerId: Int) {
        viewModelScope.launch() {
            val responseResult = detailRepo.getInvitationStatus(loanApplicationId = loanApplicationId,borrowerId = borrowerId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                    _invitationStatus.value = (responseResult.data)
                }

                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    fun getInvitationEmail(loanApplicationId:Int, borrowerId: Int){
        viewModelScope.launch() {
            val responseResult = detailRepo.getInvitationEmail(loanApplicationId = loanApplicationId,borrowerId = borrowerId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _invitationEmail.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    fun sendInvitationEmail(emailBody: SendInvitationEmailModel){
        viewModelScope.launch() {
            val responseResult = detailRepo.sendInvitationEmail(emailBody)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                    EventBus.getDefault().post(SendInvitationEvent(ResponseStatus(200,"OK","")))
                }
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(SendDocRequestEvent(ResponseStatus(400,"OK","")))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(SendDocRequestEvent(ResponseStatus(500,"OK","")))
            }
        }
    }



    fun resendInvitationEmail(emailBody: SendInvitationEmailModel){
        viewModelScope.launch() {
            val responseResult = detailRepo.resendInvitationEmail(emailBody)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                    EventBus.getDefault().post(SendInvitationEvent(ResponseStatus(200, "OK", "")))
                }
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(SendDocRequestEvent(ResponseStatus(400,"OK","")))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(SendDocRequestEvent(ResponseStatus(500,"OK","")))
            }
        }
    }

    suspend fun getBorrowerOverview(loanApplicationId:Int) {
        if(!overviewServiceRunning) {
            overviewServiceRunning = true
            viewModelScope.launch (Dispatchers.IO) {
                val responseResult = detailRepo.getLoanInfo(loanApplicationId)
                withContext(Dispatchers.Main) {
                    overviewServiceRunning = false
                    if (responseResult is Result.Success)
                        _borrowerOverviewModel.value = (responseResult.data)
                    else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                    else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                }
            }
        }
    }

    suspend fun getBorrowerDocuments(token:String, loanApplicationId:Int) {
        if(!docsServiceRunning) {
            docsServiceRunning = true
            viewModelScope.launch(Dispatchers.IO) {
                val responseResult = detailRepo.getBorrowerDocuments(
                    token = token,
                    loanApplicationId = loanApplicationId
                )
                withContext(Dispatchers.Main) {
                    docsServiceRunning = false
                    if (responseResult is Result.Success) {
                        _borrowerDocsModelList.value = (responseResult.data)
                    }
                    else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                    else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                }
            }
        }
    }

    suspend fun getBorrowerApplicationTabData(token:String, loanApplicationId:Int) {
        if(!applicationServiceRunning) {
            applicationServiceRunning = true
            viewModelScope.launch(Dispatchers.IO) {
                val responseResult = detailRepo.getBorrowerApplicationTabData(
                    token = token,
                    loanApplicationId = loanApplicationId
                )
                withContext(Dispatchers.Main) {
                    applicationServiceRunning = false
                    if (responseResult is Result.Success) {
                        _borrowerApplicationTabModel.value = (responseResult.data)
                    }
                    else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                    else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                }
            }
        }
    }

    fun downloadFile(token:String,  id:String, requestId:String, docId:String, fileId:String , fileName:String) {
        viewModelScope.launch(Dispatchers.IO) {
            val result = detailRepo.downloadFile(
                token = token,
                id = id,
                requestId = requestId,
                docId = docId,
                fileId = fileId,
                fileName = fileName
            )


            var bool = false
            Timber.e("fileName", "= $fileName")
            if(result?.body() is ResponseBody) {
                val responseBody = result.body()
                try {
                    //you can now get your file in the InputStream
                    val isRead: InputStream? = responseBody?.byteStream()
                    Timber.e("file lenght", "" + responseBody?.contentLength())
                    val totalLength = responseBody?.contentLength()
                    totalLength?.let {
                        isRead?.let { isRead ->
                            val buffer = ByteArrayOutputStream()
                            var nRead: Int
                            var progress = 0L
                            val data = ByteArray(16384)
                            while (isRead.read(data, 0, data.size).also { nRead = it } != -1) {
                                buffer.write(data, 0, nRead)
                                progress += nRead
                                // publishProgress(progress,responseBody.contentLength() )
                                withContext(Dispatchers.Main) { // invoke callback in UI thtread
                                    Timber.e("Progresss---", "$progress - $totalLength")
                                    val temp = arrayListOf<Long>()
                                    temp.add(progress)
                                    temp.add(totalLength)
                                    _progressGlobal.value = temp
                                    //callback(progress, fileSize)

                                }
                                Timber.e(
                                    "isRead = ",
                                    "" + progress + " total size = " + responseBody.contentLength()
                                )
                            }

                            bool = saveFileToExternalStorage(buffer.toByteArray(), fileName)
                            Timber.e("bool", "= $bool")
                        }
                    }
                }catch (e: Exception) {
                    Timber.e(" can not save PDF file...")
                }

            }
            Timber.e("File", " is file created??$bool")


            if(!bool)
                EventBus.getDefault().post(WebServiceErrorEvent(null, true))
            else
                EventBus.getDefault().post(FileDownloadEvent(fileName))


        }
    }

    private val _progressGlobal : MutableLiveData<ArrayList<Long>> =   MutableLiveData()
    val progressGlobal: LiveData<ArrayList<Long>> get() = _progressGlobal

    private fun saveFileToExternalStorage(data: ByteArray , fileName:String): Boolean {
        val path: File = applicationContext.filesDir
        val file = File(path, fileName )
        val outputStream: FileOutputStream?
        try {
            outputStream = FileOutputStream(file)
            outputStream.write(data)
            outputStream.flush()
            outputStream.close()
        } catch (e: java.lang.Exception) {
            Toast.makeText(applicationContext, e.message, Toast.LENGTH_LONG).show()
            return false
        }
        return true
    }

    fun getMilestoneForLoanCenter( loanApplicationId:Int) {
        viewModelScope.launch (Dispatchers.IO) {
            val responseResult = detailRepo.getMilestoneForLoanCenter( loanApplicationId = loanApplicationId)
            withContext(Dispatchers.Main) {
                overviewServiceRunning = false
                if (responseResult is Result.Success)
                    _appMileStoneResponse.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(SendDocRequestEvent(ResponseStatus(400,"OK","")))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(SendDocRequestEvent(ResponseStatus(500,"OK","")))
            }
        }
    }

    fun resetMileStoneToNull() {
        _appMileStoneResponse.value = null
        _appMileStoneResponse.postValue(null)
    }


}