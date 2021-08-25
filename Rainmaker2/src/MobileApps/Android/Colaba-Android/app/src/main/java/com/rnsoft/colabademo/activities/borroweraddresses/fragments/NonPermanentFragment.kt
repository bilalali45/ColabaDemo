package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.Context
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.CurrentResidenceLayoutBinding
import com.rnsoft.colabademo.databinding.DetailBorrowerLayoutTwoBinding
import com.rnsoft.colabademo.databinding.MailingAddressLayoutBinding
import com.rnsoft.colabademo.databinding.NonPermenantResidentLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject
import javax.xml.datatype.DatatypeConstants.MONTHS
import kotlin.collections.ArrayList
import kotlin.math.roundToInt
import android.widget.ArrayAdapter





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


        val visaStatusArray:ArrayList<String> = arrayListOf("I am a temporary worker (H-2A, etc.)", "I hold a valid work visa (H1, L1, etc.)", "Other")
        val stateNamesAdapter = ArrayAdapter(root.context, android.R.layout.simple_list_item_1,  visaStatusArray)
        binding.visaStatusView.setAdapter(stateNamesAdapter)
        binding.visaStatusView.setOnFocusChangeListener { _, _ ->
            binding.visaStatusView.showDropDown()
        }

        binding.visaStatusView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                if(position == visaStatusArray.size-1){
                    binding.relationshipLabel.visibility = View.VISIBLE
                    binding.relationshipEditText.visibility = View.VISIBLE
                }
                else{
                    binding.relationshipLabel.visibility = View.GONE
                    binding.relationshipEditText.visibility = View.GONE
                }
            }

        }

        binding.visaStatusView.setOnClickListener{
            binding.visaStatusView.showDropDown()
        }

        return root
    }

    override fun onAttach(context: Context) {
        super.onAttach(context)

    }

}