package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.inputmethod.InputMethodManager
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.DatePicker
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.google.android.gms.common.api.ApiException
import com.google.android.gms.maps.model.LatLng
import com.google.android.libraries.places.api.Places
import com.google.android.libraries.places.api.model.AutocompleteSessionToken
import com.google.android.libraries.places.api.model.RectangularBounds
import com.google.android.libraries.places.api.model.TypeFilter
import com.google.android.libraries.places.api.net.FindAutocompletePredictionsRequest
import com.google.android.libraries.places.api.net.FindAutocompletePredictionsResponse
import com.google.android.libraries.places.api.net.PlacesClient
import com.rnsoft.colabademo.databinding.TempResidenceLayoutBinding
import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.util.*
import javax.inject.Inject


@AndroidEntryPoint
class CurrentResidenceFragment : Fragment(), DatePickerDialog.OnDateSetListener {

    private var _binding: TempResidenceLayoutBinding? = null
    private val binding get() = _binding!!

    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {

        _binding = TempResidenceLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.moveInEditText.setOnClickListener { createCustomDialog() }
        binding.moveInEditText.setOnFocusChangeListener{ _ , p1 ->
            if(p1)
            createCustomDialog()
        }
        binding.moveInEditText.showSoftInputOnFocus = false

        binding.topSearchAutoTextView.onFocusChangeListener = object:View.OnFocusChangeListener{
            override fun onFocusChange(p0: View?, p1: Boolean) {
                if(p1) {
                    binding.topSearchTextInputLine.minimumHeight = 1
                    binding.topSearchTextInputLine.setBackgroundColor(resources.getColor( R.color.colaba_primary_color , requireActivity().theme))
                }
                else{
                    binding.topSearchTextInputLine.minimumHeight = 1
                    binding.topSearchTextInputLine.setBackgroundColor(resources.getColor(R.color.grey_color_four, requireActivity().theme))
                }
            }
        }


        binding.cityEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.cityEditText, binding.cityLayout, requireContext()))
        binding.streetAddressEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.streetAddressEditText, binding.streetAddressLayout, requireContext()))
        binding.unitAptInputEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.unitAptInputEditText, binding.unitAptInputLayout, requireContext()))
        binding.countyEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.countyEditText, binding.countyLayout, requireContext()))
        binding.zipcodeEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.zipcodeEditText, binding.zipcodeLayout, requireContext()))

        binding.monthlyRentEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.monthlyRentEditText, binding.monthlyRentLayout, requireContext()))
        //binding.housingEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.housingEditText, binding.housingLayout, requireContext()))
        //binding.moveInEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.moveInEditText, binding.moveInLayout, requireContext()))


        val countryAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view ,  AppSetting.countries)
        binding.countryCompleteTextView.setAdapter(countryAdapter)

        binding.countryCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.countryCompleteTextView.showDropDown()
        }
        binding.countryCompleteTextView.setOnClickListener{
            binding.countryCompleteTextView.showDropDown()
        }

        binding.countryCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.countryCompleteLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(),  R.color.grey_color_two))
            }
        }


        val stateAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view,  AppSetting.states)
        binding.stateCompleteTextView.setAdapter(stateAdapter)

        binding.stateCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.stateCompleteTextView.showDropDown()
        }
        binding.stateCompleteTextView.setOnClickListener{
            binding.stateCompleteTextView.showDropDown()
        }

        binding.stateCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                binding.stateCompleteTextInputLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(),  R.color.grey_color_two))
            }
        }




        val houseLivingTypeArray:ArrayList<String> = arrayListOf("Own", "Rent", "No Primary Housing Expense")
        val houseTypeAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view , houseLivingTypeArray)
        binding.housingCompleteTextView.setAdapter(houseTypeAdapter)

        binding.housingCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.housingCompleteTextView.showDropDown()
        }
        binding.housingCompleteTextView.setOnClickListener{
            binding.housingCompleteTextView.showDropDown()
        }

        binding.housingCompleteTextView.onItemClickListener = object: AdapterView.OnItemClickListener {
            override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                if(position == houseLivingTypeArray.size-2) {
                    binding.monthlyRentLayout.visibility = View.VISIBLE
                    binding.housingLayout.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(),  R.color.grey_color_two))
                }
               else
                    binding.monthlyRentLayout.visibility = View.GONE
            }
        }




        binding.addAddressLayout.setOnClickListener{
            findNavController().navigate(R.id.navigation_previous_address)
        }

        binding.backButton.setOnClickListener{
            val message = "Are you sure you want to delete Richard's Current Residence?"
            AddressNotSavingDialogFragment.newInstance(message).show(childFragmentManager, AddressNotSavingDialogFragment::class.java.canonicalName)
            //findNavController().popBackStack()
        }

        binding.delImageview.setOnClickListener {
            val message = "Are you sure you want to delete Richard's Current Residence?"
            AddressNotSavingDialogFragment.newInstance(message).show(childFragmentManager, AddressNotSavingDialogFragment::class.java.canonicalName)
        }



        return root

    }

    private fun initGooglePlaceSetup(){
        val TAG = "OTHER_WAY-"
        Places.initialize(requireContext(), "AIzaSyBzPEiQOTReBzy6W1UcIyHApPu7_5Die6w")
        // Create a new Places client instance.
        val placesClient: PlacesClient = Places.createClient(requireContext())

        // Create a new token for the autocomplete session. Pass this to FindAutocompletePredictionsRequest,
        // and once again when the user makes a selection (for example when calling fetchPlace()).
        val token = AutocompleteSessionToken.newInstance()

        // Create a RectangularBounds object.
        val bounds = RectangularBounds.newInstance(
            LatLng(-33.880490, 151.184363),
            LatLng(-33.858754, 151.229596)
        )
        // Use the builder to create a FindAutocompletePredictionsRequest.
        val request =
            FindAutocompletePredictionsRequest.builder()
                // Call either setLocationBias() OR setLocationRestriction().
                .setLocationBias(bounds)
                //.setLocationRestriction(bounds)
                .setOrigin(LatLng(-33.8749937, 151.2041382))
                .setCountries("AU", "NZ")
                .setTypeFilter(TypeFilter.ADDRESS)
                .setSessionToken(token)
                .setQuery("Queenstown")
                .build()
        placesClient.findAutocompletePredictions(request)
            .addOnSuccessListener { response: FindAutocompletePredictionsResponse ->
                for (prediction in response.autocompletePredictions) {
                    Log.e(TAG, prediction.placeId)

                    Log.e(TAG, prediction.getFullText(null).toString())
                    Log.e(TAG, prediction.getSecondaryText(null).toString())
                    Log.e(TAG, prediction.getPrimaryText(null).toString())
                }
            }.addOnFailureListener { exception: Exception? ->
                if (exception is ApiException) {
                    Log.e(TAG, "Place not found: " + exception.statusCode)
                }
            }
    }




    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onNotSavingAddressEvent(event: NotSavingAddressEvent) {
        if(event.boolean){
            findNavController().popBackStack()
        }
    }


    private fun hideKeyBoard() {
            val inputMethodManager = ContextCompat.getSystemService(requireContext(), InputMethodManager::class.java)!!
            inputMethodManager.hideSoftInputFromWindow(binding.moveInEditText.windowToken, 0)

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
        binding.moveInEditText.setText(sampleDate)
    }

}