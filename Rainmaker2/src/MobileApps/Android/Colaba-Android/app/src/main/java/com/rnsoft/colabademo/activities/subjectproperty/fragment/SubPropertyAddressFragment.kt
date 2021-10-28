package com.rnsoft.colabademo

import android.content.SharedPreferences
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
import androidx.activity.addCallback
import androidx.core.content.ContextCompat
import androidx.core.view.isVisible
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
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
import com.rnsoft.colabademo.databinding.CommonAddressLayoutBinding

import com.rnsoft.colabademo.utils.CustomMaterialFields
import dagger.hilt.android.AndroidEntryPoint

import kotlinx.android.synthetic.main.temp_residence_layout.*
import kotlinx.android.synthetic.main.view_placesearch.*
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.delay
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import java.io.IOException
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

/**
 * Created by Anita Kiran on 9/8/2021.
 */
@AndroidEntryPoint
class SubPropertyAddressFragment : BaseFragment(), PlacePredictionAdapter.OnPlaceClickListener {
    @Inject
    lateinit var sharedPreferences : SharedPreferences
    private val viewModel : SubjectPropertyViewModel by activityViewModels()
    private lateinit var binding: CommonAddressLayoutBinding
    private lateinit var predictAdapter: PlacePredictionAdapter
    private lateinit var token: AutocompleteSessionToken
    private lateinit var placesClient: PlacesClient
    private var predicationList: ArrayList<String> = ArrayList()
    private var addressList : ArrayList<AddressData> = ArrayList()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = CommonAddressLayoutBinding.inflate(inflater, container, false)

        setInputFields()
        getDropDownData()
        setUpCompleteViewForPlaces()
        initializeUSAstates()

        binding.addressParentLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(),binding.addressParentLayout)
            super.removeFocusFromAllFields(binding.addressLayout)
        }

        binding.addressLayout.setOnClickListener {
            HideSoftkeyboard.hide(requireActivity(),binding.addressLayout)
            super.removeFocusFromAllFields(binding.addressLayout)
        }

        binding.btnSave.setOnClickListener{
            checkValidations()
        }

        binding.backButton.setOnClickListener {
            //findNavController().navigate(R.id.action_back_fromAddress_toPurchase)
            findNavController().popBackStack()
        }

       /*requireActivity().onBackPressedDispatcher.addCallback {
           findNavController().navigate(R.id.action_back_fromAddress_toPurchase)
       } */


        super.addListeners(binding.root)
        return binding.root

    }

    private fun setData() {
        addressList = arguments?.getParcelableArrayList(AppConstant.address)!!
        if (addressList.size > 0) {
            addressList[0].street?.let {
                binding.tvSearch.setText(it)
                CustomMaterialFields.setColor(
                    binding.layoutSearchAddress,
                    R.color.grey_color_two,
                    requireActivity()
                )
            }
            addressList[0].street?.let { binding.edStreetAddress.setText(it) }
            addressList[0].city?.let { binding.edCity.setText(it) }
            addressList[0].countryName?.let {
                binding.tvCountry.setText(it)
                binding.layoutCountry.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(),
                        R.color.grey_color_two
                    )
                )
            }
            addressList[0].zipCode?.let { binding.edZipcode.setText(it) }
            addressList[0].stateName?.let {
                binding.tvState.setText(it)
                binding.layoutState.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(),
                        R.color.grey_color_two
                    )
                )
            }
            addressList[0].countyName?.let {
                binding.tvCounty.setText(it)
                binding.layoutCounty.defaultHintTextColor = ColorStateList.valueOf(
                    ContextCompat.getColor(
                        requireContext(),
                        R.color.grey_color_two
                    )
                )
            }
            addressList[0].unit?.let { binding.edUnitAtpNo.setText(it) }
            visibleAllFields()
        }
    }

    private fun getDropDownData(){
         lifecycleScope.launchWhenStarted {
             sharedPreferences.getString(AppConstant.token, "")?.let { authToken ->
             binding.loaderSubproAddress.visibility = View.VISIBLE
             delay(2000)
             coroutineScope {
                 setData()

                 viewModel.getStates(authToken)
                 // get countries
                 viewModel.getCountries(authToken)
                 // get county
                 viewModel.getCounty(authToken)

                 viewModel.states.observe(viewLifecycleOwner, { states ->
                     if (states != null && states.size > 0) {
                         val itemList: ArrayList<String> = arrayListOf()
                         for (item in states) {
                             itemList.add(item.name)
                         }
                         val stateAdapter =
                             ArrayAdapter(
                                 requireContext(),
                                 R.layout.autocomplete_text_view,
                                 itemList
                             )
                         binding.tvState.setAdapter(stateAdapter)

                         binding.tvState.setOnFocusChangeListener { _, _ ->
                             binding.tvState.showDropDown()
                             HideSoftkeyboard.hide(requireActivity(), binding.layoutCounty)
                         }
                         binding.tvState.setOnClickListener {
                             binding.tvState.showDropDown()
                             HideSoftkeyboard.hide(requireActivity(), binding.layoutState)
                         }

                         binding.tvState.onItemClickListener =
                             object : AdapterView.OnItemClickListener {
                                 override fun onItemClick(
                                     p0: AdapterView<*>?,
                                     p1: View?,
                                     position: Int,
                                     id: Long
                                 ) {
                                     binding.layoutState.defaultHintTextColor =
                                         ColorStateList.valueOf(
                                             ContextCompat.getColor(
                                                 requireContext(),
                                                 R.color.grey_color_two
                                             )
                                         )
                                     HideSoftkeyboard.hide(requireActivity(), binding.layoutState)
                                 }
                             }
                     }
                 })

                 viewModel.countries.observe(viewLifecycleOwner, { countries ->
                     if (countries != null && countries.size > 0) {
                         val itemList: ArrayList<String> = arrayListOf()
                         for (item in countries) {
                             itemList.add(item.name)
                         }
                         val countryAdapter =
                             ArrayAdapter(
                                 requireContext(),
                                 R.layout.autocomplete_text_view,
                                 itemList
                             )
                         binding.tvCountry.setAdapter(countryAdapter)

                         binding.tvCountry.setOnFocusChangeListener { _, _ ->
                             binding.tvCountry.showDropDown()
                             HideSoftkeyboard.hide(requireActivity(), binding.layoutCountry)
                         }
                         binding.tvCountry.setOnClickListener {
                             binding.tvCountry.showDropDown()
                             HideSoftkeyboard.hide(requireActivity(), binding.layoutCountry)
                         }

                         binding.tvCountry.onItemClickListener =
                             object : AdapterView.OnItemClickListener {
                                 override fun onItemClick(
                                     p0: AdapterView<*>?,
                                     p1: View?,
                                     position: Int,
                                     id: Long
                                 ) {
                                     binding.layoutCountry.defaultHintTextColor =
                                         ColorStateList.valueOf(
                                             ContextCompat.getColor(
                                                 requireContext(),
                                                 R.color.grey_color_two
                                             )
                                         )
                                     HideSoftkeyboard.hide(requireActivity(), binding.layoutCountry)
                                 }
                             }
                     }
                 })

                 viewModel.counties.observe(viewLifecycleOwner, { counties ->
                     if (counties != null && counties.size > 0) {
                         val itemList: ArrayList<String> = arrayListOf()
                         for (item in counties) {
                             itemList.add(item.name)
                         }
                         val countyAdapter = ArrayAdapter(
                             requireContext(),
                             R.layout.autocomplete_text_view,
                             itemList
                         )
                         binding.tvCounty.setAdapter(countyAdapter)

                         binding.tvCounty.setOnFocusChangeListener { _, _ ->
                             binding.tvCounty.showDropDown()
                             HideSoftkeyboard.hide(requireActivity(), binding.layoutCounty)
                         }

                         binding.tvCounty.setOnClickListener {
                             binding.tvCounty.showDropDown()
                             HideSoftkeyboard.hide(requireActivity(), binding.layoutCounty)
                         }

                         binding.tvCounty.onItemClickListener =
                             object : AdapterView.OnItemClickListener {
                                 override fun onItemClick(
                                     p0: AdapterView<*>?,
                                     p1: View?,
                                     position: Int,
                                     id: Long
                                 ) {
                                     binding.layoutCounty.defaultHintTextColor =
                                         ColorStateList.valueOf(
                                             ContextCompat.getColor(
                                                 requireContext(),
                                                 R.color.grey_color_two
                                             )
                                         )
                                     HideSoftkeyboard.hide(requireActivity(), binding.layoutCounty)
                                 }
                             }
                     }

                 })
             }
             binding.loaderSubproAddress.visibility = View.GONE
         }
     }

}

    private fun setInputFields(){
        // set lable focus
        binding.tvSearch.setOnFocusChangeListener { p0: View?, hasFocus: Boolean ->
            if (hasFocus) {
                //binding.searchSeparator.minimumHeight = 2
                binding.searchSeparator.layoutParams.height = 1
                binding.searchSeparator.setBackgroundColor(resources.getColor(R.color.colaba_apptheme_blue, requireActivity().theme))
                binding.tvSearch.addTextChangedListener(placeTextWatcher)
            } else {
                binding.tvSearch.removeTextChangedListener(placeTextWatcher)
                binding.searchSeparator.minimumHeight = 0.5.toInt()
                binding.searchSeparator.setBackgroundColor(resources.getColor(R.color.grey_color_four, requireActivity().theme))

                val search: String = binding.tvSearch.text.toString()
                if (search.length == 0) {
                    CustomMaterialFields.setColor(binding.layoutSearchAddress, R.color.grey_color_three, requireActivity())
                } else {
                    CustomMaterialFields.setColor(binding.layoutSearchAddress, R.color.grey_color_two, requireActivity())
                }
            }
        }

        binding.edUnitAtpNo.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edUnitAtpNo, binding.layoutUnitAptNo, requireContext()))
        binding.edStreetAddress.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edStreetAddress, binding.layoutStreetAddress , requireContext()))
        binding.edCity.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edCity, binding.layoutCity, requireContext()))
        //binding.edCounty.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edCounty, binding.layoutCounty, requireContext()))
        binding.edZipcode.setOnFocusChangeListener(CustomFocusListenerForEditText(binding.edZipcode, binding.layoutZipCode, requireContext()))

        CustomMaterialFields.onTextChangedLableColor(requireActivity(),binding.edUnitAtpNo, binding.layoutUnitAptNo)
        CustomMaterialFields.onTextChangedLableColor(requireActivity(),binding.edStreetAddress, binding.layoutStreetAddress)
        CustomMaterialFields.onTextChangedLableColor(requireActivity(),binding.edCity, binding.layoutCity)
        //CustomMaterialFields.onTextChangedLableColor(requireActivity(),binding.edCounty, binding.layoutCounty)
        CustomMaterialFields.onTextChangedLableColor(requireActivity(),binding.edZipcode, binding.layoutZipCode)
    }

    private fun checkValidations() {
        val searchBar: String = binding.tvSearch.text.toString()
        if (searchBar.isEmpty() || searchBar.length == 0) {
            setError()
        }
        if (searchBar.isNotEmpty() || searchBar.length > 0) {
            removeError()
            findNavController().popBackStack()
        }
    }

    private fun setStateAndCountyDropDown(){

        val countryAdapter =
            ArrayAdapter(requireContext(), R.layout.autocomplete_text_view, AppSetting.countries)
        binding.tvCountry.setAdapter(countryAdapter)

        binding.tvCountry.setOnFocusChangeListener { _, _ ->
            binding.tvCountry.showDropDown()
        }
        binding.tvCountry.setOnClickListener {
            binding.tvCountry.showDropDown()
        }

        binding.tvCountry.onItemClickListener =
            object : AdapterView.OnItemClickListener {
                override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                    binding.layoutCountry.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two))
                }
            }


        val stateAdapter =
            ArrayAdapter(requireContext(), R.layout.autocomplete_text_view, AppSetting.states)
        binding.tvState.setAdapter(stateAdapter)

        binding.tvState.setOnFocusChangeListener { _, _ ->
            binding.tvState.showDropDown()
        }
        binding.tvState.setOnClickListener {
            binding.tvState.showDropDown()
        }

        binding.tvState.onItemClickListener =
            object : AdapterView.OnItemClickListener {
                override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                    binding.layoutState.defaultHintTextColor = ColorStateList.valueOf(ContextCompat.getColor(requireContext(), R.color.grey_color_two))
                }
            }

    }

    private fun setUpCompleteViewForPlaces() {

        binding.recyclerviewPlaces.apply {
            this.setHasFixedSize(true)
            predictAdapter = PlacePredictionAdapter(this@SubPropertyAddressFragment)
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
            if (hasFocus) {
                CustomMaterialFields.setColor(binding.layoutSearchAddress,R.color.grey_color_two, requireActivity())
                binding.tvSearch.addTextChangedListener(placeTextWatcher)
            }
            else {
                binding.tvSearch.removeTextChangedListener(placeTextWatcher)
                if (binding.tvSearch.text.toString().length == 0) {
                    CustomMaterialFields.setColor(binding.layoutSearchAddress,R.color.grey_color_three, requireActivity())
                } else {
                    CustomMaterialFields.setColor(binding.layoutSearchAddress,R.color.grey_color_two, requireActivity())
                    CustomMaterialFields.clearError(binding.layoutSearchAddress, requireActivity())
                }
            }
        }

        binding.tvSearch.setOnClickListener { }
    }

    private val placeTextWatcher = (object : TextWatcher {
        override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
        override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
        override fun afterTextChanged(s: Editable) {
            val str: String = binding.tvSearch.text.toString()
            if (str.length >= 3) {
                if(binding.tvError.isVisible)
                    removeError()
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
                    //Log.e(TAG, prediction.placeId)
                    response.autocompletePredictions
                    predicationList.add(prediction.getFullText(null).toString())
                    //Log.e(TAG, prediction.getFullText(null).toString())

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
        HideSoftkeyboard.hide(requireActivity(),binding.tvSearch)
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
            subLocality?.let { binding.tvCountry.setText(it) }
            postalCode?.let { binding.edZipcode.setText(it) }
            countryName?.let { binding.tvCountry.setText(it) }
            binding.edStreetAddress.setText(place.getPrimaryText(null))
            premises?.let { binding.edUnitAtpNo.setText(it) }

            //Log.e("Bingo ", " = " + subLocality + " " + locality + "  " + postalCode)
        } catch (e: IOException) {
            e.printStackTrace()
        }

        val extractState = place.getFullText(null)
        var stateCode =  extractState.substring(extractState.lastIndexOf(",")-2,extractState.lastIndexOf(","))

        stateCode = stateCode.capitalize()
        //Log.e("stateCode ", " = $stateCode")
        //Log.e("Test State - ", " = " +map.get("LA") +"  "+map.get(stateCode))

        if(map.get(stateCode)!=null)
            binding.tvState.setText(map.get(stateCode))
        else
            binding.tvState.setText("")

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
        map.put("AL" , "Alabama")
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

    private fun setError(){
        binding.tvError.visibility = View.VISIBLE
        binding.searchSeparator.layoutParams.height = 1
        binding.searchSeparator.setBackgroundColor(resources.getColor(R.color.colaba_red_color, requireActivity().theme))
    }

    private fun removeError(){
        binding.tvError.visibility = View.GONE
        //binding.searchSeparator.layoutParams.height= 0.5.toInt()
        binding.searchSeparator.setBackgroundColor(resources.getColor(R.color.grey_color_four, requireActivity().theme))
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
    fun onErrorReceived(event: WebServiceErrorEvent) {
        if(event.isInternetError)
            SandbarUtils.showError(requireActivity(), AppConstant.INTERNET_ERR_MSG )
        else
            if(event.errorResult!=null)
                SandbarUtils.showError(requireActivity(), AppConstant.WEB_SERVICE_ERR_MSG )
        hideLoader()
    }

    private fun hideLoader(){

    }

}