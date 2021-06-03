import React, { useContext, useEffect, useState } from 'react'
import { Store } from '../../../../store/store';
import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';

import { useForm } from 'react-hook-form';
import HomeAddressFields from '../../../../Shared/Components/HomeAddressFields';
import { HomeAddressCaller } from '../../../../Utilities/Enum';
import MyPropertyActions from '../../../../store/actions/MyPropertyActions';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import { LocalDB } from '../../../../lib/LocalDB';
import { AdditionalPropertyAddressCalculator } from '../../../../Utilities/helpers/AdditionalPropertyAddressCalculator';

type AddressObj = {
    street_address: string,
    unit: string,
    city: string,
    zip_code: string,
    country: string
}
type AdditionalPropertyAddressProps = {
    setAddress: Function
}
export const AdditionalPropertyAddress = ({ setAddress }: AdditionalPropertyAddressProps) => {

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
        if (!initialData) {
            getAddress();
        }
    }, []);


    const getAddress = async () => {
        let addressData = await AdditionalPropertyAddressCalculator.getAdditionalPropertyAddress(setAddress);
        setInitialData(addressData);
    }
    const updateAddress = async (address) => {
        try {
            let res = await MyPropertyActions.addOrUpdatBorrowerAdditionalPropertyAddress(address);
            if (res) {
                NavigationHandler.moveNext();
            }
        } catch (error) {

        }
    }

    const onSubmit = async (data: AddressObj) => {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let propertyId = LocalDB.getAddtionalPropertyTypeId();
        let extractedAddres = {
            borrowerPropertyId: propertyId,
            loanApplicationId,
            ...AdditionalPropertyAddressCalculator.extractAddress(data, countries, states)
        };
        updateAddress(extractedAddres);
        setAddress(AdditionalPropertyAddressCalculator.createAddressString(extractedAddres));
    };


    const handleClick = () => {
        if (errors) {
            setToggleSection(true)
        }
        handleSubmit(onSubmit);
    }


    const setFieldValues = async () => {

    }

    const checkFormValidity = () => {

    }

    return (
        <section>
            <div className="compo-myMoney-income fadein">
                <PageHead title="My Properties" handlerBack={() => { }} />
                <TooltipTitle title={`Excellent! Please let us know about additional property address.`} />
                <form
                    className="comp-form-panel income-panel colaba-form no-minheight"
                    onSubmit={handleSubmit(onSubmit)}>

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
                            homeAddressLabel={'Property Address'}
                            homeAddressPlaceholder={"Enter City & State"}
                        />

                    </div>


                    <div className="form-footer">
                        <button data-testid="btn-save" className="btn btn-primary" type="submit" onClick={handleClick}>{'Save and continue'} </button>

                    </div>
                </form>

            </div>
        </section>
    )
}
export default AdditionalPropertyAddress;