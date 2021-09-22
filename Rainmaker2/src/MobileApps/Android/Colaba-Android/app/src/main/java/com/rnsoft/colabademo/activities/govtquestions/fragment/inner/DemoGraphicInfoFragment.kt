package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class DemoGraphicInfoFragment:Fragment() {

    //private var _binding: DemographicInfoLayoutBinding? = null
    //private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        //_binding = DemographicInfoLayoutBinding.inflate(inflater, container, false)
      //  val root: View = binding.root
        //setUpDemoGraphicScreen()
        return View(requireContext())
    }


    /*
    private fun setUpDemoGraphicScreen() {
        binding.asianCheckBox.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                binding.asianInnerConstraintLayout.visibility = View.VISIBLE
            }else{
                binding.asianInnerConstraintLayout.visibility = View.GONE
            }
        }

        binding.nativeHawaianOrOtherCheckBox.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                binding.nativeHawaianInnerLayout.visibility = View.VISIBLE
            }else{
                binding.nativeHawaianInnerLayout.visibility = View.GONE
            }
        }


        binding.hispanicOrLatino.setOnClickListener {
            binding.hispanicOrLatinoLayout.visibility = View.VISIBLE
            binding.notHispanic.isChecked = false
            binding.notTellingEthnicity.isChecked = false
        }

        binding.notHispanic.setOnClickListener{


            binding.hispanicOrLatinoLayout.visibility = View.GONE
            binding.hispanicOrLatino.isChecked = false
            binding.notTellingEthnicity.isChecked = false
        }

        binding.notTellingEthnicity.setOnClickListener{

            binding.hispanicOrLatinoLayout.visibility = View.GONE
            binding.hispanicOrLatino.isChecked = false
            binding.notHispanic.isChecked = false
        }

        binding.otherAsianCheckBox.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherAsianConstraintlayout.visibility = View.VISIBLE
            else
                binding.otherAsianConstraintlayout.visibility = View.GONE
        }

        binding.otherHispanicOrLatino.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherHispanicConstraintLayout.visibility = View.VISIBLE
            else
                binding.otherHispanicConstraintLayout.visibility = View.GONE
        }

        binding.otherPacificIslanderCheckBox.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherPacificIslanderConstraintLayout.visibility = View.VISIBLE
            else
                binding.otherPacificIslanderConstraintLayout.visibility = View.GONE

        }
    }
     */
}