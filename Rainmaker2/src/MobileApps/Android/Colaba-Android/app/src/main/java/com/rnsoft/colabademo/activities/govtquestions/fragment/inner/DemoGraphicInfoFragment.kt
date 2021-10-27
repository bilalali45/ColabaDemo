package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.CheckBox
import android.widget.CompoundButton
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.DemographicInfoLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import timber.log.Timber
import javax.inject.Inject

@AndroidEntryPoint
class DemoGraphicInfoFragment : BaseFragment() {

    private val viewModel: BorrowerApplicationViewModel by activityViewModels()
    private var _binding: DemographicInfoLayoutBinding? = null
    private val binding get() = _binding!!
    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private var newRaceList : ArrayList<RaceResponseModel> = ArrayList()


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = DemographicInfoLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root
        super.addListeners(binding.root)

        val li = layoutInflater
        val view = li.inflate(R.layout.independent_checkbox,null)


        viewModel.raceList.observe(viewLifecycleOwner,{

            if(it.size >0 ){
                for (i in 0 until it.size) {
                    val checkBox = CheckBox(requireContext())
                    checkBox.text = it.get(i).name
                    checkBox.setPadding(25, 20, 0, 0)
                    checkBox.id = it.get(i).id
                    checkBox.setOnCheckedChangeListener(handleRaceCheck(checkBox,checkBox.id))
                    newRaceList.add(RaceResponseModel(id= it.get(i).id,name= it.get(i).name, raceDetails = it.get(i).raceDetails))
                    binding.layoutRace.addView(checkBox)
                }
            }
        })



        /*

         for (j in 0 until it.get(i).raceDetails.size){
                            val checkBox = CheckBox(requireContext())
                            checkBox.text = it.get(i).raceDetails.get(j).name
                            Log.e("Detils",it.get(i).raceDetails.get(j).name)
                            checkBox.setPadding(50, 20, 0, 0)
                            checkBox.id = j
                        }
         */


        return root
    }

    private fun handleRaceCheck(chk: CheckBox, chkBoxId: Int): CompoundButton.OnCheckedChangeListener? {
        return object : CompoundButton.OnCheckedChangeListener {
            override fun onCheckedChanged(buttonView: CompoundButton?, isChecked: Boolean) {
                if (!isChecked) {
                    //uncheck
                } else {
                    //Timber.e("findCheckBox:  " + chk.id)
                   // Timber.e("Details- $newRaceList")
                    for(i in 0 until newRaceList.size){
                        if(newRaceList.get(i).id == chkBoxId){
                            if(newRaceList.get(i).raceDetails.size > 0) {
                                Log.e("chkbox.id ", ""+newRaceList.get(i).raceDetails)
                                val test = newRaceList.get(i).raceDetails
                                //Timber.e("testing == "+test.size+ " "+test.toArray().toString())
                                //for(item in test )
                                   // Timber.e(" item "+item)
                                val bundle = Bundle()
                                bundle.putParcelableArrayList("racedetail", newRaceList.get(i).raceDetails)
                                findNavController().navigate(R.id.navigation_race_details , bundle)
                                break
                            }
                        }
                    }
                }
            }
        }
    }


    /*
    private fun setUpDemoGraphicScreen() {
        binding.asianCheckBox.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                binding.asianInnerConstraintLayout.visibility = View.VISIBLE
            }else{
                binding.asianInnerConstraintLayout.visibility = View.GONE
            }
        }

        binding.nativeHawaianOrOtherCheckBox.setOnCheckedChangeListener { buttonView, isChecked ->
            if (isChecked) {
                binding.nativeHawaianInnerLayout.visibility = View.VISIBLE
            }else{
                binding.nativeHawaianInnerLayout.visibility = View.GONE
            }
        }


        binding.hispanicOrLatino.setOnClickListener {
            binding.hispanicOrLatinoLayout.visibility = View.VISIBLE
            binding.notHispanic.isChecked = false
            binding.notTellingEthnicity.isChecked = false
        }

        binding.notHispanic.setOnClickListener{


            binding.hispanicOrLatinoLayout.visibility = View.GONE
            binding.hispanicOrLatino.isChecked = false
            binding.notTellingEthnicity.isChecked = false
        }

        binding.notTellingEthnicity.setOnClickListener{

            binding.hispanicOrLatinoLayout.visibility = View.GONE
            binding.hispanicOrLatino.isChecked = false
            binding.notHispanic.isChecked = false
        }

        binding.otherAsianCheckBox.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherAsianConstraintlayout.visibility = View.VISIBLE
            else
                binding.otherAsianConstraintlayout.visibility = View.GONE
        }

        binding.otherHispanicOrLatino.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherHispanicConstraintLayout.visibility = View.VISIBLE
            else
                binding.otherHispanicConstraintLayout.visibility = View.GONE
        }

        binding.otherPacificIslanderCheckBox.setOnCheckedChangeListener{ buttonView, isChecked ->
            if(isChecked)
                binding.otherPacificIslanderConstraintLayout.visibility = View.VISIBLE
            else
                binding.otherPacificIslanderConstraintLayout.visibility = View.GONE

        }
    }
     */
}