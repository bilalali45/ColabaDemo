package com.rnsoft.colabademo

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.rnsoft.colabademo.activities.assets.model.MyAssetBorrowerDataClass
import com.rnsoft.colabademo.activities.model.StatesModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.*
import org.greenrobot.eventbus.EventBus
import timber.log.Timber
import javax.inject.Inject
import kotlin.coroutines.CoroutineContext

@HiltViewModel
class BorrowerApplicationViewModel @Inject constructor(private val bAppRepo: BorrowerApplicationRepo) : ViewModel() {

    private var _assetsModelDataClass : MutableLiveData<ArrayList<MyAssetBorrowerDataClass>> =   MutableLiveData()
    val assetsModelDataClass: LiveData<ArrayList<MyAssetBorrowerDataClass>> get() = _assetsModelDataClass

    private var _incomeDetails : MutableLiveData<ArrayList<IncomeDetailsResponse>> =   MutableLiveData()
    val incomeDetails: LiveData<ArrayList<IncomeDetailsResponse>> get() = _incomeDetails

    private var _governmentQuestionsModelClass : MutableLiveData<GovernmentQuestionsModelClass> =   MutableLiveData()
    val governmentQuestionsModelClass: LiveData<GovernmentQuestionsModelClass> get() = _governmentQuestionsModelClass

    private val _propertyType: MutableLiveData<ArrayList<DropDownResponse>> = MutableLiveData()
    val propertyType: LiveData<ArrayList<DropDownResponse>> get() = _propertyType

    private val _occupancyType: MutableLiveData<ArrayList<DropDownResponse>> = MutableLiveData()
    val occupancyType: LiveData<ArrayList<DropDownResponse>> get() = _occupancyType

    private val _countries: MutableLiveData<ArrayList<CountriesModel>> = MutableLiveData()
    val countries: LiveData<ArrayList<CountriesModel>> get() = _countries

    private val _counties: MutableLiveData<ArrayList<CountiesModel>> = MutableLiveData()
    val counties: LiveData<ArrayList<CountiesModel>> get() = _counties

    private val _states: MutableLiveData<ArrayList<StatesModel>> = MutableLiveData()
    val states: LiveData<ArrayList<StatesModel>> get() = _states

    private val _subjectPropertyDetails: MutableLiveData<SubjectPropertyDetails> = MutableLiveData()
    val subjectPropertyDetails: LiveData<SubjectPropertyDetails> get() = _subjectPropertyDetails

    private val _refinanceDetails: MutableLiveData<SubjectPropertyRefinanceDetails> =
        MutableLiveData()
    val refinanceDetails: LiveData<SubjectPropertyRefinanceDetails> get() = _refinanceDetails

    private val _coBorrowerOccupancyStatus: MutableLiveData<CoBorrowerOccupancyStatus> =
        MutableLiveData()
    val coBorrowerOccupancyStatus: LiveData<CoBorrowerOccupancyStatus> get() = _coBorrowerOccupancyStatus


    suspend fun getBorrowerAssetsDetail(token:String, loanApplicationId:Int, borrowerId: ArrayList<Int>?): Boolean {

        /*
        viewModelScope.launch (Dispatchers.IO) {
            val responseResult = bAppRepo.getBorrowerAssetsDetail(token = token, loanApplicationId = loanApplicationId , borrowerId = borrowerId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _assetsModelDataClass.value = ((responseResult as Result.Success<AssetsModelDataClass>).data)
                else if (responseResult is Result.Error && (responseResult as Result.Error).exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult as Result.Error))
            }
        }

         */

        return true

    }

    private val job = Job()
    val coroutineContext: CoroutineContext
        get() = job + Dispatchers.Main

    suspend fun getBorrowerAssetsDetail2(token:String, loanApplicationId:Int , borrowerIds:ArrayList<Int>): Boolean {
        /*
        viewModelScope.launch(Dispatchers.IO) {
            withContext(Dispatchers.Default) {
                //val content = arrayListOf<Int>()
                val content = arrayListOf<Result<AssetsModelDataClass>>()
                coroutineScope {
                    borrowerIds.forEach { id ->
                        launch { // this will allow us to run multiple tasks in parallel
                            val responseResult = bAppRepo.getBorrowerAssetsDetail(token = token, loanApplicationId = loanApplicationId , borrowerId = id)
                            if (responseResult is Result.Success) {

                            }
                        }
                    }
                }  // coroutineScope block will wait here until all child tasks are completed



                val runningTasks = borrowerIds.map { id ->
                    async { // this will allow us to run multiple tasks in parallel
                        val responseResult = bAppRepo.getBorrowerAssetsDetail(token = token, loanApplicationId = loanApplicationId , borrowerId = id)
                        if (responseResult is Result.Success) {

                        }
                    }
                }

                val responses = runningTasks.awaitAll()



                responses.forEach { (id, response) ->
                    if (response.isSuccessful()) {
                       // content.find { it.id == id }.enable = true
                    }
                }


            }


        }

         */

        return true
    }


    suspend fun getBorrowerWithAssets(token:String, loanApplicationId:Int , borrowerIds:ArrayList<Int>) {
        var errorResult:Result.Error?=null
        val borrowerAssetList: ArrayList<MyAssetBorrowerDataClass> = ArrayList()
        viewModelScope.launch(Dispatchers.IO) {
            coroutineScope {
                delay(4000)
                borrowerIds.forEach { id ->
                    Timber.e("borrowerIds.id -> "+id)
                    launch { // this will allow us to run multiple tasks in parallel
                        val responseResult = bAppRepo.getBorrowerAssetsDetail(
                            token = token,
                            loanApplicationId = loanApplicationId,
                            borrowerId = id
                        )
                        if (responseResult is Result.Success) {
                            responseResult.data.passedBorrowerId = id
                            Timber.e("borrowerIds.data.passedBorrowerId -> "+responseResult.data.passedBorrowerId)
                            borrowerAssetList.add(responseResult.data)
                        }
                        else if (responseResult is Result.Error)
                            errorResult = responseResult
                    }
                }
            }  // coroutineScope block will wait here until all child tasks are completed
            withContext(Dispatchers.Main) {
                _assetsModelDataClass.value = borrowerAssetList
            }
            if(errorResult!=null) // if service not working.....
                EventBus.getDefault().post(WebServiceErrorEvent(errorResult, false))
            else
            if(borrowerAssetList.size == 0) // service working without error but no results....
                EventBus.getDefault().post(WebServiceErrorEvent(null, false))
        }
    }

    suspend fun getBorrowerWithIncome(token:String, loanApplicationId:Int , borrowerIds:ArrayList<Int>) {
        val borrowerIncomeList: ArrayList<IncomeDetailsResponse> = ArrayList()
        var errorResult:Result.Error?=null
        viewModelScope.launch(Dispatchers.IO) {
            coroutineScope {
                delay(2000)
                borrowerIds.forEach { id ->
                    launch {
                        val responseResult = bAppRepo.getBorrowerIncomeDetail(token = token, loanApplicationId = loanApplicationId, borrowerId = id)
                        if (responseResult is Result.Success){
                            Log.e("viewmodel-income","success")
                            responseResult.data.passedBorrowerId = id
                            Timber.e("borrowerIds.data.passedBorrowerId -> "+responseResult.data.incomeData)
                            borrowerIncomeList.add(responseResult.data)
                        }
                    }
                }
            }  // coroutineScope block will wait here until all child tasks are completed
            withContext(Dispatchers.Main) {
                _incomeDetails.value = borrowerIncomeList
            }
            if(errorResult!=null) // if service not working.....
                EventBus.getDefault().post(WebServiceErrorEvent(errorResult, false))
            else
                if(borrowerIncomeList.size == 0) // service working without error but no results....
                    EventBus.getDefault().post(WebServiceErrorEvent(null, false))
        }
    }

    fun resetAssetModelClass(){
        _assetsModelDataClass  =   MutableLiveData()
    }

    fun resetIncomeModelClass(){
        _incomeDetails  =   MutableLiveData()
    }

    suspend fun getGovernmentQuestions(token:String, loanApplicationId:Int, ownTypeId:Int, borrowerId:Int ): Boolean {
        var bool = false
        viewModelScope.launch (Dispatchers.IO) {
            //delay(2000)
            val responseResult = bAppRepo.getGovernmentQuestions(token = token, loanApplicationId = loanApplicationId , ownTypeId = ownTypeId, borrowerId = borrowerId)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                     bool = true
                    _governmentQuestionsModelClass.value = responseResult.data
                }
                else if (responseResult is Result.Error && (responseResult as Result.Error).exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult as Result.Error))
            }
        }
        return bool
    }


    suspend fun getCoBorrowerOccupancyStatus(token: String, loanApplicationId: Int) {
        //Timber.e("CoBorrower: " + loanApplicationId + "Auth Token: " + token)
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = bAppRepo.getCoBorrowerOccupancyStatus(
                token = token,
                loanApplicationId = loanApplicationId
            )
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _coBorrowerOccupancyStatus.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getSubjectPropertyDetails(token: String, loanApplicationId: Int) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = bAppRepo.getSubjectPropertyDetails(
                token = token,
                loanApplicationId = loanApplicationId
            )
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _subjectPropertyDetails.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getRefinanceDetails(token: String, loanApplicationId: Int) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = bAppRepo.getSubjectPropertyRefinance(
                token = token,
                loanApplicationId = loanApplicationId
            )
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                    _refinanceDetails.value = (responseResult.data)
                } else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }

    suspend fun getPropertyTypes(token: String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = bAppRepo.getPropertyType(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _propertyType.value = (responseResult.data)
                /*else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                        EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                     else if (responseResult is Result.Error)
                        EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                    */

            }
        }
    }

    suspend fun getOccupancyType(token: String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = bAppRepo.getOccupancyType(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _occupancyType.value = (responseResult.data)
                /*else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
               EventBus.getDefault().post(WebServiceErrorEvent(null, true))
            else if (responseResult is Result.Error)
               EventBus.getDefault().post(WebServiceErrorEvent(responseResult))

             */

            }
        }
    }

    suspend fun getStates(token: String) {
        viewModelScope.launch(Dispatchers.IO) {
            val response = bAppRepo.getStates(token = token)
            withContext(Dispatchers.Main) {
                if (response is Result.Success)
                    _states.value = (response.data)
                else if (response is Result.Error && response.exception.message == AppConstant.INTERNET_ERR_MSG) {
                    //Timber.e(" GetStates " + response.toString())
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                } else if (response is Result.Error) {
                    //Timber.e(" GetStates " + response.toString())
                    EventBus.getDefault().post(WebServiceErrorEvent(response))
                }
            }
        }
    }

    suspend fun getCounty(token: String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = bAppRepo.getCounties(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success) {
                    //Timber.e("Counties-Success")
                    _counties.value = (responseResult.data)
                } else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error) {
                    //Timber.e(" Counties-Error " + responseResult.toString())
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
                }
            }
        }
    }

    suspend fun getCountries(token: String) {
        viewModelScope.launch(Dispatchers.IO) {
            val responseResult = bAppRepo.getCountries(token = token)
            withContext(Dispatchers.Main) {
                if (responseResult is Result.Success)
                    _countries.value = (responseResult.data)
                else if (responseResult is Result.Error && responseResult.exception.message == AppConstant.INTERNET_ERR_MSG)
                    EventBus.getDefault().post(WebServiceErrorEvent(null, true))
                else if (responseResult is Result.Error)
                    EventBus.getDefault().post(WebServiceErrorEvent(responseResult))
            }
        }
    }


}