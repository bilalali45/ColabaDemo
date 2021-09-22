package com.rnsoft.colabademo.activities.startapplication.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.Contacts
import com.rnsoft.colabademo.databinding.ContactListItemBinding

/**
 * Created by Anita Kiran on 9/20/2021.
 */
class ContactsAdapter(var context: Context, var contact: List<Contacts> = arrayListOf()) :
    RecyclerView.Adapter<ContactsAdapter.EpisodeViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): EpisodeViewHolder {
        var binding = ContactListItemBinding.inflate(
            LayoutInflater.from(parent.context),
            parent,
            false
        )
        return EpisodeViewHolder(binding)
    }

    override fun onBindViewHolder(holder: EpisodeViewHolder, position: Int) {
        holder.bind(contact.get(position), position)
    }

    override fun getItemCount() = contact.size

    inner class EpisodeViewHolder(val binding :ContactListItemBinding) :
        RecyclerView.ViewHolder(binding.root) {

        fun bind(contact: Contacts, position: Int) {
            binding.contactName.text = contact.contactName
            binding.contactEmail.text = contact.contactEmail
            binding.contactNum.text = contact.contactNumber
        }
    }
}