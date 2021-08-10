package com.rnsoft.colabademo

import android.Manifest
import android.content.SharedPreferences
import android.content.pm.PackageManager
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import com.pdfview.PDFView
import com.rnsoft.colabademo.databinding.PdfViewLayoutBinding
import java.io.File
import javax.inject.Inject

class PdfViewFragment : Fragment(), AdapterClickListener {
    private var _binding: PdfViewLayoutBinding? = null
    private val binding get() = _binding!!

    private lateinit var pdfFileName:String
    lateinit var pdfView: PDFView


    private val requiredPermissionList = arrayOf(
        Manifest.permission.WRITE_EXTERNAL_STORAGE,
        Manifest.permission.READ_EXTERNAL_STORAGE
    )
    private val PERMISSION_CODE = 4040

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = PdfViewLayoutBinding.inflate(inflater, container, false)
        val view: View = binding.root

        pdfView = view.findViewById(R.id.dmitry_pdf)


        //if (checkAndRequestPermission())
            launchPdf()

        //lifecycleScope.launchWhenStarted {}

        return view
    }

    override fun navigateTo(position: Int) {

    }
    override fun getCardIndex(position: Int) {

    }

    private fun checkAndRequestPermission(): Boolean {
        val permissionsNeeded = ArrayList<String>()

        for (permission in requiredPermissionList) {
            if (ContextCompat.checkSelfPermission(requireContext(), permission) !=
                PackageManager.PERMISSION_GRANTED
            ) {
                permissionsNeeded.add(permission)
            }
        }

        if (permissionsNeeded.isNotEmpty()) {
            ActivityCompat.requestPermissions(
                requireActivity(),
                permissionsNeeded.toTypedArray(),
                PERMISSION_CODE
            )
            return false
        }

        return true
    }

    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<out String>,
        grantResults: IntArray
    ) {
        when (requestCode) {
            PERMISSION_CODE -> if (grantResults.isNotEmpty()) {
                val readPermission = grantResults[0] == PackageManager.PERMISSION_GRANTED
                val writePermission = grantResults[1] == PackageManager.PERMISSION_GRANTED
                if (readPermission && writePermission)
                    launchPdf()
                else {
                    Toast.makeText(requireContext(), " Permission Denied", Toast.LENGTH_SHORT).show()
                }
            }
        }
    }

    private fun launchPdf(){
        pdfFileName = arguments?.getString(AppConstant.pdfFileName).toString()
        val file = File(requireContext().filesDir, pdfFileName )
        pdfView.fromFile(file).show()
    }

}