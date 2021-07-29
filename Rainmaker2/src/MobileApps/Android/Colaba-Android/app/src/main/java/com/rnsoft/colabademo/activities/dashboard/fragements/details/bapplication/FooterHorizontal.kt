package com.rnsoft.colabademo

import android.view.View
import android.widget.TextView
import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.items.AbstractItem


class FooterHorizontal : AbstractItem<FooterHorizontal.FooterViewHolder>() {
    var name: String = "Add more now..."


    /** defines the type defining this item. must be unique. preferably an id */

    override val type: Int = 1

    /** defines the layout which will be used for this item in the list */
    override val layoutRes: Int
        get() = R.layout.list_footer_borrower_horizontal

    override fun getViewHolder(v: View): FooterViewHolder {
        //v.layoutParams.width = (getScreenWidth(v.context) / totalItemsToBeDisplayed)
        return FooterViewHolder(v)
    }


    class FooterViewHolder(view: View) : FastAdapter.ViewHolder<FooterHorizontal>(view) {
        //var name: TextView = view.findViewById(R.id.addMoreTextView)


        override fun bindView(item: FooterHorizontal, payloads: List<Any>) {
           // name.text = item.name

        }

        override fun unbindView(item: FooterHorizontal) {
            //name.text = null

        }
    }
}