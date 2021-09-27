package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.BankruptcyLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class BankruptcyFragment:BaseFragment() {

    private var _binding: BankruptcyLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = BankruptcyLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI() {
        binding.backButton.setOnClickListener{ findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener {
            findNavController().popBackStack()
        }
    }
}