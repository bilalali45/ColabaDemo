package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.Resources
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.rnsoft.colabademo.databinding.ActiveDutyLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject
import android.util.Log
import android.widget.DatePicker
import java.lang.Exception



@AndroidEntryPoint
class ActiveDutyFragment : Fragment() {

    private var _binding: ActiveDutyLayoutBinding? = null
    private val binding get() = _binding!!

    val c = Calendar.getInstance()
    val year = c.get(Calendar.YEAR)
    val month = c.get(Calendar.MONTH)
    val day = c.get(Calendar.DAY_OF_MONTH)

    private lateinit var dpd:DatePickerDialog

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = ActiveDutyLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        dpd = DatePickerDialog( requireContext(), DatePickerDialog.OnDateSetListener { view, year, monthOfYear, dayOfMonth ->
            // Display Selected date in textbox
            //lblDate.setText("" + dayOfMonth + " " + MONTHS[monthOfYear] + ", " + year)
            var stringMonth = monthOfYear.toString()
            if(monthOfYear<10)
                stringMonth = "0$monthOfYear"
            binding.edEmail.setText(stringMonth + " / " + year)
        }, year, month, day)

        binding.edEmail.setOnClickListener{
            //createDialogWithoutDateField().show()
            dpd.show()
        }
        binding.edEmail.setOnFocusChangeListener{ _ , _ ->
            //createDialogWithoutDateField().show()
            dpd.show()
        }


        //binding.edEmail.setOnTouchListener(otl)
        binding.edEmail.showSoftInputOnFocus = false


        return root

    }


    // https://github.com/premkumarroyal/MonthAndYearPicker

    // https://github.com/ma7madfawzy/dialogPlus

    /*
    private val otl = OnTouchListener { v, event ->
        dpd.show()
        true // the listener has consumed the event
    }
     */

}