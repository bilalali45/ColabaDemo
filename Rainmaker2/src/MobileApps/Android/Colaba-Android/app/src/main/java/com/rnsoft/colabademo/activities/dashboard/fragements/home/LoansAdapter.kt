package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import java.util.*



class LoansAdapter(passedBorrowerList: List<Borrower>) : RecyclerView.Adapter<LoansAdapter.LoanViewHolder>() {

    private var borrowerList = listOf<Borrower>()

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): LoanViewHolder {
        val view: View = LayoutInflater.from(parent.context).inflate(R.layout.loan_view_holder, parent, false)
        return LoanViewHolder(view)
    }

    override fun onBindViewHolder(holder: LoanViewHolder, position: Int) {
        //holder.customer_name_textfield.text = arrayList[position]
        val borrower  = borrowerList[position]
        holder.customerName.text = borrower.borrowerName
    }

    override fun getItemCount(): Int {
        return borrowerList.size
    }

    inner class LoanViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var customerName: TextView = itemView.findViewById(R.id.customer_name_textfield)

    }

    init {
        this.borrowerList = passedBorrowerList
    }
}