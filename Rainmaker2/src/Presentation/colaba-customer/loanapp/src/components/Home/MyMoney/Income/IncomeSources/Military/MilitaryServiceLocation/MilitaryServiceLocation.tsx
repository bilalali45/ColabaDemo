import React, { useContext, useEffect, useState } from 'react'
import { useForm } from 'react-hook-form';

import { Country, ServiceLocationAddressObject, State } from '../../../../../../../Entities/Models/types';
import HomeAddressFields from '../../../../../../../Shared/Components/HomeAddressFields';
import { Store } from '../../../../../../../store/store';
import { HomeAddressCaller } from '../../../../../../../Utilities/Enum';
import {MilitaryIncomeActionTypes} from '../../../../../../../store/reducers/MilitaryIncomeReducer'

import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';

export const MilitaryServiceLocation = () => {
    const { register, errors, handleSubmit, getValues, setValue, clearErrors, control, unregister } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
      });
      
      const { state, dispatch } = useContext(Store);
      const {  states, countries }: any = state.loanManager;
      const {militaryServiceAddress, militaryEmployer}: any = state.militaryIncomeManager;

      const [toggleSection, setToggleSection] = useState<boolean>(false);
      const [initialData, setInitialData] = useState<ServiceLocationAddressObject>();
      const [showForm] = useState<boolean>(true);
      
     

      useEffect(() => {
        if(militaryServiceAddress){
            setInitialData(militaryServiceAddress);
        }
      },[militaryServiceAddress])

      useEffect(() => {
        if(!militaryEmployer)  NavigationHandler.navigation?.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/IncomeSources`);
    }, [])

     
      
      const onSubmit = async (data) => {
          let { street_address, unit, city, zip_code, country } = data;
          if(!country.length) country = "United States";
          let stateEle = document.querySelector("#state") as HTMLSelectElement;
          let countryEle = document.querySelector("#country") as HTMLSelectElement;
          let stateObj = states && states?.filter((s: State) => s.name === stateEle.value)[0];
          let countryObj = countries && countries?.filter((c: Country) => c.name === countryEle.value)[0];

          let serviceAddress: ServiceLocationAddressObject = {
            street:street_address ,
            unit,
            city, 
            stateId: stateObj ? stateObj.id : null,
            zipCode:zip_code,
            countryId: countryObj ? countryObj.id : null,
            countryName: countryObj ? countryObj.name : "",
            stateName: stateObj ? stateObj.name : "",
            
          }
            await dispatch({type: MilitaryIncomeActionTypes.SetMilitaryServiceAddress, payload: serviceAddress});
            NavigationHandler.navigation?.moveNext();
      }
      const setFieldValues = async () => {
      }
      const checkFormValidity = () => {

    }

    return (
        <div className="compo-myMoney-income fadein">
            
            <form
          id="military-service-address-form"  
          data-testid="military-service-address-form"
          onSubmit={handleSubmit(onSubmit)}
          className="colaba-form">
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
                caller={HomeAddressCaller.MilitaryService} 
                restrictCountries={false}
                unregister={unregister} 
                checkFormValidity={checkFormValidity} 
                homeAddressLabel={`Service Location Address`}
                homeAddressPlaceholder= {"Enter City & State"}
                />

        </div>
        </div> <div className="p-footer">
                <button
                    id="military-service-address-next"
                    data-testid="employer-address-next"
                    className="btn btn-primary"
                    onClick={() => {
                      if (errors) setToggleSection(true)
                      handleSubmit(onSubmit)
                      
                    }}>
                
                    Next
            </button>
            </div>
            </form>
        </div>
    )
}
