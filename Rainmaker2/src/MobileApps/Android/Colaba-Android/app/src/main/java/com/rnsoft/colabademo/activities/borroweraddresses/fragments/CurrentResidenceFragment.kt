package com.rnsoft.colabademo

import android.R
import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.CurrentResidenceLayoutBinding
import com.rnsoft.colabademo.databinding.DetailBorrowerLayoutTwoBinding
import com.rnsoft.colabademo.databinding.MailingTestLayoutBinding
import com.rnsoft.colabademo.databinding.TempResidenceLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject
import javax.xml.datatype.DatatypeConstants.MONTHS
import kotlin.math.roundToInt


@AndroidEntryPoint
class CurrentResidenceFragment : Fragment() {

    private var _binding: TempResidenceLayoutBinding? = null
    private val binding get() = _binding!!



    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = TempResidenceLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root


        binding.cityEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.cityEditText, binding.cityLayout, requireContext()))
        binding.streetAddressEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.streetAddressEditText, binding.streetAddressLayout, requireContext()))
        binding.unitAptInputEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.unitAptInputEditText, binding.unitAptInputLayout, requireContext()))
        binding.countyEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.countyEditText, binding.countyLayout, requireContext()))
        binding.zipcodeEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.zipcodeEditText, binding.zipcodeLayout, requireContext()))

        binding.monthlyRentEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.monthlyRentEditText, binding.monthlyRentLayout, requireContext()))
        binding.housingEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.housingEditText, binding.housingLayout, requireContext()))
        binding.moveInEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.moveInEditText, binding.moveInLayout, requireContext()))

        val stateAdapter = ArrayAdapter(requireContext(), R.layout.simple_dropdown_item_1line,  AppSetting.states)
        binding.countryCompleteTextView.setAdapter(stateAdapter)

        binding.countryCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.countryCompleteTextView.showDropDown()
        }
        binding.countryCompleteTextView.setOnClickListener{
            binding.countryCompleteTextView.showDropDown()
        }


        val countryAdapter = ArrayAdapter(requireContext(), R.layout.simple_dropdown_item_1line ,  AppSetting.countries)
        binding.stateCompleteTextView.setAdapter(countryAdapter)

        binding.stateCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.stateCompleteTextView.showDropDown()
        }
        binding.stateCompleteTextView.setOnClickListener{
            binding.stateCompleteTextView.showDropDown()
        }

        return root

    }

}