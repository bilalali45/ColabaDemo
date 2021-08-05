package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.*
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import androidx.recyclerview.widget.RecyclerView.OnItemTouchListener
import com.github.rubensousa.gravitysnaphelper.GravitySnapHelper
import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.adapters.ItemAdapter
import com.mikepenz.fastadapter.listeners.ClickEventHook
import com.rnsoft.colabademo.databinding.DetailApplicationTabBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject
import kotlin.math.roundToInt


@AndroidEntryPoint
class BorrowerApplicationFragment : Fragment() {

    private var _binding: DetailApplicationTabBinding? = null
    private val binding get() = _binding!!

    private lateinit var horizontalRecyclerView: RecyclerView
    private lateinit var realStateRecyclerView: RecyclerView
    private lateinit var govtQuestionsRecyclerView: RecyclerView

    private val detailViewModel: DetailViewModel by activityViewModels()

    private var borrowerInfoList: ArrayList<BorrowersInformation> = ArrayList()
    private var realStateList: ArrayList<RealStateOwn> = ArrayList()
    private var questionList: ArrayList<BorrowerQuestionsModel> = ArrayList()

    private var borrowerInfoAdapter  = CustomBorrowerAdapter(borrowerInfoList)
    private var realStateAdapter  = RealStateAdapter(realStateList)

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = DetailApplicationTabBinding.inflate(inflater, container, false)
        val root: View = binding.root

        horizontalRecyclerView = root.findViewById(R.id.horizontalRecycleView)
        realStateRecyclerView = root.findViewById(R.id.realStateHorizontalRecyclerView)
        govtQuestionsRecyclerView = root.findViewById(R.id.govtQuestionHorizontalRecyclerView)


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


       detailViewModel.borrowerApplicationTabModel.observe(viewLifecycleOwner, { appTabModel->
            if (appTabModel != null) {
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
                    binding.bAppLoanPayment.text = AppSetting.returnAmountFormattedString(it)
                }

                appTabModel.borrowerAppData?.loanInformation?.downPayment?.let{
                    binding.bAppDownPayment.text = AppSetting.returnAmountFormattedString(it)
                }

                appTabModel.borrowerAppData?.loanInformation?.downPaymentPercent?.let {
                    binding.bAppPercentage.text = "("+it.roundToInt()+"%)"
                }

                appTabModel.borrowerAppData?.assetAndIncome?.totalAsset?.let{
                    binding.bAppTotalAssets.text =AppSetting.returnAmountFormattedString(it)
                }

                appTabModel.borrowerAppData?.assetAndIncome?.totalMonthyIncome?.let{
                    binding.bAppMonthlylncome.text =AppSetting.returnAmountFormattedString(it)
                }



                appTabModel.borrowerAppData?.let { bAppData->
                    bAppData.borrowersInformation?.let {
                       borrowerInfoList.clear()
                       borrowerInfoList = it

                   }
                }

                appTabModel.borrowerAppData?.let { bAppData->
                    bAppData.borrowerQuestionsModel?.let {
                        questionList.clear()
                        questionList = it
                        setUpGovtQuestionsRecycleView(questionList)
                    }
                }

                appTabModel.borrowerAppData?.let { bAppData->
                    bAppData.realStateOwns?.let {
                        realStateList.clear()
                        realStateList = it

                    }
                }




                //////////////////////////////////////////////////////////////////////////////////////////////////
                // add add-more last cell to the adapters
                borrowerInfoList.add(BorrowersInformation(0,"","","",0, true))
                borrowerInfoAdapter  = CustomBorrowerAdapter(borrowerInfoList)
                horizontalRecyclerView.adapter = borrowerInfoAdapter
                borrowerInfoAdapter.notifyDataSetChanged()


                realStateList.add(RealStateOwn(null,0,0,0,"", true))
                realStateAdapter  = RealStateAdapter(realStateList)
                realStateRecyclerView.adapter = realStateAdapter
                realStateAdapter.notifyDataSetChanged()

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

        govtQuestionsRecyclerView.addOnItemTouchListener(object : OnItemTouchListener {
            override fun onInterceptTouchEvent(rv: RecyclerView, e: MotionEvent): Boolean {
                when (e.action) {
                    MotionEvent.ACTION_MOVE -> rv.parent.requestDisallowInterceptTouchEvent(true)
                }
                return false
            }

            override fun onTouchEvent(rv: RecyclerView, e: MotionEvent) {}
            override fun onRequestDisallowInterceptTouchEvent(disallowIntercept: Boolean) {}
        })

        return root

    }




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
                    Log.e("viewHolder", viewHolder.toString())
                    viewHolder.itemView

                } else
                    null
            }

            override fun onClick(v: View, position: Int, fastAdapter: FastAdapter<GovtQuestionsHorizontal>, item: GovtQuestionsHorizontal) {}


        })
    }

}


