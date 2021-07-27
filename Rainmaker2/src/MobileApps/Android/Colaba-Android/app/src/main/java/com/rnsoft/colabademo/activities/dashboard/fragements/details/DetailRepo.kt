package com.rnsoft.colabademo

import android.content.ActivityNotFoundException
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.net.Uri
import android.os.Environment
import android.util.Log
import androidx.core.content.ContextCompat.startActivity
import androidx.core.content.FileProvider
import dagger.hilt.android.qualifiers.ApplicationContext
import okhttp3.ResponseBody
import retrofit2.Response
import java.io.File
import java.io.FileOutputStream
import java.io.InputStream
import javax.inject.Inject


class DetailRepo  @Inject constructor(
    private val detailDataSource: DetailDataSource, private val spEditor: SharedPreferences.Editor,
    @ApplicationContext val applicationContext: Context
) {


   suspend fun getLoanInfo(token:String ,loanApplicationId:Int):Result<BorrowerOverviewModel>{
        return detailDataSource.getLoanInfo(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun getBorrowerDocuments(token:String ,loanApplicationId:Int):Result<ArrayList<BorrowerDocsModel>>{
        return detailDataSource.getBorrowerDocuments(token = token , loanApplicationId = loanApplicationId)
    }

    suspend fun downloadFile(token:String , id:String, requestId:String, docId:String, fileId:String): Response<ResponseBody>? {
        val result = detailDataSource.downloadFile(token = token , id = id, requestId = requestId, docId = docId, fileId = fileId)
        if(result?.body() is ResponseBody) {
            val responseBody = result.body()
            val fileName= "testFileName.pdf"
            val pathWhereYouWantToSaveFile = applicationContext.filesDir.absolutePath+fileName
            val whatSaved = saveFile(responseBody , pathWhereYouWantToSaveFile)
            Log.e("file-save", whatSaved)

            //val file = File(Environment.getExternalStorageDirectory().absolutePath + "/" + filename)
            val savedFile = File(whatSaved)
            val target = Intent(Intent.ACTION_VIEW)

            val photoURI = FileProvider.getUriForFile(applicationContext, applicationContext.packageName + ".provider", savedFile)

            target.setDataAndType(photoURI, "application/pdf")
            target.flags = Intent.FLAG_ACTIVITY_NEW_TASK

            val intent = Intent.createChooser(target, "Open File")
            intent.flags = Intent.FLAG_ACTIVITY_NEW_TASK;
            try {
                startActivity(applicationContext, intent, null)
            } catch (e: ActivityNotFoundException) {
                // Instruct the user to install a PDF reader here, or something
            }

            /*
                val is: InputStream = response.body() as InputStream
                val testFile = response.body()
                val tempFileName = System.currentTimeMillis()
                testFile.byteStream()?.saveToFile("${tempFileName}.pdf")
                Log.e("file-", isFile.toString())
                 */

        }
        return  result
    }

    private fun InputStream.saveToFile(file: String) = use { input ->
        File(file).outputStream().use { output ->
            input.copyTo(output)
        }
    }

    fun saveFile(body: ResponseBody?, pathWhereYouWantToSaveFile: String):String{
        if (body==null)
            return ""
        var input: InputStream? = null
        try {
            input = body.byteStream()
            //val file = File(getCacheDir(), "cacheFileAppeal.srl")
            val fos = FileOutputStream(pathWhereYouWantToSaveFile)
            fos.use { output ->
                val buffer = ByteArray(4 * 1024) // or other buffer size
                var read: Int
                while (input.read(buffer).also { read = it } != -1) {
                    output.write(buffer, 0, read)
                }
                output.flush()
            }
            return pathWhereYouWantToSaveFile
        }catch (e:Exception){
            Log.e("saveFile",e.toString())
        }
        finally {
            input?.close()
        }
        return ""
    }

}