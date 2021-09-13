package com.rnsoft.colabademo

import android.app.DatePickerDialog
import android.content.SharedPreferences
import android.content.res.ColorStateList
import android.location.Address
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
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
import androidx.recyclerview.widget.LinearLayoutManager
import com.google.android.gms.common.api.ApiException
import com.google.android.gms.maps.model.LatLng
import com.google.android.libraries.places.api.Places
import com.google.android.libraries.places.api.model.*
import com.google.android.libraries.places.api.net.FindAutocompletePredictionsRequest
import com.google.android.libraries.places.api.net.FindAutocompletePredictionsResponse
import com.google.android.libraries.places.api.net.PlacesClient

import com.rnsoft.colabademo.utils.MonthYearPickerDialog
import dagger.hilt.android.AndroidEntryPoint

import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject
import kotlin.collections.ArrayList
import android.location.Geocoder
import java.io.IOException
import java.util.*
import com.rnsoft.colabademo.databinding.TempResidenceLayoutBinding
import kotlinx.android.synthetic.main.temp_residence_layout.*


@AndroidEntryPoint
class CurrentResidenceFragment : Fragment(), DatePickerDialog.OnDateSetListener,
    PlacePredictionAdapter.OnPlaceClickListener {

    private var _binding: TempResidenceLayoutBinding? = null
    private val binding get() = _binding!!
    @Inject
    lateinit var sharedPreferences: SharedPreferences

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        _binding = TempResidenceLayoutBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.moveInEditText.setOnClickListener {
            createCustomDialog()
            binding.topSearchAutoTextView.clearFocus()
        }
        binding.moveInEditText.setOnFocusChangeListener { _, p1 ->
            if (p1)
                createCustomDialog()
        }
        binding.moveInEditText.showSoftInputOnFocus = false

        binding.topSearchAutoTextView.onFocusChangeListener = object : View.OnFocusChangeListener {
            override fun onFocusChange(p0: View?, p1: Boolean) {
                if (p1) {
                    binding.topSearchTextInputLine.minimumHeight = 1
                    binding.topSearchTextInputLine.setBackgroundColor(
                        resources.getColor(
                            R.color.colaba_primary_color,
                            requireActivity().theme
                        )
                    )
                } else {
                    binding.topSearchTextInputLine.minimumHeight = 1
                    binding.topSearchTextInputLine.setBackgroundColor(
                        resources.getColor(
                            R.color.grey_color_four,
                            requireActivity().theme
                        )
                    )
                    binding.topSearchAutoTextView.clearFocus()
                }
            }
        }


        binding.cityEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.cityEditText,
                binding.cityLayout,
                requireContext()
            )
        )
        binding.streetAddressEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.streetAddressEditText,
                binding.streetAddressLayout,
                requireContext()
            )
        )
        binding.unitAptInputEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.unitAptInputEditText,
                binding.unitAptInputLayout,
                requireContext()
            )
        )
        binding.countyEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.countyEditText,
                binding.countyLayout,
                requireContext()
            )
        )
        binding.zipcodeEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.zipcodeEditText,
                binding.zipcodeLayout,
                requireContext()
            )
        )

        binding.monthlyRentEditText.setOnFocusChangeListener(
            CustomFocusListenerForEditText(
                binding.monthlyRentEditText,
                binding.monthlyRentLayout,
                requireContext()
            )
        )
        //binding.housingEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.housingEditText, binding.housingLayout, requireContext()))
        //binding.moveInEditText.setOnFocusChangeListener(MyCustomFocusListener(binding.moveInEditText, binding.moveInLayout, requireContext()))


        val countryAdapter =
            ArrayAdapter(requireContext(), R.layout.autocomplete_text_view, AppSetting.countries)
        binding.countryCompleteTextView.setAdapter(countryAdapter)

        binding.countryCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.countryCompleteTextView.showDropDown()
        }
        binding.countryCompleteTextView.setOnClickListener {
            binding.countryCompleteTextView.showDropDown()
        }

        binding.countryCompleteTextView.onItemClickListener =
            object : AdapterView.OnItemClickListener {
                override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                    binding.countryCompleteLayout.defaultHintTextColor = ColorStateList.valueOf(
                        ContextCompat.getColor(
                            requireContext(),
                            R.color.grey_color_two
                        )
                    )
                }
            }


        val stateAdapter =
            ArrayAdapter(requireContext(), R.layout.autocomplete_text_view, AppSetting.states)
        binding.stateCompleteTextView.setAdapter(stateAdapter)

        binding.stateCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.stateCompleteTextView.showDropDown()
        }
        binding.stateCompleteTextView.setOnClickListener {
            binding.stateCompleteTextView.showDropDown()
        }

        binding.stateCompleteTextView.onItemClickListener =
            object : AdapterView.OnItemClickListener {
                override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                    binding.stateCompleteTextInputLayout.defaultHintTextColor =
                        ColorStateList.valueOf(
                            ContextCompat.getColor(
                                requireContext(),
                                R.color.grey_color_two
                            )
                        )
                }
            }


        val houseLivingTypeArray: ArrayList<String> =
            arrayListOf("Own", "Rent", "No Primary Housing Expense")
        val houseTypeAdapter =
            ArrayAdapter(requireContext(), R.layout.autocomplete_text_view, houseLivingTypeArray)
        binding.housingCompleteTextView.setAdapter(houseTypeAdapter)

        binding.housingCompleteTextView.setOnFocusChangeListener { _, _ ->
            binding.housingCompleteTextView.showDropDown()

        }
        binding.housingCompleteTextView.setOnClickListener {
            binding.housingCompleteTextView.showDropDown()
            binding.topSearchAutoTextView.clearFocus()
        }

        binding.housingCompleteTextView.onItemClickListener =
            object : AdapterView.OnItemClickListener {
                override fun onItemClick(p0: AdapterView<*>?, p1: View?, position: Int, id: Long) {
                    if (position == houseLivingTypeArray.size - 2) {
                        binding.monthlyRentLayout.visibility = View.VISIBLE
                        binding.housingLayout.defaultHintTextColor = ColorStateList.valueOf(
                            ContextCompat.getColor(
                                requireContext(),
                                R.color.grey_color_two
                            )
                        )
                    } else
                        binding.monthlyRentLayout.visibility = View.GONE
                }
            }




        binding.addAddressLayout.setOnClickListener {
            findNavController().navigate(R.id.navigation_previous_address)
        }

        binding.backButton.setOnClickListener {
            val message = "Are you sure you want to delete Richard's Current Residence?"
            //binding.topDelImageview.setColorFilter(resources.getColor(R.color.biometric_error_color, activity?.theme))
            AddressNotSavingDialogFragment.newInstance(message).show(
                childFragmentManager,
                AddressNotSavingDialogFragment::class.java.canonicalName
            )
            //findNavController().popBackStack()
        }

        binding.topDelImageview.setOnClickListener {
            val message = "Are you sure you want to delete Richard's Current Residence?"
            binding.topDelImageview.setColorFilter(resources.getColor(R.color.biometric_error_color, activity?.theme))
            AddressNotSavingDialogFragment.newInstance(message).show(
                childFragmentManager,
                AddressNotSavingDialogFragment::class.java.canonicalName
            )
        }

        binding.saveCurrentAddress.setOnClickListener {
            findNavController().popBackStack()
        }

        binding.currentResidenceParentLayout.setOnClickListener{
            binding.topDelImageview.setColorFilter(resources.getColor(R.color.grey_color_three, activity?.theme))
            binding.topSearchAutoTextView.clearFocus()
        }

        setUpCompleteViewForPlaces()


        initializeUSAstates()

        return root

    }



    // Create a RectangularBounds object.
    private val bounds = RectangularBounds.newInstance(
        LatLng(24.7433195, -124.7844079),
        LatLng(49.3457868, -66.9513812)
    )

    private val bias: LocationBias = RectangularBounds.newInstance(
        LatLng(37.7576948, -122.4727051), // SW lat, lng
        LatLng(37.808300, -122.391338) // NE lat, lng
    )

    private lateinit var token: AutocompleteSessionToken
    private lateinit var placesClient: PlacesClient

    private lateinit var predictAdapter: PlacePredictionAdapter

    private fun setUpCompleteViewForPlaces() {

        val linearLayoutManager = LinearLayoutManager(activity, LinearLayoutManager.VERTICAL, false)
        predictAdapter = PlacePredictionAdapter(this@CurrentResidenceFragment)
        binding.fakePlaceSearchRecyclerView.apply {
            this.layoutManager = linearLayoutManager
            this.setHasFixedSize(true)
            predictAdapter = PlacePredictionAdapter(this@CurrentResidenceFragment)
            this.adapter = predictAdapter
        }

        // Create a new token for the autocomplete session. Pass this to FindAutocompletePredictionsRequest,
        // and once again when the user makes a selection (for example when calling fetchPlace()).
        token = AutocompleteSessionToken.newInstance()

        Places.initialize(requireContext(), "AIzaSyBzPEiQOTReBzy6W1UcIyHApPu7_5Die6w")
        // Create a new Places client instance.
        placesClient = Places.createClient(requireContext())


        //autoCompleteAdapter = ArrayAdapter(requireContext(), R.layout.autocomplete_text_view,  predicationList)
        //binding.topSearchAutoTextView.freezesText = false
        //binding.topSearchAutoTextView.threshold = 4
        //binding.topSearchAutoTextView.setAdapter(autoCompleteAdapter)
        binding.topSearchAutoTextView.dropDownHeight = 0

        binding.topSearchAutoTextView.setOnFocusChangeListener { p0: View?, hasFocus: Boolean ->
            if (hasFocus)
                binding.topSearchAutoTextView.addTextChangedListener(placeTextWatcher)
            else
                binding.topSearchAutoTextView.removeTextChangedListener(placeTextWatcher)
        }


        binding.topSearchAutoTextView.setOnClickListener {
            //binding.topSearchAutoTextView.addTextChangedListener(placeTextWatcher)
        }




    }

    private val placeTextWatcher = (object : TextWatcher {
        override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
        override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
        override fun afterTextChanged(s: Editable) {
            val str: String = binding.topSearchAutoTextView.text.toString()
            if (str.length >= 3) {
                searchForGooglePlaces(str)
            } else
                if (str.length in 0..2) {
                    binding.fakePlaceSearchRecyclerView.visibility = View.GONE
                    predictAdapter.setPredictions(null)
                } else if (str.isEmpty()) {
                    binding.fakePlaceSearchRecyclerView.visibility = View.GONE
                    predictAdapter.setPredictions(null)
                    //binding.topSearchAutoTextView.clearFocus()
                    //hideKeyBoard()
                    //binding.topSearchAutoTextView.removeTextChangedListener(this)
                    //binding.topSearchAutoTextView.clearFocus()
                }


        }
    })


    private var predicationList: ArrayList<String> = ArrayList()
    //private lateinit var autoCompleteAdapter:ArrayAdapter<String>    //= ArrayAdapter(requireContext(), R.layout.autocomplete_text_view,  predicationList)

    private fun searchForGooglePlaces(queryPlace: String) {

        val TAG = "OTHER_WAY-"

        binding.fakePlaceSearchRecyclerView.visibility = View.VISIBLE

        // Use the builder to create a FindAutocompletePredictionsRequest.
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
                    Log.e(TAG, "Place not found: " + exception.statusCode)
                }
            }

        Log.e("predicationList", predicationList.size.toString())


        //var al2: ArrayList<String> = ArrayList<String>(predicationList.subList(1, 4))

        //if(predicationList.size>20)
        //predicationList = ArrayList(predicationList.subList(0,predicationList.size/2)


        // predictAdapterModified.notifyDataSetChanged()


        //predicationList.add("irfan")

        //this.autoCompleteAdapter.notifyDataSetChanged()
        //binding.topSearchAutoTextView.postDelayed({
        //(binding.topSearchAutoTextView as AutoCompleteTextView).showDropDown()

        //}, 200)
    }


    /*
    private var listAutocompletePrediction:List<AutocompletePrediction> = listOf()

    private val activityScope = CoroutineScope(Dispatchers.IO)

    private suspend fun searchGooglePlacesUsingExperimentalApi(queryPlace:String){

        activityScope.launch {
            val response = placesClient.awaitFindAutocompletePredictions {
                locationBias = bias
                typeFilter = TypeFilter.ESTABLISHMENT
                this.query = query
                countries = listOf("US")
            }

            listAutocompletePrediction = response.autocompletePredictions

            withContext(Dispatchers.Main){
                predictAdapter.setPredictions(listAutocompletePrediction)
            }
    }

     */


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
        binding.topDelImageview.setColorFilter(resources.getColor(R.color.grey_color_three, activity?.theme))

        if (event.boolean) {
            findNavController().popBackStack()
        }
    }


    private fun hideKeyBoard() {
        val inputMethodManager =
            ContextCompat.getSystemService(requireContext(), InputMethodManager::class.java)!!
        inputMethodManager.hideSoftInputFromWindow(binding.moveInEditText.windowToken, 0)

    }

    private fun createCustomDialog() {
        val pd = MonthYearPickerDialog()
        pd.setListener(this)
        pd.show(requireActivity().supportFragmentManager, "MonthYearPickerDialog")
    }

    override fun onDateSet(p0: DatePicker?, p1: Int, p2: Int, p3: Int) {
        var stringMonth = p2.toString()
        if (p2 < 10)
            stringMonth = "0$p2"

        val sampleDate = "$stringMonth / $p1"
        binding.moveInEditText.setText(sampleDate)
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


    override fun onPlaceClicked(place: AutocompletePrediction) {
        Log.e("which-place", "desc = " + place.getFullText(null).toString())
        predictAdapter.setPredictions(null)
        binding.fakePlaceSearchRecyclerView.visibility = View.GONE
        val placeSelected = place.getFullText(null).toString()
        binding.topSearchAutoTextView.clearFocus()
        hideKeyBoard()
        binding.topSearchAutoTextView.removeTextChangedListener(placeTextWatcher)
        binding.topSearchAutoTextView.setText(placeSelected)
        binding.topSearchAutoTextView.clearFocus()



        //val latLng: LatLng = place
        //val MyLat = latLng.latitude
        //val MyLong = latLng.longitude
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



            locality?.let { binding.cityEditText.setText(it) }
            //stateName?.let {  binding.stateCompleteTextView.setText(it) }
            subLocality?.let { binding.countyEditText.setText(it) }
            postalCode?.let { binding.zipcodeEditText.setText(it) }
            countryName?.let { binding.countryCompleteTextView.setText(it) }
            //addressLine?.let {  binding.streetAddressEditText.setText(it) }

            binding.streetAddressEditText.setText(place.getPrimaryText(null))

            premises?.let { binding.unitAptInputEditText.setText(it) }
            //unitAptInputEditText.setText(place.getSecondaryText(null))

            //featureName?.let {  binding.cityEditText.setText(it) }
            //countryCode?.let {  binding.cityEditText.setText(it) }

            //cityName?.let {  binding.cityEditText.setText(it) }


            Log.e("Bingo ", " = " + subLocality + " " + locality + "  " + postalCode)
            //edit_profile_city_editText.setText(place.getName().toString() + "," + stateName)
        } catch (e: IOException) {
            e.printStackTrace()
        }


        val extractState = place.getFullText(null)
        var stateCode =  extractState.substring(extractState.lastIndexOf(",")-2,extractState.lastIndexOf(","))

        stateCode = stateCode.capitalize()
        Log.e("stateCode ", " = $stateCode")

        Log.e("Test State - ", " = " +map.get("LA") +"  "+map.get(stateCode))

        if(map.get(stateCode)!=null)
            stateCompleteTextView.setText(map.get(stateCode))
        else
            stateCompleteTextView.setText("")



        visibleAllFields()
    }

    private fun visibleAllFields() {
        binding.cityLayout.visibility = View.VISIBLE
        binding.countyLayout.visibility = View.VISIBLE
        binding.countryCompleteLayout.visibility = View.VISIBLE
        binding.zipcodeLayout.visibility = View.VISIBLE
        binding.unitAptInputLayout.visibility = View.VISIBLE
        binding.streetAddressLayout.visibility = View.VISIBLE
        binding.stateCompleteTextInputLayout.visibility = View.VISIBLE


        binding.addAddressLayout.visibility = View.VISIBLE
        //binding.showAddressLayout.visibility = View.VISIBLE  // condition visibility
        //binding.monthlyRentLayout.visibility = View.VISIBLE


    }



}