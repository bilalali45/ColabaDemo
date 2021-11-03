package com.rnsoft.colabademo

import com.rnsoft.colabademo.activities.assets.model.CATEGORIES

open class AssetBaseFragment:BaseFragment() {

    protected fun getSampleAssets():ArrayList<TestAssetsModelClass>{
        val assetModelCell = TestAssetsModelClass( headerTitle = CATEGORIES.BankAccount.categoryName, headerAmount = "$0" , footerTitle = "Add Bank Account",
            contentCell = arrayListOf(
             //   TestContentCell("Chase", "Checking" ,"$20,000"), TestContentCell("Ally Bank", "Saving", "$6,000")
            ), navigateToBank)

        val assetModelCell2 = TestAssetsModelClass( headerTitle = CATEGORIES.RetirementAccount.categoryName, headerAmount = "$0" , footerTitle = "Add Retirement Account",
            contentCell = arrayListOf(
               // TestContentCell("401K", "Retirement Account" ,"$10,000" )
            ), navigateToRetirement)

        val assetModelCell3 = TestAssetsModelClass( headerTitle = CATEGORIES.StocksBondsOtherFinancialAssets.categoryName, headerAmount = "$0" , footerTitle = "Add Financial Assets",
            contentCell = arrayListOf(
                //TestContentCell("AHC", "Mutual Funds" ,"$200"  )
            ), navigateToStockBonds)


        val assetModelCell4 = TestAssetsModelClass( headerTitle = CATEGORIES.ProceedFromTransaction.categoryName, headerAmount = "$0" , footerTitle = "Add Proceeds From Transaction",
            contentCell = arrayListOf(
               // TestContentCell("Proceeds From Selling Non-Real Es...", "Proceeds From Transaction" ,"$1,200" )
            ), navigateToTransactionAsset)


        val assetModelCell5 = TestAssetsModelClass( headerTitle = CATEGORIES.GiftFunds.categoryName, headerAmount = "$0" , footerTitle = "Add Gifts Account",
            contentCell = arrayListOf(
                //TestContentCell("Relative", "Cash Gifts" ,"$2000" )
            ), navigateToGiftAsset)


        val assetModelCell6 = TestAssetsModelClass( headerTitle = CATEGORIES.Other.categoryName, headerAmount = "$0" , footerTitle = "Add Other Assets",
            contentCell = arrayListOf(
                //TestContentCell("Individual Development Account", "Other" ,"$600" )
            ) , navigateToOtherAsset)



        val modelArrayList:ArrayList<TestAssetsModelClass> = arrayListOf()
        modelArrayList.add(assetModelCell)
        modelArrayList.add(assetModelCell2)
        modelArrayList.add(assetModelCell3)
        modelArrayList.add(assetModelCell4)
        modelArrayList.add(assetModelCell5)
        modelArrayList.add(assetModelCell6)


        return modelArrayList

    }



    private val navigateToBank =  R.id.action_assets_bank_account //View.OnClickListener { findNavController().navigate(R.id.action_assets_bank_account) }
    private val navigateToRetirement = R.id.action_assets_retirement  //View.OnClickListener { findNavController().navigate(R.id.action_assets_retirement) }
    private val navigateToStockBonds =  R.id.action_assets_stocks_bond  // View.OnClickListener { findNavController().navigate(R.id.action_assets_stocks_bond) }
    private val navigateToTransactionAsset = R.id.action_assets_proceeds_transaction  //View.OnClickListener { findNavController().navigate(R.id.action_assets_proceeds_transaction) }
    private val navigateToGiftAsset =  R.id.action_assets_gift  //View.OnClickListener { findNavController().navigate(R.id.action_assets_gift) }
    private val navigateToOtherAsset = R.id.action_assets_other  //View.OnClickListener { findNavController().navigate(R.id.action_assets_other) }
}