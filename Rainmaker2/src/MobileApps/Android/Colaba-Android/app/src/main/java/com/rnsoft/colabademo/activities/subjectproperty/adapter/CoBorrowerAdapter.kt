package com.rnsoft.colabademo

import android.annotation.SuppressLint
import android.content.Context
import android.graphics.Typeface
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.core.content.ContentProviderCompat.requireContext
import androidx.core.content.ContextCompat
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.databinding.OccupancyListItemBinding

/**
 * Created by Anita Kiran on 11/11/2021.
 */
class CoBorrowerAdapter(var context: Context, clickListner: CoBorrowerOccupancyClickListener) : RecyclerView.Adapter<CoBorrowerAdapter.DataViewHolder>() {

    var bList: List<CoBorrowerOccupancyData> = arrayListOf()
    private var clickEvent: CoBorrowerOccupancyClickListener = clickListner

    init {
        this.clickEvent = clickListner
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DataViewHolder {
        var binding = OccupancyListItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)


        return DataViewHolder(binding)
    }

    override fun onBindViewHolder(holder: DataViewHolder, position: Int) {
        holder.bind(bList.get(position), position)
    }

    override fun getItemCount() = bList.size

    inner class DataViewHolder(val binding : OccupancyListItemBinding) : RecyclerView.ViewHolder(binding.root) {
        fun bind(data: CoBorrowerOccupancyData, position: Int) {
            binding.coBorrowerName.text = data.borrowerFirstName.plus(" ").plus(data.borrowerLastName)
            data.willLiveInSubjectProperty?.let { willLiveInSubjectProperty->
                if(willLiveInSubjectProperty){
                    binding.radioOccupying.isChecked = true
                    binding.radioOccupying.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
                }
                else {
                    binding.radioNonOccupying.isChecked = true
                    binding.radioNonOccupying.setTextColor(ContextCompat.getColor(context, R.color.grey_color_two))
                }

            }

            binding.radioOccupying.setOnCheckedChangeListener { _, isChecked ->
                if(isChecked){
                    binding.radioOccupying.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
                    binding.radioNonOccupying.setTextColor(ContextCompat.getColor(context,R.color.grey_color_two))
                    clickEvent.onCoborrowerClick(position,true)
                }
            }
            // radio non occupying
            binding.radioNonOccupying.setOnCheckedChangeListener { _, isChecked ->
                if(isChecked){
                    binding.radioNonOccupying.setTextColor(ContextCompat.getColor(context,R.color.grey_color_one))
                    binding.radioOccupying.setTextColor(ContextCompat.getColor(context,R.color.grey_color_two))
                    clickEvent.onCoborrowerClick(position,false)
                }
            }
        }
    }

    @SuppressLint("NotifyDataSetChanged")
    fun setBorrowers(bList: List<CoBorrowerOccupancyData>) {
        this.bList = bList
        notifyDataSetChanged()
    }
}