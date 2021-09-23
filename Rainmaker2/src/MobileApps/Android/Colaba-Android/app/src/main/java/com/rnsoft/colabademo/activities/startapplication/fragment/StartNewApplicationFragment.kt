package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.Fragment

import com.rnsoft.colabademo.databinding.StartApplicationFragLayoutBinding
import com.rnsoft.colabademo.activities.startapplication.adapter.ContactsAdapter


/**
 * Created by Anita Kiran on 9/17/2021.
 */
class StartNewApplicationFragment : BaseFragment(), View.OnClickListener {

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

            initViews()
            setLabelFocus()
            super.addListeners(binding.root)
            savedViewInstance
        }
    }

    private fun initViews() {

        binding.findContactBtn.setOnClickListener(this)
        binding.createNewContactBtn.setOnClickListener(this)
        binding.btnLoanRefinance.setOnClickListener(this)
        binding.btnLoanPurchase.setOnClickListener(this)
        binding.rbDebtConsolidation.setOnClickListener(this)
        binding.rbCashout.setOnClickListener(this)
        binding.rbLowerPaymentterms.setOnClickListener(this)
        binding.parentLayout.setOnClickListener(this)
        binding.backButton.setOnClickListener(this)

        adapter = ContactsAdapter(requireActivity(),searchList)
        searchList.add(Contacts("Richard Glenn Randall","richard.glenn@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Arnold Richard","arnold634@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Richard Glenn Randall","richard.glenn@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Arnold Richard","arnold634@gmail.com","(121) 353 1343"))

        binding.searchEdittext.doAfterTextChanged {
            it.let {
                binding.recyclerviewContacts.adapter = adapter
                binding.recyclerviewContacts.visibility = View.VISIBLE
            }
        }

    }

    override fun onClick(view: View?) {
        when (view?.getId()) {
            R.id.find_contact_btn -> findOrCreateContactClick()
            R.id.btn_loan_purchase -> onLoanPurposeClick()
            R.id.btn_loan_refinance -> onLoanPurposeClick()
            R.id.rb_lower_paymentterms -> onLoanGoalClick()
            R.id.rb_cashout -> onLoanGoalClick()
            R.id.rb_debt_consolidation -> onLoanGoalClick()
            R.id.create_new_contact_btn ->findOrCreateContactClick()
            R.id.backButton -> requireActivity().finish()
            R.id.parentLayout -> HideSoftkeyboard.hide(requireActivity(),binding.parentLayout)
        }
    }

    private fun setLabelFocus(){
        // set lable focus
        binding.edFirstName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edFirstName, binding.layoutFirstName, requireContext()))
        binding.edLastName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edLastName, binding.layoutLastName, requireContext()))
        binding.edEmail.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edEmail, binding.layoutEmail, requireContext()))
        binding.edMobile.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edMobile, binding.layoutMobileNum, requireContext()))
        binding.edMobile.addTextChangedListener(PhoneTextFormatter(binding.edMobile, "(###) ###-####"))
    }

    private fun findOrCreateContactClick(){
        if(binding.findContactBtn.isChecked) {
            searchList.clear()
            binding.findContactBtn.isChecked = false
            binding.createNewContactBtn.visibility=View.VISIBLE
            binding.layoutFindContact.visibility = View.VISIBLE
            binding.layoutCreateContact.visibility = View.GONE
            binding.findContactBtn.visibility = View.GONE
        }
        else {
            if (binding.createNewContactBtn.isChecked) {
                binding.createNewContactBtn.isChecked = false
                binding.createNewContactBtn.visibility=View.GONE
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
        if(binding.rbLowerPaymentterms.isChecked){
            binding.rbLowerPaymentterms.setTypeface(null, Typeface.BOLD)
            binding.rbCashout.setTypeface(null, Typeface.NORMAL)
            binding.rbDebtConsolidation.setTypeface(null, Typeface.NORMAL)
        }
        else if(binding.rbCashout.isChecked) {
            binding.rbLowerPaymentterms.setTypeface(null, Typeface.NORMAL)
            binding.rbCashout.setTypeface(null, Typeface.BOLD)
            binding.rbDebtConsolidation.setTypeface(null, Typeface.NORMAL)
        }
        else if (binding.rbLowerPaymentterms.isChecked){
            binding.rbLowerPaymentterms.setTypeface(null, Typeface.NORMAL)
            binding.rbCashout.setTypeface(null, Typeface.NORMAL)
            binding.rbDebtConsolidation.setTypeface(null, Typeface.BOLD)
        }
    }

}