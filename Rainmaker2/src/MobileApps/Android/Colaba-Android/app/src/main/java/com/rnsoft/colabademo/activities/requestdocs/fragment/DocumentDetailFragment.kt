package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.DocDetailLayoutBinding

class DocumentDetailFragment : BaseFragment() {

    private lateinit var binding : DocDetailLayoutBinding

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = DocDetailLayoutBinding.inflate(inflater, container, false)
        val title = arguments?.getString(AppConstant.heading).toString()
        binding.toolbarTitle.text = title
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