package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.*
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.activityViewModels
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import androidx.recyclerview.widget.RecyclerView.OnItemTouchListener
import com.rnsoft.colabademo.activities.details.bapplication.RealEstateClickListener
import com.rnsoft.colabademo.databinding.DetailApplicationTabBinding
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import javax.inject.Inject
import kotlin.math.roundToInt


@AndroidEntryPoint
class BorrowerApplicationFragment : BaseFragment() , AdapterClickListener, GovernmentQuestionClickListener,RealEstateClickListener {

    private var _binding: DetailApplicationTabBinding? = null
    private val binding get() = _binding!!
    private lateinit var horizontalRecyclerView: RecyclerView
    private lateinit var realStateRecyclerView: RecyclerView
    private lateinit var questionsRecyclerView: RecyclerView
    private lateinit var loanLayout : ConstraintLayout
    private lateinit var subjectPropertyLayout : ConstraintLayout
    private val detailViewModel: DetailViewModel by activityViewModels()
    private val borrowerApplicationViewModel: BorrowerApplicationViewModel by activityViewModels()
    private var borrowerInfoList: ArrayList<BorrowersInformation> = ArrayList()
    private var realStateList: ArrayList<RealStateOwn> = ArrayList()
    private var questionList: ArrayList<BorrowerQuestionsModel> = ArrayList()
    private var borrowerInfoAdapter  = CustomBorrowerAdapter(borrowerInfoList , this)
    private var realStateAdapter  = RealStateAdapter(realStateList,this)
    private var questionAdapter  = QuestionAdapter(questionList, this)

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = DetailApplicationTabBinding.inflate(inflater, container, false)
        val root: View = binding.root
        (activity as DetailActivity).binding.requestDocFab.visibility = View.GONE
        horizontalRecyclerView = root.findViewById(R.id.horizontalRecycleView)
        realStateRecyclerView = root.findViewById(R.id.realStateHorizontalRecyclerView)
        questionsRecyclerView = root.findViewById(R.id.govtQuestionHorizontalRecyclerView)
        loanLayout = root.findViewById(R.id.loanInfoLayout)
        subjectPropertyLayout = root.findViewById(R.id.constraintLayout5)

        //applicationTopContainer = root.findViewById(R.id.application_top_container)

        binding.assetsConstraintLayout.setOnClickListener{
            var borrowerIndex = 0
            borrowerApplicationViewModel.resetAssetModelClass()
            navigateToAssetActivity()
        }

        binding.incomeConstraintLayout.setOnClickListener{
            borrowerApplicationViewModel.resetIncomeModelClass()
            navigateToIncomeActivity()
        }

        loanLayout.setOnClickListener {

            val detailActivity = (activity as? DetailActivity)
            detailActivity?.let {
                val borrowerLoanActivity = Intent(requireActivity(), BorrowerLoanActivity::class.java)
                it.loanApplicationId?.let { loanId ->
                    borrowerLoanActivity.putExtra(AppConstant.loanApplicationId, loanId)
                    //Log.e("Loan Id", ""+it.loanApplicationId)
                }
                it.borrowerLoanPurpose?.let{ loanPurpose->
                    borrowerLoanActivity.putExtra(AppConstant.loanPurpose, loanPurpose)
                }
                startActivity(borrowerLoanActivity)
            }
        }

        subjectPropertyLayout.setOnClickListener {
            val detailActivity = (activity as? DetailActivity)
            detailActivity?.let {
                val intent = Intent(requireActivity(), SubjectPropertyActivity::class.java)
                //Log.e("loanAppID", ""+ it.loanApplicationId)
                //Log.e("purpose", ""+ it.)
                intent.putExtra(AppConstant.loanApplicationId, it.loanApplicationId)
                intent.putExtra(AppConstant.borrowerPurpose, it.borrowerLoanPurpose)
                startActivity(intent)

            }
        }

        val linearLayoutManager = LinearLayoutManager(activity , LinearLayoutManager.HORIZONTAL, false)

        horizontalRecyclerView.apply {
            this.layoutManager =linearLayoutManager
            //this.setHasFixedSize(true)
            this.adapter = borrowerInfoAdapter
        }

        val realStateLayoutManager = LinearLayoutManager(activity , LinearLayoutManager.HORIZONTAL, false)

        realStateRecyclerView.apply {
            this.layoutManager =realStateLayoutManager
            //this.setHasFixedSize(true)
            this.adapter = realStateAdapter
        }
        realStateAdapter.notifyDataSetChanged()

        val questionLayoutManger = LinearLayoutManager(activity , LinearLayoutManager.HORIZONTAL, false)

        questionsRecyclerView.apply {
            this.layoutManager =questionLayoutManger
            //this.setHasFixedSize(true)
            this.adapter = questionAdapter
        }
        questionAdapter.notifyDataSetChanged()

        binding.applicationTabLayout.visibility = View.INVISIBLE

        detailViewModel.borrowerApplicationTabModel.observe(viewLifecycleOwner, { appTabModel->
            if (appTabModel != null) {
                binding.applicationTopContainer.visibility = View.VISIBLE
                binding.applicationTabLayout.visibility = View.VISIBLE

                appTabModel.borrowerAppData?.subjectProperty?.subjectPropertyAddress?.let {
                    binding.bAppAddress.text = it.street+" "+it.unit+"\n"+it.city+" "+it.stateName+" "+it.zipCode+" "+it.countryName
                }

                appTabModel.borrowerAppData?.subjectProperty?.propertyTypeName?.let{
                    binding.bAppPropertyType.text = it
                }

                appTabModel.borrowerAppData?.subjectProperty?.propertyUsageDescription.let{
                    binding.bAppPropertyUsage.text = it
                }

                appTabModel.borrowerAppData?.loanInformation?.loanAmount?.let{
                    binding.bAppLoanPayment.text = "$".plus(AppSetting.returnAmountFormattedString(it))
                }

                appTabModel.borrowerAppData?.loanInformation?.downPayment?.let{
                    binding.bAppDownPayment.text = "$".plus(AppSetting.returnAmountFormattedString(it))
                }

                appTabModel.borrowerAppData?.loanInformation?.downPaymentPercent?.let {
                    binding.bAppPercentage.text = "("+it.roundToInt()+"%)"
                }

                appTabModel.borrowerAppData?.assetAndIncome?.totalAsset?.let{
                    binding.bAppTotalAssets.text = "$".plus(AppSetting.returnAmountFormattedString(it))
                    binding.bAppTotalAssets.text = "$".plus(AppSetting.returnAmountFormattedString(it))
                }

                appTabModel.borrowerAppData?.assetAndIncome?.totalMonthyIncome?.let{
                    binding.bAppMonthlylncome.text = "$".plus(AppSetting.returnAmountFormattedString(it))
                }

                var races:ArrayList<Race>? =null
                var ethnicities:ArrayList<Ethnicity>? =null

                appTabModel.borrowerAppData?.let { bAppData->
                    bAppData.borrowersInformation?.let { borrowersList ->
                        borrowerInfoList.clear()
                        borrowerInfoList = borrowersList

                        for(borrower in borrowersList){
                            Timber.e("BApp -- " + borrower.firstName , borrower.borrowerId, borrower.owntypeId , borrower.genderName)
                           if(borrower.owntypeId == 1) {
                               borrower.races?.let {
                                   races = it
                               }
                               borrower.ethnicities?.let {
                                   ethnicities = it
                               }
                           }
                        }
                   }
                }



                appTabModel.borrowerAppData?.let { bAppData->
                    bAppData.realStateOwns?.let {
                        for(item in it){
                            Timber.e(" Get -- "+item)
                            Timber.e(" details -- "+item.borrowerId + "  "+item.propertyInfoId +"  "+ item.propertyTypeId + "  "+item.propertyTypeName)
                        }
                        realStateList.clear()
                        realStateList = it

                    }
                }

                appTabModel.borrowerAppData?.let { bAppData->
                    bAppData.borrowerQuestionsModel?.let {
                        questionList.clear()
                        questionList = it
                    }
                }


                //////////////////////////////////////////////////////////////////////////////////////////////////
                // add add-more last cell to the adapters
                borrowerInfoList.add(BorrowersInformation(-1,"",0,"","", "",0,null, null, true))
                borrowerInfoAdapter  = CustomBorrowerAdapter(borrowerInfoList , this@BorrowerApplicationFragment )
                horizontalRecyclerView.adapter = borrowerInfoAdapter
                borrowerInfoAdapter.notifyDataSetChanged()


                realStateList.add(RealStateOwn(null,0,0,0,"", true))
                realStateAdapter  = RealStateAdapter(realStateList,this@BorrowerApplicationFragment)
                realStateRecyclerView.adapter = realStateAdapter
                realStateAdapter.notifyDataSetChanged()


                questionList.add(BorrowerQuestionsModel(null,null, true, races, ethnicities ))
                questionAdapter  = QuestionAdapter(questionList , this@BorrowerApplicationFragment)
                questionsRecyclerView.adapter = questionAdapter
                questionAdapter.notifyDataSetChanged()

            }
            else
               binding.applicationTabLayout.visibility = View.INVISIBLE
        })

        horizontalRecyclerView.addOnItemTouchListener(object : OnItemTouchListener {
            override fun onInterceptTouchEvent(rv: RecyclerView, e: MotionEvent): Boolean {
                when (e.action) {
                    MotionEvent.ACTION_MOVE -> rv.parent.requestDisallowInterceptTouchEvent(true)
                }
                return false
            }

            override fun onTouchEvent(rv: RecyclerView, e: MotionEvent) {}
            override fun onRequestDisallowInterceptTouchEvent(disallowIntercept: Boolean) {}
        })

        realStateRecyclerView.addOnItemTouchListener(object : OnItemTouchListener {
            override fun onInterceptTouchEvent(rv: RecyclerView, e: MotionEvent): Boolean {
                when (e.action) {
                    MotionEvent.ACTION_MOVE -> rv.parent.requestDisallowInterceptTouchEvent(true)
                }
                return false
            }

            override fun onTouchEvent(rv: RecyclerView, e: MotionEvent) {}
            override fun onRequestDisallowInterceptTouchEvent(disallowIntercept: Boolean) {}
        })

        questionsRecyclerView.addOnItemTouchListener(object : OnItemTouchListener {
            override fun onInterceptTouchEvent(rv: RecyclerView, e: MotionEvent): Boolean {
                when (e.action) {
                    MotionEvent.ACTION_MOVE -> rv.parent.requestDisallowInterceptTouchEvent(true)
                }
                return false
            }

            override fun onTouchEvent(rv: RecyclerView, e: MotionEvent) {}

            override fun onRequestDisallowInterceptTouchEvent(disallowIntercept: Boolean) {}
        })


        super.addListeners(binding.root)
        return root

    }


    private fun navigateToAssetActivity(){
        val detailActivity = (activity as? DetailActivity)
        detailActivity?.let {
            val assetsActivity = Intent(requireActivity(), AssetsActivity::class.java)
            val bList:ArrayList<Int> = arrayListOf()
            for(item in borrowerInfoList) {
                //Timber.d("borrowerInfoList borrowerId  "+ item)
                if(item.borrowerId!=-1)
                    bList.add(item.borrowerId)
            }
            assetsActivity.putIntegerArrayListExtra( AppConstant.assetBorrowerList, bList)
            it.loanApplicationId?.let { loanId ->
                assetsActivity.putExtra(AppConstant.loanApplicationId, loanId)
            }
            it.borrowerLoanPurpose?.let{ loanPurpose->
                assetsActivity.putExtra(AppConstant.loanPurpose, loanPurpose)
            }
            startActivity(assetsActivity)
        }
    }

    private fun navigateToIncomeActivity(){
        val detailActivity = (activity as? DetailActivity)
        detailActivity?.let {
            val incomeActivity = Intent(requireActivity(), IncomeActivity::class.java)
            val bList:ArrayList<Int> = arrayListOf()
            for(item in borrowerInfoList) {
                //Timber.d("borrowerInfoList borrowerId  "+ item)
                if(item.borrowerId!=-1)
                    bList.add(item.borrowerId)
            }
            incomeActivity.putIntegerArrayListExtra( AppConstant.incomeBorrowerList, bList)
            it.loanApplicationId?.let { loanId ->
                incomeActivity.putExtra(AppConstant.loanApplicationId, loanId)
            }
            it.borrowerLoanPurpose?.let{ loanPurpose->
                incomeActivity.putExtra(AppConstant.loanPurpose, loanPurpose)
            }
            startActivity(incomeActivity)
        }
    }


    override fun getSingleItemIndex(position: Int) {

    }

    override fun navigateTo(position: Int) {
        startActivity(Intent(requireActivity(), BorrowerAddressActivity::class.java))
    }

    override fun onRealEstateClick(position: Int) {
        val detailActivity = (activity as? DetailActivity)
        detailActivity?.let {
            val intent = Intent(requireActivity(), RealEstateActivity::class.java)
            //Log.e("loanAppID", ""+ it.loanApplicationId)
            //Log.e("purpose", ""+ it.)
            intent.putExtra(AppConstant.loanApplicationId, it.loanApplicationId)
            //intent.putExtra(AppConstant.borrowerPropertyId,it )
            startActivity(intent)
        }


    }

    override fun navigateToGovernmentActivity(position: Int) {
        //startActivity(Intent(requireActivity(), GovtQuestionActivity::class.java))
        val detailActivity = (activity as? DetailActivity)
        detailActivity?.let {
            val govtQuestionActivity = Intent(requireActivity(), GovtQuestionActivity::class.java)
            val bList:ArrayList<Int> = arrayListOf()
            val bListOwnTypeId:ArrayList<Int> = arrayListOf()
            for(item in borrowerInfoList) {
                //Timber.d("borrowerInfoList borrowerId  "+ item)
                if(item.borrowerId!=-1) {
                    Timber.e("passing borrowerId -- "+item.borrowerId)
                    Timber.e("passing owntypeId -- "+item.owntypeId)
                    bList.add(item.borrowerId)
                    bListOwnTypeId.add(item.owntypeId)
                }
            }
            govtQuestionActivity.putIntegerArrayListExtra( AppConstant.borrowerList, bList)
            govtQuestionActivity.putIntegerArrayListExtra( AppConstant.borrowerOwnTypeList, bListOwnTypeId)
            it.loanApplicationId?.let { loanId ->
                govtQuestionActivity.putExtra(AppConstant.loanApplicationId, loanId)
                Timber.e("loanApplicationId -- "+loanId)
            }

            startActivity(govtQuestionActivity)
        }




    }

    override fun onResume() {
        super.onResume()
        (activity as DetailActivity).binding.requestDocFab.visibility = View.GONE
    }



    /*
    private fun setUpGovtQuestionsRecycleView(passedList: ArrayList<BorrowerQuestionsModel>) {
        val simpleItemsList: ArrayList<GovtQuestionsHorizontal> = ArrayList()
        for (eachItem in passedList) {
            val simpleItem = GovtQuestionsHorizontal()
            simpleItem.questionTitle = eachItem.questionDetail?.questionHeader
            simpleItem.question = eachItem.questionDetail?.questionText
            eachItem.questionResponses?.let { answers->
               for(answer in answers){
                   if(answer.questionResponseText.equals("Yes", true))
                       simpleItem.answer1 = "- "+answer.borrowerFirstName
                   else
                   if(answer.questionResponseText.equals("No", true))
                        simpleItem.answer2 = "- "+answer.borrowerFirstName
                   else
                       simpleItem.answer3 = "- "+answer.borrowerFirstName
               }
            }
            simpleItemsList.add(simpleItem)
        }

        val itemAdapter = ItemAdapter<GovtQuestionsHorizontal>()
        val fastAdapter = FastAdapter.with(itemAdapter)
        govtQuestionsRecyclerView.adapter = fastAdapter
        itemAdapter.add(simpleItemsList)

        val snapHelper = GravitySnapHelper(Gravity.START)
        snapHelper.attachToRecyclerView(govtQuestionsRecyclerView)

        govtQuestionsRecyclerView.layoutManager =
            LinearLayoutManager(activity, LinearLayoutManager.HORIZONTAL, false)
        fastAdapter.notifyAdapterDataSetChanged()

        fastAdapter.addEventHook(object : ClickEventHook<GovtQuestionsHorizontal>() {
            override fun onBind(viewHolder: RecyclerView.ViewHolder): View? {
                //return the views on which you want to bind this event
                return if (viewHolder is GovtQuestionsHorizontal.ViewHolder) {
                    //Log.e("viewHolder", viewHolder.toString())
                    viewHolder.itemView

                } else
                    null
            }

            override fun onClick(v: View, position: Int, fastAdapter: FastAdapter<GovtQuestionsHorizontal>, item: GovtQuestionsHorizontal) {}


        })
    }

     */

}


