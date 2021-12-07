package com.rnsoft.colabademo

import android.annotation.SuppressLint
import android.content.Context
import android.content.SharedPreferences
import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.MotionEvent
import android.view.View
import android.view.ViewGroup
import android.view.inputmethod.InputMethodManager
import androidx.core.widget.doAfterTextChanged
import com.rnsoft.colabademo.activities.startapplication.adapter.ContactsAdapter
import com.rnsoft.colabademo.utils.CustomMaterialFields
import kotlinx.android.synthetic.main.dependent_input_field.view.*
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*
import java.util.regex.Pattern
import androidx.activity.addCallback
import androidx.core.widget.doOnTextChanged
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.rnsoft.colabademo.databinding.StartApplicationFragLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class StartNewApplicationFragment : BaseFragment(), RecyclerviewClickListener {

    private lateinit var binding: StartApplicationFragLayoutBinding
    private var savedViewInstance: View? = null
    private lateinit var adapter : ContactsAdapter
    private var searchList = ArrayList<SearchResultResponseItem>()

    private val viewModel: StartNewAppViewModel by activityViewModels()

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
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
        

        searchList.add(SearchResultResponseItem( 1,"richard.glenn@gmail.com","Richard Glenn Randall", mobileNumber =   "(121) 353 1343"))
        searchList.add(SearchResultResponseItem(2,"arnold634@gmail.com", "Arnold Richard", mobileNumber = "(121) 353 1343"))
        searchList.add(SearchResultResponseItem(3,"richard.glenn@gmail.com", "Richard Glenn Randall", mobileNumber = "(121) 353 1343"))

        adapter = ContactsAdapter(requireActivity(), this@StartNewApplicationFragment)
        binding.recyclerviewContacts.setHasFixedSize(true)

        binding.recyclerviewContacts.setOnTouchListener(object : View.OnTouchListener {
            override fun onTouch(v: View, m: MotionEvent): Boolean {
                //binding.scrollviewStartApplication.requestDisallowInterceptTouchEvent(true)
                //binding.scrollviewStartApplication.setOnTouchListener(disableScrollViewListener)
                binding.scrollviewStartApplication.setEnableScrolling(false) //
                //binding.recyclerviewContacts.isEnabled = false
                return false
            }
        })

        binding.parentLayout.setOnTouchListener(object : View.OnTouchListener {
            override fun onTouch(v: View, m: MotionEvent): Boolean {
                binding.scrollviewStartApplication.setEnableScrolling(true); //
                return false
            }
        })

       /* binding.recyclerviewContacts.setOnTouchListener(object : View.OnTouchListener {
            override fun onTouch(v: View?, event: MotionEvent?): Boolean {
                when (event?.action) {
                    MotionEvent.ACTION_DOWN ->   binding.scrollviewStartApplication.requestDisallowInterceptTouchEvent(true)
                }

                return v?.onTouchEvent(event) ?: false
            }
        }) */

        binding.findContactBtn.setOnClickListener { findOrCreateContactClick() }

        binding.createContactBtn.setOnClickListener { findOrCreateContactClick() }

        binding.btnLoanPurchase.setOnClickListener { onLoanPurposeClick() }

        binding.btnLoanRefinance.setOnClickListener { onLoanPurposeClick() }

        binding.btnLowerPaymentTerms.setOnClickListener { onLoanGoalClick() }

        binding.btnCashout.setOnClickListener { onLoanGoalClick() }

        binding.btnDebtConsolidation.setOnClickListener { onLoanGoalClick() }

        binding.btnPreApproval.setOnClickListener {
            binding.btnPreApproval.setTypeface(null, Typeface.BOLD)
            binding.btnPropertyUnderCont.setTypeface(null, Typeface.NORMAL)
        }

        binding.btnPropertyUnderCont.setOnClickListener {
            binding.btnPreApproval.setTypeface(null, Typeface.NORMAL)
            binding.btnPropertyUnderCont.setTypeface(null, Typeface.BOLD)
        }

        binding.backButton.setOnClickListener {
            requireActivity().finish()
            requireActivity().overridePendingTransition(R.anim.hold, R.anim.slide_out_left) }

        binding.assignLoanOfficer.setOnClickListener {
            AssignBorrowerBottomDialogFragment.newInstance(this@StartNewApplicationFragment).show(childFragmentManager, AssignBorrowerBottomDialogFragment::class.java.canonicalName)
        }

        binding.btnCreateApplication.setOnClickListener {
            lifecycleScope.launchWhenStarted {
                sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
                    if(emailPhoneValidated()) {
                        val phoneNumber = binding.edMobile.text.toString()
                        var correctPhoneNumber = phoneNumber.replace(" ", "")
                        correctPhoneNumber = correctPhoneNumber.replace("+1", "")
                        correctPhoneNumber = correctPhoneNumber.replace("-", "")
                        correctPhoneNumber = correctPhoneNumber.replace("(", "")
                        correctPhoneNumber = correctPhoneNumber.replace(")", "")
                        viewModel.lookUpBorrowerContact(authToken, binding.edEmail.text.toString(), correctPhoneNumber)
                    }
                }
            }
        }

        viewModel.lookUpBorrowerContactResponse.observe(viewLifecycleOwner, {
            if(it.code == "200" || it.status.equals("OK", true)) {
                if (it.borrowerData != null) {
                    BottomEmailPhoneErrorFragment.newInstance().show(childFragmentManager, BottomEmailPhoneErrorFragment::class.java.canonicalName)
                }
            }
        })

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
           checkRequiredFields()
        }

        binding.edLastName.doAfterTextChanged {
            checkRequiredFields()
        }

        binding.edEmail.doAfterTextChanged {
            checkRequiredFields()
        }

        binding.edMobile.doAfterTextChanged {
            checkRequiredFields()
        }

    }

    private fun emailPhoneValidated():Boolean{

        val phoneNumber = binding.edMobile.text.toString()
        if(phoneNumber.length!=14)
            return false

        val emailString = binding.edEmail.text.toString()
        if(emailString.isNotBlank() && emailString.isNotEmpty()) {
            if (!isValidEmailAddress(binding.edEmail.text.toString().trim()))
                return false
        }
        else
            return  false

        return true
    }



    private val disableScrollViewListener  = object : View.OnTouchListener {
        override fun onTouch(p0: View?, p1: MotionEvent?): Boolean {
            return true
        }

    }

    private val enableScrollViewListener  = object : View.OnTouchListener {
        override fun onTouch(p0: View?, p1: MotionEvent?): Boolean {
            return true
        }

    };

    private fun checkRequiredFields() {
        binding.btnCreateApplication.isEnabled =
            !binding.edFirstName.text.toString().isEmpty() && !binding.edLastName.text.toString().isEmpty() && !binding.edMobile.text.toString().isEmpty() &&
                !binding.edEmail.text.toString().isEmpty()
    }

    override fun onItemClick(position: Int) {
        binding.searchedContactName.text = searchList.get(position).firstName
        binding.searchedContactEmail.text = searchList.get(position).emailAddress
        binding.searchedContactPhone.text = searchList.get(position).mobileNumber

        binding.layoutResult.visibility = View.VISIBLE
        binding.searchEdittext.visibility = View.GONE
        //binding.searchEdittext.setText("")
        binding.recyclerviewContacts.visibility = View.GONE
        binding.layoutEditText.setBackgroundResource(R.drawable.layout_style_flat_bottom)

        // searchList.clear()
    }

    private fun setLabelFocus(){
        // set lable focus
        binding.edFirstName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edFirstName, binding.layoutFirstName, requireContext(),getString(R.string.error_field_required)))
        binding.edLastName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edLastName, binding.layoutLastName, requireContext(),getString(R.string.error_field_required)))
        binding.edMobile.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edMobile, binding.layoutMobileNum, requireContext(),getString(R.string.invalid_phone_num)))
        binding.edMobile.addTextChangedListener(PhoneTextFormatter(binding.edMobile, "(###) ###-####"))

        binding.edEmail.setOnFocusChangeListener { view, hasFocus ->
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
        if(binding.btnLoanPurchase.isChecked){
            binding.btnLoanPurchase.setTypeface(null, Typeface.BOLD)
            binding.btnLoanRefinance.setTypeface(null, Typeface.NORMAL)
            binding.rgPurchaseLoanGoal.visibility= View.VISIBLE
            binding.rgRefinanceLoanGoal.visibility = View.GONE
        }
        else {
            binding.btnLoanPurchase.setTypeface(null, Typeface.NORMAL)
            binding.btnLoanRefinance.setTypeface(null, Typeface.BOLD)
            binding.rgPurchaseLoanGoal.visibility= View.GONE
            binding.rgRefinanceLoanGoal.visibility = View.VISIBLE
        }
    }

    private fun onLoanGoalClick(){
        if(binding.btnLowerPaymentTerms.isChecked){
            binding.btnLowerPaymentTerms.setTypeface(null, Typeface.BOLD)
            binding.btnCashout.setTypeface(null, Typeface.NORMAL)
            binding.btnDebtConsolidation.setTypeface(null, Typeface.NORMAL)
        }
        else if(binding.btnCashout.isChecked) {
            binding.btnLowerPaymentTerms.setTypeface(null, Typeface.NORMAL)
            binding.btnCashout.setTypeface(null, Typeface.BOLD)
            binding.btnDebtConsolidation.setTypeface(null, Typeface.NORMAL)
        }
        else if (binding.btnDebtConsolidation.isChecked){
            binding.btnLowerPaymentTerms.setTypeface(null, Typeface.NORMAL)
            binding.btnCashout.setTypeface(null, Typeface.NORMAL)
            binding.btnDebtConsolidation.setTypeface(null, Typeface.BOLD)
        }
    }

    private fun isValidEmailAddress(email: String?): Boolean {
        val ePattern =
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,3}))$"
        val p = Pattern.compile(ePattern)
        val m = p.matcher(email)
        return m.matches()
    }


   // fun CharSequence?.isValidEmail() = !isNullOrEmpty() && Patterns.EMAIL_ADDRESS.matcher(email).matches()


    private fun View.hideKeyboard() {
        val imm = context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        imm.hideSoftInputFromWindow(windowToken, 0)
    }

}


/*binding.searchEdittext.addTextChangedListener(object : TextWatcher {
           override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
           override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
           override fun afterTextChanged(s: Editable) {
               adapter.showResult(searchList)
               binding.recyclerviewContacts.adapter = adapter

               binding.layoutFindContact.visibility = View.VISIBLE
               binding.recyclerviewContacts.visibility = View.VISIBLE
               binding.layoutEditText.setBackgroundResource(R.drawable.layout_style_flat_bottom)

               //binding.searchEdittext.setBackground(requireActivity().resources.getDrawable(R.drawable.layout_style_flat_bottom))
               //HideSoftkeyboard.hide(requireActivity(),binding.parentLayout)

           }
       }) */