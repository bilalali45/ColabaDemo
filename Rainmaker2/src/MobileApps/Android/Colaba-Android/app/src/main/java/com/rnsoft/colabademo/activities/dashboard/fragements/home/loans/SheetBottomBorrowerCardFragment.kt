package com.rnsoft.colabademo.activities.dashboard.fragements.home.loans

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import androidx.annotation.Nullable
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import com.rnsoft.colabademo.AppConstant
import com.rnsoft.colabademo.Borrower
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.DialogFragmentBorrowerCardSheetBinding

class BorrowerCardBottomSheetDialogFragment : BottomSheetDialogFragment() {

    companion object {
        fun newInstance() = BorrowerCardBottomSheetDialogFragment()
    }

    override fun onCreate(@Nullable savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setStyle(STYLE_NORMAL, R.style.roundedBottomSheetDialog)
    }
  
    lateinit var binding: DialogFragmentBorrowerCardSheetBinding

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogFragmentBorrowerCardSheetBinding.inflate(inflater, container, false)
        val borrowerParcelObject =  arguments?.getParcelable<Borrower>(AppConstant.borrowerParcelObject)
        borrowerParcelObject?.let {
            binding.borrowerName.text = it.borrowerName
        }

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