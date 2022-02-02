package com.rnsoft.colabademo.activities.details

import android.app.Activity
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.CheckBox
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.R

class PreapprovalAdapter(var mainact: Activity, var featuresArrayList: ArrayList<String>?) : RecyclerView.Adapter<PreapprovalAdapter.ViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        return ViewHolder(
            LayoutInflater.from(mainact).inflate(R.layout.items, parent, false)
        )
    }

    override fun getItemCount(): Int {
        return featuresArrayList!!.size
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {



    }

    class ViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        var chk = view.findViewById<CheckBox>(R.id.checkbox)

    }


}