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
import kotlinx.android.synthetic.main.ownership_interest_layout.view.detail_text2
import kotlinx.android.synthetic.main.ownership_interest_layout.view.detail_title2
import kotlinx.android.synthetic.main.ownership_interest_layout.view.govt_detail_box2
import com.google.gson.Gson
import kotlin.collections.ArrayList
import kotlin.collections.HashMap
import com.google.gson.internal.LinkedTreeMap
import dagger.hilt.android.AndroidEntryPoint

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
import javax.inject.Inject
import kotlin.properties.Delegates

@AndroidEntryPoint
class BorrowerOneQuestions : GovtQuestionBaseFragment() {

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
    var ownerShipGlobalData:ArrayList<String> = arrayListOf()
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
                        borrowerAppViewModel.addOrUpdateGovernmentQuestions(authToken, updateGovernmentQuestionByBorrowerId)
                    }
                }
            }
        }


        setupLayout()
        super.addListeners(binding.root)
        return binding.root
    }


    private fun setupLayout(){

        /*
            borrowerAppViewModel.addUpdateDemoGraphicResponse.observe(
                viewLifecycleOwner,
                { addUpdateDemoGraphicResponse ->
                    Timber.e("demo-graphic updated....")
                })

         */



        setUpDynamicTabs( )
    }



    private var updateGovernmentQuestionByBorrowerId = UpdateGovernmentQuestions()

    private fun setUpDynamicTabs(){
        val governmentQuestionActivity = (activity as? GovtQuestionActivity)

        borrowerAppViewModel.governmentQuestionsModelClassList.observe(viewLifecycleOwner,  { governmentQuestionsModelClassList->

            if(governmentQuestionsModelClassList.size>0) {
                var selectedGovernmentQuestionModel: GovernmentQuestionsModelClass? = null
                for (item in governmentQuestionsModelClassList) {
                    if (item.passedBorrowerId == tabBorrowerId) {
                        selectedGovernmentQuestionModel = item

                        governmentQuestionActivity?.let { governmentQuestionActivity ->
                            item.questionData?.let { questionDataList ->
                                item.passedBorrowerId?.let { passedBorrowerId ->
                                    updateGovernmentQuestionByBorrowerId = UpdateGovernmentQuestions(passedBorrowerId, governmentQuestionActivity.loanApplicationId.toString(), questionDataList)
                                    //udateGovernmentQuestionsList.add(test)
                                }
                            }
                        }

                        break
                    }
                }


                /*
                val governmentQuestionActivity = (activity as? GovtQuestionActivity)
                governmentQuestionActivity?.let { governmentQuestionActivity ->
                    for (item in governmentQuestionsModelClassList) {
                        item.questionData?.let { questionDataList ->
                            item.passedBorrowerId?.let { passedBorrowerId ->
                                val test = UpdateGovernmentQuestions(passedBorrowerId, governmentQuestionActivity.loanApplicationId.toString() , questionDataList)
                                 udateGovernmentQuestionsList.add(test)
                            }
                        }
                    }
                }
                 */


                selectedGovernmentQuestionModel?.let{ selectedGovernmentQuestionModel->

                    val govtQuestionActivity = (activity as GovtQuestionActivity)
                    govtQuestionActivity.binding.govtDataLoader.visibility = View.INVISIBLE
                    var zeroIndexAppCompat:AppCompatTextView?= null
                    childGlobalList= arrayListOf()
                    bankruptcyGlobalData = BankruptcyAnswerData()
                    ownerShipGlobalData = arrayListOf()

                    selectedGovernmentQuestionModel.questionData?.let { questionData ->
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

                        if (zeroIndexAppCompat != null) {
                            val appCompactTextView = createAppCompactTextView(AppConstant.demographicInformation, 0)
                            binding.horizontalTabs.addView(appCompactTextView)
                            val contentView = createContentLayoutForTab(
                                QuestionData(id = 5000, parentQuestionId = null, headerText = AppConstant.demographicInformation,
                                    questionSectionId = 1, ownTypeId = 1, firstName = lastQData.firstName, lastName = lastQData.lastName,
                                    question = "", answer = null, answerDetail = null, selectionOptionId = null, answerData = null))

                            innerLayoutHashMap.put(appCompactTextView, contentView)
                            idToContentMapping.put(5000, contentView)

                            binding.parentContainer.addView(contentView)
                            appCompactTextView.setOnClickListener(openTabMenuScreen)
                            horizontalTabArrayList.add(appCompactTextView)
                        }

                        var ownerShip = true

                        for (qData in questionData) {
                            qData.parentQuestionId?.let { parentQuestionId ->
                                if (parentQuestionId == bankruptcyConstraintLayout.id) {
                                    Timber.e("bankruptcyConstraintLayout " + qData.question)
                                    Timber.e(qData.answerDetail.toString())
                                    var extractedAnswer = ""
                                    val bankruptAnswerData = qData.answerData as ArrayList<*>
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
                                        if ( bankruptAnswerData.size > 1 && bankruptAnswerData[1] != null ) {
                                            val getrow: Any = bankruptAnswerData[1]
                                            val t: LinkedTreeMap<Any, Any> =
                                                getrow as LinkedTreeMap<Any, Any>
                                            val chapter2 = t["`2`"].toString()
                                            extractedAnswer = "$extractedAnswer, $chapter2"
                                            bankruptcyGlobalData.value2 = true
                                            Timber.e("2 = " + chapter2)

                                        }

                                        if ( bankruptAnswerData.size > 2 && bankruptAnswerData[2] != null) {
                                            val getrow: Any = bankruptAnswerData[2]
                                            val t: LinkedTreeMap<Any, Any> =
                                                getrow as LinkedTreeMap<Any, Any>
                                            val chapter3 = t["`3`"].toString()
                                            extractedAnswer = "$extractedAnswer, $chapter3"
                                            bankruptcyGlobalData.value3 = true
                                            Timber.e("3 = " + chapter3)
                                        }

                                        if (bankruptAnswerData.size > 3 && bankruptAnswerData[3] != null ) {
                                            val getrow: Any = bankruptAnswerData[3]
                                            val t: LinkedTreeMap<Any, Any> =
                                                getrow as LinkedTreeMap<Any, Any>
                                            val chapter4 = t["`4`"].toString()
                                            bankruptcyGlobalData.value4 = true
                                            extractedAnswer = "$extractedAnswer, $chapter4"
                                            Timber.e("4 = " + chapter4)
                                        }

                                        Timber.e(" extracted answer = " + extractedAnswer)
                                        bankruptcyConstraintLayout.detail_text.text = extractedAnswer
                                        bankruptcyConstraintLayout.detail_title.setTypeface(null, Typeface.NORMAL)
                                        bankruptcyConstraintLayout.detail_text.setTypeface(null, Typeface.BOLD)
                                        bankruptcyConstraintLayout.govt_detail_box.visibility = View.VISIBLE
                                    }
                                } else if (parentQuestionId == childConstraintLayout.id) {
                                    Timber.e("childConstraintLayout " + qData.question)
                                    Timber.e(qData.answerDetail.toString())
                                }
                                else
                                    if (parentQuestionId == ownerShipConstraintLayout.id && qData.answer != null && !qData.answer.equals("No", true)) {
                                        Timber.e("ownerShipConstraintLayout " + qData.question)
                                        Timber.e(qData.answerDetail.toString())

                                        if (ownerShip) {
                                            ownerShip = false
                                            ownerShipConstraintLayout.detail_title.text = qData.question
                                            ownerShipConstraintLayout.detail_text.text = qData.answer
                                            ownerShipConstraintLayout.detail_title.setTypeface(null, Typeface.NORMAL)
                                            ownerShipConstraintLayout.detail_text.setTypeface(null, Typeface.BOLD)
                                            ownerShipGlobalData.add(qData.answer!!)
                                            ownerShipConstraintLayout.govt_detail_box.visibility = View.VISIBLE
                                        } else {
                                            ownerShipConstraintLayout.detail_title2.text = qData.question
                                            ownerShipConstraintLayout.detail_text2.text = qData.answer
                                            ownerShipConstraintLayout.detail_title2.setTypeface(null, Typeface.NORMAL)
                                            ownerShipConstraintLayout.detail_text2.setTypeface(null, Typeface.BOLD)
                                            ownerShipGlobalData.add(qData.answer!!)
                                            ownerShipConstraintLayout.govt_detail_box2.visibility = View.VISIBLE
                                        }

                                    }

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
    private lateinit var bankruptcyConstraintLayout:ConstraintLayout
    private lateinit var demoGraphicConstraintLayout:ConstraintLayout

    //private lateinit var variableQuestionData: QuestionData


    private fun createContentLayoutForTab(questionData:QuestionData):ConstraintLayout{
        val variableQuestionData: QuestionData = questionData
        var childSupport = false
        var ownerShip = false
        var headerTitle = ""
        val contentCell: ConstraintLayout
        if(questionData.headerText == AppConstant.ownershipConstantValue) {
            contentCell = layoutInflater.inflate(R.layout.ownership_interest_layout, null) as ConstraintLayout
            ownerShip = true
            ownerShipConstraintLayout = contentCell
            questionData.id?.let {
                ownerShipConstraintLayout.id = it
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
            demoGraphicConstraintLayout = contentCell
            questionData.id?.let {
                demoGraphicConstraintLayout.id = it
                contentCell.id = it
            }
            contentCell.visibility = View.INVISIBLE
            demoGraphicConstraintLayout.visibility = View.INVISIBLE
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
                navigateToInnerScreen(headerTitle , questionId)
            }
            contentCell.govt_detail_box2.setOnClickListener { navigateToInnerScreen(headerTitle , questionId) }
        }

        if(childSupport){
            contentCell.govt_detail_box.setOnClickListener {  navigateToInnerScreen(headerTitle ,  questionId) }
            contentCell.govt_detail_box2.setOnClickListener {navigateToInnerScreen(headerTitle , questionId) }
            contentCell.govt_detail_box3.setOnClickListener { navigateToInnerScreen(headerTitle , questionId) }

            val childInnerDetailQuestions:ArrayList<ConstraintLayout> = arrayListOf()
            val childAnswerData = questionData.answerData as ArrayList<*>
            if(childAnswerData!=null) {
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

                    childGlobalList.add(ChildAnswerData(liabilityName,liabilityTypeId, monthlyPayment, name, remainingMonth ))
                    childInnerDetailQuestions.add(contentCell.govt_detail_box)
                }

                if (childAnswerData.size > 1 && childAnswerData[1] != null) {
                    val getrow: Any = childAnswerData[1]
                    val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                    val liabilityName = t["liabilityName"].toString()
                    val monthlyPayment = t["monthlyPayment"].toString()
                    val liabilityTypeId = t["liabilityTypeId"].toString()
                    val name = t["name"].toString()
                    val remainingMonth = t["remainingMonth"].toString()

                    childGlobalList.add( ChildAnswerData(liabilityName,liabilityTypeId, monthlyPayment, name, remainingMonth ))

                    Timber.e("liabilityName = " + liabilityName + "  " + t["name"] + "  " + t["monthlyPayment"])
                    contentCell.detail_title2.text = liabilityName
                    contentCell.detail_text2.text = monthlyPayment


                    contentCell.govt_detail_box2.visibility = View.VISIBLE
                    childInnerDetailQuestions.add(contentCell.govt_detail_box2)

                }

                if (childAnswerData.size > 2 && childAnswerData[2] != null) {
                    val getrow: Any = childAnswerData[2]
                    val t: LinkedTreeMap<Any, Any> = getrow as LinkedTreeMap<Any, Any>
                    val liabilityName = t["liabilityName"].toString()
                    val monthlyPayment = t["monthlyPayment"].toString()
                    val liabilityTypeId = t["liabilityTypeId"].toString()
                    val name = t["name"].toString()
                    val remainingMonth = t["remainingMonth"].toString()
                    childGlobalList.add(ChildAnswerData(liabilityName,liabilityTypeId, monthlyPayment, name, remainingMonth ))

                    Timber.e("liabilityName = " + liabilityName + "  " + t["name"] + "  " + t["monthlyPayment"])

                    contentCell.detail_title3.text = liabilityName
                    contentCell.detail_text3.text = monthlyPayment
                    contentCell.govt_detail_box3.visibility = View.VISIBLE
                    childInnerDetailQuestions.add(contentCell.govt_detail_box3)
                }

                if(questionData.answer.equals("no",true)) {
                    contentCell.ans_no.isChecked = true
                    for(item in childInnerDetailQuestions)
                        item.visibility = View.INVISIBLE
                }
                else {
                    contentCell.ans_yes.isChecked = true
                    for(item in childInnerDetailQuestions)
                        item.visibility = View.VISIBLE
                }
                contentCell.ans_no.setOnClickListener {
                    for(item in childInnerDetailQuestions)
                        item.visibility = View.INVISIBLE
                }
                contentCell.ans_yes.setOnClickListener {
                    for(item in childInnerDetailQuestions)
                        item.visibility = View.VISIBLE
                    navigateToInnerScreen(headerTitle , questionId)
                }


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

            contentCell.ans_no.setOnClickListener {
                contentCell.govt_detail_box.visibility = View.INVISIBLE
                contentCell.govt_detail_box2?.visibility = View.INVISIBLE
                contentCell.govt_detail_box3?.visibility = View.INVISIBLE
                variableQuestionData.answer = "No"
                updateGovernmentData(variableQuestionData)
            }
            contentCell.ans_yes.setOnClickListener {
                if(questionData.answer.equals("Yes", true) && questionData.answerDetail!=null && questionData.answerDetail!!.isNotBlank() && questionData.answerDetail!!.isNotEmpty()) {
                    contentCell.govt_detail_box.visibility = View.VISIBLE
                    contentCell.govt_detail_box2?.visibility = View.VISIBLE
                    contentCell.govt_detail_box3?.visibility = View.VISIBLE
                }
                variableQuestionData.answer = "Yes"
                updateGovernmentData(variableQuestionData)
                navigateToInnerScreen(headerTitle, questionId)
            }
        }

        return contentCell
    }

    private fun updateGovernmentData(testData:QuestionData){
        for (item in updateGovernmentQuestionByBorrowerId.Questions) {
            if(item.id == testData.id){
                item.answer = testData.answer
            }
        }
    }

    private fun navigateToInnerScreen(stringForSpecificFragment:String, questionId: Int){
        val bundle = Bundle()
        bundle.putInt(AppConstant.questionId, questionId)
        bundle.putParcelable(AppConstant.updateGovernmentQuestionByBorrowerId , updateGovernmentQuestionByBorrowerId)

        when(stringForSpecificFragment) {
               "Undisclosed Borrowered Funds" ->{
                   findNavController().navigate(R.id.action_undisclosed_borrowerfund, bundle )
               }
               "Family or business affiliation" ->{  findNavController().navigate(R.id.action_family_affiliation , bundle ) }
               "Ownership Interest in Property" ->{

                   bundle.putStringArrayList(AppConstant.ownerShipGlobalData, ownerShipGlobalData)
                   findNavController().navigate(R.id.action_ownership_interest , bundle)
               }
               "Own Property Type" ->{}
               "Debt Co-Signer or Guarantor" ->{  findNavController().navigate(R.id.action_debt_co , bundle )}
               "Outstanding Judgements" ->{  findNavController().navigate(R.id.action_outstanding , bundle)}
               "Federal Debt Deliquency" ->{ findNavController().navigate(R.id.action_federal_debt , bundle)}
               "Party to Lawsuit" ->{
                   bundle.putStringArrayList(AppConstant.ownerShipGlobalData, ownerShipGlobalData)
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

    private val asianChildList:ArrayList<DemoGraphicRaceDetail> = arrayListOf()

    private val nativeHawaiianChildList:ArrayList<DemoGraphicRaceDetail> = arrayListOf()

    private lateinit var variableDemoGraphicData:DemoGraphicData
    private lateinit var variableRaceList:ArrayList<DemoGraphicRace>
    private lateinit var variableEthnicityList: ArrayList<EthnicityDemoGraphic>
    private var variableGender:Int? = null


    private fun showEthnicityInnerBox(){
        if(otherEthnicity.isNotEmpty() && otherEthnicity.isNotBlank()) {
            otherEthnicity = "Other Pacific Islander: $otherEthnicity"
            otherEthnicity = otherEthnicity.substring(0, otherEthnicity.length-2)
            demoGraphicConstraintLayout.other_ethnicity.text = otherEthnicity
            demoGraphicConstraintLayout.other_ethnicity.visibility = View.VISIBLE
            demoGraphicConstraintLayout.hispanic_or_latino_child_box_layout.visibility = View.VISIBLE
        }
        else
            demoGraphicConstraintLayout.other_ethnicity.visibility = View.GONE

        if(ethnicityChildNames.isNotEmpty() && ethnicityChildNames.isNotBlank()) {
            ethnicityChildNames = ethnicityChildNames.substring(0, ethnicityChildNames.length-2)
            demoGraphicConstraintLayout.ethnicity_children.text = ethnicityChildNames
            demoGraphicConstraintLayout.ethnicity_children.visibility = View.VISIBLE
            demoGraphicConstraintLayout.hispanic_or_latino_child_box_layout.visibility = View.VISIBLE
        }
        else
            demoGraphicConstraintLayout.ethnicity_children.visibility = View.GONE
    }

    private fun showNativeHawaiiInnerBox(){
        if(nativeHawaiiOtherRace.isNotEmpty() && nativeHawaiiOtherRace.isNotBlank()) {
            nativeHawaiiOtherRace = "Other Pacific Islander: $nativeHawaiiOtherRace"
            nativeHawaiiOtherRace = nativeHawaiiOtherRace.substring(0, nativeHawaiiOtherRace.length-2)
            demoGraphicConstraintLayout.other_typed_native_hawaiian.text = nativeHawaiiOtherRace
            demoGraphicConstraintLayout.other_typed_native_hawaiian.visibility = View.VISIBLE
            demoGraphicConstraintLayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
        }
        else
            demoGraphicConstraintLayout.other_typed_native_hawaiian.visibility = View.GONE

        if(nativeHawaiiChildNames.isNotEmpty() && nativeHawaiiChildNames.isNotBlank()) {
            nativeHawaiiChildNames = nativeHawaiiChildNames.substring(0, nativeHawaiiChildNames.length-2)
            demoGraphicConstraintLayout.child_native_hawaiian.text = nativeHawaiiChildNames
            demoGraphicConstraintLayout.child_native_hawaiian.visibility = View.VISIBLE
            demoGraphicConstraintLayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
        }
        else
            demoGraphicConstraintLayout.child_native_hawaiian.visibility = View.GONE
    }

    private fun showAsianInnerBox(){
        if(otherAsianRace.isNotEmpty() && otherAsianRace.isNotBlank()) {
            otherAsianRace = "Other Asian: $otherAsianRace"
            demoGraphicConstraintLayout.other_asian_race.text = otherAsianRace
            demoGraphicConstraintLayout.other_asian_race.visibility = View.VISIBLE
            demoGraphicConstraintLayout.asian_child_box_layout.visibility = View.VISIBLE
        }
        else
            demoGraphicConstraintLayout.other_asian_race.visibility = View.GONE

        if(asianChildNames.isNotEmpty() && asianChildNames.isNotBlank()) {
            asianChildNames = asianChildNames.substring(0, asianChildNames.length-2)
            demoGraphicConstraintLayout.asian_child_names.text = asianChildNames
            demoGraphicConstraintLayout.asian_child_names.visibility = View.VISIBLE
            demoGraphicConstraintLayout.asian_child_box_layout.visibility = View.VISIBLE
        }
        else
            demoGraphicConstraintLayout.asian_child_names.visibility = View.GONE
    }

    private fun addDemoGraphicEvents(){

        demoGraphicConstraintLayout.do_not_wish_check_box.setOnClickListener{
            demoGraphicConstraintLayout.white_check_box.isChecked = false
            demoGraphicConstraintLayout.black_or_african_check_box.isChecked = false
            demoGraphicConstraintLayout.american_or_indian_check_box.isChecked = false
            demoGraphicConstraintLayout.native_hawaian_or_other_check_box.isChecked = false
            demoGraphicConstraintLayout.asian_check_box.isChecked = false

            if(demoGraphicConstraintLayout.do_not_wish_check_box.isChecked) {
                demoGraphicConstraintLayout.asian_child_box_layout.visibility = View.GONE
                demoGraphicConstraintLayout.native_hawaian_child_box_layout.visibility = View.GONE
            }

        }

        demoGraphicConstraintLayout.white_check_box.setOnClickListener{ demoGraphicConstraintLayout.do_not_wish_check_box.isChecked = false }
        demoGraphicConstraintLayout.native_hawaian_or_other_check_box.setOnClickListener{ demoGraphicConstraintLayout.do_not_wish_check_box.isChecked = false }
        demoGraphicConstraintLayout.black_or_african_check_box.setOnClickListener{ demoGraphicConstraintLayout.do_not_wish_check_box.isChecked = false }
        demoGraphicConstraintLayout.asian_check_box.setOnClickListener{ demoGraphicConstraintLayout.do_not_wish_check_box.isChecked = false }
        demoGraphicConstraintLayout.american_or_indian_check_box.setOnClickListener{ demoGraphicConstraintLayout.do_not_wish_check_box.isChecked = false }

    }



    private fun observeDemoGraphicData( contentCell:ConstraintLayout){
        borrowerAppViewModel.demoGraphicInfoList.observe(viewLifecycleOwner,{ demoGraphicInfoList->

            demoGraphicConstraintLayout.asian_child_box_layout.visibility = View.GONE
            demoGraphicConstraintLayout.native_hawaian_child_box_layout.visibility = View.GONE
            demoGraphicConstraintLayout.hispanic_or_latino_child_box_layout.visibility = View.GONE

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
                                demoGraphicConstraintLayout.demo_male.isChecked = true
                            else
                            if (genderId == 2)
                                demoGraphicConstraintLayout.demo_female.isChecked = true
                            else
                            if (genderId == 3)
                                demoGraphicConstraintLayout.demo_do_not_wish_to_provide.isChecked = true
                        }

                        demoGraphicData.ethnicity?.let { ethnicityList ->
                            variableEthnicityList = ethnicityList
                            if (ethnicityList.isNotEmpty()) {
                                val selectedEthnicity = ethnicityList[0]
                                if (selectedEthnicity.ethnicityId == 1) {
                                    demoGraphicConstraintLayout.hispanic_or_latino.isChecked = true
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
                                    demoGraphicConstraintLayout.not_hispanic.isChecked = true
                                else
                                if (selectedEthnicity.ethnicityId == 3)
                                    demoGraphicConstraintLayout.not_telling_ethnicity.isChecked = true
                            }
                        }

                        demoGraphicData.race?.let { raceList ->
                            variableRaceList = raceList
                            for (race in raceList) {
                                if (race.raceId == 1) {
                                    demoGraphicConstraintLayout.american_or_indian_check_box.isChecked = true
                                }
                                if (race.raceId == 2) {
                                    demoGraphicConstraintLayout.asian_check_box.isChecked = true
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
                                    demoGraphicConstraintLayout.black_or_african_check_box.isChecked =
                                        true
                                }
                                if (race.raceId == 4) {
                                    demoGraphicConstraintLayout.native_hawaian_or_other_check_box.isChecked =
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
                                    demoGraphicConstraintLayout.white_check_box.isChecked = true
                                }
                                if (race.raceId == 6) {
                                    demoGraphicConstraintLayout.do_not_wish_check_box.performClick()
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

        demoGraphicConstraintLayout.american_or_indian_check_box.setOnCheckedChangeListener{ buttonView, isChecked ->
            updateDemoGraphicRace(1, isChecked)
        }

        demoGraphicConstraintLayout.asian_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                demoGraphicConstraintLayout.asian_child_box_layout.visibility = View.VISIBLE
                val bundle = bundleOf(AppConstant.asianChildList to asianChildList)
                findNavController().navigate(R.id.action_asian , bundle)
                Timber.e("not accessible...")
            }
            updateDemoGraphicRace(2, isChecked)

        }

        demoGraphicConstraintLayout.black_or_african_check_box.setOnCheckedChangeListener{ buttonView, isChecked ->
            updateDemoGraphicRace(3, isChecked)
        }

        demoGraphicConstraintLayout.native_hawaian_or_other_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                demoGraphicConstraintLayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
                val bundle = bundleOf(AppConstant.nativeHawaianChildList to nativeHawaiianChildList)
                findNavController().navigate(R.id.action_native_hawai, bundle)
            }
            updateDemoGraphicRace(4, isChecked)
        }

        demoGraphicConstraintLayout.white_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            updateDemoGraphicRace(5, isChecked)
        }


        demoGraphicConstraintLayout.do_not_wish_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            variableRaceList.clear()
            updateDemoGraphicRace(6, isChecked)
        }

        demoGraphicConstraintLayout.native_hawaian_child_box_layout.setOnClickListener{
            if (demoGraphicConstraintLayout.native_hawaian_or_other_check_box.isChecked) {
                demoGraphicConstraintLayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
                val bundle = bundleOf(AppConstant.nativeHawaianChildList to nativeHawaiianChildList)
                findNavController().navigate(R.id.action_native_hawai, bundle)
            }

        }

        demoGraphicConstraintLayout.asian_child_box_layout.setOnClickListener{
            if ( demoGraphicConstraintLayout.asian_check_box.isChecked) {
                demoGraphicConstraintLayout.asian_child_box_layout.visibility = View.VISIBLE
                val bundle = bundleOf(AppConstant.asianChildList to asianChildList)
                findNavController().navigate(R.id.action_asian , bundle)
                Timber.e("not accessible...")
            }
        }

        demoGraphicConstraintLayout.hispanic_or_latino.setOnClickListener {
            val bundle = bundleOf(AppConstant.ethnicityChildList to ethnicityChildList)
            findNavController().navigate(R.id.action_hispanic , bundle)
            demoGraphicConstraintLayout.not_hispanic.isChecked = false
            demoGraphicConstraintLayout.not_telling_ethnicity.isChecked = false
            showEthnicityInnerBox()
            updateDemoGraphicEthnicity(1)
        }

        demoGraphicConstraintLayout.hispanic_or_latino_child_box_layout.setOnClickListener {
            val bundle = bundleOf(AppConstant.ethnicityChildList to ethnicityChildList)
            findNavController().navigate(R.id.action_hispanic, bundle)
            demoGraphicConstraintLayout.not_hispanic.isChecked = false
            demoGraphicConstraintLayout.not_telling_ethnicity.isChecked = false
        }

        demoGraphicConstraintLayout.not_hispanic.setOnClickListener{
            demoGraphicConstraintLayout.hispanic_or_latino.isChecked = false
            demoGraphicConstraintLayout.not_telling_ethnicity.isChecked = false
            demoGraphicConstraintLayout.hispanic_or_latino_child_box_layout.visibility = View.GONE
            updateDemoGraphicEthnicity(2)
        }

        demoGraphicConstraintLayout.not_telling_ethnicity.setOnClickListener{
            demoGraphicConstraintLayout.hispanic_or_latino.isChecked = false
            demoGraphicConstraintLayout.not_hispanic.isChecked = false
            demoGraphicConstraintLayout.hispanic_or_latino_child_box_layout.visibility = View.GONE
            updateDemoGraphicEthnicity(3)
        }


            demoGraphicConstraintLayout.demo_male.setOnClickListener { updateDemoGraphicGender(1)}
            demoGraphicConstraintLayout.demo_female.setOnClickListener { updateDemoGraphicGender(2)}
            demoGraphicConstraintLayout.demo_do_not_wish_to_provide.setOnClickListener { updateDemoGraphicGender(3)}

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
                updateDemoGraphicService()
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
                updateDemoGraphicService()
            }

        }

        navController.currentBackStackEntry?.savedStateHandle?.getLiveData<ArrayList<EthnicityDetailDemoGraphic>>(AppConstant.selectedEthnicityChildList)?.observe(
            viewLifecycleOwner) { selectedEthnicityChildList ->
                // Do something with the result.
                ethnicityChildNames = ""
                otherEthnicity = ""
                selectedEthnicityChildList?.let { theList ->
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
                }
                showEthnicityInnerBox()
                variableEthnicityList[0].ethnicityDetails  = selectedEthnicityChildList
                updateDemoGraphicService()
        }



    }




    private fun updateDemoGraphicService(){
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                variableDemoGraphicData.genderId = variableGender
                variableDemoGraphicData.race = variableRaceList
                variableDemoGraphicData.ethnicity = variableEthnicityList
                borrowerAppViewModel.addOrUpdateDemoGraphic(authToken, variableDemoGraphicData)
            }
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


