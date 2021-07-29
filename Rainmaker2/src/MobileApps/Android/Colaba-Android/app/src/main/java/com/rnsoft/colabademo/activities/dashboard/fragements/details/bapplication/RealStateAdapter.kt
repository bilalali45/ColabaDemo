package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class RealStateAdapter internal constructor(private var realStateDataList: ArrayList<TabRealStateList>) :
    RecyclerView.Adapter<RealStateAdapter.BaseViewHolder>(){

    abstract class BaseViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) { abstract fun bind(item: TabRealStateList) }


    inner class RealStateViewHolder(itemView: View) : BaseViewHolder(itemView) {
        private var propertyAddress: TextView = itemView.findViewById(R.id.propertyAddress)
        private var propertyType: TextView = itemView.findViewById(R.id.propertyType)
        override fun bind(item: TabRealStateList) {
            propertyAddress.text = item.propertyAddress
            propertyType.text = item.propertyType
        }
    }

    inner class RealStateFooterViewHolder(itemView: View) : BaseViewHolder(itemView) {
        override fun bind(item: TabRealStateList) {}
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BaseViewHolder {
        val holder: BaseViewHolder?
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
        holder = if(viewType == R.layout.realstate_footer_horizontal)
            RealStateFooterViewHolder(inflater.inflate(R.layout.realstate_footer_horizontal, parent, false))
        else
            RealStateViewHolder(inflater.inflate(R.layout.realstate_horizontal, parent, false))
        return holder
    }

    override fun getItemViewType(position: Int): Int {
        return if(realStateDataList[position].isFooter)
            R.layout.realstate_footer_horizontal
        else
            R.layout.realstate_horizontal
    }

    override fun onBindViewHolder(holder: BaseViewHolder, position: Int){ holder.bind(realStateDataList[position]) }

    override fun getItemCount() = realStateDataList.size







}