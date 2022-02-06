package com.rnsoft.colabademo

import android.os.Bundle
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter
import com.rnsoft.colabademo.GovtQuestionsTabFragment.Companion.fragmentcount
import com.rnsoft.colabademo.GovtQuestionsTabFragment.Companion.tabBorrowerId
import com.rnsoft.colabademo.activities.govtquestions.fragment.AllGovQuestionsFragment
import com.rnsoft.colabademo.activities.govtquestions.fragment.AllGovQuestionsFragmentTwo


class GovtQuestionPagerAdapter(fragmentManager: FragmentManager, lifecycle: Lifecycle , private val tabIds:ArrayList<Int>) :
        FragmentStateAdapter(fragmentManager, lifecycle) {


    override fun getItemCount(): Int {
        return tabIds.size
    }

//    override fun createFragment(position: Int): Fragment {
//
//        val borrowerOneQuestions = AllGovQuestionsFragment()
//        val args = Bundle()
//        args.putInt(AppConstant.tabBorrowerId, tabIds[position])
//        borrowerOneQuestions.arguments = args
//        return borrowerOneQuestions
//    }

   // AllGovQuestionsFragment
   /// BorrowerOneQuestions


//     override fun getItemCount(): Int {
//        return GOVT_QUESTIONS_TABS
//     }

     override fun createFragment(position: Int): Fragment {

        when (position) {
            0 -> {
                tabBorrowerId = tabIds[position]
                fragmentcount = position
                val borrowerOneQuestions = AllGovQuestionsFragment()
                val args = Bundle()
                args.putInt(AppConstant.tabBorrowerId, tabIds[position])
                borrowerOneQuestions.arguments = args
                return borrowerOneQuestions
            }
            1 -> {
                tabBorrowerId = tabIds[position]
                fragmentcount = position


                val borrowerOneQuestions = AllGovQuestionsFragmentTwo()
                val args = Bundle()
                args.putInt(AppConstant.tabBorrowerId, tabIds[position])
                borrowerOneQuestions.arguments = args
                return borrowerOneQuestions
            }

            2 -> {
                return BaseFragment()
            }

            3 -> {
                return BaseFragment()
            }
        }


        return BaseFragment()

    }


}