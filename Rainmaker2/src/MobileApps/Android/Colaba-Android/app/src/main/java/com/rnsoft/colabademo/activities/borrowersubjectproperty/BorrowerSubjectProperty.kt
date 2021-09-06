package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import com.rnsoft.colabademo.databinding.BorrowerSubjectPropertyLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class BorrowerSubjectProperty : BaseActivity() {
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: BorrowerSubjectPropertyLayoutBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = BorrowerSubjectPropertyLayoutBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val navController = findNavController(R.id.nav_host_borrower_subject_property)
        val appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.navigation_primary_borrower_info,
                R.id.navigation_current_address
            )
        )
    }

    }