package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.databinding.CurrentResidenceLayoutBinding
import com.rnsoft.colabademo.databinding.DetailBorrowerLayoutTwoBinding
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject
import javax.xml.datatype.DatatypeConstants.MONTHS
import kotlin.math.roundToInt


@AndroidEntryPoint
class CurrentResidenceFragment : Fragment() {

    private var _binding: CurrentResidenceLayoutBinding? = null
    private val binding get() = _binding!!

    val c = Calendar.getInstance()
    val year = c.get(Calendar.YEAR)
    val month = c.get(Calendar.MONTH)
    val day = c.get(Calendar.DAY_OF_MONTH)


    val dpd = DatePickerDialog( requireActivity(), DatePickerDialog.OnDateSetListener { view, year, monthOfYear, dayOfMonth ->
        // Display Selected date in textbox
        //lblDate.setText("" + dayOfMonth + " " + MONTHS[monthOfYear] + ", " + year)
    }, year, month, day)

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = CurrentResidenceLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root


        return root

    }

}