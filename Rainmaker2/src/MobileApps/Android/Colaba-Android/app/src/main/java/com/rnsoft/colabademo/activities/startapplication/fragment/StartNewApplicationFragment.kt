package com.rnsoft.colabademo

import android.annotation.SuppressLint
import android.graphics.Typeface
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.MotionEvent
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.core.widget.doOnTextChanged
import com.rnsoft.colabademo.activities.addresses.info.fragment.DeleteCurrentResidenceDialogFragment

import com.rnsoft.colabademo.databinding.StartApplicationFragLayoutBinding
import com.rnsoft.colabademo.activities.startapplication.adapter.ContactsAdapter
import com.rnsoft.colabademo.utils.CustomMaterialFields
import kotlinx.android.synthetic.main.dependent_input_field.view.*
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*
import timber.log.Timber
import java.util.regex.Pattern


/**
 * Created by Anita Kiran on 9/17/2021.
 */
class StartNewApplicationFragment : BaseFragment(), RecyclerviewClickListener {

    private lateinit var binding: StartApplicationFragLayoutBinding
    private var savedViewInstance: View? = null
    private lateinit var adapter : ContactsAdapter
    val searchList = ArrayList<Contacts>()

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

            binding.btnCreateApp.isEnabled = false







            setupUI()
            setLabelFocus()
            super.addListeners(binding.root)

            savedViewInstance
        }
    }

    @SuppressLint("ClickableViewAccessibility", "UseCompatLoadingForDrawables")
    private fun setupUI() {

        searchList.add(Contacts("Richard Glenn Randall","richard.glenn@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Arnold Richard","arnold634@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Richard Glenn Randall","richard.glenn@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Arnold Richard","arnold634@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Richard Glenn Randall","richard.glenn@gmail.com","(121) 353 1343"))

        adapter = ContactsAdapter(requireActivity(), this@StartNewApplicationFragment)
        binding.recyclerviewContacts.setHasFixedSize(true)
        binding.recyclerviewContacts.setNestedScrollingEnabled(false)

          binding.searchEdittext.addTextChangedListener(object : TextWatcher {
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
        })

//        binding.recyclerviewContacts.setOnTouchListener(object : View.OnTouchListener {
//            override fun onTouch(v: View, m: MotionEvent): Boolean {
//                binding.scrollviewStartApplication.requestDisallowInterceptTouchEvent(true)
//                return false
//            }
//        })


        binding.recyclerviewContacts.setOnTouchListener(object : View.OnTouchListener {
            override fun onTouch(v: View?, event: MotionEvent?): Boolean {
                when (event?.action) {
                    MotionEvent.ACTION_DOWN ->   binding.scrollviewStartApplication.requestDisallowInterceptTouchEvent(true)
                }

                return v?.onTouchEvent(event) ?: false
            }
        })

        /*binding.searchEdittext.setOnFocusChangeListener { view, hasFocus ->
            if(hasFocus){
                binding.searchEdittext.setBackground(requireActivity().resources.getDrawable(R.drawable.layout_style_flat_bottom))

            } else {
                binding.searchEdittext.setBackground(requireActivity().resources.getDrawable(R.drawable.edittext_search_contact_style))
            }
        } */


        binding.findContactBtn.setOnClickListener { findOrCreateContactClick() }

        binding.createContactBtn.setOnClickListener { findOrCreateContactClick() }

        binding.btnLoanPurchase.setOnClickListener { onLoanPurposeClick() }

        binding.btnLoanRefinance.setOnClickListener { onLoanPurposeClick() }

        binding.btnLowerPaymentTerms.setOnClickListener { onLoanGoalClick() }

        binding.btnCashout.setOnClickListener { onLoanGoalClick() }

        binding.btnDebtConsolidation.setOnClickListener { onLoanGoalClick() }

        binding.backButton.setOnClickListener { requireActivity().finish() }

        binding.assignLoanOfficer.setOnClickListener {
            AssignBorrowerBottomDialogFragment.newInstance(this@StartNewApplicationFragment).show(childFragmentManager, AssignBorrowerBottomDialogFragment::class.java.canonicalName)
        }

        binding.btnCancelContact.setOnClickListener{
            binding.recyclerviewContacts.visibility = View.VISIBLE
            binding.layoutResult.visibility = View.GONE
            binding.searchEdittext.visibility = View.VISIBLE
            //binding.layoutEditText.setBackground(requireActivity().resources.getDrawable(R.drawable.edittext_search_contact_style))

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


    private fun checkRequiredFields() {
        if (!binding.edFirstName.text.toString().isEmpty() && !binding.edLastName.text.toString().isEmpty() && !binding.edMobile.text.toString().isEmpty() &&
            !binding.edEmail.text.toString().isEmpty()) {
            binding.btnCreateApp.isEnabled = true
        }
        else
            binding.btnCreateApp.isEnabled = false
    }


    override fun onItemClick(position: Int) {
        binding.searchedContactName.text = searchList.get(position).contactName
        binding.searchedContactEmail.text = searchList.get(position).contactEmail
        binding.searchedContactPhone.text = searchList.get(position).contactNumber

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
                    if (!isValidEmailAddress(binding.edEmail.text.toString())) {
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
        }
        else {
            if (binding.createContactBtn.isChecked) {
                binding.createContactBtn.isChecked = false
                binding.createContactBtn.visibility=View.GONE
                binding.findContactBtn.visibility = View.VISIBLE
                binding.layoutFindContact.visibility = View.GONE
                binding.layoutCreateContact.visibility = View.VISIBLE
            }
        }
    }

    private fun onLoanPurposeClick(){
        if(binding.btnLoanPurchase.isChecked){
            binding.btnLoanPurchase.setTypeface(null, Typeface.BOLD)
            binding.btnLoanRefinance.setTypeface(null, Typeface.NORMAL)
        }
        else {
            binding.btnLoanPurchase.setTypeface(null, Typeface.NORMAL)
            binding.btnLoanRefinance.setTypeface(null, Typeface.BOLD)
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
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$"
        val p = Pattern.compile(ePattern)
        val m = p.matcher(email)
        return m.matches()
    }

}