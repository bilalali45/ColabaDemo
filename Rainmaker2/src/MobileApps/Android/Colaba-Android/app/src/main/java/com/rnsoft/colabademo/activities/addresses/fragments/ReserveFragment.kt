package com.rnsoft.colabademo


import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.*
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class ReserveFragment : BaseFragment() {

    private var _binding: ReserveLayoutBinding? = null
    private val binding get() = _binding!!
    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = ReserveLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        super.addListeners(binding.root)

        return root


    }

}