import React, { useContext, useEffect, useState } from 'react'
import { useForm } from 'react-hook-form';
import { Country, State } from '../../../../../../../Entities/Models/types';
import HomeAddressFields from '../../../../../../../Shared/Components/HomeAddressFields';
import { Store } from '../../../../../../../store/store';
import { HomeAddressCaller } from '../../../../../../../Utilities/Enum';

export const SelfEmploymentAddress = ({ selfIncome = { jobTitle: '' }, updateFormValuesOnChange }) => {
    const { register, errors, handleSubmit, getValues, setValue, clearErrors, control, unregister } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    const [toggleSection, setToggleSection] = useState<boolean>(false);

    const [initialData, setInitialData] = useState();
    const { state } = useContext(Store);
    const { states, countries }: any = state.loanManager;
    const [showForm] = useState<boolean>(true);

    useEffect(() => {
        if (selfIncome) {
            if (!initialData) {
                setInitialData(selfIncome['address']);
            }
        }
    }, [selfIncome['jobTitle']])

    const saveValues = (data) => updateFormValuesOnChange({ key: 'address', value: extractAddress(data) })

    const onSubmit = async (data) => saveValues(data);


    const extractAddress = (data) => {
        console.log(data);
        let { street_address, unit, city, zip_code, country } = data;

        if (!street_address) {
            return "";
        }

        if (!country.length) country = "United States";
        let stateEle = document.querySelector("#state") as HTMLSelectElement;
        let countryEle = document.querySelector("#country") as HTMLSelectElement;
        let stateObj = states && states?.filter((s: State) => s.name === stateEle.value)[0];
        let countryObj = countries && countries?.filter((c: Country) => c.name === countryEle.value)[0];

        return {
            city: city,
            countryId: countryObj ? countryObj.id : null,
            stateId: stateObj ? stateObj.id : null,
            stateName: stateObj ? stateObj.name : "",
            street: street_address,
            unit: unit,
            zipCode: zip_code,
            countryName: country
        }
    }



    const setFieldValues = async () => {

    }

    const checkFormValidity = () => {

    }

    return (
        <div className="compo-myMoney-income fadein">

            <form
                id="self-employment-address-form"
                data-testid="self-employment-address-form"
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
                            caller={HomeAddressCaller.SelfEmployment}
                            restrictCountries={false}
                            unregister={unregister}
                            checkFormValidity={checkFormValidity}
                            homeAddressLabel={`${selfIncome['businessName']}'s Address`}
                            homeAddressPlaceholder={"Enter City & State"}
                        />

                    </div>
                </div><div className="p-footer">
                    <button
                        id="self-employment-address-next"
                        data-testid="self-employment-address-next"
                        className="btn btn-primary"
                        type="submit"
                        onClick={() => {
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
