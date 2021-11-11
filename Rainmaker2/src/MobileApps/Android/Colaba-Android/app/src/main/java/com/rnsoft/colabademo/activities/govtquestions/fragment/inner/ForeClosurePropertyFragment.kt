package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.ForeClosurePropertyLayoutBinding

import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class ForeClosurePropertyFragment:BaseFragment() {

    private var _binding: ForeClosurePropertyLayoutBinding? = null
    private val binding get() = _binding!!
    private var updateGovernmentQuestionByBorrowerId = UpdateGovernmentQuestions()
    private var questionId:Int = 0


    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = ForeClosurePropertyLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        arguments?.let {
            questionId = it.getInt(AppConstant.questionId)
            updateGovernmentQuestionByBorrowerId = it.getParcelable(AppConstant.updateGovernmentQuestionByBorrowerId)!!
        }
        fillWithData()
        return root
    }

    private fun fillWithData(){
        for (item in updateGovernmentQuestionByBorrowerId.Questions) {
            if(item.id == questionId){
                item.answerDetail?.let {
                    binding.edDetails.setText(it)
                }
            }
        }
    }

    private val borrowerAppViewModel: BorrowerApplicationViewModel by activityViewModels()

    private fun setUpUI() {
        binding.backButton.setOnClickListener { findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener {
            updateGovernmentAndSaveData()


            /*
            val fieldsValidated = checkEmptyFields()
            if(fieldsValidated) {
                findNavController().popBackStack()
            }
             */
        }
    }

    private fun updateGovernmentAndSaveData(){
        for (item in updateGovernmentQuestionByBorrowerId.Questions) {
            if(item.id == questionId){
                item.answerDetail = binding.edDetails.text.toString()
            }
        }
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                borrowerAppViewModel.addOrUpdateGovernmentQuestions(authToken, updateGovernmentQuestionByBorrowerId)
                findNavController().popBackStack()
            }
        }


    }

    private fun checkEmptyFields():Boolean{
        var bool = true
        if(binding.edDetails.text?.isEmpty() == true || binding.edDetails.text?.isBlank() == true) {
            CustomMaterialFields.setError(binding.layoutDetail, "This field is required." , requireContext())
            bool = false
        }
        else
            CustomMaterialFields.clearError(binding.layoutDetail,  requireContext())

        return bool
    }

    private fun updateGovernmentData(testData:QuestionData){
        for (item in updateGovernmentQuestionByBorrowerId.Questions) {
            if(item.id == testData.id){
                item.answer = testData.answer
            }
        }
    }
}