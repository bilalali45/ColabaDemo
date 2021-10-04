package com.rnsoft.colabademo.activities.requestdocs.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import android.widget.TextView
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.activities.requestdocs.model.Template

/**
 * Created by Anita Kiran on 10/4/2021.
 */
class EmailTemplateSpinnerAdapter(val context: Context, var dataSource: List<Template>) : BaseAdapter() {

    private val inflater: LayoutInflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {

        val view: View
        val viewHolder: ItemHolder
        if (convertView == null) {
            view = inflater.inflate(R.layout.email_template_item, parent, false)
            viewHolder = ItemHolder(view)
            view?.tag = viewHolder
        } else {
            view = convertView
            viewHolder = view.tag as ItemHolder
        }
        viewHolder.type.text = dataSource.get(position).docType
        viewHolder.desc.text = dataSource.get(position).docDesc


        return view
    }

    override fun getItem(position: Int): Any? {
        return dataSource[position];
    }

    override fun getCount(): Int {
        return dataSource.size;
    }

    override fun getItemId(position: Int): Long {
        return position.toLong();
    }

    private class ItemHolder(row: View?) {
        val type: TextView
        val desc: TextView

        init {
            type = row?.findViewById(R.id.tv_template_type) as TextView
            desc = row?.findViewById(R.id.tv_template_desc) as TextView
        }
    }

}