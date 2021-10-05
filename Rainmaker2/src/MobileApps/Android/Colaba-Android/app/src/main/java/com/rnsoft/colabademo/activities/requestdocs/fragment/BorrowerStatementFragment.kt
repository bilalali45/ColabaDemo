package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.AppConstant
import com.rnsoft.colabademo.BaseFragment
import com.rnsoft.colabademo.DeleteDocumentDialogFragment
import com.rnsoft.colabademo.databinding.BankStatementLayoutBinding

/**
 * Created by Anita Kiran on 10/4/2021.
 */
class BorrowerStatementFragment : BaseFragment() {

    private lateinit var binding : BankStatementLayoutBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
           binding = BankStatementLayoutBinding.inflate(inflater, container, false)

        val title = arguments?.getString(AppConstant.heading).toString()
        binding.toolbarTitle.setText(title)
        setupUI()


        return binding.root

        }

    private fun setupUI(){

        binding.btnTopDelete.setOnClickListener {
            DeleteDocumentDialogFragment.newInstance("Tax returns with schedules (Personals-Two Years").show(childFragmentManager, DeleteDocumentDialogFragment::class.java.canonicalName)
        }

        binding.btnClose.setOnClickListener {
            findNavController().popBackStack()
        }

    }
}