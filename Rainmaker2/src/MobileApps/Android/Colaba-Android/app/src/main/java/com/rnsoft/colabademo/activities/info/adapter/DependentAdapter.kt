package com.rnsoft.colabademo.activities.info.adapter

import android.content.Context
import android.content.res.ColorStateList
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.DocsViewClickListener
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.RecyclerviewClickListener
import kotlinx.android.synthetic.main.dependent_input_field.view.*

/**
 * Created by Anita Kiran on 8/24/2021.
 */
class DependentAdapter (val mContext : Context, private val items: ArrayList<String> , clickListner: RecyclerviewClickListener) : RecyclerView.Adapter<DependentAdapter.DataViewHolder>() {

    private var deleteClick: RecyclerviewClickListener = clickListner

    init {
        this.deleteClick = clickListner
    }

    class DataViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        fun bind(item: String) {
            itemView.til_dependent.setHint(item.plus( " Dependent Age (Years)"))

            itemView.ed_age.setOnFocusChangeListener { view, hasFocus ->
                if (hasFocus) {
                    itemView.til_dependent.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(itemView.context,R.color.grey_color_two))
                    itemView.til_dependent.setEndIconDrawable(R.drawable.ic_minus_delete)
                }
                else {
                    itemView.til_dependent.setEndIconDrawable(null)
                    if (itemView.ed_age.text?.length == 0) {
                        itemView.til_dependent.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(itemView.context,R.color.grey_color_three))
                    } else {
                        itemView.til_dependent.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(itemView.context,R.color.grey_color_two))
                    }
                }
            }


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

    override fun onBindViewHolder(holder: DataViewHolder, position: Int) {
        holder.bind(items[position])


        holder.itemView.til_dependent.setEndIconOnClickListener {
            Log.e("icon", "click")
            deleteClick.deleteItemClick(position)

        //items.removeAt(position)
            //notifyItemRemoved(position)
            //notifyDataSetChanged()
        }
    }


}