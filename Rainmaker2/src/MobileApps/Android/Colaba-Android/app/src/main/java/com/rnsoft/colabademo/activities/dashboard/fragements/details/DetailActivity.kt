package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.net.Uri
import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import com.rnsoft.colabademo.databinding.DetailTopLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class DetailActivity : AppCompatActivity() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: DetailTopLayoutBinding

    var loanApplicationId:Int? = null
    var borrowerFirstName:String? = null
    var borrowerLastName:String? = null
    var borrowerLoanPurpose:String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = DetailTopLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            borrowerFirstName = it.getString(AppConstant.firstName)
            borrowerLastName = it.getString(AppConstant.lastName)
            borrowerLoanPurpose = it.getString(AppConstant.loanPurpose)
            Log.e("Names- ", "$borrowerFirstName $borrowerLastName")
        }

        binding.emailFab.setOnClickListener{
            val intent = Intent(Intent.ACTION_SEND)
            intent.type = "plain/text"
            intent.putExtra(Intent.EXTRA_EMAIL, arrayOf("info@colaba.com"))
            intent.putExtra(Intent.EXTRA_SUBJECT, "subject")
            intent.putExtra(Intent.EXTRA_TEXT, "mail body")
            startActivity(Intent.createChooser(intent, ""))
        }

        binding.messageFab.setOnClickListener{
            val smsIntent = Intent(Intent.ACTION_VIEW)
            smsIntent.type = "vnd.android-dir/mms-sms"
            smsIntent.putExtra("address", "450-450-8548")
            smsIntent.putExtra("sms_body", "Colaba info message")
            startActivity(smsIntent)
        }

        binding.phoneFab.setOnClickListener{
            val intent = Intent(Intent.ACTION_DIAL)
            intent.data = Uri.parse("tel:4504508548")
            startActivity(intent)
        }
    }
}