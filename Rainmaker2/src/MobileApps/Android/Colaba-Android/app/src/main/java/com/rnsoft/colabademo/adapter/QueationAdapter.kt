package com.rnsoft.colabademo.adapter

import androidx.databinding.DataBindingUtil
import com.rnsoft.colabademo.R

internal class QueationAdapter(private val context: Context?, private val arrayList: ArrayList<Feedmodel>?) : RecyclerView.Adapter<Feedadapter.CustomView?>() {
    private var layoutInflater: LayoutInflater? = null

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CustomView {
        if (layoutInflater == null) {
            layoutInflater = LayoutInflater.from(parent.context)
        }
        val categoryBinding = DataBindingUtil.inflate<CategoryBinding?>(layoutInflater!!, R.layout.innerlayout, parent, false)
        categoryBinding!!.presenter = object : FeedPresenter {
            override fun onprofileNavigator() {


            }

            override fun onlikelistNavigator() {

            }

            override fun items() {

            }

            override fun chat() {

            }

            override fun address() {

            }

            override fun onComentNavigator() {

            }

            override fun onlikeNavigator() {

            }
        }
        return CustomView(categoryBinding)
    }

    fun updateData(viewModels: ArrayList<Feedmodel>?) {

        notifyDataSetChanged()
    }



    override fun onBindViewHolder(holder: CustomView, position: Int) {
        val categoryViewModel = arrayList!!.get(position)
        holder.bind(categoryViewModel)
        val post = categoryViewModel!!.getPostFile()


        //  binding.spinnerGender.performClick();
    }

    private fun setupSpinner() {

    }
    override fun getItemCount(): Int {
        return arrayList!!.size
        ///list.size + 1
    }

    internal inner class CustomView(private val categoryBinding: CategoryBinding?) : RecyclerView.ViewHolder(categoryBinding!!.getRoot()) {



        fun bind(categoryViewModel: Feedmodel?) {
            categoryBinding!!.setCategorymodel(categoryViewModel)
            categoryBinding.executePendingBindings()

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