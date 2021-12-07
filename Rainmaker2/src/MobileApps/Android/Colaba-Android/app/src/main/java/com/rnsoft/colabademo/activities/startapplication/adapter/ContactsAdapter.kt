package com.rnsoft.colabademo.activities.startapplication.adapter

import android.annotation.SuppressLint
import android.content.Context
import android.graphics.Typeface
import android.text.Spannable
import android.text.SpannableStringBuilder
import android.text.style.StyleSpan
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.RecyclerviewClickListener
import com.rnsoft.colabademo.SearchResultResponseItem
import com.rnsoft.colabademo.databinding.ContactListItemBinding
import java.util.HashMap

class ContactsAdapter(var context: Context,clickListner: RecyclerviewClickListener) :    //, var contact: List<Contacts> = arrayListOf()
    RecyclerView.Adapter<ContactsAdapter.EpisodeViewHolder>() {

    private var searchResultResponseItemList: List<SearchResultResponseItem> = arrayListOf()
    private var clickEvent: RecyclerviewClickListener = clickListner

    init {
        this.clickEvent = clickListner
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): EpisodeViewHolder {
        var binding = ContactListItemBinding.inflate(LayoutInflater.from(parent.context), parent,
            false)


        return EpisodeViewHolder(binding)
    }

    override fun onBindViewHolder(holder: EpisodeViewHolder, position: Int) {
        holder.bind(searchResultResponseItemList.get(position), position)

        holder.itemView.setOnClickListener {
            clickEvent.onItemClick(position)
        }

    }

    override fun getItemCount() = searchResultResponseItemList.size

    inner class EpisodeViewHolder(val binding :ContactListItemBinding) :
        RecyclerView.ViewHolder(binding.root) {

        fun bind(contact: SearchResultResponseItem, position: Int) {
            binding.contactName.text = contact.firstName
            binding.contactEmail.text = contact.emailAddress
            binding.contactNum.text = contact.mobileNumber

            var search = "richard"
            val searchMap: HashMap<String,List<String>> = HashMap()
            val values : List<String> = binding.contactName.text.split(" ")
            for (i in 0 until values.size) {
                 val singleWord = values[i]
                 if(singleWord.equals(search, ignoreCase = true)) {
                     //searchMap.put("", values)

                     for (j in 0 until values.size) {
                         val sentence = values[j]
                         val startIndex = sentence.indexOf(search, 0, true)
                         val endIndex = startIndex + singleWord.length
                         val str = SpannableStringBuilder(singleWord)
                         if (startIndex >= 0) {
                             str.setSpan(
                                 StyleSpan(Typeface.BOLD),
                                 startIndex,
                                 endIndex,
                                 Spannable.SPAN_EXCLUSIVE_EXCLUSIVE
                             )
                         }

                         //binding.contactName.text = str
                     }
                 }
            }

        }

    }

    @SuppressLint("NotifyDataSetChanged")
    fun showResult(contact: ArrayList<SearchResultResponseItem>) {
        this.searchResultResponseItemList = contact
        notifyDataSetChanged()
    }
}