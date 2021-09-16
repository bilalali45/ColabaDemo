package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.AppHeaderWithCrossDeleteBinding
import com.rnsoft.colabademo.databinding.RealEstateOwnedLayoutBinding

/**
 * Created by Anita Kiran on 9/16/2021.
 */
class RealEstateOwnedFragment : Fragment() , View.OnClickListener {

    private lateinit var binding: RealEstateOwnedLayoutBinding
    private lateinit var toolbar: AppHeaderWithCrossDeleteBinding
    private var savedViewInstance: View? = null


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = RealEstateOwnedLayoutBinding.inflate(inflater, container, false)
            toolbar = binding.headerRealestate
            savedViewInstance = binding.root

            // set Header title
            toolbar.toolbarTitle.setText(getString(R.string.real_estate_owned))

            //initViews()
            savedViewInstance

        }
    }

    override fun onClick(view: View?) {

    }
}