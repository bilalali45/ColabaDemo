package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.*
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.MailingAddressLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject

@AndroidEntryPoint
class MailingAddressFragment : Fragment() {

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

        return root

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