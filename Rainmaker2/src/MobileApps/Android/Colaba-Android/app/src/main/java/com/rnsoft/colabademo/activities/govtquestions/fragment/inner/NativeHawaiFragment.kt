package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.NativeHawaiianLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class NativeHawaiFragment:BaseFragment() {

    private var _binding: NativeHawaiianLayoutBinding? = null
    private val binding get() = _binding!!

    private var nativeHawaiianChildList:ArrayList<DemoGraphicRaceDetail> = arrayListOf()

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = NativeHawaiianLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        nativeHawaiianChildList = arguments?.getParcelableArrayList(AppConstant.nativeHawaianChildList)!!
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI() {
        binding.edDetails.setOnFocusChangeListener(CustomFocusListenerForEditText( binding.edDetails , binding.layoutDetail , requireContext()))
        binding.backButton.setOnClickListener { findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener { findNavController().popBackStack() }
        binding.otherPacificIslanderCheckBox.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.layoutDetail.visibility = View.VISIBLE
            else
                binding.layoutDetail.visibility = View.GONE
        }
        // pre selection from the webservice...
        for(item in nativeHawaiianChildList){

            if(item.name == binding.nativeHawaiianCheckBox.text)
                binding.nativeHawaiianCheckBox.isChecked = true
            else
            if(item.name == binding.chineeseCheckbox.text)
                binding.chineeseCheckbox.isChecked = true
            else
            if(item.name == binding.samoanCheckBox.text)
                binding.samoanCheckBox.isChecked = true
            else
            if(item.name == binding.otherPacificIslanderCheckBox.text)
                binding.otherPacificIslanderCheckBox.isChecked = true

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