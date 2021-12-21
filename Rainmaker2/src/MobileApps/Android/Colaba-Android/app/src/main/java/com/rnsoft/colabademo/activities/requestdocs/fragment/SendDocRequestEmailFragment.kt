package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.Editable
import android.text.Html
import android.text.Spanned
import android.text.TextWatcher
import android.text.style.ImageSpan
import android.util.Log
import android.view.KeyEvent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.inputmethod.EditorInfo
import android.widget.AdapterView
import androidx.core.content.ContextCompat
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.google.android.flexbox.FlexboxLayout
import com.google.android.material.chip.Chip
import com.google.android.material.chip.ChipDrawable
import com.rnsoft.colabademo.databinding.SendDocRequestLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.async
import javax.inject.Inject
import android.widget.TextView
import android.widget.TextView.OnEditorActionListener
import com.google.android.material.chip.ChipGroup
import java.util.regex.Pattern
import android.widget.Toast








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

            binding.etRecipientEmail.setOnEditorActionListener(OnEditorActionListener { v, actionId, event ->
                if(event != null && event.getKeyCode() == KeyEvent.KEYCODE_ENTER || actionId == EditorInfo.IME_ACTION_DONE || event.keyCode == KeyEvent.KEYCODE_SPACE
                    || actionId == KeyEvent.KEYCODE_SPACE){
                    Log.e("Key","Pressed")

                    validateEmailAddress()

                    true
                } else {
                    false
                }
            })


            binding.etRecipientEmail.addTextChangedListener(object : TextWatcher {
                override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {
                    val text = binding.etRecipientEmail.text.toString()
                    if(text.length > 0 ) {
                        val lastChar: String = s.toString().substring(s.length - 1)
                        if (lastChar == " ") {
                            validateEmailAddress()
                            Toast.makeText(
                                context, "space bar pressed",
                                Toast.LENGTH_SHORT
                            ).show()
                        }
                    }



                }
                override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
                override fun afterTextChanged(s: Editable) {}
            })















            savedViewInstance
        }
    }

    private fun validateEmailAddress(){
        val email = binding.etRecipientEmail.text.toString().trim()
        if(isValidEmailAddress(email)){
            addNewChip(email,binding.recipientGroupFL)
            binding.recipientEmailError.visibility = View.GONE
        } else {
            binding.recipientEmailError.visibility = View.VISIBLE
        }

    }




    private fun addNewChip(email: String,chipGroup : FlexboxLayout){
        val chip = LayoutInflater.from(context).inflate(R.layout.chip, chipGroup, false) as Chip
        //val chip = Chip(context)
        chip.text = email
        chip.chipIcon = ContextCompat.getDrawable(requireContext(), R.mipmap.ic_launcher_round)
        chip.isCloseIconEnabled = true
        chip.isClickable = true
        chip.isCheckable = false
        chipGroup.addView(chip as View, chipGroup.childCount - 1)
        chip.setOnCloseIconClickListener { chipGroup.removeView(chip as View) }
        binding.etRecipientEmail.setText("")
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
                        Log.e("combineDocList","$combineDocList")

                        for(item in combineDocList.indices){
                           // builder.append("<b>").append(houseLivingTypeArray.get(item)).append("<br/>")
                              builder.append("<p><span style=\"color: rgb(78,78,78);background-color: rgb(255,255,255);font-size: 14px;font-family: Rubik, sans-serif;\">")
                                  .append("\u25CF").append(" ").append(combineDocList.get(item).docType).append("</span></p>")
                        }

                        val newText = it.replace("###RequestDocumentList###", builder.toString())
                        //Log.e("newText", newText)
                        binding.tvEmailBody.text = Html.fromHtml(newText)
                        binding.layoutEmailBody.visibility = View.VISIBLE
                    }
                }
            }
        })
    }

    private fun isValidEmailAddress(email: String?): Boolean {
        val ePattern =
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,3}))$"
        val p = Pattern.compile(ePattern)
        val m = p.matcher(email)
        return m.matches()
    }

}
