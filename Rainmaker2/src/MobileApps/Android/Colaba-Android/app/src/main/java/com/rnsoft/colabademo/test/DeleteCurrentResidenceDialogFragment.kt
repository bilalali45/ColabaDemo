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
import com.rnsoft.colabademo.databinding.DialogFragmentDeleteCurrentResidenceBinding

class DeleteCurrentResidenceDialogFragment : BottomSheetDialogFragment() {

    companion object {
         lateinit var baseFragment:BaseFragment

        fun newInstance(topFragment:BaseFragment): CustomFilterBottomSheetDialogFragment {
            baseFragment    =   topFragment
            return CustomFilterBottomSheetDialogFragment()
        }

        //fun newInstance() = CustomFilterBottomSheetDialogFragment()
    }
  
    lateinit var binding: DialogFragmentDeleteCurrentResidenceBinding


    override fun onCreate(@Nullable savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setStyle(STYLE_NORMAL, R.style.roundedBottomSheetDialog)

    }

    private fun setInitialSelection(){


    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogFragmentDeleteCurrentResidenceBinding.inflate(inflater, container, false)
        binding.crossImageView.setOnClickListener{
            dismiss();
        }
        setStyle(DialogFragment.STYLE_NORMAL, R.style.roundedBottomSheetDialog)


        binding.yesBtn.setOnClickListener {
            dismiss()


        }

        binding.noBtn.setOnClickListener {
            dismiss()


        }

        setInitialSelection()

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