package com.rnsoft.colabademo

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class QuestionAdapter internal constructor(private var questionsModel: ArrayList<BorrowerQuestionsModel>) :
    RecyclerView.Adapter<QuestionAdapter.BaseViewHolder>(){

    abstract class BaseViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) { abstract fun bind(item: BorrowerQuestionsModel) }


    inner class GovtQuestionViewHolder(view: View) : BaseViewHolder(view) {
        private var questionTitle: TextView = view.findViewById(R.id.questionTitle)
        private var question: TextView = view.findViewById(R.id.question)

        private var answer1Icon: ImageView = view.findViewById(R.id.answer1_icon)
        private var answer2Icon: ImageView = view.findViewById(R.id.answer2_icon)
        private var answer3Icon: ImageView = view.findViewById(R.id.answer3_icon)

        private var answer1Name:TextView = view.findViewById(R.id.answer1_name)
        private var answer2Name:TextView = view.findViewById(R.id.answer2_name)
        private var answer3Name:TextView = view.findViewById(R.id.answer3_name)

        private var answer1Yes:TextView = view.findViewById(R.id.answer1_yes)
        private var answer2No:TextView = view.findViewById(R.id.answer2_no)
        private  var answer3Na:TextView = view.findViewById(R.id.answer3_na)

        override fun bind(item: BorrowerQuestionsModel) {
            questionTitle.text = item.questionDetail?.questionHeader
            question.text = item.questionDetail?.questionText

            item.questionResponses?.let { answers ->

                var answer1 = ""
                var answer2 = ""
                var answer3 = ""

                for(answer in answers){
                    if(answer.questionResponseText.equals("Yes", true))
                        answer1 = "- "+answer.borrowerFirstName
                    else
                    if(answer.questionResponseText.equals("No", true))
                        answer2 = "- "+answer.borrowerFirstName
                    else
                        answer3 = "- "+answer.borrowerFirstName
                }

                if(answer1.isNullOrEmpty() || answer1.isNullOrBlank()){
                    answer1Icon.visibility = View.GONE
                    answer1Name.visibility = View.GONE
                    answer1Yes.visibility = View.GONE
                }
                else
                    answer1Name.text = answer1

                if(answer2.isNullOrEmpty() || answer2.isNullOrBlank()){
                    answer2Icon.visibility = View.GONE
                    answer2Name.visibility = View.GONE
                    answer2No.visibility = View.GONE
                }
                else
                    answer2Name.text = answer2

                if(answer3.isNullOrEmpty() || answer3.isNullOrBlank()){
                    answer3Icon.visibility = View.GONE
                    answer3Name.visibility = View.GONE
                    answer3Na.visibility = View.GONE
                }
                else
                    answer3Name.text = answer3

            }
        }
    }

    inner class DemoGraphicViewHolder(itemView: View) : BaseViewHolder(itemView) {
        override fun bind(item: BorrowerQuestionsModel) {
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BaseViewHolder {
        val holder: BaseViewHolder?
        val inflater: LayoutInflater = LayoutInflater.from(parent.context)
        holder = if(viewType == R.layout.demographic_horizontal)
            DemoGraphicViewHolder(inflater.inflate(R.layout.demographic_horizontal, parent, false))
        else
            GovtQuestionViewHolder(inflater.inflate(R.layout.list_govt_question_horizontal, parent, false))
        return holder
    }



    override fun getItemViewType(position: Int): Int {
        return if(questionsModel[position].isDemoGraphic)
            R.layout.demographic_horizontal
        else
            R.layout.list_govt_question_horizontal
    }

    override fun onBindViewHolder(holder: BaseViewHolder, position: Int){ holder.bind(questionsModel[position]) }

    override fun getItemCount() = questionsModel.size







}