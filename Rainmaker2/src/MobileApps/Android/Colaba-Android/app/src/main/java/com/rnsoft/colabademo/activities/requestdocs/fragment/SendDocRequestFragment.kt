package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.rnsoft.colabademo.activities.requestdocs.model.Template
import com.rnsoft.colabademo.databinding.SendDocRequestLayoutBinding

/**
 * Created by Anita Kiran on 10/4/2021.
 */
class SendDocRequestFragment : BaseFragment() {

    private lateinit var binding : SendDocRequestLayoutBinding
    private var savedViewInstance: View? = null
    var templateList: ArrayList<Template> = ArrayList()
    //lateinit var adapter : EmailTemplateSpinnerAdapter



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

            setEmailTemplate()


            savedViewInstance

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
//        binding.tvEmailType.onItemClickListener = object :
//            AdapterView.OnItemClickListener {
//            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
//                binding.layoutEmailTemplate.defaultHintTextColor = ColorStateList.valueOf(
//                    ContextCompat.getColor(requireContext(), com.rnsoft.colabademo.R.color.grey_color_two))
//            }
//        }

    }
}