package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater

import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatCheckBox
import androidx.appcompat.widget.AppCompatRadioButton
import androidx.appcompat.widget.AppCompatTextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.iterator
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.*
import timber.log.Timber


class BorrowerTwoQuestions : GovtQuestionBaseFragment() {

    private lateinit var binding: BorrowerOneQuestionsLayoutBinding

    private var innerLayoutHashMap = HashMap<AppCompatTextView, ConstraintLayout>(0)
    private var openDetailBoxHashMap = HashMap<AppCompatRadioButton, ConstraintLayout>(0)
    private var closeDetailBoxHashMap = HashMap<AppCompatRadioButton, ConstraintLayout>(0)
    private lateinit var test:ConstraintLayout

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = BorrowerOneQuestionsLayoutBinding.inflate(inflater, container, false)
        setupLayout()
        test = binding.root.findViewById<ConstraintLayout>(R.id.asian_inner_constraint_layout)
        super.addListeners(binding.root)
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
        horizontalTabArrayList.addAll(arrayListOf(binding.btnDebtCo,binding.btnOutstanding,
            binding.btnFederalDebt, binding.btnPartyTo, binding.btnOwnershipInterest,  binding.btnTitleConveyance , binding.btnPreForeclouser,
            binding.btnForeclosuredProperty, binding.btnBankruptcy ,  binding.btnChildSupport ,  binding.btnDemographicInfo,
            binding.btnUndisclosedBorrowed , binding.btnUndisclosedCredit, binding.btnUndisclosedMortgage ,  binding.btnPriorityLiens))


        //testHashMap.put(arrayListOf((binding.btnDebtCo, binding.debtCoContainer),()))
        innerLayoutHashMap = hashMapOf((binding.btnDebtCo to binding.debtCoContainer),
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

        // add yes button listeners
        binding.debtCoYes.setOnClickListener(openDetailBox)
        binding.outstandingJudgementYes.setOnClickListener(openDetailBox)
        binding.federalDeptYes.setOnClickListener(openDetailBox)
        binding.partyToLawsuitYes.setOnClickListener(openDetailBox)
        binding.ownershipInterestYes.setOnClickListener(openDetailBox)
        binding.titleConveyanceYes.setOnClickListener(openDetailBox)
        binding.preForeclosureShortSaleYes.setOnClickListener(openDetailBox)
        binding.foreclosuredPropertyYes.setOnClickListener(openDetailBox)
        binding.bankruptcyYes.setOnClickListener(openDetailBox)
        binding.childSupportYes.setOnClickListener(openDetailBox)
        //binding.btnDemographicInfo.setOnClickListener(openDetailBox)
        binding.undisclosedYes.setOnClickListener(openDetailBox)
        binding.undisclosedCreditYes.setOnClickListener(openDetailBox)
        binding.undisclosedMortgageYes.setOnClickListener(openDetailBox)
        binding.priorityLiensYes.setOnClickListener(openDetailBox)

        openDetailBoxHashMap = hashMapOf((binding.debtCoYes to binding.debtCoDetailBox), (binding.childSupportYes to binding.childSupportDetailBox),
            (binding.partyToLawsuitYes to binding.partyToLawsuitDetailBox),
            (binding.ownershipInterestYes to binding.ownershipInterestDetailBox),
            (binding.outstandingJudgementYes to binding.outstandingJudgementDetailBox), (binding.federalDeptYes to binding.federalDeptDetailBox),

            (binding.titleConveyanceYes to binding.titleConveyanceDetailBox), (binding.preForeclosureShortSaleYes to binding.preForeclosureShortSaleDetailBox),
            (binding.foreclosuredPropertyYes to binding.foreclosuredPropertyDetailBox), (binding.bankruptcyYes  to binding.bankruptcyDetailBox),
            (binding.undisclosedYes to binding.undisclosedDetailBox), (binding.undisclosedCreditYes to binding.undisclosedCreditDetailBox),
            (binding.undisclosedMortgageYes to binding.undisclosedMortgageDetailBox), (binding.priorityLiensYes to binding.priorityLiensDetailBox)
        )


        binding.debtCoNo.setOnClickListener(closeDetailBox)
        binding.outstandingJudgementNo.setOnClickListener(closeDetailBox)
        binding.federalDeptNo.setOnClickListener(closeDetailBox)
        binding.partyToLawsuitNo.setOnClickListener(closeDetailBox)
        binding.ownershipInterestNo.setOnClickListener(closeDetailBox)
        binding.titleConveyanceNo.setOnClickListener(closeDetailBox)
        binding.preForeclosureShortSaleNo.setOnClickListener(closeDetailBox)
        binding.foreclosuredPropertyNo.setOnClickListener(closeDetailBox)
        binding.bankruptcyNo.setOnClickListener(closeDetailBox)
        binding.childSupportNo.setOnClickListener(closeDetailBox)
        //binding.btnDemographicInfo.setOnClickListener(closeDetailBox)
        binding.undisclosedNo.setOnClickListener(closeDetailBox)
        binding.undisclosedCreditNo.setOnClickListener(closeDetailBox)
        binding.undisclosedMortgageNo.setOnClickListener(closeDetailBox)
        binding.priorityLiensNo.setOnClickListener(closeDetailBox)

        closeDetailBoxHashMap = hashMapOf((binding.debtCoNo to binding.debtCoDetailBox), (binding.childSupportNo to binding.childSupportDetailBox),
            (binding.partyToLawsuitNo to binding.partyToLawsuitDetailBox),
            (binding.ownershipInterestNo to binding.ownershipInterestDetailBox),
            (binding.outstandingJudgementNo to binding.outstandingJudgementDetailBox), (binding.federalDeptNo to binding.federalDeptDetailBox),

            (binding.titleConveyanceNo to binding.titleConveyanceDetailBox), (binding.preForeclosureShortSaleNo to binding.preForeclosureShortSaleDetailBox),
            (binding.foreclosuredPropertyNo to binding.foreclosuredPropertyDetailBox), (binding.bankruptcyNo  to binding.bankruptcyDetailBox),
            (binding.undisclosedNo to binding.undisclosedDetailBox), (binding.undisclosedCreditNo to binding.undisclosedCreditDetailBox),
            (binding.undisclosedMortgageNo to binding.undisclosedMortgageDetailBox), (binding.priorityLiensNo to binding.priorityLiensDetailBox)
        )

        binding.btnUndisclosedBorrowed.performClick()

        binding.doNotWishCheckBox.setOnClickListener{
            binding.whiteCheckBox.isChecked = false
            binding.nativeHawaianOrOtherCheckBox.isChecked = false
            val items = binding.nativeHawaianInnerLayout
            for(item in items){
                if(item is AppCompatCheckBox){
                    item.isChecked = false
                }
            }
            binding.blackOrAfricanCheckBox.isChecked = false
            binding.asianCheckBox.isChecked = false
            val asianItems = binding.asianInnerConstraintLayout
            for(item in asianItems){
                if(item is AppCompatCheckBox){
                    item.isChecked = false
                }
            }
            binding.americanOrIndianCheckBox.isChecked = false
        }

        binding.whiteCheckBox.setOnClickListener{ binding.doNotWishCheckBox.isChecked = false }
        binding.nativeHawaianOrOtherCheckBox.setOnClickListener{ binding.doNotWishCheckBox.isChecked = false }
        binding.blackOrAfricanCheckBox.setOnClickListener{ binding.doNotWishCheckBox.isChecked = false }
        binding.asianCheckBox.setOnClickListener{ binding.doNotWishCheckBox.isChecked = false }
        binding.americanOrIndianCheckBox.setOnClickListener{ binding.doNotWishCheckBox.isChecked = false }




    }

    private val openDetailBox = object:View.OnClickListener{
        override fun onClick(p0: View) {
            openDetailBoxHashMap.getValue(p0 as AppCompatRadioButton).visibility = View.VISIBLE
            when(p0) {
                binding.childSupportYes -> {
                    findNavController().navigate(R.id.action_child_support)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_child_support)
                    }
                }
                binding.bankruptcyYes -> {
                    findNavController().navigate(R.id.action_bankruptcy)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_bankruptcy)
                    }
                }
                binding.priorityLiensYes -> {
                    findNavController().navigate(R.id.action_priority_liens)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_priority_liens)
                    }
                }
                binding.ownershipInterestYes -> {
                    findNavController().navigate(R.id.action_ownership_interest)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_ownership_interest)
                    }
                }
                binding.undisclosedYes -> {
                    findNavController().navigate(R.id.action_undisclosed_borrower_fund)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_undisclosed_borrower_fund)
                    }
                }
                binding.debtCoYes-> {
                    findNavController().navigate(R.id.action_debt_co)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_debt_co)
                    }
                }
                binding.outstandingJudgementYes-> {
                    findNavController().navigate(R.id.action_outstanding)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_outstanding)
                    }
                }
                binding.federalDeptYes-> {
                    findNavController().navigate(R.id.action_federal_debt)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_federal_debt)
                    }
                }
                binding.partyToLawsuitYes-> {
                    findNavController().navigate(R.id.action_party_to)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_party_to)
                    }
                }

                binding.titleConveyanceYes-> {
                    findNavController().navigate(R.id.action_title_conveyance)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_title_conveyance)
                    }
                }
                binding.preForeclosureShortSaleYes-> {
                    findNavController().navigate(R.id.action_pre_for_closure)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_pre_for_closure)
                    }
                }
                binding.foreclosuredPropertyYes-> {
                    findNavController().navigate(R.id.action_fore_closure_property)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_fore_closure_property)
                    }
                }

                binding.undisclosedCreditYes-> {
                    findNavController().navigate(R.id.action_undisclosed_credit)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_undisclosed_credit)
                    }
                }
                binding.undisclosedMortgageYes-> {
                    findNavController().navigate(R.id.action_undisclosed_mortgage)
                    openDetailBoxHashMap.getValue(p0).setOnClickListener{
                        findNavController().navigate(R.id.action_undisclosed_mortgage)
                    }
                }


                else -> {
                }
            }
        }
    }

    private val closeDetailBox = object:View.OnClickListener{
        override fun onClick(p0: View) {
            closeDetailBoxHashMap.getValue(p0 as AppCompatRadioButton).visibility = View.GONE
        }
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

                for ((key, value) in innerLayoutHashMap) {
                    if(key == p0)
                        value.visibility = View.VISIBLE
                    else
                        value.visibility = View.GONE
                }

                when(p0){
                    binding.btnDemographicInfo-> {
                        binding.asianInnerConstraintLayout.visibility = View.GONE
                        binding.nativeHawaianInnerLayout.visibility = View.GONE
                        binding.hispanicOrLatinoLayout.visibility = View.GONE

                    }
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


    }


}


    /*


    when(p0){

                binding.btnDemographicInfo-> {
                    binding.asianInnerConstraintLayout.visibility = View.GONE
                    binding.nativeHawaianInnerLayout.visibility = View.GONE
                    binding.hispanicOrLatinoLayout.visibility = View.GONE
                    binding.otherAsianConstraintlayout.visibility = View.GONE
                    binding.otherHispanicConstraintLayout.visibility = View.GONE
                    binding.otherPacificIslanderConstraintLayout.visibility = View.GONE
                }

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
                binding.btnUndisclosedBorrowed-> {}
                binding.btnUndisclosedCredit-> {}
                binding.btnUndisclosedMortgage -> {}
                binding.btnPriorityLiens-> {}
                else ->{}
            }

     */
