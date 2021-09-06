package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.net.Uri
import android.os.Bundle
import android.util.Log
import android.view.View
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.DetailTopLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class DetailActivity : BaseActivity() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: DetailTopLayoutBinding

    var loanApplicationId:Int? = null
    var borrowerFirstName:String? = null
    var borrowerLastName:String? = null
    var borrowerLoanPurpose:String? = null
    var borrowerCellNumber:String? = null
    var borrowerEmail:String? = null

    var innerScreenName:String? = null

    private val detailViewModel: DetailViewModel by viewModels()

    override fun onCreate(savedInstanceState: Bundle?) {

        val extras = intent.extras
        extras?.let {
            loanApplicationId = it.getInt(AppConstant.loanApplicationId)
            borrowerFirstName = it.getString(AppConstant.firstName)
            borrowerLastName = it.getString(AppConstant.lastName)
            borrowerLoanPurpose = it.getString(AppConstant.loanPurpose)
            borrowerCellNumber = it.getString(AppConstant.bPhoneNumber)
            borrowerEmail = it.getString(AppConstant.bEmail)
            innerScreenName = it.getString(AppConstant.innerScreenName)
            Log.e("Names- ", "$borrowerFirstName $borrowerLastName")
        }


        super.onCreate(savedInstanceState)
        binding = DetailTopLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.emailFab.setOnClickListener{
            if(borrowerEmail!=null) {
                val intent = Intent(Intent.ACTION_SEND)
                intent.type = "plain/text"
                intent.putExtra(Intent.EXTRA_EMAIL, arrayOf(it))
                intent.putExtra(Intent.EXTRA_SUBJECT, "subject")
                intent.putExtra(Intent.EXTRA_TEXT, "mail body")
                startActivity(Intent.createChooser(intent, ""))
            }
            else
                SandbarUtils.showRegular(this@DetailActivity, "Email not found...")
        }

        binding.messageFab.setOnClickListener{
           if(borrowerCellNumber!=null) {
                val smsIntent = Intent(Intent.ACTION_VIEW)
                smsIntent.data = Uri.parse("sms:")
                //smsIntent.type = "vnd.android-dir/mms-sms"
                smsIntent.putExtra("address", borrowerCellNumber)
                //smsIntent.putExtra("sms_body", "Colaba info message")
                startActivity(smsIntent)
            }
            else
               SandbarUtils.showRegular(this@DetailActivity, "Phone number not found...")
        }

        binding.phoneFab.setOnClickListener{
            if(borrowerCellNumber!=null) {
                val intent = Intent(Intent.ACTION_DIAL)
                intent.data = Uri.parse("tel:$it")
                startActivity(intent)
            }
            else
                SandbarUtils.showRegular(this@DetailActivity, "Phone number not found...")
        }

        observeForCallEmailMessage()
    }



    private fun observeForCallEmailMessage(){
        detailViewModel.borrowerOverviewModel.observe(this@DetailActivity, {  overviewModel->
            if(overviewModel!=null) {
                borrowerCellNumber = overviewModel.cellPhone
                borrowerEmail  = overviewModel.email
            }
            else
                Log.e("should-stop"," here....")

        })
    }

    fun hideFabIcons(){
        binding.emailFab.visibility = View.INVISIBLE
        binding.messageFab.visibility = View.INVISIBLE
        binding.phoneFab.visibility = View.INVISIBLE
    }

    fun showFabIcons(){
        binding.emailFab.visibility = View.VISIBLE
        binding.messageFab.visibility = View.VISIBLE
        binding.phoneFab.visibility = View.VISIBLE
    }

    fun checkIfUnreadFileOpened(){
        lifecycleScope.launchWhenStarted {
            loanApplicationId?.let { loanId->
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    detailViewModel.getBorrowerDocuments(token = authToken, loanApplicationId = loanId)
                }
            }
        }
    }

}