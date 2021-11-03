package com.rnsoft.colabademo


import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.*
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class ReserveFragment : BaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    //private val viewModel : PrimaryBorrowerViewModel by viewModels()
    private var _binding: ReserveLayoutBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = ReserveLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        super.addListeners(binding.root)

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.radioButtonYes.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                binding.radioButtonYes.setTypeface(null, Typeface.BOLD)
            else
                binding.radioButtonYes.setTypeface(null, Typeface.NORMAL)
        }

        binding.radioButton2No.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked)
                binding.radioButton2No.setTypeface(null, Typeface.BOLD)
            else
                binding.radioButton2No.setTypeface(null, Typeface.NORMAL)
        }

        setData()

        return root
    }

    private fun setData(){
        if(arguments != null){
            val reserve = arguments?.getBoolean(AppConstant.RESERVE_ACTIVATED)
            reserve.let {
                if(it == true)
                    binding.radioButtonYes.isChecked = true
                else
                    binding.radioButton2No.isChecked = true
            }
        }
    }

}