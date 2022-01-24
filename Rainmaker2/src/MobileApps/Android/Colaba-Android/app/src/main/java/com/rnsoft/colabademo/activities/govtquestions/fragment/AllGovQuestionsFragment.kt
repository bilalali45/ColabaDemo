package com.rnsoft.colabademo.activities.govtquestions.fragment

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatTextView
import androidx.core.os.bundleOf
import androidx.databinding.DataBindingUtil
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.databinding.FragmentBinding
import com.rnsoft.colabademo.adapter.QueationAdapter
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import javax.inject.Inject


class AllGovQuestionsFragment : Fragment() {
    var binding : FragmentBinding? = null
    private var tabBorrowerId: Int? = null
    private var currentBorrowerId:Int = 0
    private var adapter: QueationAdapter? = null
    private var bankruptcyAnswerData: BankruptcyAnswerData = BankruptcyAnswerData()
    private var childSupportAnswerDataList: java.util.ArrayList<ChildAnswerData> = arrayListOf()
    private var ownerShipInnerScreenParams: java.util.ArrayList<String> = arrayListOf()
    private var asianChildList: java.util.ArrayList<DemoGraphicRaceDetail> = arrayListOf()
    private var qustionheaderarray: ArrayList<QuestionData>? = null
    private var subquestionarray: ArrayList<QuestionData>? = null
    private val borrowerAppViewModel: BorrowerApplicationViewModel by activityViewModels()
    private var nativeHawaiianChildList: java.util.ArrayList<DemoGraphicRaceDetail> = arrayListOf()
    var ownershipInterestAnswerData1: BorrowerOneQuestions.OwnershipInterestAnswerData?= null
    var ownershipInterestAnswerData2: BorrowerOneQuestions.OwnershipInterestAnswerData?= null
    private var governmentParams = GovernmentParams()
    private lateinit var lastQData: QuestionData
    var row: View? = null
    var position : Int? = 0
    private var bankruptcyMap = hashMapOf<String, String>()
    @Inject
    lateinit var sharedPreferences: SharedPreferences
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
            updateGovernmentQuestionApiCall()
            EventBus.getDefault().postSticky(BorrowerApplicationUpdatedEvent(true))
            requireActivity().finish()
        }


        listnser()

        instan = this
        return binding!!.root
    }


    private fun updateGovernmentQuestionApiCall() {

        if(governmentParams.Questions.size>0)
        {
            //testGovernmentParams.BorrowerId = governmentParams.BorrowerId
            //testGovernmentParams.LoanApplicationId = governmentParams.LoanApplicationId
            for (question in governmentParams.Questions) {
                if(question.id == 21){
                    Timber.e("what is  "+question.answerData)
                    if(ownershipInterestAnswerData1==null)
                        question.answerData = null
                    if(ownershipInterestAnswerData1?.selectionOptionId == null)
                        question.answerData = null

                    continue
                }

                if (question.parentQuestionId == 130) {
                    if(bankruptcyMap.size == 0)
                        question.answerData = null
                    else {
                        val test = hashMapOf<String, String>()
                        val newTestList = arrayListOf(HashMap<String, String>())
                        for(item in bankruptcyMap){
                            Timber.e("item kia ha ?"+item.value+"  and "+item.key)
                            test.put(item.key, item.value)
                            val json = Gson().toJson(test)
                            val mapCopy: HashMap<String, String> = Gson().fromJson(json, object : TypeToken<HashMap<String?, String>>() {}.type)
                            newTestList.add(mapCopy)
                            test.clear()
                        }
                        newTestList.removeAt(0)
                        question.answerData = newTestList
                    }
                    continue
                }

                if(question.id == 22){
                    Timber.e("what is  "+question.answerData)
                    if(ownershipInterestAnswerData2==null)
                        question.answerData = null
                    if(ownershipInterestAnswerData2?.selectionOptionId == null)
                        question.answerData = null
                    continue
                }
                question.answerData = null

                if (question.id == 140) {
                    //newChild.answerData = childSupportAnswerDataList
                    question.answerData = childSupportAnswerDataList
                }
                else
                    if(question.id == 45){   //family
                        //question.answerData = FamilyAnswerData()
                    }

                // ownership interest, it is handled when sent back....
                //if(question.parentQuestionId == 20){}

                // Bankruptcy


            }
            //governmentParams.Questions.add(childQuestionData)

            lifecycleScope.launchWhenStarted {

             //   borrowerAppViewModel.addOrUpdateGovernmentQuestions(LoginFragment.webtoken!!, governmentParams)

                borrowerAppViewModel.addgovernmetnjson(LoginFragment.webtoken!!, governmentParams)


//                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
//                    borrowerAppViewModel.addOrUpdateGovernmentQuestions(authToken, governmentParams)
//                }
            }
            findNavController().popBackStack()
        }


    }
    private fun listnser() {

        binding!!.q1radioButton.setOnClickListener {
            binding!!.q1radioButtonNo.isChecked = false
            discuss(binding!!.qh1.text.toString(),"1")
        }
        binding!!.q2radioButton.setOnClickListener {  discuss(binding!!.qh2.text.toString(),"2") }
        binding!!.q3radioButton.setOnClickListener {  discuss(binding!!.qh3.text.toString(),"3") }
        binding!!.q4radioButton.setOnClickListener {  discuss(binding!!.qh4.text.toString(),"4") }
        binding!!.q5radioButton.setOnClickListener {  discuss(binding!!.qh5.text.toString(),"5") }
        binding!!.q6radioButton.setOnClickListener {  discuss(binding!!.qh6.text.toString(),"6") }
        binding!!.q7radioButton.setOnClickListener {  discuss(binding!!.qh7.text.toString(),"7") }
        binding!!.q8radioButton.setOnClickListener {  discuss(binding!!.qh8.text.toString(),"8") }
        binding!!.q9radioButton.setOnClickListener {  discuss("Bankruptcy ","9") }
        binding!!.q10radioButton.setOnClickListener { discuss("Child Support, Alimony, etc.","10") }

        binding!!.layoutRaceAsian.setOnClickListener {
            val copyAsianChildList = java.util.ArrayList(asianChildList.map { it.copy() })
            val bundle = bundleOf(AppConstant.asianChildList to copyAsianChildList)
            findNavController().navigate(R.id.action_asian , bundle)
        }
        binding!!.nativehawaian.setOnClickListener {
            val copyNativeHawaiianChildList =
                java.util.ArrayList(nativeHawaiianChildList.map { it.copy() })
            val bundle = bundleOf(AppConstant.nativeHawaianChildList to copyNativeHawaiianChildList)
            findNavController().navigate(R.id.action_native_hawai, bundle)
        }
        binding!!.q1radioButtonNo.setOnClickListener {
            binding!!.q1radioButton.isChecked = false
            binding!!.qv1.visibility = View.GONE
        }




    }




    private fun updateGovernmentData(testData:QuestionData){
        for (item in governmentParams.Questions) {
            if(item.id == testData.id){
                item.answer = testData.answer
            }
        }
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
                                                            if(qData.answer != null){
                                                                if(qData.id == 70){
                                                                    setdata("",
                                                                        qData.answerDetail!!,
                                                                        0,
                                                                        "3",
                                                                        70)
                                                                }
                                                            }

                                                        }else{
                                                             subquestionarray!!.add(qData)
                                                            if(qData.answer != null){
                                                                if(qData.parentQuestionId == 10){
                                                                    setdata("",
                                                                        qData.answer!!,
                                                                        0,
                                                                        "1",
                                                                        10)
                                                                }
                                                            }
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

    fun setdata(
        detailTitle: String,
        title: String,
        whichBorrowerId: Int,
        questionnumber: String?,
        questionId: Int
    ) {
        when (questionnumber) {
            "1" -> {
                  binding!!.qv1.visibility = View.VISIBLE
                  binding!!.qva1.text = "$" +title
                  updatedata(questionId,title,detailTitle)
            }
            "2" -> {
                binding!!.qv2.visibility = View.VISIBLE
                binding!!.qv22.visibility = View.VISIBLE
                binding!!.qva2.text = detailTitle
                binding!!.qva22.text = title
                updatedata(questionId,title,detailTitle)

            }
            "3" -> {
                binding!!.qv3.visibility = View.VISIBLE
                binding!!.qvs3.text = "Detail"
                binding!!.qva3.text = title
                updatedata(questionId,title,detailTitle)

            }
            "4" -> {
                binding!!.qv4.visibility = View.VISIBLE
             //   binding!!.qvs4.text = title

                binding!!.qvs4.text = "Detail"
                binding!!.qva4.text = title
                updatedata(questionId,title,detailTitle)

            }
            "5" -> {
                binding!!.qv5.visibility = View.VISIBLE
               // binding!!.qvs5.text = title

                binding!!.qvs5.text = "Detail"
                binding!!.qva5.text = title
                updatedata(questionId,title,detailTitle)

            }
            "6" -> {
                binding!!.qv6.visibility = View.VISIBLE
               // binding!!.qvs6.text = title
                binding!!.qvs6.text = "Detail"
                binding!!.qva6.text = title
                updatedata(questionId,title,detailTitle)

            }
            "7" -> {
                binding!!.qv7.visibility = View.VISIBLE
                binding!!.qvs7.text = title
                updatedata(questionId,title,detailTitle)
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

    private fun updatedata(questionId: Int, title: String, detailTitle: String) {
        governmentParams.Questions.let { questions ->
            for (question in questions) {
                  if (questionId == 10) {
                        question.answer = title
                        question.answerDetail = detailTitle
                        break
                    }else if(questionId == 70){
                        question.answer = title
                         question.answerDetail = detailTitle
                        break
                    }else if(questionId == 80){
                      question.answer = title
                      question.answerDetail = detailTitle
                      break
                  }else if(questionId == 90){
                      question.answer = title
                      question.answerDetail = detailTitle
                      break
                  }



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
        for (i in 0 until childAnswerList.size) {
            binding!!.qv10.visibility = View.VISIBLE

          if(i == 0){
              binding!!.one.visibility = View.VISIBLE
              binding!!.qva101.visibility = View.VISIBLE


              binding!!.qvs101.text = childAnswerList.get(i).liabilityName
              binding!!.qva101.text = childAnswerList.get(i).monthlyPayment.toString()

          }else if(i == 1){
              binding!!.two.visibility = View.VISIBLE
              binding!!.qva102.visibility = View.VISIBLE

              binding!!.qvs102.text = childAnswerList.get(i).liabilityName
              binding!!.qva102.text = childAnswerList.get(i).monthlyPayment.toString()


          }else if(i == 2){
              binding!!.three.visibility = View.VISIBLE
              binding!!.qva103.visibility = View.VISIBLE
              binding!!.qvs103.text = childAnswerList.get(i).liabilityName
              binding!!.qva103.text = childAnswerList.get(i).monthlyPayment.toString()
          }

        }


    }
}