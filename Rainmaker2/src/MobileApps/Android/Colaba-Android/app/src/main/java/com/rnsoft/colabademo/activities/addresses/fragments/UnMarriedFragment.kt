package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
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
import androidx.core.content.ContextCompat
import androidx.navigation.fragment.findNavController
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*

@AndroidEntryPoint
class UnMarriedFragment : BaseFragment() {

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

        binding.backButton.setOnClickListener {
            //findNavController().popBackStack()
            requireActivity().onBackPressed()
        }

        setRelationShipField()
        setStateField()

        super.addListeners(binding.root)

        return root
    }

    private fun setRelationShipField(){
        val relationshipAdapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,  relationshipTypes)
        binding.relationshipTypeCompleteView.setAdapter(relationshipAdapter)
        binding.relationshipTypeCompleteView.setOnFocusChangeListener { _, _ ->
            binding.relationshipTypeCompleteView.showDropDown()
        }
        binding.relationshipTypeCompleteView.setOnClickListener{
            binding.relationshipTypeCompleteView.showDropDown()
        }
        binding.relationshipTypeCompleteView.onItemClickListener = object: OnItemClickListener{
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.relationTypeLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two ))
                if(position == relationshipTypes.size-1)
                    binding.relationshipDetailLayout.visibility = View.VISIBLE
                else
                    binding.relationshipDetailLayout.visibility = View.GONE
            }
        }
    }

    private fun setStateField(){
        val stateNamesAdapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1,  AppSetting.states)
        binding.stateCompleteTextView.setAdapter(stateNamesAdapter)
        binding.stateCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.stateCompleteTextView.showDropDown()
        }
        binding.stateCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.stateCompleteTextInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two ))
            }
        }
    }

}