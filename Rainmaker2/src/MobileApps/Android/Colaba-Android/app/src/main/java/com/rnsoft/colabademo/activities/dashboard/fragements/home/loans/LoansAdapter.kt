package com.rnsoft.colabademo.activities.dashboard.fragements.home.loans

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.Borrower
import com.rnsoft.colabademo.R


class LoansAdapter
internal constructor(
    passedBorrowerList: List<Borrower>, onLoanItemClickListener: LoanItemClickListener
) :  RecyclerView.Adapter<LoansAdapter.LoanViewHolder>() {


    //: RecyclerView.Adapter<LoansAdapter.LoanViewHolder>() {

    private var borrowerList = listOf<Borrower>()
    private var classScopedItemClickListener: LoanItemClickListener = onLoanItemClickListener

    init {
        this.borrowerList = passedBorrowerList
        this.classScopedItemClickListener = onLoanItemClickListener
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): LoanViewHolder {
        //val view: View = LayoutInflater.from(parent.context).inflate(R.layout.loan_view_holder, parent, false)
        //return LoanViewHolder(view)
        val holder: LoanViewHolder
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
        holder = LoanViewHolder(inflater.inflate(R.layout.loan_view_holder, parent, false))
        return holder

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

    inner class LoanViewHolder(itemView: View ) : RecyclerView.ViewHolder(itemView), View.OnClickListener {
        var customerName: TextView = itemView.findViewById(R.id.customer_name_textfield)
        var openLoanImageView: ImageView = itemView.findViewById(R.id.open_inside_view)
        var closeLoanImageView: ImageView = itemView.findViewById(R.id.close_inside_view)
        var dotsImageView: ImageView = itemView.findViewById(R.id.loandotsImageView)
        var loanDetailLayout: ConstraintLayout = itemView.findViewById(R.id.inside_Loan_layout)
        init {
            dotsImageView.setOnClickListener(this)
        }
        override fun onClick(v: View?) {
            classScopedItemClickListener.getCardIndex(adapterPosition)
        }

        //fun bind(item: ConstraintLayout, listener: OnItemClickListener) {
            //itemView.setOnClickListener { listener.onItemClick(item) }
       // }



    }






}