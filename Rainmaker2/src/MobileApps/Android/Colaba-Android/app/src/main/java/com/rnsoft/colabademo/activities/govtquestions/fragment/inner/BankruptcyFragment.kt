package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.OnBackPressedCallback
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.govtquestions.fragment.AllGovQuestionsFragment
import com.rnsoft.colabademo.activities.govtquestions.fragment.AllGovQuestionsFragment.Companion.banMap
import com.rnsoft.colabademo.databinding.BankruptcyLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import javax.inject.Inject

@AndroidEntryPoint
class BankruptcyFragment:BaseFragment() {
    protected var questionId:Int = 0

    private var _binding: BankruptcyLayoutBinding? = null
    private val binding get() = _binding!!
    private  var answerData:BankruptcyAnswerData = BankruptcyAnswerData()
    private var userName:String? = null
    private var whichBorrowerId:Int = 0
    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = BankruptcyLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        banMap = hashMapOf<String, String>()
        setUpUI()
        super.addListeners(binding.root)
        arguments?.let { arguments->
            questionId = arguments.getInt(AppConstant.questionId)
            answerData = arguments.getParcelable(AppConstant.bankruptcyAnswerData)!!
            userName = arguments.getString(AppConstant.govtUserName)
            whichBorrowerId = arguments.getInt(AppConstant.whichBorrowerId)
        }
        binding.borrowerPurpose.text = userName
        activity?.onBackPressedDispatcher?.addCallback(viewLifecycleOwner, backToGovernmentScreen )

        binding.chapter7.setOnClickListener{
            binding.errorField.visibility = View.INVISIBLE
        }
        binding.chapter11.setOnClickListener{
            binding.errorField.visibility = View.INVISIBLE
        }
        binding.chapter12.setOnClickListener{
            binding.errorField.visibility = View.INVISIBLE
        }
        binding.chapter13.setOnClickListener{
            binding.errorField.visibility = View.INVISIBLE
        }

        fillGlobalData()
        return root
    }

    private val backToGovernmentScreen: OnBackPressedCallback = object : OnBackPressedCallback(true) {
        override fun handleOnBackPressed() {
            findNavController().popBackStack()
        }
    }

    private fun fillGlobalData(){
        if(answerData.`1`)
            binding.chapter7.isChecked = true
        if(answerData.`2`)
            binding.chapter11.isChecked = true
        if(answerData.`3`)
            binding.chapter12.isChecked = true
        if(answerData.`4`)
            binding.chapter13.isChecked = true

        binding.edDetails.setText(answerData.extraDetail)
    }

    private fun setUpUI() {
        binding.backButton.setOnClickListener{ findNavController().popBackStack() }
        binding.saveBtn.setOnClickListener {
            val selectedValues = returnSelectedValues()
            if(selectedValues.isNotBlank() && selectedValues.isNotEmpty()) {
                answerData.extraDetail = binding.edDetails.text.toString()

                if(AllGovQuestionsFragment.instan != null){
                    AllGovQuestionsFragment.instan!!.setdata(
                        "Yes",
                        selectedValues,
                        whichBorrowerId,
                        "9",
                        questionId,
                        "1"
                    )
                }

                EventBus.getDefault().post(BankruptcyUpdateEvent(detailDescription = selectedValues, bankruptcyAnswerData = answerData , whichBorrowerId = whichBorrowerId ))
                findNavController().popBackStack()
            }
            else
                binding.errorField.visibility = View.VISIBLE

        }
    }

    private fun returnSelectedValues():String{
        var bool = false

        answerData.`1` = false
        answerData.`2` = false
        answerData.`3` = false
        answerData.`4` = false

        var displayedString = ""
        if(binding.chapter7.isChecked) {
            bool = true
            answerData.`1` = true
            displayedString = "Chapter 7,"
            banMap.put("1", "Chapter 7")

        }
        if(binding.chapter11.isChecked) {
            bool = true
            answerData.`2` = true
            displayedString = "$displayedString Chapter 11,"
            banMap.put("2", "Chapter 11")

        }
        if(binding.chapter12.isChecked) {
            bool = true
            answerData.`3` = true
            displayedString = "$displayedString Chapter 12,"
            banMap.put("3", "Chapter 12")

        }
        if(binding.chapter13.isChecked) {
            bool = true
            answerData.`4` = true
            displayedString = "$displayedString Chapter 13"
            banMap.put("4", "Chapter 13")

        }
        return displayedString
    }
}