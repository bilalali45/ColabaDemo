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
import com.rnsoft.colabademo.databinding.*
import dagger.hilt.android.AndroidEntryPoint
import java.util.*
import javax.inject.Inject
import javax.xml.datatype.DatatypeConstants.MONTHS
import kotlin.math.roundToInt


@AndroidEntryPoint
class ReserveFragment : Fragment() {

    private var _binding: ReserveLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = ReserveLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root


        return root

    }

}