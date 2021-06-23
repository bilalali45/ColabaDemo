package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView



class SearchAdapter
internal constructor(
    passedSearchList: List<SearchModel>, onSearchClickListener: SearchFragment
) :  RecyclerView.Adapter<SearchAdapter.SearchViewHolder>() {


    //: RecyclerView.Adapter<LoansAdapter.LoanViewHolder>() {

    private var searchList = listOf<SearchModel>()
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
        //holder.customer_name_textfield.text = arrayList[position]
        val singleNotification  = searchList[position]

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
        //var notificationName: TextView = itemView.findViewById(R.id.notification_name)
        //var notificationTime: TextView = itemView.findViewById(R.id.notification_time)
        //var activeImage: ImageView = itemView.findViewById(R.id.notification_active_icon)




        //fun bind(item: ConstraintLayout, listener: OnItemClickListener) {
            //itemView.setOnClickListener { listener.onItemClick(item) }
       // }



    }



    interface SearchClickListener {
        fun onSearchItemClick( view:View)
    }


}