package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.inputmethod.InputMethodManager
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.TempResidenceLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject


@AndroidEntryPoint
class CurrentResidenceFragment : Fragment() {

    private var _binding: TempResidenceLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {

        _binding = TempResidenceLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root



        binding.moveInEditText.setOnClickListener { openCalendar() }
        binding.moveInEditText.setOnFocusChangeListener{ _ , p1 ->
            if(p1)
            openCalendar()
        }
        binding.moveInEditText.showSoftInputOnFocus = false

        binding.topSearchAutoTextView.onFocusChangeListener = object:View.OnFocusChangeListener{
            override fun onFocusChange(p0: View?, p1: Boolean) {
                if(p1) {
                    binding.topSearchTextInputLine.minimumHeight = 1
                    binding.topSearchTextInputLine.setBackgroundColor(resources.getColor( R.color.colaba_primary_color , requireActivity().theme))
                }
                else{
                    binding.topSearchTextInputLine.minimumHeight = 1
                    binding.topSearchTextInputLine.setBackgroundColor(resources.getColor(R.color.grey_color_four, requireActivity().theme))
                }
            }
        }


        binding.cityEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.cityEditText, binding.cityLayout, requireContext()))
        binding.streetAddressEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.streetAddressEditText, binding.streetAddressLayout, requireContext()))
        binding.unitAptInputEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.unitAptInputEditText, binding.unitAptInputLayout, requireContext()))
        binding.countyEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.countyEditText, binding.countyLayout, requireContext()))
        binding.zipcodeEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.zipcodeEditText, binding.zipcodeLayout, requireContext()))

        binding.monthlyRentEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.monthlyRentEditText, binding.monthlyRentLayout, requireContext()))
        //binding.housingEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.housingEditText, binding.housingLayout, requireContext()))
        //binding.moveInEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.moveInEditText, binding.moveInLayout, requireContext()))


        val countryAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view ,  AppSetting.countries)
        binding.countryCompleteTextView.setAdapter(countryAdapter)

        binding.countryCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.countryCompleteTextView.showDropDown()
        }
        binding.countryCompleteTextView.setOnClickListener{
            binding.countryCompleteTextView.showDropDown()
        }

        binding.countryCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.countryCompleteLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(),  R.color.grey_color_two))
            }
        }


        val stateAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view,  AppSetting.states)
        binding.stateCompleteTextView.setAdapter(stateAdapter)

        binding.stateCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.stateCompleteTextView.showDropDown()
        }
        binding.stateCompleteTextView.setOnClickListener{
            binding.stateCompleteTextView.showDropDown()
        }

        binding.stateCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.stateCompleteTextInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(),  R.color.grey_color_two))
            }
        }




        val houseLivingTypeArray:ArrayList<String> = arrayListOf("Own", "Rent", "No Primary Housing Expense")
        val houseTypeAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view , houseLivingTypeArray)
        binding.housingCompleteTextView.setAdapter(houseTypeAdapter)

        binding.housingCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.housingCompleteTextView.showDropDown()
        }
        binding.housingCompleteTextView.setOnClickListener{
            binding.housingCompleteTextView.showDropDown()
        }

        binding.housingCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                if(position == houseLivingTypeArray.size-2) {
                    binding.monthlyRentLayout.visibility = View.VISIBLE
                    binding.housingLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(),  R.color.grey_color_two))
                }
               else
                    binding.monthlyRentLayout.visibility = View.GONE
            }
        }




        binding.addAddressLayout.setOnClickListener{
            findNavController().navigate(R.id.navigation_mailing_address)
        }

        binding.backButton.setOnClickListener{
            findNavController().popBackStack()
        }



        return root

    }

    private fun openCalendar(){

        hideKeyBoard()

        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        val dpd = DatePickerDialog(
            requireActivity(), { view, year, monthOfYear, dayOfMonth ->
                var stringMonth = monthOfYear.toString()
                if(monthOfYear<10)
                    stringMonth = "0$monthOfYear"
                binding.moveInEditText.setText(stringMonth + " / " + year)

                binding.moveInLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(),  R.color.grey_color_two))
            },
            year,
            month,
            day
        )
        dpd.show()

    }

    private fun hideKeyBoard() {
            val inputMethodManager = ContextCompat.getSystemService(requireContext(), InputMethodManager::class.java)!!
            inputMethodManager.hideSoftInputFromWindow(binding.moveInEditText.windowToken, 0)

    }

}