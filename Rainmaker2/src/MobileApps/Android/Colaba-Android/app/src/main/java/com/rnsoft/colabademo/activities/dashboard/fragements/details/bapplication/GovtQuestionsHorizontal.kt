package com.rnsoft.colabademo

import android.view.View
import android.widget.ImageView
import android.widget.TextView

import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.items.AbstractItem

open class GovtQuestionsHorizontal : AbstractItem<GovtQuestionsHorizontal.ViewHolder>() {
    var questionTitle: String? = null
    var question: String? = null

    //var answer1Icon:ImageView?  = null
    //var answer2Icon:ImageView?  = null
    //var answer3Icon:ImageView?  = null

    var answer1:String?  = null
    var answer2:String?  = null
    var answer3:String?  = null



    /** defines the type defining this item. must be unique. preferably an id */
    override val type: Int = 1

    /** defines the layout which will be used for this item in the list */
    override val layoutRes: Int
        get() = R.layout.list_govt_question_horizontal

    override fun getViewHolder(v: View): ViewHolder = ViewHolder(v)

    class ViewHolder(view: View) : FastAdapter.ViewHolder<GovtQuestionsHorizontal>(view) {
        var questionTitle: TextView = view.findViewById(R.id.questionTitle)
        var question: TextView = view.findViewById(R.id.question)

        var answer1Icon:ImageView = view.findViewById(R.id.answer1_icon)
        var answer2Icon:ImageView = view.findViewById(R.id.answer2_icon)
        var answer3Icon:ImageView = view.findViewById(R.id.answer3_icon)

        var answer1Name:TextView = view.findViewById(R.id.answer1_name)
        var answer2Name:TextView = view.findViewById(R.id.answer2_name)
        var answer3Name:TextView = view.findViewById(R.id.answer3_name)

        var answer1Yes:TextView = view.findViewById(R.id.answer1_yes)
        var answer2No:TextView = view.findViewById(R.id.answer2_no)
        var answer3Na:TextView = view.findViewById(R.id.answer3_na)

        override fun bindView(item: GovtQuestionsHorizontal, payloads: List<Any>) {
            questionTitle.text = item.questionTitle
            question.text = item.question

            if(item.answer1.isNullOrEmpty() || item.answer1.isNullOrBlank()){
                answer1Icon.visibility = View.GONE
                answer1Name.visibility = View.GONE
                answer1Yes.visibility = View.GONE
            }
            else
                answer1Name.text = item.answer1

            if(item.answer2.isNullOrEmpty() || item.answer2.isNullOrBlank()){
                answer2Icon.visibility = View.GONE
                answer2Name.visibility = View.GONE
                answer2No.visibility = View.GONE
            }
            else
                answer2Name.text = item.answer2

            if(item.answer3.isNullOrEmpty() || item.answer3.isNullOrBlank()){
                answer3Icon.visibility = View.GONE
                answer3Name.visibility = View.GONE
                answer3Na.visibility = View.GONE
            }
            else
                answer3Name.text = item.answer3

        }

        override fun unbindView(item: GovtQuestionsHorizontal) {
            question.text = null
            questionTitle.text = null
        }
    }
}