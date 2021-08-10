package com.rnsoft.colabademo


import android.Manifest
import android.app.Activity
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.content.pm.PackageManager
import android.net.Uri
import android.os.Build
import android.os.Bundle
import android.os.Environment
import android.util.Log
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.core.content.ContextCompat.startActivity
import androidx.navigation.Navigation
import androidx.navigation.fragment.findNavController
import com.google.gson.Gson
import dagger.hilt.android.qualifiers.ApplicationContext
import okhttp3.ResponseBody
import org.apache.commons.io.IOUtils
import retrofit2.Response
import java.io.*
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

    suspend fun getBorrowerApplicationTabData(token:String ,loanApplicationId:Int):Result<BorrowerApplicationTabModel>{
        return detailDataSource.getBorrowerApplicationTabData(token = token , loanApplicationId = loanApplicationId)
    }

     suspend fun downloadFile(token:String , id:String, requestId:String, docId:String, fileId:String , fileName:String): Boolean {
        val result = detailDataSource.downloadFile(token = token , id = id, requestId = requestId, docId = docId, fileId = fileId )
         var bool = false
         Log.e("fileName", "= $fileName")
         if(result?.body() is ResponseBody) {
             val responseBody = result.body()
             try {
                 //you can now get your file in the InputStream
                 val isRead: InputStream? = responseBody?.byteStream()
                 Log.e("file lenght", ""+responseBody?.contentLength())
                 isRead?.let { isRead ->
                     val buffer = ByteArrayOutputStream()
                     var nRead: Int
                     val data = ByteArray(16384)
                     while (isRead.read(data, 0, data.size).also { nRead = it } != -1) {
                         buffer.write(data, 0, nRead)
                     }

                     bool = saveFileToExternalStorage(buffer.toByteArray() , fileName)
                     Log.e("bool", "= $bool")
                 }
             } catch (e: Exception) {
                 Log.e("Exception", " can not save PDF file...")
             }
         }
         Log.e("File", " is file created??$bool")

        return  bool
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












    private fun savaTextFileForTesting(){
        val path: File = applicationContext.cacheDir
        Log.e("path", " is "+ path)
        val file = File(path, "irfan.txt")
        val stream = FileOutputStream(file)
        stream.use { stream ->
            stream.write("text-to-write".toByteArray())
        }
    }




    private fun someOtherTest(result:Response<ResponseBody>){
        if(result?.body() is ResponseBody) {
            val responseBody = result.body()
            try {
                val path = Environment.getExternalStorageDirectory()
                val file = File(path, "file_name.jpg")
                val fileOutputStream = FileOutputStream(file)
                IOUtils.write(responseBody?.bytes(), fileOutputStream)
                Log.e("file--", "written on storage...")
            } catch (ex: java.lang.Exception) {
                Log.e("Bingo--","can not write....")
            }

            try {
                //you can now get your file in the InputStream
                val isRead: InputStream? = responseBody?.byteStream()

                //isRead?.toFile(Environment.DIRECTORY_DCIM)

                isRead?.let { isRead->

                    val buffer = ByteArrayOutputStream()

                    var nRead: Int

                    val data = ByteArray(16384)

                    while (isRead.read(data, 0, data.size).also { nRead = it } != -1) {
                        buffer.write(data, 0, nRead)
                    }

                    val filesaved = saveFileToExternalStorage(buffer.toByteArray() , "test")



                    saveFileNow(responseBody,"/data/user/0/com.rnsoft.colabademo/bingo.pdf")
                }
                val testFile =   File(ContextCompat.getExternalFilesDirs(applicationContext,null).toString() + File.separator + "testing.png")

                if(isRead!=null)
                    testFile.copyInputStreamToFile(isRead)

                //if (isRead != null) { copyStreamToFile(isRead) }

            } catch (e: IOException) {
                e.printStackTrace()
            }


            val fileName= "testFileName.pdf"
            val pathWhereYouWantToSaveFile = applicationContext.filesDir.absolutePath+fileName
            val whatSaved = saveFile(responseBody, pathWhereYouWantToSaveFile)
            Log.e("file-save", whatSaved)

            val writtenToDisk = responseBody?.let {
                writeResponseBodyToDisk(it)
            }
            Log.d("File download was a success? ", writtenToDisk.toString())


            /*
            val localUri = FileProvider.getUriForFile(applicationContext, applicationContext.packageName + ".provider", fileName)
            val i = Intent(Intent.ACTION_VIEW)
            i.flags = Intent.FLAG_ACTIVITY_NEW_TASK
            i.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION)
            i.setDataAndType(localUri, applicationContext.getContentResolver().getType(localUri))
            applicationContext.startActivity(i)

             */

            //val file = File(Environment.getExternalStorageDirectory().absolutePath + "/" + filename)
            /*val savedFile = File(whatSaved)
            val target = Intent(Intent.ACTION_VIEW)

            val photoURI = FileProvider.getUriForFile(applicationContext, applicationContext.packageName + ".provider", savedFile)

            target.setDataAndType(photoURI, "application/pdf")
            target.flags = Intent.FLAG_ACTIVITY_NEW_TASK

            val intent = Intent.createChooser(target, "Open File")
            intent.flags = Intent.FLAG_ACTIVITY_NEW_TASK
            try {
                startActivity(applicationContext, intent, null)
            } catch (e: ActivityNotFoundException) {
                // Instruct the user to install a PDF reader here, or something
            } */

            /*val savedFile = File(whatSaved)
            val localUri = FileProvider.getUriForFile(applicationContext, applicationContext.packageName + ".provider", savedFile)
            val i = Intent(Intent.ACTION_VIEW)
            i.flags = Intent.FLAG_ACTIVITY_NEW_TASK
            i.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION)
            i.setDataAndType(localUri, applicationContext.getContentResolver().getType(localUri))
            applicationContext.startActivity(i) */

            /*
            val intent = Intent(applicationContext,DocViewerActivity::class.java)
            intent.flags = Intent.FLAG_ACTIVITY_NEW_TASK
            intent.putExtra("file",whatSaved)
            applicationContext.startActivity(intent) */

        }
    }


    private val REQUEST_PERMISSIONS_CODE_WRITE_STORAGE = 20



    private fun openSavedPDf(file: File) {
        // Method - 1
        val path = Uri.fromFile(file)
        val go = Intent(Intent.ACTION_VIEW)
        go.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION)
        go.setDataAndType(path, "application/pdf")
        go.flags = Intent.FLAG_ACTIVITY_CLEAR_TOP
        applicationContext.startActivity(go)


        // Method - 2
        val intent: Intent = Intent(Intent.ACTION_VIEW).apply {
            setDataAndType(Uri.fromFile(file), "application/pdf")
            flags = Intent.FLAG_ACTIVITY_NEW_TASK

        }
        applicationContext.startActivity(intent)

    }

    fun saveFileNow(body: ResponseBody?, pathWhereYouWantToSaveFile: String):String{
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

    fun File.copyInputStreamToFile(inputStream: InputStream) {
        this.outputStream().use { fileOut ->
            inputStream.copyTo(fileOut)
        }
    }


    fun copyStreamToFile(inputStream: InputStream) {
       // val storeDirectory = this.getExternalFilesDir(Environment.DIRECTORY_DCIM) // DCIM folder

        val outputFile =   File(ContextCompat.getExternalFilesDirs(applicationContext,null).toString() + File.separator + "testing.png")
            //File(storeDirectory, "testing.png")
        inputStream.use { input ->
            val outputStream = outputFile as FileOutputStream
            outputStream.use { output ->
                val buffer = ByteArray(4 * 1024) // buffer size
                while (true) {
                    val byteCount = input.read(buffer)
                    if (byteCount < 0) break
                    output.write(buffer, 0, byteCount)
                }
                Log.e("output--", output.toString())
                output.flush()
            }
        }
    }

    fun InputStream.toFile(path: String) {
        File(path).outputStream().use { this.copyTo(it) }
    }


    private fun writeResponseBodyToDisk(body: ResponseBody): Boolean {
        return try {
            // todo change the file location/name according to your needs
            val futureStudioIconFile: File =
                File(ContextCompat.getExternalFilesDirs(applicationContext,null).toString() + File.separator + "Future Studio Icon.png")
            var inputStream: InputStream? = null
            var outputStream: OutputStream? = null
            try {
                val fileReader = ByteArray(4096)
                val fileSize = body.contentLength()
                var fileSizeDownloaded: Long = 0
                inputStream = body.byteStream()
                outputStream = FileOutputStream(futureStudioIconFile)
                while (true) {
                    val read = inputStream.read(fileReader)
                    if (read == -1) {
                        break
                    }
                    outputStream.write(fileReader, 0, read)
                    fileSizeDownloaded += read.toLong()
                    Log.d("File Download: ", "$fileSizeDownloaded of $fileSize")
                }
                outputStream.flush()
                true
            } catch (e: IOException) {
                false
            } finally {
                inputStream?.close()
                if (outputStream != null) {
                    outputStream.close()
                }
            }
        } catch (e: IOException) {
            false
        }
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