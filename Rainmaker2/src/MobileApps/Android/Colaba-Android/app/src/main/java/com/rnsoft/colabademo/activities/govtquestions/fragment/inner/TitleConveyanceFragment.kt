package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.TitleConveyanceLayoutBinding

class TitleConveyanceFragment:GovtDetailBaseFragment() {

    private var _binding: TitleConveyanceLayoutBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = TitleConveyanceLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        arguments?.let {
            questionId = it.getInt(AppConstant.questionId)
            updateGovernmentQuestionByBorrowerId = it.getParcelable(AppConstant.addUpdateQuestionsParams)
        }
        fillWithData(binding.edDetails)
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI() {
        binding.backButton.setOnClickListener { findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener {
            updateGovernmentAndSaveData(binding.edDetails.text.toString())
        }
    }
}