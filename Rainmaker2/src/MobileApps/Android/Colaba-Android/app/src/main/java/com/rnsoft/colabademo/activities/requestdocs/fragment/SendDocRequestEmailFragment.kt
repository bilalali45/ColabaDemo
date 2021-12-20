package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.Html
import android.text.Spanned
import android.text.style.ImageSpan
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import androidx.core.content.ContextCompat
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.google.android.material.chip.Chip
import com.google.android.material.chip.ChipDrawable
import com.rnsoft.colabademo.databinding.SendDocRequestLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.async
import javax.inject.Inject

/**
 * Created by Anita Kiran on 10/4/2021.
 */

@AndroidEntryPoint
class SendDocRequestEmailFragment : DocsTypesBaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private val viewModel: RequestDocsViewModel by activityViewModels()
    private lateinit var binding : SendDocRequestLayoutBinding
    private var savedViewInstance: View? = null
    private var loanApplicationId: Int? = null
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
            //setEmailTemplate()
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    var call = async {
                        viewModel.refreshTemplateList()
                        viewModel.getEmailTemplates(authToken)
                    }
                    call.await()
                    setEmailTemplate()
                }
            }


//            val chip = Chip(requireContext())
//            chip.text = "test@gmail.com"
//            chip.isCloseIconEnabled = true
//            binding.chipGroup.addView(chip)

            savedViewInstance
        }
    }

    private fun setupUI(){
        binding.btnSendRequest.setOnClickListener {
          findNavController().navigate(R.id.action_request_sent)
        }

        binding.backButton.setOnClickListener {
            findNavController().navigate(R.id.action_selected_doc_fragment)
        }
    }

    private fun setEmailTemplate(){
        //viewModel.refreshTemplateList()
        viewModel.emailTemplates.observe(viewLifecycleOwner, { data ->
            if (data != null && data.size > 0){
                for(item in data){
                    templateList.add(
                        Template(templateId = item.id, templateName = item.templateName, docDesc = item.templateDescription))
                }
                val spinnerAdapter = EmailTemplateSpinnerAdapter(requireContext(), R.layout.email_template_item, templateList)
                binding.tvEmailType.setAdapter(spinnerAdapter)
                binding.tvEmailType.setOnClickListener {
                    binding.tvEmailType.showDropDown()
                }
                binding.tvEmailType.onItemClickListener =
                    AdapterView.OnItemClickListener { p0, p1, position, id ->
                        selectedItem = position
                        //Log.e("onClick-id",templateList.get(position).templateId + " " +  templateList.get(position).templateName)
                        binding.layoutEmailTemplate.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two))

                        lifecycleScope.launchWhenStarted {
                            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                                // showloader here
                                binding.tvEmailBody.setText("")
                                val call = async {
                                    viewModel.getEmailTemplateBody(authToken,47,templateList.get(position).templateId) }
                                call.await()
                                setEmailBody()
                            }
                        }
                    }
            }
        })

    }

    private fun setEmailBody(){
        viewModel.emailTemplateBody.observe(viewLifecycleOwner, { body ->

            if(body != null){
                body.emailBody?.let {
                    val builder = StringBuilder()
                    if(it.contains("###RequestDocumentList###")){
                        Log.e("body","hasPattern True")
                        Log.e("combineDocList","$combineDocList")
                        val houseLivingTypeArray: ArrayList<String> = arrayListOf("Own", "Rent", "No Primary Housing Expense")


                        for(item in houseLivingTypeArray.indices){
                            builder.append(houseLivingTypeArray.get(item)).append("\n")
                        }

                        var newText = it.replace("###RequestDocumentList###".toRegex(),builder.toString())
                        Log.e("newText", newText)
                        binding.tvEmailBody.setText(Html.fromHtml(newText))
                        binding.layoutEmailBody.visibility = View.VISIBLE
                    }




                }
            }
        })
    }

}
