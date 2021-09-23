package com.rnsoft.colabademo

import android.content.res.ColorStateList
import android.location.Address
import android.location.Geocoder
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.google.android.gms.common.api.ApiException
import com.google.android.gms.maps.model.LatLng
import com.google.android.libraries.places.api.Places
import com.google.android.libraries.places.api.model.AutocompletePrediction
import com.google.android.libraries.places.api.model.AutocompleteSessionToken
import com.google.android.libraries.places.api.model.RectangularBounds
import com.google.android.libraries.places.api.model.TypeFilter
import com.google.android.libraries.places.api.net.FindAutocompletePredictionsRequest
import com.google.android.libraries.places.api.net.FindAutocompletePredictionsResponse
import com.google.android.libraries.places.api.net.PlacesClient
import com.rnsoft.colabademo.databinding.SubjectPropertyAddressBinding
import com.rnsoft.colabademo.utils.CustomMaterialFields
import com.rnsoft.colabademo.utils.HideSoftkeyboard
import java.io.IOException
import java.util.*
import kotlin.collections.ArrayList

/**
 * Created by Anita Kiran on 9/15/2021.
 */
class IncomeAddress : Fragment() , PlacePredictionAdapter.OnPlaceClickListener {

    lateinit var binding: SubjectPropertyAddressBinding
    private lateinit var predictAdapter: PlacePredictionAdapter
    private lateinit var token: AutocompleteSessionToken
    private lateinit var placesClient: PlacesClient
    private var predicationList: ArrayList<String> = ArrayList()


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = SubjectPropertyAddressBinding.inflate(inflater, container, false)


        val title = arguments?.getString("address").toString()
        //Log.e("title", title)
        binding.titleTextView.setText(title)

        binding.backButton.setOnClickListener {
            findNavController().popBackStack()
            //findNavController().navigate(R.id.action_exit_address)

        }

        setInputFields()
        setStateAndCountyDropDown()
        setUpCompleteViewForPlaces()
        initializeUSAstates()

        binding.parentLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(), binding.parentLayout)
        }

        binding.subPropertyAddressLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(), binding.subPropertyAddressLayout)
        }

        binding.btnSave.setOnClickListener {
            //checkValidations()
            findNavController().popBackStack()
        }

        return binding.root

    }

    /*override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        ViewCompat.setTranslationZ(view, 1F)
    } */

    private fun setInputFields() {

        /*binding.tvSearch.onFocusChangeListener = object : View.OnFocusChangeListener {
            override fun onFocusChange(p0: View?, p1: Boolean) {
                if (p1) {
                    Log.e("has Focus", "true")
                    binding.searchSeparator.minimumHeight = 1
                    binding.searchSeparator.setBackgroundColor(resources.getColor(R.color.colaba_primary_color, requireActivity().theme))

                } else {
                    Log.e("has Focus", "removed")

                    binding.searchSeparator.minimumHeight = 1
                    binding.searchSeparator.setBackgroundColor(resources.getColor(R.color.grey_color_four, requireActivity().theme))
                    binding.searchSeparator.clearFocus()

                    if(binding.tvSearch.text.toString().isNotEmpty()){
                        CustomMaterialFields.setColor(binding.layoutSearchAddress, R.color.grey_color_two, requireContext())
                    }
                }
           }
        } */

        binding.layoutSearchAddress.setOnFocusChangeListener { view, hasFocus ->
            if (hasFocus) {
                //setTextInputLayoutHintColor(bi.layoutLastName, R.color.grey_color_two )
                CustomMaterialFields.setColor(
                    binding.layoutSearchAddress,
                    R.color.grey_color_two,
                    requireActivity()
                )
            } else {
                val search: String = binding.tvSearch.text.toString()
                if (search.length == 0) {
                    CustomMaterialFields.setColor(
                        binding.layoutSearchAddress,
                        R.color.grey_color_three,
                        requireActivity()
                    )
                } else {
                    CustomMaterialFields.setColor(
                        binding.layoutSearchAddress,
                        R.color.grey_color_three,
                        requireActivity()
                    )
                    CustomMaterialFields.clearError(
                        binding.layoutSearchAddress,
                        requireActivity()
                    )
                }
            }
        }


        // set lable focus
        binding.edUnitAtpNo.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edUnitAtpNo,
                binding.layoutUnitAptNo,
                requireContext()
            )
        )
        binding.edStreetAddress.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edStreetAddress,
                binding.layoutStreetAddress,
                requireContext()
            )
        )
        binding.edCity.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edCity,
                binding.layoutCity,
                requireContext()
            )
        )
        binding.edCounty.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edCounty,
                binding.layoutCounty,
                requireContext()
            )
        )
        binding.edZipcode.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.edZipcode,
                binding.layoutZipCode,
                requireContext()
            )
        )
        //binding.tvCountrySpinner.setOnFocusChangeListener(MyCustomFocusListener(binding.tvCountrySpinner, binding.layoutCountry, requireContext()))
        //binding.tvStateSpinner.setOnFocusChangeListener(MyCustomFocusListener(binding.tvStateSpinner, binding.layoutState, requireContext()))

        CustomMaterialFields.onTextChangedLableColor(
            requireActivity(),
            binding.edUnitAtpNo,
            binding.layoutUnitAptNo
        )
        CustomMaterialFields.onTextChangedLableColor(
            requireActivity(),
            binding.edStreetAddress,
            binding.layoutStreetAddress
        )
        CustomMaterialFields.onTextChangedLableColor(
            requireActivity(),
            binding.edCity,
            binding.layoutCity
        )
        CustomMaterialFields.onTextChangedLableColor(
            requireActivity(),
            binding.edCounty,
            binding.layoutCounty
        )
        CustomMaterialFields.onTextChangedLableColor(
            requireActivity(),
            binding.edZipcode,
            binding.layoutZipCode
        )

    }

    private fun checkValidations() {

        val searchBar: String = binding.tvSearch.text.toString()
//        val purchasePrice: String = binding.edPurchasePrice.text.toString()
//        val loanAmount: String = binding.edLoanAmount.text.toString()
//        val downPayment: String = binding.edDownPayment.text.toString()
//        val percentage: String = binding.edPercent.text.toString()
//        val closingDate: String = binding.edClosingDate.text.toString()

        if (searchBar.isEmpty() || searchBar.length == 0) {
            CustomMaterialFields.setError(
                binding.layoutSearchAddress,
                getString(R.string.error_field_required),
                requireActivity()
            )
        }

        if (searchBar.isNotEmpty() || searchBar.length > 0) {
            CustomMaterialFields.clearError(binding.layoutSearchAddress, requireActivity())
        }

    }

    private fun setStateAndCountyDropDown() {

        val countryAdapter =
            ArrayAdapter(
                requireContext(),
                R.layout.autocomplete_text_view,
                AppSetting.countries
            )
        binding.tvCountrySpinner.setAdapter(countryAdapter)

        binding.tvCountrySpinner.setOnFocusChangeListener { _, _ ->
            binding.tvCountrySpinner.showDropDown()
        }
        binding.tvCountrySpinner.setOnClickListener {
            binding.tvCountrySpinner.showDropDown()
        }

        binding.tvCountrySpinner.onItemClickListener =
            object : AdapterView.OnItemClickListener {
                override fun onItemClick(
                    p0: AdapterView<*>?,
                    p1: View?,
                    position: Int,
                    id: Long
                ) {
                    binding.layoutCountry.defaultHintTextColor = ColorStateList.valueOf(
                        ContextCompat.getColor(
                            requireContext(),
                            R.color.grey_color_two
                        )
                    )
                }
            }


        val stateAdapter =
            ArrayAdapter(requireContext(), R.layout.autocomplete_text_view, AppSetting.states)
        binding.tvStateSpinner.setAdapter(stateAdapter)

        binding.tvStateSpinner.setOnFocusChangeListener { _, _ ->
            binding.tvStateSpinner.showDropDown()
        }
        binding.tvStateSpinner.setOnClickListener {
            binding.tvStateSpinner.showDropDown()
        }

        binding.tvStateSpinner.onItemClickListener =
            object : AdapterView.OnItemClickListener {
                override fun onItemClick(
                    p0: AdapterView<*>?,
                    p1: View?,
                    position: Int,
                    id: Long
                ) {
                    binding.layoutState.defaultHintTextColor = ColorStateList.valueOf(
                        ContextCompat.getColor(requireContext(), R.color.grey_color_two)
                    )
                }
            }

    }

    private fun setUpCompleteViewForPlaces() {

        binding.recyclerviewPlaces.apply {
            this.setHasFixedSize(true)
            predictAdapter = PlacePredictionAdapter(this@IncomeAddress)
            this.adapter = predictAdapter
        }
        // Create a new token for the autocomplete session. Pass this to FindAutocompletePredictionsRequest,
        // and once again when the user makes a selection (for example when calling fetchPlace()).
        token = AutocompleteSessionToken.newInstance()

        Places.initialize(requireContext(), "AIzaSyBzPEiQOTReBzy6W1UcIyHApPu7_5Die6w")
        // Create a new Places client instance.
        placesClient = Places.createClient(requireContext())


        binding.tvSearch.dropDownHeight = 0
        binding.tvSearch.setOnFocusChangeListener { p0: View?, hasFocus: Boolean ->
            if (hasFocus)
                binding.tvSearch.addTextChangedListener(placeTextWatcher)
            else
                binding.tvSearch.removeTextChangedListener(placeTextWatcher)
        }

        binding.tvSearch.setOnClickListener { }
    }

    private val placeTextWatcher = (object : TextWatcher {
        override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
        override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
        override fun afterTextChanged(s: Editable) {
            val str: String = binding.tvSearch.text.toString()
            if (str.length >= 3) {
                searchForGooglePlaces(str)
            } else
                if (str.length in 0..2) {
                    binding.recyclerviewPlaces.visibility = View.GONE
                    predictAdapter.setPredictions(null)
                } else if (str.isEmpty()) {
                    binding.recyclerviewPlaces.visibility = View.GONE
                    predictAdapter.setPredictions(null)
                }
        }
    })

    private val bounds = RectangularBounds.newInstance(
        LatLng(24.7433195, -124.7844079),
        LatLng(49.3457868, -66.9513812)
    )

    private fun searchForGooglePlaces(queryPlace: String) {

        val TAG = "SubProperty-Search"
        binding.recyclerviewPlaces.visibility = View.VISIBLE

        val request =
            FindAutocompletePredictionsRequest.builder()
                // Call either setLocationBias() OR setLocationRestriction().
                .setLocationBias(bounds)
                //.setLocationRestriction(bounds)
                .setOrigin(LatLng(37.0902, 95.7129))
                //s.setCountries("USA")
                .setTypeFilter(TypeFilter.ADDRESS)
                .setSessionToken(token)
                .setQuery(queryPlace)
                .build()

        placesClient.findAutocompletePredictions(request)
            .addOnSuccessListener { response: FindAutocompletePredictionsResponse ->
                for (prediction in response.autocompletePredictions) {
                    Log.e(TAG, prediction.placeId)
                    response.autocompletePredictions
                    predicationList.add(prediction.getFullText(null).toString())
                    Log.e(TAG, prediction.getFullText(null).toString())

                }
                predictAdapter.setPredictions(response.autocompletePredictions)

            }.addOnFailureListener { exception: Exception? ->
                if (exception is ApiException) {
                    //Log.e(TAG, "Place not found: " + exception.statusCode)
                }
            }

        //Log.e("predicationList", predicationList.size.toString())

    }


    override fun onPlaceClicked(place: AutocompletePrediction) {
        //Log.e("which-place", "desc = " + place.getFullText(null).toString())
        predictAdapter.setPredictions(null)
        //binding.searchSeparator.visibility = View.GONE
        val placeSelected = place.getFullText(null).toString()
        binding.tvSearch.clearFocus()
        HideSoftkeyboard.hide(requireActivity(), binding.tvSearch)
        binding.tvSearch.removeTextChangedListener(placeTextWatcher)
        binding.tvSearch.setText(placeSelected)
        binding.tvSearch.clearFocus()

        val geocoder = Geocoder(requireContext(), Locale.getDefault())
        try {
            val addresses: List<Address>? =
                geocoder.getFromLocationName(place.getFullText(null).toString(), 1)
            val countryName: String? = addresses?.get(0)?.countryName
            //val stateName: String? = addresses?.get(0)?.locale
            val locality: String? = addresses?.get(0)?.locality
            val subLocality: String? = addresses?.get(0)?.subLocality
            val postalCode: String? = addresses?.get(0)?.postalCode
            val featureName: String? = addresses?.get(0)?.featureName
            val addressLine: String? = addresses?.get(0)?.getAddressLine(0)
            val premises: String? = addresses?.get(0)?.premises
            val countryCode: String? = addresses?.get(0)?.countryCode


            locality?.let { binding.edCity.setText(it) }
            subLocality?.let { binding.edCounty.setText(it) }
            postalCode?.let { binding.edZipcode.setText(it) }
            countryName?.let { binding.tvCountrySpinner.setText(it) }
            binding.edStreetAddress.setText(place.getPrimaryText(null))
            premises?.let { binding.edUnitAtpNo.setText(it) }

            //Log.e("Bingo ", " = " + subLocality + " " + locality + "  " + postalCode)
        } catch (e: IOException) {
            e.printStackTrace()
        }

        val extractState = place.getFullText(null)
        var stateCode = extractState.substring(
            extractState.lastIndexOf(",") - 2,
            extractState.lastIndexOf(",")
        )

        stateCode = stateCode.capitalize()
        //Log.e("stateCode ", " = $stateCode")
        //Log.e("Test State - ", " = " +map.get("LA") +"  "+map.get(stateCode))

        if (map.get(stateCode) != null)
            binding.tvStateSpinner.setText(map.get(stateCode))
        else
            binding.tvStateSpinner.setText("")

        visibleAllFields()
    }

    private fun visibleAllFields() {
        binding.layoutCity.visibility = View.VISIBLE
        binding.layoutCounty.visibility = View.VISIBLE
        binding.layoutCountry.visibility = View.VISIBLE
        binding.layoutZipCode.visibility = View.VISIBLE
        binding.layoutUnitAptNo.visibility = View.VISIBLE
        binding.layoutStreetAddress.visibility = View.VISIBLE
        binding.layoutState.visibility = View.VISIBLE
    }

    private var map: HashMap<String, String> = HashMap()

    private fun initializeUSAstates() {
        map.put("AL", "Alabama")
        map.put("AK", "Alaska")
        map.put("AZ", "Arizona")
        map.put("AR", "Arkansas")
        map.put("CA", "California")
        map.put("CO", "Colorado")
        map.put("CT", "Connecticut")
        map.put("DE", "Delaware")
        map.put("DC", "District of Columbia")
        map.put("FL", "Florida")
        map.put("GA", "Georgia")
        map.put("HI", "Hawaii")
        map.put("ID", "Idaho")
        map.put("IL", "Illinois")
        map.put("IN", "Indiana")
        map.put("IA", "Iowa")
        map.put("KS", "Kansas")
        map.put("KY", "Kentucky")
        map.put("LA", "Louisiana")
        map.put("ME", "Maine")
        map.put("MD", "Maryland")
        map.put("MA", "Massachusetts")
        map.put("MI", "Michigan")
        map.put("MN", "Minnesota")
        map.put("MS", "Mississippi")
        map.put("MO", "Missouri")
        map.put("MT", "Montana")
        map.put("NE", "Nebraska")
        map.put("NV", "Nevada")
        map.put("NH", "New Hampshire")
        map.put("NJ", "New Jersey")
        map.put("NM", "New Mexico")
        map.put("NY", "New York")
        map.put("NC", "North Carolina")
        map.put("ND", "North Dakota")
        map.put("OH", "Ohio")
        map.put("OK", "Oklahoma")
        map.put("OR", "Oregon")
        map.put("PA", "Pennsylvania")
        map.put("RI", "Rhode Island")
        map.put("SC", "South Carolina")
        map.put("SD", "South Dakota")
        map.put("TN", "Tennessee")
        map.put("TX", "Texas")
        map.put("UT", "Utah")
        map.put("VT", "Vermont")
        map.put("VA", "Virginia")
        map.put("WA", "Washington")
        map.put("WV", "West Virginia")
        map.put("WI", "Wisconsin")
        map.put("WY", "Wyoming")
    }

}