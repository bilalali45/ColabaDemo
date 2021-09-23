package com.rnsoft.colabademo

import android.view.View
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.income.fragment.BottomDialogSelectEmployment

open class IncomeBaseFragment:BaseFragment() {
    fun getSampleIncome():ArrayList<IncomeModelClass>{
        val assetModelCell = IncomeModelClass( headerTitle = "Employment", headerAmount = "$5,000/mo" , footerTitle = "Add Employment",
            incomeContentCell = arrayListOf(
                IncomeContentCell("Google LLC", "Chief Executive Officer" ,"$5,000" ,"From Jun 2020",navigateToCurrentEmployment),
                IncomeContentCell("Disrupt", "Admin Manager", "$4,000" , "Dec 2019 to May 2020",navigateToPreviousEmployment)
            ), openBottomFragment)

        val assetModelCell2 = IncomeModelClass( headerTitle = "Self Employment / Independent Contractor", headerAmount = "$3,000/mo" , footerTitle = "Add Self Employment",
            incomeContentCell = arrayListOf(
                IncomeContentCell("Freelance", "Content Writer" ,"$3,000","From Dec 2020")
            ), navigateToSelfEmployment)

        val assetModelCell3 = IncomeModelClass( headerTitle = "Business" ,headerAmount = "$6,000/mo" , footerTitle = "Add Business",
            incomeContentCell = arrayListOf(
                IncomeContentCell("OPTP", "Director" ,"$6,000","From Jun 2020")
            ), navigateToBusinessIncome)


        val assetModelCell4 = IncomeModelClass( headerTitle = "Military Pay", headerAmount = "$2,000" , footerTitle = "Add Military Service",
            incomeContentCell = arrayListOf(
                IncomeContentCell("US Army", "Field 15 — Aviation" ,"$2,000","From Jun 2020")
            ),navigateToMilitaryPay)


        val assetModelCell5 = IncomeModelClass( headerTitle = "Retirement", headerAmount = "$1,200/mo" , footerTitle = "Add Retirement",
            incomeContentCell = arrayListOf(
                IncomeContentCell("Google LLC", "Pension" ,"$1,200")
            ) , navigateToRetirementIncome)


        val assetModelCell6 = IncomeModelClass( headerTitle = "Other", headerAmount = "$4,125/mo" , footerTitle = "Add Other Income",
            incomeContentCell = arrayListOf(
                IncomeContentCell("Alimony", "Family" ,"$4,125")
            ), navigateToOtherIncome)


        val modelArrayList:ArrayList<IncomeModelClass> = arrayListOf()
        modelArrayList.add(assetModelCell)
        modelArrayList.add(assetModelCell2)
        modelArrayList.add(assetModelCell3)
        modelArrayList.add(assetModelCell4)
        modelArrayList.add(assetModelCell5)
        modelArrayList.add(assetModelCell6)


        return modelArrayList

    }

    private val navigateToCurrentEmployment = View.OnClickListener { findNavController().navigate(R.id.navigation_income_current_employment) }
    private val navigateToPreviousEmployment = View.OnClickListener { findNavController().navigate(R.id.navigation_income_prev_employment) }
    private val navigateToSelfEmployment = View.OnClickListener { findNavController().navigate(R.id.navigation_selfEmployment) }
    private val navigateToBusinessIncome = View.OnClickListener { findNavController().navigate(R.id.navigation_business) }
    private val navigateToMilitaryPay = View.OnClickListener { findNavController().navigate(R.id.navigation_military_pay) }
    private val navigateToRetirementIncome = View.OnClickListener { findNavController().navigate(R.id.navigation_retirement) }
    private val navigateToOtherIncome = View.OnClickListener { findNavController().navigate(R.id.navigation_other) }

    private val openBottomFragment = View.OnClickListener {
        BottomDialogSelectEmployment.newInstance().show(childFragmentManager, BottomDialogSelectEmployment::class.java.canonicalName)
    }


}