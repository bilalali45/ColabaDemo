package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatTextView
import com.rnsoft.colabademo.databinding.*

class BorrowerOneQuestions : GovtQuestionBaseFragment() {

    private lateinit var binding: BorrowerOneQuestionsLayoutBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = BorrowerOneQuestionsLayoutBinding.inflate(inflater, container, false)
        setupLayout()
        return binding.root
    }

    private fun setupLayout(){
        setUpTabs()
    }

    private fun setUpTabs(){
        binding.btnDebtCo.setOnClickListener(scrollerTab)
        binding.btnOutstanding.setOnClickListener(scrollerTab)
        binding.btnFederalDebt.setOnClickListener(scrollerTab)
        binding.btnPartyTo.setOnClickListener(scrollerTab)
        binding.btnOwnershipInterest.setOnClickListener(scrollerTab)
        binding.btnTitleConveyance.setOnClickListener(scrollerTab)
        binding.btnPreForeclouser.setOnClickListener(scrollerTab)
        binding.btnForeclosuredProperty.setOnClickListener(scrollerTab)
        binding.btnBankruptcy.setOnClickListener(scrollerTab)
        binding.btnChildSupport.setOnClickListener(scrollerTab)
        binding.btnDemographicInfo.setOnClickListener(scrollerTab)
        binding.btnUndisclosedBorrowed.setOnClickListener(scrollerTab)
        binding.btnUndisclosedCredit.setOnClickListener(scrollerTab)
        binding.btnUndisclosedMortgage.setOnClickListener(scrollerTab)
        binding.btnPriorityLiens.setOnClickListener(scrollerTab)
        horizontalTabArrayList.addAll(arrayListOf<AppCompatTextView>(binding.btnDebtCo,binding.btnOutstanding,
            binding.btnFederalDebt, binding.btnPartyTo, binding.btnOwnershipInterest,  binding.btnTitleConveyance , binding.btnPreForeclouser,
            binding.btnForeclosuredProperty, binding.btnBankruptcy ,  binding.btnChildSupport ,  binding.btnDemographicInfo,
            binding.btnUndisclosedBorrowed , binding.btnUndisclosedCredit, binding.btnUndisclosedMortgage ,  binding.btnPriorityLiens))
    }

    private val scrollerTab = object:View.OnClickListener{
        override fun onClick(p0: View) {
            if(p0 is AppCompatTextView){
                for(item in horizontalTabArrayList){
                    item.isActivated = false
                    item.setTextColor(resources.getColor(R.color.doc_filter_txt_color, requireActivity().theme))
                }
                p0.isActivated = true
                p0.setTextColor(resources.getColor(R.color.colaba_primary_color, requireActivity().theme))

                when(p0){
                    binding.btnDebtCo ->{}
                    binding.btnOutstanding ->{}
                    binding.btnFederalDebt ->{}
                    binding.btnPartyTo-> {}
                    binding.btnOwnershipInterest ->  {}
                    binding.btnTitleConveyance -> {}
                    binding.btnPreForeclouser->{}
                    binding.btnForeclosuredProperty-> {}
                    binding.btnBankruptcy ->{}
                    binding.btnChildSupport -> {}
                    binding.btnDemographicInfo-> {}
                    binding.btnUndisclosedBorrowed-> {}
                    binding.btnUndisclosedCredit-> {}
                    binding.btnUndisclosedMortgage -> {}
                    binding.btnPriorityLiens-> {}

                    else ->{}
                }
            }
        }

    }



}
