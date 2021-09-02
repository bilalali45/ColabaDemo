package com.rnsoft.colabademo

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView

class CustomBorrowerAdapter internal constructor(private var tabBorrowerDataList: ArrayList<BorrowersInformation> , onAdapterClickListener: AdapterClickListener) :
    RecyclerView.Adapter<CustomBorrowerAdapter.BaseViewHolder>(){

    private var classScopedItemClickListener: AdapterClickListener = onAdapterClickListener

    init {
        this.classScopedItemClickListener = onAdapterClickListener
    }

    abstract class BaseViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView)  { abstract fun bind(
        item: BorrowersInformation
    ) }
    //abstract class BaseViewHolder<TabBorrowerList>(itemView: View) : RecyclerView.ViewHolder(itemView) { abstract fun bind(item: TabBorrowerList) }

    inner class BorrowerItemViewHolder(itemView: View) : BaseViewHolder(itemView)  {
        private val coBorrowerNames : TextView = itemView.findViewById(R.id.co_borrower_test)
        private val mainBorrowerName:TextView = itemView.findViewById(R.id.main_borrower_test)
        override fun bind(item: BorrowersInformation) {
            if(item.owntypeId==1) {
                mainBorrowerName.text = item.firstName + " " + item.lastName
                coBorrowerNames.text = "Primary Borrower"
            }
            else{
                mainBorrowerName.text = item.firstName + " " + item.lastName
                coBorrowerNames.text = "Co-Borrower"
            }

        }

    }

    inner class BorrowerFooterViewHolder(itemView: View) : BaseViewHolder(itemView){

        var addBorrowerLayout: ConstraintLayout = itemView.findViewById(R.id.addBorrowerLayout)

        init {
            addBorrowerLayout.setOnClickListener {
                classScopedItemClickListener.navigateTo(adapterPosition)
            }
        }

        override fun bind(item: BorrowersInformation) {}

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
        //Log.e("is-footer", layoutType.isFooter.toString() )
        return if(layoutType.isFooter)
            R.layout.list_footer_borrower_horizontal
        else
            R.layout.list_borrower_horizontal
    }

    override fun onBindViewHolder(holder: BaseViewHolder, position: Int){ holder.bind(tabBorrowerDataList[position]) }

    override fun getItemCount() = tabBorrowerDataList.size







}