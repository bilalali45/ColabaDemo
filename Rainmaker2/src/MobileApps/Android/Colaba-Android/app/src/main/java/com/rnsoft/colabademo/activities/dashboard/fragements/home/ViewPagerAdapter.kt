package com.rnsoft.colabademo

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter

private const val NUM_TABS = 3

class ViewPagerAdapter(fragmentManager: FragmentManager, lifecycle: Lifecycle) :
        FragmentStateAdapter(fragmentManager, lifecycle) {

    override fun getItemCount(): Int {
        return NUM_TABS
    }

    val fragmentHashMap: HashMap<Int, Fragment> = HashMap()

    override fun createFragment(position: Int): Fragment {

        when (position) {
            0 -> {
                val fragment = AllLoansFragment()
                fragmentHashMap[position] = fragment
                return fragment
            }
            1 -> {
                val fragment = ActiveLoansFragment()
                fragmentHashMap[position] = fragment
                return fragment
            }

            2 -> {
                val fragment = NonActiveLoansFragment()
                fragmentHashMap[position] = fragment
                return fragment
            }
        }


        return Fragment()

    }
}