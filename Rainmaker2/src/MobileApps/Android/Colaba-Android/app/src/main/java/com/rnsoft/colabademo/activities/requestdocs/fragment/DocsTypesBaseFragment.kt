package com.rnsoft.colabademo

import android.view.View
import androidx.navigation.fragment.findNavController

open class DocsTypesBaseFragment:BaseFragment() {

    protected fun getSampleDocsTemplate():ArrayList<DocTypeModelClass>{
        val docTypeModelClass = DocTypeModelClass( headerTitle = "My Templates", totalSelected = "0" , footerTitle = "",
            contentCell = arrayListOf(
                DocTypeContentCell("Income Template", "Checking"),
                DocTypeContentCell("My Standard Checklist", "Saving"),
                DocTypeContentCell("Assets template", "Saving")
            ), navigateToBank)

        val docTypeModelClass2 = DocTypeModelClass( headerTitle = "System Templates", totalSelected = "0" , footerTitle = "",
            contentCell = arrayListOf(
                DocTypeContentCell("Income Template", "Checking"),
                DocTypeContentCell("My Standard Checklist", "Saving"),
                DocTypeContentCell("Assets template", "Saving")
            ), navigateToBank)

        val modelArrayList:ArrayList<DocTypeModelClass> = arrayListOf()
        modelArrayList.add(docTypeModelClass)
        modelArrayList.add(docTypeModelClass2)
        return modelArrayList

    }

    private val navigateToBank = View.OnClickListener { findNavController().navigate(R.id.action_assets_bank_account) }
    private val navigateToRetirement = View.OnClickListener { findNavController().navigate(R.id.action_assets_retirement) }
    private val navigateToStockBonds = View.OnClickListener { findNavController().navigate(R.id.action_assets_stocks_bond) }
    private val navigateToTransactionAsset = View.OnClickListener { findNavController().navigate(R.id.action_assets_proceeds_transaction) }
    private val navigateToGiftAsset = View.OnClickListener { findNavController().navigate(R.id.action_assets_gift) }
    private val navigateToOtherAsset = View.OnClickListener { findNavController().navigate(R.id.action_assets_other) }
}