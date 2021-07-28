package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.ApplicationStatusLayoutBinding


import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerAppStatusFragment : Fragment(), AdapterClickListener {

    private var _binding: ApplicationStatusLayoutBinding? = null
    private val binding get() = _binding!!

    var list: ArrayList<String> = ArrayList()

    lateinit var circleBlue : ImageView
    lateinit var circleGrey : ImageView
    lateinit var circleRed: ImageView
    lateinit var lineBlue :View
    lateinit var lineGrey :View

    //private val detailViewModel: DetailViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    lateinit var rootTestView: View

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = ApplicationStatusLayoutBinding.inflate(inflater, container, false)
        rootTestView = binding.root
        createList()
        showApplicationStatus()
        binding.backButtonImageView.setOnClickListener{
           findNavController().popBackStack()
        }
        return rootTestView
    }

    private fun createList() {
        list.add("Application Started")
        list.add("Application Submitted")
        list.add("Processing")
        list.add("Underwriting")
        list.add("Approvals")
        list.add("Closing")
        list.add("Application Completed")
    }

    private fun showApplicationStatus() {
        val parentLayout = rootTestView.findViewById(R.id.layout_parent) as ViewGroup
        val layoutInflater = layoutInflater
        var view: View

        for (i in list.indices) {
            view = layoutInflater.inflate(R.layout.layout_loan_status_item, null)
            var textView = view.findViewById<TextView>(R.id.app_status_title)
            textView.text = list.get(i)

            if(list[i] == "Application Completed"){
                var lineBlue = view.findViewById<View>(R.id.app_status_line_blue)
                lineBlue.visibility = View.GONE
            }

            parentLayout.addView(view, i)
        }
    }





    /*

      private fun initViews(){
        circleBlue = rootTestView.findViewById(R.id.app_status_circle_blue)
        circleGrey = rootTestView.findViewById(R.id.app_status_circle_grey)
        circleRed = rootTestView.findViewById(R.id.app_status_circle_red)
        lineBlue = rootTestView.findViewById(R.id.app_status_line_blue)
        lineGrey = rootTestView.findViewById(R.id.app_status_line_grey)

    }

    private fun showStatusBar(blue:Boolean,grey:Boolean,red:Boolean){

        if(blue){
            binding.appStatusCircleBlue.visibility = View.VISIBLE
            binding.appStatusLineBlue.visibility = View.VISIBLE

            binding.appStatusCircleGrey.visibility = View.GONE
            binding.appStatusLineGrey.visibility = View.GONE
            binding.appStatusCircleRed.visibility = View.GONE
        }
        if(grey){
            binding.appStatusCircleGrey.visibility = View.VISIBLE
            binding.appStatusLineGrey.visibility = View.VISIBLE
            binding.appStatusCircleBlue.visibility = View.GONE
            binding.appStatusLineBlue.visibility = View.GONE
            binding.appStatusCircleRed.visibility = View.GONE
        }
    }

    private fun showOnlyCircle(blue:Boolean,grey:Boolean,red:Boolean){
        if(blue){
            binding.appStatusCircleBlue.visibility = View.VISIBLE
            binding.appStatusLineBlue.visibility = View.GONE
            binding.appStatusCircleGrey.visibility = View.GONE
            binding.appStatusLineGrey.visibility = View.GONE
            binding.appStatusCircleRed.visibility = View.GONE
        }
        if(grey){
            binding.appStatusCircleGrey.visibility = View.VISIBLE
            binding.appStatusLineGrey.visibility = View.GONE
            binding.appStatusCircleBlue.visibility = View.GONE
            binding.appStatusLineBlue.visibility = View.GONE
            binding.appStatusCircleRed.visibility = View.GONE
        }
        if(red){
            binding.appStatusCircleRed.visibility = View.VISIBLE
            binding.appStatusCircleGrey.visibility = View.GONE
            binding.appStatusLineGrey.visibility = View.GONE
            binding.appStatusCircleBlue.visibility = View.GONE
            binding.appStatusLineBlue.visibility = View.GONE
        }
    }
     */

    override fun getCardIndex(position: Int) {

    }

    override fun navigateTo(position: Int) {

    }

}