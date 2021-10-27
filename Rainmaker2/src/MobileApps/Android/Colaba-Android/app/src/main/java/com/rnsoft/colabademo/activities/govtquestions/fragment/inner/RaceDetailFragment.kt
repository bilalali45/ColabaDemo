package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.CheckBox
import com.rnsoft.colabademo.databinding.DemographicDetailLayoutBinding
import timber.log.Timber

/**
 * Created by Anita Kiran on 10/27/2021.
 */
class RaceDetailFragment : BaseFragment() {

    private var _binding: DemographicDetailLayoutBinding? = null
    private val binding get() = _binding!!
    private var detailList : ArrayList<AllRaceDetails> = ArrayList()
    val args = arguments

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = DemographicDetailLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        arguments?.let {
            detailList = it.getParcelableArrayList("racedetail")!!
            Timber.e("size******"+ detailList.size)
            /*
            if(detailList.size > 0) {
                for (i in 0 until detailList.size) {
                    val checkBox = CheckBox(requireContext())
                    checkBox.text = detailList.get(i).name
                    checkBox.setPadding(25, 20, 0, 0)
                    checkBox.id = detailList.get(i).id
                    //checkBox.setOnCheckedChangeListener(handleRaceCheck(checkBox,checkBox.id))
                    binding.layoutDetails.addView(checkBox)
                }
            }
             */
        }

       // detailList = arguments?.getParcelableArrayList("racedetail")

        return root

    }
}