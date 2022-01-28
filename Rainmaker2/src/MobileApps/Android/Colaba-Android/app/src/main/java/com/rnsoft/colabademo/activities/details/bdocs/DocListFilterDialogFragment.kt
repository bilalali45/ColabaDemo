package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import androidx.annotation.Nullable
import androidx.fragment.app.DialogFragment
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import com.rnsoft.colabademo.databinding.DialogDocFilterBinding
import org.greenrobot.eventbus.EventBus

/**
 * Created by Anita Kiran on 12/15/2021.
 */
class DocListFilterDialogFragment : BottomSheetDialogFragment() {

    companion object {
        var selection: String = AppConstant.filter_all

        fun newInstance(docFilter : String  ): DocListFilterDialogFragment {
            selection = docFilter
            return DocListFilterDialogFragment()
        }
    }

    lateinit var binding: DialogDocFilterBinding

    override fun onCreate(@Nullable savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setStyle(STYLE_NORMAL, R.style.roundedBottomSheetDialog)
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogDocFilterBinding.inflate(inflater, container, false)
        binding.crossImageView.setOnClickListener{
            dismiss()
        }
        setStyle(DialogFragment.STYLE_NORMAL, R.style.roundedBottomSheetDialog)

        binding.filterAll.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(onDocFilterEvent(AppConstant.filter_all))
        }

        binding.tvIndraft.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(onDocFilterEvent(AppConstant.filter_inDraft))
        }

        binding.tvManuallyAdded.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(onDocFilterEvent(AppConstant.filter_manuallyAdded))
        }

        binding.tvBorrowerTodo.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(onDocFilterEvent(AppConstant.filter_borrower_todo))
        }

        binding.tvStarted.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(onDocFilterEvent(AppConstant.filter_started))
        }

        binding.tvPending.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(onDocFilterEvent(AppConstant.filter_pending_review))
        }

        binding.tvCompleted.setOnClickListener {
            dismiss()
            EventBus.getDefault().post(onDocFilterEvent(AppConstant.filter_completed))
        }

        setDocSelection()

        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        initDialog()
    }

    private fun setDocSelection(){
        when(selection) {
            AppConstant.filter_all -> {
                binding.filterAll.setTextColor(resources.getColor(R.color.grey_color_one, activity?.theme))
                binding.filterAll.setTypeface(null, Typeface.BOLD)
            }
            AppConstant.filter_inDraft -> {
                binding.tvIndraft.setTextColor(resources.getColor(R.color.grey_color_one, activity?.theme))
                binding.tvIndraft.setTypeface(null, Typeface.BOLD)
            }
            AppConstant.filter_manuallyAdded -> {
                binding.tvManuallyAdded.setTextColor(resources.getColor(R.color.grey_color_one, activity?.theme))
                binding.tvManuallyAdded.setTypeface(null, Typeface.BOLD)
            }
            AppConstant.filter_borrower_todo -> {
                binding.tvBorrowerTodo.setTextColor(resources.getColor(R.color.grey_color_one, activity?.theme))
                binding.tvBorrowerTodo.setTypeface(null, Typeface.BOLD)
            }
            AppConstant.filter_started -> {
                binding.tvStarted.setTextColor(resources.getColor(R.color.grey_color_one, activity?.theme))
                binding.tvStarted.setTypeface(null, Typeface.BOLD)
            }
            AppConstant.filter_pending_review -> {
                binding.tvPending.setTextColor(resources.getColor(R.color.grey_color_one, activity?.theme))
                binding.tvPending.setTypeface(null, Typeface.BOLD)
            }
            AppConstant.filter_completed -> {
                binding.tvCompleted.setTextColor(resources.getColor(R.color.grey_color_one, activity?.theme))
                binding.tvCompleted.setTypeface(null, Typeface.BOLD)
            }
            else -> {

            }
        }
    }

    private fun initDialog() {
        requireDialog().window?.addFlags(WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS)
        requireDialog().window?.statusBarColor = requireContext().getColor(android.R.color.transparent)
    }
}