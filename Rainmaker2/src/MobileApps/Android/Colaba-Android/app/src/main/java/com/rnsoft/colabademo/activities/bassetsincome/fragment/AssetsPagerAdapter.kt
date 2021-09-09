package com.rnsoft.colabademo

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter

private const val NUM_TABS = 3

class AssetsPagerAdapter(fragmentManager: FragmentManager, lifecycle: Lifecycle) :
        FragmentStateAdapter(fragmentManager, lifecycle) {

    override fun getItemCount(): Int {
        return NUM_TABS
    }

    override fun createFragment(position: Int): Fragment {

        when (position) {
            0 -> {
                return AssetsMainFragment()
            }
            1 -> {
                val fragment = BorrowerApplicationFragment()
                return fragment
            }

            2 -> {
                val fragment = BorrowerDocumentFragment()
                return fragment
            }
        }


        return Fragment()

    }
}