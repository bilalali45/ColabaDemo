package com.rnsoft.colabademo

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

import java.util.*

class LeftActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_lleft)
        val recyclerView = findViewById<RecyclerView>(R.id.recycler_view)
        recyclerView.adapter = LeftAdapter(items)
        recyclerView.layoutManager = LinearLayoutManager(this)
    }

    private val items: List<String>
        private get() {
            val items: MutableList<String> = ArrayList()
            for (i in 0..7) {
                items.add("item $i")
            }
            return items
        }
}