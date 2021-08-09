package com.rnsoft.colabademo.activities.dashboard

import android.graphics.Bitmap
import android.os.Bundle
import android.util.Log
import android.webkit.WebSettings
import android.webkit.WebView
import android.webkit.WebViewClient
import androidx.appcompat.app.AppCompatActivity
import com.rnsoft.colabademo.R


class DocViewerActivity : AppCompatActivity() {
    lateinit var webView : WebView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_doc_viewer)

        Log.e("Activity", "onCreate")

        /*
        val selectedFile = "/data/data/com.rnsoft.colabademo/filestestFileName.pdf"
        Log.e("file", "$selectedFile")
        webView = findViewById(R.id.webview)
        webView.getSettings().setJavaScriptEnabled(true);

        val pdf = "https://www.entnet.org/wp-content/uploads/2021/04/Instructions-for-Adding-Your-Logo-2.pdf"
        webView.loadUrl("https://www.entnet.org/wp-content/uploads/2021/04/Instructions-for-Adding-Your-Logo-2.pdf")
        */
        webView = findViewById(R.id.webview)
        webView.settings.javaScriptEnabled = true
        webView.webViewClient = HelloWebViewClient()
        webView.loadUrl(
            "https://www.entnet.org/wp-content/uploads/2021/04/Instructions-for-Adding-Your-Logo-2.pdf"
        )

    }


    private inner class HelloWebViewClient : WebViewClient() {

        override fun shouldOverrideUrlLoading(view: WebView, url: String): Boolean {
            return true
        }

        override fun onPageStarted(view: WebView?, url: String?, favicon: Bitmap?) {
            super.onPageStarted(view, url, favicon)
        }
        override fun onPageFinished(view: WebView, url: String) {
            // TODO Auto-generated method stub
            super.onPageFinished(view, url)
        }

    }



   /* private class Callback : WebViewClient() {
        override fun shouldOverrideUrlLoading(
            view: WebView, url: String
        ): Boolean {
            return false
        }
    } */


        /*val savedFile = File(selectedFile)
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
    } */

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

