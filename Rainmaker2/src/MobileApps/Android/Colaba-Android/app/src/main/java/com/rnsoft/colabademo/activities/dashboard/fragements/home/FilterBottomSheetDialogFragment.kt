package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import androidx.annotation.Nullable
import androidx.fragment.app.DialogFragment
import androidx.fragment.app.activityViewModels
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import com.rnsoft.colabademo.databinding.DialogFragmentModalBottomSheetBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import java.text.SimpleDateFormat
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

@AndroidEntryPoint
class FilterBottomSheetDialogFragment : BottomSheetDialogFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences


    companion object {
        fun newInstance() = FilterBottomSheetDialogFragment()
    }
  
    lateinit var binding: DialogFragmentModalBottomSheetBinding

    override fun onCreate(@Nullable savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setStyle(STYLE_NORMAL, R.style.roundedBottomSheetDialog)
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DialogFragmentModalBottomSheetBinding.inflate(inflater, container, false)
        binding.crossImageView.setOnClickListener{
            dismiss()
        }
        setStyle(DialogFragment.STYLE_NORMAL, R.style.roundedBottomSheetDialog)



        binding.mostPendingLayout.setOnClickListener {

            loadLoanApplications(orderBy = 0)
        }

        binding.mostRecentLayout.setOnClickListener {

            loadLoanApplications(1)
        }

        binding.borrowerAToZLayout.setOnClickListener {

            loadLoanApplications(2)
        }
        binding.borrowerZToALayout.setOnClickListener {

            loadLoanApplications(3)
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

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private val loanViewModel: LoanViewModel by activityViewModels()
    //private var stringDateTime: String = ""
    private var pageNumber: Int = 1
    private var pageSize: Int = 20
    private var loanFilter: Int = 0
    //private var orderBy: Int = 0
    private var assignedToMe: Boolean = false

    //private lateinit var mostPendingLayout:ConstraintLayout
    //private lateinit var mostRecentLayout:ConstraintLayout
   // private lateinit var borrower_a_to_z_layout:ConstraintLayout
    //private lateinit var borrower_z_to_a_layout:ConstraintLayout

    private fun loadLoanApplications(orderBy:Int) {
        dismiss()
        EventBus.getDefault().post(AllLoansLoadedEvent(ArrayList()))
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.loanApiDateTime.isEmpty())
                AppSetting.loanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(
                    Date()
                )
            Log.e("Why-", AppSetting.loanApiDateTime)
            Log.e("pageNumber-", pageNumber.toString() +" and page size = "+pageSize)
            loanViewModel.getAllLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.loanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe,
                optionalClear = true
            )
        }
    }

    private fun loadNonActiveApplications(orderBy:Int){
        dismiss()
        EventBus.getDefault().post(NonActiveLoansEvent(ArrayList()))
        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.nonActiveloanApiDateTime.isEmpty())
                AppSetting.nonActiveloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())

            loanViewModel.getNonActiveLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.nonActiveloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
        }
    }

    private fun loadActiveApplications(orderBy:Int) {
        dismiss()
        EventBus.getDefault().post(ActiveLoansEvent(ArrayList()))

        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
            if(AppSetting.activeloanApiDateTime.isEmpty())
                AppSetting.activeloanApiDateTime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.getDefault()).format(Date())

            loanViewModel.getActiveLoans(
                token = AppConstant.fakeUserToken,
                dateTime = AppSetting.activeloanApiDateTime, pageNumber = pageNumber,
                pageSize = pageSize, loanFilter = loanFilter,
                orderBy = orderBy, assignedToMe = assignedToMe
            )
        }
    }

}
