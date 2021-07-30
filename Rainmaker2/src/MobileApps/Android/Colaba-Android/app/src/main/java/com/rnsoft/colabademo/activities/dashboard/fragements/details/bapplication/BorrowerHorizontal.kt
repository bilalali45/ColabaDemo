package com.rnsoft.colabademo

import android.content.Context
import android.graphics.Point
import android.util.Log
import android.view.View
import android.view.WindowManager
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.items.AbstractItem

open class BorrowerHorizontal(private val isLastItem: Boolean = true ) : AbstractItem<BorrowerHorizontal.ViewHolder>() {
    var name: String? = null
    var coName: String? = null



    /** defines the type defining this item. must be unique. preferably an id */

    override val type: Int = 1

    /** defines the layout which will be used for this item in the list */
    override val layoutRes: Int
        get() = R.layout.list_borrower_horizontal



    override fun getViewHolder(v: View): ViewHolder {
        //v.layoutParams.width = (getScreenWidth(v.context) / totalItemsToBeDisplayed)
        Log.e("getViewHolder", isLastItem.toString())
        return ViewHolder(v, isLastItem)
    }

    fun getScreenWidth(context: Context): Int {
        val wm = context.getSystemService(Context.WINDOW_SERVICE) as WindowManager
        val display = wm.defaultDisplay
        val size = Point()
        display.getSize(size)
        return size.x
    }

    class ViewHolder(view: View, private val isLastItem:Boolean = true) : FastAdapter.ViewHolder<BorrowerHorizontal>(view) {
        var name: TextView = view.findViewById(R.id.main_borrower_test)
        var coName: TextView = view.findViewById(R.id.co_borrower_test)
        private val dataCellConstraintLayout:ConstraintLayout = view.findViewById(R.id.data_cell) as ConstraintLayout
        //val lastCellConstraintLayout:ConstraintLayout = view.findViewById(R.id.last_cell) as ConstraintLayout



        override fun bindView(item: BorrowerHorizontal, payloads: List<Any>) {
            name.text = item.name
            coName.text = item.coName
            Log.e("is-last-item", isLastItem.toString())

        }

        override fun unbindView(item: BorrowerHorizontal) {
            name.text = null
            coName.text = null
        }
    }
}