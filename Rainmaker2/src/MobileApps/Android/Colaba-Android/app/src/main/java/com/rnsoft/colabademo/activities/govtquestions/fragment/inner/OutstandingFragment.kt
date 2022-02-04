package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.govtquestions.fragment.AllGovQuestionsFragment
import com.rnsoft.colabademo.databinding.OutstandingLayoutBinding

class OutstandingFragment:GovtDetailBaseFragment() {

    private var _binding: OutstandingLayoutBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = OutstandingLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        arguments?.let {
            questionId = it.getInt(AppConstant.questionId)
            whichBorrowerId = it.getInt(AppConstant.whichBorrowerId)
            updateGovernmentQuestionByBorrowerId = it.getParcelable(AppConstant.addUpdateQuestionsParams)
            userName = it.getString(AppConstant.govtUserName)
        }
        binding.borrowerPurpose.text = userName
        activity?.onBackPressedDispatcher?.addCallback(viewLifecycleOwner, backToGovernmentScreen )
        fillWithData(binding.edDetails)
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI() {
        binding.backButton.setOnClickListener { findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener {
            AllGovQuestionsFragment.callservices = true

            if(AllGovQuestionsFragment.instan != null){
                AllGovQuestionsFragment.instan!!.setdata(
                    "Yes",
                    binding.edDetails.text.toString(),
                    whichBorrowerId,
                    "3",
                    questionId,
                    "1","Yes"
                )
            }
            updateGovernmentAndSaveData(binding.edDetails.text.toString())
        }
    }
}