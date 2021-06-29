package com.rnsoft.colabademo

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import androidx.recyclerview.widget.RecyclerView.ViewHolder
import com.zerobranch.layout.SwipeLayout

class LeftAdapter internal constructor(private val items: List<String>) :
    RecyclerView.Adapter<LeftAdapter.ItemHolder>() {
    override fun onCreateViewHolder(viewGroup: ViewGroup, viewType: Int): ItemHolder {
        return when (viewType) {
            0 -> ItemHolder(
                LayoutInflater.from(
                    viewGroup.context
                ).inflate(R.layout.layout_item_0, viewGroup, false)
            )
            1 -> ItemHolder(
                LayoutInflater.from(
                    viewGroup.context
                ).inflate(R.layout.layout_item_1, viewGroup, false)
            )
            else -> ItemHolder(
                LayoutInflater.from(
                    viewGroup.context
                ).inflate(R.layout.layout_item_7, viewGroup, false)
            )
        }
    }

    override fun onBindViewHolder(itemHolder: ItemHolder, position: Int) {
        itemHolder.dragItem.text = items[position]
    }

    override fun getItemViewType(position: Int): Int {
        return position
    }

    override fun getItemCount(): Int {
        return items.size
    }

    private fun remove(context: Context, position: Int) {
        Toast.makeText(context, "removed item $position", Toast.LENGTH_SHORT).show()
    }

    inner class ItemHolder(itemView: View) : ViewHolder(itemView) {
        var dragItem: TextView
        var rightView: ImageView?
        var rightTextView: TextView?
        var swipeLayout: SwipeLayout

        init {
            dragItem = itemView.findViewById(R.id.drag_item)
            swipeLayout = itemView.findViewById(R.id.swipe_layout)
            rightView = itemView.findViewById(R.id.right_view)
            rightTextView = itemView.findViewById(R.id.right_text_view)
            if (rightView != null) {
                rightView!!.setOnClickListener {
                    if (adapterPosition != RecyclerView.NO_POSITION) {
                        remove(itemView.context, adapterPosition)
                    }
                }
            }
            if (rightTextView != null) {
                rightTextView!!.setOnClickListener {
                    if (adapterPosition != RecyclerView.NO_POSITION) {
                        remove(itemView.context, adapterPosition)
                    }
                }
            }
            swipeLayout.setOnActionsListener(object : SwipeLayout.SwipeActionsListener {
                override fun onOpen(direction: Int, isContinuous: Boolean) {
                    if (direction == SwipeLayout.LEFT && isContinuous) {
                        if (adapterPosition != RecyclerView.NO_POSITION) {
                            remove(itemView.context, adapterPosition)
                        }
                    }
                }

                override fun onClose() {}
            })
        }
    }
}