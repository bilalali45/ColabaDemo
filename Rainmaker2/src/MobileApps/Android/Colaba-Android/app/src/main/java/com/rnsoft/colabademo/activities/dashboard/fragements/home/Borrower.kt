package com.rnsoft.colabademo

import android.content.Context

data class Borrower(
    val id: Int,
    val banner: Int,
    val borrowerName: String?,
    val subtitle: String?,
    val about: String?
)  {



    companion object {
        fun customersList(context: Context): List<Borrower> {
            return listOf(
                Borrower(0, R.drawable.bird,
                    context.getString(R.string.title_rugby),
                    context.getString(R.string.subtitle_rugby),
                    context.getString(R.string.subtitle_rugby)
                ),
                Borrower(1, R.drawable.dog,
                    context.getString(R.string.title_cricket),
                    context.getString(R.string.subtitle_cricket),
                    context.getString(R.string.subtitle_cricket)
                ),
                Borrower(2, R.drawable.cat,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.subtitle_hockey),
                    context.getString(R.string.subtitle_hockey)
                ),
                Borrower(3, R.drawable.dollar_bag,
                    context.getString(R.string.title_basketball),
                    context.getString(R.string.subtitle_basketball),
                    context.getString(R.string.subtitle_basketball)
                )
            )
        }
    }
}



