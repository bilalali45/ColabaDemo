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
import java.lang.Exception
import kotlin.concurrent.fixedRateTimer


class MarriageDetailFragment : BaseFragment() {

    private lateinit var binding : SpouseDetailLayoutBinding
    var marriage_type : String? = null
    var borrower_name : String? = null
    var loanApplicationId : Int? = null
    var borrowerId : Int?= null
    var coBorrowerCount : Int = 0
    var ownTypeId : Int = 0
    val coborrowerList: ArrayList<CoborrowerList> = arrayListOf()
    private var maritalStatus : MaritalStatus? = null
    var firstName : String? = null
    var lastName : String? = null


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = SpouseDetailLayoutBinding.inflate(inflater, container, false)
        super.addListeners(binding.root)

        arguments?.let { arguments ->
            marriage_type = arguments.getString(AppConstant.marriage_type)
            ownTypeId = arguments.getInt(AppConstant.owntypeid)
            binding.marriageType.text = marriage_type
        }
        initViews()
        setCoBorrowers()

        if(ownTypeId == 1) {
            if (coBorrowerCount == 0){
                binding.layoutQuestion.visibility = View.GONE // don't ask ques only ask names
                binding.spouseInfoLayout.visibility = View.VISIBLE
                if (marriage_type.equals(AppConstant.married)) {
                    binding.layoutFirstName.hint = "Spouse First Name"
                    binding.layoutMiddleName.hint = "Spouse Middle Name"
                    binding.layoutLastName.hint = "Spouse Last Name"
                }
                if (marriage_type.equals(AppConstant.separated)) {
                    binding.layoutFirstName.hint = "Legal Spouse First Name"
                    binding.layoutMiddleName.hint = "Legal Spouse Middle Name"
                    binding.layoutLastName.hint = "Legal Spouse Last Name"
                }
            } else
                binding.tvQuestion.text = getString(R.string.married_to_coborower)

        } else if(ownTypeId == 2){

        }

        setData()

        return binding.root
    }

    private fun setData(){
        try {
            maritalStatus = arguments?.getParcelable(AppConstant.marital_status)!!
            if(maritalStatus != null){
                 maritalStatus?.spouseBorrowerId?.let { selectedId->
                    for(item in coborrowerList){
                        if (item.borrowerId == selectedId){
                            binding.yesRadioBtn.isChecked = true
                            binding.tvCoborrower.setText(item.coborrowerFirstName.plus( " ").plus(item.coborrowerLastName), false)
                            CustomMaterialFields.setColor(binding.layoutCoborrower, R.color.grey_color_two, requireActivity())
                            break
                        }
                    }
                }
                maritalStatus?.firstName?.let { firstName->
                    binding.noRadioBtn.isChecked = true
                    if(firstName.isNotEmpty() && firstName.isNotBlank()){
                        binding.etFirstName.setText(firstName)
                    }
                }
                maritalStatus?.middleName?.let { middleName->
                    binding.noRadioBtn.isChecked = true
                    if(middleName.isNotEmpty() && middleName.isNotBlank()){
                        binding.etMiddleName.setText(middleName)
                    }
                }
                maritalStatus?.lastName?.let { lastName->
                    binding.noRadioBtn.isChecked = true
                    if(lastName.isNotEmpty() && lastName.isNotBlank()){
                        binding.etLastName.setText(lastName)
                    }
                }

                // if spouse borrower id null && names are also null
            }
        } catch (e:Exception){ }

    }

    private fun setCoBorrowers(){
        val activity = (activity as? BorrowerAddressActivity)
        activity?.borrowerInfoList?.let { list->
            coBorrowerCount = list.size
            if(list.size > 0){
                val itemList: ArrayList<String> = arrayListOf()
                for(item in list.indices) {
                    if (list.get(item).owntypeId == 2) {
                        itemList.add(list.get(item).firstName + " " + list.get(item).lastName)
                        coborrowerList.add(CoborrowerList(list.get(item).borrowerId, list.get(item).owntypeId, list.get(item).firstName!!, list.get(item).lastName!!))
                    }
                }
                //Log.e("coborrowerList",""+coborrowerList)


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

    private fun saveData(){
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
            if(loanApplicationId != null){

                var firstName: String? = null
                var lastName: String? = null
                var middleName: String? = null
                var spouseBorrowerId: Int? = null
                if(binding.spouseInfoLayout.isVisible){
                    firstName = if (binding.etFirstName.text.toString().trim().length > 0) binding.etFirstName.text.toString() else null
                    lastName = if (binding.etLastName.text.toString().trim().length > 0) binding.etLastName.text.toString() else null
                    middleName = if (binding.etMiddleName.text.toString().trim().length > 0) binding.etMiddleName.text.toString() else null
                }

                if(binding.layoutCoborrower.isVisible){
                    val coborrowerName : String = binding.tvCoborrower.getText().toString().trim()
                    if(coborrowerName.length > 0 && coborrowerList.size > 0){
                        for(i in coborrowerList.indices){
                            var name: String =  coborrowerList.get(i).coborrowerFirstName.plus(" ").plus(coborrowerList.get(i).coborrowerLastName)
                            if(name.equals(coborrowerName,true)){
                                spouseBorrowerId = coborrowerList.get(i).borrowerId // get borrower id coborrower
                                break
                            }
                        }
                    }
                    //Log.e("SpouseId",  ""+spouseBorrowerId)
                }

                // don't sent   relationWithPrimaryId = null, spouseMaritalStatusId for updated

                var maritalStatusId : Int =  if(marriage_type.equals(AppConstant.married)) 1 else 2

                val maritalStatus = MaritalStatus(
                    loanApplicationId = loanApplicationId!!,
                    borrowerId = borrowerId!!,
                    firstName = firstName,
                    middleName = middleName ,
                    lastName = lastName,
                    isInRelationship = null,
                    otherRelationshipExplanation = null ,
                    relationFormedStateId = null,
                    relationshipTypeId = null,
                    spouseBorrowerId = spouseBorrowerId,
                    spouseLoanContactId = null,
                    maritalStatusId = maritalStatusId)

                Log.e("MarriageFrag","$maritalStatus")

                findNavController().previousBackStackEntry?.savedStateHandle?.set(AppConstant.marital_status, maritalStatus)
                findNavController().popBackStack()
            }
        }
    }

    private fun initViews(){
        binding.etFirstName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.etFirstName, binding.layoutFirstName, requireContext(),getString(R.string.error_field_required)))
        binding.etMiddleName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.etMiddleName, binding.layoutMiddleName, requireContext(),getString(R.string.error_field_required)))
        binding.etLastName.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.etLastName, binding.layoutLastName, requireContext(),getString(R.string.error_field_required)))

        binding.btnSave.setOnClickListener{
            saveData()
        }
        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.yesRadioBtn.setOnCheckedChangeListener { _, isChecked ->
            if(isChecked){
                binding.layoutCoborrower.visibility = View.VISIBLE
                //binding.tvCoborrower.setText("")
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