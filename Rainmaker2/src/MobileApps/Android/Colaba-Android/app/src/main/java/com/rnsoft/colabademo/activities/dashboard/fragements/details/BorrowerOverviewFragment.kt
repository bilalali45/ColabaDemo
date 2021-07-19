package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import com.rnsoft.colabademo.databinding.DetailBorrowerLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerOverviewFragment : Fragment()  {

    private var _binding: DetailBorrowerLayoutBinding? = null
    private val binding get() = _binding!!

    private val detailViewModel: DetailViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = DetailBorrowerLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        detailViewModel.borrowerOverviewModel.observe(viewLifecycleOwner, {  borrowerOverviewModel->
            if(borrowerOverviewModel!=null) {
                binding.mainBorrowerName.text = ""
                val coBorrowers = borrowerOverviewModel.coBorrowers
                binding.coBorrowerNames.text = coBorrowers?.joinToString(separator = ",")
                binding.loanId.text ="Loan#"+borrowerOverviewModel.loanNumber
                binding.loanPurpose.text = borrowerOverviewModel.loanPurpose
                binding.loanPayment.text ="$"+borrowerOverviewModel.loanAmount
                binding.downPayment.text ="$"+borrowerOverviewModel.downPayment



            }
            else
                Log.e("should-stop"," here....")

        })

        return root

    }

}