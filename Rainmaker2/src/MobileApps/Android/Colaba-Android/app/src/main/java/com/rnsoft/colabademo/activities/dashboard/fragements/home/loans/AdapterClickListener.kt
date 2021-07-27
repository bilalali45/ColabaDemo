package com.rnsoft.colabademo

interface AdapterClickListener {
    //fun onItemClick(testLayout: ConstraintLayout)
    fun getCardIndex(position: Int)
    fun navigateTo(position: Int)
}

