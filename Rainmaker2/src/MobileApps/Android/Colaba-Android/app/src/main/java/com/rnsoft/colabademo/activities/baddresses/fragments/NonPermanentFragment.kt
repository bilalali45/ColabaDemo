package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.NonPermenantResidentLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*

@AndroidEntryPoint
class NonPermanentFragment : Fragment() {

    private var _binding: NonPermenantResidentLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = NonPermenantResidentLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

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


        binding.relationshipEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.relationshipEditText, binding.relationshipDetailLayout, requireContext()))


        binding.visaStatusCompleteView.setOnClickListener{
            binding.visaStatusCompleteView.showDropDown()
        }

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        return root
    }



    override fun onAttach(context: Context) {
        super.onAttach(context)

    }

}