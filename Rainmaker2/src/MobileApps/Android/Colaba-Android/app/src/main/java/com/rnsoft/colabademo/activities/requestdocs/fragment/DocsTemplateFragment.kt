package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.get
import com.rnsoft.colabademo.databinding.DocsTemplateLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.docs_type_header_cell.view.*
import kotlinx.android.synthetic.main.docs_type_middle_cell.view.*

import javax.inject.Inject
@AndroidEntryPoint
class DocsTemplateFragment:DocsTypesBaseFragment() {

    private var _binding: DocsTemplateLayoutBinding? = null
    private val binding get() = _binding!!



    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = DocsTemplateLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpUI()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpUI(){

        val sampleDocs = getSampleDocsTemplate()
        for (i in 0 until sampleDocs.size) {

            val modelData = sampleDocs[i]

            val mainCell: LinearLayoutCompat =
                layoutInflater.inflate(R.layout.docs_type_top_main_cell, null) as LinearLayoutCompat



            val topCell: View = layoutInflater.inflate(R.layout.docs_type_header_cell, null)
            topCell.cell_header_title.text =  modelData.headerTitle
            topCell.total_selected.text = modelData.totalSelected

            // always hide this...
            topCell.total_selected.visibility = View.GONE
            topCell.items_selected_imageview.visibility = View.GONE
            topCell.tag = R.string.docs_top_cell
            mainCell.addView(topCell)


            val emptyCellStart: View = layoutInflater.inflate(R.layout.docs_type_empty_space_cell, null)
            //emptyCell.visibility = View.GONE
            mainCell.addView(emptyCellStart)

            for (j in 0 until modelData.contentCell.size) {
                val contentCell: View =
                    layoutInflater.inflate(R.layout.docs_type_middle_cell, null)
                val contentData = modelData.contentCell[j]
                contentCell.checkbox.text = contentData.checkboxContent
                contentCell.checkbox.setOnCheckedChangeListener{ buttonView, isChecked ->
                    if(isChecked)
                        buttonView.setTypeface(null, Typeface.BOLD) //only text style(only bold)
                    else
                        buttonView.setTypeface(null, Typeface.NORMAL) //only text style(only bold)
                }
                //contentCell.content_desc.text = contentData.description
                contentCell.visibility = View.VISIBLE
                contentCell.info_imageview.setOnClickListener(modelData.contentListenerAttached)
                mainCell.addView(contentCell)
            }

            val emptyCellEnd: View = layoutInflater.inflate(R.layout.docs_type_empty_space_cell, null)
            //emptyCell.visibility = View.GONE
            mainCell.addView(emptyCellEnd)


            mainCell.visibility = View.INVISIBLE
            binding.docsTypeParentContainer.addView(mainCell)
            binding.docsTypeParentContainer.postDelayed({
                hideOtherBoxes()
                mainCell.visibility = View.VISIBLE
            },500)



            topCell.setOnClickListener {
                //hideOtherBoxes()
                hideAllAndOpenedSelectedCell(topCell, mainCell)
            }

            topCell.docs_arrow_up.setOnClickListener {
                hideCurrentlyOpenedCell(topCell, mainCell)
            }



        }
    }

    private fun hideAllAndOpenedSelectedCell(topCell:View, mainCell:LinearLayoutCompat){
        hideOtherBoxes() // if you want to hide other boxes....
        topCell.docs_arrow_up.visibility = View.VISIBLE
        topCell.docs_arrow_down.visibility = View.INVISIBLE
        toggleContentCells(mainCell, View.VISIBLE)
        //bottomCell.visibility = View.VISIBLE
    }

    private fun hideCurrentlyOpenedCell(topCell:View, mainCell:LinearLayoutCompat){
        topCell.docs_arrow_up.visibility = View.INVISIBLE
        topCell.docs_arrow_down.visibility = View.VISIBLE
        //contentCell.visibility = View.GONE
        toggleContentCells(mainCell , View.GONE)
        //bottomCell.visibility = View.GONE
    }

    private fun toggleContentCells(mainCell: LinearLayoutCompat, display:Int){
        for (j in 0 until mainCell.childCount){
            if(mainCell[j].tag != R.string.docs_top_cell)
                mainCell[j].visibility = display
        }
    }

    private fun hideOtherBoxes(){
        val layout = binding.docsTypeParentContainer
        var mainCell: LinearLayoutCompat?
        for (i in 0 until layout.childCount) {
            mainCell = layout[i] as LinearLayoutCompat
            for(j in 0 until mainCell.childCount) {
                val innerCell = mainCell[j] as ConstraintLayout
                if (innerCell.tag == R.string.docs_top_cell) {
                    innerCell.docs_arrow_up.visibility = View.GONE
                    innerCell.docs_arrow_down.visibility = View.VISIBLE
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