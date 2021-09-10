package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.text.method.HideReturnsTransformationMethod
import android.text.method.PasswordTransformationMethod
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.BankAccountLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.login_layout.*
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*
import javax.inject.Inject

@AndroidEntryPoint
class BankAccountFragment : Fragment() {

    private var _binding: BankAccountLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = BankAccountLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.visaStatusCompleteView.requestFocus()

        val stateNamesAdapter = ArrayAdapter(root.context, android.R.layout.simple_list_item_1,  AppSetting.bankAccounts)
        binding.visaStatusCompleteView.setAdapter(stateNamesAdapter)
        binding.visaStatusCompleteView.setOnFocusChangeListener { _, _ ->
            binding.visaStatusCompleteView.showDropDown()
        }

        binding.visaStatusCompleteView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                visaStatusViewLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two ))
                if(position ==AppSetting.bankAccounts.size-1) {

                }
                else{}

            }
        }


        binding.accountNumberLayout.setEndIconOnClickListener(View.OnClickListener {
            if (binding.accountNumberEdittext.getTransformationMethod()
                    .equals(PasswordTransformationMethod.getInstance())
            ) { //  hide password
                binding.accountNumberEdittext.setTransformationMethod(HideReturnsTransformationMethod.getInstance())
                binding.accountNumberLayout.setEndIconDrawable(R.drawable.ic_eye_hide)
            } else {
                binding.accountNumberEdittext.setTransformationMethod(PasswordTransformationMethod.getInstance())
                binding.accountNumberLayout.setEndIconDrawable(R.drawable.ic_eye_icon_svg)
            }
        })

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.phoneFab.setOnClickListener {
            findNavController().popBackStack()
        }

        return root
    }
}