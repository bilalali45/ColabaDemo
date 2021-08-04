package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.Gravity
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.github.rubensousa.gravitysnaphelper.GravitySnapHelper
import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.adapters.ItemAdapter
import com.mikepenz.fastadapter.listeners.ClickEventHook
import com.rnsoft.colabademo.databinding.DetailApplicationTabBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


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


        //borrowerInfoList.add(TabBorrowerList(0, "Richard Glenn Randall", "Co Borrower"))
       // borrowerInfoList.add(TabBorrowerList(1, "Maria Randall", "Co-Borrower"))
       // borrowerInfoList.add(TabBorrowerList(2, "Test-b", "Add Borrowers", true))
        //setUpBorrowerHorizontalFastAdapter(localList)

        val linearLayoutManager = LinearLayoutManager(activity , LinearLayoutManager.HORIZONTAL, false)

        horizontalRecyclerView.apply {
            this.layoutManager =linearLayoutManager
            //this.setHasFixedSize(true)
            this.adapter = borrowerInfoAdapter
        }
        //borrowerInfoAdapter.notifyDataSetChanged()




        //realStateList.add(TabRealStateList(0, "5919 Trussville Crossings\nParkways,", "Land"))
        //realStateList.add(TabRealStateList(1, "727 Ashleigh Lane,\n" + "South Lake TX, 76092", "Single Family Property"))
        //realStateList.add(TabRealStateList(2, "5919 Trussville Crossings\nParkways,", "Land"))
        //realStateList.add(TabRealStateList(3, "5919 Trussville Crossings\nParkways,", "Land", true))
        //setUpRealStateRecycleView(realStateList)

        val realStateLayoutManager = LinearLayoutManager(activity , LinearLayoutManager.HORIZONTAL, false)

        realStateRecyclerView.apply {
            this.layoutManager =realStateLayoutManager
            //this.setHasFixedSize(true)
            this.adapter = realStateAdapter
        }
        realStateAdapter.notifyDataSetChanged()



        //questionList.add(TabGovtQuestionList(0, "Undisclosed Borrowed Funds", "Are you borrowing any money for this real estate transaction (e.g., money for your ..."))
        //questionList.add(TabGovtQuestionList(1, "Ownership Interest in Property", "Have you had an ownership interest in another property in the last three years?"))
        //questionList.add(TabGovtQuestionList(2, "Priority Liens", "Are you currently the delinquent or in the default on a Federal debt list?"))
        //questionList.add(TabGovtQuestionList(3, "Undisclosed Mortgage Applications", "Have you had an ownership interest in another property in the last three years?"))
       // questionList.add(TabGovtQuestionList(4, "Debt Co-signer or Guarantor", "Are you a co-signer or guarantor on any debt or loan that is not disclosed on this application..."))
        //setUpGovtQuestionsRecycleView(questionList)

        detailViewModel.borrowerApplicationTabModel.observe(viewLifecycleOwner, { appTabModel->
            if (appTabModel != null) {
                appTabModel.borrowerAppData?.subjectProperty?.subjectPropertyAddress?.let {
                    binding.bAppAddress.text = it.street+" "+it.unit+"\n"+it.city+" "+it.stateName+" "+it.zipCode+" "+it.countryName
                }



                appTabModel.borrowerAppData?.subjectProperty?.propertyTypeName?.let{
                    binding.bAppPropertyType.text = it
                }

                appTabModel.borrowerAppData?.subjectProperty?.propertyUsageName.let{
                    binding.bAppPropertyUsage.text = it
                }

                appTabModel.borrowerAppData?.loanInformation?.loanAmount?.let{
                    binding.bAppLoanPayment.text = it.toString()
                }

                binding.bAppPercentage.text = "("+30.toString()+"%)"

                binding.bAppTotalAssets.text =  appTabModel.borrowerAppData?.assetAndIncome?.totalAsset.toString()

                binding.bAppMonthlylncome.text = appTabModel.borrowerAppData?.assetAndIncome?.totalAsset.toString()



                appTabModel.borrowerAppData?.let { bAppData->
                    bAppData.borrowersInformation?.let {
                       borrowerInfoList.clear()
                       borrowerInfoList = it
                       borrowerInfoList.add(BorrowersInformation(0,"","","",0, true))
                       borrowerInfoAdapter  = CustomBorrowerAdapter(borrowerInfoList)
                       horizontalRecyclerView.adapter = borrowerInfoAdapter

                       borrowerInfoAdapter.notifyDataSetChanged()
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
                        realStateList.add(RealStateOwn(null,0,0,0,"", true))
                        realStateAdapter  = RealStateAdapter(realStateList)
                        realStateRecyclerView.adapter = realStateAdapter
                        realStateAdapter.notifyDataSetChanged()
                    }
                }



            }
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


