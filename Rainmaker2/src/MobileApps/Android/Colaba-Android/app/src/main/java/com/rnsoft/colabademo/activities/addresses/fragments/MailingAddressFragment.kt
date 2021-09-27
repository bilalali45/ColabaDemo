package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.*
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.MailingAddressLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject

@AndroidEntryPoint
class MailingAddressFragment : BaseFragment() {

    private var _binding: MailingAddressLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = MailingAddressLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        activity?.window?.setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN)

          binding.saveBtn.setOnClickListener {
           checkValidations()
        }


        binding.topSearchAutoTextView.setOnFocusChangeListener { p0: View?, hasFocus: Boolean ->
            if (hasFocus) {
                binding.topSearchTextInputLine.layoutParams.height = 3
                binding.topSearchTextInputLine.setBackgroundColor(resources.getColor(R.color.colaba_apptheme_blue, requireActivity().theme))
            } else {
                binding.topSearchTextInputLine.layoutParams.height = 1
                binding.topSearchTextInputLine.setBackgroundColor(resources.getColor(R.color.grey_color_four, requireActivity().theme))

                val search: String = binding.topSearchAutoTextView.text.toString()
                if (search.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutSearchField, R.color.grey_color_three, requireActivity())
                } else {
                    CustomMaterialFields.setColor(binding.layoutSearchField, R.color.grey_color_three, requireActivity())
                }
            }
        }

        binding.cityEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.cityEditText, binding.cityLayout, requireContext()))
        binding.streetAddressEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.streetAddressEditText, binding.streetAddressLayout, requireContext()))
        binding.unitAptInputEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.unitAptInputEditText, binding.unitAptInputLayout, requireContext()))
        binding.countyEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.countyEditText, binding.countyLayout, requireContext()))
        binding.zipcodeEditText.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.zipcodeEditText, binding.zipcodeLayout, requireContext()))


        val countryAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view,  AppSetting.countries)
        binding.countryCompleteTextView.setAdapter(countryAdapter)

        binding.countryCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.countryCompleteTextView.showDropDown()
        }
        binding.countryCompleteTextView.setOnClickListener{
            binding.countryCompleteTextView.showDropDown()
        }


        val stateAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view,  AppSetting.states)
        binding.stateCompleteTextView.setAdapter(stateAdapter)

        binding.stateCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.stateCompleteTextView.showDropDown()
            //if(binding.stateCompleteTextView.text.equals(" "))
                //binding.stateCompleteTextView.setText("")
        }
        binding.stateCompleteTextView.setOnClickListener{
            binding.stateCompleteTextView.showDropDown()

        }



        binding.backButton.setOnClickListener{
            val message = "Are you sure you want to delete Richard's Mailing Address?"
            AddressNotSavingDialogFragment.newInstance(message).show(childFragmentManager, AddressNotSavingDialogFragment::class.java.canonicalName)
            //findNavController().popBackStack()
        }

        binding.delImageview.setOnClickListener {
            val message = "Are you sure you want to delete Richard's Mailing Address?"
            AddressNotSavingDialogFragment.newInstance(message).show(childFragmentManager, AddressNotSavingDialogFragment::class.java.canonicalName)
        }


        binding.backButton.setOnClickListener{
            findNavController().popBackStack()
        }

        super.addListeners(binding.root)

        return root

    }


    private fun checkValidations() {
        val searchBar: String = binding.topSearchAutoTextView.text.toString()
        if (searchBar.isEmpty() || searchBar.length == 0) {
            setError()
        }
        if (searchBar.isNotEmpty() || searchBar.length > 0) {
            removeError()
            findNavController().popBackStack()
        }
    }

    private fun setError(){
        binding.tvError.visibility = View.VISIBLE
        binding.topSearchTextInputLine.layoutParams.height = 3
        binding.topSearchTextInputLine.setBackgroundColor(resources.getColor(R.color.colaba_red_color, requireActivity().theme))
    }

    private fun removeError() {
        binding.tvError.visibility = View.GONE
        binding.topSearchTextInputLine.layoutParams.height = 1
        binding.topSearchTextInputLine.setBackgroundColor(resources.getColor(R.color.grey_color_four, requireActivity().theme))
    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onNotSavingAddressEvent(event: NotSavingAddressEvent) {
        if(event.boolean){
            findNavController().popBackStack()
        }
    }

}