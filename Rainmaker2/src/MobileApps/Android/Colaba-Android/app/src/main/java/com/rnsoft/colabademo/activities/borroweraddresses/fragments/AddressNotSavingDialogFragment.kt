package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import androidx.annotation.Nullable
import androidx.fragment.app.DialogFragment
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import com.rnsoft.colabademo.activities.borroweraddresses.info.fragment.SwipeToDeleteEvent
import com.rnsoft.colabademo.databinding.DialogFragmentCurrentResidenceBinding
import org.greenrobot.eventbus.EventBus

class SaveCurrentResidenceDialogFragment : BottomSheetDialogFragment() {

    companion object {
        lateinit var userMessage:String
        fun newInstance(message:String): SaveCurrentResidenceDialogFragment {
            userMessage    =   message
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


    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogFragmentCurrentResidenceBinding.inflate(inflater, container, false)
        setStyle(DialogFragment.STYLE_NORMAL, R.style.roundedBottomSheetDialog)

        binding.crossImageView.setOnClickListener{
            dismiss()
        }
        binding.saveContinuelayout.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(NotSavingAddressEvent(true))
            //baseFragment.setOrderId(orderBy = 1)
        }

        binding.discardChangesLayout.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(NotSavingAddressEvent(true))
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