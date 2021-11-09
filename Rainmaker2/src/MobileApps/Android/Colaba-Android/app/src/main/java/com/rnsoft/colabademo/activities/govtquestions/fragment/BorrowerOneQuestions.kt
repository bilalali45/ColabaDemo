package com.rnsoft.colabademo

import android.content.Context
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatRadioButton
import androidx.appcompat.widget.AppCompatTextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.*
import timber.log.Timber
import android.widget.LinearLayout
import android.util.DisplayMetrics
import androidx.core.os.bundleOf
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


class BorrowerOneQuestions : GovtQuestionBaseFragment() {

    private lateinit var binding: BorrowerOneQuestionsLayoutBinding


    private var idToContentMapping = HashMap<Int, ConstraintLayout>(0)
    private var innerLayoutHashMap = HashMap<AppCompatTextView, ConstraintLayout>(0)
    //private var openDetailBoxHashMap = HashMap<AppCompatRadioButton, ConstraintLayout>(0)
    //private var closeDetailBoxHashMap = HashMap<AppCompatRadioButton, ConstraintLayout>(0)
    //private val tabArrayList:ArrayList<AppCompatTextView> = arrayListOf()

    private val bAppViewModel: BorrowerApplicationViewModel by activityViewModels()

    private var ethnicityChildNames = ""
    private var otherEthnicity = ""

    private var nativeHawaiiChildNames = ""
    private var nativeHawaiiOtherRace = ""

    private var asianChildNames = ""
    private var otherAsianRace = ""

    private  var tabBorrowerId:Int? = null

    private  lateinit var lastQData:QuestionData

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = BorrowerOneQuestionsLayoutBinding.inflate(inflater, container, false)
        arguments?.let {
            tabBorrowerId = it.getInt(AppConstant.tabBorrowerId)
        }
        setupLayout()
        super.addListeners(binding.root)
        return binding.root
    }


    private fun setupLayout(){
        setUpDynamicTabs( )
    }

    private fun setUpDynamicTabs(){
        bAppViewModel.governmentQuestionsModelClassList.observe(viewLifecycleOwner, Observer { governmentQuestionsModelClassList->

            if(governmentQuestionsModelClassList.size>0){
                var selectedGovernmentQuestionModel: GovernmentQuestionsModelClass? =null
                for(item in governmentQuestionsModelClassList){
                    if(item.passedBorrowerId == tabBorrowerId ) {
                        selectedGovernmentQuestionModel = item
                        break
                    }
                }

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
                                    appCompactTextView.setOnClickListener(scrollerTab)
                                    horizontalTabArrayList.add(appCompactTextView)
                                }
                            }
                        }

                        if (zeroIndexAppCompat != null) {
                            val appCompactTextView =
                                createAppCompactTextView(AppConstant.demographicInformation, 0)
                            binding.horizontalTabs.addView(appCompactTextView)
                            val contentView = createContentLayoutForTab(
                                QuestionData(id = 100, parentQuestionId = null, headerText = AppConstant.demographicInformation,
                                    questionSectionId = 1,
                                    ownTypeId = 1,
                                    firstName = lastQData.firstName,
                                    lastName = lastQData.lastName,
                                    question = "", answer = null, answerDetail = null,
                                    selectionOptionId = null, answerData = null)
                            )

                            innerLayoutHashMap.put(appCompactTextView, contentView)
                            idToContentMapping.put(100, contentView)

                            binding.parentContainer.addView(contentView)
                            appCompactTextView.setOnClickListener(scrollerTab)
                            horizontalTabArrayList.add(appCompactTextView)
                        }

                        var ownerShip = true

                        for (qData in questionData) {
                            qData.parentQuestionId?.let { parentQuestionId ->
                                if (parentQuestionId == bankruptcyConstraintLayout.id) {
                                    Timber.e("bankruptcyConstraintLayout " + qData.question)
                                    Timber.e(qData.answerDetail.toString())
                                    var extractedAnswer = ""
                                    val bankruptyAnswerData = qData.answerData as ArrayList<*>
                                    if (bankruptyAnswerData != null && bankruptyAnswerData.size > 0 && qData.answer != null && !qData.answer.equals("No", true)) {
                                        if (bankruptyAnswerData[0] != null) {
                                            val getrow: Any = bankruptyAnswerData[0]
                                            val t: LinkedTreeMap<Any, Any> =
                                                getrow as LinkedTreeMap<Any, Any>
                                            val chapter1 = t["`1`"].toString()
                                            extractedAnswer = chapter1
                                            bankruptcyGlobalData.value1 = true
                                            Timber.e("1 = " + chapter1)
                                        }
                                        if (bankruptyAnswerData[1] != null && bankruptyAnswerData.size > 1) {
                                            val getrow: Any = bankruptyAnswerData[1]
                                            val t: LinkedTreeMap<Any, Any> =
                                                getrow as LinkedTreeMap<Any, Any>
                                            val chapter2 = t["`2`"].toString()
                                            extractedAnswer = "$extractedAnswer, $chapter2"
                                            bankruptcyGlobalData.value2 = true
                                            Timber.e("2 = " + chapter2)

                                        }

                                        if (bankruptyAnswerData[2] != null && bankruptyAnswerData.size > 2) {
                                            val getrow: Any = bankruptyAnswerData[2]
                                            val t: LinkedTreeMap<Any, Any> =
                                                getrow as LinkedTreeMap<Any, Any>
                                            val chapter3 = t["`3`"].toString()
                                            extractedAnswer = "$extractedAnswer, $chapter3"
                                            bankruptcyGlobalData.value3 = true
                                            Timber.e("3 = " + chapter3)
                                        }

                                        if (bankruptyAnswerData[3] != null && bankruptyAnswerData.size > 3) {
                                            val getrow: Any = bankruptyAnswerData[2]
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
                                else if (parentQuestionId == ownerShipConstraintLayout.id && qData.answer != null && !qData.answer.equals("No", true)) {
                                    Timber.e("ownerShipConstraintLayout " + qData.question)
                                    Timber.e(qData.answerDetail.toString())

                                    if (ownerShip) {
                                        ownerShip = false
                                        ownerShipConstraintLayout.detail_title.text = qData.question
                                        ownerShipConstraintLayout.detail_text.text = qData.answer
                                        ownerShipConstraintLayout.detail_title.setTypeface(null, Typeface.NORMAL)
                                        ownerShipConstraintLayout.detail_text.setTypeface(null, Typeface.BOLD)
                                        ownerShipGlobalData.add(qData.answer)
                                        ownerShipConstraintLayout.govt_detail_box.visibility = View.VISIBLE
                                    } else {
                                        ownerShipConstraintLayout.detail_title2.text = qData.question
                                        ownerShipConstraintLayout.detail_text2.text = qData.answer
                                        ownerShipConstraintLayout.detail_title2.setTypeface(null, Typeface.NORMAL)
                                        ownerShipConstraintLayout.detail_text2.setTypeface(null, Typeface.BOLD)
                                        ownerShipGlobalData.add(qData.answer)
                                        ownerShipConstraintLayout.govt_detail_box2.visibility = View.VISIBLE
                                    }

                                        }

                                }
                            }
                    }  // close of let block...
                    zeroIndexAppCompat?.performClick()
                }
            }
            binding.parentContainer.postDelayed({ binding.parentContainer.visibility = View.VISIBLE },500)

        })
    }

    companion object{
        var childGlobalList:ArrayList<ChildAnswerData> = arrayListOf()
        var bankruptcyGlobalData:BankruptcyAnswerData = BankruptcyAnswerData()
        var ownerShipGlobalData:ArrayList<String> = arrayListOf()
    }

    fun <T> stringToArray2(s: String?, clazz: Class<T>?): T {
        val newList = Gson().fromJson(s, clazz)!!
        return newList //or return Arrays.asList(new Gson().fromJson(s, clazz)); for a one-liner
    }

    fun convertPixelsToDp(px: Float, context: Context): Int {
        return (px / (context.resources.displayMetrics.densityDpi.toFloat() / DisplayMetrics.DENSITY_DEFAULT)).toInt()
    }

    fun convertDpToPixel(dp: Float, context: Context): Int {
        return (dp * (context.resources.displayMetrics.densityDpi.toFloat() / DisplayMetrics.DENSITY_DEFAULT)).toInt()
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

    private fun createContentLayoutForTab(questionData:QuestionData):ConstraintLayout{
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
            }
        }
        else
        if(questionData.headerText == AppConstant.childConstantValue) {
            contentCell = layoutInflater.inflate(R.layout.children_separate_layout, null) as ConstraintLayout
            childSupport = true
            childConstraintLayout = contentCell
            questionData.id?.let {
                childConstraintLayout.id = it
            }
        }
        else
        if(questionData.headerText?.trim() == AppConstant.Bankruptcy) {
            contentCell = layoutInflater.inflate(R.layout.common_govt_content_layout, null) as ConstraintLayout
            bankruptcyConstraintLayout = contentCell
            questionData.id?.let {
                bankruptcyConstraintLayout.id = it
            }

        }
        else
        if(questionData.headerText?.trim() == AppConstant.demographicInformation) {
            contentCell = layoutInflater.inflate(R.layout.new_demo_graphic_show_layout, null) as ConstraintLayout
            demoGraphicConstraintLayout = contentCell
            questionData.id?.let {
                demoGraphicConstraintLayout.id = it
            }

            observeDemoGraphicData()
            return contentCell
        }
        else
            contentCell = layoutInflater.inflate(R.layout.common_govt_content_layout, null) as ConstraintLayout

        //contentCell.visibility = View.INVISIBLE
        //Timber.e(" questionData.question "+questionData.question)

        questionData.headerText?.let {
            contentCell.govt_detail_box.tag = it
            contentCell.ans_yes.tag = it
            headerTitle = it
        }
        contentCell.govt_question.text =  questionData.question
        questionData.answerDetail?.let {
            contentCell.detail_text.text = it
        }


        if(ownerShip) {
            contentCell.govt_detail_box.setOnClickListener {  openDetailBoxNew(headerTitle) }
            contentCell.govt_detail_box2.setOnClickListener { openDetailBoxNew(headerTitle) }
        }

        if(childSupport){
            contentCell.govt_detail_box.setOnClickListener {  openDetailBoxNew(headerTitle) }
            contentCell.govt_detail_box2.setOnClickListener {openDetailBoxNew(headerTitle) }
            contentCell.govt_detail_box3.setOnClickListener { openDetailBoxNew(headerTitle) }

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
                    openDetailBoxNew(headerTitle)
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
                if(questionData.answerDetail!=null && questionData.answer.equals("Yes", true))
                    contentCell.govt_detail_box.visibility = View.VISIBLE
            }

            contentCell.ans_no.setOnClickListener {
                contentCell.govt_detail_box.visibility = View.INVISIBLE
                contentCell.govt_detail_box2?.visibility = View.INVISIBLE
                contentCell.govt_detail_box3?.visibility = View.INVISIBLE
            }
            contentCell.ans_yes.setOnClickListener {
                if(questionData.answer.equals("Yes", true))
                    contentCell.govt_detail_box.visibility = View.VISIBLE
                    contentCell.govt_detail_box2?.visibility = View.VISIBLE
                    contentCell.govt_detail_box3?.visibility = View.VISIBLE
                    openDetailBoxNew(headerTitle)
                }
        }

        return contentCell
    }

    private fun openDetailBoxNew(stringForSpecificFragment:String){
           when(stringForSpecificFragment) {
               "Undisclosed Borrowered Funds" ->{  findNavController().navigate(R.id.action_undisclosed_borrowerfund)}
               "Amount to Borrow?" ->{}
               "Ownership Interest in Property" ->{  findNavController().navigate(R.id.action_ownership_interest)}
               "Own Property Type" ->{}
               "Debt Co-Signer or Guarantor" ->{  findNavController().navigate(R.id.action_debt_co)}
               "Outstanding Judgements" ->{  findNavController().navigate(R.id.action_outstanding)}
               "Federal Debt Deliquency" ->{ findNavController().navigate(R.id.action_federal_debt)}
               "Party to Lawsuit" ->{ findNavController().navigate(R.id.action_party_to) }
               "Bankruptcy " ->{    findNavController().navigate(R.id.action_bankruptcy) }
               "Child Support, Alimony, etc." ->{ findNavController().navigate(R.id.action_child_support) }
               "Type" ->{}
               "Foreclosured Property" ->{ findNavController().navigate(R.id.action_fore_closure_property) }
               "Pre-Foreclosureor Short Sale" ->{ findNavController().navigate(R.id.action_pre_for_closure) }
               "Title Conveyance" ->{ findNavController().navigate(R.id.action_title_conveyance)}
               "Property Title" ->{}
               "Family or Business affiliation" ->{}
               else->{
                   Timber.e(" not matching with header title...")
               }
           }
    }

    private val ethnicityChildList:ArrayList<EthnicityDetailDemoGraphic> = arrayListOf()

    private val asianChildList:ArrayList<DemoGraphicRaceDetail> = arrayListOf()

    private val nativeHawaiianChildList:ArrayList<DemoGraphicRaceDetail> = arrayListOf()

    private fun observeDemoGraphicData(){
        bAppViewModel.demoGraphicInfoList.observe(viewLifecycleOwner,{  demoGraphicInfoList->
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
                        ethnicityChildNames = ""
                        otherEthnicity = ""
                        nativeHawaiiChildNames = ""
                        nativeHawaiiOtherRace = ""
                        asianChildNames = ""
                        otherAsianRace = ""

                        demoGraphicData.genderId?.let { genderId ->
                            if (genderId == 1)
                                demoGraphicConstraintLayout.demo_male.isChecked = true
                            else
                                if (genderId == 2)
                                    demoGraphicConstraintLayout.demo_female.isChecked = true
                                else
                                    if (genderId == 3)
                                        demoGraphicConstraintLayout.demo_do_not_wish_to_provide.isChecked =
                                            true
                        }

                        demoGraphicData.ethnicity?.let { ethnicityList ->
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
                                } else
                                    if (selectedEthnicity.ethnicityId == 2)
                                        demoGraphicConstraintLayout.not_hispanic.isChecked = true
                                    else
                                        if (selectedEthnicity.ethnicityId == 3)
                                            demoGraphicConstraintLayout.not_telling_ethnicity.isChecked =
                                                true
                            }
                        }



                        demoGraphicData.race?.let { raceList ->
                            for (race in raceList) {
                                if (race.raceId == 1) {
                                    demoGraphicConstraintLayout.american_or_indian_check_box.isChecked =
                                        true
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

    private fun setUpDemoGraphicScreen() {

        demoGraphicConstraintLayout.asian_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                demoGraphicConstraintLayout.asian_child_box_layout.visibility = View.VISIBLE
                val bundle = bundleOf(AppConstant.asianChildList to asianChildList)
                findNavController().navigate(R.id.action_asian , bundle)
                Timber.e("not accessible...")
            }

        }

        demoGraphicConstraintLayout.native_hawaian_or_other_check_box.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                demoGraphicConstraintLayout.native_hawaian_child_box_layout.visibility = View.VISIBLE
                val bundle = bundleOf(AppConstant.nativeHawaianChildList to nativeHawaiianChildList)
                findNavController().navigate(R.id.action_native_hawai, bundle)
            }
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
        }

        demoGraphicConstraintLayout.not_telling_ethnicity.setOnClickListener{
            demoGraphicConstraintLayout.hispanic_or_latino.isChecked = false
            demoGraphicConstraintLayout.not_hispanic.isChecked = false
            demoGraphicConstraintLayout.hispanic_or_latino_child_box_layout.visibility = View.GONE
        }


    }


    private val scrollerTab = object:View.OnClickListener{
        override fun onClick(p0: View) {
            if(p0 is AppCompatTextView){
                for(item in horizontalTabArrayList){
                    item.isActivated = false
                    item.setTextColor(resources.getColor(R.color.doc_filter_txt_color, requireActivity().theme))
                }
                p0.isActivated = true
                p0.setTextColor(resources.getColor(R.color.colaba_primary_color, requireActivity().theme))
                for ((key, value) in innerLayoutHashMap) {
                    if(key == p0)
                        value.visibility = View.VISIBLE
                    else
                        value.visibility = View.INVISIBLE
                }
            }
        }
    }




}


