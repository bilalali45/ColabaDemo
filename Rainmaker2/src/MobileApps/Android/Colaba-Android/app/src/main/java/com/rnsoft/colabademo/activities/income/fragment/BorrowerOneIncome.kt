package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.get
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.income.fragment.EventAddEmployment
import com.rnsoft.colabademo.databinding.*
import kotlinx.android.synthetic.main.assets_bottom_cell.view.*
import kotlinx.android.synthetic.main.assets_middle_cell.view.content_amount
import kotlinx.android.synthetic.main.assets_middle_cell.view.content_desc
import kotlinx.android.synthetic.main.assets_middle_cell.view.content_title
import kotlinx.android.synthetic.main.assets_top_cell.view.*
import kotlinx.android.synthetic.main.income_middle_cell.view.*
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode

class BorrowerOneIncome : IncomeBaseFragment() {

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



    private fun setupLayout(){

        val sampleIncomeData = getSampleIncome()

        for (i in 0 until sampleIncomeData.size) {

            val modelData = sampleIncomeData[i]
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
                if(contentData.contentListenerAttached!=null)
                    contentCell.setOnClickListener(contentData.contentListenerAttached)
                else
                    contentCell.setOnClickListener(modelData.listenerAttached)
                mainCell.addView(contentCell)
            }


            val bottomCell: View = layoutInflater.inflate(R.layout.income_bottom_cell, null)
            bottomCell.footer_title.text =  modelData.footerTitle
            bottomCell.visibility = View.GONE
            bottomCell.setOnClickListener(modelData.listenerAttached)
            mainCell.addView(bottomCell)

            binding.assetParentContainer.addView(mainCell)

            topCell.setOnClickListener {
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



    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onAddEmploymentEvent(event: EventAddEmployment) {
        if(event.boolean) {
            findNavController().navigate(R.id.navigation_income_current_employment)
        }
        else {
            findNavController().navigate(R.id.navigation_income_prev_emplyoyment)
        }
    }



}
