package com.rnsoft.colabademo.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import androidx.recyclerview.widget.RecyclerView
import com.rnsoft.colabademo.QuestionData
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.activities.govtquestions.fragment.AllGovQuestionsFragment
import com.rnsoft.colabademo.databinding.CategoryBinding

internal class QueationAdapter(
    private val context: Context?,
    private val arrayList: ArrayList<QuestionData>?,
    var subquestionarray: ArrayList<QuestionData>
) : RecyclerView.Adapter<QueationAdapter.CustomView?>() {
    private var layoutInflater: LayoutInflater? = null

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CustomView {
        if (layoutInflater == null) {
            layoutInflater = LayoutInflater.from(parent.context)
        }
        val categoryBinding = DataBindingUtil.inflate<CategoryBinding?>(layoutInflater!!, R.layout.innerlayout, parent, false)
        categoryBinding!!.presenter = object : QuestionPresenter {
            override fun onNav() {
//                categoryBinding!!.subquesview.visibility = View.VISIBLE
//                categoryBinding!!.radioButtonyes.isChecked = true
//                categoryBinding!!.radioButtonNo.isChecked = false


                if(AllGovQuestionsFragment.instan != null){
                    AllGovQuestionsFragment.instan!!.nav(categoryBinding.categorymodel!!.id,categoryBinding.categorymodel!!.headerText,categoryBinding.categorymodel!!.firstName,categoryBinding.categorymodel!!.lastName)
                }
            }
            override fun onitem() {
//                categoryBinding!!.subquesview.visibility = View.GONE
//                categoryBinding!!.radioButtonyes.isChecked = false
//                categoryBinding!!.radioButtonNo.isChecked = true
            }

          }


        return CustomView(categoryBinding)
    }

    fun updateData(viewModels: ArrayList<QuestionData>?) {

        notifyDataSetChanged()
    }



    override fun onBindViewHolder(holder: CustomView, position: Int) {
        val categoryViewModel = arrayList!!.get(position)
        holder.bind(categoryViewModel)
       // val post = categoryViewModel!!.getPostFile()


        //  binding.spinnerGender.performClick();
    }

    private fun setupSpinner() {

    }
    override fun getItemCount(): Int {
        return arrayList!!.size
        ///list.size + 1
    }

    internal inner class CustomView(private val categoryBinding: CategoryBinding?) : RecyclerView.ViewHolder(categoryBinding!!.getRoot()) {



        fun bind(categoryViewModel: QuestionData?) {
            categoryBinding!!.setCategorymodel(categoryViewModel)
            categoryBinding.executePendingBindings()

//            for (qData in subquestionarray!!) {
//                if(categoryViewModel!!.id == qData.parentQuestionId){
//                    categoryBinding.subquestionheader.text = qData.question
//                }
//            }

        }

        fun getCategoryBinding(): CategoryBinding? {
            return categoryBinding
        }

        init {



        }
    }



    init {

    }
}