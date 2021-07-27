package com.rnsoft.colabademo

import android.content.Context
import android.graphics.Point
import android.view.View
import android.view.WindowManager
import android.widget.TextView

import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.items.AbstractItem

open class RealStateHorizontal : AbstractItem<RealStateHorizontal.ViewHolder>() {
    var propertyAddress: String? = null
    var propertyType: String? = null

    /** defines the type defining this item. must be unique. preferably an id */

    override val type: Int = 1

    /** defines the layout which will be used for this item in the list */
    override val layoutRes: Int
        get() = R.layout.list_realstate_horizontal

    override fun getViewHolder(v: View): ViewHolder {

        return ViewHolder(v)
    }


    class ViewHolder(view: View) : FastAdapter.ViewHolder<RealStateHorizontal>(view) {
        var propertyAddress: TextView = view.findViewById(R.id.propertyAddress)
        var propertyType: TextView = view.findViewById(R.id.propertyType)

        override fun bindView(item: RealStateHorizontal, payloads: List<Any>) {
            propertyAddress.text = item.propertyAddress
            propertyType.text = item.propertyType
        }

        override fun unbindView(item: RealStateHorizontal) {
            propertyAddress.text = null
            propertyType.text = null
        }
    }
}