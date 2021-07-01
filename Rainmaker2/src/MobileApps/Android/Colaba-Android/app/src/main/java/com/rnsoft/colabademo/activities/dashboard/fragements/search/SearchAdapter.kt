package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView



class SearchAdapter
internal constructor(
    passedSearchList: List<SearchItem>, onSearchClickListener: SearchFragment) :  RecyclerView.Adapter<SearchAdapter.SearchViewHolder>() {

    private var searchList = listOf<SearchItem>()
    private var clickListener: SearchClickListener = onSearchClickListener

    init {
        this.searchList = passedSearchList
        this.clickListener = onSearchClickListener
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): SearchViewHolder  {
        val view: View = LayoutInflater.from(parent.context).inflate(R.layout.search_view_holder, parent, false)
        return SearchViewHolder(view)
    }


    override fun onBindViewHolder(holder: SearchViewHolder, position: Int) {

        val tenant:SearchItem  = searchList[position]

        holder.borrowerName.text = tenant.firstName+" "+tenant.lastName
        holder.borrowerId.text = tenant.loanNumber
        holder.borrowerStatus.text = tenant.status

        var tenantAddress = ""
        if( tenant.streetAddress!=null ) tenantAddress += tenant.streetAddress
        if( tenant.unitNumber!=null ) tenantAddress += tenant.unitNumber
        if( tenant.cityName!=null ) tenantAddress += tenant.cityName

        if( tenant.stateAbbreviation!=null  && tenant.stateAbbreviation.isNotEmpty())
            tenantAddress += "\n"+tenant.stateAbbreviation+", "
        if( tenant.countryName!=null ) tenantAddress += tenant.countryName+" "
        if( tenant.zipCode!=null ) tenantAddress += tenant.zipCode
        holder.borrowerAddress.text = tenantAddress


        //holder.notificationTime.text = singleNotification.notificationTime
        /*
        val word1 =  SpannableString("documents")
        val word2 =  SpannableString("has submitted");
        val fullMessage = singleNotification.notificationName

        val docsIndex = singleNotification.notificationName?.indexOf("documents",0, ignoreCase = true)

        fullMessage?.let { demoMessage->
            docsIndex?.let {
                //val extractName = SpannableString (demoMessage.substring(0,it-1))
                //extractName.setSpan( ForegroundColorSpan(Color.BLUE), 0, extractName.length, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE)
            }
        }
        word1.setSpan( ForegroundColorSpan(Color.BLUE), 0, word1.length, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE)
        word2.setSpan( ForegroundColorSpan(Color.RED), 0, word2.length, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE)

        val gatherString = fullMessage+word1+word2

        holder.notificationName.text = gatherString

         */
    }

    override fun getItemCount(): Int {
        return searchList.size
    }

    inner class SearchViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var borrowerName: TextView = itemView.findViewById(R.id.searchBorrowerName)
        var borrowerAddress: TextView = itemView.findViewById(R.id.searchBorrowerAddress)
        var borrowerId: TextView = itemView.findViewById(R.id.searchBorrowerId)
        var borrowerStatus: TextView = itemView.findViewById(R.id.searchBorrowerStatus)

    }



    interface SearchClickListener {
        fun onSearchItemClick( view:View)
    }


}