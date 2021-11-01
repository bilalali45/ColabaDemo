package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.AsianLayoutBinding
import com.rnsoft.colabademo.databinding.FederalDeptLayoutBinding
import com.rnsoft.colabademo.databinding.PriorityLiensLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class AsianFragment:BaseFragment() {

    private var _binding: AsianLayoutBinding? = null
    private val binding get() = _binding!!

    private var asianChildList:ArrayList<DemoGraphicRaceDetail> = arrayListOf()

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = AsianLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        asianChildList = arguments?.getParcelableArrayList(AppConstant.asianChildList)!!
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI() {
        binding.edDetails.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.edDetails , binding.layoutDetail , requireContext()))
        binding.backButton.setOnClickListener { findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener {
           findNavController().popBackStack()
        }
        binding.otherAsianCheckBox.setOnCheckedChangeListener{ buttonView, isChecked ->
            // if(isChecked) binding.layoutDetail.visibility = View.VISIBLE
            // else binding.layoutDetail.visibility = View.GONE
        }
        // pre selection from the webservice...
        for(item in asianChildList){
            if(item.name == binding.asianIndianCheckBox.text)
                binding.asianIndianCheckBox.isChecked = true
            else
            if(item.name == binding.chineseCheckBox.text)
                binding.chineseCheckBox.isChecked = true
            else
            if(item.name == binding.filipinoCheckBox.text)
                binding.filipinoCheckBox.isChecked = true
            else
            if(item.name == binding.japaneseCheckBox.text)
                binding.japaneseCheckBox.isChecked = true
            else
            if(item.name == binding.koreanCheckBox.text)
                binding.koreanCheckBox.isChecked = true
            else
            if(item.name == binding.vietnameseCheckBox.text)
                binding.vietnameseCheckBox.isChecked = true
            else
            if(item.name == binding.otherAsianCheckBox.text) {
                binding.otherAsianCheckBox.isChecked = true
                if(item.isOther == true){
                    item.otherRace?.let { otherRace->
                        binding.edDetails.setText(otherRace)
                    }
                }
            }
        }
    }

    private fun checkEmptyFields():Boolean{
        var bool = true
        if(binding.edDetails.text?.isEmpty() == true || binding.edDetails.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.layoutDetail, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.layoutDetail,  requireContext())

        return bool
    }
}