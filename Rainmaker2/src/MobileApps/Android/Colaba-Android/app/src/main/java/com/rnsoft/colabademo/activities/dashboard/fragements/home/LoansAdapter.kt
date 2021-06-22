package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView



class LoansAdapter
internal constructor(
    passedBorrowerList: List<Borrower>, onProductListener: LoanFragment
) :  RecyclerView.Adapter<LoansAdapter.LoanViewHolder>() {


    //: RecyclerView.Adapter<LoansAdapter.LoanViewHolder>() {

    private var borrowerList = listOf<Borrower>()
    private var mOnProductListener: OnItemClickListener = onProductListener

    init {
        this.borrowerList = passedBorrowerList
        this.mOnProductListener = onProductListener
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): LoanViewHolder  {
        val view: View = LayoutInflater.from(parent.context).inflate(R.layout.loan_view_holder, parent, false)
        return LoanViewHolder(view)
    }




    override fun onBindViewHolder(holder: LoanViewHolder, position: Int) {
        //holder.customer_name_textfield.text = arrayList[position]
        val borrower  = borrowerList[position]
        holder.customerName.text = borrower.borrowerName

        holder.openLoanImageView.setOnClickListener {
            //mOnProductListener.onItemClick(holder.loanDetailLayout)
            holder.loanDetailLayout.visibility = View.VISIBLE
            holder.closeLoanImageView.visibility = View.VISIBLE
            holder.openLoanImageView.visibility = View.INVISIBLE
        }

        holder.closeLoanImageView.setOnClickListener {
            //mOnProductListener.onItemClick(holder.loanDetailLayout)
            holder.loanDetailLayout.visibility = View.GONE
            holder.closeLoanImageView.visibility = View.INVISIBLE
            holder.openLoanImageView.visibility = View.VISIBLE

        }




    }

    override fun getItemCount(): Int {
        return borrowerList.size
    }

    inner class LoanViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var customerName: TextView = itemView.findViewById(R.id.customer_name_textfield)
        var openLoanImageView: ImageView = itemView.findViewById(R.id.open_inside_view)
        var closeLoanImageView: ImageView = itemView.findViewById(R.id.close_inside_view)
        var loanDetailLayout: ConstraintLayout = itemView.findViewById(R.id.inside_Loan_layout)

        //fun bind(item: ConstraintLayout, listener: OnItemClickListener) {
            //itemView.setOnClickListener { listener.onItemClick(item) }
       // }



    }



    interface OnItemClickListener {
        fun onItemClick( testLayout:ConstraintLayout)
    }


}