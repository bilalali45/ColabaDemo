package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.cardview.widget.CardView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView


class DocsAdapter
internal constructor(
    passedDocsList: ArrayList<DocItem>, onLoanItemClickListener: LoanItemClickListener
) :  RecyclerView.Adapter<DocsAdapter.DocsViewHolder>() {

    private var docsList = ArrayList<DocItem>()
    private var classScopedItemClickListener: LoanItemClickListener = onLoanItemClickListener

    init {
        this.docsList = passedDocsList
        this.classScopedItemClickListener = onLoanItemClickListener
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

    }

    override fun onBindViewHolder(holder: DocsViewHolder, position: Int) {
        val doc  = docsList[position]

        holder.docType.text = doc.docType

        holder.docUploadedTime.text = doc.docUploadedTime

        /*
        doc.docUploadedTime?.let { activityTime->
           var newString = activityTime.substring( 0 , activityTime.length-9)
            newString+="Z"
            newString = AppSetting.returnLongTimeNow(newString)
            holder.docUploadedTime.text =  newString
        }
         */


        if(doc.totalDocs != null && doc.totalDocs == 0) {
            holder.containsThreeChild.visibility = View.GONE
            holder.containsNoChild.visibility = View.VISIBLE
        }
        else {
            holder.containsThreeChild.visibility = View.VISIBLE
            holder.containsNoChild.visibility = View.GONE
            if(!doc.docOneName.isNullOrEmpty()) {
                holder.docOneName.text = doc.docOneName
                holder.docOneImage.visibility = View.VISIBLE
            }
            else
                holder.docOneLayout.visibility = View.INVISIBLE

            if(!doc.docTwoName.isNullOrEmpty()) {
                holder.docTwoName.text = doc.docTwoName
                holder.docTwoImage.visibility = View.VISIBLE
            }
            else
                holder.docTwoLayout.visibility = View.INVISIBLE

            doc.totalDocs?.let {
                if(it>2)
                    holder.docThreeName.text = "+"+(it.minus(2)).toString()
                else
                    holder.docThreeLayout.visibility = View.INVISIBLE
            }

        }
    }

    override fun getItemCount(): Int =  docsList.size






}