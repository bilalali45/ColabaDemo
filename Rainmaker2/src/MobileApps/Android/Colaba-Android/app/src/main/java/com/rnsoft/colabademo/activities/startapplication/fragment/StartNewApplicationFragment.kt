package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup

import com.rnsoft.colabademo.databinding.StartApplicationFragLayoutBinding
import com.rnsoft.colabademo.activities.startapplication.adapter.ContactsAdapter
import kotlinx.android.synthetic.main.non_permenant_resident_layout.*
import timber.log.Timber


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

            setupUI()
            setLabelFocus()
            super.addListeners(binding.root)

            savedViewInstance
        }
    }

    private fun setupUI() {

        searchList.add(Contacts("Richard Glenn Randall","richard.glenn@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Arnold Richard","arnold634@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Richard Glenn Randall","richard.glenn@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Arnold Richard","arnold634@gmail.com","(121) 353 1343"))
        searchList.add(Contacts("Richard Glenn Randall","richard.glenn@gmail.com","(121) 353 1343"))

        adapter = ContactsAdapter(requireActivity(), this@StartNewApplicationFragment)

          binding.searchEdittext.addTextChangedListener(object : TextWatcher {
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
            override fun afterTextChanged(s: Editable) {
                adapter.showResult(searchList)
                binding.recyclerviewContacts.adapter = adapter
                binding.layoutFindContact.visibility = View.VISIBLE
                binding.recyclerviewContacts.visibility = View.VISIBLE

            }
        })

        binding.findContactBtn.setOnClickListener { findOrCreateContactClick() }

        binding.createContactBtn.setOnClickListener { findOrCreateContactClick() }

        binding.btnLoanPurchase.setOnClickListener { onLoanPurposeClick() }

        binding.btnLoanRefinance.setOnClickListener { onLoanPurposeClick() }

        binding.btnLowerPaymentTerms.setOnClickListener { onLoanGoalClick() }

        binding.btnCashout.setOnClickListener { onLoanGoalClick() }

        binding.btnDebtConsolidation.setOnClickListener { onLoanGoalClick() }

        binding.backButton.setOnClickListener { requireActivity().finish() }

    }

    override fun onItemClick(position: Int) {
        binding.searchedContactName.text = searchList.get(position).contactName
        binding.searchedContactEmail.text = searchList.get(position).contactEmail
        binding.searchedContactPhone.text = searchList.get(position).contactNumber

        binding.layoutResult.visibility = View.VISIBLE
        binding.searchEdittext.visibility = View.GONE
        binding.searchEdittext.setText("")
        binding.recyclerviewContacts.visibility = View.GONE
       // searchList.clear()
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

}