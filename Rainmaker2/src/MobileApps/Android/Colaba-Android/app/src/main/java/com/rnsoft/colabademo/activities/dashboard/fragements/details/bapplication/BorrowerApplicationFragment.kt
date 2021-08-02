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

        val localList: ArrayList<TabBorrowerList> = ArrayList()
        localList.add(TabBorrowerList(0, "Richard Glenn Randall", "Co Borrower"))
        localList.add(TabBorrowerList(1, "Maria Randall", "Co-Borrower"))
        localList.add(TabBorrowerList(2, "Test-b", "Add Borrowers", true))
        //setUpBorrowerHorizontalFastAdapter(localList)

        val linearLayoutManager = LinearLayoutManager(activity , LinearLayoutManager.HORIZONTAL, false)
        val customBorrowerAdapter  = CustomBorrowerAdapter(localList)
        horizontalRecyclerView.apply {
            this.layoutManager =linearLayoutManager
            this.setHasFixedSize(true)
            this.adapter = customBorrowerAdapter
        }
        customBorrowerAdapter.notifyDataSetChanged()



        val realStateList: ArrayList<TabRealStateList> = ArrayList()
        realStateList.add(TabRealStateList(0, "5919 Trussville Crossings\nParkways,", "Land"))
        realStateList.add(TabRealStateList(1, "727 Ashleigh Lane,\n" + "South Lake TX, 76092", "Single Family Property"))
        realStateList.add(TabRealStateList(2, "5919 Trussville Crossings\nParkways,", "Land"))
        realStateList.add(TabRealStateList(3, "5919 Trussville Crossings\nParkways,", "Land", true))
        //setUpRealStateRecycleView(realStateList)

        val realStateLayoutManager = LinearLayoutManager(activity , LinearLayoutManager.HORIZONTAL, false)
        val realStateAdapter  = RealStateAdapter(realStateList)
        realStateRecyclerView.apply {
            this.layoutManager =realStateLayoutManager
            this.setHasFixedSize(true)
            this.adapter = realStateAdapter
        }
        realStateAdapter.notifyDataSetChanged()


        val questionList: ArrayList<TabGovtQuestionList> = ArrayList()
        questionList.add(TabGovtQuestionList(0, "Undisclosed Borrowed Funds", "Are you borrowing any money for this real estate transaction (e.g., money for your ..."))
        questionList.add(TabGovtQuestionList(1, "Ownership Interest in Property", "Have you had an ownership interest in another property in the last three years?"))
        questionList.add(TabGovtQuestionList(2, "Priority Liens", "Are you currently the delinquent or in the default on a Federal debt list?"))
        questionList.add(TabGovtQuestionList(3, "Undisclosed Mortgage Applications", "Have you had an ownership interest in another property in the last three years?"))
        questionList.add(TabGovtQuestionList(4, "Debt Co-signer or Guarantor", "Are you a co-signer or guarantor on any debt or loan that is not disclosed on this application..."))
        setUpGovtQuestionsRecycleView(questionList)

        detailViewModel.borrowerApplicationTabModel.observe(viewLifecycleOwner, { appTabModel->
            if (appTabModel != null) {
                appTabModel.borrowerAppData?.subjectProperty?.borrowerAddress?.let {
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


            }
        })

        return root

    }

    private fun  setUpBorrowerList(localList: ArrayList<TabBorrowerList>) {

    }

    private fun setUpBorrowerHorizontalFastAdapter(localTabBorrowerList: ArrayList<TabBorrowerList>) {
        val simpleItemsList: ArrayList<BorrowerHorizontal> = ArrayList()

        for (eachItem in localTabBorrowerList) {
            val simpleItem = BorrowerHorizontal(false)
            simpleItem.name = eachItem.name
            simpleItem.coName = eachItem.coName
            simpleItemsList.add(simpleItem)
        }
        //simpleItemsList.add(BorrowerHorizontal(true))
        Log.e("simpleItemsList", simpleItemsList.size.toString())

        //val simpleFooterList: ArrayList<BorrowerHorizontal> = ArrayList()
        //simpleFooterList.add(BorrowerHorizontal())



        //create the ItemAdapter holding your Items
        val itemAdapter  = ItemAdapter<BorrowerHorizontal>()

        //create last item holder your Items
        //val headerAdapter = ItemAdapter<BorrowerHorizontal>()


       // val headerAdapter: HeaderViewListAdapter<FooterHorizontal> = HeaderViewListAdapter()

        //val itemAdapterGeneric = GenericItemAdapter()
        //val genericItemAdapter = GenericItemAdapter()

        //val footerAdapter = ItemAdapter<FooterHorizontal>()

        //create the managing FastAdapter, by passing in the itemAdapter
        //val fastAdapter :FastAdapter<GenericItem>  = FastAdapter.with(listOf(headerAdapter , itemAdapter ))
        val fastAdapter  = FastAdapter.with(itemAdapter)

        //val fastAdapterTwo = FastAdapter.with<GenericItem, ItemAdapter<*>>(listOf(itemAdapter, headerAdapter))

        //set our adapters to the RecyclerView
        //horizontalRecyclerView.setAdapter(fastAdapter)
        //horizontalRecyclerView.itemAnimator = DefaultItemAnimator()
        horizontalRecyclerView.adapter =    fastAdapter





        itemAdapter.add(simpleItemsList)

        //headerAdapter.add(simpleItemsList)

        //set the items to your ItemAdapter
        //horizontalRecyclerView.itemAnimator = DefaultItemAnimator()
        // horizontalRecyclerView.adapter = fastAdapter
        //snapHelper.attachToRecyclerView(horizontalRecyclerView)
        val snapHelper = GravitySnapHelper(Gravity.START)
        snapHelper.attachToRecyclerView(horizontalRecyclerView)

        horizontalRecyclerView.layoutManager =
            LinearLayoutManager(activity, LinearLayoutManager.HORIZONTAL, false)
        fastAdapter.notifyAdapterDataSetChanged()

        /*
        fastAdapter.addEventHook(object : ClickEventHook<GenericItem>() {
            override fun onBind(viewHolder: RecyclerView.ViewHolder): View? {
                //return the views on which you want to bind this event
                return if (viewHolder is GenericItem) {
                    Log.e("viewHolder", viewHolder.toString())
                    viewHolder.itemView

                } else {
                    null
                }
            }

            override fun onClick(
                v: View,
                position: Int,
                fastAdapter: FastAdapter<GenericItem>,
                item: GenericItem
            ) {
                //react on the click event
                //productActivityViewModel.loadItem(itemList[position])
                // findNavController().navigate(R.id.detail_fragment, null)
            }
        })

         */


    }


    private fun setUpRealStateRecycleView(passedList: ArrayList<TabRealStateList>) {
        val simpleItemsList: ArrayList<RealStateHorizontal> = ArrayList()

        for (eachItem in passedList) {
            val simpleItem = RealStateHorizontal()
            simpleItem.propertyAddress = eachItem.propertyAddress
            simpleItem.propertyType = eachItem.propertyType
            simpleItemsList.add(simpleItem)
        }
        Log.e("simpleItemsList", simpleItemsList.size.toString())

        //create the ItemAdapter holding your Items
        val itemAdapter = ItemAdapter<RealStateHorizontal>()

        //create the managing FastAdapter, by passing in the itemAdapter
        val fastAdapter = FastAdapter.with(itemAdapter)

        //set our adapters to the RecyclerView
        //horizontalRecyclerView.setAdapter(fastAdapter)
        //horizontalRecyclerView.itemAnimator = DefaultItemAnimator()
        realStateRecyclerView.adapter = fastAdapter



        itemAdapter.add(simpleItemsList)

        //set the items to your ItemAdapter
        //horizontalRecyclerView.itemAnimator = DefaultItemAnimator()
        // horizontalRecyclerView.adapter = fastAdapter
        //snapHelper.attachToRecyclerView(horizontalRecyclerView)
        val snapHelper = GravitySnapHelper(Gravity.START)
        snapHelper.attachToRecyclerView(realStateRecyclerView)

        realStateRecyclerView.layoutManager =
            LinearLayoutManager(activity, LinearLayoutManager.HORIZONTAL, false)
        fastAdapter.notifyAdapterDataSetChanged()

        fastAdapter.addEventHook(object : ClickEventHook<RealStateHorizontal>() {
            override fun onBind(viewHolder: RecyclerView.ViewHolder): View? {
                //return the views on which you want to bind this event
                return if (viewHolder is RealStateHorizontal.ViewHolder) {
                    Log.e("viewHolder", viewHolder.toString())
                    viewHolder.itemView

                } else {
                    null
                }
            }

            override fun onClick(
                v: View,
                position: Int,
                fastAdapter: FastAdapter<RealStateHorizontal>,
                item: RealStateHorizontal
            ) {
                //react on the click event
                //productActivityViewModel.loadItem(itemList[position])
                // findNavController().navigate(R.id.detail_fragment, null)
            }
        })
    }


    private fun setUpGovtQuestionsRecycleView(passedList: ArrayList<TabGovtQuestionList>) {
        val simpleItemsList: ArrayList<GovtQuestionsHorizontal> = ArrayList()
        for (eachItem in passedList) {
            val simpleItem = GovtQuestionsHorizontal()
            simpleItem.questionTitle = eachItem.questionTitle
            simpleItem.question = eachItem.question
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


