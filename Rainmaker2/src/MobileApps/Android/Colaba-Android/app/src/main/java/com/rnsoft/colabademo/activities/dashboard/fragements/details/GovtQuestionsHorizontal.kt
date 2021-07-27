package com.rnsoft.colabademo

import android.content.Context
import android.graphics.Point
import android.view.View
import android.view.WindowManager
import android.widget.TextView

import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.items.AbstractItem

open class GovtQuestionsHorizontal : AbstractItem<GovtQuestionsHorizontal.ViewHolder>() {
    var questionTitle: String? = null
    var question: String? = null

    /** defines the type defining this item. must be unique. preferably an id */
    override val type: Int = 1

    /** defines the layout which will be used for this item in the list */
    override val layoutRes: Int
        get() = R.layout.list_govt_question_horizontal

    override fun getViewHolder(v: View): ViewHolder = ViewHolder(v)

    class ViewHolder(view: View) : FastAdapter.ViewHolder<GovtQuestionsHorizontal>(view) {
        var questionTitle: TextView = view.findViewById(R.id.questionTitle)
        var question: TextView = view.findViewById(R.id.question)

        override fun bindView(item: GovtQuestionsHorizontal, payloads: List<Any>) {
            questionTitle.text = item.questionTitle
            question.text = item.question
        }

        override fun unbindView(item: GovtQuestionsHorizontal) {
            question.text = null
            questionTitle.text = null
        }
    }
}