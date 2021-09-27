package com.rnsoft.colabademo

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.addCallback
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.MixedUsePropertyBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields

/**
 * Created by Anita Kiran on 9/8/2021.
 */
class MixedUsePropertyFragment : BaseFragment() {

    lateinit var binding : MixedUsePropertyBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = MixedUsePropertyBinding.inflate(inflater, container, false)

        binding.backButton.setOnClickListener {
          //  findNavController().popBackStack(R.id.action_back_from_mixedproperty_toPurchase,false)
            requireActivity().onBackPressed()
        }

        /*requireActivity().onBackPressedDispatcher.addCallback {
            findNavController().popBackStack(R.id.action_back_from_mixedproperty_toPurchase,true)
        } */


        binding.btnSave.setOnClickListener {
            val details: String = binding.edDetails.text.toString()
            if (details.isEmpty() || details.length == 0) {
                CustomMaterialFields.setError(binding.layoutDetail,getString(R.string.error_field_required),requireActivity())
            } else{
                requireActivity().onBackPressed()
            }
        }
        binding.edDetails.doAfterTextChanged {
            it.let {
                CustomMaterialFields.clearError(binding.layoutDetail,requireActivity())
            }
        }
        super.addListeners(binding.root)
        return binding.root

    }


}