package com.rnsoft.colabademo

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.cardview.widget.CardView
import androidx.recyclerview.widget.RecyclerView



class DocumentListAdapter
internal constructor(
    passedDocsList: ArrayList<SubFiles>, onAdapterClickListener: AdapterClickListener
) :  RecyclerView.Adapter<DocumentListAdapter.DocInnerListViewHolder>() {

    private var docsList = ArrayList<SubFiles>()
    private var classScopedItemClickListener: AdapterClickListener = onAdapterClickListener

    init {
        this.docsList = passedDocsList
        this.classScopedItemClickListener = onAdapterClickListener
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
        var fileCardView:CardView = itemView.findViewById(R.id.fileCardView)
        init {
            fileCardView.setOnClickListener{
                classScopedItemClickListener.navigateTo(adapterPosition)
            }
        }

    }

    override fun onBindViewHolder(holder: DocInnerListViewHolder, position: Int) {
        val doc  = docsList[position]
        val docName = if(doc.clientName.isEmpty() || doc.clientName.isBlank()) doc.mcuName else doc.clientName
        holder.docName.text = docName
        val docType = getDocType(docName)
        setDocImage(docType,holder.docImage)
         /* if(doc.clientName.isEmpty() || doc.clientName.isBlank())
            holder.docName.text = doc.mcuName
        else
            holder.docName.text = doc.clientName
            holder.docUploadedTime.text = doc.fileUploadedOn
            */
    }

    override fun getItemCount(): Int =  docsList.size


    private fun getDocType(docType: String): String {
        val doctype = docType.split(".").toTypedArray()
        val type = doctype[1]
        return type
    }

    private fun setDocImage(docType: String, imageView: ImageView) {
        if (docType.equals(AppConstant.file_format_png,ignoreCase = true)) {
            imageView.setImageResource(R.drawable.ic_png)
        } else if (docType.equals(AppConstant.file_format_pdf,ignoreCase = true)) {
            imageView.setImageResource(R.drawable.ic_pdf)
        } else if (docType.equals(AppConstant.file_format_jpg,ignoreCase = true) || (docType.equals(AppConstant.file_format_jpeg,ignoreCase = true))) {
            imageView.setImageResource(R.drawable.ic_jpg)
        }
    }


}