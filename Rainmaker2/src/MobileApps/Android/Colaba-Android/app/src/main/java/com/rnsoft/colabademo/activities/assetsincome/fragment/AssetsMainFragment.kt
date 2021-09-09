package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.get
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.*
import kotlinx.android.synthetic.main.assets_top_cell.view.*

class AssetsMainFragment : Fragment() {

    private lateinit var binding: AssetFragmentLayoutBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = AssetFragmentLayoutBinding.inflate(inflater, container, false)

        setupLayout()

        return binding.root
    }

    private fun setupLayout(){
        for (i in 1..5) {
            val mainCell: LinearLayoutCompat =
                layoutInflater.inflate(R.layout.assets_main_cell, null) as LinearLayoutCompat
            val topCell: View = layoutInflater.inflate(R.layout.assets_top_cell, null)
            val middleCell: View = layoutInflater.inflate(R.layout.assets_middle_cell, null)
            val bottomCell: View = layoutInflater.inflate(R.layout.assets_bottom_cell, null)
            mainCell.addView(topCell)
            mainCell.setTag(R.string.asset_top_cell, topCell)
            mainCell.addView(middleCell)
            mainCell.setTag(R.string.asset_middle_cell, middleCell)
            mainCell.addView(bottomCell)
            mainCell.setTag(R.string.asset_bottom_cell, bottomCell)
            middleCell.visibility = View.GONE
            bottomCell.visibility = View.GONE
            binding.assetParentContainer.addView(mainCell)

            topCell.arrow_down.setOnClickListener {
                //hideOtherBoxes() // if you want to hide other boxes....
                topCell.arrow_up.visibility = View.VISIBLE
                topCell.arrow_down.visibility = View.GONE
                middleCell.visibility = View.VISIBLE
                bottomCell.visibility = View.VISIBLE
            }

            topCell.arrow_up.setOnClickListener {
                topCell.arrow_up.visibility = View.GONE
                topCell.arrow_down.visibility = View.VISIBLE
                middleCell.visibility = View.GONE
                bottomCell.visibility = View.GONE
            }
        }
    }

    private fun hideOtherBoxes(){
        val layout = binding.assetParentContainer
        val count: Int = layout.childCount
        var mainCell: LinearLayoutCompat?
        for (i in 0 until count) {
            mainCell = layout[i] as LinearLayoutCompat
            val topCell = mainCell.getTag(R.string.asset_top_cell) as ConstraintLayout
            val middleCell = mainCell.getTag(R.string.asset_middle_cell) as ConstraintLayout
            val bottomCell = mainCell.getTag(R.string.asset_bottom_cell) as ConstraintLayout
            topCell.arrow_up.visibility = View.GONE
            topCell.arrow_down.visibility = View.VISIBLE
            middleCell.visibility = View.GONE
            bottomCell.visibility = View.GONE
        }
    }





}
