package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.HispanicLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class HispanicFragment:BaseFragment() {

    private var _binding: HispanicLayoutBinding? = null
    private val binding get() = _binding!!

    private var ethnicityChildList:ArrayList<EthnicityDetailDemoGraphic> = arrayListOf()

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = HispanicLayoutBinding.inflate(inflater, container, false)

        ethnicityChildList = arguments?.getParcelableArrayList(AppConstant.ethnicityChildList)!!

        for (item in ethnicityChildList){
            if(item.detailId == 1){
                binding.mexican.isChecked = true
            }
            else if(item.detailId == 2){
                binding.puertoRican.isChecked = true
            }
            else if(item.detailId == 3){
                binding.cuban.isChecked = true
            }
            else if(item.detailId == 4 && item.isOther==true){
                binding.otherHispanicOrLatino.performClick()
                item.otherEthnicity?.let{
                    binding.edDetails.setText(it)
                }
            }
        }

        val root: View = binding.root
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
        binding.otherHispanicOrLatino.setOnCheckedChangeListener{ buttonView, isChecked ->
            //if(isChecked) binding.layoutDetail.visibility = View.VISIBLE
            //else binding.layoutDetail.visibility = View.GONE
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