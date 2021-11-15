package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.get
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.*
import com.rnsoft.colabademo.utils.Common
import kotlinx.android.synthetic.main.assets_bottom_cell.view.*
import kotlinx.android.synthetic.main.assets_middle_cell.view.*
import kotlinx.android.synthetic.main.assets_top_cell.view.*
import org.greenrobot.eventbus.EventBus
import timber.log.Timber
import androidx.navigation.fragment.findNavController


class BorrowerOneAssets : AssetBaseFragment() {

    private lateinit var binding: DynamicAssetFragmentLayoutBinding

    private val borrowerApplicationViewModel: BorrowerApplicationViewModel by activityViewModels()

    private  var tabBorrowerId:Int? = null

    private var grandTotalAmount:Double = 0.0

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = DynamicAssetFragmentLayoutBinding.inflate(inflater, container, false)
        arguments?.let {
            tabBorrowerId = it.getInt(AppConstant.tabBorrowerId)
        }

        lifecycleScope.launchWhenStarted {
            borrowerApplicationViewModel.assetsModelDataClass.observe(
                viewLifecycleOwner,
                Observer { observableSampleContent ->
                    val assetsActivity = (activity as? AssetsActivity)
                    assetsActivity?.let { assetsActivity->
                        //assetsActivity.borrowerTabList?.let { borrowerTabList-> }
                        assetsActivity.binding.assetDataLoader.visibility = View.INVISIBLE
                    }

                    var observerCounter = 0
                    var getBorrowerAssets: ArrayList<BorrowerAsset> = arrayListOf()
                    while (observerCounter < observableSampleContent.size) {
                        val webAssets = observableSampleContent[observerCounter]
                        if (tabBorrowerId == webAssets.passedBorrowerId) {
                            webAssets.bAssetData?.borrower?.borrowerAssets?.let { webBorrowerAssets ->
                                getBorrowerAssets = webBorrowerAssets
                            }
                        }
                        observerCounter++
                    }
                    val sampleAssets = getSampleAssets()
                    for (m in 0 until sampleAssets.size) {
                        val modelData = sampleAssets[m]
                        //Timber.e("header", modelData.headerTitle)
                        //Timber.e("h-amount", modelData.headerAmount)
                        val mainCell: LinearLayoutCompat = layoutInflater.inflate(R.layout.asset_top_main_cell, null) as LinearLayoutCompat
                        val topCell: View = layoutInflater.inflate(R.layout.assets_top_cell, null)
                        topCell.header_title.text = modelData.headerTitle
                        topCell.header_amount.setText(modelData.headerAmount)
                        topCell.tag = R.string.asset_top_cell
                        mainCell.addView(topCell)


                        var totalAmount = 0.0
                        for (i in 0 until getBorrowerAssets.size) {
                            val webModelData = getBorrowerAssets[i]
                            webModelData.assetsCategory?.let { assetsCategory->
                                if(assetsCategory == modelData.headerTitle)   {
                                    webModelData.assets?.let {
                                        for (j in 0 until it.size) {
                                            val contentCell: View = layoutInflater.inflate(R.layout.assets_middle_cell, null)
                                            val contentData = webModelData.assets[j]
                                            //Timber.e(" content data - "+contentData)
                                            contentCell.content_title.text = contentData.assetCategoryName
                                            contentCell.content_desc.text = contentData.assetName
                                            contentData.assetValue?.let{ assetValue->
                                                totalAmount += assetValue
                                                contentCell.content_amount.text = "$".plus(Common.addNumberFormat(assetValue))
                                            }
                                            contentCell.visibility = View.GONE
                                            contentCell.setOnClickListener {
                                                val parentActivity = activity as? AssetsActivity
                                                parentActivity?.let {
                                                    val bundle = Bundle()
                                                    parentActivity.loanApplicationId?.let { it1 -> bundle.putInt(AppConstant.loanApplicationId, it1) }
                                                    tabBorrowerId?.let { it1 -> bundle.putInt(AppConstant.borrowerId, it1) }
                                                    contentData.assetId?.let { it1 -> bundle.putInt(AppConstant.borrowerAssetId, it1) }
                                                    contentData.assetCategoryId?.let { it1 -> bundle.putInt(AppConstant.assetCategoryId, it1) }
                                                    contentData.assetTypeID?.let{ it1 ->  bundle.putInt(AppConstant.assetTypeID, it1)  }
                                                    parentActivity.loanPurpose?.let { it1 -> bundle.putString(AppConstant.loanPurpose, it1) }
                                                    Timber.e(" content data - "+contentData)

                                                    findNavController().navigate(modelData.listenerAttached, bundle)
                                                }
                                            }
                                            mainCell.addView(contentCell)
                                        }
                                    }
                                }
                            }
                        }

                        topCell.header_amount.text = "$".plus(Common.addNumberFormat(totalAmount)) //"$"+totalAmount.roundToInt().toString()
                        grandTotalAmount += totalAmount

                        val bottomCell: View =
                            layoutInflater.inflate(R.layout.assets_bottom_cell, null)
                        bottomCell.footer_title.text = modelData.footerTitle
                        //bottomCell.tag = R.string.asset_bottom_cell
                        bottomCell.visibility = View.GONE
                        bottomCell.setOnClickListener {
                            val parentActivity = activity as? AssetsActivity
                            parentActivity?.let {
                                val bundle = Bundle()
                                parentActivity.loanApplicationId?.let { it1 -> bundle.putInt(AppConstant.loanApplicationId, it1) }
                                tabBorrowerId?.let { it1 -> bundle.putInt(AppConstant.borrowerId, it1) }
                                parentActivity.loanPurpose?.let { it1 -> bundle.putString(AppConstant.loanPurpose, it1) }
                                findNavController().navigate(modelData.listenerAttached , bundle)
                            }

                        }

                        mainCell.addView(bottomCell)

                        binding.assetParentContainer.addView(mainCell)

                        //val drawable = R.drawable.toast_err

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
                            toggleContentCells(mainCell, View.GONE)
                            //bottomCell.visibility = View.GONE
                        }
                    }

                    EventBus.getDefault().post(GrandTotalEvent("$".plus(Common.addNumberFormat(grandTotalAmount)))) // "$"+grandTotalAmount.roundToInt().toString()))
                })
        }


        super.addListeners(binding.root)

        return binding.root
    }

    private fun setupLayout(){

        Timber.d("setupLayout onCreateView function")

        val sampleAssets = getSampleAssets()

        for (i in 0 until sampleAssets.size) {

            val modelData = sampleAssets[i]
            //Log.e("header",modelData.headerTitle )
            //Log.e("h-amount",modelData.headerAmount )

            val mainCell: LinearLayoutCompat =
                layoutInflater.inflate(R.layout.asset_top_main_cell, null) as LinearLayoutCompat
            val topCell: View = layoutInflater.inflate(R.layout.assets_top_cell, null)
            topCell.header_title.text =  modelData.headerTitle

            topCell.header_amount.setText(modelData.headerAmount)

            topCell.tag = R.string.asset_top_cell
            mainCell.addView(topCell)


            for (j in 0 until modelData.contentCell.size) {
                val contentCell: View =
                    layoutInflater.inflate(R.layout.assets_middle_cell, null)
                val contentData = modelData.contentCell[j]
                contentCell.content_title.text = contentData.title
                contentCell.content_desc.text = contentData.description
                contentCell.content_amount.text = contentData.contentAmount
                contentCell.visibility = View.GONE
                contentCell.setOnClickListener {
                    findNavController().navigate(modelData.listenerAttached)
                }
                mainCell.addView(contentCell)
            }


            val bottomCell: View = layoutInflater.inflate(R.layout.assets_bottom_cell, null)
            bottomCell.footer_title.text =  modelData.footerTitle
            //bottomCell.tag = R.string.asset_bottom_cell
            bottomCell.visibility = View.GONE
            bottomCell.setOnClickListener {
                findNavController().navigate(modelData.listenerAttached)
            }

            mainCell.addView(bottomCell)

            binding.assetParentContainer.addView(mainCell)

            val drawable = R.drawable.toast_err

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

    private fun toggleContentCells(mainCell: LinearLayoutCompat, display:Int){
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

    override fun onStop() {
        super.onStop()
        Timber.e("onStop from Fragment called....")
    }


}
