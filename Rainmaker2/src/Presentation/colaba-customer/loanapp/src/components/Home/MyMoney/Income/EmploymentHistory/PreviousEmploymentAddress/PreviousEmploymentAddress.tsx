import _ from 'lodash';
import React, { useContext, useEffect, useState } from 'react'
import { useForm } from 'react-hook-form';
import { EmployerOfficeAddress } from '../../../../../../Entities/Models/Employment';
import { Country, CurrentHomeAddressReqObj, State } from '../../../../../../Entities/Models/types';
import { ApplicationEnv } from '../../../../../../lib/appEnv';
import HomeAddressFields from '../../../../../../Shared/Components/HomeAddressFields';
import { EmploymentHistoryActionTypes } from '../../../../../../store/reducers/EmploymentHistoryReducer';
import { Store } from '../../../../../../store/store';
import { HomeAddressCaller } from '../../../../../../Utilities/Enum';
import { NavigationHandler } from '../../../../../../Utilities/Navigation/NavigationHandler';

type addressObj = {
  street_address: string, 
  unit: string, 
  city: string, 
  zip_code: string, 
  country: string
}
export const PreviousEmploymentAddress = () => {
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
      const { previousEmployerInfo, previousEmployerAddress }: any = state.employmentHistory;
      const { states, countries }: any = state.employment;
      const [showForm] = useState<boolean>(true);
      
      
      
      useEffect(()=>{
        if(previousEmployerInfo){
          if(previousEmployerAddress){
            let address : CurrentHomeAddressReqObj;
            if(previousEmployerAddress?.streetAddress){
              const {streetAddress,  unitNo, cityName, stateId, zipCode, countryId} = previousEmployerAddress;
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
              const {StreetAddress,  UnitNo, CityName, StateId, StateName, ZipCode, CountryId, CountryName} = previousEmployerAddress;
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
        //   if(!_.isEmpty(employerAddress))
            dispatch({type: EmploymentHistoryActionTypes.SetPreviousEmployerAddress, payload: employerAddress});
            NavigationHandler.navigation?.moveNext();

      }
      const setFieldValues = async () => {
      }
      const checkFormValidity = () => {

    }

    return (
        <div className="compo-myMoney-income fadein">
            
            <form
          id="previous-employment-address-form"
          data-testid="previous-employment-address-form"
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
                caller={HomeAddressCaller.PreviousEmployment} 
                restrictCountries={false}
                unregister={unregister} 
                checkFormValidity={checkFormValidity} 
                homeAddressLabel={`${previousEmployerInfo && previousEmployerInfo.EmployerName}'s Main Address`}
                homeAddressPlaceholder= {"Enter City & State"}
                />

        </div>
        </div><div className="p-footer">
                <button
                    id="previous-employment-address-next"
                    data-testid="previous-employment-address-next"
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
