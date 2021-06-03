import React, {useContext, useEffect, useState} from 'react'
import {useForm} from 'react-hook-form';
import {Country, CurrentHomeAddressReqObj, State} from '../../../../../../../Entities/Models/types';
import HomeAddressFields from '../../../../../../../Shared/Components/HomeAddressFields';
import {Store} from '../../../../../../../store/store';
import {HomeAddressCaller} from '../../../../../../../Utilities/Enum';
import {BusinessActionTypes} from "../../../../../../../store/reducers/BusinessIncomeReducer";

import {ApplicationEnv} from "../../../../../../../lib/appEnv";
import {BusinessAddressProto, CurrentBusinessDetails} from "../../../../../../../Entities/Models/Business";

import _ from 'lodash';
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler';
import {StringServices} from "../../../../../../../Utilities/helpers/StringServices";

export const BusinessAddress = () => {

    const {register, errors, handleSubmit, getValues, setValue, clearErrors, control, unregister} = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    const [toggleSection, setToggleSection] = useState<boolean>(false);
    
    const {state, dispatch} = useContext(Store);
    const {
        businessInfo,
    }: any = state.business;
    const { states, countries }: any = state.loanManager;
    const [showForm] = useState<boolean>(true);
    const [isClicked, setIsClicked] = useState<boolean>(false);
    const [initialData] = useState<CurrentHomeAddressReqObj>(
        businessInfo?.address?businessInfo?.address:null
    );
    useEffect(() => {
        ()=>{
            setIsClicked(false)
        }
    }, []);
    const onSubmit = async (data) => {
        if (!isClicked) {
            setIsClicked(true)

            console.log(data)
            let {street_address, unit, zip_code, country} = data;
            if (!country.length) country = "United States";
            let stateEle = document.querySelector("#state") as HTMLSelectElement;
            let countryEle = document.querySelector("#country") as HTMLSelectElement;
            let stateObj = states && states?.filter((s: State) => s.name === stateEle.value)[0];
            let countryObj = countries && countries?.filter((c: Country) => c.name === countryEle.value)[0];


            let businessAddress: BusinessAddressProto = {
                city: data.city,
                countryId: countryObj ? countryObj.id : null,
                stateId: stateObj ? stateObj.id : null,
                stateName: stateObj ? stateObj.name : "",
                street: street_address,
                unit: unit,
                zipCode: zip_code
            }


            if (!_.isEmpty(businessAddress)) {
                let currentBusinessDetails: CurrentBusinessDetails = {...businessInfo}
                currentBusinessDetails.address = businessAddress;
                dispatch({type: BusinessActionTypes.SetCurrentBusinessDetails, payload: currentBusinessDetails});
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Business/BusinessRevenue`);
            }

            //dispatch({type: BusinessActionTypes.SetCurrentBusinessAddress, payload: {...data}});

        }
    }
    const setFieldValues = async () => {
    }
    const checkFormValidity = () => {

    }

    return (
        <div className="compo-myMoney-income fadein">

            <form
                id="business-address-form"
                data-testid="business-address-form"
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
                        caller={HomeAddressCaller.Business}
                        restrictCountries={false}
                        unregister={unregister}
                        checkFormValidity={checkFormValidity}
                        homeAddressLabel={StringServices.capitalizeFirstLetter(businessInfo?.businessName)+"'s Main Address"}
                        homeAddressPlaceholder= {"Enter City & State"}
                    />



                </div>
                </div>
                <div className="p-footer">
                    <button
                        id="business-address-next"
                        data-testid="business-address-next"
                        className="btn btn-primary"
                        type="submit"
                        onClick={()=>{
                            if (errors) setToggleSection(true);
                            handleSubmit(onSubmit);
                        }}
                    >
                        Next
                    </button>
                </div>
            </form>
        </div>
    )
}
