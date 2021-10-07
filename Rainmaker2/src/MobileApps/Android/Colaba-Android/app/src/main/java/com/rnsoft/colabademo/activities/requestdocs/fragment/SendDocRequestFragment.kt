package com.rnsoft.colabademo

import android.content.res.ColorStateList
import android.os.Bundle
import android.text.Spanned
import android.text.style.ImageSpan
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import androidx.core.content.ContextCompat
import androidx.navigation.fragment.findNavController
import com.google.android.material.chip.Chip
import com.google.android.material.chip.ChipDrawable
import com.rnsoft.colabademo.activities.requestdocs.fragment.model.Template
import com.rnsoft.colabademo.databinding.SendDocRequestLayoutBinding

/**
 * Created by Anita Kiran on 10/4/2021.
 */
class SendDocRequestFragment : BaseFragment() {

    private lateinit var binding : SendDocRequestLayoutBinding
    private var savedViewInstance: View? = null
    var templateList: ArrayList<Template> = ArrayList()
    companion object {
        var selectedItem = -1
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = SendDocRequestLayoutBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root
            super.addListeners(savedViewInstance as ViewGroup)

            setupUI()
            setEmailTemplate()


            val chip = Chip(requireContext())
            chip.text = "test@gmail.com"
            chip.isCloseIconEnabled = true
            binding.chipGroup.addView(chip)



            //binding.chip.text = "anita@gmail.com"

            savedViewInstance
        }
    }

    private fun setupUI(){

        binding.btnSendRequest.setOnClickListener {
          findNavController().navigate(R.id.action_request_sent)
        }

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }
    }

    private fun setEmailTemplate(){
        templateList.add(Template("Document Request with Intro","Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque"))
        templateList.add(Template("Default Document Request ","Sed ut perspiciatis unde omnis iste natus error sit voluptatem"))
        templateList.add(Template("Only Tenant Intro","Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque"))

        val spinnerAdapter = EmailTemplateSpinnerAdapter(requireContext(), R.layout.email_template_item, templateList)
        binding.tvEmailType.setAdapter(spinnerAdapter)
        binding.tvEmailType.setOnClickListener {
            binding.tvEmailType.showDropDown()
        }
        binding.tvEmailType.onItemClickListener =
            AdapterView.OnItemClickListener { p0, p1, position, id ->
                selectedItem = position
                binding.layoutEmailTemplate.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two))
            }
    }

}