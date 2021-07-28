package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.cardview.widget.CardView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView
import java.text.SimpleDateFormat
import java.util.*
import kotlin.collections.ArrayList

class BorrowerDocumentAdapter
internal constructor(
    passedDocsList: ArrayList<BorrowerDocsModel>, onAdapterClickListener: AdapterClickListener
) :  RecyclerView.Adapter<BorrowerDocumentAdapter.DocsViewHolder>() {

    private var docsList = ArrayList<BorrowerDocsModel>()
    private var classScopedItemClickListener: AdapterClickListener = onAdapterClickListener

    init {
        this.docsList = passedDocsList
        this.classScopedItemClickListener = onAdapterClickListener
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DocsViewHolder {
        val holder: DocsViewHolder
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
        holder = DocsViewHolder(inflater.inflate(R.layout.doc_view_holder_layout, parent, false))
        return holder
    }

    inner class DocsViewHolder(itemView: View ) : RecyclerView.ViewHolder(itemView) {

        var containsThreeChild: ConstraintLayout = itemView.findViewById(R.id.containsThreeChild)
        var containsNoChild: ConstraintLayout = itemView.findViewById(R.id.containsNoChild)

        var docType: TextView = itemView.findViewById(R.id.doc_type)
        var docUploadedTime: TextView = itemView.findViewById(R.id.doc_uploaded_time)

        var docOneLayout:ConstraintLayout = itemView.findViewById(R.id.doc_one)
        var docOneName: TextView = itemView.findViewById(R.id.doc_one_name)
        var docOneImage: ImageView = itemView.findViewById(R.id.doc_one_image)

        var docTwoLayout:ConstraintLayout = itemView.findViewById(R.id.doc_two)
        var docTwoName: TextView = itemView.findViewById(R.id.doc_two_name)
        var docTwoImage: ImageView = itemView.findViewById(R.id.doc_two_image)

        var docThreeLayout:ConstraintLayout = itemView.findViewById(R.id.doc_three)
        var docThreeName: TextView = itemView.findViewById(R.id.doc_three_name)

        var docCardView: CardView = itemView.findViewById(R.id.docCardView)

        init {
            docCardView.setOnClickListener{
                classScopedItemClickListener.navigateTo(adapterPosition)
            }
        }

    }

    override fun onBindViewHolder(holder: DocsViewHolder, position: Int) {
        val doc  = docsList[position]

        holder.docType.text = doc.docName



        doc.createdOn.let { activityTime->
            //var newString = activityTime.substring( 0 , activityTime.length-5)
            //newString+="Z"
            val newString = returnDocCreatedTime(activityTime)
            holder.docUploadedTime.text =  newString
        }




        if(doc.subFiles.isEmpty()){
            holder.containsNoChild.visibility = View.VISIBLE
            holder.containsThreeChild.visibility = View.GONE
        }
        else
        if(doc.subFiles.isNotEmpty()) {
            holder.containsThreeChild.visibility = View.VISIBLE
            holder.containsNoChild.visibility = View.GONE

            val fileOne = doc.subFiles[0]
            if(fileOne.clientName.isNotEmpty() && fileOne.clientName.isNotBlank()) {
                holder.docOneName.text = fileOne.clientName
                holder.docOneImage.visibility = View.VISIBLE
            }
            else {
                holder.docOneName.text = fileOne.mcuName
                holder.docOneImage.visibility = View.VISIBLE
            }



            var fileTwo:SubFiles? = null
            if(doc.subFiles.size>1)
                fileTwo = doc.subFiles[1]

            if(fileTwo!=null) {
                if (fileTwo.clientName.isNotEmpty() && fileTwo.clientName.isNotBlank()) {
                    holder.docTwoLayout.visibility = View.VISIBLE
                    holder.docTwoName.text = fileTwo.clientName
                    holder.docTwoImage.visibility = View.VISIBLE
                } else if (fileTwo.mcuName.isNotEmpty() && fileTwo.mcuName.isNotBlank()) {
                    holder.docTwoLayout.visibility = View.VISIBLE
                    holder.docTwoName.text = fileTwo.mcuName
                    holder.docTwoImage.visibility = View.VISIBLE
                } else {
                    holder.docTwoLayout.visibility = View.INVISIBLE
                }
            }


            if(doc.subFiles.size>2) {
                holder.docThreeLayout.visibility = View.VISIBLE
                holder.docThreeName.text = "+" + (doc.subFiles.size.minus(2)).toString()
            }
            else
                holder.docThreeLayout.visibility = View.INVISIBLE


        }
    }

    override fun getItemCount(): Int =  docsList.size

    private fun returnDocCreatedTime(input:String):String{

        var lastSeen = input

        val formatter = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", Locale.US)
        val oldDate: Date? = formatter.parse(input)


        val oldMillis = oldDate?.time

        oldMillis?.let {
            //Log.e("oldMillis", "Date in milli :: FOR API >= 26 >>> $oldMillis")
            //Log.e("lastseen", "Date in milli :: FOR API >= 26 >>>"+ lastseen(oldMillis))
            lastSeen = AppSetting.lastseen(oldMillis)
        }

        return lastSeen

    }




}