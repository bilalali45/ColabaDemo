package com.rnsoft.colabademo

import android.annotation.SuppressLint
import android.content.Intent
import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.rnsoft.colabademo.activities.startapplication.adapter.ContactsAdapter
import com.rnsoft.colabademo.utils.CustomMaterialFields
import kotlinx.android.synthetic.main.dependent_input_field.view.*
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*
import java.util.regex.Pattern
import androidx.activity.addCallback
import androidx.core.widget.doAfterTextChanged
import androidx.core.widget.doOnTextChanged
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.bumptech.glide.Glide
import com.rnsoft.colabademo.databinding.StartApplicationFragLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import timber.log.Timber
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

@AndroidEntryPoint
class StartNewApplicationFragment : BaseFragment(), RecyclerviewClickListener {

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    private lateinit var binding: StartApplicationFragLayoutBinding
    private var savedViewInstance: View? = null
    private lateinit var adapter : ContactsAdapter
    private var searchList = ArrayList<SearchResultResponseItem>()
    private val viewModel: StartNewAppViewModel by activityViewModels()

    private val createNewApplicationParams = CreateNewApplicationParams()

    private var isFindContactSelected = false

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return if (savedViewInstance != null) {
            savedViewInstance
        } else {
            binding = StartApplicationFragLayoutBinding.inflate(inflater, container, false)
            savedViewInstance = binding.root
            binding.btnCreateApplication.isEnabled = false
            setupUI()
            setLabelFocus()
            super.addListeners(binding.root)
            savedViewInstance
        }
    }

    @SuppressLint("ClickableViewAccessibility", "UseCompatLoadingForDrawables")
    private fun setupUI() {
        searchList.add(SearchResultResponseItem( 1,"richard.glenn@gmail.com","Richard Glenn Randall", mobileNumber =   "(121) 353 1343"))
        searchList.add(SearchResultResponseItem(2,"arnold634@gmail.com", "Arnold Richard", mobileNumber = "(121) 353 1343"))
        searchList.add(SearchResultResponseItem(3,"richard.glenn@gmail.com", "Richard Glenn Randall", mobileNumber = "(121) 353 1343"))

        adapter = ContactsAdapter(requireActivity(), this@StartNewApplicationFragment)
        binding.recyclerviewContacts.setHasFixedSize(true)

        binding.recyclerviewContacts.setOnTouchListener { _, _ -> //binding.scrollviewStartApplication.requestDisallowInterceptTouchEvent(true)
            //binding.scrollviewStartApplication.setOnTouchListener(disableScrollViewListener)
            binding.scrollviewStartApplication.setEnableScrolling(false) //
            //binding.recyclerviewContacts.isEnabled = false
            false
        }

        binding.parentLayout.setOnTouchListener { _, _ ->
            binding.scrollviewStartApplication.setEnableScrolling(true)
            false
        }

        binding.findContactBtn.setOnClickListener {
            isFindContactSelected = true
            findOrCreateContactClick()
            if(!binding.findContactBtn.isChecked) {
                createNewApplicationParams.let {
                    it.MobileNumber = null
                    it.EmailAddress = null
                    it.contactId = null
                    it.FirstName = null
                    it.LastName = null
                }
                viewModel.setCreateNewParams(createNewApplicationParams)
            }
        }

        binding.createContactBtn.setOnClickListener {
            isFindContactSelected = false
            findOrCreateContactClick()

            if (binding.edFirstName.text.isNullOrBlank() || binding.edFirstName.text.isNullOrEmpty())
                createNewApplicationParams.FirstName = null
            else
                createNewApplicationParams.FirstName = binding.edFirstName.text.toString()

            if (binding.edLastName.text.isNullOrBlank() || binding.edLastName.text.isNullOrEmpty())
                createNewApplicationParams.LastName = null
            else
                createNewApplicationParams.LastName = binding.edLastName.text.toString()

            if (binding.edMobile.text.isNullOrBlank() || binding.edMobile.text.isNullOrEmpty())
                createNewApplicationParams.MobileNumber = null
            else
                createNewApplicationParams.MobileNumber = binding.edMobile.text.toString()

            if (binding.edEmail.text.isNullOrBlank() || binding.edEmail.text.isNullOrEmpty())
                createNewApplicationParams.EmailAddress = null
            else
                createNewApplicationParams.EmailAddress = binding.edEmail.text.toString()

            viewModel.setCreateNewParams(createNewApplicationParams)
        }

        binding.rgLoanPurpose.addOnButtonCheckedListener { toggleButton, checkedId, isChecked ->
            if(isChecked) toggleButton.isEnabled
            Timber.e(" toggleButton, checkedId, isChecked ",toggleButton.toString(), checkedId.toString(), isChecked.toString())

            if (binding.btnLoanPurchase.isPressed) {
                createNewApplicationParams.let {
                    it.LoanPurpose = 1
                    it.LoanGoal = null
                }
                viewModel.setCreateNewParams(createNewApplicationParams)
                binding.btnPropertyUnderCont.setTypeface(null, Typeface.NORMAL)
                binding.btnPropertyUnderCont.isChecked = false
                binding.btnPreApproval.setTypeface(null, Typeface.NORMAL)
                binding.btnPreApproval.isChecked = false
            }
            else {
                preSelectInitialization()
                createNewApplicationParams.let {
                    it.LoanPurpose = 2
                    it.LoanGoal = null
                }
                viewModel.setCreateNewParams(createNewApplicationParams)
            }
            onLoanPurposeClick()
        }


        /*
        binding.btnLoanPurchase.setOnClickListener {
            binding.btnLoanRefinance.isSelected = false
            binding.btnLoanRefinance.isClickable = true
            binding.btnLoanPurchase.isClickable = false
            onLoanPurposeClick()
        }
        binding.btnLoanRefinance.setOnClickListener {
            binding.btnLoanPurchase.isSelected = false
            binding.btnLoanPurchase.isClickable = true
            binding.btnLoanRefinance.isClickable = false
            onLoanPurposeClick()
        }
         */

        binding.btnLowerPaymentTerms.setOnClickListener {
            preSelectInitialization()
            createNewApplicationParams.LoanGoal = 5
            viewModel.setCreateNewParams(createNewApplicationParams)
            binding.btnLowerPaymentTerms.isChecked = true
            binding.btnLowerPaymentTerms.setTypeface(null, Typeface.BOLD)
        }

        binding.btnCashout.setOnClickListener {
            preSelectInitialization()
            createNewApplicationParams.LoanGoal = 6
            viewModel.setCreateNewParams(createNewApplicationParams)
            binding.btnCashout.isChecked = true
            binding.btnCashout.setTypeface(null, Typeface.BOLD)
        }

        binding.btnDebtConsolidation.setOnClickListener {
            preSelectInitialization()
            createNewApplicationParams.LoanGoal = 7
            viewModel.setCreateNewParams(createNewApplicationParams)
            binding.btnDebtConsolidation.isChecked = true
            binding.btnDebtConsolidation.setTypeface(null, Typeface.BOLD)
        }

        binding.btnPreApproval.setOnClickListener {
            createNewApplicationParams.LoanGoal = 3
            viewModel.setCreateNewParams(createNewApplicationParams)
            binding.btnPreApproval.setTypeface(null, Typeface.BOLD)
            binding.btnPropertyUnderCont.setTypeface(null, Typeface.NORMAL)
            binding.btnPropertyUnderCont.isChecked = false
        }

        binding.btnPropertyUnderCont.setOnClickListener{
            createNewApplicationParams.LoanGoal = 4
            viewModel.setCreateNewParams(createNewApplicationParams)
            
            binding.btnPreApproval.setTypeface(null, Typeface.NORMAL)
            binding.btnPropertyUnderCont.setTypeface(null, Typeface.BOLD)
            binding.btnPreApproval.isChecked = false
        }

        binding.backButton.setOnClickListener {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        viewModel.createNewApplicationParams.observe(viewLifecycleOwner, {
            Timber.e("listening == $createNewApplicationParams")
            createNewApplicationParams.let {

                if(it.EmailAddress != null && it.FirstName != null &&  it.LastName!= null && it.LoanGoal != null && it.LoanOfficerUserId != null
                        && it.branchId != null && it.LoanPurpose != null){
                    if(isFindContactSelected) {
                        binding.btnCreateApplication.isEnabled = true
                    }
                    else{
                        if (it.MobileNumber != null) {
                            binding.btnCreateApplication.isEnabled = it.MobileNumber!!.length == 10
                        }
                        else
                            binding.btnCreateApplication.isEnabled = false
                    }

                }
                else
                    binding.btnCreateApplication.isEnabled = false
            }
        })
        


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*
       binding.searchEdittext.setOnEditorActionListener(TextView.OnEditorActionListener { v, actionId, event ->
           if (actionId == EditorInfo.IME_ACTION_SEARCH) {
               binding.searchEdittext.clearFocus()
               binding.searchEdittext.hideKeyboard()
               val searchWord = binding.searchEdittext.text.toString()
               if (searchWord.isNotEmpty() && searchWord.isNotBlank()) {
                   lifecycleScope.launchWhenStarted {
                       sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                           startNewAppViewModel.searchByBorrowerContact(authToken, searchWord)
                       }
                   }

                   return@OnEditorActionListener true
               }
           }
          false
       })
        */

        binding.searchEdittext.doOnTextChanged { text, start, before, count ->
            if(count>1) {
                lifecycleScope.launchWhenStarted {
                    sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                        viewModel.searchByBorrowerContact(authToken, text.toString())
                    }
                }
            }
        }

        viewModel.searchResultResponse.observe(viewLifecycleOwner, {
            searchList = it
            adapter.showResult(searchList)
            binding.recyclerviewContacts.adapter = adapter
            binding.layoutFindContact.visibility = View.VISIBLE
            binding.recyclerviewContacts.visibility = View.VISIBLE
            binding.layoutEditText.setBackgroundResource(R.drawable.layout_style_flat_bottom)
        })

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        binding.btnCreateApplication.setOnClickListener {
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    if(emailPhoneValidated()) {
                        createNewApplicationParams.EmailAddress?.let { email ->
                            viewModel.lookUpBorrowerContact(authToken, email, createNewApplicationParams.MobileNumber)
                        }
                    }
                }
            }
        }

        viewModel.lookUpBorrowerContactResponse.observe(viewLifecycleOwner, {
            Timber.e("Atleast in look up observer....")
            if(it.code == "200" || it.status.equals("OK", true)) {
                Timber.e("Atleast code is = 200....")
                if (it.borrowerData != null) {
                    Timber.e("Atleast borrowerData is not null....")
                    BottomEmailPhoneErrorFragment.newInstance().show(childFragmentManager, BottomEmailPhoneErrorFragment::class.java.canonicalName)
                }
                else
                {
                    lifecycleScope.launchWhenStarted {
                        sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                            viewModel.createApplication(authToken, createNewApplicationParams)
                        }
                    }
                }
            }
        })

        viewModel.createNewAppResponse.observe(viewLifecycleOwner, {
            Timber.e("Atleast in look up observer....")
            if(it.code == "200" || it.status.equals("OK", true)) {
                startDetailActivity(it)
            }
        })
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        binding.loanOfficerAssigned.setOnClickListener{
            AssignBorrowerBottomDialogFragment.newInstance(this@StartNewApplicationFragment).show(childFragmentManager, AssignBorrowerBottomDialogFragment::class.java.canonicalName)
        }

        binding.assignLoanOfficer.setOnClickListener {
            AssignBorrowerBottomDialogFragment.newInstance(this@StartNewApplicationFragment).show(childFragmentManager, AssignBorrowerBottomDialogFragment::class.java.canonicalName)
        }

        // Pre-call Loan officer service..
        /*
        lifecycleScope.launchWhenStarted {
            sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                viewModel.getMcusByRoleId(authToken, filterLoanOfficer = true)
            }
        }
         */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////






        requireActivity().onBackPressedDispatcher.addCallback {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left)
        }

        binding.btnCancelContact.setOnClickListener{
            binding.recyclerviewContacts.visibility = View.VISIBLE
            binding.layoutResult.visibility = View.GONE
            binding.searchEdittext.visibility = View.VISIBLE
        }

       binding.parentLayout.setOnClickListener {
           HideSoftkeyboard.hide(requireActivity(),binding.parentLayout)
           super.removeFocusFromAllFields(binding.parentLayout)
       }

        binding.edFirstName.doAfterTextChanged {
            if(binding.edFirstName.text.isNullOrEmpty() || binding.edFirstName.text.isNullOrBlank())
                createNewApplicationParams.FirstName = null
            else
                createNewApplicationParams.FirstName = binding.edFirstName.text.toString()
            viewModel.setCreateNewParams(createNewApplicationParams)
        }

        binding.edLastName.doAfterTextChanged {
            if(binding.edLastName.text.isNullOrEmpty() || binding.edLastName.text.isNullOrBlank())
                createNewApplicationParams.LastName = null
            else
                createNewApplicationParams.LastName = binding.edLastName.text.toString()
            viewModel.setCreateNewParams(createNewApplicationParams)
        }

        binding.edEmail.doAfterTextChanged {
            if (!isValidEmailAddress(binding.edEmail.text.toString().trim())) {
                createNewApplicationParams.EmailAddress = null
                CustomMaterialFields.setError(binding.layoutEmail,getString(R.string.invalid_email),requireActivity())
            } else {
                CustomMaterialFields.clearError(binding.layoutEmail,requireActivity())
                createNewApplicationParams.EmailAddress = binding.edEmail.text.toString()
            }
            viewModel.setCreateNewParams(createNewApplicationParams)

        }

        binding.edMobile.doAfterTextChanged {
               if(binding.edMobile.text?.length==14) {
                    val phoneNumber = binding.edMobile.text.toString()
                    var correctPhoneNumber = phoneNumber.replace(" ", "")
                    correctPhoneNumber = correctPhoneNumber.replace("+1", "")
                    correctPhoneNumber = correctPhoneNumber.replace("-", "")
                    correctPhoneNumber = correctPhoneNumber.replace("(", "")
                    correctPhoneNumber = correctPhoneNumber.replace(")", "")
                    createNewApplicationParams.MobileNumber = correctPhoneNumber
            }
            else
                createNewApplicationParams.MobileNumber = null
            viewModel.setCreateNewParams(createNewApplicationParams)
        }

    }


    private fun emailPhoneValidated():Boolean{

        if(!isFindContactSelected) {
            val phoneNumber = createNewApplicationParams.MobileNumber
            if (phoneNumber != null) {
                if (phoneNumber.length != 10)
                    return false
            }
        }
        //else return true // consider no number is provided and let it go

        val emailString = createNewApplicationParams.EmailAddress
        if (emailString != null) {
            if(emailString.isNotBlank() && emailString.isNotEmpty()) {
                if (!isValidEmailAddress(emailString))
                    return false
            }
        }
        else
            return  false

        return true
    }

    private fun startDetailActivity(createAppResponse: CreateNewApplicationResponse){
        val borrowerDetailIntent = Intent(requireActivity(), DetailActivity::class.java)
        createAppResponse.createNewAppData?.let { createAppResponse->
            borrowerDetailIntent.putExtra(AppConstant.loanApplicationId, createAppResponse.loanApplicationId)
            borrowerDetailIntent.putExtra(AppConstant.loanPurpose, createAppResponse.loanPurpose)
            borrowerDetailIntent.putExtra(AppConstant.firstName, createAppResponse.firstName)
            borrowerDetailIntent.putExtra(AppConstant.lastName, createAppResponse.lastName)
            borrowerDetailIntent.putExtra(AppConstant.bPhoneNumber, createAppResponse.mobileNumber)
            borrowerDetailIntent.putExtra(AppConstant.bEmail, createAppResponse.emailAddress)
        }
        startActivity(borrowerDetailIntent)
        requireActivity().finish()
    }

    override fun onItemClick(position: Int) {
        binding.searchedContactName.text = searchList[position].firstName
        binding.searchedContactEmail.text = searchList[position].emailAddress
        binding.searchedContactPhone.text = searchList[position].mobileNumber

        createNewApplicationParams.let {
            it.FirstName = searchList[position].firstName
            it.LastName = searchList[position].lastName
            it.EmailAddress = searchList[position].emailAddress
            it.MobileNumber = searchList[position].mobileNumber
            it.contactId =  searchList[position].contactId
        }
        viewModel.setCreateNewParams(createNewApplicationParams)

        binding.layoutResult.visibility = View.VISIBLE
        binding.searchEdittext.visibility = View.GONE
        //binding.searchEdittext.setText("")
        binding.recyclerviewContacts.visibility = View.GONE
        binding.layoutEditText.setBackgroundResource(R.drawable.layout_style_flat_bottom)

        // searchList.clear()
    }

    private fun setLabelFocus(){
        // set lable focus
        binding.edFirstName.onFocusChangeListener = CustomFocusListenerForEditText(binding.edFirstName, binding.layoutFirstName, requireContext(),getString(R.string.error_field_required))
        binding.edLastName.onFocusChangeListener = CustomFocusListenerForEditText(binding.edLastName, binding.layoutLastName, requireContext(),getString(R.string.error_field_required))
        binding.edMobile.onFocusChangeListener = CustomFocusListenerForEditText(binding.edMobile, binding.layoutMobileNum, requireContext(),getString(R.string.invalid_phone_num))
        binding.edMobile.addTextChangedListener(PhoneTextFormatter(binding.edMobile, "(###) ###-####"))

        binding.edEmail.setOnFocusChangeListener { _, hasFocus ->
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutEmail, R.color.grey_color_two,requireActivity())
            } else {
                if (binding.edEmail.text?.length == 0) {
                    CustomMaterialFields.setError(binding.layoutEmail,getString(R.string.error_field_required),requireActivity())
                    CustomMaterialFields.setColor(binding.layoutEmail, R.color.grey_color_three,requireActivity())
                } else {
                    CustomMaterialFields.setColor(binding.layoutEmail, R.color.grey_color_two,requireActivity())
                    if (!isValidEmailAddress(binding.edEmail.text.toString().trim())) {
                        CustomMaterialFields.setError(binding.layoutEmail,getString(R.string.invalid_email),requireActivity())
                    } else {
                        CustomMaterialFields.clearError(binding.layoutEmail,requireActivity())
                    }
                }
            }
        }

    }

    private fun findOrCreateContactClick(){
        if(binding.findContactBtn.isChecked) {
            //searchList.clear()
            binding.findContactBtn.isChecked = false
            binding.createContactBtn.visibility=View.VISIBLE
            binding.layoutFindContact.visibility = View.VISIBLE
            binding.searchEdittext.visibility = View.VISIBLE
            binding.layoutResult.visibility = View.GONE
            binding.layoutCreateContact.visibility = View.GONE
            binding.findContactBtn.visibility = View.GONE
            binding.searchEdittext.setText("")
            binding.layoutEditText.setBackgroundResource(R.drawable.edittext_search_contact_style)
        }
        else {
            if (binding.createContactBtn.isChecked) {
                binding.createContactBtn.isChecked = false
                binding.createContactBtn.visibility=View.GONE
                binding.findContactBtn.visibility = View.VISIBLE
                binding.layoutFindContact.visibility = View.GONE
                binding.layoutCreateContact.visibility = View.VISIBLE
                binding.recyclerviewContacts.visibility = View.GONE

            }
        }
    }

    private fun onLoanPurposeClick(){

        //Timber.e(" binding.btnLoanPurchase.isSelected "+binding.btnLoanPurchase.isSelected)

        //Timber.e(" binding.btnLoanRefinance.isSelected "+binding.btnLoanRefinance.isSelected)

        if(binding.btnLoanPurchase.isPressed){
            //binding.btnLoanPurchase.setTypeface(null, Typeface.BOLD)
            //binding.btnLoanRefinance.setTypeface(null, Typeface.NORMAL)
            binding.rgPurchaseLoanGoal.visibility= View.VISIBLE
            binding.rgRefinanceLoanGoal.visibility = View.GONE
        }
        else {
            //binding.btnLoanPurchase.setTypeface(null, Typeface.NORMAL)
            //binding.btnLoanRefinance.setTypeface(null, Typeface.BOLD)
            binding.rgPurchaseLoanGoal.visibility= View.GONE
            binding.rgRefinanceLoanGoal.visibility = View.VISIBLE
        }
    }

    private fun preSelectInitialization(){
        binding.btnLowerPaymentTerms.setTypeface(null, Typeface.NORMAL)
        binding.btnCashout.setTypeface(null, Typeface.NORMAL)
        binding.btnDebtConsolidation.setTypeface(null, Typeface.NORMAL)

        binding.btnLowerPaymentTerms.isChecked = false
        binding.btnCashout.isChecked = false
        binding.btnDebtConsolidation.isChecked = false
    }

    private fun isValidEmailAddress(email: String?): Boolean {
        val ePattern =
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3})|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,3}))$"
        val p = Pattern.compile(ePattern)
        val m = p.matcher(email)
        return m.matches()
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
    fun onLoanOfficerSelected(loanOfficerSelectedEvent: LoanOfficerSelectedEvent) {

        Timber.e(" loan officer selected........."+ loanOfficerSelectedEvent.mcu.profileimageurl)
        binding.assignLoanOfficer.visibility = View.GONE
        binding.loanOfficerAssigned.visibility = View.VISIBLE
        Glide.with(requireActivity())
            .load(loanOfficerSelectedEvent.mcu.profileimageurl)
            .circleCrop()
            .into(binding.loImage)
        binding.loName.text = loanOfficerSelectedEvent.mcu.fullName
        binding.loDetail.text = loanOfficerSelectedEvent.mcu.branchName
        createNewApplicationParams.let {
            it.branchId = loanOfficerSelectedEvent.mcu.branchId
            it.LoanOfficerUserId = loanOfficerSelectedEvent.mcu.userId
        }
        viewModel.setCreateNewParams(createNewApplicationParams)
        //SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG)

    }

}


/*

    private fun View.hideKeyboard() {
        val imm = context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        imm.hideSoftInputFromWindow(windowToken, 0)
    }

    private val disableScrollViewListener  = View.OnTouchListener { p0, p1 -> true }

    private val enableScrollViewListener  = View.OnTouchListener { p0, p1 -> true };
 */