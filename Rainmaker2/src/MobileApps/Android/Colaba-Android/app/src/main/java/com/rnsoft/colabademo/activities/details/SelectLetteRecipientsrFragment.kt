package com.rnsoft.colabademo.activities.details

import android.app.Activity
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.databinding.DataBindingUtil
import androidx.recyclerview.widget.LinearLayoutManager
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.PreApprovalBind

class SelectLetteRecipientsrFragment : Activity() {
    var binding: PreApprovalBind? = null
    var adapter : PreapprovalAdapter? = null
    var dummydata: ArrayList<String>? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = DataBindingUtil.setContentView(this, R.layout.fragment_select_lette_recipientsr);

        setData()
    }

    private fun setData() {

        dummydata = ArrayList()
        dummydata!!.add("One")
        dummydata!!.add("two")
        dummydata!!.add("three")
        dummydata!!.add("Four")

        binding!!.recycler.setLayoutManager(
            LinearLayoutManager(
                this,
                LinearLayoutManager.VERTICAL,
                false
            )
        )
        adapter = PreapprovalAdapter(this!!, dummydata)
        binding!!.recycler.adapter = adapter


    }


}