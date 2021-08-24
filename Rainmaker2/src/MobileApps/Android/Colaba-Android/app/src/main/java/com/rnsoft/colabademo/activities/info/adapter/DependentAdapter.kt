package com.rnsoft.colabademo.activities.info.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.R
import kotlinx.android.synthetic.main.dependent_input_field.view.*

/**
 * Created by Anita Kiran on 8/24/2021.
 */
class DependentAdapter (private val items: List<String>
) : RecyclerView.Adapter<DependentAdapter.DataViewHolder>() {

    class DataViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        fun bind(item: String) {
            itemView.til_dependent.setHint(item.plus( " Dependent Age (Years)"))
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) =
        DataViewHolder(
            LayoutInflater.from(parent.context).inflate(
                R.layout.dependent_input_field, parent,
                false
            )
        )

    override fun getItemCount(): Int = items.size

    override fun onBindViewHolder(holder: DataViewHolder, position: Int) =
        holder.bind(items[position])

    /*
    fun addData(list: List<ItemDetails>) {
        users.addAll(list)
    } */
}