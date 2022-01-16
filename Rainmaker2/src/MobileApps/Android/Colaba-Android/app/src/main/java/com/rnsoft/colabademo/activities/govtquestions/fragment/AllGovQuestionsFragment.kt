package com.rnsoft.colabademo.activities.govtquestions.fragment

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.BorrowerOneQuestionsLayoutBinding
import com.rnsoft.colabademo.databinding.FragmentBinding


class AllGovQuestionsFragment : Fragment() {
   // var binding : AllGovQuestionsFragment

     var binding : FragmentBinding? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment

        binding =
            DataBindingUtil.inflate(
                inflater,
                R.layout.fragment_all_gov_questions,
                container,
                false
            )



        return binding!!.root
    }

    companion object {
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            AllGovQuestionsFragment().apply {

            }
    }
}