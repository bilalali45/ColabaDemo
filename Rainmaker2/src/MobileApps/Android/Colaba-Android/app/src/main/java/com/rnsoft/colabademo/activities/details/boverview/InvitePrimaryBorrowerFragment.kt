package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.InvitePrimaryBorrowerLayoutBinding


import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class InvitePrimaryBorrowerFragment : BaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: InvitePrimaryBorrowerLayoutBinding

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = InvitePrimaryBorrowerLayoutBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)

        (activity as DetailActivity).hideFabIcons()

        binding.backButtonImageView.setOnClickListener{
           findNavController().popBackStack()
        }

        return binding.root
    }


}