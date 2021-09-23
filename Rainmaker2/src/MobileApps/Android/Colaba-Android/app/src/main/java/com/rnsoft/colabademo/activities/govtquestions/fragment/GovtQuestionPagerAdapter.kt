package com.rnsoft.colabademo

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter


class GovtQuestionPagerAdapter(fragmentManager: FragmentManager, lifecycle: Lifecycle) :
        FragmentStateAdapter(fragmentManager, lifecycle) {

    companion object{
        const val GOVT_QUESTIONS_TABS = 2 // this number will decide, how many tabs and fragments will be displayed...
    }

    override fun getItemCount(): Int {
        return GOVT_QUESTIONS_TABS
    }

    override fun createFragment(position: Int): Fragment {

        when (position) {
            0 -> {
                return BorrowerOneQuestions()
            }
            1 -> {
                return BorrowerTwoQuestions()
            }

            2 -> {
                return Fragment()
            }

            3 -> {
                return Fragment()
            }
        }


        return Fragment()

    }
}