import React, {
  ChangeEvent,
  Fragment,
  useCallback,
  useContext,
  useEffect,
  useRef,
  useState,
} from "react";
import InputField from "./InputField";
import { SearchLocationInput } from "./SearchLocationInput";

import GettingToKnowYouActions from "../../store/actions/GettingToKnowYouActions";
import { ErrorHandler } from "../../Utilities/helpers/ErrorHandler";
import {
  Country,
  CurrentHomeAddressReqObj,
  HomeAddressObject,
  State,
} from "../../Entities/Models/types";
import { LoanApplicationActionsType } from "../../store/reducers/LoanApplicationReducer";
import { Store } from "../../store/store";
import { Control, FieldValues } from "react-hook-form";
import AutoCompleteDropdown from "./AutocompleteDropdown";

import _ from "lodash";
import { HomeAddressCaller } from "../../Utilities/Enum";

type HomeAddressProps = {
  register: any;
  errors: any;
  getValues: Function;
  setValue: Function;
  setToggleSection: Function;
  toggleSection: boolean;
  setFieldValues: Function;
  initialData: CurrentHomeAddressReqObj | undefined;
  clearErrors: Function;
  control: Control<FieldValues>;
  showForm:boolean,
  caller:number,
  unregister:Function
  setIsButtonActive?:Function, 
  checkFormValidity?:Function,
  homeAddressLabel:string,
  homeAddressPlaceholder:string,
  restrictCountries:boolean
};
const HomeAddressFields = ({
  register,
  errors,
  getValues,
  setValue,
  clearErrors,
  setToggleSection,
  toggleSection,
  setFieldValues,
  initialData,
  control, 
  caller,
  unregister,
  checkFormValidity = () => {},
  homeAddressLabel,
  homeAddressPlaceholder,
  restrictCountries
}: HomeAddressProps) => {
  const { state, dispatch } = useContext(Store);
  const { states, countries }: any = state.loanManager;
  
  const statesRef = useRef([]);
  const countriesRef = useRef([]);
  const [selectedState, setSelectedState] = useState<string>("");
  const [selectedCountry, setSelectedCountry] = useState<string>("");
  const [initialAddress, setInitialAddress ] = useState<string>("");
  const [zipCode, setZipCode] = useState<string | undefined>("");
  const [unit, setUnit] = useState<string | undefined>("");
  const [city, setCity] = useState<string | undefined>("");
  const [restrictedCountries, setRestrictedCountries] = useState<string[]>([])
  const [stateLabel, setStateLabel]= useState<string>("State / Province");
  const [zipCodeLabel, setZipCodeLabel]= useState<string>("Zip Code / Postal Code");
  const [isShowState, setIsShowState]= useState<boolean>(false);

  useEffect(() => {
    getAllCountries();
    getAllStates();
  }, []);

  useEffect(() => {
    if (initialData) setInitialDataFields(initialData);
  }, [initialData]);

  useEffect(() => {
    if(restrictCountries){
      setValue("country", "United States")
      setSelectedCountry("United States");
      setRestrictedCountries(["us"])
      unregister("country");
      getStateLabel("United States")
      getZipCodeLabel("United States")
    }
    if(initialData){
      const {countryId} = initialData
      let country = setCountryValue(countryId);
      if (country && country?.name) {
        setValue("country", country?.name);
        setSelectedCountry(country?.name);
        getStateLabel(country?.name);
        getZipCodeLabel(country?.name);
      }
    }
   
  },[countries, initialData])

  useEffect(() => {
    if(initialData){
      const {stateId} = initialData
      let state = setStateValue(stateId);
      if (state && state?.name) {
        setValue("state", state?.name);
        setSelectedState(state?.name);
      }
    }
   
  },[states, initialData])

  useEffect(()=>{

    if(selectedCountry){
     register("country", {
        required: "This field is required.",
      });
    }
      else unregister("country")
  },[selectedCountry])

  useEffect(()=>{

    if(!selectedState){
      register("state", {
        required: "This field is required.",
      });
    }
      else unregister("state")
  },[selectedState])

  const setInitialDataFields = (dataValues: CurrentHomeAddressReqObj) => {
    if (!_.isEmpty(dataValues)) {
      const { street, unit, city, stateId, zipCode, countryId } = dataValues;

      setValue("street_address", street);
      let state = setStateValue(stateId);
      if (state && state?.name) {
        setValue("state", state?.name);
        setSelectedState(state?.name);
      }
      let country = setCountryValue(countryId);
      if (country && country?.name) {
        setValue("country", country?.name);
        setSelectedCountry(country?.name);
        getStateLabel(country?.name);
        getZipCodeLabel(country?.name);
      }
      setCity(city);
      setUnit(unit);
      setZipCode(zipCode);
      setToggleSection(true);

      

      let completeAddress:string = "";
      completeAddress+= street? street:""
      completeAddress+= city? ", "+city:""
      completeAddress+= state && state.shortCode? ", "+state.shortCode : "";
      completeAddress+= zipCode ? " "+zipCode:"";
      completeAddress+= country && country.shortCode ? ", "+country.shortCode:"";
      
      if(completeAddress !== ", US")
     setInitialAddress(completeAddress);
    
     checkFormValidity()
    }
    else{
      refreshFields();
      setInitialAddress("")
    }
  };
  

  const refreshFields = () => {
    setValue("street_address", "");
    setValue("unit", "");
    setValue("city", "");
    setValue("state", "");
    setValue("country", "");
    setValue("zip_code", "");
    
  };
  const getAllCountries = async () => {
    let res: any = await GettingToKnowYouActions.getAllCountries();

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        dispatch({
          type: LoanApplicationActionsType.SetCountries,
          payload: res.data,
        });
      }
      else{
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  const getAllStates = async () => {
    
    let res: any = await GettingToKnowYouActions.getStates(caller === HomeAddressCaller.SubjectProperty);

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        dispatch({
          type: LoanApplicationActionsType.SetStates,
          payload: res.data,
        });
      }
      else{
        ErrorHandler.setError(dispatch, res);
        }
    }
  };

  const setDataFieldValues = (addressFieldValues: HomeAddressObject) => {
    if (
      addressFieldValues?.state &&
      addressFieldValues?.state?.shortCode &&
      statesRef.current &&
      statesRef.current.length
    )
      addressFieldValues.state = setStateValue(
        addressFieldValues?.state?.shortCode
      );
    if (
      addressFieldValues.country &&
      addressFieldValues?.country?.shortCode &&
      countriesRef.current &&
      countriesRef.current.length
    )
      addressFieldValues.country = setCountryValue(
        addressFieldValues.country.shortCode
      );

    const { unit } = getValues();
    unit ? (addressFieldValues.unit = unit) : (addressFieldValues.unit = "");

    fillFieldValues(addressFieldValues);
    setFieldValues(addressFieldValues);
  };

  const fillFieldValues = (fieldValues: HomeAddressObject) => {
    if(fieldValues){
      let{street, city, country, state, zipCode}:HomeAddressObject =fieldValues
      setInputFieldValue("street_address", street && street);
      setCity(city && city);
      setCountryVal(country?.name);
      setStateVal(state && state.name && state?.name);
      setZipCode(zipCode && zipCode);
      getZipCodeLabel(country?.name);
      setToggleSection(true);
    }
    

    removeErrors();
    checkFormValidity();
  };

  const removeErrors = () => {
    clearErrors("city");
    clearErrors("zip_code");
    clearErrors("state");
    clearErrors("country");
    clearErrors("street_address");
  };
  const setInputFieldValue = (id: string, value: string | undefined) => {
    let field = document.querySelector("#" + id) as HTMLInputElement;
    if (field && value) {
      field.value = value;
      clearErrors(id);
    }
  };

  const setStateVal = (stateVal: string | undefined) => {
    if(stateVal !== undefined){
      let field = document.querySelector("#state") as HTMLSelectElement;
      if (field) {
        field.value = stateVal;
        setSelectedState(stateVal);
        setValue("state", stateVal);
        if(stateVal.length)unregister("state");
      }
    }
    else {
      setSelectedState("None");
        setValue("state", "None");
    }
    
  };

  const setCountryVal = (countryVal: string | undefined) => {
    let countryName:string= "";
    
    if(countryVal !== undefined){
      let field = document.querySelector("#country") as HTMLSelectElement;
      if (field) {
        if(caller !== HomeAddressCaller.SubjectProperty){
          countryName = countryVal;
          setSelectedCountry(countryVal);
          setValue("country", countryVal);
          if(countryVal.length)unregister("country");
        }
        
      }
    }
    else{
      if(restrictCountries){
        countryName = "United States"
        setSelectedCountry("United States");
        setValue("country", "United States");
      }
      else{
        countryName="None"
        setSelectedCountry("None");
          setValue("country", "None");
      }
      
    }
    
    getStateLabel(countryName);
    getZipCodeLabel(countryName)
  };

  const setStateValue = useCallback(
    (stateValue: string | number | undefined) => {
      let state: State = {};
      if (typeof stateValue === "string") {
        state =
          states &&
          states?.filter((state: State) => state.shortCode === stateValue)[0];
      } else if (typeof stateValue === "number") {
        state =
          states && states?.filter((state: State) => state.id === stateValue)[0];
      }
      if (state && !state.name) state.name = "None";
      return state;
    },
    [states]
  );

  const setCountryValue = (countryValue: string | number | undefined) => {
    let country: Country = {};
    if (typeof countryValue === "string") {
      country =
        countries &&
        countries?.filter(
          (country: Country) => country.shortCode === countryValue
        )[0];
    } else if (typeof countryValue === "number") {
      country =
        countries &&
        countries?.filter((country: Country) => country.id === countryValue)[0];
    }

    if (country && !country.name) {
      if(caller === HomeAddressCaller.SubjectProperty) country.name = "United States";
      else country.name = "None";
    }
    return country;
  };
  
  const getStateLabel = (country:string) => {
    
    if(country && country === "United States") {
      setIsShowState(true)
      setStateLabel("State")
      
    }
    else {
      setIsShowState(false)
      setStateLabel("State / Province")
    }

  }

  const getZipCodeLabel = (country:string | undefined) => {
    if(country === "United States") setZipCodeLabel("Zip Code")
    else setZipCodeLabel("Zip Code / Postal Code")

  }

  const getStateClasses = () =>{
    let classNames = "col-md-6 fadein";
    if(toggleSection && restrictCountries) return classNames;
    if(!toggleSection) classNames+= " d-none";
    if(toggleSection && !isShowState) classNames+=" d-none";
    return classNames
  }

  const removeGoogleAutoComplete = (name:string) => {

    var autoCompleteDropDown: HTMLInputElement | null = document.querySelector("#"+name);
    if (autoCompleteDropDown){
      autoCompleteDropDown.setAttribute("autocomplete", "search");
    }
  }
  return (
    <Fragment>
      <div className="col-sm-12">
        <SearchLocationInput
        caller={caller}
          labelText={homeAddressLabel}
          status={toggleSection}
          handlerToggle={() => {
            setToggleSection(!toggleSection);
          }}
          setFieldValues={setDataFieldValues}
          refreshFields={refreshFields}
          initialAddress={initialAddress}
          restrictedCountries = {restrictedCountries}
          homeAddressPlaceholder={homeAddressPlaceholder}
          restrictCountries={restrictCountries}
        />
      </div>

      <div className={`col-md-6 fadein ${!toggleSection && "d-none"}`}>
        <InputField
          label={"Street Address"}
          data-testid="street_address"
          id="street_address"
          name="street_address"
          type={"text"}
          placeholder={"Full Street Address"}
          register={register}
          rules={{
            required: "This field is required.",
          }}
          errors={errors}
          onChange={() => {
            clearErrors("street_address");
            checkFormValidity()
          }}
          onFocus={()=>{removeGoogleAutoComplete("street_address")}}
        />
      </div>
      <div className={`col-md-6 fadein ${!toggleSection && "d-none"}`}>
        <InputField
          label={"Unit or Apt. number"}
          data-testid="Unit"
          id="unit"
          name="unit"
          type="text"
          value={unit}
          placeholder={"Unit or Apt. Number"}
          register={register}
          maxLength={50}
          rules={
            {
              // required: "This field is required.",
            }
          }
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            const value = event.currentTarget.value;
            setUnit(value);
          }}
          onFocus={()=>{removeGoogleAutoComplete("unit")}}
          errors={errors}
        />
      </div>
      <div className={`col-md-6 fadein ${!toggleSection && "d-none"}`}>
        <InputField
          label={"City"}
          data-testid="City"
          id="city"
          name="city"
          value={city}
          type={"text"}
          placeholder={"Enter City"}
          register={register}
          rules={{
            required: "This field is required.",
          }}
          errors={errors}
          maxLength={20}
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            const value = event.currentTarget.value;
            if (value?.length > 0 && !/^[A-Za-z\s&(%'.-]+$/.test(value)) {
              return;
            }
            clearErrors("city");
            setCity(value);
            checkFormValidity()
          }}
          onFocus={()=>{removeGoogleAutoComplete("city")}}
        />
      </div>
      
        <div className={getStateClasses()}>
        <AutoCompleteDropdown
          disabled = {false}
          name={"state"}
          options={states ? states : []}
          placeholder="Select State"
          label={stateLabel}
          value={selectedState}
          onChange={() => {}}
          onInputChange={(event: ChangeEvent<HTMLInputElement>, newInputValue: string) => {
            console.log(event)
            setStateVal(newInputValue)
            if(newInputValue.length)unregister("state");
            clearErrors("state");
            checkFormValidity()
          }}
          control={control}
          errors={errors}
          
        />
      </div>

      
      <div className={`col-md-6 fadein ${!toggleSection && "d-none"}`}>
        <InputField
          label={zipCodeLabel}
          data-testid="Zip-Code"
          id="zip_code"
          name="zip_code"
          value={zipCode}
          type={"text"}
          maxLength={5}
          placeholder={"XXXXX"}
          register={register}
          rules={{
            required: "This field is required.",
            // minLength: {
            //   value: 5,
            //   message: "Please enter at least 5 characters.",
            // },
          }}
          errors={errors}
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            const value = event.currentTarget.value;
            if (isNaN(Number(value))) {
              setZipCode("");
              return;
            }
            clearErrors("zip_code");
            setZipCode(value);
            checkFormValidity()
          }}
          onFocus={()=>{removeGoogleAutoComplete("zip_code")}}
        />
      </div>
      <div className={`col-md-6 fadein ${!toggleSection && "d-none"}`}>
        <AutoCompleteDropdown
          disabled = {restrictCountries}
          name={"country"}
          options={countries ? countries : []}
          placeholder="Select Country"
          label="Country"
          value={selectedCountry}
          onChange={() => {}}
          onInputChange={(event: ChangeEvent<HTMLInputElement>, newInputValue: string) => {
            console.log(event)
            setCountryVal(newInputValue)
            if(newInputValue.length)unregister("country");
            clearErrors("country");
            checkFormValidity()
          }}
          control={control}
          errors={errors}
        />
      </div>
    </Fragment>
  );
};

export default HomeAddressFields;
