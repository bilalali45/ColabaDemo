package com.rnsoft.colabademo

import android.view.View
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController

open class IncomeBaseFragment:Fragment() {
    fun getSampleIncome():ArrayList<IncomeModelClass>{
        val assetModelCell = IncomeModelClass( headerTitle = "Employment", headerAmount = "$5,000/mo" , footerTitle = "Add Employment",
            incomeContentCell = arrayListOf(
                IncomeContentCell("Google LLC", "Chief Executive Officer" ,"$5,000" ,"From Jun 2020"),
                IncomeContentCell("Disrupt", "Admin Manager", "$4,000" , "Dec 2019 to May 2020")
            ))

        val assetModelCell2 = IncomeModelClass( headerTitle = "Self Employment / Independent Contractor", headerAmount = "$3,000/mo" , footerTitle = "Add Self Employment",
            incomeContentCell = arrayListOf(
                IncomeContentCell("Freelance", "Content Writer" ,"$3,000","From Dec 2020")
            ))

        val assetModelCell3 = IncomeModelClass( headerTitle = "Business" ,headerAmount = "$6,000/mo" , footerTitle = "Add Business",
            incomeContentCell = arrayListOf(
                IncomeContentCell("OPTP", "Director" ,"$6,000","From Jun 2020")
            ))


        val assetModelCell4 = IncomeModelClass( headerTitle = "Military Pay", headerAmount = "$2,000" , footerTitle = "Add Military Service",
            incomeContentCell = arrayListOf(
                IncomeContentCell("US Army", "Field 15 — Aviation" ,"$2,000","From Jun 2020")
            ))


        val assetModelCell5 = IncomeModelClass( headerTitle = "Retirement", headerAmount = "$1,200/mo" , footerTitle = "Add Retirement",
            incomeContentCell = arrayListOf(
                IncomeContentCell("Google LLC", "Pension" ,"$1,200")
            ))


        val assetModelCell6 = IncomeModelClass( headerTitle = "Other", headerAmount = "$4,125/mo" , footerTitle = "Add Other Income",
            incomeContentCell = arrayListOf(
                IncomeContentCell("Alimony", "Family" ,"$4,125")
            ))



        val modelArrayList:ArrayList<IncomeModelClass> = arrayListOf()
        modelArrayList.add(assetModelCell)
        modelArrayList.add(assetModelCell2)
        modelArrayList.add(assetModelCell3)
        modelArrayList.add(assetModelCell4)
        modelArrayList.add(assetModelCell5)
        modelArrayList.add(assetModelCell6)


        return modelArrayList

    }

    private val navigateToBank = View.OnClickListener { findNavController().navigate(R.id.navigation_bank_account) }
    private val navigateToRetirement = View.OnClickListener { findNavController().navigate(R.id.navigation_retirement_fragment) }
    private val navigateToStockBonds = View.OnClickListener { findNavController().navigate(R.id.navigation_stock_bonds) }
    private val navigateToTransactionAsset = View.OnClickListener { findNavController().navigate(R.id.navigation_proceed_from_transaction) }
    private val navigateToGiftAsset = View.OnClickListener { findNavController().navigate(R.id.navigation_gift_assets) }
    private val navigateToOtherAsset = View.OnClickListener { findNavController().navigate(R.id.navigation_other_asset) }
}