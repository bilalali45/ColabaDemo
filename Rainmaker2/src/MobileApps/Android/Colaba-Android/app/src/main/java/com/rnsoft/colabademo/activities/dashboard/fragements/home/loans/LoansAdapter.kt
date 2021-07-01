package com.rnsoft.colabademo

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView


class LoansAdapter
internal constructor(
    passedBorrowerList: ArrayList<LoanItem>, onLoanItemClickListener: LoanItemClickListener
) :  RecyclerView.Adapter<LoansAdapter.LoanViewHolder>() {


    //: RecyclerView.Adapter<LoansAdapter.LoanViewHolder>() {

    private var borrowerList = ArrayList<LoanItem>()
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
        //holder.setIsRecyclable(false)

        val borrower  = borrowerList[position]

        holder.customerName.text = borrower.firstName+" "+borrower.lastName

        borrower.activityTime?.let { activityTime->

           var newString = activityTime.substring( 0 , activityTime.length-9)
            newString+="Z"



            newString = AppSetting.returnLongTimeNow(newString)

            //Log.e("newString",newString)

            holder.cardTime.text =  newString
        }
        //Log.d(TAG, "Date in milli :: FOR API >= 26 >>> $timeInMilliseconds")

        if(borrower.documents == null)
            holder.docsToReview.visibility = View.GONE
        else {
            holder.docsToReview.visibility = View.VISIBLE
            holder.docsToReview.text = borrower.documents.toString() + " Document to Review"
        }

        holder.mileStone.text = borrower.milestone

        holder.loanPurpose.text = borrower.loanPurpose
       
        if(borrower.coBorrowerCount!=null && borrower.coBorrowerCount>0)
            holder.coBorrowerCount.text = "+"+borrower.coBorrowerCount
        else
            holder.coBorrowerCount.visibility = View.GONE

        if(borrower.detail?.loanAmount!=null )
            holder.loanAmount.text = "$ "+borrower.detail?.loanAmount
        else
            holder.loanAmount.text = ""

        if(borrower.detail?.propertyValue!=null )
            holder.propertyValue.text = "$ "+borrower.detail?.propertyValue
        else
            holder.propertyValue.text = ""

        var addressFieldOne = ""
        var addressFieldTwo = ""

        borrower.detail?.let { details ->
            details.address?.let { address ->

                if( address.street!=null ) addressFieldOne += address.street
                if( address.unit!=null ) addressFieldOne += address.unit
                if( address.city!=null ) addressFieldOne += address.city

                if( address.stateName!=null  && address.stateName!="None") addressFieldTwo += address.stateName+", "
                if( address.countryName!=null ) addressFieldTwo += address.countryName+" "
                if( address.zipCode!=null ) addressFieldTwo += address.zipCode

            }
        }
        
        holder.addressOne.text = addressFieldOne
        holder.addressTwo.text = addressFieldTwo





        holder.openLoanImageView.setOnClickListener {
            //mOnProductListener.onItemClick(holder.loanDetailLayout)
            holder.loanDetailLayout.visibility = View.VISIBLE
            holder.closeLoanImageView.visibility = View.VISIBLE
            holder.openLoanImageView.visibility = View.INVISIBLE
            borrower.recycleCardState = true
        }

        holder.closeLoanImageView.setOnClickListener {
            //mOnProductListener.onItemClick(holder.loanDetailLayout)
            holder.loanDetailLayout.visibility = View.GONE
            holder.closeLoanImageView.visibility = View.INVISIBLE
            holder.openLoanImageView.visibility = View.VISIBLE
            borrower.recycleCardState = false
        }


        if(borrower.recycleCardState == true)
            holder.openLoanImageView.performClick()
        else
            holder.closeLoanImageView.performClick()


    }

    override fun getItemCount(): Int {

        return borrowerList.size
    }

    inner class LoanViewHolder(itemView: View ) : RecyclerView.ViewHolder(itemView), View.OnClickListener {

        var customerName: TextView = itemView.findViewById(R.id.customer_name_textfield)
        var loanPurpose: TextView = itemView.findViewById(R.id.loanPurposeTextView)
        var mileStone: TextView = itemView.findViewById(R.id.milestoneTextView)
        var cardTime: TextView = itemView.findViewById(R.id.cardTimeTextView)
        var docsToReview: TextView = itemView.findViewById(R.id.docsToReviewTextView)
        var coBorrowerCount: TextView = itemView.findViewById(R.id.coBorrowerCountTextView)

        var loanAmount: TextView = itemView.findViewById(R.id.loanAmountTextView)
        var propertyValue: TextView = itemView.findViewById(R.id.propertyValueTextView)

        var addressOne: TextView = itemView.findViewById(R.id.addressOne)
        var addressTwo: TextView = itemView.findViewById(R.id.addressTwo)

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

    /*
    override fun getItemId(position: Int): Long {
        return position.toLong()
    }

    override fun getItemViewType(position: Int): Int {
        return position
    }
    */




}