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
import com.rnsoft.colabademo.databinding.DetailBorrowerLayoutTwoBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class BorrowerOverviewFragment : Fragment()  {

    private var _binding: DetailBorrowerLayoutTwoBinding? = null
    private val binding get() = _binding!!

    private val detailViewModel: DetailViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = DetailBorrowerLayoutTwoBinding.inflate(inflater, container, false)
        val root: View = binding.root

        detailViewModel.borrowerOverviewModel.observe(viewLifecycleOwner, {  overviewModel->
            if(overviewModel!=null) {
                binding.mainBorrowerName.text = ""
                val coBorrowers = overviewModel.coBorrowers

                var mainBorrowerName = ""
                if (coBorrowers != null) {
                    val coBorrowerNames:ArrayList<String> = ArrayList()
                    if(coBorrowers.size == 0)
                        binding.coBorrowerNames.visibility = View.GONE
                    else{
                        for (coBorrower in coBorrowers){
                            val coBorrowerName = coBorrower.firstName+" "+coBorrower.lastName
                            if(coBorrower.ownTypeId!=1)
                                coBorrowerNames.add(coBorrowerName)
                            else
                                mainBorrowerName = coBorrowerName
                        }
                        binding.coBorrowerNames.visibility = View.VISIBLE
                        binding.coBorrowerNames.text = coBorrowerNames.joinToString(separator = ",")
                    }
                }

                binding.mainBorrowerName.text = mainBorrowerName

                 if(overviewModel.loanNumber!=null && !overviewModel.loanNumber.equals("null", true)  && overviewModel.loanNumber.isNotEmpty()) {
                     binding.loanId.visibility = View.VISIBLE
                     binding.loanId.text = "Loan#" + overviewModel.loanNumber
                 }
                else
                     binding.loanId.visibility = View.GONE

                binding.loanPurpose.text = overviewModel.loanPurpose
                binding.loanPayment.text ="$"+overviewModel.loanAmount
                binding.downPayment.text ="$"+overviewModel.downPayment
                overviewModel.webBorrowerAddress?.let {
                    // 4101  Oak Tree Avenue  LN # 222,\nChicago, MD 60605
                    binding.completeAddress.text = it.street+" "+it.city+",\n"+it.stateName+", "+it.countryName+" "+it.zipCode

                }

                if(overviewModel.postedOn!=null && !overviewModel.postedOn.equals("null", true)) {
                    binding.bytesPosted.text = "Posted to Byte on " + overviewModel.postedOn
                    binding.bytesPosted.visibility = View.VISIBLE
                    binding.bytesTick.visibility = View.VISIBLE
                }
                else {
                    binding.bytesPosted.visibility = View.GONE
                    binding.bytesTick.visibility = View.GONE
                }


            }
            else
                Log.e("should-stop"," here....")

        })

        return root

    }

}