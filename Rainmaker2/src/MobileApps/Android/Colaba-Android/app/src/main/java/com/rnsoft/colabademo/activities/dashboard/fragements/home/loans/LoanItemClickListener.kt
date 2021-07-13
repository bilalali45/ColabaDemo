package com.rnsoft.colabademo

interface LoanItemClickListener {
    //fun onItemClick(testLayout: ConstraintLayout)
    fun getCardIndex(position: Int)
    fun navigateCardToDetailActivity(position: Int)
}

