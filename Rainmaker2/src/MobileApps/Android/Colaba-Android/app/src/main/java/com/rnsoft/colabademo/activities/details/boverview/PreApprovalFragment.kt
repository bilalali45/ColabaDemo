package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.OnBackPressedCallback
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.PreApprovalLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject

@AndroidEntryPoint
class PreApprovalFragment : BaseFragment(){

    private var _binding: PreApprovalLayoutBinding? = null
    private val binding get() = _binding!!

    private val viewModel: DetailViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    lateinit var rootTestView: View

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = PreApprovalLayoutBinding.inflate(inflater, container, false)
        rootTestView = binding.root
        val detailActivity = (activity as? DetailActivity)
        detailActivity?.hideFabIcons()
        binding.backButton.setOnClickListener{
            findNavController().popBackStack()
        }
        activity?.onBackPressedDispatcher?.addCallback(viewLifecycleOwner, navigateToPreviousScreen)

        setupUI()
        super.addListeners(binding.root)
        return rootTestView
    }

    private val navigateToPreviousScreen: OnBackPressedCallback = object : OnBackPressedCallback(true) {
        override fun handleOnBackPressed() {
            findNavController().popBackStack()
        }
    }

    private fun setupUI(){
        CustomMaterialFields.setDollarPrefix(binding.layoutLoanAmount,requireContext())
        CustomMaterialFields.setDollarPrefix(binding.downPaymentAmount,requireContext())
        binding.dateOfTransferEditText.onFocusChangeListener = CustomFocusListenerForEditText(
            binding.dateOfTransferEditText,
            binding.layoutTransferDate,
            requireContext()
        )
        binding.dateOfTransferEditText.setOnFocusChangeListener{ _ , _ ->  openCalendar() }
    }

    private fun openCalendar(){
        val c = Calendar.getInstance()
        val year = c.get(Calendar.YEAR)
        val month = c.get(Calendar.MONTH)
        val day = c.get(Calendar.DAY_OF_MONTH)
        // New Style Calendar Added....
        val datePickerDialog = DatePickerDialog(
            requireActivity(), R.style.MySpinnerDatePickerStyle, {
                    view, selectedYear, monthOfYear, dayOfMonth -> binding.dateOfTransferEditText.setText("" + (monthOfYear+1) + "/" + dayOfMonth + "/" + selectedYear) },
            year, month, day
        )
        datePickerDialog.show()
    }

}