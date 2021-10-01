package com.rnsoft.colabademo.activities.startapplication.adapter

import android.annotation.SuppressLint
import android.content.Context
import android.graphics.Typeface
import android.text.Spannable
import android.text.SpannableStringBuilder
import android.text.style.StyleSpan
import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.Contacts
import com.rnsoft.colabademo.RecyclerviewClickListener
import com.rnsoft.colabademo.activities.addresses.info.model.Address
import com.rnsoft.colabademo.databinding.ContactListItemBinding

/**
 * Created by Anita Kiran on 9/20/2021.
 */
class ContactsAdapter(var context: Context,clickListner: RecyclerviewClickListener) :    //, var contact: List<Contacts> = arrayListOf()
    RecyclerView.Adapter<ContactsAdapter.EpisodeViewHolder>() {

    var contact: List<Contacts> = arrayListOf()
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
        holder.bind(contact.get(position), position)

        holder.itemView.setOnClickListener {
            clickEvent.onItemClick(position)
        }

    }

    override fun getItemCount() = contact.size

    inner class EpisodeViewHolder(val binding :ContactListItemBinding) :
        RecyclerView.ViewHolder(binding.root) {

        fun bind(contact: Contacts, position: Int) {
            binding.contactName.text = contact.contactName
            binding.contactEmail.text = contact.contactEmail
            binding.contactNum.text = contact.contactNumber


            /*binding.contactName.text.split(" ").forEach {
                if(it.equals("Richard", ignoreCase = false)){
                    //it.setTypeface(null, Typeface.NORMAL)
                }
            } */


//            val spannableStringBuilder = SpannableStringBuilder(contact.contactName)
//            spannableStringBuilder.setSpan(StyleSpan(Typeface.BOLD), Spannable.SPAN_EXCLUSIVE_EXCLUSIVE))
//            spannableStringBuilder.append(" ")
//            spannableStringBuilder.append(subText)
            //textView.text = spannableStringBuilder




        }

    }

    @SuppressLint("NotifyDataSetChanged")
    fun showResult(contact: List<Contacts>) {
        this.contact = contact
        notifyDataSetChanged()
    }
}