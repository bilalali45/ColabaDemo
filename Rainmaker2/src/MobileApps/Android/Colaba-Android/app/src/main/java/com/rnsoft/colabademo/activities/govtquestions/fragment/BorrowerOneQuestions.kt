package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater

import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatTextView
import androidx.constraintlayout.widget.ConstraintLayout
import com.rnsoft.colabademo.databinding.*
import timber.log.Timber


class BorrowerOneQuestions : GovtQuestionBaseFragment() {

    private lateinit var binding: BorrowerOneQuestionsLayoutBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = BorrowerOneQuestionsLayoutBinding.inflate(inflater, container, false)
        setupLayout()
        test = binding.root.findViewById<ConstraintLayout>(R.id.asian_inner_constraint_layout)
        return binding.root
    }

    private fun setupLayout(){
        setUpTabs()
    }

    private lateinit var test:ConstraintLayout

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
        horizontalTabArrayList.addAll(arrayListOf(binding.btnDebtCo,binding.btnOutstanding,
            binding.btnFederalDebt, binding.btnPartyTo, binding.btnOwnershipInterest,  binding.btnTitleConveyance , binding.btnPreForeclouser,
            binding.btnForeclosuredProperty, binding.btnBankruptcy ,  binding.btnChildSupport ,  binding.btnDemographicInfo,
            binding.btnUndisclosedBorrowed , binding.btnUndisclosedCredit, binding.btnUndisclosedMortgage ,  binding.btnPriorityLiens))





        //testHashMap.put(arrayListOf((binding.btnDebtCo, binding.debtCoContainer),()))
        testHashMap = hashMapOf((binding.btnDebtCo to binding.debtCoContainer),
            (binding.btnOutstanding to binding.outstandingJudgementContainer), (binding.btnFederalDebt to binding.federalDeptContainer),
            (binding.btnOwnershipInterest to binding.ownershipInterestContainer), (binding.btnUndisclosedMortgage to binding.undisclosedMortgageContainer),
            (binding.btnPartyTo to binding.partyToLawsuitContainer), (binding.btnTitleConveyance to binding.titleConveyanceContainer),
            (binding.btnDemographicInfo to binding.insideScrollConstraintLayout),
            (binding.btnPreForeclouser to binding.preForeclosureShortSaleContainer), (binding.btnBankruptcy  to binding.bankruptcyContainer),
            (binding.btnForeclosuredProperty to binding.foreclosuredPropertyContainer), (binding.btnChildSupport to binding.childSupportContainer),
            (binding.btnUndisclosedBorrowed to binding.undisclosedContainer), (binding.btnUndisclosedCredit to binding.undisclosedCreditContainer),
            (binding.btnPriorityLiens to binding.priorityLiensContainer)
        )

        setUpDemoGraphicScreen()
    }

    //private val testHashMap:HashMap = HashMap()
    var testHashMap = HashMap<AppCompatTextView, ConstraintLayout>(0)

    private val scrollerTab = object:View.OnClickListener{
        override fun onClick(p0: View) {
            if(p0 is AppCompatTextView){
                for(item in horizontalTabArrayList){
                    item.isActivated = false
                    item.setTextColor(resources.getColor(R.color.doc_filter_txt_color, requireActivity().theme))
                }
                p0.isActivated = true
                p0.setTextColor(resources.getColor(R.color.colaba_primary_color, requireActivity().theme))

                for ((key, value) in testHashMap) {
                    //println("$key = $value")
                    if(key == p0){
                        value.visibility = View.VISIBLE
                    }
                    else
                        value.visibility = View.GONE
                }

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
                    binding.btnDemographicInfo-> {
                        binding.asianInnerConstraintLayout.visibility = View.GONE
                        binding.nativeHawaianInnerLayout.visibility = View.GONE
                        binding.hispanicOrLatinoLayout.visibility = View.GONE
                        binding.otherAsianConstraintlayout.visibility = View.GONE
                        binding.otherHispanicConstraintLayout.visibility = View.GONE
                        binding.otherPacificIslanderConstraintLayout.visibility = View.GONE

                    }
                    binding.btnUndisclosedBorrowed-> {}
                    binding.btnUndisclosedCredit-> {}
                    binding.btnUndisclosedMortgage -> {}
                    binding.btnPriorityLiens-> {}

                    else ->{}
                }
            }
        }
    }

    private fun setUpDemoGraphicScreen() {

        binding.asianCheckBox.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {

                test.visibility = View.VISIBLE
                binding.asianInnerConstraintLayout.visibility = View.VISIBLE
                Timber.e("not accessible...")
                Timber.e("not accessible...")
            }else{
                binding.asianInnerConstraintLayout.visibility = View.GONE
            }
        }

        binding.nativeHawaianOrOtherCheckBox.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                binding.nativeHawaianInnerLayout.visibility = View.VISIBLE
            }else{
                binding.nativeHawaianInnerLayout.visibility = View.GONE
            }
        }


        binding.hispanicOrLatino.setOnClickListener {
            binding.hispanicOrLatinoLayout.visibility = View.VISIBLE
            binding.notHispanic.isChecked = false
            binding.notTellingEthnicity.isChecked = false
        }

        binding.notHispanic.setOnClickListener{


            binding.hispanicOrLatinoLayout.visibility = View.GONE
            binding.hispanicOrLatino.isChecked = false
            binding.notTellingEthnicity.isChecked = false
        }

        binding.notTellingEthnicity.setOnClickListener{

            binding.hispanicOrLatinoLayout.visibility = View.GONE
            binding.hispanicOrLatino.isChecked = false
            binding.notHispanic.isChecked = false
        }

        binding.otherAsianCheckBox.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherAsianConstraintlayout.visibility = View.VISIBLE
            else
                binding.otherAsianConstraintlayout.visibility = View.GONE
        }

        binding.otherHispanicOrLatino.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherHispanicConstraintLayout.visibility = View.VISIBLE
            else
                binding.otherHispanicConstraintLayout.visibility = View.GONE
        }

        binding.otherPacificIslanderCheckBox.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherPacificIslanderConstraintLayout.visibility = View.VISIBLE
            else
                binding.otherPacificIslanderConstraintLayout.visibility = View.GONE

        }
    }


}
