import React, { useState, useEffect, useRef } from "react";

import { GetZipCodeByStateCountyCityNameRes, HomeAddressObject, SearchByZipCodeCityCountryStateRes } from "../../Entities/Models/types";
import { ApplicationEnv } from "../../lib/appEnv";
import GettingToKnowYouActions from "../../store/actions/GettingToKnowYouActions";

import { ErrorHandler } from "../../Utilities/helpers/ErrorHandler";
import { IconSearch } from "./SVGs";

let autoComplete;
let service;
type SearchLocationInputProps = {
  labelText: String;
  status: boolean;
  handlerToggle: Function;
  setFieldValues: Function;
  refreshFields: Function;
  initialAddress: string;
  restrictedCountries:string[];
  caller:number,
  homeAddressPlaceholder:string,
  restrictCountries:boolean
};


export const SearchLocationInput = ({
  labelText,
  status,
  handlerToggle,
  setFieldValues,
  refreshFields,
  initialAddress,
  restrictedCountries,
  homeAddressPlaceholder,
  restrictCountries
}: SearchLocationInputProps) => {

  const [query, setQuery] = useState("");
  const autoCompleteRef = useRef<any>(null);
  
  
  useEffect(() => {
    loadGoogleMaps(() => handleScriptLoad(autoCompleteRef));
    var autoCompleteSearch: HTMLInputElement | null = document.querySelector("#autoCompleteSearch");
    if (autoCompleteSearch){
      autoCompleteSearch.setAttribute("autocomplete", "search");
    }
  }, []);

  useEffect(() => {
    if (initialAddress) setInitialAddressFields(initialAddress);
  }, [initialAddress]);



  const setInitialAddressFields = (addressVal: string) => {
    if (addressVal) {
    setQuery(addressVal)

    }
  };
  const loadGoogleMaps = (callback) => {
    const existingScript = document.getElementById("googlePlaces");

    if (!existingScript) {
      const script = document.createElement("script");
      script.src = `https://maps.googleapis.com/maps/api/js?key=${ApplicationEnv.GOOGLE_PLACES_API}&libraries=places`;
      script.id = "googlePlaces";
      document.body.appendChild(script);

      script.onload = () => {
        if (callback) callback();
      };
    }

    if (existingScript && callback) callback();
  };

  const handlePlaceSelect = async (updateQuery) => {
    const addressObject = autoComplete.getPlace();
    refreshFields()
    if (addressObject) {
      console.log(addressObject)
      updateQuery(addressObject.formatted_address);

      let homeAddressObj = await fillAddressInFieldsFromGoogleAPI(addressObject, updateQuery)
      
      if(homeAddressObj && homeAddressObj.country && homeAddressObj.country.id === 1 && homeAddressObj.country.name ==="United States" ){
        let result = await getDataFromServer(addressObject);
        if (result) {
          homeAddressObj = await fillAddressInFieldsFromServer(result[0], homeAddressObj)
        }
  
      }
     
      setFieldValues(homeAddressObj)
    }
  };
  const handleScriptLoad = (autoCompleteRef) => {
    if (typeof google === 'object' && typeof google.maps === 'object') {
      service = new google.maps.places.AutocompleteService();
          if(restrictCountries){
        autoComplete = new google.maps.places.Autocomplete(
          autoCompleteRef.current,
          { types: ["geocode"], componentRestrictions: { country: ["us"]}}
        );
      }
      else{
        autoComplete = new google.maps.places.Autocomplete(
          autoCompleteRef.current,
          { types: ["geocode"], componentRestrictions: { country: []}}
        );
      }
      
      autoComplete.setFields(["address_components", "formatted_address"]);
      autoComplete.addListener("place_changed", () =>{
        handlePlaceSelect(setQuery)
      } 
      );
    }     
  };
  const getPlaceSuggestions = () => {
    service.getPlacePredictions(
      {input: autoCompleteRef?.current?.value,
        types: ['geocode'],
        componentRestrictions: {country: restrictedCountries}
      },
      displaySuggestions
    );
  }

  const displaySuggestions = function (predictions, status) {
    if (
      status != google.maps.places.PlacesServiceStatus.OK ||
      !predictions
    ) {
      let confirmAddressTag = document.querySelector("#confirm-address-li") as HTMLLIElement;
      if(confirmAddressTag){
        if(autoCompleteRef?.current?.value?.length){ 
          confirmAddressTag.innerText = "Confirm Address:" + autoCompleteRef?.current?.value;
        }
        else{
          confirmAddressTag.innerText = "";
        }
        
      }
      
      return;
    }
    
    // handlePlaceSelect(setQuery)
  };

  const getDataFromServer = (addressObject) => {
    if (addressObject?.name && !isNaN(addressObject?.name) && addressObject?.name?.length === 5) {
      return getSearchByZipCode(+addressObject.name)
    }
    else {
      let homeAddressObject = getDataFromGoogleAPIRes(addressObject);

      return getSearchByStateCountyCity(homeAddressObject?.city + " " + homeAddressObject?.state?.shortCode)
    }
  }

  const getDataFromGoogleAPIRes = (addressObject) => {
    let homeAddressObject: HomeAddressObject = {
      street: "",
      unit: "",
      city: "",
      zipCode: "",
      state: {
        shortCode:"",
        name:""

      },
      country: {}
    };

    if(addressObject && addressObject?.address_components &&addressObject?.address_components?.length ){
      for (const component of addressObject?.address_components as google.maps.GeocoderAddressComponent[]) {
        // @ts-ignore remove once typings fixed
        const componentType = component.types[0];
        switch (componentType) {
  
          case "street_number": {
            homeAddressObject.street = component.long_name;
            break;
          }
  
          case "route": {
            homeAddressObject.street += " " + component.short_name;
            break;
          }
  
          case "postal_code": {
  
            homeAddressObject.zipCode = component.long_name;
            break;
          }
  
          // case "postal_code_suffix": {
          //   postcode = `${postcode}-${component.long_name}`;
          //   break;
          // }
  
          case "locality":
            homeAddressObject.city = component.long_name;
            break;
  
          case "administrative_area_level_1": {
            if(homeAddressObject.state){
              homeAddressObject.state.shortCode = component.short_name;
              homeAddressObject.state.name = component.long_name;
            }
            
            break;
          }
  
          case "country":
            if(homeAddressObject.country){
              homeAddressObject.country.shortCode = component.short_name;
              homeAddressObject.country.name = component.long_name;
            }
            
            break;
        }
  
  
      }
    }
    
    return homeAddressObject;
  }

  const fillAddressInFieldsFromGoogleAPI = async (addressObject, updateQuery: Function) => {
    updateQuery(addressObject.formatted_address);

    let homeAddressObject = getDataFromGoogleAPIRes(addressObject);
    let { zipCode, city, state, country, street, unit } = homeAddressObject;

    if(country && country.shortCode === "US" && country.name =="United States" && state?.name && city && zipCode){
      let addressData: GetZipCodeByStateCountyCityNameRes = await setZipCodeByName(state?.name, country.name, city, zipCode);
      if (addressData) {
        homeAddressObject.street = street;
        homeAddressObject.unit = unit;
        homeAddressObject.zipCode = addressData.zipPostalCode;
        if(homeAddressObject.state){
          homeAddressObject.state.id = addressData.stateId;
        homeAddressObject.state.name = addressData.stateName;
        homeAddressObject.state.shortCode = addressData.abbreviation;
        }
        homeAddressObject.city = addressData.cityName;
        if(homeAddressObject.country)
        {
          homeAddressObject.country.id = country.id;
        homeAddressObject.country.name = country.name;
      }
      }
    }
    else{
        homeAddressObject.street = street;
        homeAddressObject.unit = unit;
        homeAddressObject.zipCode = zipCode;
        if(homeAddressObject.state){
        homeAddressObject.state.id= 0;
        homeAddressObject.state.name = "None";
        }
        homeAddressObject.city = city;
        
    }
    // console.log(homeAddressObject)
    

    return homeAddressObject

  }

  const fillAddressInFieldsFromServer = async (addressObject: SearchByZipCodeCityCountryStateRes, homeAddressObj: HomeAddressObject) => {
    let homeAddressObject: HomeAddressObject = {
      street: "",
      unit: "",
      city: "",
      zipCode: "",
      state: {},
      country: {
        id: 1,
        name: "United States",
        shortCode: "US"
      }
    };
    console.log('---------> 12');
    let locations = addressObject.label.split(',')
    let ids:string[]=[]
    ids = addressObject?.ids?.split(",")


    if (ids && ids.length && +ids[0]! > 0) homeAddressObject.zipCode = ids[0];
    else {
      let zipCode = await setZipCode(+ids[1]!, +ids[2]!, +ids[3]!)
      homeAddressObject.zipCode = zipCode.zipPostalCode;
    }

    if(homeAddressObject.state && ids) homeAddressObject.state.id = +ids[1]!;
    // homeAddressObject.country.id = +ids[2];

    if (locations?.length > 2) {
      homeAddressObject.city = locations[0];
      if(homeAddressObject.state) homeAddressObject.state.name = addressObject.stateName;
    }
    else {
      homeAddressObject.city = "";
      if(homeAddressObject.state) homeAddressObject.state.name = addressObject.stateName;
    }
    homeAddressObject.street = homeAddressObj.street;
    homeAddressObject.unit = homeAddressObj.unit;

    return homeAddressObject;
  }

  const setZipCode = async (stateId: number, countryId: number, cityId: number) => {

    let res: any = await GettingToKnowYouActions.getZipCodeByStateCountryCity(cityId, countryId, stateId);

    if (res) {
      // console.log(res)
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        return res.data;
        // setInitialData(res.data)
      }
      // else{
      //   ErrorHandler.setError(dispatch, res);
      //   }
    }
  }

  const setZipCodeByName = async (stateName: string, countryName: string, cityName: string, zipCode: string) => {

    let res: any = await GettingToKnowYouActions.getZipCodeByStateCountyCityName(cityName, stateName, countryName, zipCode);

    if (res) {
      // console.log(res)
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        return res.data;
        // setInitialData(res.data)
      }
      // else{
      //   ErrorHandler.setError(dispatch, res);
      //   }
    }
  }
  const getSearchByZipCode = async (searchKey: number) => {
    let res: any = await GettingToKnowYouActions.getSearchByZipCode(searchKey);

    if (res) {
      // console.log(res)
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        return res.data;
        // setInitialData(res.data)
      }
      // else{
      //   ErrorHandler.setError(dispatch, res);
      //   }
    }
  }

  const getSearchByStateCountyCity = async (searchKey: string) => {
    let res: any = await GettingToKnowYouActions.getSearchByStateCountyCity(searchKey);

    if (res) {
      // console.log(res)
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        return res.data;
        // setInitialData(res.data)
      }
      // else{
      //   ErrorHandler.setError(dispatch, res);
      //   }
    }
  }

  return (
    <div className="form-group input-search">
      <label className="form-label" data-testid={"current-home-add-lbl"}>
        <div className="form-text">{labelText}</div>
      </label>
      <div className="input-group">
        <div className="input-group-prepend" >
          <IconSearch />
        </div>
        <input
          name="autoCompleteSearch"
          id="autoCompleteSearch"
          autoComplete="autoCompleteSearch"
          className={`form-control`}          
          ref={autoCompleteRef}
          onMouseEnter={()=>{ 
            var autoCompleteSearch: HTMLInputElement | null= document.querySelector("#autoCompleteSearch");
            if (autoCompleteSearch){
              autoCompleteSearch.setAttribute("autocomplete", "search");
            }
          }}
          onChange={(event)=>{setQuery(event.target.value); getPlaceSuggestions();}}
          // onKeyDown={setMaxLength}
          placeholder={homeAddressPlaceholder}
          value={query}
          maxLength={100}
          autoFocus
        />
        <div className="input-group-append">
          <span id={"toggle-fields-btn"}className={`btn ${status ? 'active' : ''}`} onClick={(e) => handlerToggle(e)}>
            <em className="zmdi zmdi-chevron-down"></em>
          </span>
        </div>
      </div>
      {/* <ul>
          <li id="confirm-address-li" onClick={setStreetAddress}></li>
        </ul> */}
    </div>
  );
};
