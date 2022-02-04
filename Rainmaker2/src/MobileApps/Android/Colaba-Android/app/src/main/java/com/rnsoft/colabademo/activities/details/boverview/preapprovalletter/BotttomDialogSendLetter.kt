package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import androidx.annotation.Nullable
import androidx.fragment.app.DialogFragment
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.SendApprovalLetterEvent
import com.rnsoft.colabademo.databinding.DialogSendLetterBinding
import org.greenrobot.eventbus.EventBus

/**
 * Created by Anita Kiran on 1/31/2022.
 */
class BotttomDialogSendLetter : BottomSheetDialogFragment() {

    companion object {
        fun newInstance(): BotttomDialogSendLetter {
            return BotttomDialogSendLetter()
        }
    }

    lateinit var binding: DialogSendLetterBinding

    override fun onCreate(@Nullable savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setStyle(STYLE_NORMAL, R.style.roundedBottomSheetDialog)

    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogSendLetterBinding.inflate(inflater, container, false)
        setStyle(DialogFragment.STYLE_NORMAL, R.style.roundedBottomSheetDialog)

        binding.crossImageView.setOnClickListener{
            dismiss()
        }
        binding.qualificationLetter.setOnClickListener {
            EventBus.getDefault().post(SendApprovalLetterEvent(false))
            dismiss()
        }

        binding.conditionLetter.setOnClickListener {
            EventBus.getDefault().post(SendApprovalLetterEvent(true))
            dismiss()
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