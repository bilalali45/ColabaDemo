package com.ecommerce.testapp

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.NotificationClickListener
import com.rnsoft.colabademo.NotificationModel
import com.rnsoft.colabademo.R

const val totalItemsToBeDisplayed = 5
const val leftRightPadding = totalItemsToBeDisplayed * 6


class NewNotificationListAdapter internal constructor(
    private var passedList: List<NotificationModel>,
    private val notificationClickListener: NotificationClickListener
) :  RecyclerView.Adapter<NewNotificationListAdapter.BaseViewHolder>(){


    //private val notificationClickListener: NotificationItemClickListener = notificationClickListener

    abstract class BaseViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) { abstract fun bind(
        item: NotificationModel
    ) }

    inner class ContentViewHolder(itemView: View, globalOnProductListener: NotificationClickListener) : BaseViewHolder(
        itemView
    ), View.OnClickListener {

        var notificationName: TextView = itemView.findViewById(R.id.notification_name)
        var notificationTime: TextView = itemView.findViewById(R.id.notification_time)
        var activeCircleIcon: ImageView = itemView.findViewById(R.id.circle_icon)
        var activeBookIcon: ImageView = itemView.findViewById(R.id.activeBookIcon)
        var nonActiveBookIcon: ImageView = itemView.findViewById(R.id.nonActiveBookIcon)

        private var contentClickListener: NotificationClickListener

        init {
            itemView.setOnClickListener(this)
            this.contentClickListener = globalOnProductListener
        }

        override fun onClick(v: View?) {
            Log.e("onClick - ", v.toString())
            //notificationClickListener.onItemClick(v)
        }

        override fun bind(item: NotificationModel) {
            notificationName.text = item.notificationName
            notificationTime.text = item.notificationTime
            if(item.notificationActive == true) {
                activeBookIcon.visibility = View.VISIBLE
                activeCircleIcon.visibility = View.VISIBLE
                nonActiveBookIcon.visibility = View.INVISIBLE
            }
            else {
                activeBookIcon.visibility = View.INVISIBLE
                activeCircleIcon.visibility = View.INVISIBLE
                nonActiveBookIcon.visibility = View.VISIBLE
            }


           //holder.productImage.loadImage("https://via.placeholder.com/150" )
        }
    }

    inner class HeaderViewHolder(
        itemView: View,
        globalOnProductListener: NotificationClickListener
    ) : BaseViewHolder(itemView), View.OnClickListener {
        //val productImage:ImageView = itemView.product_image
        private val notificationName : TextView= itemView.findViewById(R.id.notificationHeaderTextView)

        private var headerClickListener: NotificationClickListener
        init {
            itemView.setOnClickListener(this)
            this.headerClickListener = globalOnProductListener
        }
        override fun bind(item: NotificationModel) {
            notificationName.text = item.notificationName
             //productImage.loadImage("https://via.placeholder.com/150" )
        }
        override fun onClick(v: View) {
            headerClickListener.onItemClick(v)
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BaseViewHolder {
        val holder: BaseViewHolder?
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
       if(viewType == R.layout.notification_view_holder) {
           holder =  ContentViewHolder(
               inflater.inflate(R.layout.notification_view_holder, parent, false),
               notificationClickListener
           )
       }
        else {
           holder= HeaderViewHolder(
               inflater.inflate(
                   R.layout.notification_header_view_holder,
                   parent,
                   false
               ), notificationClickListener
           )
        }
        return holder
    }


    override fun getItemViewType(position: Int): Int {
        val notificationType = passedList[position]

        return if(notificationType.isContent == true)
            R.layout.notification_view_holder
        else
            R.layout.notification_header_view_holder

    }

    override fun onBindViewHolder(holder: BaseViewHolder, position: Int)          {
        holder.bind(passedList[position])
    }

    internal fun setAllItems(itemList: List<NotificationModel>) {
        this.passedList = itemList
        notifyDataSetChanged()
    }

    override fun getItemCount() = passedList.size

    /*
    interface NewNotificationItemClickListener {
        fun onItemClick( view:View)
    }

     */

}