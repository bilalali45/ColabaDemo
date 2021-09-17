package com.rnsoft.colabademo.activities.govtquestions.fragment

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.PriorityLiensLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class PriorityLiensFragment:Fragment() {

    private var _binding: PriorityLiensLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = PriorityLiensLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        return root
    }

    private fun setUpUI() {

    }
}