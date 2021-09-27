package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater

import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.AppCompatRadioButton
import androidx.appcompat.widget.AppCompatTextView
import androidx.constraintlayout.widget.ConstraintLayout
import com.rnsoft.colabademo.GovtQuestionBaseFragment
import com.rnsoft.colabademo.R
import com.rnsoft.colabademo.databinding.*


class BottomLoanProcessor : BottomBorrowerBaseFragment() {

    private lateinit var binding: BottomBorrowerCommonLayoutBinding


    private lateinit var test: ConstraintLayout

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = BottomBorrowerCommonLayoutBinding.inflate(inflater, container, false)
        setupLayout()

        super.addListeners(binding.root)
        return binding.root
    }


    private fun setupLayout() {
        setUpTabs()
    }

    private fun setUpTabs() {}
}