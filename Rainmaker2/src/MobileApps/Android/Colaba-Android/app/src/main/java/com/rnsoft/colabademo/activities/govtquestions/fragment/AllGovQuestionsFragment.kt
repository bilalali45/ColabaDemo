package com.rnsoft.colabademo.activities.govtquestions.fragment

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatTextView
import androidx.databinding.DataBindingUtil
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.databinding.FragmentBinding
import androidx.recyclerview.widget.LinearLayoutManager
import com.rnsoft.colabademo.adapter.QueationAdapter
import kotlinx.android.synthetic.main.fragment_all_gov_questions.*
import timber.log.Timber


class AllGovQuestionsFragment : Fragment() {
    var binding : FragmentBinding? = null
    private var tabBorrowerId: Int? = null
    private var currentBorrowerId:Int = 0
    private var adapter: QueationAdapter? = null
    private var qustionheaderarray: ArrayList<QuestionData>? = null
    private var subquestionarray: ArrayList<QuestionData>? = null
    private val borrowerAppViewModel: BorrowerApplicationViewModel by activityViewModels()
    private var governmentParams = GovernmentParams()
    private lateinit var lastQData: QuestionData

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding =
            DataBindingUtil.inflate(
                inflater,
                R.layout.fragment_all_gov_questions,
                container,
                false
            )
        setUpDynamicTabs()
        return binding!!.root
    }

    private fun setUpDynamicTabs() {
        qustionheaderarray = ArrayList()
        subquestionarray = ArrayList()
        val governmentQuestionActivity = (activity as? GovtQuestionActivity)
        borrowerAppViewModel.governmentQuestionsModelClassList.observe(viewLifecycleOwner,
                { governmentQuestionsModelClassList ->
                    var zeroIndexAppCompat: AppCompatTextView? = null
                    var selectedGovernmentQuestionModel: GovernmentQuestionsModelClass? = null
                    if (governmentQuestionsModelClassList.size > 0) {
                        Log.i("TAG", "setUpDynamicTabs: "+governmentQuestionsModelClassList)

                        var questionmodel =  governmentQuestionsModelClassList.get(0).questionData
                        for (qData in questionmodel!!) {
                            if (qData.parentQuestionId == null && qData.id != null){
                                 qustionheaderarray!!.add(qData)
                            }else{
                                 subquestionarray!!.add(qData)
                            }
                          }
                        adapter = QueationAdapter(requireActivity(), qustionheaderarray, subquestionarray!!)
                        rvquestions.setLayoutManager(
                            LinearLayoutManager(
                                activity
                            )
                        )
                        rvquestions.setAdapter(adapter)
                    }
                });
    }

    companion object {
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            AllGovQuestionsFragment().apply {
            }
    }

    private fun navigateToInnerScreen(stringForSpecificFragment:String, questionId: Int){
        val bundle = Bundle()
        bundle.putInt(AppConstant.questionId, questionId)
        bundle.putInt(AppConstant.whichBorrowerId, currentBorrowerId)
        bundle.putParcelable(AppConstant.addUpdateQuestionsParams , governmentParams)
        bundle.putString(AppConstant.govtUserName , (lastQData.firstName+" "+lastQData.lastName))

        when(stringForSpecificFragment) {
            "Undisclosed Borrowered Funds" ->{
                findNavController().navigate(R.id.action_undisclosed_borrowerfund, bundle )
            }
            "Family or Business affiliation" ->{  findNavController().navigate(R.id.action_family_affiliation , bundle ) }
//            "Ownership Interest in Property" ->{
//                bundle.putStringArrayList(AppConstant.ownerShipGlobalData, ownerShipInnerScreenParams)
//                findNavController().navigate(R.id.action_ownership_interest , bundle)
//            }
            "Own Property Type" ->{ }
            "Debt Co-Signer or Guarantor" ->{  findNavController().navigate(R.id.action_debt_co , bundle )}
            "Outstanding Judgements" ->{  findNavController().navigate(R.id.action_outstanding , bundle)}
            "Federal Debt Deliquency" ->{ findNavController().navigate(R.id.action_federal_debt , bundle)}
            "Party to Lawsuit" ->{

                findNavController().navigate(R.id.action_party_to , bundle)
            }
//            "Bankruptcy " ->{
//                val bankruptcyAnswerDataCopy = bankruptcyAnswerData.copy() //ArrayList(bankruptcyAnswerData.map { it.copy() })
//                bundle.putParcelable(AppConstant.bankruptcyAnswerData, bankruptcyAnswerDataCopy)
//                findNavController().navigate(R.id.navigation_bankruptcy , bundle)
//            }

//            "Child Support, Alimony, etc." ->{
//                bundle.putParcelableArrayList(AppConstant.childGlobalList, childSupportAnswerDataList)
//                findNavController().navigate(R.id.action_child_support, bundle)
//            }
            "Foreclosured Property" ->{ findNavController().navigate(R.id.action_fore_closure_property , bundle) }
            "Pre-Foreclosureor Short Sale" ->{ findNavController().navigate(R.id.action_pre_for_closure , bundle) }
            "Title Conveyance" ->{ findNavController().navigate(R.id.action_title_conveyance, bundle) }
            else->{
                Timber.e(" not matching with header title...")
            }
        }
    }
}