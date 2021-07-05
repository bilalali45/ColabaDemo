package com.ecommerce.testapp

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.*

const val totalItemsToBeDisplayed = 5
const val leftRightPadding = totalItemsToBeDisplayed * 6


class NewNotificationListAdapter internal constructor(
    private var passedList: ArrayList<NotificationItem>,
    private var notificationClickListener: NotificationClickListener
) :  RecyclerView.Adapter<NewNotificationListAdapter.BaseViewHolder>(){



    abstract class BaseViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) { abstract fun bind(
        item: NotificationItem
    ) }

    inner class ContentViewHolder(itemView: View, localClickListener: NotificationClickListener) : BaseViewHolder(
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
            this.contentClickListener = localClickListener
        }

        override fun onClick(v: View) {

            Log.e("onClick - ", v.toString())
            contentClickListener.getNotificationIndex(adapterPosition)
        }

        override fun bind(item: NotificationItem) {
            item.payload?.let{ payload->
                payload.payloadData?.let {  payloadData->
                    payloadData.name?.let {
                        notificationName.text = it
                    }
                    payloadData.dateTime?.let { activityTime->
                        var newString = activityTime.substring( 0 , activityTime.length-5)
                        newString+="Z"
                        Log.e("newString-",newString)
                        val newTime = AppSetting.returnNotificationTime(newString)
                        Log.e("newTime-",newTime)
                        notificationTime.text =  newTime
                    }

                    var tenantAddress = ""
                    if( payloadData.address!=null ) tenantAddress += payloadData.address
                    if( payloadData.unitNumber!=null ) tenantAddress += payloadData.unitNumber
                    if( payloadData.city!=null ) tenantAddress += payloadData.city

                    if( payloadData.state!=null  && payloadData.state.isNotEmpty())
                        tenantAddress += "\n"+payloadData.state+", "
                    //if( payloadData.countryName!=null ) tenantAddress += payloadData.countryName+" "
                    if( payloadData.zipCode!=null ) tenantAddress += payloadData.zipCode
                    //holder.borrowerAddress.text = tenantAddress

                }
            }

            if(item.status.equals( "Read", true)) {

                activeBookIcon.visibility = View.INVISIBLE
                activeCircleIcon.visibility = View.INVISIBLE
                nonActiveBookIcon.visibility = View.VISIBLE

            }
            else {
                activeBookIcon.visibility = View.VISIBLE
                activeCircleIcon.visibility = View.VISIBLE
                nonActiveBookIcon.visibility = View.INVISIBLE

            }


           //holder.productImage.loadImage("https://via.placeholder.com/150" )
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BaseViewHolder {
        val holder: BaseViewHolder?
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
        holder = ContentViewHolder(
            inflater.inflate(R.layout.notification_view_holder, parent, false),
            notificationClickListener
        )
        return holder
    }



    override fun getItemViewType(position: Int): Int {
        val notificationType = passedList[position]

        //return if(notificationType.isContent)
           return R.layout.notification_view_holder
        //else
          //  R.layout.notification_header_view_holder

    }

    override fun onBindViewHolder(holder: BaseViewHolder, position: Int)          {
        holder.bind(passedList[position])
    }



    override fun getItemCount() = passedList.size

    /*
    interface NewNotificationItemClickListener {
        fun onItemClick( view:View)
    }

     */


    /*

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
        override fun bind(item: NotificationItem) {
            notificationName.text = item.notificationName
             //productImage.loadImage("https://via.placeholder.com/150" )
        }
        override fun onClick(v: View) {
            headerClickListener.onItemClick(v)
        }
    }
     */

}