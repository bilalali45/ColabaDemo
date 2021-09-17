package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.StartApplicationFragLayoutBinding
import com.rnsoft.colabademo.utils.HideSoftkeyboard

/**
 * Created by Anita Kiran on 9/17/2021.
 */
class StartNewApplicationFragment : Fragment(), View.OnClickListener {

    private lateinit var binding: StartApplicationFragLayoutBinding
    private var savedViewInstance: View? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = StartApplicationFragLayoutBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root

            binding.findContactBtn.setOnClickListener(this)
            binding.createNewContactBtn.setOnClickListener(this)
            binding.parentLayout.setOnClickListener(this)
            binding.backButton.setOnClickListener(this)


            savedViewInstance
        }
    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.find_contact_btn -> findOrCreateContactClick()
            R.id.create_new_contact_btn ->findOrCreateContactClick()
            R.id.backButton -> requireActivity().finish()
            R.id.parentLayout -> HideSoftkeyboard.hide(requireActivity(),binding.parentLayout)
        }
    }


    private fun findOrCreateContactClick(){
        if(binding.findContactBtn.isChecked) {
            binding.findContactBtn.isChecked = false
            binding.createNewContactBtn.visibility=View.VISIBLE
            binding.layoutFindContact.visibility = View.VISIBLE
            binding.layoutCreateContact.visibility = View.GONE
            binding.findContactBtn.visibility = View.GONE
        }
        else {
            if (binding.createNewContactBtn.isChecked) {
                binding.createNewContactBtn.isChecked = false
                binding.createNewContactBtn.visibility=View.GONE
                binding.findContactBtn.visibility = View.VISIBLE
                binding.layoutFindContact.visibility = View.GONE
                binding.layoutCreateContact.visibility = View.VISIBLE
            }
        }
    }






}