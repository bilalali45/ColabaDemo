package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import com.rnsoft.colabademo.databinding.DialogFragmentModalBottomSheetBinding

class FilterBottomSheetDialogFragment : BottomSheetDialogFragment() {

    companion object {
        fun newInstance() = FilterBottomSheetDialogFragment()
    }
  
    lateinit var binding: DialogFragmentModalBottomSheetBinding

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogFragmentModalBottomSheetBinding.inflate(inflater, container, false)
        binding.crossImageView.setOnClickListener{
            dismiss();
        }
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        initDialog()
    }

    private fun initDialog() {
        requireDialog().window?.addFlags(WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS)
        requireDialog().window?.statusBarColor = requireContext().getColor(android.R.color.transparent)
    }
}