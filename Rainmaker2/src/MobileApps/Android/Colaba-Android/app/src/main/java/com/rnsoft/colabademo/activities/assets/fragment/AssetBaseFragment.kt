package com.rnsoft.colabademo

import android.view.View
import androidx.navigation.fragment.findNavController

open class AssetBaseFragment:BaseFragment() {

    protected fun getSampleAssets():ArrayList<AssetsModelDataClass>{
        val assetModelCell = AssetsModelDataClass( headerTitle = "Bank Account", headerAmount = "$26,000" , footerTitle = "Add Bank Account",
            contentCell = arrayListOf(
                ContentCell("Chase", "Checking" ,"$20,000"), ContentCell("Ally Bank", "Saving", "$6,000")
            ), navigateToBank)

        val assetModelCell2 = AssetsModelDataClass( headerTitle = "Retirement Account", headerAmount = "$10,000" , footerTitle = "Add Retirement Account",
            contentCell = arrayListOf(
                ContentCell("401K", "Retirement Account" ,"$10,000" )
            ), navigateToRetirement)

        val assetModelCell3 = AssetsModelDataClass( headerTitle = "Stocks, Bonds, or Other\n" +
                "Financial Assets", headerAmount = "$800" , footerTitle = "Add Financial Assets",
            contentCell = arrayListOf(
                ContentCell("AHC", "Mutual Funds" ,"$200"  )
            ), navigateToStockBonds)


        val assetModelCell4 = AssetsModelDataClass( headerTitle = "Proceeds From Transaction", headerAmount = "$1,200" , footerTitle = "Add Proceeds From Transaction",
            contentCell = arrayListOf(
                ContentCell("Proceeds From Selling Non-Real Es...", "Proceeds From Transaction" ,"$1,200" )
            ), navigateToTransactionAsset)


        val assetModelCell5 = AssetsModelDataClass( headerTitle = "Gift Funds", headerAmount = "$2000" , footerTitle = "Add Gifts Account",
            contentCell = arrayListOf(
                ContentCell("Relative", "Cash Gifts" ,"$2000" )
            ), navigateToGiftAsset)


        val assetModelCell6 = AssetsModelDataClass( headerTitle = "Other", headerAmount = "$600" , footerTitle = "Add Other Assets",
            contentCell = arrayListOf(
                ContentCell("Individual Development Account", "Other" ,"$600" )
            ) , navigateToOtherAsset)



        val modelArrayList:ArrayList<AssetsModelDataClass> = arrayListOf()
        modelArrayList.add(assetModelCell)
        modelArrayList.add(assetModelCell2)
        modelArrayList.add(assetModelCell3)
        modelArrayList.add(assetModelCell4)
        modelArrayList.add(assetModelCell5)
        modelArrayList.add(assetModelCell6)


        return modelArrayList

    }

    private val navigateToBank = View.OnClickListener { findNavController().navigate(R.id.action_assets_bank_account) }
    private val navigateToRetirement = View.OnClickListener { findNavController().navigate(R.id.action_assets_retirement) }
    private val navigateToStockBonds = View.OnClickListener { findNavController().navigate(R.id.action_assets_stocks_bond) }
    private val navigateToTransactionAsset = View.OnClickListener { findNavController().navigate(R.id.action_assets_proceeds_transaction) }
    private val navigateToGiftAsset = View.OnClickListener { findNavController().navigate(R.id.action_assets_gift) }
    private val navigateToOtherAsset = View.OnClickListener { findNavController().navigate(R.id.action_assets_other) }
}