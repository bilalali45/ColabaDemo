package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.View
import android.view.inputmethod.InputMethodManager
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
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

        val navController = findNavController(R.id.nav_host_borrower_subject_property)
        navController.navigate(R.id.nav_sub_property_purchase)


        /*val extras = intent.extras
        extras?.let {
            purpose = it.getString(AppConstant.borrowerPurpose)
        }

        val navController = findNavController(R.id.nav_host_borrower_subject_property)
        if(purpose.equals(AppConstant.purchase, ignoreCase = true))
            navController.navigate(R.id.nav_sub_property)
        else if(purpose.equals(AppConstant.refinance, ignoreCase = true)) {
            navController.navigate(R.id.nav_sub_property_refinance)
        } */
    }
 }