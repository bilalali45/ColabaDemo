package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.DetailBorrowerLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerApplicationFragment : Fragment()  {

    private var _binding: DetailBorrowerLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = DetailBorrowerLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        return root

    }

}