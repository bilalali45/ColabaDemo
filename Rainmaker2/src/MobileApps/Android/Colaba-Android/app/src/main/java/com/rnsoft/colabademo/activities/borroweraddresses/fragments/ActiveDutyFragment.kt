package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
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
import androidx.navigation.fragment.findNavController
import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import java.lang.Exception

@AndroidEntryPoint
class ActiveDutyFragment : Fragment(), DatePickerDialog.OnDateSetListener {

    private var _binding: ActiveDutyLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = ActiveDutyLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        createCustomDialog()

        binding.edEmail.showSoftInputOnFocus = false
        binding.edEmail.setOnClickListener { createCustomDialog() }
        binding.edEmail.setOnFocusChangeListener{ _ , _ ->  createCustomDialog() }

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }


        return root

    }

    private fun createCustomDialog(){
        val pd = MonthYearPickerDialog()
        pd.setListener(this)
        pd.show(requireActivity().supportFragmentManager, "MonthYearPickerDialog")
    }

    override fun onDateSet(p0: DatePicker?, p1: Int, p2: Int, p3: Int) {
        var stringMonth = p2.toString()
        if(p2<10)
            stringMonth = "0$p2"

        val sampleDate = "$stringMonth / $p1"
        binding.edEmail.setText(sampleDate)
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