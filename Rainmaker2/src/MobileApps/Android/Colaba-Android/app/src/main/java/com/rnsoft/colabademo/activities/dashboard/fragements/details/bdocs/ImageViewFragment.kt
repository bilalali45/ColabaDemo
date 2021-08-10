package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import androidx.fragment.app.Fragment
import com.bumptech.glide.Glide
import com.rnsoft.colabademo.databinding.ImageViewLayoutBinding
import java.io.File
import javax.inject.Inject

class ImageViewFragment : Fragment(), AdapterClickListener {
    private var _binding: ImageViewLayoutBinding? = null
    private val binding get() = _binding!!

    private lateinit var imageFileName:String
    lateinit var imageView: ImageView


    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = ImageViewLayoutBinding.inflate(inflater, container, false)
        val view: View = binding.root

        imageView = view.findViewById(R.id.imagesImageView)
        Log.e("launch-pdf", "this has been called...")
        imageFileName = arguments?.getString(AppConstant.downloadedFileName).toString()
        val file = File(requireContext().filesDir, imageFileName )
        Glide.with(requireActivity())
            .load(file) // Uri of the picture
            .into(imageView)

        return view
    }

    override fun navigateTo(position: Int) {

    }
    override fun getCardIndex(position: Int) {

    }


}