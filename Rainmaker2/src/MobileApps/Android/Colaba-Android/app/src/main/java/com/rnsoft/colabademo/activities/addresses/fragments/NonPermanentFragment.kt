package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.NonPermenantResidentLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject
import kotlin.collections.ArrayList
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.core.view.isVisible
import com.rnsoft.colabademo.utils.CustomMaterialFields
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*

@AndroidEntryPoint
class NonPermanentFragment : BaseFragment() {

    private var _binding: NonPermenantResidentLayoutBinding? = null
    private val binding get() = _binding!!
    private var citizenship : BorrowerCitizenship? = null
    var firstName : String? = null
    var lastName : String? = null


    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = NonPermenantResidentLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val activity = (activity as? BorrowerAddressActivity)
        activity?.firstName?.let {
            firstName = it
        }
        activity?.lastName?.let {
            lastName = it
        }

        if(firstName !=null && lastName !=null){
            binding.borrowerName.setText(firstName.plus(" ").plus(lastName))
        }


        binding.visaStatusCompleteView.requestFocus()
        val visaStatusArray:ArrayList<String> = arrayListOf("I am a temporary worker (H-2A, etc.)", "I hold a valid work visa (H1, L1, etc.)", "Other")
        val stateNamesAdapter = ArrayAdapter(root.context, android.R.layout.simple_list_item_1,  visaStatusArray)
        binding.visaStatusCompleteView.setAdapter(stateNamesAdapter)
        binding.visaStatusCompleteView.setOnFocusChangeListener { _, _ ->
            binding.visaStatusCompleteView.showDropDown()
        }

        binding.visaStatusCompleteView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                visaStatusViewLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two ))
                if(position == visaStatusArray.size-1) {
                    binding.relationshipDetailLayout.visibility = View.VISIBLE
                }
               else
                    binding.relationshipDetailLayout.visibility = View.GONE
            }
        }


        binding.relationshipEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.relationshipEditText, binding.relationshipDetailLayout, requireContext()))


        binding.visaStatusCompleteView.setOnClickListener{
            binding.visaStatusCompleteView.showDropDown()
        }

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.btnSave.setOnClickListener {
            saveData()
        }

        setData()

        super.addListeners(binding.root)

        return root
    }

    private fun setData(){
        try {
            if (arguments != null) {
                citizenship = arguments?.getParcelable(AppConstant.borrower_citizenship)!!
                citizenship?.let {
                    //Log.e("OtherFrag","$it")
                    it.residencyStatusId?.let {
                        if(it==4) {
                            binding.visaStatusCompleteView.setText(
                                "I am a temporary worker (H-2A, etc.)",
                                false
                            )
                        }
                        if(it==3)
                            binding.visaStatusCompleteView.setText("I hold a valid work visa (H1, L1, etc.)",false)

                        if(it==5) {
                            binding.visaStatusCompleteView.setText("Other", false)
                            binding.relationshipDetailLayout.visibility = View.VISIBLE
                        }
                        CustomMaterialFields.setColor(binding.visaStatusViewLayout, R.color.grey_color_two, requireActivity())
                    }

                    it.residencyStatusExplanation?.let {
                        if(it.isNotEmpty() && it.isNotBlank() && it.length >0 ){
                            binding.relationshipEditText.setText(it)
                            binding.relationshipDetailLayout.visibility = View.VISIBLE
                            CustomMaterialFields.setColor(binding.relationshipDetailLayout, R.color.grey_color_two, requireActivity())
                        }
                    }
                }

            }
        } catch (e: Exception) {
        }
    }

     private fun saveData(){
         var isDataEntered : Boolean = false
         val visaStatus = binding.visaStatusCompleteView.text.toString()
         var desc = binding.relationshipEditText.text.toString()

         if (visaStatus.isEmpty() || visaStatus.length == 0) {
             isDataEntered = false
             CustomMaterialFields.setError(binding.visaStatusViewLayout,getString(R.string.error_field_required), requireActivity())
         }

         if (visaStatus.length > 0) {
             isDataEntered = true
             CustomMaterialFields.clearError(binding.visaStatusViewLayout, requireActivity())
         }

         if(binding.relationshipDetailLayout.isVisible){
             if (desc.isEmpty() || desc.length == 0) {
                 isDataEntered = false
                 CustomMaterialFields.setError(binding.relationshipDetailLayout,getString(R.string.error_field_required), requireActivity())
             }

             if (desc.length > 0) {
                 isDataEntered = true
                 CustomMaterialFields.clearError(binding.relationshipDetailLayout, requireActivity())
             }
         }

         if(isDataEntered){
             var statusId: Int? = null

             if(binding.visaStatusCompleteView.text.toString().equals("I am a temporary worker (H-2A, etc.)")){
                 statusId = 4
             }

             if(binding.visaStatusCompleteView.text.toString().equals("I hold a valid work visa (H1, L1, etc.)")){
                 statusId = 3
             }

             if(binding.visaStatusCompleteView.text.toString().equals("Other")){
                 statusId = 5
             }

            desc = if(binding.relationshipEditText.text.toString().length > 0 ) desc else ""

             citizenship = BorrowerCitizenship(residencyStatusId = statusId,residencyStatusExplanation =  desc)

             findNavController().previousBackStackEntry?.savedStateHandle?.set(AppConstant.borrower_citizenship, citizenship)
             findNavController().popBackStack()
         }
     }


 }