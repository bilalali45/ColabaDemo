package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.DetailTopLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.util.*
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
    }
}