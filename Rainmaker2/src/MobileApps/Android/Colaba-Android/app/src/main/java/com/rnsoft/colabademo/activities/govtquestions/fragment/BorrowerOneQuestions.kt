package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatTextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.*
import timber.log.Timber
import android.widget.LinearLayout
import android.util.DisplayMetrics
import androidx.core.os.bundleOf
import androidx.lifecycle.lifecycleScope
import kotlinx.android.synthetic.main.children_separate_layout.view.*
import kotlinx.android.synthetic.main.common_govt_content_layout.view.ans_no
import kotlinx.android.synthetic.main.common_govt_content_layout.view.ans_yes
import kotlinx.android.synthetic.main.common_govt_content_layout.view.detail_text
import kotlinx.android.synthetic.main.common_govt_content_layout.view.detail_title
import kotlinx.android.synthetic.main.common_govt_content_layout.view.govt_detail_box
import kotlinx.android.synthetic.main.common_govt_content_layout.view.govt_question
import com.google.gson.Gson
import kotlin.collections.ArrayList
import kotlin.collections.HashMap
import com.google.gson.internal.LinkedTreeMap
import com.google.gson.reflect.TypeToken
import com.rnsoft.colabademo.utils.Common
import com.rnsoft.colabademo.utils.JSONUtils
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.android.synthetic.main.children_separate_layout.view.detail_text2
import kotlinx.android.synthetic.main.children_separate_layout.view.detail_title2
import kotlinx.android.synthetic.main.children_separate_layout.view.govt_detail_box2

/*
import kotlinx.android.synthetic.main.ownership_interest_layout.view.detail_text2
import kotlinx.android.synthetic.main.ownership_interest_layout.view.detail_title2
import kotlinx.android.synthetic.main.ownership_interest_layout.view.detail_text
import kotlinx.android.synthetic.main.ownership_interest_layout.view.detail_title
import kotlinx.android.synthetic.main.ownership_interest_layout.view.govt_detail_box
import kotlinx.android.synthetic.main.ownership_interest_layout.view.detail_title2

 */


import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.*
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.american_or_indian_check_box
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.asian_check_box
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.black_or_african_check_box
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.do_not_wish_check_box
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.hispanic_or_latino
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.native_hawaian_or_other_check_box
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.not_hispanic
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.not_telling_ethnicity
import kotlinx.android.synthetic.main.new_demo_graphic_show_layout.view.white_check_box
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject



interface JSONConvertable {
    fun toJSON(): String = Gson().toJson(this)
}


inline fun <reified T: JSONConvertable> String.toObject(): T = Gson().fromJson(this, T::class.java)


@AndroidEntryPoint
class BorrowerOneQuestions : GovtQuestionBaseFragment(), JSONConvertable {

    private lateinit var binding: BorrowerOneQuestionsLayoutBinding

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    private var idToContentMapping = HashMap<Int, ConstraintLayout>(0)
    private var innerLayoutHashMap = HashMap<AppCompatTextView, ConstraintLayout>(0)
    //private var openDetailBoxHashMap = HashMap<AppCompatRadioButton, ConstraintLayout>(0)
    //private var closeDetailBoxHashMap = HashMap<AppCompatRadioButton, ConstraintLayout>(0)
    //private val tabArrayList:ArrayList<AppCompatTextView> = arrayListOf()

    private val borrowerAppViewModel: BorrowerApplicationViewModel by activityViewModels()

    private var ethnicityChildNames = ""
    private var otherEthnicity = ""

    private var nativeHawaiiChildNames = ""
    private var nativeHawaiiOtherRace = ""

    private var asianChildNames = ""
    private var otherAsianRace = ""

    private  var tabBorrowerId:Int? = null

    //private var udateGovernmentQuestionsList:ArrayList<UpdateGovernmentQuestions> = arrayListOf()

    //companion object{
    var childGlobalList:ArrayList<ChildAnswerData> = arrayListOf()
    var bankruptcyGlobalData:BankruptcyAnswerData = BankruptcyAnswerData()
    var ownerShipInnerScreenParams:ArrayList<String> = arrayListOf()
    // }

    private  lateinit var lastQData:QuestionData

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = BorrowerOneQuestionsLayoutBinding.inflate(inflater, container, false)
        arguments?.let {
            tabBorrowerId = it.getInt(AppConstant.tabBorrowerId)
        }
        binding.saveBtn.setOnClickListener {
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    if (demoGraphicScreenDisplaying) {
                        variableDemoGraphicData.genderId = variableGender
                        variableDemoGraphicData.race = variableRaceList
                        variableDemoGraphicData.ethnicity = variableEthnicityList
                        borrowerAppViewModel.addOrUpdateDemoGraphic(authToken, variableDemoGraphicData)
                    } else {
                        borrowerAppViewModel.addOrUpdateGovernmentQuestions(authToken, governmentParams)
                    }
                    EventBus.getDefault().postSticky(BorrowerApplicationUpdatedEvent(true))
                    requireActivity().finish()
                }
            }
        }

        setUpDynamicTabs()
        super.addListeners(binding.root)
        return binding.root
    }


    private var governmentParams = GovernmentParams()

    private var saveGovtQuestionForDetailAnswer:ArrayList<QuestionData>? = null

    data class TestAnswerData(
        val selectionOptionId: String,
        val selectionOptionText: String
    )

    private fun guessTheType(any: Any) = when (any){
        is Int -> Timber.e("It's an Integer !")
        is String -> Timber.e("It's a String !")
        is Boolean -> Timber.e("It's a Boolean !")
        is Array<*> -> Timber.e("It's an Array !")
        is ArrayList<*> -> Timber.e("It's an ArrayList !")
        is Gson-> Timber.e("It's an Gson !")
        is List<*> -> Timber.e("It's a List !")
        is Set<*> -> Timber.e("It's a Set !")
        is TestAnswerData -> Timber.e("It's a TestAnswerData !")
        is ChildAnswerData -> Timber.e("It's an ChildAnswerData !")
        is BankruptcyAnswerData -> Timber.e("It's an BankruptcyAnswerData !")
        else -> Timber.e("Error ! Type not recognized...")
    }




    private fun setUpDynamicTabs(){
        val governmentQuestionActivity = (activity as? GovtQuestionActivity)

        borrowerAppViewModel.governmentQuestionsModelClassList.observe(viewLifecycleOwner,  { governmentQuestionsModelClassList->

            if(governmentQuestionsModelClassList.size>0) {
                var selectedGovernmentQuestionModel: GovernmentQuestionsModelClass? = null
                for (item in governmentQuestionsModelClassList) {
                    if (item.passedBorrowerId == tabBorrowerId) {
                        selectedGovernmentQuestionModel = item

                        governmentQuestionActivity?.let { governmentQuestionActivity ->
                            governmentQuestionActivity.loanApplicationId?.let { nonNullLoanApplicationId ->
                                item.questionData?.let { questionDataList ->



                                    for(question in questionDataList){



                                        //var yourModel = gson.fromJson(yourJsonString, YourModel::class.java)

                                        question.id?.let{ questionId->
                                            if(questionId == 140 || questionId == 130 || questionId == 45)
                                                question.answerData = null
                                        }
                                        question.parentQuestionId?.let { parentQuestionId ->
                                            var validJson = false
                                            question.answerData?.let {
                                                validJson = JSONUtils.isJSONValid(it.toString())
                                                Timber.e("bool is  $validJson")
                                                question.headerText?.let{
                                                    Timber.e(" header text = "+it)
                                                }

                                                guessTheType(it)
                                            }

                                            val gson = Gson()

                                            if(parentQuestionId == 140 || parentQuestionId == 130)
                                                question.answerData = null

                                            if(parentQuestionId == 20){
                                                //= question.answerData as answerData
                                                question.answerData?.let {

                                                    //val extractValue = (it as TestAnswerData).selectionOptionId
                                                    //val listType: Type = object : TypeToken<ArrayList<TestAnswerData?>?>() {}.getType()
                                                    //val yourClassList: List<TestAnswerData> = Gson().fromJson(jsonArray, listType)
                                                    //val obj:TestAnswerData = Gson().fromJson(Gson().toJson(((LinkedTreeMap<String, Object>) TestAnswerData)), TestAnswerData.class)
                                                    //= new Gson().fromJson(new Gson().toJson(((LinkedTreeMap<String, Object>) theLinkedTreeMapObject)), MyClass .class)


                                                    val t: LinkedTreeMap<Any, Any> = it as LinkedTreeMap<Any, Any>
                                                    val selectionOptionId = t["selectionOptionId"].toString()
                                                    val selectionOptionText = t["selectionOptionText"].toString()

                                                    Timber.e("selectionOptionId $selectionOptionId")

                                                    if(validJson) {
                                                        val yourModel = gson.fromJson(
                                                            it.toString(),
                                                            TestAnswerData::class.java
                                                        )


                                                        val yourObject = gson.fromJson( it.toString(), TestAnswerData::class.java)
                                                        Timber.e("value is " + yourModel.selectionOptionId +" and "+
                                                            yourModel.selectionOptionText
                                                        )
                                                    }
                                                }
                                            }
                                        }



                                    }

                                    item.passedBorrowerId?.let { passedBorrowerId ->
                                        governmentParams =
                                            GovernmentParams(
                                                passedBorrowerId, nonNullLoanApplicationId,
                                                questionDataList
                                            )
                                        Timber.e(
                                            "TingoPingo = ",
                                            governmentParams.BorrowerId,
                                            governmentParams.toString()
                                        )
                                        //udateGovernmentQuestionsList.add(test)
                                    }
                                }
                            }
                        }

                        break
                    }
                }


                selectedGovernmentQuestionModel?.let{ selectedGovernmentQuestionModel->

                    val govtQuestionActivity = (activity as GovtQuestionActivity)
                    govtQuestionActivity.binding.govtDataLoader.visibility = View.INVISIBLE
                    var zeroIndexAppCompat:AppCompatTextView?= null
                    childGlobalList= arrayListOf()
                    bankruptcyGlobalData = BankruptcyAnswerData()
                    ownerShipInnerScreenParams = arrayListOf()

                    selectedGovernmentQuestionModel.questionData?.let { questionData ->
                        saveGovtQuestionForDetailAnswer = questionData

                        for (qData in questionData) {
                            lastQData = qData
                            qData.headerText?.let { tabTitle ->
                                if (qData.parentQuestionId == null) {
                                    binding.parentContainer.visibility = View.INVISIBLE
                                    val appCompactTextView = createAppCompactTextView(tabTitle, 0)
                                    if (zeroIndexAppCompat == null)
                                        zeroIndexAppCompat = appCompactTextView
                                    //tabArrayList.add(appCompactTextView)
                                    binding.horizontalTabs.addView(appCompactTextView)
                                    val contentView = createContentLayoutForTab(qData)

                                    innerLayoutHashMap.put(appCompactTextView, contentView)
                                    qData.id?.let { it1 -> idToContentMapping.put(it1, contentView) }

                                    binding.parentContainer.addView(contentView)
                                    appCompactTextView.setOnClickListener(openTabMenuScreen)
                                    horizontalTabArrayList.add(appCompactTextView)
                                }
                            }
                        }

                        // adding demo graphic tab here....
                        if (zeroIndexAppCompat != null) {
                            val appCompactTextView = createAppCompactTextView(AppConstant.demographicInformation, 0)
                            binding.horizontalTabs.addView(appCompactTextView)
                            val contentView = createContentLayoutForTab(
                                QuestionData(
                                    id = 5000, parentQuestionId = null, headerText = AppConstant.demographicInformation,
                                    questionSectionId = 1, ownTypeId = 1, firstName = lastQData.firstName, lastName = lastQData.lastName,
                                    question = "", answer = null, answerDetail = null, selectionOptionId = null, answerData = null))

                            innerLayoutHashMap.put(appCompactTextView, contentView)
                            idToContentMapping.put(5000, contentView)

                            binding.parentContainer.addView(contentView)
                            appCompactTextView.setOnClickListener(openTabMenuScreen)
                            horizontalTabArrayList.add(appCompactTextView)
                        }

                        var ownerShipBoxOneEnabled = true

                        for (qData in questionData) {
                            qData.parentQuestionId?.let { parentQuestionId ->
                                Timber.e("parentQuestionId...$parentQuestionId")
                                if (parentQuestionId == bankruptcyConstraintLayout.id) {
                                        Timber.e("bankruptcyConstraintLayout " + qData.question)
                                        Timber.e(qData.answerDetail.toString())
                                        var extractedAnswer = ""
                                        qData.answerData?.let {
                                            val bankruptAnswerData = it as ArrayList<*>
                                            if (bankruptAnswerData.size > 0) {
                                                if (bankruptAnswerData[0] != null) {
                                                    val getrow: Any = bankruptAnswerData[0]
                                                    val t: LinkedTreeMap<Any, Any> =
                                                        getrow as LinkedTreeMap<Any, Any>
                                                    val chapter1 = t["`1`"].toString()
                                                    extractedAnswer = chapter1
                                                    bankruptcyGlobalData.value1 = true
                                                    Timber.e("1 = " + chapter1)
                                                }
                                                if (bankruptAnswerData.size > 1 && bankruptAnswerData[1] != null) {
                                                    val getrow: Any = bankruptAnswerData[1]
                                                    val t: LinkedTreeMap<Any, Any> =
                                                        getrow as LinkedTreeMap<Any, Any>
                                                    val chapter2 = t["`2`"].toString()
                                                    extractedAnswer = "$extractedAnswer, $chapter2"
                                                    bankruptcyGlobalData.value2 = true
                                                    Timber.e("2 = " + chapter2)

                                                }

                                                if (bankruptAnswerData.size > 2 && bankruptAnswerData[2] != null) {
                                                    val getrow: Any = bankruptAnswerData[2]
                                                    val t: LinkedTreeMap<Any, Any> =
                                                        getrow as LinkedTreeMap<Any, Any>
                                                    val chapter3 = t["`3`"].toString()
                                                    extractedAnswer = "$extractedAnswer, $chapter3"
                                                    bankruptcyGlobalData.value3 = true
                                                    Timber.e("3 = " + chapter3)
                                                }

                                                if (bankruptAnswerData.size > 3 && bankruptAnswerData[3] != null) {
                                                    val getrow: Any = bankruptAnswerData[3]
                                                    val t: LinkedTreeMap<Any, Any> =
                                                        getrow as LinkedTreeMap<Any, Any>
                                                    val chapter4 = t["`4`"].toString()
                                                    bankruptcyGlobalData.value4 = true
                                                    extractedAnswer = "$extractedAnswer, $chapter4"
                                                    Timber.e("4 = " + chapter4)
                                                }

                                                Timber.e(" extracted answer = " + extractedAnswer)
                                                bankruptcyConstraintLayout.detail_text.text =
                                                    extractedAnswer
                                                bankruptcyConstraintLayout.detail_title.setTypeface(
                                                    null,
                                                    Typeface.NORMAL
                                                )
                                                bankruptcyConstraintLayout.detail_text.setTypeface(
                                                    null,
                                                    Typeface.BOLD
                                                )
                                                bankruptcyConstraintLayout.govt_detail_box.visibility =
                                                    View.VISIBLE
                                            }
                                        }
                                    }
                                else
                                if (parentQuestionId == childConstraintLayout.id) {
                                    Timber.e("childConstraintLayout " + qData.question)
                                    Timber.e(qData.answerDetail.toString())
                                }
                                else
                                if (parentQuestionId == ownerShipConstraintLayout.id ) {
                                    qData.answer?.let {answer->
                                        if(!answer.equals("No", true)){
                                            if(ownerShipBoxOneEnabled) {
                                                ownerShipBoxOneEnabled = false
                                                Timber.e("ownerShipConstraintLayout " + qData.question)
                                                Timber.e(qData.answerDetail.toString())
                                                ownerShipConstraintLayout.detail_title.text = qData.question
                                                ownerShipConstraintLayout.detail_text.text = qData.answer
                                                ownerShipConstraintLayout.detail_title.setTypeface(null, Typeface.NORMAL)
                                                ownerShipConstraintLayout.detail_text.setTypeface(null, Typeface.BOLD)
                                                ownerShipInnerScreenParams.add(qData.answer!!)
                                                ownerShipConstraintLayout.govt_detail_box.visibility = View.VISIBLE
                                            }
                                            else{
                                                ownerShipConstraintLayout.detail_title2.text = qData.question
                                                ownerShipConstraintLayout.detail_text2.text = qData.answer
                                                ownerShipConstraintLayout.detail_title2.setTypeface(null, Typeface.NORMAL)
                                                ownerShipConstraintLayout.detail_text2.setTypeface(null, Typeface.BOLD)
                                                ownerShipInnerScreenParams.add(qData.answer!!)
                                                ownerShipConstraintLayout.govt_detail_box2.visibility = View.VISIBLE
                                            }
                                        }
                                    }
                                }
                                else
                                if (parentQuestionId == undisclosedLayout.id) {
                                    qData.answer?.let { answer->
                                        if (answer.isNotEmpty() && answer.isNotBlank()) {
                                            Timber.e("undisclosedLayout " + qData.question)
                                            Timber.e(qData.answerDetail.toString())
                                            undisclosedLayout.detail_title.text = qData.question
                                            undisclosedLayout.detail_text.text = answer

                                            //ownerShipGlobalData.add(qData.answer!!)
                                            undisclosedLayout.govt_detail_box.visibility = View.VISIBLE
                                        }
                                    }
                                    undisclosedLayout.detail_title.setTypeface(null, Typeface.NORMAL)
                                    undisclosedLayout.detail_text.setTypeface(null, Typeface.BOLD)
                                }
                                else
                                    Timber.e("nothing")

                                }
                            }
                    }  // close of let block...
                    zeroIndexAppCompat?.performClick()
                }
            }
            binding.parentContainer.postDelayed({ binding.parentContainer.visibility = View.VISIBLE },100)

        })
    }

    private fun createAppCompactTextView(tabTitle:String, tabIndex:Int):AppCompatTextView{
        //val appCompactTextView = AppCompatTextView(requireContext())

        val appCompactTextView: AppCompatTextView =
            layoutInflater.inflate(R.layout.govt_text_view, null) as AppCompatTextView

        //appCompactTextView.setBackgroundColor(R.drawable.blue_white_style_filter)
        //appCompactTextView.setPadding(12,0,12,0)
        //appCompactTextView.height = 40

        val textParam = LinearLayout.LayoutParams(
            LinearLayout.LayoutParams.WRAP_CONTENT,
            convertDpToPixel(30.0f,requireContext()),
            1.0f
        )

        textParam.setMargins( convertDpToPixel(4.0f,requireContext()), 0, convertDpToPixel(4.0f,requireContext()), 0)

        appCompactTextView.setLayoutParams(textParam)

       // appCompactTextView.setTextColor(resources.getColor(R.color.doc_filter_text_color_selector, activity?.theme ))
        //appCompactTextView.gravity = Gravity.CENTER
        //appCompactTextView.setTextSize(13, 13F)
        //appCompactTextView.isAllCaps = false
        //appCompactTextView.id = tabIndex
        appCompactTextView.tag = tabTitle
        appCompactTextView.text = tabTitle
        return appCompactTextView
    }

    private lateinit var ownerShipConstraintLayout:ConstraintLayout
    private lateinit var childConstraintLayout:ConstraintLayout
    private lateinit var undisclosedLayout:ConstraintLayout
    private lateinit var bankruptcyConstraintLayout:ConstraintLayout
    private lateinit var dGlayout:ConstraintLayout

    //private lateinit var variableQuestionData: QuestionData


    private lateinit var clickedContentCell:ConstraintLayout

    private fun createContentLayoutForTab(questionData:QuestionData):ConstraintLayout{
        val variableQuestionData: QuestionData = questionData
        var childSupport = false
        var ownerShip = false
        var headerTitle = ""
        val contentCell: ConstraintLayout
        if(questionData.headerText == AppConstant.ownershipConstantValue) {
            ownerShipConstraintLayout = layoutInflater.inflate(R.layout.ownership_interest_layout, null) as ConstraintLayout
            ownerShip = true
            contentCell = ownerShipConstraintLayout
            questionData.id?.let {
                ownerShipConstraintLayout.id = it
                contentCell.id = it
            }
        }
        else
        if(questionData.headerText == AppConstant.UndisclosedBorrowerFunds) {
            contentCell = layoutInflater.inflate(R.layout.common_govt_content_layout, null) as ConstraintLayout
            contentCell.detail_title.text = UndisclosedBorrowerFundFragment.UndisclosedBorrowerQuestionConstant
            contentCell.govt_detail_box.detail_title.setTypeface(null, Typeface.NORMAL)

            undisclosedLayout = contentCell
            questionData.id?.let {
                undisclosedLayout.id = it
                contentCell.id = it
            }
        }
        else
        if(questionData.headerText == AppConstant.childConstantValue) {
            contentCell = layoutInflater.inflate(R.layout.children_separate_layout, null) as ConstraintLayout
            childSupport = true
            childConstraintLayout = contentCell
            questionData.id?.let {
                childConstraintLayout.id = it
                contentCell.id = it
            }
        }
        else
        if(questionData.headerText?.trim() == AppConstant.Bankruptcy) {
            contentCell = layoutInflater.inflate(R.layout.common_govt_content_layout, null) as ConstraintLayout
            bankruptcyConstraintLayout = contentCell
            questionData.id?.let {
                bankruptcyConstraintLayout.id = it
                contentCell.id = it
            }
        }
        else
        if(questionData.headerText?.trim() == AppConstant.demographicInformation) {
            contentCell = layoutInflater.inflate(R.layout.new_demo_graphic_show_layout, null) as ConstraintLayout
            dGlayout = contentCell
            questionData.id?.let {
                dGlayout.id = it
                contentCell.id = it
            }
            contentCell.visibility = View.INVISIBLE
            dGlayout.visibility = View.INVISIBLE
            observeDemoGraphicData(contentCell)
            return contentCell
        }
        else {
            contentCell = layoutInflater.inflate(R.layout.common_govt_content_layout, null) as ConstraintLayout
            questionData.id?.let {
              contentCell.id = it
            }
        }


        questionData.headerText?.let {
            contentCell.govt_detail_box.tag = it
            contentCell.ans_yes.tag = it
            headerTitle = it
        }
        contentCell.govt_question.text =  questionData.question
        questionData.answerDetail?.let {
            contentCell.detail_text.text = it
        }
        var questionId:Int = -100
        questionData.id?.let {
            questionId = it
        }

        if(ownerShip) {
            contentCell.govt_detail_box.setOnClickListener {
                clickedContentCell = contentCell
                navigateToInnerScreen(headerTitle , questionId)
            }
        }

        if(childSupport){
            contentCell.govt_detail_box.setOnClickListener {
                clickedContentCell = contentCell
                navigateToInnerScreen(headerTitle ,  questionId)
            }
            contentCell.govt_detail_box2.setOnClickListener {
                clickedContentCell = contentCell
                navigateToInnerScreen(headerTitle , questionId)
            }
            contentCell.govt_detail_box3.setOnClickListener {
                clickedContentCell = contentCell
                navigateToInnerScreen(headerTitle , questionId)
            }
            val childInnerDetailQuestions:ArrayList<ConstraintLayout> = arrayListOf()
            childInnerDetailQuestions.add(contentCell.govt_detail_box)
            childInnerDetailQuestions.add(contentCell.govt_detail_box2)
            childInnerDetailQuestions.add(contentCell.govt_detail_box3)
            questionData.answerData?.let { notNullChildAnswerData->
                val childAnswerData = notNullChildAnswerData as ArrayList<*>
                if (childAnswerData.size > 0 && childAnswerData[0] != null) {
                    val getrow: Any = childAnswerData[0]
                    val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                    val liabilityName = t["liabilityName"].toString()
                    val monthlyPayment = t["monthlyPayment"].toString()
                    val liabilityTypeId = t["liabilityTypeId"].toString()
                    val name = t["name"].toString()
                    val remainingMonth = t["remainingMonth"].toString()

                    Timber.e("liabilityName = " + liabilityName + "  " + t["name"] + "  " + t["monthlyPayment"])
                    contentCell.detail_title.text = liabilityName
                    contentCell.detail_text.text = monthlyPayment

                    contentCell.govt_detail_box.visibility = View.VISIBLE

                    childGlobalList.add(ChildAnswerData(
                            liabilityName,
                            liabilityTypeId,
                            monthlyPayment,
                            name,
                            remainingMonth
                        )
                    )

                }

                if (childAnswerData.size > 1 && childAnswerData[1] != null) {
                    val getrow: Any = childAnswerData[1]
                    val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                    val liabilityName = t["liabilityName"].toString()
                    val monthlyPayment = t["monthlyPayment"].toString()
                    val liabilityTypeId = t["liabilityTypeId"].toString()
                    val name = t["name"].toString()
                    val remainingMonth = t["remainingMonth"].toString()

                    childGlobalList.add(
                        ChildAnswerData(
                            liabilityName,
                            liabilityTypeId,
                            monthlyPayment,
                            name,
                            remainingMonth
                        )
                    )

                    Timber.e("liabilityName = " + liabilityName + "  " + t["name"] + "  " + t["monthlyPayment"])
                    contentCell.detail_title2.text = liabilityName
                    contentCell.detail_text2.text = monthlyPayment


                    contentCell.govt_detail_box2.visibility = View.VISIBLE


                }

                if (childAnswerData.size > 2 && childAnswerData[2] != null) {
                    val getrow: Any = childAnswerData[2]
                    val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                    val liabilityName = t["liabilityName"].toString()
                    val monthlyPayment = t["monthlyPayment"].toString()
                    val liabilityTypeId = t["liabilityTypeId"].toString()
                    val name = t["name"].toString()
                    val remainingMonth = t["remainingMonth"].toString()
                    childGlobalList.add(
                        ChildAnswerData(
                            liabilityName,
                            liabilityTypeId,
                            monthlyPayment,
                            name,
                            remainingMonth
                        )
                    )

                    Timber.e("liabilityName = " + liabilityName + "  " + t["name"] + "  " + t["monthlyPayment"])

                    contentCell.detail_title3.text = liabilityName
                    contentCell.detail_text3.text = monthlyPayment
                    contentCell.govt_detail_box3.visibility = View.VISIBLE

                }

                if (questionData.answer.equals("no", true)) {
                    contentCell.ans_no.isChecked = true
                    for (item in childInnerDetailQuestions)
                        item.visibility = View.INVISIBLE
                } else {
                    contentCell.ans_yes.isChecked = true
                    for (item in childInnerDetailQuestions)
                        item.visibility = View.VISIBLE
                }
            }
            contentCell.ans_no.setOnClickListener {
                for (item in childInnerDetailQuestions)
                    item.visibility = View.INVISIBLE
            }
            contentCell.ans_yes.setOnClickListener {
                val count = childGlobalList.size-1
                for (i in 0..count){
                    val item = childInnerDetailQuestions[i]
                    item.visibility = View.VISIBLE
                }
                clickedContentCell = contentCell
                navigateToInnerScreen(headerTitle, questionId)
            }
        }
        else
        {
            if(questionData.answer.equals("no",true)) {
                contentCell.ans_no.isChecked = true
                contentCell.govt_detail_box.visibility = View.INVISIBLE
                contentCell.govt_detail_box2?.visibility = View.INVISIBLE
                contentCell.govt_detail_box3?.visibility = View.INVISIBLE
            }
            else if(questionData.answer.equals("yes",true)) {
                contentCell.ans_yes.isChecked = true
                if(questionData.answerDetail!=null && questionData.answer.equals("Yes", true) &&  questionData.answerDetail!!.isNotBlank() && questionData.answerDetail!!.isNotEmpty())
                    contentCell.govt_detail_box.visibility = View.VISIBLE
            }

            if(questionData.answer.equals("Yes", true) && questionData.answerDetail!=null && questionData.answerDetail!!.isNotBlank() && questionData.answerDetail!!.isNotEmpty()) {
                contentCell.govt_detail_box.visibility = View.VISIBLE
                contentCell.govt_detail_box2?.visibility = View.VISIBLE
                contentCell.govt_detail_box3?.visibility = View.VISIBLE
            }

            contentCell.ans_no.setOnClickListener {
                contentCell.govt_detail_box.visibility = View.INVISIBLE
                contentCell.govt_detail_box2?.visibility = View.INVISIBLE
                contentCell.govt_detail_box3?.visibility = View.INVISIBLE
                variableQuestionData.answer = "No"
                updateGovernmentData(variableQuestionData)
            }
            contentCell.ans_yes.setOnClickListener {
                if(contentCell.detail_text.text.toString().isNotBlank() && contentCell.detail_text.text.toString().isNotEmpty())
                    contentCell.govt_detail_box.visibility = View.VISIBLE

                contentCell.govt_detail_box2?.let{ govt_detail_box2->
                    if(contentCell.detail_text2.text.toString().isNotBlank() && contentCell.detail_text2.text.toString().isNotEmpty())
                        govt_detail_box2.visibility = View.VISIBLE

                    govt_detail_box2.setOnClickListener {
                        variableQuestionData.answer = "Yes"
                        updateGovernmentData(variableQuestionData)
                        clickedContentCell = contentCell
                        navigateToInnerScreen(headerTitle, questionId )
                    }
                }

                contentCell.govt_detail_box3?.let{ govt_detail_box3->
                    if(contentCell.detail_text3.text.toString().isNotBlank() && contentCell.detail_text3.text.toString().isNotEmpty())
                        govt_detail_box3.visibility = View.VISIBLE

                    govt_detail_box3.setOnClickListener {
                        variableQuestionData.answer = "Yes"
                        updateGovernmentData(variableQuestionData)
                        clickedContentCell = contentCell
                        navigateToInnerScreen(headerTitle, questionId )
                    }
                }

                variableQuestionData.answer = "Yes"
                updateGovernmentData(variableQuestionData)
                clickedContentCell = contentCell
                navigateToInnerScreen(headerTitle, questionId )
            }

            contentCell.govt_detail_box.setOnClickListener {
                variableQuestionData.answer = "Yes"
                updateGovernmentData(variableQuestionData)
                clickedContentCell = contentCell
                navigateToInnerScreen(headerTitle, questionId )
            }
        }

        return contentCell
    }

    private fun updateGovernmentData(testData:QuestionData){
        for (item in governmentParams.Questions) {
            if(item.id == testData.id){
                item.answer = testData.answer
            }
        }
    }

    private fun navigateToInnerScreen(stringForSpecificFragment:String, questionId: Int){
        val bundle = Bundle()
        bundle.putInt(AppConstant.questionId, questionId)
        bundle.putParcelable(AppConstant.addUpdateQuestionsParams , governmentParams)

        when(stringForSpecificFragment) {
               "Undisclosed Borrowered Funds" ->{

                   findNavController().navigate(R.id.action_undisclosed_borrowerfund, bundle )
               }
               "Family or Business affiliation" ->{  findNavController().navigate(R.id.action_family_affiliation , bundle ) }
               "Ownership Interest in Property" ->{

                   bundle.putStringArrayList(AppConstant.ownerShipGlobalData, ownerShipInnerScreenParams)
                   findNavController().navigate(R.id.action_ownership_interest , bundle)
               }
               "Own Property Type" ->{}
               "Debt Co-Signer or Guarantor" ->{  findNavController().navigate(R.id.action_debt_co , bundle )}
               "Outstanding Judgements" ->{  findNavController().navigate(R.id.action_outstanding , bundle)}
               "Federal Debt Deliquency" ->{ findNavController().navigate(R.id.action_federal_debt , bundle)}
               "Party to Lawsuit" ->{
                   bundle.putStringArrayList(AppConstant.ownerShipGlobalData, ownerShipInnerScreenParams)
                   findNavController().navigate(R.id.action_party_to , bundle)
               }
               "Bankruptcy " ->{
                   bundle.putParcelable(AppConstant.bankruptcyGlobalData, bankruptcyGlobalData)
                   findNavController().navigate(R.id.action_bankruptcy , bundle)
               }
               "Child Support, Alimony, etc." ->{
                   bundle.putParcelableArrayList(AppConstant.childGlobalList, childGlobalList)
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

    private val openTabMenuScreen = View.OnClickListener { p0 ->
        demoGraphicScreenDisplaying = false
        if(p0 is AppCompatTextView){
            for(item in horizontalTabArrayList){
                item.isActivated = false
                item.setTextColor(resources.getColor(R.color.doc_filter_txt_color, requireActivity().theme))
            }
            p0.isActivated = true
            p0.setTextColor(resources.getColor(R.color.colaba_primary_color, requireActivity().theme))
            for ((key, value) in innerLayoutHashMap) {
                if(key == p0) {
                    if(key.text == AppConstant.demographicInformation)
                        demoGraphicScreenDisplaying = true
                    value.visibility = View.VISIBLE
                }
                else
                    value.visibility = View.INVISIBLE
            }
        }
    }

    private var demoGraphicScreenDisplaying:Boolean = false

    private val ethnicityChildList:ArrayList<EthnicityDetailDemoGraphic> = arrayListOf()

    private var asianChildList:ArrayList<DemoGraphicRaceDetail> = arrayListOf()

    private val nativeHawaiianChildList:ArrayList<DemoGraphicRaceDetail> = arrayListOf()

    private lateinit var variableDemoGraphicData:DemoGraphicData
    private lateinit var variableRaceList:ArrayList<DemoGraphicRace>
    private lateinit var variableEthnicityList: ArrayList<EthnicityDemoGraphic>
    private var variableGender:Int? = null


    private fun showEthnicityInnerBox(){
        if(otherEthnicity.isNotEmpty() && otherEthnicity.isNotBlank()) {
            otherEthnicity = "Other Hispanic or Latino: $otherEthnicity"
            dGlayout.other_ethnicity.text = otherEthnicity
            dGlayout.other_ethnicity.visibility = View.VISIBLE
            dGlayout.hispanic_or_latino_child_box_layout.visibility = View.VISIBLE
        }
        else
            dGlayout.other_ethnicity.visibility = View.GONE

        if(ethnicityChildNames.isNotEmpty() && ethnicityChildNames.isNotBlank()) {
            //ethnicityChildNames = ethnicityChildNames.substring(0, ethnicityChildNames.length-2)
            dGlayout.ethnicity_children.text = ethnicityChildNames
            dGlayout.ethnicity_children.visibility = View.VISIBLE
            dGlayout.hispanic_or_latino_child_box_layout.visibility = View.VISIBLE
        }
        else
            dGlayout.ethnicity_children.visibility = View.GONE
    }

    private fun showNativeHawaiiInnerBox(){
        if(nativeHawaiiOtherRace.isNotEmpty() && nativeHawaiiOtherRace.isNotBlank()) {
            nativeHawaiiOtherRace = "Other Pacific Islander: $nativeHawaiiOtherRace"

            dGlayout.other_typed_native_hawaiian.text = nativeHawaiiOtherRace
            dGlayout.other_typed_native_hawaiian.visibility = View.VISIBLE
            dGlayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
        }
        else
            dGlayout.other_typed_native_hawaiian.visibility = View.GONE

        if(nativeHawaiiChildNames.isNotEmpty() && nativeHawaiiChildNames.isNotBlank()) {
            dGlayout.child_native_hawaiian.text = nativeHawaiiChildNames
            dGlayout.child_native_hawaiian.visibility = View.VISIBLE
            dGlayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
        }
        else
            dGlayout.child_native_hawaiian.visibility = View.GONE
    }

    private fun showAsianInnerBox(){
        if(otherAsianRace.isNotEmpty() && otherAsianRace.isNotBlank()) {
            otherAsianRace = "Other Asian: $otherAsianRace"
            dGlayout.other_asian_race.text = otherAsianRace
            dGlayout.other_asian_race.visibility = View.VISIBLE
            dGlayout.asian_child_box_layout.visibility = View.VISIBLE
        }
        else
            dGlayout.other_asian_race.visibility = View.GONE

        if(asianChildNames.isNotEmpty() && asianChildNames.isNotBlank()) {
            //asianChildNames = asianChildNames.substring(0, asianChildNames.length-2)
            dGlayout.asian_child_names.text = asianChildNames
            dGlayout.asian_child_names.visibility = View.VISIBLE
            dGlayout.asian_child_box_layout.visibility = View.VISIBLE
        }
        else
            dGlayout.asian_child_names.visibility = View.GONE
    }

    private fun addDemoGraphicEvents(){

        dGlayout.do_not_wish_check_box.setOnClickListener{
            dGlayout.white_check_box.isChecked = false
            dGlayout.black_or_african_check_box.isChecked = false
            dGlayout.american_or_indian_check_box.isChecked = false
            dGlayout.native_hawaian_or_other_check_box.isChecked = false
            dGlayout.asian_check_box.isChecked = false

            if(dGlayout.do_not_wish_check_box.isChecked) {
                dGlayout.asian_child_box_layout.visibility = View.GONE
                dGlayout.native_hawaian_child_box_layout.visibility = View.GONE
            }

        }

        dGlayout.white_check_box.setOnClickListener{ dGlayout.do_not_wish_check_box.isChecked = false }
        dGlayout.native_hawaian_or_other_check_box.setOnClickListener{ dGlayout.do_not_wish_check_box.isChecked = false }
        dGlayout.black_or_african_check_box.setOnClickListener{ dGlayout.do_not_wish_check_box.isChecked = false }
        dGlayout.asian_check_box.setOnClickListener{ dGlayout.do_not_wish_check_box.isChecked = false }
        dGlayout.american_or_indian_check_box.setOnClickListener{ dGlayout.do_not_wish_check_box.isChecked = false }

    }

    private fun observeDemoGraphicData( contentCell:ConstraintLayout){
        borrowerAppViewModel.demoGraphicInfoList.observe(viewLifecycleOwner,{ demoGraphicInfoList->

            dGlayout.asian_child_box_layout.visibility = View.GONE
            dGlayout.native_hawaian_child_box_layout.visibility = View.GONE
            dGlayout.hispanic_or_latino_child_box_layout.visibility = View.GONE

            if(demoGraphicInfoList.size>0){
                var selectedDemoGraphicInfoList: DemoGraphicResponseModel? =null
                for(item in demoGraphicInfoList){
                    if(item.passedBorrowerId == tabBorrowerId ) {
                        selectedDemoGraphicInfoList = item
                        break
                    }
                }




                selectedDemoGraphicInfoList?.let {
                    it.demoGraphicData?.let { demoGraphicData ->

                        variableDemoGraphicData = demoGraphicData
                        ethnicityChildNames = ""
                        otherEthnicity = ""
                        nativeHawaiiChildNames = ""
                        nativeHawaiiOtherRace = ""
                        asianChildNames = ""
                        otherAsianRace = ""

                        demoGraphicData.genderId?.let { genderId ->
                            variableGender = genderId
                            if (genderId == 1)
                                dGlayout.demo_male.isChecked = true
                            else
                            if (genderId == 2)
                                dGlayout.demo_female.isChecked = true
                            else
                            if (genderId == 3)
                                dGlayout.demo_do_not_wish_to_provide.isChecked = true
                        }

                        demoGraphicData.ethnicity?.let { ethnicityList ->
                            variableEthnicityList = ethnicityList
                            if (ethnicityList.isNotEmpty()) {
                                val selectedEthnicity = ethnicityList[0]
                                if (selectedEthnicity.ethnicityId == 1) {
                                    dGlayout.hispanic_or_latino.isChecked = true
                                    selectedEthnicity.ethnicityDetails?.let { theList ->
                                        for (item in theList) {
                                            ethnicityChildList.add(item)
                                            item.isOther?.let { isOther ->
                                                if (isOther)
                                                    item.otherEthnicity?.let {
                                                        otherEthnicity = it
                                                    }
                                                else
                                                    ethnicityChildNames =
                                                        ethnicityChildNames + item.name + ", "
                                            }
                                        }
                                    }

                                    showEthnicityInnerBox()
                                }
                                else
                                if (selectedEthnicity.ethnicityId == 2)
                                    dGlayout.not_hispanic.isChecked = true
                                else
                                if (selectedEthnicity.ethnicityId == 3)
                                    dGlayout.not_telling_ethnicity.isChecked = true
                            }
                        }

                        demoGraphicData.race?.let { raceList ->
                            variableRaceList = raceList
                            for (race in raceList) {
                                if (race.raceId == 1) {
                                    dGlayout.american_or_indian_check_box.isChecked = true
                                }
                                if (race.raceId == 2) {
                                    dGlayout.asian_check_box.isChecked = true
                                    race.raceDetails?.let { asianChildList ->

                                        for (item in asianChildList) {
                                            this.asianChildList.add(item)
                                            item.isOther?.let { isOther ->
                                                if (isOther)
                                                    item.otherRace?.let {
                                                        otherAsianRace = it
                                                    }
                                                else
                                                    asianChildNames =
                                                        asianChildNames + item.name + ", "
                                            }
                                        }
                                    }
                                    showAsianInnerBox()
                                }
                                if (race.raceId == 3) {
                                    dGlayout.black_or_african_check_box.isChecked =
                                        true
                                }
                                if (race.raceId == 4) {
                                    dGlayout.native_hawaian_or_other_check_box.isChecked =
                                        true

                                    race.raceDetails?.let { nativeHawaianChildList ->
                                        for (item in nativeHawaianChildList) {
                                            nativeHawaiianChildList.add(item)
                                            item.isOther?.let { isOther ->
                                                if (isOther)
                                                    item.otherRace?.let {
                                                        nativeHawaiiOtherRace = it
                                                    }
                                                else
                                                    nativeHawaiiChildNames =
                                                        nativeHawaiiChildNames + item.name + ", "
                                            }
                                        }
                                    }


                                    showNativeHawaiiInnerBox()

                                }
                                if (race.raceId == 5) {
                                    dGlayout.white_check_box.isChecked = true
                                }
                                if (race.raceId == 6) {
                                    dGlayout.do_not_wish_check_box.performClick()
                                }
                            }


                        }


                        // Now add static events....
                        setUpDemoGraphicScreen()
                        addDemoGraphicEvents()
                    }
                }
            }
        })
    }

    private fun setUpDemoGraphicScreen() {

        dGlayout.american_or_indian_check_box.setOnCheckedChangeListener{ buttonView, isChecked ->
            updateDemoGraphicRace(1, isChecked)
        }

        dGlayout.asian_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                if((dGlayout.asian_child_names.text.isNotEmpty() &&
                        dGlayout.asian_check_box.text.isNotBlank()) ||
                    (dGlayout.other_asian_race.text.isNotEmpty() &&
                    dGlayout.other_asian_race.text.isNotBlank())
                        )
                dGlayout.asian_child_box_layout.visibility = View.VISIBLE

                val copyAsianChildList =  ArrayList(asianChildList.map { it.copy() })
                val bundle = bundleOf(AppConstant.asianChildList to copyAsianChildList)
                findNavController().navigate(R.id.action_asian , bundle)
                Timber.e("not accessible...")
            }
            else
                dGlayout.asian_child_box_layout.visibility = View.GONE

            updateDemoGraphicRace(2, isChecked)

        }

        dGlayout.black_or_african_check_box.setOnCheckedChangeListener{ buttonView, isChecked ->
            updateDemoGraphicRace(3, isChecked)
        }

        dGlayout.native_hawaian_or_other_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                if((dGlayout.child_native_hawaiian.text.isNotEmpty() &&
                            dGlayout.child_native_hawaiian.text.isNotBlank()) ||
                    (dGlayout.other_typed_native_hawaiian.text.isNotEmpty() &&
                            dGlayout.other_typed_native_hawaiian.text.isNotBlank())
                )
                    dGlayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
                val bundle = bundleOf(AppConstant.nativeHawaianChildList to nativeHawaiianChildList)
                findNavController().navigate(R.id.action_native_hawai, bundle)
            }
            else
                dGlayout.native_hawaian_child_box_layout.visibility = View.GONE
            updateDemoGraphicRace(4, isChecked)
        }

        dGlayout.white_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            updateDemoGraphicRace(5, isChecked)
        }


        dGlayout.do_not_wish_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            variableRaceList.clear()
            updateDemoGraphicRace(6, isChecked)
        }

        dGlayout.native_hawaian_child_box_layout.setOnClickListener{
            if (dGlayout.native_hawaian_or_other_check_box.isChecked) {
                dGlayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
                val bundle = bundleOf(AppConstant.nativeHawaianChildList to nativeHawaiianChildList)
                findNavController().navigate(R.id.action_native_hawai, bundle)
            }

        }

        dGlayout.asian_child_box_layout.setOnClickListener{
            if ( dGlayout.asian_check_box.isChecked) {
                dGlayout.asian_child_box_layout.visibility = View.VISIBLE
                val copyAsianChildList =  ArrayList(asianChildList.map { it.copy() })
                val bundle = bundleOf(AppConstant.asianChildList to copyAsianChildList)
                findNavController().navigate(R.id.action_asian , bundle)
                Timber.e("not accessible...")
            }
        }

        dGlayout.hispanic_or_latino.setOnClickListener {
            val bundle = bundleOf(AppConstant.ethnicityChildList to ethnicityChildList)
            findNavController().navigate(R.id.action_hispanic , bundle)
            dGlayout.not_hispanic.isChecked = false
            dGlayout.not_telling_ethnicity.isChecked = false
            showEthnicityInnerBox()
            updateDemoGraphicEthnicity(1)
        }

        dGlayout.hispanic_or_latino_child_box_layout.setOnClickListener {
            val bundle = bundleOf(AppConstant.ethnicityChildList to ethnicityChildList)
            findNavController().navigate(R.id.action_hispanic, bundle)
            dGlayout.not_hispanic.isChecked = false
            dGlayout.not_telling_ethnicity.isChecked = false
        }

        dGlayout.not_hispanic.setOnClickListener{
            dGlayout.hispanic_or_latino.isChecked = false
            dGlayout.not_telling_ethnicity.isChecked = false
            dGlayout.hispanic_or_latino_child_box_layout.visibility = View.GONE
            updateDemoGraphicEthnicity(2)
        }

        dGlayout.not_telling_ethnicity.setOnClickListener{
            dGlayout.hispanic_or_latino.isChecked = false
            dGlayout.not_hispanic.isChecked = false
            dGlayout.hispanic_or_latino_child_box_layout.visibility = View.GONE
            updateDemoGraphicEthnicity(3)
        }


            dGlayout.demo_male.setOnClickListener { updateDemoGraphicGender(1)}
            dGlayout.demo_female.setOnClickListener { updateDemoGraphicGender(2)}
            dGlayout.demo_do_not_wish_to_provide.setOnClickListener { updateDemoGraphicGender(3)}

    }

    private fun updateDemoGraphicRace(raceId:Int, removeFromList:Boolean){
       if(!removeFromList) {
           for (race in variableRaceList) {
               if (race.raceId == raceId) {
                   variableRaceList.remove(race)
                   break
               }
           }
       }
       else
           variableRaceList.add(DemoGraphicRace(arrayListOf(), raceId))
   }

    private fun updateDemoGraphicEthnicity(ethnicityId:Int){
        variableEthnicityList.clear()
        variableEthnicityList.add(EthnicityDemoGraphic(ethnicityId = ethnicityId , ethnicityDetails = arrayListOf()) )
    }

    private fun updateDemoGraphicGender(genderId:Int){
        variableGender = genderId
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        val navController = findNavController()
        // We use a String here, but any type that can be put in a Bundle is supported
        navController.currentBackStackEntry?.savedStateHandle?.getLiveData<ArrayList<DemoGraphicRaceDetail>>(AppConstant.selectedAsianChildList)?.observe(
            viewLifecycleOwner) { resultAsianChildList ->
            // Do something with the result.
            asianChildList = resultAsianChildList
            if(resultAsianChildList.size>0) {
                for(item in variableRaceList){
                    if(item.raceId == 2) {
                        item.raceDetails = resultAsianChildList
                        break
                    }
                }


                asianChildNames = ""
                otherAsianRace = ""

                for(item in resultAsianChildList) {
                    item.isOther?.let { isOther ->
                        if (isOther)
                            item.otherRace?.let {
                                otherAsianRace = it
                            }
                        else
                            asianChildNames =
                                asianChildNames + item.name + ", "
                    }
                }
                showAsianInnerBox()
                //updateDemoGraphicService()
            }
            else
            {
                dGlayout.asian_child_names.text =""
                dGlayout.other_asian_race.text=""
                dGlayout.asian_child_box_layout.visibility = View.GONE
                dGlayout.asian_check_box.isChecked = false
            }

        }

        navController.currentBackStackEntry?.savedStateHandle?.getLiveData<ArrayList<DemoGraphicRaceDetail>>(AppConstant.selectedNativeHawaianChildList)?.observe(
            viewLifecycleOwner) { selectedNativeHawaianChildList ->

            if(selectedNativeHawaianChildList.size>0) {
                nativeHawaiiChildNames = ""
                nativeHawaiiOtherRace = ""
                for (item in selectedNativeHawaianChildList) {
                    item.isOther?.let { isOther ->
                        if (isOther)
                            item.otherRace?.let {
                                nativeHawaiiOtherRace = it
                            }
                        else
                            nativeHawaiiChildNames = nativeHawaiiChildNames + item.name + ", "
                    }
                }
                showNativeHawaiiInnerBox()

                for(item in variableRaceList){
                    if(item.raceId == 4) {
                        item.raceDetails = selectedNativeHawaianChildList
                        break
                    }
                }
                //updateDemoGraphicService()
            }
            else
            {
                dGlayout.child_native_hawaiian.text =""
                dGlayout.other_typed_native_hawaiian.text=""
                dGlayout.native_hawaian_child_box_layout.visibility = View.GONE
                dGlayout.native_hawaian_or_other_check_box.isChecked = false
            }

        }

        navController.currentBackStackEntry?.savedStateHandle?.getLiveData<ArrayList<EthnicityDetailDemoGraphic>>(AppConstant.selectedEthnicityChildList)?.observe(
            viewLifecycleOwner) { theList ->
            // Do something with the result.
            if(theList.size>0) {
                ethnicityChildNames = ""
                otherEthnicity = ""
                for (item in theList) {
                    item.isOther?.let { isOther ->
                        if (isOther)
                            item.otherEthnicity?.let {
                                otherEthnicity = it
                            }
                        else
                            ethnicityChildNames = ethnicityChildNames + item.name + ", "
                    }
                }
                showEthnicityInnerBox()
                variableEthnicityList[0].ethnicityDetails  = theList
                //updateDemoGraphicService()
            }
            else
            {
                dGlayout.ethnicity_children.text =""
                dGlayout.other_ethnicity.text=""
                dGlayout.hispanic_or_latino_child_box_layout.visibility = View.GONE
                dGlayout.hispanic_or_latino.isChecked = false
            }
        }
    }

    private fun updateDemoGraphicService(){
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                variableDemoGraphicData.genderId = variableGender
                variableDemoGraphicData.race = variableRaceList
                variableDemoGraphicData.ethnicity = variableEthnicityList
                borrowerAppViewModel.addOrUpdateDemoGraphic(authToken, variableDemoGraphicData)
                //findNavController().popBackStack()
            }
        }
    }


    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun updateReceivedFromInnerScreen(updateEvent: GovtScreenUpdateEvent) {
        clickedContentCell.govt_detail_box.detail_title.text = updateEvent.detailTitle
        clickedContentCell.govt_detail_box.detail_text.text = updateEvent.detailDescription
        if(updateEvent.detailDescription.isNotEmpty() && updateEvent.detailDescription.isNotBlank())
            clickedContentCell.govt_detail_box.visibility = View.VISIBLE
        else
            clickedContentCell.govt_detail_box.visibility = View.INVISIBLE
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun updateUndisclosedBorrowerFunds(borrowerFundUpdateEvent: UndisclosedBorrowerFundUpdateEvent) {
        clickedContentCell.govt_detail_box.detail_title.text =  UndisclosedBorrowerFundFragment.UndisclosedBorrowerQuestionConstant
        clickedContentCell.govt_detail_box.detail_title.setTypeface(null, Typeface.NORMAL)
        clickedContentCell.govt_detail_box.detail_text.text = "$".plus(Common.addNumberFormat(borrowerFundUpdateEvent.detailDescription.toDouble()))
        clickedContentCell.govt_detail_box.detail_text.setTypeface(null, Typeface.BOLD)
        clickedContentCell.govt_detail_box.visibility = View.VISIBLE

        governmentParams.Questions.let { questions->
            for (question in questions) {
                question.parentQuestionId?.let { parentQuestionId ->
                    if (parentQuestionId == undisclosedLayout.id) {
                        question.answer = borrowerFundUpdateEvent.detailDescription
                        question.answerDetail = borrowerFundUpdateEvent.detailTitle
                    }
                }
            }
        }
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun updateOwnershipInterest(updateEvent: OwnershipInterestUpdateEvent) {
        var secondMatched = true
        ownerShipInnerScreenParams.clear()

        governmentParams.Questions.let { questions ->
            for (item in questions) {
                item.parentQuestionId?.let { parentQuestionId ->
                    if (parentQuestionId == ownerShipConstraintLayout.id) {
                        if (secondMatched) {
                            clickedContentCell.govt_detail_box.detail_title.text =  OwnershipInterestInPropertyFragment.ownershipQuestionOne
                            clickedContentCell.govt_detail_box.detail_title.setTypeface(null, Typeface.NORMAL)
                            clickedContentCell.govt_detail_box.detail_text.text = updateEvent.answer1
                            clickedContentCell.govt_detail_box.detail_text.setTypeface(null, Typeface.BOLD)
                            clickedContentCell.govt_detail_box.visibility = View.VISIBLE
                            secondMatched = false
                            ownerShipInnerScreenParams.add(updateEvent.answer1)
                            //item.answerData = TestAnswerData(updateEvent.index1.toString(), updateEvent.answer1)
                        } else {
                            clickedContentCell.detail_text2?.text =  OwnershipInterestInPropertyFragment.ownershipQuestionTwo
                            clickedContentCell.detail_text2?.setTypeface(null, Typeface.NORMAL)
                            clickedContentCell.detail_text2?.text = updateEvent.answer2
                            clickedContentCell.detail_text2?.setTypeface(null, Typeface.BOLD)
                            clickedContentCell.govt_detail_box2?.visibility = View.VISIBLE
                            ownerShipInnerScreenParams.add(updateEvent.answer2)
                            //item.answerData = TestAnswerData(updateEvent.index2.toString(), updateEvent.answer2)
                        }
                    }
                }
            }
        }


        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                borrowerAppViewModel.addOrUpdateGovernmentQuestions(authToken, governmentParams)
            }
        }

    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun updateBankruptcy(updateEvent: BankruptcyUpdateEvent) {
        val displayValue = updateEvent.detailDescription
        displayValue.trim()
        clickedContentCell.govt_detail_box.detail_title.text = updateEvent.detailTitle
        clickedContentCell.govt_detail_box.detail_title.setTypeface(null, Typeface.NORMAL)
        clickedContentCell.govt_detail_box.detail_text.text = displayValue
        clickedContentCell.govt_detail_box.detail_text.setTypeface(null, Typeface.BOLD)
        clickedContentCell.govt_detail_box.visibility = View.VISIBLE

        governmentParams.Questions.let { questions->
            for (question in questions) {
                question.parentQuestionId?.let { parentQuestionId ->
                    if (parentQuestionId == bankruptcyConstraintLayout.id) {

                    }
                }
            }
        }
    }


    @Subscribe(threadMode = ThreadMode.MAIN)
    fun updateChildSupport(updateEvent: ChildSupportUpdateEvent) {
        val list = updateEvent.childAnswerList
        childGlobalList.clear()
        childGlobalList = list
        if(list.size>0){
            clickedContentCell.govt_detail_box.detail_title.text = list[0].liabilityName
            clickedContentCell.govt_detail_box.detail_title.setTypeface(null, Typeface.BOLD )
            clickedContentCell.govt_detail_box.detail_text.text = "$".plus(Common.addNumberFormat(list[0].monthlyPayment.toDouble()))
            clickedContentCell.govt_detail_box.detail_text.setTypeface(null, Typeface.NORMAL)
            clickedContentCell.govt_detail_box.visibility = View.VISIBLE
        }
        if(list.size>1){
            clickedContentCell.govt_detail_box2.detail_title2.text = list[1].liabilityName
            clickedContentCell.govt_detail_box2.detail_title2.setTypeface(null, Typeface.BOLD )
            clickedContentCell.govt_detail_box2.detail_text2.text = "$".plus(Common.addNumberFormat(list[1].monthlyPayment.toDouble()))
            clickedContentCell.govt_detail_box2.detail_text2.setTypeface(null, Typeface.NORMAL)
            clickedContentCell.govt_detail_box2.visibility = View.VISIBLE
        }

        if(list.size>2){
            clickedContentCell.govt_detail_box3.detail_title3.text = list[2].liabilityName
            clickedContentCell.govt_detail_box3.detail_title3.setTypeface(null, Typeface.BOLD )
            clickedContentCell.govt_detail_box3.detail_text3.text = "$".plus(Common.addNumberFormat(list[2].monthlyPayment.toDouble()))
            clickedContentCell.govt_detail_box3.detail_text3.setTypeface(null, Typeface.NORMAL)
            clickedContentCell.govt_detail_box3.visibility = View.VISIBLE
        }



    }

    private fun <T> stringToArray2(s: String?, clazz: Class<T>?): T {
        val newList = Gson().fromJson(s, clazz)!!
        return newList //or return Arrays.asList(new Gson().fromJson(s, clazz)); for a one-liner
    }

    private fun convertPixelsToDp(px: Float, context: Context): Int {
        return (px / (context.resources.displayMetrics.densityDpi.toFloat() / DisplayMetrics.DENSITY_DEFAULT)).toInt()
    }

    private fun convertDpToPixel(dp: Float, context: Context): Int {
        return (dp * (context.resources.displayMetrics.densityDpi.toFloat() / DisplayMetrics.DENSITY_DEFAULT)).toInt()
    }



}






