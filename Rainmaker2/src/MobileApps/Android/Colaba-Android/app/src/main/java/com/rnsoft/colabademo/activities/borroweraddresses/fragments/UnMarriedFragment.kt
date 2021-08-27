package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.UnmarriedLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject
import android.widget.AdapterView
import android.widget.AdapterView.OnItemClickListener
import android.widget.RadioGroup
import androidx.navigation.fragment.findNavController

@AndroidEntryPoint
class UnMarriedFragment : Fragment() {

    private var _binding: UnmarriedLayoutBinding? = null
    private val binding get() = _binding!!

    private val relationshipTypes = listOf("Civil Unions", "Domestic Partners", "Registered Reciprocal", "Other")

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = UnmarriedLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root


        binding.radioGroup.setOnCheckedChangeListener(RadioGroup.OnCheckedChangeListener { _, checkedId ->
            when (checkedId) {
                R.id.yesRadioBtn -> {
                    binding.relationshipContainer.visibility = View.VISIBLE
                }
                R.id.noRadioBtn -> {
                    binding.relationshipContainer.visibility = View.GONE
                }
                else -> {
                }
            }
        })



        val relationshipAdapter = ArrayAdapter(root.context, android.R.layout.simple_list_item_1,  relationshipTypes)

        binding.relationshipSpinner.setAdapter(relationshipAdapter)
        //binding.relationshipSpinner.onItemSelectedListener = relationItemSelected
        binding.relationshipSpinner.setOnFocusChangeListener { _, _ ->
            binding.relationshipSpinner.showDropDown()
        }
        binding.relationshipSpinner.setOnClickListener{
            binding.relationshipSpinner.showDropDown()
        }

        binding.relationshipSpinner.onItemClickListener = object: OnItemClickListener{
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                if(position == relationshipTypes.size-1){
                    binding.relationshipLayout.visibility = View.VISIBLE

                }
                else{
                    binding.relationshipLayout.visibility = View.GONE
                }
            }
        }





        val stateNamesAdapter = ArrayAdapter(root.context, android.R.layout.simple_list_item_1,  AppSetting.states)
        binding.stateCompleteTextView.setAdapter(stateNamesAdapter)
        binding.stateCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.stateCompleteTextView.showDropDown()
        }

        binding.backButton.setOnClickListener {
            //findNavController().popBackStack()
            requireActivity().onBackPressed()
        }


        return root

    }

    private  val relationItemSelected = object : AdapterView.OnItemSelectedListener {
        override fun onItemSelected(parentView: AdapterView<*>?, selectedItemView: View?, position: Int, id: Long) {
            if (position == relationshipTypes.size-1)
                    binding.relationshipLayout.visibility = View.VISIBLE
            else
                binding.relationshipLayout.visibility = View.GONE


        }
        override fun onNothingSelected(parentView: AdapterView<*>?) {}
    }

}