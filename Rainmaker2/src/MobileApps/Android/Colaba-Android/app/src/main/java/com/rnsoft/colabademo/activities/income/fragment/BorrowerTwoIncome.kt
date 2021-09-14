package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.get
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.*
import kotlinx.android.synthetic.main.assets_bottom_cell.view.*
import kotlinx.android.synthetic.main.assets_top_cell.view.*
import kotlinx.android.synthetic.main.income_middle_cell.view.*

class BorrowerTwoIncome : Fragment() {

    private lateinit var binding: DynamicIncomeFragmentLayoutBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = DynamicIncomeFragmentLayoutBinding.inflate(inflater, container, false)

        setupLayout()

        return binding.root
    }

    private fun getSampleIncome():ArrayList<IncomeModelClass>{
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

    private fun setupLayout(){

        val sampleIncomes = getSampleIncome()

        for (i in 0 until sampleIncomes.size) {

            val modelData = sampleIncomes[i]
            //Log.e("header",modelData.headerTitle )
            //Log.e("h-amount",modelData.headerAmount )

            val mainCell: LinearLayoutCompat =
                layoutInflater.inflate(R.layout.income_main_cell, null) as LinearLayoutCompat
            val topCell: View = layoutInflater.inflate(R.layout.income_top_cell, null)
            topCell.header_title.text =  modelData.headerTitle

            topCell.header_amount.setText(modelData.headerAmount)

            topCell.tag = R.string.asset_top_cell
            mainCell.addView(topCell)


            for (j in 0 until modelData.incomeContentCell.size) {
                val contentCell: View =
                    layoutInflater.inflate(R.layout.income_middle_cell, null)
                val contentData = modelData.incomeContentCell[j]
                contentCell.content_title.text = contentData.title
                contentCell.content_desc.text = contentData.description
                contentCell.content_amount.text = contentData.contentAmount
                contentCell.tenureTextView.text = contentData.tenure
                contentCell.visibility = View.GONE
                mainCell.addView(contentCell)
            }


            val bottomCell: View = layoutInflater.inflate(R.layout.income_bottom_cell, null)
            bottomCell.footer_title.text =  modelData.footerTitle
            //bottomCell.tag = R.string.asset_bottom_cell
            bottomCell.visibility = View.GONE

            if(i == 0)
                bottomCell.setOnClickListener{
                    findNavController().navigate(R.id.navigation_bank_account)
                }
            else
                if(i == 1)
                    bottomCell.setOnClickListener{
                        findNavController().navigate(R.id.navigation_retirement_fragment)
                    }
                else
                    if(i == 2)
                        bottomCell.setOnClickListener{
                            findNavController().navigate(R.id.navigation_stock_bonds)
                        }
            mainCell.addView(bottomCell)




            binding.assetParentContainer.addView(mainCell)

            topCell.arrow_down.setOnClickListener {
                hideOtherBoxes() // if you want to hide other boxes....
                topCell.arrow_up.visibility = View.VISIBLE
                topCell.arrow_down.visibility = View.GONE
                toggleContentCells(mainCell, View.VISIBLE)
                //bottomCell.visibility = View.VISIBLE
            }

            topCell.arrow_up.setOnClickListener {
                topCell.arrow_up.visibility = View.GONE
                topCell.arrow_down.visibility = View.VISIBLE
                //contentCell.visibility = View.GONE
                toggleContentCells(mainCell , View.GONE)
                //bottomCell.visibility = View.GONE
            }
        }
    }



   /* private fun setupLayout(){
        for (i in 1..6) {
            val mainCell: LinearLayoutCompat =
                layoutInflater.inflate(R.layout.assets_main_cell, null) as LinearLayoutCompat
            val topCell: View = layoutInflater.inflate(R.layout.assets_top_cell, null)

            val bottomCell: View = layoutInflater.inflate(R.layout.assets_bottom_cell, null)

            topCell.tag = R.string.asset_top_cell
            mainCell.addView(topCell)

            if(i == 1) {
                for (j in 1..2) {
                    val contentCell: View = layoutInflater.inflate(R.layout.assets_middle_cell, null)
                    //contentCell.tag = R.string.asset_middle_cell
                    contentCell.visibility = View.GONE
                    mainCell.addView(contentCell)
                }
            }
            else {
                val contentCell: View = layoutInflater.inflate(R.layout.assets_middle_cell, null)
                contentCell.visibility = View.GONE
                mainCell.addView(contentCell)
            }




            //bottomCell.tag = R.string.asset_bottom_cell
            bottomCell.visibility = View.GONE
            mainCell.addView(bottomCell)




            binding.assetParentContainer.addView(mainCell)

            topCell.arrow_down.setOnClickListener {
                //hideOtherBoxes() // if you want to hide other boxes....
                topCell.arrow_up.visibility = View.VISIBLE
                topCell.arrow_down.visibility = View.GONE
                toggleContentCells(mainCell, View.VISIBLE)
                //bottomCell.visibility = View.VISIBLE
            }

            topCell.arrow_up.setOnClickListener {
                topCell.arrow_up.visibility = View.GONE
                topCell.arrow_down.visibility = View.VISIBLE
                //contentCell.visibility = View.GONE
                toggleContentCells(mainCell , View.GONE)
                //bottomCell.visibility = View.GONE
            }
        }
    } */

    private fun toggleContentCells(mainCell: LinearLayoutCompat , display:Int){
        for (j in 0 until mainCell.childCount){
            if(mainCell[j].tag != R.string.asset_top_cell)
                mainCell[j].visibility = display
        }
    }

    private fun hideOtherBoxes(){
        val layout = binding.assetParentContainer
        var mainCell: LinearLayoutCompat?
        for (i in 0 until layout.childCount) {
            mainCell = layout[i] as LinearLayoutCompat
            for(j in 0 until mainCell.childCount) {
                val innerCell = mainCell[j] as ConstraintLayout
                    if (innerCell.tag == R.string.asset_top_cell) {
                        innerCell.arrow_up.visibility = View.GONE
                        innerCell.arrow_down.visibility = View.VISIBLE
                    } else {
                        innerCell.visibility = View.GONE
                    }
            }

            /*
            val topCell = mainCell.getTag(R.string.asset_top_cell) as ConstraintLayout
            val middleCell = mainCell.getTag(R.string.asset_middle_cell) as ConstraintLayout
            val bottomCell = mainCell.getTag(R.string.asset_bottom_cell) as ConstraintLayout
            topCell.arrow_up.visibility = View.GONE
            topCell.arrow_down.visibility = View.VISIBLE
            middleCell.visibility = View.GONE
            bottomCell.visibility = View.GONE
             */
        }
    }





}
