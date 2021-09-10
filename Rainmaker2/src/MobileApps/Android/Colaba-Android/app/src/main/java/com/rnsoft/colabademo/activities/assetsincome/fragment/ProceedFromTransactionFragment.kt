package com.rnsoft.colabademo
import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.ProceedFromTransLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


    @AndroidEntryPoint
    class ProceedFromTransactionFragment : Fragment() {

        private var _binding: ProceedFromTransLayoutBinding? = null
        private val binding get() = _binding!!

        @Inject
        lateinit var sharedPreferences: SharedPreferences
        override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
        ): View {

            _binding = ProceedFromTransLayoutBinding.inflate(inflater, container, false)
            val root: View = binding.root

            return root
        }
    }