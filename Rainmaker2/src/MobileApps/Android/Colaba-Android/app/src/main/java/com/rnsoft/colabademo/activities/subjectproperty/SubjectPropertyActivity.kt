package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import androidx.navigation.findNavController
import com.rnsoft.colabademo.databinding.BorrowerSubjectPropertyLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class SubjectPropertyActivity : BaseActivity() {
    @Inject
    lateinit var sharedPreferences : SharedPreferences
    private lateinit var binding : BorrowerSubjectPropertyLayoutBinding
    var purpose : String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = BorrowerSubjectPropertyLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)
        overridePendingTransition(R.anim.slide_in_right, R.anim.hold)


        val extras = intent.extras
        extras?.let {
            purpose = it.getString(AppConstant.borrowerPurpose)
        }

        val navController = findNavController(R.id.nav_host_borrower_subject_property)
        if(purpose.equals(AppConstant.purchase, ignoreCase = true))
            navController.navigate(R.id.nav_sub_property_purchase)
        else if(purpose.equals(AppConstant.refinance, ignoreCase = true)) {
            navController.navigate(R.id.nav_sub_property_refinance)
        }
    }
 }