package com.rnsoft.colabademo

import android.content.Context

data class NotificationModel(
    val id: Int,
    val notificationName: String?,
    val notificationTime: String?
)  {



    companion object {
        fun sampleNotificationList(context: Context): List<NotificationModel> {
            return listOf(
                NotificationModel(0,
                    context.getString(R.string.submitted_docs),
                    context.getString(R.string.fifteen_thirty_four)
                ),
                NotificationModel(1,
                    context.getString(R.string.title_cricket),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(2,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(3,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(4,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(5,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(6,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(7,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(8,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(9,
                    context.getString(R.string.title_hockey),
                    context.getString(R.string.submitted_docs)
                ),
                NotificationModel(10,
                    context.getString(R.string.title_basketball),
                    context.getString(R.string.submitted_docs)
                )
            )
        }
    }
}



