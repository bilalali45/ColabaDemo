package com.rnsoft.colabademo

import android.graphics.Typeface
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.view.isVisible
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.SpouseDetailLayoutBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import kotlin.concurrent.fixedRateTimer


class MarriageDetailFragment : BaseFragment() {

    private lateinit var binding : SpouseDetailLayoutBinding
    var marriage_type : String? = null
    var borrower_name : String? = null
    var loanApplicationId : Int? = null
    var borrowerId : Int?= null
    val coborrowerList: ArrayList<CoborrowerList> = arrayListOf()


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = SpouseDetailLayoutBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)

        arguments?.let { arguments ->
            marriage_type = arguments.getString(AppConstant.marriage_type)
        }

        if(marriage_type.equals(AppConstant.married)){
            binding.marriageType.text = AppConstant.married
            binding.tvQuestion.text = getString(R.string.married_to_coborower)
        }

        if(marriage_type.equals(AppConstant.separated)){
            binding.marriageType.text = AppConstant.separated
            binding.tvQuestion.text = getString(R.string.coborower_legal_spouse)
        }

        initViews()
        setCoBorrowers()

        return binding.root
    }

    private fun setCoBorrowers() {
        val activity = (activity as? BorrowerAddressActivity)
        activity?.borrowerInfoList?.let { list->
            if(list.size >0){
                val itemList: ArrayList<String> = arrayListOf()
                for(item in list.indices) {
                    if (list.get(item).owntypeId == 2) {
                        itemList.add(list.get(item).firstName + " " + list.get(item).lastName)
                        coborrowerList.add(CoborrowerList(list.get(item).borrowerId, list.get(item).owntypeId, list.get(item).firstName!!, list.get(item).lastName!!))
                    }
                }
                Log.e("coborrowerList",""+coborrowerList)


                val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, itemList)
                binding.tvCoborrower.setAdapter(adapter)

                binding.tvCoborrower.setOnFocusChangeListener { _, _ ->
                    binding.tvCoborrower.showDropDown()
                }
                binding.tvCoborrower.setOnClickListener {
                    binding.tvCoborrower.showDropDown()
                }

                binding.tvCoborrower.onItemClickListener = object :
                    AdapterView.OnItemClickListener {
                    override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                        CustomMaterialFields.setColor(binding.layoutCoborrower, R.color.grey_color_two, requireActivity())
                    }
                }

            }

        }
    }

    private fun initViews(){

        binding.btnSave.setOnClickListener{
            var isDataEntered : Boolean = false

            if(binding.layoutCoborrower.isVisible){
                val coborrower = binding.tvCoborrower.getText().toString().trim()
                if(coborrower.isEmpty() || coborrower.length == 0){
                    isDataEntered = false
                    CustomMaterialFields.setError(binding.layoutCoborrower, getString(R.string.error_field_required), requireActivity())
                }
                if(coborrower.length >0){
                    isDataEntered = true
                    CustomMaterialFields.clearError(binding.layoutCoborrower, requireActivity())
                }
            }

            if(binding.spouseInfoLayout.isVisible){
                isDataEntered = true
            }

            if(isDataEntered){
                val activity = (activity as? BorrowerAddressActivity)
                activity?.loanApplicationId?.let { loanId ->
                 loanApplicationId = loanId }

                activity?.borrowerId?.let { bId ->
                    borrowerId = bId
                }
                if(loanApplicationId != null && borrowerId !=null){

                    var firstName = binding.etFirstName.text.toString().trim()
                     firstName = if(firstName.isNotEmpty() && firstName.length > 0) firstName else


                    val maritalStatus = MaritalStatus(
                        loanApplicationId = loanApplicationId!!,
                        borrowerId = borrowerId!!,
                        firstName = ,
                        middleName = ,
                        lastName = ,
                        relationWithPrimaryId = ,
                        isInRelationship =,
                        otherRelationshipExplanation = ,
                        relationFormedStateId = null,
                        relationshipTypeId = null,
                        spouseBorrowerId = ,
                        spouseLoanContactId = ,
                        spouseMaritalStatusId =
                    )
                }

            }


        }

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }


        binding.yesRadioBtn.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.layoutCoborrower.visibility = View.VISIBLE
                binding.tvCoborrower.setText("")
                binding.yesRadioBtn.setTypeface(null, Typeface.BOLD)
            }
            else {
                binding.layoutCoborrower.visibility = View.GONE
                binding.yesRadioBtn.setTypeface(null, Typeface.NORMAL)
            }
        }

        binding.noRadioBtn.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.spouseInfoLayout.visibility = View.VISIBLE
                binding.noRadioBtn.setTypeface(null, Typeface.BOLD)
            }
            else {
                binding.spouseInfoLayout.visibility = View.GONE
                binding.noRadioBtn.setTypeface(null, Typeface.NORMAL)
            }
        }
    }

}