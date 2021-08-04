package com.rnsoft.colabademo.activities.dashboard

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.core.content.FileProvider
import com.rnsoft.colabademo.R
import java.io.File


class DocViewerActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_doc_viewer)

        val selectedFile = "/data/data/com.rnsoft.colabademo/filestestFileName.pdf"
        //intent.getStringExtra("file")
        Log.e("file", "$selectedFile")


        val savedFile = File(selectedFile)
        val localUri = FileProvider.getUriForFile(
            applicationContext,
            applicationContext.packageName + ".provider",
            savedFile
        )

        val i = Intent(Intent.ACTION_VIEW)
        i.flags = Intent.FLAG_ACTIVITY_NEW_TASK
        i.addFlags(Intent.FLAG_GRANT_PERSISTABLE_URI_PERMISSION)
        i.setDataAndType(localUri,"application/pdf")
        applicationContext.startActivity(i)
    }

}

/* shows blank screen
val file =
    File(Environment.getExternalStorageDirectory().absoluteFile.toString() + "/filestestFileName.pdf")
if (file.exists()) {
    val intent = Intent(Intent.ACTION_VIEW)
    val uri = Uri.fromFile(file)
    intent.setDataAndType(uri, "application/pdf")
    intent.flags = Intent.FLAG_ACTIVITY_CLEAR_TOP
    try {
        startActivity(intent)
    } catch (e: ActivityNotFoundException) {
        Toast.makeText(
            this,
            "No Application available to view pdf",
            Toast.LENGTH_LONG
        ).show()
    }
}
}


} */


/*val pdfFile = File(getExternalFilesDir("/data/data/com.rnsoft.colabademo/filestestFileName.pdf"),"filestestFileName.pdf" )
val file =  File("filestestFileName.pdf")
val path =
    FileProvider.getUriForFile(this, BuildConfig.APPLICATION_ID +".provider", file)
Log.e("create pdf uri path==>", "" + path)

try {
    val intent = Intent(Intent.ACTION_VIEW)
    intent.setDataAndType(path, "application/pdf")
    intent.flags = Intent.FLAG_ACTIVITY_CLEAR_TOP
    //intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION)
    startActivity(intent)
    finish()
} catch (e: ActivityNotFoundException) {
    Toast.makeText(
        applicationContext,
        "There is no any PDF Viewer",
        Toast.LENGTH_SHORT
    ).show()
    finish()
}

} */

