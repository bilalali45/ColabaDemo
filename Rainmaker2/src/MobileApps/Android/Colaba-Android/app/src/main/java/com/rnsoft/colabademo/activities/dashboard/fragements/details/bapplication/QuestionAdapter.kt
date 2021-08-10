package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class QuestionAdapter internal constructor(private var questionsModel: ArrayList<BorrowerQuestionsModel>) :
    RecyclerView.Adapter<QuestionAdapter.BaseViewHolder>(){

    abstract class BaseViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) { abstract fun bind(item: BorrowerQuestionsModel) }


    inner class GovtQuestionViewHolder(itemView: View) : BaseViewHolder(itemView) {
        private var questionTitle: TextView = itemView.findViewById(R.id.questionTitle)
        private var question: TextView = itemView.findViewById(R.id.question)
        override fun bind(item: BorrowerQuestionsModel) {
            questionTitle.text = item.questionDetail?.questionHeader
            question.text = item.questionDetail?.questionText
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BaseViewHolder {
        val holder: BaseViewHolder?
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
        holder =  GovtQuestionViewHolder(inflater.inflate(R.layout.list_govt_question_horizontal, parent, false))
        return holder
    }

    override fun getItemViewType(position: Int): Int {
        return R.layout.list_govt_question_horizontal
    }

    override fun onBindViewHolder(holder: BaseViewHolder, position: Int){ holder.bind(questionsModel[position]) }

    override fun getItemCount() = questionsModel.size







}