package com.rnsoft.colabademo

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter

/**
 * Created by Anita Kiran on 1/19/2022.
 */

private const val NUM_TABS = 2

class SearchPagerAdapter (fragmentManager: FragmentManager, lifecycle: Lifecycle) :
FragmentStateAdapter(fragmentManager, lifecycle) {

    override fun getItemCount(): Int {
        return NUM_TABS
    }

    override fun createFragment(position: Int): Fragment {

        when (position) {
            0 -> {
                val fragment = SearchedApplicationFragment()
                return fragment
            }
            1 -> {
                val fragment = SearchedContactsFragment()
                return fragment
            }
        }

        return BaseFragment()
    }
}