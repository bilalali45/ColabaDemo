package com.rnsoft.colabademo.activities.borroweraddresses.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import androidx.annotation.Nullable
import androidx.fragment.app.DialogFragment
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.activities.dashboard.fragements.home.BaseFragment
import com.rnsoft.colabademo.databinding.DialogFragmentCurrentResidenceBinding

class SaveCurrentResidenceDialogFragment : BottomSheetDialogFragment() {

    companion object {
        lateinit var baseFragment:BaseFragment
        /*fun newInstance(topFragment:BaseFragment): CustomFilterBottomSheetDialogFragment {
            baseFragment    =   topFragment
            return CustomFilterBottomSheetDialogFragment()
        } */

        fun newInstance(): SaveCurrentResidenceDialogFragment {
            return SaveCurrentResidenceDialogFragment()
        }
    }

    lateinit var binding: DialogFragmentCurrentResidenceBinding

    override fun onCreate(@Nullable savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setStyle(STYLE_NORMAL, R.style.roundedBottomSheetDialog)

    }

    private fun setInitialSelection(){

        binding.pendingIcon.setColorFilter(resources.getColor(R.color.grey_color_two, activity?.theme))
        binding.pendingTextView.setTextColor(resources.getColor(R.color.grey_color_two, activity?.theme))

        binding.recentIcon.setColorFilter(resources.getColor(R.color.grey_color_two, activity?.theme))
        binding.recentTextView.setTextColor(resources.getColor(R.color.grey_color_two, activity?.theme))

        when(BaseFragment.globalOrderBy){
            0->{

                binding.pendingIcon.setColorFilter(resources.getColor(R.color.colaba_apptheme_blue, activity?.theme))
                binding.pendingTextView.setTextColor(resources.getColor(R.color.colaba_apptheme_blue, activity?.theme))

            }
            1->{

                binding.recentIcon.setColorFilter(resources.getColor(R.color.colaba_apptheme_blue, activity?.theme))
                binding.recentTextView.setTextColor(resources.getColor(R.color.colaba_apptheme_blue, activity?.theme))


            }

            else-> {

            }
        }
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogFragmentCurrentResidenceBinding.inflate(inflater, container, false)
        binding.crossImageView.setOnClickListener{
            dismiss();
        }
        setStyle(DialogFragment.STYLE_NORMAL, R.style.roundedBottomSheetDialog)


        binding.saveContinuelayout.setOnClickListener {
            dismiss()
            //baseFragment.setOrderId(orderBy = 1)
        }

        binding.discardChangesLayout.setOnClickListener {
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