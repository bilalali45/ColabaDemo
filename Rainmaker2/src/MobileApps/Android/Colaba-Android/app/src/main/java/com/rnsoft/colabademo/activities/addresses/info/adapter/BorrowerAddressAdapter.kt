package com.rnsoft.colabademo

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.activities.addresses.info.AddressClickListener
import com.rnsoft.colabademo.databinding.ResidenceItemBinding
import java.util.ArrayList

/**
 * Created by Anita Kiran on 11/2/2021.
 */
class BorrowerAddressAdapter(var context: Context,clickListner: AddressClickListener) :
    RecyclerView.Adapter<BorrowerAddressAdapter.AddressViewHolder>() {

    var address: List<PrimaryBorrowerAddress> = arrayListOf()
    private var clickEvent: AddressClickListener = clickListner

    init {
        this.clickEvent = clickListner
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AddressViewHolder {
        var binding = ResidenceItemBinding.inflate(LayoutInflater.from(parent.context), parent,
            false)

        return AddressViewHolder(binding)
    }

    override fun onBindViewHolder(holder: AddressViewHolder, position: Int) {
        holder.bind(address.get(position), position)

        holder.itemView.setOnClickListener {
            clickEvent.onAddressClick(position)
        }

    }

    override fun getItemCount() = address.size

    inner class AddressViewHolder(val binding : ResidenceItemBinding) :
        RecyclerView.ViewHolder(binding.root) {

        fun bind(address: PrimaryBorrowerAddress, position: Int){
          // val desc =  address.get tion).street + " " + address.unit + "\n" + address.city + " " + address.stateName + " " + address.zipCode + " " + address.countryName
            binding.tvAddress.text = address.addressDesc

            if (address.isCurrentAddress) {
                binding.tvCurrentAddressHeading.setVisibility(View.VISIBLE)
                binding.tvResidenceDate.text = "From ".plus(address.fromDate)
                address.monthlyRent?.let {
                    if(it > 0){
                        binding.tvHomerent.text = "$".plus(address.monthlyRent.toString())
                        binding.tvHomerent.setVisibility(View.VISIBLE)
                    }
                }

            } else {
                binding.tvCurrentAddressHeading.setVisibility(View.GONE)
                binding.tvHomerent.setVisibility(View.GONE)
                //holder.tvDate.setText("From Aug 2019 to Nov 2020")
                binding.tvResidenceDate.text = "From ".plus(address.fromDate).plus(" to ").plus(address.toDate)
            }
        }
    }


    @SuppressLint("NotifyDataSetChanged")
    fun setTaskList(addressList: ArrayList<PrimaryBorrowerAddress>){
        address = addressList
        notifyDataSetChanged()
    }

}