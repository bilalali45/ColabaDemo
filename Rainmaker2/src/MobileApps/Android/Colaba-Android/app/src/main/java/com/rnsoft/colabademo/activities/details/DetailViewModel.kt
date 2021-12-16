package com.rnsoft.colabademo

import android.content.Context
import android.util.Log
import android.widget.Toast
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import dagger.hilt.android.qualifiers.ApplicationContext
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import okhttp3.ResponseBody
import org.greenrobot.eventbus.EventBus
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


    private var docsServiceRunning:Boolean = false
    private var overviewServiceRunning:Boolean = false
    private var applicationServiceRunning:Boolean = false


    suspend fun getBorrowerOverview(token:String, loanApplicationId:Int) {
        Log.e("Token", token)
        if(!overviewServiceRunning) {
            overviewServiceRunning = true
            viewModelScope.launch (Dispatchers.IO) {
                val responseResult = detailRepo.getLoanInfo(token = token, loanApplicationId = loanApplicationId)
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
            Log.e("fileName", "= $fileName")
            if(result?.body() is ResponseBody) {
                val responseBody = result.body()
                try {
                    //you can now get your file in the InputStream
                    val isRead: InputStream? = responseBody?.byteStream()
                    Log.e("file lenght", "" + responseBody?.contentLength())
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
                                    Log.e("Progresss---", "$progress - $totalLength")
                                    val temp = arrayListOf<Long>()
                                    temp.add(progress)
                                    temp.add(totalLength)
                                    _progressGlobal.value = temp
                                    //callback(progress, fileSize)

                                }
                                Log.e(
                                    "isRead = ",
                                    "" + progress + " total size = " + responseBody.contentLength()
                                )
                            }

                            bool = saveFileToExternalStorage(buffer.toByteArray(), fileName)
                            Log.e("bool", "= $bool")
                        }
                    }
                }catch (e: Exception){Log.e("Exception", " can not save PDF file...")}

            }
            Log.e("File", " is file created??$bool")


            if(!bool)
                EventBus.getDefault().post(WebServiceErrorEvent(null, true))
            else
                EventBus.getDefault().post(FileDownloadEvent(fileName))


        }
    }

    private val _progressGlobal : MutableLiveData<ArrayList<Long>> =   MutableLiveData()
    val progressGlobal: LiveData<ArrayList<Long>> get() = _progressGlobal


    private suspend fun publishProgress(
        progress: Long, //bytes
        fileSize: Long  //bytes
    ) {
        withContext(Dispatchers.Main) { // invoke callback in UI thtread
            Log.e("Progresss---", "$progress  $fileSize")
            //callback(progress, fileSize)

        }
    }

    private fun saveFileToExternalStorage(data: ByteArray , fileName:String): Boolean {
        val path: File = applicationContext.filesDir
        val file = File(path, fileName )
        var outputStream: FileOutputStream? = null
        try {
            outputStream = FileOutputStream(file)
            outputStream.write(data)
            outputStream.flush()
            outputStream.close()
        } catch (e: java.lang.Exception) {
            Toast.makeText(applicationContext, e.message, Toast.LENGTH_LONG)
            return false
        }
        return true
    }


    suspend fun getMilestoneForLoanCenter(token:String, loanApplicationId:Int) {
        viewModelScope.launch (Dispatchers.IO) {
            val responseResult = detailRepo.getMilestoneForLoanCenter(token = token, loanApplicationId = loanApplicationId)
            withContext(Dispatchers.Main) {
                overviewServiceRunning = false
                if (responseResult is Result.Success)
                    _appMileStoneResponse.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    fun resetMileStoneToNull() {
        _appMileStoneResponse.value = null
        _appMileStoneResponse.postValue(null)
    }



}