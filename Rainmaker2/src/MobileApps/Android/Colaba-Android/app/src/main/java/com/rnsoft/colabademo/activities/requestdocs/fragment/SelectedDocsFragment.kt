package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.LinearLayoutCompat
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.os.bundleOf
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.SelectedDocsLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.select_doc_main_cell.view.*
import java.util.HashMap

import javax.inject.Inject
@AndroidEntryPoint
class SelectedDocsFragment:DocsTypesBaseFragment() {

    private var _binding: SelectedDocsLayoutBinding? = null
    private val binding get() = _binding!!



    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = SelectedDocsLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        setUpLayout()
        super.addListeners(binding.root)
        return root
    }

    private fun setUpLayout(){
        val sampleList: HashMap<String, String> = HashMap()
        sampleList.put("Earnest Money Deposit", "")
        sampleList.put("Bank Statements", "Please provide 2 most recent month's bank statement with sufficient funds for cash to close.")
        sampleList.put("Profit and Loss Statement", "")
        sampleList.put("Form 1099 (Miscellaneous Income)", "Please provide the extra income evidence document.")
        sampleList.put("Earnest Money Deposit", "")
        sampleList.put("Financial Statements", "")
        var mainCell: ConstraintLayout = layoutInflater.inflate(R.layout.select_doc_main_cell, null) as ConstraintLayout
        for ((key, value) in sampleList) {
            mainCell = layoutInflater.inflate(R.layout.select_doc_main_cell, null) as ConstraintLayout
            mainCell.selectedDocTitle.text = key
            mainCell.setOnClickListener {
                val bundle = bundleOf(AppConstant.heading to key)
                findNavController().navigate(R.id.action_doc_detail_fragment , bundle)
            }
            if(value.isNotEmpty() && value.isNotBlank()) {
                mainCell.selectedDocDetail.visibility = View.VISIBLE
                mainCell.selectedDocDetail.text = value
            }
            else
                mainCell.selectedDocDetail.visibility = View.GONE

            binding.selectDocContainer.addView(mainCell)
        }

        binding.btnNext.setOnClickListener {
            findNavController().navigate(R.id.action_send_email_request)
        }

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }
    }

}