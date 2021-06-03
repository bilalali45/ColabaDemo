import _ from 'lodash';
import React, { useContext, useEffect, useState } from 'react'
import { useForm } from 'react-hook-form';
import { EmployerOfficeAddress } from '../../../../../../../Entities/Models/Employment';
import { Country, CurrentHomeAddressReqObj, State } from '../../../../../../../Entities/Models/types';
import HomeAddressFields from '../../../../../../../Shared/Components/HomeAddressFields';
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler';
import { EmploymentIncomeActionTypes } from '../../../../../../../store/reducers/EmploymentIncomeReducer';
import { Store } from '../../../../../../../store/store';
import { HomeAddressCaller } from '../../../../../../../Utilities/Enum';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';

type addressObj = {
  street_address: string, 
  unit: string, 
  city: string, 
  zip_code: string, 
  country: string
}
export const EmployerAddress = () => {

    const { register, errors, handleSubmit, getValues, setValue, clearErrors, control, unregister } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
      });
    
      const [toggleSection, setToggleSection] = useState<boolean>(false);
      
      const [initialData, setInitialData] = useState<CurrentHomeAddressReqObj>();
      const { state, dispatch } = useContext(Store);
      const { employerInfo, employerAddress }: any = state.employment;
      const { states, countries }: any = state.loanManager;
      const [showForm] = useState<boolean>(true);
      const [btnClick, setBtnClick] = useState<boolean>(false);
      
      useEffect(()=>{
        if(employerInfo){
          if(employerAddress){
            let address : CurrentHomeAddressReqObj;
            if(employerAddress?.streetAddress){
              const {streetAddress,  unitNo, cityName, stateId, zipCode, countryId} = employerAddress;
              address = {
                street: streetAddress,
                unit: unitNo,
                city: cityName,
                stateId: stateId,
                zipCode: zipCode,
                countryId: countryId
              }
            }
            else{
              const {StreetAddress,  UnitNo, CityName, StateId, StateName, ZipCode, CountryId, CountryName} = employerAddress;
               address = {
                street: StreetAddress,
                unit: UnitNo,
                city: CityName,
                stateId: StateId,
                stateName: StateName,
                zipCode: ZipCode,
                countryId: CountryId, 
                countryName: CountryName
              }
            }
            
            setInitialData(address)
          }
        }
        
        else {
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/IncomeSources`);
        }
      },[])

      const onSubmit = async (data: addressObj) => {
        if(!btnClick){
          setBtnClick(true)
          console.log(data)
          let { street_address, unit, city, zip_code, country } = data;
          if(!country.length) country = "United States";
          
          let stateEle = document.querySelector("#state") as HTMLSelectElement;
          let countryEle = document.querySelector("#country") as HTMLSelectElement;
          let stateObj = states && states?.filter((s: State) => s.name === stateEle.value)[0];
          let countryObj = countries && countries?.filter((c: Country) => c.name === countryEle.value)[0];

          let employerAddress:EmployerOfficeAddress = {
            // city:city  
            CityId:null,
            CityName:city,
            CountryId: countryObj ? countryObj.id : null,
            StateId: stateObj ? stateObj.id : null,
            StateName: stateObj ? stateObj.name : "",
            StreetAddress: street_address,
            UnitNo:unit,
            ZipCode:zip_code
          }
          // if(!_.isEmpty(employerAddress))
            dispatch({type: EmploymentIncomeActionTypes.SetEmployerAddress, payload: employerAddress});
            NavigationHandler.navigation?.moveNext();
        }
        

      }
      const setFieldValues = async () => {
      }
      const checkFormValidity = () => {

    }

    return (
        <div className="compo-myMoney-income fadein">
            
            <form
          id="employer-address-form"
          data-testid="employer-address-form"
          className="colaba-form"
          onSubmit={handleSubmit(onSubmit)}>
            <div className="p-body">
          <div className="row form-group">

            <HomeAddressFields 
                toggleSection={toggleSection} 
                setToggleSection={setToggleSection} 
                register={register} 
                errors={errors} 
                getValues={getValues} 
                setValue={setValue} 
                clearErrors={clearErrors} 
                setFieldValues={setFieldValues} 
                initialData={initialData} 
                control={control} 
                showForm={showForm} 
                caller={HomeAddressCaller.Employment} 
                restrictCountries={false}
                unregister={unregister} 
                checkFormValidity={checkFormValidity} 
                homeAddressLabel={`${employerInfo && employerInfo.EmployerName}'s Main Address`}
                homeAddressPlaceholder= {"Enter City & State"}
                />

        </div></div>
            <div className="p-footer">
                <button
                    id="employer-address-next"
                    data-testid="employer-address-next"
                    className="btn btn-primary"
                    type="submit"
                    onClick={()=>{
                      if (errors) setToggleSection(true);
                      handleSubmit(onSubmit);
                  }}
                >
                    NEXT
            </button>
            </div>
            </form>
        </div>
    )
}
