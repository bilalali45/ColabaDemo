package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.text.Html
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.activities.details.model.SendInvitationEmailModel
import com.rnsoft.colabademo.databinding.InvitePrimaryBorrowerLayoutBinding


import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class InvitePrimaryBorrowerFragment : BaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private lateinit var binding: InvitePrimaryBorrowerLayoutBinding
    private val detailViewModel: DetailViewModel by activityViewModels()
    private var borrowerId : Int? = null
    private var loanApplicationId : Int? = null
    private var invitation_type: String? = null
    private var emailBody: String? = null
    private var emailSubject: String? = null

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = InvitePrimaryBorrowerLayoutBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)

        (activity as DetailActivity).hideFabIcons()

        binding.backButtonImageView.setOnClickListener{
           findNavController().popBackStack()
        }

        binding.btnSendInvitation.setOnClickListener {
            detailViewModel.sendInvitationEmail(SendInvitationEmailModel(loanApplicationId!!, borrowerId!!,emailSubject!!,emailBody!!))
        }

        binding.btnResendInvitation.setOnClickListener {
            detailViewModel.resendInvitationEmail(SendInvitationEmailModel(loanApplicationId!!, borrowerId!!,emailSubject!!,emailBody!!))
        }

        getData()

        return binding.root
    }

    private fun getData(){
        if(arguments != null) {
            borrowerId = arguments?.getInt(AppConstant.borrowerId)
            loanApplicationId = arguments?.getInt(AppConstant.loanApplicationId)
            invitation_type = arguments?.getString(AppConstant.INVITATION_TYPE)

            if(invitation_type == AppConstant.INVITATION_STATUS_INVITE){
                binding.btnSendInvitation.visibility = View.VISIBLE
            } else{
                binding.btnResendInvitation.visibility = View.VISIBLE
            }

            if (borrowerId != null && loanApplicationId != null) {
                binding.loaderEmailData.visibility = View.VISIBLE
                detailViewModel.getInvitationEmail(loanApplicationId!!, borrowerId!!)
            }
        }

        detailViewModel.invitationEmail.observe(viewLifecycleOwner, { emailData ->
            emailData?.emailBody?.let {
                binding.loaderEmailData.visibility = View.GONE
                if(it !="null" && it.isNotEmpty()) {
                    binding.detailTextView.text = Html.fromHtml(it)
                    emailBody = it
                }
            }

            emailData?.emailSubject?.let {
                emailSubject = it
            }
        })
    }
}