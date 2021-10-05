package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.inputmethod.EditorInfo
import android.view.inputmethod.InputMethodManager
import android.widget.TextView
import androidx.activity.addCallback
import androidx.core.view.isVisible
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.viewpager2.widget.ViewPager2
import androidx.viewpager2.widget.ViewPager2.OnPageChangeCallback
import com.facebook.shimmer.ShimmerFrameLayout
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator
import com.rnsoft.colabademo.databinding.FragmentSearchBinding
import com.rnsoft.colabademo.databinding.RequestDocsSearchLayoutBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

private val docsTypeTabArray = arrayOf(
    "Document Templates",
    "Document List"
)

@AndroidEntryPoint
class RequestDocSearchFragment : BaseFragment() {

    @Inject
    lateinit var sharedPreferences: SharedPreferences
    private var _binding: RequestDocsSearchLayoutBinding? = null
    private val binding get() = _binding!!


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {

        _binding = RequestDocsSearchLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root


        val linearLayoutManager = LinearLayoutManager(activity)


        binding.searchEditTextField.setOnEditorActionListener(TextView.OnEditorActionListener { v, actionId, event ->
            if (actionId == EditorInfo.IME_ACTION_SEARCH) {
                binding.searchEditTextField.clearFocus()
                binding.searchEditTextField.hideKeyboard()

                return@OnEditorActionListener true
            }
            false
        })

        binding.searchEditTextField.addTextChangedListener(object : TextWatcher {
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
            override fun afterTextChanged(s: Editable) {
                val str: String = binding.searchEditTextField.text.toString()
                if(str.isNotEmpty())
                    binding.searchcrossImageView.visibility = View.VISIBLE
                else
                    binding.searchcrossImageView.visibility = View.INVISIBLE
            }
        })

        setFocusToSearchField()

        binding.searchcrossImageView.setOnClickListener{
            binding.searchEditTextField.setText("")
            binding.searchEditTextField.clearFocus()
            binding.searchEditTextField.hideKeyboard()
            binding.searchcrossImageView.visibility = View.INVISIBLE



        }

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
        }


        return root
    }


    private fun setFocusToSearchField(){
        binding.searchEditTextField.setFocusableInTouchMode(true);
        binding.searchEditTextField.requestFocus();
        //val inputMethodManager =  binding.searchEditTextField.context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        //inputMethodManager.showSoftInput(binding.searchEditTextField, InputMethodManager.SHOW_IMPLICIT)
        showSoftKeyboard(binding.searchEditTextField)
    }

    private fun showSoftKeyboard(view: View) {
        if (view.requestFocus()) {
            val imm = view.context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager?

            // here is one more tricky issue
            // imm.showSoftInputMethod doesn't work well
            // and imm.toggleSoftInput(InputMethodManager.SHOW_IMPLICIT, 0) doesn't work well for all cases too
            imm?.toggleSoftInput(InputMethodManager.SHOW_FORCED, 0)
        }
    }

    private fun View.hideKeyboard() {
        val imm = context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        imm.hideSoftInputFromWindow(windowToken, 0)
    }


}

