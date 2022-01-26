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
import com.google.gson.internal.LinkedTreeMap
import com.google.gson.reflect.TypeToken
import com.rnsoft.colabademo.*
import com.rnsoft.colabademo.AppConstant.Questions
import com.rnsoft.colabademo.AppConstant.answerData
import com.rnsoft.colabademo.AppConstant.ethnicityarry
import com.rnsoft.colabademo.AppConstant.jARRAY
import com.rnsoft.colabademo.AppConstant.jsonaddquestion
import com.rnsoft.colabademo.AppConstant.racearr
import com.rnsoft.colabademo.activities.DemoGetGovermentmodel
import com.rnsoft.colabademo.activities.DemoGraphicModel
import com.rnsoft.colabademo.activities.GetGovermentmodel
import com.rnsoft.colabademo.activities.RaceModel
import com.rnsoft.colabademo.activities.govtquestions.model.answerData
import com.rnsoft.colabademo.databinding.FragmentBinding
import com.rnsoft.colabademo.adapter.QueationAdapter
import kotlinx.android.synthetic.main.fragment_all_gov_questions.*
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import org.json.JSONArray
import org.json.JSONObject
import timber.log.Timber
import javax.inject.Inject


class AllGovQuestionsFragment : Fragment() {
    var binding: FragmentBinding? = null
    private var tabBorrowerId: Int? = null
    private var currentBorrowerId: Int = 0
    private var adapter: QueationAdapter? = null
    private var bankruptcyAnswerData: BankruptcyAnswerData = BankruptcyAnswerData()
    private var childSupportAnswerDataList: java.util.ArrayList<ChildAnswerData> = arrayListOf()
    private var ownerShipInnerScreenParams: java.util.ArrayList<String> = arrayListOf()
    private var asianChildList: java.util.ArrayList<DemoGraphicRaceDetail> = arrayListOf()
    private var qustionheaderarray: ArrayList<QuestionData>? = null
    private var subquestionarray: ArrayList<QuestionData>? = null
    private val borrowerAppViewModel: BorrowerApplicationViewModel by activityViewModels()
    private var nativeHawaiianChildList: java.util.ArrayList<DemoGraphicRaceDetail> = arrayListOf()
    var ownershipInterestAnswerData1: BorrowerOneQuestions.OwnershipInterestAnswerData? = null
    var ownershipInterestAnswerData2: BorrowerOneQuestions.OwnershipInterestAnswerData? = null
    private var governmentParams = GovernmentParams()
    private lateinit var lastQData: QuestionData
    var row: View? = null
    var position: Int? = 0
    var demomodel: DemoGraphicModel = DemoGraphicModel()
    var questionmodel: DemoGetGovermentmodel = DemoGetGovermentmodel()

    private var ethnicityChildList: java.util.ArrayList<EthnicityDetailDemoGraphic> = arrayListOf()

    private var bankruptcyMap = hashMapOf<String, String>()

    // var questionmodel = null
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
        demomodel = DemoGraphicModel()
        racearr = ArrayList()
        setUpDynamicTabs()

        binding!!.saveBtn.setOnClickListener {
//            if (demoGraphicScreenDisplaying)
            //updateDemoGraphicApiCall()
//            else
            apidemograpic()

            updateGovernmentQuestionApiCall()
            EventBus.getDefault().postSticky(BorrowerApplicationUpdatedEvent(true))
            requireActivity().finish()
        }


        listnser()


        instan = this
        return binding!!.root
    }

    private  fun apidemograpic() {
//        jsonaddquestion.put("BorrowerId", governmentParams.BorrowerId)
//        jsonaddquestion.put("LoanApplicationId", governmentParams.LoanApplicationId)
        if(demomodel != null){
            demomodel.loanApplicationId = governmentParams.LoanApplicationId
            demomodel.borrowerId = governmentParams.BorrowerId
            demomodel.race = racearr
            demomodel.ethnicity = ethnicityarry


            val gson = Gson()
            val strJson = gson.toJson(demomodel)
            Log.i("TAG", "onFailure: "+strJson)
            borrowerAppViewModel.adddemographic("Bearer " + LoginFragment.webtoken!!, demomodel)
        }


    }

    fun demographic() {
        for (qData in data!!.race!!) {
            if (qData.raceId == 2) {
                asiancheckbox.isChecked = true
            } else if (qData.raceId == 3) {
                checkboxblack.isChecked = true
            }else if (qData.raceId == 4) {
                americannativecheckbox.isChecked = true
            } else if (qData.raceId == 5) {
                whitecheckbox.isChecked = true
            }else if (qData.raceId == 6) {
                idonotwishcheckbox.isChecked = true
            }else if (qData.raceId == 1) {
                checkbox.isChecked = true
            }
        }

        if(data!!.genderId == 1){
            male.isChecked = true

        }else if(data!!.genderId == 2){
            female.isChecked = true

        }else if(data!!.genderId == 3) {
            sexidontsharerb.isChecked = true
        }

        for (qData in data!!.ethnicity!!) {
            if (qData.ethnicityId == 1) {
                HispanicorLatino.isChecked = true
            } else if (qData.ethnicityId == 2) {
                notsHispanicorLatino.isChecked = true
            }else if (qData.ethnicityId == 3) {
                ethnicidonotwishrbbox.isChecked = true
            }
        }

   }

    private fun updateDemoGraphicApiCall() {
        borrowerAppViewModel.GetDemographicInformation("Bearer " + LoginFragment.webtoken!!, governmentParams.LoanApplicationId, governmentParams.BorrowerId)
    }


    private fun updateGovernmentQuestionApiCall() {

        if (governmentParams.Questions.size > 0) {
            //testGovernmentParams.BorrowerId = governmentParams.BorrowerId
            // testGovernmentParams.LoanApplicationId = governmentParams.LoanApplicationId
            for (question in governmentParams.Questions) {
                if (question.id == 21) {
                    Timber.e("what is  " + question.answerData)
                    if (ownershipInterestAnswerData1 == null)
                        question.answerData = null
                    if (ownershipInterestAnswerData1?.selectionOptionId == null)
                        question.answerData = null

                    continue
                }

                if (question.parentQuestionId == 130) {
                    if (bankruptcyMap.size == 0)
                        question.answerData = null
                    else {
                        val test = hashMapOf<String, String>()
                        val newTestList = arrayListOf(HashMap<String, String>())
                        for (item in bankruptcyMap) {
                            Timber.e("item kia ha ?" + item.value + "  and " + item.key)
                            test.put(item.key, item.value)
                            val json = Gson().toJson(test)
                            val mapCopy: HashMap<String, String> = Gson().fromJson(
                                json,
                                object : TypeToken<HashMap<String?, String>>() {}.type
                            )
                            newTestList.add(mapCopy)
                            test.clear()
                        }
                        newTestList.removeAt(0)
                        question.answerData = newTestList
                    }
                    continue
                }

                if (question.id == 22) {
                    Timber.e("what is  " + question.answerData)
                    if (ownershipInterestAnswerData2 == null)
                        question.answerData = null
                    if (ownershipInterestAnswerData2?.selectionOptionId == null)
                        question.answerData = null
                    continue
                }
                question.answerData = null

                if (question.id == 140) {
                    //newChild.answerData = childSupportAnswerDataList
                    question.answerData = childSupportAnswerDataList
                } else
                    if (question.id == 45) {   //family
                        //question.answerData = FamilyAnswerData()
                    }

                // ownership interest, it is handled when sent back....
                //  if(question.parentQuestionId == 20){}
                // Bankruptcy


            }
            //governmentParams.Questions.add(childQuestionData)

            lifecycleScope.launchWhenStarted {

                // //  borrowerAppViewModel.addOrUpdateGovernmentQuestions(LoginFragment.webtoken!!, governmentParams)
                val gson = Gson()
                var data = getDataList(jsonaddquestion.toString())
                val strJson = gson.toJson(questionmodel)
                Log.i("TAG", "updateGovernmentQuestionApiCall: "+strJson)

                borrowerAppViewModel.addgovernmetnjson("Bearer " + LoginFragment.webtoken!!, data!!)


//                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
//                    borrowerAppViewModel.addOrUpdateGovernmentQuestions(authToken, governmentParams)
//                }
            }
            findNavController().popBackStack()
        }


    }

    fun getDataList(tag: String?): DemoGetGovermentmodel? {
        var datalist: DemoGetGovermentmodel = DemoGetGovermentmodel()
        val gson = Gson()
        datalist = gson.fromJson<DemoGetGovermentmodel>(tag, object : TypeToken<DemoGetGovermentmodel?>() {}.type)
        return datalist
    }

    private fun listnser() {

        binding!!.q1radioButton.setOnClickListener {
            binding!!.q1radioButtonNo.isChecked = false
            discuss(binding!!.qh1.text.toString(), "1")
        }
        binding!!.q2radioButton.setOnClickListener {
            binding!!.q2radioButtonNo.isChecked = false

            discuss(binding!!.qh2.text.toString(), "2")
        }
        binding!!.q3radioButton.setOnClickListener {
            binding!!.q3radioButtonNo.isChecked = false

            discuss(binding!!.qh3.text.toString(), "3")
        }
        binding!!.q4radioButton.setOnClickListener {
            binding!!.q4radioButtonNo.isChecked = false

            discuss(binding!!.qh4.text.toString(), "4")
        }
        binding!!.q5radioButton.setOnClickListener {
            binding!!.q5radioButtonNo.isChecked = false

            discuss(binding!!.qh5.text.toString(), "5")
        }
        binding!!.q6radioButton.setOnClickListener {
            binding!!.q6radioButtonNo.isChecked = false

            discuss(binding!!.qh6.text.toString(), "6")
        }
        binding!!.q7radioButton.setOnClickListener {
            binding!!.q7radioButtonNo.isChecked = false

            discuss(binding!!.qh7.text.toString(), "7")
        }
        binding!!.q8radioButton.setOnClickListener {
            binding!!.q8radioButtonNo.isChecked = false

            discuss(binding!!.qh8.text.toString(), "8")
        }
        binding!!.q9radioButton.setOnClickListener {
            binding!!.q9radioButtonNo.isChecked = false

            discuss("Bankruptcy ", "9")
        }
        binding!!.q10radioButton.setOnClickListener {
            binding!!.q10radioButtonNo.isChecked = false

            discuss(
                "Child Support, Alimony, etc.",
                "10"
            )
        }
        binding!!.q1radioButtonNo.setOnClickListener {
            binding!!.q1radioButton.isChecked = false
            binding!!.qv1.visibility = View.GONE
        }
        binding!!.q2radioButtonNo.setOnClickListener {
            binding!!.q2radioButton.isChecked = false
            binding!!.qv2.visibility = View.GONE
        }
        binding!!.q3radioButtonNo.setOnClickListener {
            binding!!.q3radioButton.isChecked = false
            binding!!.qv3.visibility = View.GONE
        }
        binding!!.q4radioButtonNo.setOnClickListener {
            binding!!.q4radioButton.isChecked = false
            binding!!.qv4.visibility = View.GONE
        }

        binding!!.q5radioButtonNo.setOnClickListener {
            binding!!.q5radioButton.isChecked = false
            binding!!.qv5.visibility = View.GONE
        }
        binding!!.q6radioButtonNo.setOnClickListener {
            binding!!.q6radioButton.isChecked = false
            binding!!.qv6.visibility = View.GONE
        }
        binding!!.q7radioButtonNo.setOnClickListener {
            binding!!.q7radioButton.isChecked = false
            binding!!.qv7.visibility = View.GONE
        }
        binding!!.q8radioButtonNo.setOnClickListener {
            binding!!.q8radioButton.isChecked = false
            binding!!.qv8.visibility = View.GONE
        }
        binding!!.q9radioButtonNo.setOnClickListener {
            binding!!.q9radioButton.isChecked = false
            binding!!.qv9.visibility = View.GONE
        }


        binding!!.asiancheckbox.setOnClickListener {
            if(asiancheckbox.isChecked) {
                val copyAsianChildList = java.util.ArrayList(asianChildList.map { it.copy() })
                val bundle = bundleOf(AppConstant.asianChildList to copyAsianChildList)
                findNavController().navigate(R.id.action_asian, bundle)
            }
        }
        binding!!.nativehawaian.setOnClickListener {
            val copyNativeHawaiianChildList =
                java.util.ArrayList(nativeHawaiianChildList.map { it.copy() })
            val bundle = bundleOf(AppConstant.nativeHawaianChildList to copyNativeHawaiianChildList)
            findNavController().navigate(R.id.action_native_hawai, bundle)
        }

//               var jsonv = JSONObject()
//               jsonv.put("raceId",1)
//               race.put(jsonv)
        binding!!.checkbox.setOnClickListener {
           if(checkbox.isChecked){
               var race :  RaceModel = RaceModel()
               race.raceId = 1
               racearr!!.add(race)
           }
        }
        binding!!.checkboxblack.setOnClickListener {
            if(checkboxblack.isChecked){
                var race :  RaceModel = RaceModel()
                race.raceId = 3
                racearr!!.add(race)
            }
        }


        binding!!.whitecheckbox.setOnClickListener {
            if(whitecheckbox.isChecked){
                var race :  RaceModel = RaceModel()
                race.raceId = 5
                racearr!!.add(race)
            }
        }

        binding!!.idonotwishcheckbox.setOnClickListener {
            if(idonotwishcheckbox.isChecked){
                var race :  RaceModel = RaceModel()
                race.raceId = 6
                racearr!!.add(race)
            }
        }

        binding!!.ethnicidonotwishrbbox.setOnClickListener {
            if(ethnicidonotwishrbbox.isChecked){
                var eth :  EthnicityModel = EthnicityModel()
                eth.ethnicityId = 3
                //ethnicityarry!!.add(eth)
                binding!!.HispanicorLatino.isChecked = false
                binding!!.notsHispanicorLatino.isChecked = false
                ethnicityarry!!.add(0,eth)
            }
        }



        binding!!.HispanicorLatino.setOnClickListener {
            if(HispanicorLatino.isChecked){
                binding!!.notsHispanicorLatino.isChecked = false
                binding!!.ethnicidonotwishrbbox.isChecked = false
                val copyEthnicityChildList =
                    java.util.ArrayList(ethnicityChildList.map { it.copy() })
                val bundle = bundleOf(AppConstant.ethnicityChildList to copyEthnicityChildList)
                 findNavController().navigate(R.id.action_hispanic, bundle)
            }
        }

        binding!!.notsHispanicorLatino.setOnClickListener {
            if(notsHispanicorLatino.isChecked){
                var eth :  EthnicityModel = EthnicityModel()
                eth.ethnicityId = 2
                //ethnicityarry!!.add(eth)
                binding!!.HispanicorLatino.isChecked = false
                binding!!.sexidontsharerb.isChecked = false
                ethnicityarry!!.add(0,eth)
            }
        }

        binding!!.sexidontsharerb.setOnClickListener {
            if(sexidontsharerb.isChecked){
                var eth :  EthnicityModel = EthnicityModel()
                eth.ethnicityId = 3
                binding!!.HispanicorLatino.isChecked = false
                binding!!.notsHispanicorLatino.isChecked = false
                ethnicityarry!!.add(0,eth)
            }
        }

        binding!!.male.setOnClickListener {
            if(male.isChecked){
                if(demomodel != null){
                    demomodel.genderId = 1
                    binding!!.female.isChecked = false
                    binding!!.sexidontsharerb.isChecked = false
                }
            }
        }

        binding!!.female.setOnClickListener {
            if(female.isChecked){
                if(demomodel != null){
                    demomodel.genderId = 2
                    binding!!.male.isChecked = false
                    binding!!.sexidontsharerb.isChecked = false
                }
            }
        }

        binding!!.sexidontsharerb.setOnClickListener {
            if(sexidontsharerb.isChecked){

                if(demomodel != null){
                    demomodel.genderId = 3
                    binding!!.male.isChecked = false
                    binding!!.female.isChecked = false
                }
            }
        }

    }


    private fun updateGovernmentData(testData: QuestionData) {
        for (item in governmentParams.Questions) {
            if (item.id == testData.id) {
                item.answer = testData.answer
            }
        }
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun updateUndisclosedBorrowerFunds(updateEvent: UndisclosedBorrowerFundUpdateEvent) {
        if (updateEvent.whichBorrowerId == currentBorrowerId) {

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
                                                var questionmodel =
                                                    governmentQuestionsModelClassList.get(0).questionData
                                                for (qData in questionmodel!!) {
                                                    if (qData.id != null) {
                                                        qustionheaderarray!!.add(qData)
                                                        if (qData.answer != null) {
                                                         //   if (qData.answer == "Yes") {

                                                                if (qData.id == 11) {
                                                                    if (qData.answer != null && qData.answer!!.isNotEmpty()) {
                                                                        setdata(
                                                                            "",
                                                                            qData.answer!!,
                                                                            0,
                                                                            "1",
                                                                            qData.id,"0"
                                                                        )
                                                                    }
                                                                } else if (qData.id == 70) {
                                                                    setdata(
                                                                        "",
                                                                        qData.answerDetail!!,
                                                                        0,
                                                                        "3",
                                                                        qData.id,
                                                                        "0"
                                                                    )

                                                                } else if (qData.id == 80) {
                                                                    setdata(
                                                                        "",
                                                                        qData.answerDetail!!,
                                                                        0,
                                                                        "4",
                                                                        qData.id,
                                                                        "0"
                                                                    )

                                                                } else if (qData.id == 90) {
                                                                    setdata(
                                                                        "",
                                                                        qData.answerDetail!!,
                                                                        0,
                                                                        "5",
                                                                        qData.id,
                                                                        "0"
                                                                    )

                                                                } else if (qData.id == 100) {
                                                                    setdata(
                                                                        "",
                                                                        qData.answerDetail!!,
                                                                        0,
                                                                        "6",
                                                                        qData.id,
                                                                        "0"
                                                                    )

                                                                } else if (qData.id == 120) {
                                                                    setdata(
                                                                        "",
                                                                        qData.answerDetail!!,
                                                                        0,
                                                                        "8",
                                                                        qData.id,
                                                                        "0"
                                                                    )

                                                                } else if (qData.id == 130) {
//                                                                        setdata("",
//                                                                            qData.answerDetail!!,
//                                                                            0,
//                                                                            "9",
//                                                                            qData.id)

                                                                } else if (qData.id == 140) {
//                                                                    setarray(
//                                                                        "0",
//                                                                        qData.answerData as ArrayList<ChildAnswerData>,
//                                                                        0,
//                                                                        "10"
//                                                                    )

                                                                }
                                                           // }
                                                        }

                                                    } else {
                                                        subquestionarray!!.add(qData)
                                                        if (qData.answer != null) {

                                                        }
                                                    }

                                                    if (qData.answer != null) {
                                                        // setdata("", qData.answer.toString(), 1,"1")
                                                    }

                                                }


                                            }
                                        }
                                    }
                                }
                                updateDemoGraphicApiCall()
                                break
                            }
                        }
                    }

                }
            });
    }

    companion object {
        var data: DemoGraphicModel? = null
        var instan: AllGovQuestionsFragment? = null
        fun newInstance() = AllGovQuestionsFragment()

    }

    private fun navigateToInnerScreen(
        stringForSpecificFragment: String,
        questionId: Int,
        firstName: String?,
        lastName: String?,
        no: String
    ) {
        val bundle = Bundle()
        bundle.putInt(AppConstant.questionId, questionId)
        bundle.putInt(AppConstant.whichBorrowerId, currentBorrowerId)
        bundle.putParcelable(AppConstant.addUpdateQuestionsParams, governmentParams)
        bundle.putString(AppConstant.govtUserName, (firstName + " " + lastName))
        bundle.putString(AppConstant.questionno, no)

        when (stringForSpecificFragment) {
            "Undisclosed Borrowered Funds" -> {
                findNavController().navigate(R.id.action_undisclosed_borrowerfund, bundle)
            }
            "Family or Business affiliation" -> {
                findNavController().navigate(R.id.action_family_affiliation, bundle)
            }
            "Ownership Interest in Property" -> {
                bundle.putStringArrayList(
                    AppConstant.ownerShipGlobalData,
                    ownerShipInnerScreenParams
                )
                findNavController().navigate(R.id.action_ownership_interest, bundle)
            }
            "Own Property Type" -> {
            }
            "Debt Co-Signer or Guarantor" -> {
                findNavController().navigate(R.id.action_debt_co, bundle)
            }
            "Outstanding Judgements" -> {
                findNavController().navigate(R.id.action_outstanding, bundle)
            }
            "Federal Debt Deliquency" -> {
                findNavController().navigate(R.id.action_federal_debt, bundle)
            }
            "Party to Lawsuit" -> {
                findNavController().navigate(R.id.action_party_to, bundle)
            }
            "Bankruptcy " -> {
                val bankruptcyAnswerDataCopy =
                    bankruptcyAnswerData.copy() //ArrayList(bankruptcyAnswerData.map { it.copy() })
                bundle.putParcelable(AppConstant.bankruptcyAnswerData, bankruptcyAnswerDataCopy)
                bundle.putInt(AppConstant.questionId, questionId)
                findNavController().navigate(R.id.navigation_bankruptcy, bundle)
            }
            "Child Support, Alimony, etc." -> {
                bundle.putParcelableArrayList(
                    AppConstant.childGlobalList,
                    childSupportAnswerDataList
                )
                findNavController().navigate(R.id.action_child_support, bundle)
            }
            "Foreclosured Property" -> {
                findNavController().navigate(R.id.action_fore_closure_property, bundle)
            }
            "Pre-Foreclosureor Short Sale" -> {
                findNavController().navigate(R.id.action_pre_for_closure, bundle)
            }
            "Title Conveyance" -> {
                findNavController().navigate(R.id.action_title_conveyance, bundle)
            }
            else -> {
                Timber.e(" not matching with header title...")
            }
        }
    }

    fun setdata(
        detailTitle: String,
        title: String,
        whichBorrowerId: Int,
        questionnumber: String?,
        questionId: Int,
        s: String
    ) {
        when (questionnumber) {
            "1" -> {
                binding!!.qv1.visibility = View.VISIBLE
                binding!!.qva1.text = "$" + title
                updatedata(questionId, title, detailTitle, questionId,s)
                // setdataarray(questionId,10)


            }
            "2" -> {
                binding!!.qv2.visibility = View.VISIBLE
                binding!!.qv22.visibility = View.VISIBLE
                binding!!.qva2.text = detailTitle
                binding!!.qva22.text = title
                updatedata(questionId, title, detailTitle, questionId, s)

            }
            "3" -> {
                binding!!.qv3.visibility = View.VISIBLE
                binding!!.qvs3.text = "Detail"
                binding!!.qva3.text = title
                updatedata(questionId, title, detailTitle, questionId, s)

            }
            "4" -> {
                binding!!.qv4.visibility = View.VISIBLE
                //   binding!!.qvs4.text = title

                binding!!.qvs4.text = "Detail"
                binding!!.qva4.text = title
                updatedata(questionId, title, detailTitle, questionId, s)

            }
            "5" -> {
                binding!!.qv5.visibility = View.VISIBLE
                // binding!!.qvs5.text = title

                binding!!.qvs5.text = "Detail"
                binding!!.qva5.text = title
                updatedata(questionId, title, detailTitle, questionId, s)

            }
            "6" -> {
                binding!!.qv6.visibility = View.VISIBLE
                // binding!!.qvs6.text = title
                binding!!.qvs6.text = "Detail"
                binding!!.qva6.text = title
                updatedata(questionId, title, detailTitle, questionId, s)

            }
            "7" -> {
                binding!!.qv7.visibility = View.VISIBLE
                binding!!.qvs7.text = title
                updatedata(questionId, title, detailTitle, questionId, s)
            }
            "8" -> {
                binding!!.qv8.visibility = View.VISIBLE
                // binding!!.qvs8.text = title

                binding!!.qvs8.text = "Detail"
                binding!!.qva8.text = title
                updatedata(questionId, title, detailTitle, questionId, s)
            }
            "9" -> {
                binding!!.qv9.visibility = View.VISIBLE
                //  binding!!.qvs9.text = title

                binding!!.qvs9.text = "Which Type?"
                binding!!.qva9.text = title
                updatedata(questionId, title, detailTitle, questionId, s)
            }
            else -> { // Note the block
                print("x is neither 1 nor 2")
            }
        }
    }


    private fun updatedata(questionId: Int, title: String, detailTitle: String, i: Int, s: String) {
        governmentParams.Questions.let { questions ->
            jARRAY = JSONArray()
            jsonaddquestion = JSONObject()
             Questions = ArrayList()
            for (qData in questions) {
                if (questionId == qData.id) {
                    if (questionId == 130) {
                        var jone = JSONObject()
                        jone.put("id", qData.id)
                        jone.put("parentQuestionId", qData.parentQuestionId)
                        jone.put("headerText", qData.headerText)
                        jone.put("questionSectionId", qData.questionSectionId)
                        jone.put("ownTypeId", qData.ownTypeId)
                        jone.put("firstName", qData.firstName)
                        jone.put("lastName", qData.lastName)
                        jone.put("question", qData.question)

                        jone.put("selectionOptionId", qData.selectionOptionId)
                        jone.put("answerData", qData.answerData)
                        jARRAY.put(jone)
                        var jtwo = JSONArray()
                        var jone1 = JSONObject()
                        jone1.put("id", "131")
                        jone1.put("parentQuestionId", "130")
                        jone1.put("headerText", "Type")
                        jone1.put("questionSectionId", "3")
                        jone1.put("ownTypeId", "1")
                        jone1.put("firstName", qData.firstName)
                        jone1.put("lastName", qData.lastName)
                        jone1.put("question", "Which Type?")
                        jone1.put("answer", null)
                        jone1.put("answerDetail", null)
                        jone1.put("selectionOptionId", null)
                        var jone3 = JSONObject()
                        jone3.put("2", title)
                        jtwo.put(jone3)
                        jone1.put("answerData", jtwo)
                        jARRAY.put(jone1)


//                        var govern :  GetGovermentmodel = GetGovermentmodel()
//                        govern.id = qData.id
//                        govern.parentQuestionId = qData.parentQuestionId
//                        govern.headerText = qData.headerText
//                        govern.questionSectionId = qData.questionSectionId!!
//                        govern.ownTypeId = qData.ownTypeId!!
//                        govern.firstName= qData.firstName
//                        govern.lastName = qData.lastName
//                        govern.question = qData.question
//                        if(s == "1"){
//                            govern.answer = "Yes"
//                            govern.answerDetail = title
//
//                        }else{
//                            govern.answer = qData.question
//                            govern.answerDetail = qData.answerDetail
//
//                        }
//
//                        var ans :  answerData = answerData()
//                        ans.selectionOptionId = 1
//                        ans.selectionOptionText =title
//                        //govern.answerData = ans
//                        Questions!!.add(govern)
//
//                        var govern1 :  GetGovermentmodel = GetGovermentmodel()
//                        govern1.id = 131
//                        govern1.parentQuestionId = 130
//                        govern1.headerText = "Type"
//                        govern1.questionSectionId = 3
//                        govern1.ownTypeId = 1
//                        govern1.firstName= qData.firstName
//                        govern1.lastName = qData.lastName
//                        govern1.question = "Which Type?"
//                        govern1.answer = ""
//                        govern1.answerDetail = ""
//                        var ans1 :  answerData = answerData()
//                        ans1.selectionOptionId = 1
//                        ans1.selectionOptionText =title
//                       // govern1.answerData = ans1
//                        Questions!!.add(govern1)


                    } else {
                        if(questionId == 10) {
                            var jone = JSONObject()
                            jone.put("id", 11)
                            jone.put("parentQuestionId", qData.id)
                            jone.put("headerText", qData.headerText)
                            jone.put("questionSectionId", qData.questionSectionId)
                            jone.put("ownTypeId", qData.ownTypeId)
                            jone.put("firstName", qData.firstName)
                            jone.put("lastName", qData.lastName)
                            jone.put("question", qData.question)
                            jone.put("answer", "Yes")
                            jone.put("answerDetail", title)
                            jone.put("selectionOptionId", qData.selectionOptionId)
                            jARRAY.put(jone)

                        }else if (questionId != 131 && questionId != 140 && questionId != 11) {
                            var jone = JSONObject()
                            jone.put("id", qData.id)
                            jone.put("parentQuestionId", qData.parentQuestionId)
                            jone.put("headerText", qData.headerText)
                            jone.put("questionSectionId", qData.questionSectionId)
                            jone.put("ownTypeId", qData.ownTypeId)
                            jone.put("firstName", qData.firstName)
                            jone.put("lastName", qData.lastName)
                            jone.put("question", qData.question)
                            jone.put("answer", "Yes")
                            jone.put("answerDetail", title)
                            jone.put("selectionOptionId", qData.selectionOptionId)

//                            var govern :  GetGovermentmodel = GetGovermentmodel()
//                            govern.id = qData.id
//                            govern.parentQuestionId = qData.parentQuestionId
//                            govern.headerText = qData.headerText
//                            govern.questionSectionId = qData.questionSectionId!!
//                            govern.ownTypeId = qData.ownTypeId!!
//                            govern.firstName= qData.firstName
//                            govern.lastName = qData.lastName
//                            govern.question = qData.question
//
//                            if(s == "1"){
//                                govern.answer = "Yes"
//                                govern.answerDetail = title
//
//                            }else{
//                                govern.answer = qData.question
//                                govern.answerDetail = qData.answerDetail
//
//                            }



                            var jonet = JSONObject()
                            if(qData.answerData != null){
                                val getrow: Any = qData.answerData!!
                                val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                                val id = t["selectionOptionId"].toString().toDouble().toInt()
                                val text = t["selectionOptionText"].toString()
                                jonet.put("selectionOptionId",id)
                                jonet.put("selectionOptionText",text)
//                                var ans :  answerData = answerData()
//                                ans.selectionOptionId = id
//                                ans.selectionOptionText =text
                               // govern.answerData = ans


                            }
                            jone.put("answerData", jonet)
                            jARRAY.put(jone)

                           // Questions!!.add(govern)

                        }
                    }

                } else {
                    if (qData.id != 140 && qData.id != 131 && qData.id != 11) {
                        var jone = JSONObject()
                        jone.put("id", qData.id)
                        jone.put("parentQuestionId", qData.parentQuestionId)
                        jone.put("headerText", qData.headerText)
                        jone.put("questionSectionId", qData.questionSectionId)
                        jone.put("ownTypeId", qData.ownTypeId)
                        jone.put("firstName", qData.firstName)
                        jone.put("lastName", qData.lastName)
                        jone.put("question", qData.question)
                        jone.put("answer", qData.answer)
                        jone.put("answerDetail", qData.answerDetail)
                        jone.put("selectionOptionId", qData.selectionOptionId)

//                        var govern :  GetGovermentmodel = GetGovermentmodel()
//                        govern.id = qData.id
//                        govern.parentQuestionId = qData.parentQuestionId
//                        govern.headerText = qData.headerText
//                        govern.questionSectionId = qData.questionSectionId!!
//                        govern.ownTypeId = qData.ownTypeId!!
//                        govern.firstName= qData.firstName
//                        govern.lastName = qData.lastName
//                        govern.question = qData.question
//                        govern.answer = qData.question
//                        govern.answerDetail = qData.answerDetail






                        var jonet = JSONObject()
                        if(qData.answerData != null){
                            val getrow: Any = qData.answerData!!
                            val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                            if(t["selectionOptionId"] != null) {
                                val id = t["selectionOptionId"].toString().toDouble().toInt()
                                val text = t["selectionOptionText"].toString()
                                jonet.put("selectionOptionId", id)
                                jonet.put("selectionOptionText", text)
                                jone.put("answerData", jonet)
//                                var ans :  answerData = answerData()
//                                ans.selectionOptionId = id
//                                ans.selectionOptionText =title
                                //govern.answerData = ans
                            }
                        }


                       // Questions!!.add(govern)
                        jARRAY.put(jone)
                    }
                }


            }

//            questionmodel = DemoGetGovermentmodel()
//            questionmodel.BorrowerId = governmentParams.BorrowerId
//            questionmodel.LoanApplicationId = governmentParams.LoanApplicationId
//            questionmodel.Questions = Questions


            jsonaddquestion.put("Questions", jARRAY)
            jsonaddquestion.put("BorrowerId", governmentParams.BorrowerId)
            jsonaddquestion.put("LoanApplicationId", governmentParams.LoanApplicationId)
            Log.i("TAG", "updatedata: " + jsonaddquestion)
            Log.i("TAG", "updatedata: " + questionmodel)

        }
    }

    fun discuss(detailTitle: String, no: String) {
        for (qData in qustionheaderarray!!) {
            if (qData.headerText == detailTitle) {
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
        childAnswerList: ArrayList<ChildAnswerData>,
        whichBorrowerId: Int,
        s1: String
    ) {

        governmentParams.Questions.let { questions ->
            for (qData in questions) {
                answerData = JSONArray()
                if (qData.id == 140) {
                    var jone = JSONObject()
                    jone.put("id", qData.id)
                    jone.put("parentQuestionId", qData.parentQuestionId)
                    jone.put("headerText", qData.headerText)
                    jone.put("questionSectionId", qData.questionSectionId)
                    jone.put("ownTypeId", qData.ownTypeId)
                    jone.put("firstName", qData.firstName)
                    jone.put("lastName", qData.lastName)
                    jone.put("question", qData.question)
                    jone.put("answer", "Yes")
                    jone.put("answerDetail", null)
                    jone.put("selectionOptionId", qData.selectionOptionId)

                    for (i in 0 until childAnswerList.size) {
                        binding!!.qv10.visibility = View.VISIBLE

                        if (i == 0) {
                            var jonedata = JSONObject()
                           // val intDemo = strDemo!!.floatToInt()

                            if(s == "0") {
                                val getrow: Any = childAnswerList[i]
                                val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                                val liabilityName = t["liabilityName"].toString()
                                val monthlyPayment =
                                    t["monthlyPayment"].toString().toDouble().toInt()
                                val liabilityTypeId =
                                    t["liabilityTypeId"].toString().toDouble().toInt()
                                val name = t["name"].toString()
                                val remainingMonth =
                                    t["remainingMonth"].toString().toDouble().toInt()


                                jonedata.put("liabilityTypeId", liabilityTypeId)
                                jonedata.put("liabilityName", liabilityName)
                                jonedata.put("remainingMonth", remainingMonth)
                                jonedata.put("monthlyPayment", monthlyPayment)
                                jonedata.put("name", name)
                                binding!!.qvs101.text = liabilityName
                                binding!!.qva101.text = monthlyPayment.toString()

                            }else{

                                jonedata.put("liabilityTypeId", childAnswerList.get(i).liabilityTypeId.toString().toDouble().toInt())
                                jonedata.put("liabilityName", childAnswerList.get(i).liabilityName)
                                jonedata.put("remainingMonth", childAnswerList.get(i).remainingMonth.toString().toDouble().toInt())
                                jonedata.put("monthlyPayment", childAnswerList.get(i).monthlyPayment.toString().toDouble().toInt())
                                jonedata.put("name", childAnswerList.get(i).name)

                                binding!!.qvs101.text =  childAnswerList.get(i).liabilityName
                                binding!!.qva101.text = childAnswerList.get(i).monthlyPayment.toString()
                            }
                            answerData.put(jonedata)
                            binding!!.one.visibility = View.VISIBLE
                            binding!!.qva101.visibility = View.VISIBLE


                        } else if (i == 1) {

                            var jonedata = JSONObject()
                            if(s == "0") {
                                val getrow: Any = childAnswerList[i]
                                val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                                val liabilityName = t["liabilityName"].toString()
                                val monthlyPayment =
                                    t["monthlyPayment"].toString().toDouble().toInt()
                                val liabilityTypeId =
                                    t["liabilityTypeId"].toString().toDouble().toInt()
                                val name = t["name"].toString()
                                val remainingMonth =
                                    t["remainingMonth"].toString().toDouble().toInt()



                                jonedata.put("liabilityTypeId", liabilityTypeId)
                                jonedata.put("liabilityName", liabilityName)
                                jonedata.put("remainingMonth", remainingMonth)
                                jonedata.put("monthlyPayment", monthlyPayment)
                                jonedata.put("name", name)
                                binding!!.qvs102.text = liabilityName
                                binding!!.qva102.text = monthlyPayment.toString()
                            }else {

                                jonedata.put("liabilityTypeId", childAnswerList.get(i).liabilityTypeId.toString().toDouble().toInt())
                                jonedata.put("liabilityName", childAnswerList.get(i).liabilityName)
                                jonedata.put("remainingMonth", childAnswerList.get(i).remainingMonth.toString().toDouble().toInt())
                                jonedata.put("monthlyPayment", childAnswerList.get(i).monthlyPayment.toString().toDouble().toInt())
                                jonedata.put("name", childAnswerList.get(i).name)

                                binding!!.qvs102.text =  childAnswerList.get(i).liabilityName
                                binding!!.qva102.text = childAnswerList.get(i).monthlyPayment.toString()

                            }

                            answerData.put(jonedata)
                            binding!!.two.visibility = View.VISIBLE
                            binding!!.qva102.visibility = View.VISIBLE

                        } else if (i == 2) {
                            var jonedata = JSONObject()
                            if(s == "0") {
                                val getrow: Any = childAnswerList[i]
                                val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                                val liabilityName = t["liabilityName"].toString()
                                val monthlyPayment =
                                    t["monthlyPayment"].toString().toDouble().toInt()
                                val liabilityTypeId =
                                    t["liabilityTypeId"].toString().toDouble().toInt()
                                val name = t["name"].toString()
                                val remainingMonth =
                                    t["remainingMonth"].toString().toDouble().toInt()



                                jonedata.put("liabilityTypeId", liabilityTypeId)
                                jonedata.put("liabilityName", liabilityName)
                                jonedata.put("remainingMonth", remainingMonth)
                                jonedata.put("monthlyPayment", monthlyPayment)
                                jonedata.put("name", name)

                                binding!!.qvs103.text = liabilityName
                                binding!!.qva103.text = monthlyPayment.toString()

                            }else{
                                jonedata.put("liabilityTypeId", childAnswerList.get(i).liabilityTypeId.toString().toDouble().toInt())
                                jonedata.put("liabilityName", childAnswerList.get(i).liabilityName)
                                jonedata.put("remainingMonth", childAnswerList.get(i).remainingMonth.toString().toDouble().toInt())
                                jonedata.put("monthlyPayment", childAnswerList.get(i).monthlyPayment.toString().toDouble().toInt())
                                jonedata.put("name", childAnswerList.get(i).name)

                                binding!!.qvs103.text =  childAnswerList.get(i).liabilityName
                                binding!!.qva103.text = childAnswerList.get(i).monthlyPayment.toString()

                            }
                            answerData.put(jonedata)
                            binding!!.three.visibility = View.VISIBLE
                            binding!!.qva103.visibility = View.VISIBLE

                        }
                        //jone.put("answerData", answerData)
                        jARRAY.put(jone)
                        jsonaddquestion.put("Questions", jARRAY)
                        jsonaddquestion.put("BorrowerId", governmentParams.BorrowerId)
                        jsonaddquestion.put("LoanApplicationId", governmentParams.LoanApplicationId)
                    }
                }
            }
        }

    }

    fun apidemographic(asianChildList: ArrayList<DemoGraphicRaceDetail>) {
        val copyAsianChildList = java.util.ArrayList(this.asianChildList.map { it.copy() })

        var race :  RaceModel = RaceModel()
        race.raceId = 2
        for (i in 0 until asianChildList.size) {
               var deatil :  Detailmodel = Detailmodel()
               deatil.detailID = asianChildList.get(i).detailId!!
               deatil.isOther = asianChildList.get(i).isOther!!
               race.raceDetails!!.add(deatil)


//            if(item.detailId == 7){
//                item.otherRace = binding.edDetails.text.toString()
//            }
        }
        racearr!!.add(race)


    }
    fun nativeHawaiianChildList(asianChildList: ArrayList<DemoGraphicRaceDetail>) {
        val copyAsianChildList = java.util.ArrayList(this.asianChildList.map { it.copy() })


        var race :  RaceModel = RaceModel()
        race.raceId = 4
        for (i in 0 until asianChildList.size) {
            var deatil :  Detailmodel = Detailmodel()
            deatil.detailID = asianChildList.get(i).detailId!!
            deatil.isOther = asianChildList.get(i).isOther!!
            race.raceDetails!!.add(deatil)


//            if(item.detailId == 7){
//                item.otherRace = binding.edDetails.text.toString()
//            }
        }
        racearr!!.add(race)

    }


    fun nativehispanic(asianChildList: ArrayList<EthnicityDetailDemoGraphic>) {
        val copyAsianChildList = java.util.ArrayList(this.asianChildList.map { it.copy() })

            var race :  EthnicityModel = EthnicityModel()
        race.ethnicityId = 1
        for (i in 0 until asianChildList.size) {
            var deatil :  Detailmodel = Detailmodel()
            deatil.detailID = asianChildList.get(i).detailId!!
            deatil.isOther = asianChildList.get(i).isOther!!
            race.ethnicityDetails!!.add(deatil)


//            if(item.detailId == 7){
//                item.otherRace = binding.edDetails.text.toString()
//            }
        }

        ethnicityarry!!.add(0,race)

    }

}