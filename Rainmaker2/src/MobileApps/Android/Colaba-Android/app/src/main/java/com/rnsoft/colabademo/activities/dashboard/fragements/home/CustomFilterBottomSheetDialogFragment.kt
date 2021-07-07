package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import androidx.annotation.Nullable
import androidx.fragment.app.DialogFragment
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import com.rnsoft.colabademo.activities.dashboard.fragements.home.BaseFragment
import com.rnsoft.colabademo.databinding.DialogFragmentModalBottomSheetBinding

class CustomFilterBottomSheetDialogFragment : BottomSheetDialogFragment() {

    companion object {
         lateinit var baseFragment:BaseFragment

        fun newInstance(topFragment:BaseFragment): CustomFilterBottomSheetDialogFragment {
            baseFragment =   topFragment
            return CustomFilterBottomSheetDialogFragment()
        }

        //fun newInstance() = CustomFilterBottomSheetDialogFragment()
    }
  
    lateinit var binding: DialogFragmentModalBottomSheetBinding

    override fun onCreate(@Nullable savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setStyle(STYLE_NORMAL, R.style.roundedBottomSheetDialog)
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogFragmentModalBottomSheetBinding.inflate(inflater, container, false)
        binding.crossImageView.setOnClickListener{
            dismiss();
        }
        setStyle(DialogFragment.STYLE_NORMAL, R.style.roundedBottomSheetDialog)


        binding.mostPendingLayout.setOnClickListener {
            dismiss()
            baseFragment.setOrderId(orderBy = 0)
            //loadLoanApplications(orderBy = 0)
        }

        binding.mostRecentLayout.setOnClickListener {
            dismiss()
            baseFragment.setOrderId(orderBy = 1)
            //loadLoanApplications(1)
        }

        binding.borrowerAToZLayout.setOnClickListener {
            dismiss()
            baseFragment.setOrderId(orderBy = 2)
            //loadLoanApplications(2)
        }
        binding.borrowerZToALayout.setOnClickListener {
            dismiss()
            baseFragment.setOrderId(orderBy = 3)
            //loadLoanApplications(3)
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