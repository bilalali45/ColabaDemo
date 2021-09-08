package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.MixedUsePropertyBinding
import com.rnsoft.colabademo.databinding.SubjectPropertyBinding

/**
 * Created by Anita Kiran on 9/8/2021.
 */
class MixedUsePropertyFragment : Fragment() {


    lateinit var binding : MixedUsePropertyBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = MixedUsePropertyBinding.inflate(inflater, container, false)

        binding.backButton.setOnClickListener {
            requireActivity().onBackPressed()
        }


        return binding.root

    }


}