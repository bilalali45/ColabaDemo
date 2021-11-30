package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.DebtCoLayoutBinding

class DebtCoFragment:GovtDetailBaseFragment() {

    private var _binding: DebtCoLayoutBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = DebtCoLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        arguments?.let {
            questionId = it.getInt(AppConstant.questionId)
            updateGovernmentQuestionByBorrowerId = it.getParcelable(AppConstant.addUpdateQuestionsParams)
        }
        activity?.onBackPressedDispatcher?.addCallback(viewLifecycleOwner, backToGovernmentScreen )
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