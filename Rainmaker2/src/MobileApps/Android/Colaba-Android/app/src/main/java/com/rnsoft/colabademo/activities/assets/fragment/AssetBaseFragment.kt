package com.rnsoft.colabademo

import android.view.View
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController

open class AssetBaseFragment:BaseFragment() {

    protected fun getSampleAssets():ArrayList<AssetsModelClass>{
        val assetModelCell = AssetsModelClass( headerTitle = "Bank Account", headerAmount = "$26,000" , footerTitle = "Add Bank Account",
            contentCell = arrayListOf(
                ContentCell("Chase", "Checking" ,"$20,000"), ContentCell("Ally Bank", "Saving", "$6,000")
            ), navigateToBank)

        val assetModelCell2 = AssetsModelClass( headerTitle = "Retirement Account", headerAmount = "$10,000" , footerTitle = "Add Retirement Account",
            contentCell = arrayListOf(
                ContentCell("401K", "Retirement Account" ,"$10,000" )
            ), navigateToRetirement)

        val assetModelCell3 = AssetsModelClass( headerTitle = "Stocks, Bonds, or Other\n" +
                "Financial Assets", headerAmount = "$800" , footerTitle = "Add Financial Assets",
            contentCell = arrayListOf(
                ContentCell("AHC", "Mutual Funds" ,"$200"  )
            ), navigateToStockBonds)


        val assetModelCell4 = AssetsModelClass( headerTitle = "Proceeds From Transaction", headerAmount = "$1,200" , footerTitle = "Add Proceeds From Transaction",
            contentCell = arrayListOf(
                ContentCell("Proceeds From Selling Non-Real Es...", "Proceeds From Transaction" ,"$1,200" )
            ), navigateToTransactionAsset)


        val assetModelCell5 = AssetsModelClass( headerTitle = "Gift Funds", headerAmount = "$2000" , footerTitle = "Add Gifts Account",
            contentCell = arrayListOf(
                ContentCell("Relative", "Cash Gifts" ,"$2000" )
            ), navigateToGiftAsset)


        val assetModelCell6 = AssetsModelClass( headerTitle = "Other", headerAmount = "$600" , footerTitle = "Add Other Assets",
            contentCell = arrayListOf(
                ContentCell("Individual Development Account", "Other" ,"$600" )
            ) , navigateToOtherAsset)



        val modelArrayList:ArrayList<AssetsModelClass> = arrayListOf()
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