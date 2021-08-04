package com.rnsoft.colabademo

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class CustomBorrowerAdapter internal constructor(private var tabBorrowerDataList: ArrayList<TabBorrowerList>) :
    RecyclerView.Adapter<CustomBorrowerAdapter.BaseViewHolder>(){

    abstract class BaseViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) { abstract fun bind(item: TabBorrowerList) }
    //abstract class BaseViewHolder<TabBorrowerList>(itemView: View) : RecyclerView.ViewHolder(itemView) { abstract fun bind(item: TabBorrowerList) }

    inner class BorrowerItemViewHolder(itemView: View) : BaseViewHolder(itemView) {
        private val coBorrowerNames : TextView = itemView.findViewById(R.id.co_borrower_test)
        private val mainBorrowerName:TextView = itemView.findViewById(R.id.main_borrower_test)
        override fun bind(item: TabBorrowerList) {
            mainBorrowerName.text = item.name
            coBorrowerNames.text = item.coName
        }
    }

    inner class BorrowerFooterViewHolder(itemView: View) : BaseViewHolder(itemView) {
        override fun bind(item: TabBorrowerList) {}
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BaseViewHolder {
        val holder: BaseViewHolder?
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
        holder = if(viewType == R.layout.list_footer_borrower_horizontal)
                    BorrowerFooterViewHolder(inflater.inflate(R.layout.list_footer_borrower_horizontal, parent, false))
                else
                    BorrowerItemViewHolder(inflater.inflate(R.layout.list_borrower_horizontal, parent, false))
        return holder
    }

    override fun getItemViewType(position: Int): Int {
        val layoutType = tabBorrowerDataList[position]
        Log.e("is-footer", layoutType.isFooter.toString() )
        return if(layoutType.isFooter)
            R.layout.list_footer_borrower_horizontal
        else
            R.layout.list_borrower_horizontal
    }

    override fun onBindViewHolder(holder: BaseViewHolder, position: Int){ holder.bind(tabBorrowerDataList[position]) }

    override fun getItemCount() = tabBorrowerDataList.size







}