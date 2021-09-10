package com.rnsoft.colabademo

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter


class AssetsPagerAdapter(fragmentManager: FragmentManager, lifecycle: Lifecycle) :
        FragmentStateAdapter(fragmentManager, lifecycle) {

    companion object{
        const val BORROWER_ASSET_TABS = 4 // this number will decide, how many tabs and fragments will be displayed...
    }

    override fun getItemCount(): Int {
        return BORROWER_ASSET_TABS
    }

    override fun createFragment(position: Int): Fragment {

        when (position) {
            0 -> {
                return BorrowerOneAssets()
            }
            1 -> {
                return BorrowerTwoAssets()
            }

            2 -> {
                return BorrowerThreeAssets()
            }

            3 -> {
                return BorrowerFourAssets()
            }
        }


        return Fragment()

    }
}