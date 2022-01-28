package com.rnsoft.colabademo

import android.annotation.SuppressLint
import android.graphics.Typeface
import android.telephony.PhoneNumberUtils
import android.text.Spannable
import android.text.SpannableStringBuilder
import android.text.style.StyleSpan
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.databinding.SearchedContactsItemBinding
import kotlinx.android.synthetic.main.searched_contacts_item.view.*
import java.lang.Exception
import java.util.ArrayList

/**
 * Created by Anita Kiran on 1/19/2022.
 */
class SearchContactAdapter(private val items: ArrayList<SearchResultResponseItem>, clickListner: SearchItemClickListener) :
    RecyclerView.Adapter<SearchContactAdapter.SearchViewHolder>() {

    private var onItemClick: SearchItemClickListener = clickListner
    private var searchResultResponseItemList: List<SearchResultResponseItem> = arrayListOf()
    private var searchKeyword = "richard"

    init {
        this.onItemClick = clickListner
    }

    inner class SearchViewHolder(val binding : SearchedContactsItemBinding) : RecyclerView.ViewHolder(binding.root) {
        fun bind(model: SearchResultResponseItem, position: Int) {
            binding.searchBorrowerName.text = (model.firstName + " " + model.lastName)
            binding.searchEmail.text = model.emailAddress
            try {
                model.mobileNumber?.let {
                    if (it.length > 0 ) {
                        val phoneNumber =  PhoneNumberUtils.formatNumber(it, "US")
                        binding.searchContactPhone.setText(phoneNumber)
                        binding.searchContactPhone.visibility = View.VISIBLE
                    } else
                        binding.searchContactPhone.visibility = View.GONE
                } ?: run {
                    binding.searchContactPhone.visibility = View.GONE
                }
            } catch (e: Exception){ }

            var sentence = binding.searchBorrowerName.text.toString()
            var startIndex = sentence.indexOf(searchKeyword, 0, true)
            var endIndex = startIndex + searchKeyword.length
            var str = SpannableStringBuilder(sentence)
            if (startIndex >= 0) {
                str.setSpan(
                    StyleSpan(Typeface.BOLD),
                    startIndex,
                    endIndex,
                    Spannable.SPAN_EXCLUSIVE_EXCLUSIVE
                )
            }
            binding.searchBorrowerName.text = str


            sentence = binding.searchEmail.text.toString()
            startIndex = sentence.indexOf(searchKeyword, 0, true)
            endIndex = startIndex + searchKeyword.length
            str = SpannableStringBuilder(sentence)
            if (startIndex >= 0) {
                str.setSpan(
                    StyleSpan(Typeface.BOLD),
                    startIndex,
                    endIndex,
                    Spannable.SPAN_EXCLUSIVE_EXCLUSIVE
                )
            }
            binding.searchEmail.text = str
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) :  SearchViewHolder {
        var binding = SearchedContactsItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return SearchViewHolder(binding)
    }

    override fun getItemCount(): Int = searchResultResponseItemList.size

    override fun onBindViewHolder(holder: SearchContactAdapter.SearchViewHolder, position: Int) {
        holder.bind(searchResultResponseItemList.get(position), position)

        holder.itemView.search_contact_phone.setOnClickListener {
            onItemClick.onPhoneClick(position)
        }

        holder.itemView.search_email.setOnClickListener {
            onItemClick.onEmailClick(position)
        }
    }

    @SuppressLint("NotifyDataSetChanged")
    fun showResult(contact: ArrayList<SearchResultResponseItem> , searchKey:String) {
        searchKeyword = searchKey
        this.searchResultResponseItemList = contact
        notifyDataSetChanged()
    }


}