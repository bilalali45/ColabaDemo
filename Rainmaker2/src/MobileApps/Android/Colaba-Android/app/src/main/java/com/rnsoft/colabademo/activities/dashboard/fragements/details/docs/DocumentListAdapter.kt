package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView



class DocumentListAdapter
internal constructor(
    passedDocsList: ArrayList<DocItem>, onLoanItemClickListener: LoanItemClickListener
) :  RecyclerView.Adapter<DocumentListAdapter.DocInnerListViewHolder>() {

    private var docsList = ArrayList<DocItem>()
    private var classScopedItemClickListener: LoanItemClickListener = onLoanItemClickListener

    init {
        this.docsList = passedDocsList
        this.classScopedItemClickListener = onLoanItemClickListener
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DocInnerListViewHolder {
        val holder: DocInnerListViewHolder
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
        holder = DocInnerListViewHolder(inflater.inflate(R.layout.detail_list_inner_view_holder, parent, false))
        return holder
    }

    inner class DocInnerListViewHolder(itemView: View ) : RecyclerView.ViewHolder(itemView) {
        var docName: TextView = itemView.findViewById(R.id.doc_name)
        var docUploadedTime: TextView = itemView.findViewById(R.id.doc_uploaded_time)
        var docImage: ImageView = itemView.findViewById(R.id.doc_image)

    }

    override fun onBindViewHolder(holder: DocInnerListViewHolder, position: Int) {
        val doc  = docsList[position]
        holder.docName.text = doc.docType
        holder.docUploadedTime.text = doc.docUploadedTime
        //holder.docImage = Glide.
    }

    override fun getItemCount(): Int =  docsList.size


}