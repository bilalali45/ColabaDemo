package com.rnsoft.colabademo

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.get
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.income.fragment.BottomDialogSelectEmployment
import com.rnsoft.colabademo.activities.income.fragment.EventAddEmployment
import com.rnsoft.colabademo.databinding.*
import kotlinx.android.synthetic.main.assets_bottom_cell.view.*
import kotlinx.android.synthetic.main.assets_middle_cell.view.content_amount
import kotlinx.android.synthetic.main.assets_middle_cell.view.content_desc
import kotlinx.android.synthetic.main.assets_middle_cell.view.content_title
import kotlinx.android.synthetic.main.assets_top_cell.view.*
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import kotlin.math.roundToInt

class BorrowerOneIncome : IncomeBaseFragment() {

    private lateinit var binding: DynamicIncomeFragmentLayoutBinding
    private val viewModel: BorrowerApplicationViewModel by activityViewModels()
    private  var tabBorrowerId:Int? = null
    private var grandTotalAmount:Double = 0.0
    val Employment = "Employment"

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = DynamicIncomeFragmentLayoutBinding.inflate(inflater, container, false)

        arguments?.let {
            tabBorrowerId = it.getInt(AppConstant.tabBorrowerId)
        }

        setupLayout()

        return binding.root
    }


    private fun setupLayout(){
        lifecycleScope.launchWhenStarted {
            viewModel.incomeDetails.observe(viewLifecycleOwner, { observableSampleContent ->
                    val incomeActivity = (activity as? IncomeActivity)
                    incomeActivity?.let { incomeActivity->
                        incomeActivity.binding.assetDataLoader.visibility = View.INVISIBLE
                    }

                    var observerCounter = 0
                    var getBorrowerIncome: ArrayList<BorrowerIncome> = arrayListOf()
                    while (observerCounter < observableSampleContent.size) {
                        val webIncome = observableSampleContent[observerCounter]
                        if (tabBorrowerId == webIncome.passedBorrowerId) {
                            webIncome.incomeData?.borrower?.borrowerIncomes?.let { webBorrowerIncome ->
                                getBorrowerIncome = webBorrowerIncome
                            }
                        }
                        observerCounter++
                    }
                    val sampleIncome = getSampleIncome()
                    for (m in 0 until sampleIncome.size) {
                        val modelData = sampleIncome[m]
                        //Timber.e("header", modelData.headerTitle)
                        //Timber.e("h-amount", modelData.headerAmount)
                        val mainCell: LinearLayoutCompat =
                            layoutInflater.inflate(R.layout.income_main_cell, null) as LinearLayoutCompat
                        val topCell: View =
                            layoutInflater.inflate(R.layout.income_top_cell, null)
                        topCell.header_title.text = modelData.headerTitle
                        topCell.header_amount.setText(modelData.headerAmount)
                        topCell.tag = R.string.asset_top_cell
                        mainCell.addView(topCell)


                        var totalAmount = 0.0
                        for (i in 0 until getBorrowerIncome.size) {

                            val webModelData = getBorrowerIncome[i]
                            webModelData.incomeCategory?.let { incomeCategory->
                                if(incomeCategory == modelData.headerTitle)   {
                                    webModelData.incomes?.let {
                                        for (j in 0 until it.size) {
                                            val contentCell: View = layoutInflater.inflate(R.layout.income_middle_cell, null)
                                            val contentData = webModelData.incomes[j]
                                            contentCell.content_title.text =
                                                contentData.incomeTypeDisplayName
                                            contentCell.content_desc.text =
                                                contentData.incomeName

                                            contentData.incomeValue?.let{ incomeValue->
                                                totalAmount += incomeValue
                                                contentCell.content_amount.text = "$"+incomeValue.toString()
                                            }
                                            contentCell.visibility = View.GONE
                                            if(contentData.endDate == null && contentData.incomeTypeDisplayName == Employment)
                                                contentCell.setOnClickListener(currentEmploymentListener)

                                            else
                                                contentCell.setOnClickListener(modelData.listenerAttached)
                                            mainCell.addView(contentCell)
                                        }
                                    }
                                }
                            }
                        }

                        topCell.header_amount.text = "$"+totalAmount.roundToInt().toString()
                        grandTotalAmount += totalAmount

                        val bottomCell: View =
                            layoutInflater.inflate(R.layout.income_bottom_cell, null)
                        bottomCell.footer_title.text = modelData.footerTitle
                        bottomCell.visibility = View.GONE
                        if(modelData.footerTitle == AppConstant.footerAddEmployment) {
                            bottomCell.setOnClickListener (bottomEmploymentListener)
                        }
                        else
                            bottomCell.setOnClickListener(modelData.listenerAttached)

                        mainCell.addView(bottomCell)
                        binding.assetParentContainer.addView(mainCell)

                        topCell.setOnClickListener {
                            hideOtherBoxes() // if you want to hide other boxes....
                            topCell.arrow_up.visibility = View.VISIBLE
                            topCell.arrow_down.visibility = View.GONE
                            toggleContentCells(mainCell, View.VISIBLE)
                        }

                        topCell.arrow_up.setOnClickListener {
                            topCell.arrow_up.visibility = View.GONE
                            topCell.arrow_down.visibility = View.VISIBLE
                            toggleContentCells(mainCell, View.GONE)
                        }
                    }

                    EventBus.getDefault().post(GrandTotalEvent("$"+grandTotalAmount.roundToInt().toString()))
                })
        }

    }

    val currentEmploymentListener:View.OnClickListener= View.OnClickListener {
        findNavController().navigate(R.id.action_current_employement)
    }

    val bottomEmploymentListener:View.OnClickListener= View.OnClickListener {
        BottomDialogSelectEmployment.newInstance().show(childFragmentManager, BottomDialogSelectEmployment::class.java.canonicalName)
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
            findNavController().navigate(R.id.action_current_employement)
        }
        else {
            findNavController().navigate(R.id.action_prev_employment)
        }
    }

}
