package com.rnsoft.colabademo.activities.govtquestions.fragment

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.LinearLayout
import android.widget.TextView
import androidx.appcompat.widget.AppCompatRadioButton
import androidx.appcompat.widget.AppCompatTextView
import androidx.databinding.DataBindingUtil
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.databinding.FragmentBinding
import com.rnsoft.colabademo.adapter.QueationAdapter
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber


class AllGovQuestionsFragment : Fragment() {
    var binding : FragmentBinding? = null
    private var tabBorrowerId: Int? = null
    private var currentBorrowerId:Int = 0
    private var adapter: QueationAdapter? = null
    private var bankruptcyAnswerData: BankruptcyAnswerData = BankruptcyAnswerData()
    private var childSupportAnswerDataList: java.util.ArrayList<ChildAnswerData> = arrayListOf()
    private var ownerShipInnerScreenParams: java.util.ArrayList<String> = arrayListOf()

    private var qustionheaderarray: ArrayList<QuestionData>? = null
    private var subquestionarray: ArrayList<QuestionData>? = null
    private val borrowerAppViewModel: BorrowerApplicationViewModel by activityViewModels()
    private var governmentParams = GovernmentParams()
    private lateinit var lastQData: QuestionData
    var row: View? = null
    var position : Int? = 0

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
        arguments?.let {
            tabBorrowerId = it.getInt(AppConstant.tabBorrowerId)
        }
        setUpDynamicTabs()

        binding!!.saveBtn.setOnClickListener {
//            if (demoGraphicScreenDisplaying)
//                updateDemoGraphicApiCall()
//            else
//                updateGovernmentQuestionApiCall()
            EventBus.getDefault().postSticky(BorrowerApplicationUpdatedEvent(true))
            requireActivity().finish()
        }


        listnser()

        instan = this
        return binding!!.root
    }

    private fun listnser() {
        binding!!.q1radioButton.setOnClickListener {discuss(binding!!.qh1.text.toString(),"1")}
        binding!!.q2radioButton.setOnClickListener { discuss(binding!!.qh2.text.toString(),"2") }
        binding!!.q3radioButton.setOnClickListener { discuss(binding!!.qh3.text.toString(),"3") }
        binding!!.q4radioButton.setOnClickListener { discuss(binding!!.qh4.text.toString(),"4") }
        binding!!.q5radioButton.setOnClickListener { discuss(binding!!.qh5.text.toString(),"5") }
        binding!!.q6radioButton.setOnClickListener { discuss(binding!!.qh6.text.toString(),"6") }
        binding!!.q7radioButton.setOnClickListener { discuss(binding!!.qh7.text.toString(),"7") }
        binding!!.q8radioButton.setOnClickListener { discuss(binding!!.qh8.text.toString(),"8") }
        binding!!.q9radioButton.setOnClickListener { discuss("Bankruptcy ","9") }
    }


    @Subscribe(threadMode = ThreadMode.MAIN)
    fun updateUndisclosedBorrowerFunds(updateEvent: UndisclosedBorrowerFundUpdateEvent) {
        if(updateEvent.whichBorrowerId == currentBorrowerId) {

//            clickedContentCell.govt_detail_box.detail_title.text =
//                UndisclosedBorrowerFundFragment.UndisclosedBorrowerQuestionConstant
//            clickedContentCell.govt_detail_box.detail_title.setTypeface(null, Typeface.NORMAL)
//            clickedContentCell.govt_detail_box.detail_text.text =
//                "$".plus(Common.addNumberFormat(updateEvent.detailDescription.toDouble()))
//            clickedContentCell.govt_detail_box.detail_text.setTypeface(null, Typeface.BOLD)
//            clickedContentCell.govt_detail_box.visibility = View.VISIBLE
//
//
//            governmentParams.Questions.let { questions ->
//                for (question in questions) {
//                    question.parentQuestionId?.let { parentQuestionId ->
//                        if (parentQuestionId == undisclosedLayout.id) {
//                            question.answer = updateEvent.detailDescription
//                            question.answerDetail = updateEvent.detailTitle
//                        }
//                    }
//                }
//            }

     }
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
                        Log.i("TAG", "setUpDynamicTabs: " + governmentQuestionsModelClassList)

                        var zeroIndexAppCompat: AppCompatTextView? = null
                        if (governmentQuestionsModelClassList.size > 0) {
                            var selectedGovernmentQuestionModel: GovernmentQuestionsModelClass? =
                                null
                            for (item in governmentQuestionsModelClassList) {
                                if (item.passedBorrowerId == tabBorrowerId) {
                                    selectedGovernmentQuestionModel = item
                                    currentBorrowerId = tabBorrowerId!!
                                    governmentQuestionActivity?.let { governmentQuestionActivity ->
                                        governmentQuestionActivity.loanApplicationId?.let { nonNullLoanApplicationId ->
                                            item.questionData?.let { questionDataList ->
                                                item.passedBorrowerId?.let { passedBorrowerId ->
                                                    governmentParams =
                                                        GovernmentParams(
                                                            passedBorrowerId,
                                                            nonNullLoanApplicationId,
                                                            questionDataList
                                                        )
                                                    Timber.e(
                                                        "TingoPingo = ",
                                                        governmentParams.BorrowerId,
                                                        governmentParams.toString()
                                                    )
                                                    var questionmodel =  governmentQuestionsModelClassList.get(0).questionData
                                                    for (qData in questionmodel!!) {
                                                        if (qData.parentQuestionId == null && qData.id != null){
                                                             qustionheaderarray!!.add(qData)

                                                        }else{
                                                             subquestionarray!!.add(qData)
                                                        }

                                                        if(qData.answer != null){
                                                           // setdata("", qData.answer.toString(), 1,"1")
                                                        }

                                                      }



//                                                    createcell(binding!!.root,
//                                                        qustionheaderarray!!, subquestionarray!!
//                                                    )

//                                                    adapter = QueationAdapter(requireActivity(), qustionheaderarray, subquestionarray!!)
//                                                    rvquestions.setLayoutManager(
//                                                        LinearLayoutManager(
//                                                            activity
//                                                        )
//                                                    )
//                                                    rvquestions.setAdapter(adapter)

                                                    //udateGovernmentQuestionsList.add(test)
                                                }
                                            }
                                        }
                                    }

                                    break
                                }
                            }
                        }

                    }
                });
    }

    companion object {



        var instan : AllGovQuestionsFragment? = null
        fun newInstance() = AllGovQuestionsFragment()

    }
    private fun createcell(
        root: View,
        qustionheaderarray: ArrayList<QuestionData>,
        subquestionarray: ArrayList<QuestionData>) {

//        for (qData in qustionheaderarray!!) {
//
//                     val li = LayoutInflater.from(activity)
//                     row = li.inflate(R.layout.listquestion, null)
//                     row!!.findViewById<TextView>(R.id.questionheader).text = qData.question
//                     row!!.findViewById<AppCompatRadioButton>(R.id.radioButtonyes).setOnClickListener {
//                        navigateToInnerScreen(
//                            qData.headerText!!,
//                            qData.id!!,
//                            qData.firstName,
//                            qData.lastName,
//                            no
//                        )
//                    }
//                    root.findViewById<LinearLayout>(R.id.list_Category).addView(row)
//        }


    }




    private fun navigateToInnerScreen(
        stringForSpecificFragment: String,
        questionId: Int,
        firstName: String?,
        lastName: String?,
        no: String
    ){
        val bundle = Bundle()
        bundle.putInt(AppConstant.questionId, questionId)
        bundle.putInt(AppConstant.whichBorrowerId, currentBorrowerId)
        bundle.putParcelable(AppConstant.addUpdateQuestionsParams , governmentParams)
        bundle.putString(AppConstant.govtUserName , (firstName+" "+lastName))
        bundle.putString(AppConstant.questionno ,no)

        when(stringForSpecificFragment) {
            "Undisclosed Borrowered Funds" ->{
                findNavController().navigate(R.id.action_undisclosed_borrowerfund, bundle)
            }
            "Family or Business affiliation" ->{  findNavController().navigate(R.id.action_family_affiliation , bundle) }
            "Ownership Interest in Property" ->{
                bundle.putStringArrayList(AppConstant.ownerShipGlobalData, ownerShipInnerScreenParams)
                findNavController().navigate(R.id.action_ownership_interest , bundle)
            }
            "Own Property Type" ->{ }
            "Debt Co-Signer or Guarantor" ->{  findNavController().navigate(R.id.action_debt_co , bundle )}
            "Outstanding Judgements" ->{  findNavController().navigate(R.id.action_outstanding , bundle)}
            "Federal Debt Deliquency" ->{ findNavController().navigate(R.id.action_federal_debt , bundle)}
            "Party to Lawsuit" ->{ findNavController().navigate(R.id.action_party_to , bundle) }
            "Bankruptcy " ->{
                val bankruptcyAnswerDataCopy = bankruptcyAnswerData.copy() //ArrayList(bankruptcyAnswerData.map { it.copy() })
                bundle.putParcelable(AppConstant.bankruptcyAnswerData, bankruptcyAnswerDataCopy)
                findNavController().navigate(R.id.navigation_bankruptcy , bundle)
            }
            "Child Support, Alimony, etc." ->{
                bundle.putParcelableArrayList(AppConstant.childGlobalList, childSupportAnswerDataList)
                findNavController().navigate(R.id.action_child_support, bundle)
            }
            "Foreclosured Property" ->{ findNavController().navigate(R.id.action_fore_closure_property , bundle) }
            "Pre-Foreclosureor Short Sale" ->{ findNavController().navigate(R.id.action_pre_for_closure , bundle) }
            "Title Conveyance" ->{ findNavController().navigate(R.id.action_title_conveyance, bundle) }
            else->{
                Timber.e(" not matching with header title...")
            }
        }
    }

    fun nav(id: Int?, headerText: String?, firstName: String?, lastName: String?) {
       // navigateToInnerScreen(headerText!!.toString(), id!!, firstName, lastName, no)
    }

    fun setdata(detailTitle: String, title: String, whichBorrowerId: Int, questionnumber: String?) {
        when (questionnumber) {
            "1" -> {
                binding!!.qv1.visibility = View.VISIBLE
                binding!!.qva1.text = "$" +title

            }
            "2" -> {
                binding!!.qv2.visibility = View.VISIBLE
                binding!!.qva2.text = "$" +title
                binding!!.qvs2.text = detailTitle
            }
            "3" -> {
                binding!!.qv3.visibility = View.VISIBLE
                binding!!.qvs3.text = "Detail"
                binding!!.qva3.text = title

            }
            "4" -> {
                binding!!.qv4.visibility = View.VISIBLE
             //   binding!!.qvs4.text = title

                binding!!.qvs4.text = "Detail"
                binding!!.qva4.text = title

            }
            "5" -> {
                binding!!.qv5.visibility = View.VISIBLE
               // binding!!.qvs5.text = title

                binding!!.qvs5.text = "Detail"
                binding!!.qva5.text = title

            }
            "6" -> {
                binding!!.qv6.visibility = View.VISIBLE
               // binding!!.qvs6.text = title
                binding!!.qvs6.text = "Detail"
                binding!!.qva6.text = title

            }
            "7" -> {
                binding!!.qv7.visibility = View.VISIBLE
                binding!!.qvs7.text = title
            }
            "8" -> {
                binding!!.qv8.visibility = View.VISIBLE
               // binding!!.qvs8.text = title

                binding!!.qvs8.text = "Detail"
                binding!!.qva8.text = title
            }
            "9" -> {
                binding!!.qv9.visibility = View.VISIBLE
              //  binding!!.qvs9.text = title


                binding!!.qvs9.text = "Which Type?"
                binding!!.qva9.text = title
            }
            else -> { // Note the block
                print("x is neither 1 nor 2")
            }
        }
    }
    fun discuss(detailTitle: String, no: String) {
        for (qData in qustionheaderarray!!) {
            if(qData.headerText == detailTitle) {
                navigateToInnerScreen(
                    qData.headerText!!,
                    qData.id!!,
                    qData.firstName,
                    qData.lastName,
                    no
                )
                break
            }
        }
    }

    fun setarray(
        s: String,
        childAnswerList: java.util.ArrayList<ChildAnswerData>,
        whichBorrowerId: Int,
        s1: String
    ) {


    }
}