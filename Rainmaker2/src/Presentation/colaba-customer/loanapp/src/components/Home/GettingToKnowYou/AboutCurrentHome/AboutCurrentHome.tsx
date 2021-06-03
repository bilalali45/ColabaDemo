import React, { Fragment, useContext, useEffect, useState } from "react";
import _ from "lodash";
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";

import { AddressHomeIcon } from "../../../../Shared/Components/SVGs";
import { useForm } from "react-hook-form";

import HomeAddressFields from "../../../../Shared/Components/HomeAddressFields";
import HousingStatus from "../../../../Shared/Components/HousingStatus";
import { Country, CurrentHomeAddressReqObj, HomeOwnershipTypes, State } from "../../../../Entities/Models/types";
import GettingToKnowYouActions from "../../../../store/actions/GettingToKnowYouActions";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { LocalDB } from "../../../../lib/LocalDB";
import { Store } from "../../../../store/store";

import InputRadioBox from "../../../../Shared/Components/InputRadioBox";
import { HomeAddressCaller, OwnTypeEnum } from "../../../../Utilities/Enum";
import { StringServices } from "../../../../Utilities/helpers/StringServices";
import Loader from "../../../../Shared/Components/Loader";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { HousingStatusEnum } from "../../../../Utilities/Enumerations/HomeEnums";
import { MyPropertiesSteps, MyPropertiesStepsConstants } from "../../../../Utilities/Navigation/navigation_config/MyProperties";
import { LoanApplicationActionsType } from "../../../../store/reducers/LoanApplicationReducer";
import { TenantConfigFieldNameEnum } from "../../../../Utilities/Enumerations/TenantConfigEnums";

const AboutCurrentHome = ({ setcurrentStep }: any) => {
    const { register, errors, handleSubmit, getValues, setValue, clearErrors, control, unregister } = useForm({
        mode: "onBlur",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });


    const {
        register: register2,
        errors: errors2,
        handleSubmit: handleSubmit2
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });


    const [toggleSection, setToggleSection] = useState<boolean>(false);
    const [selectedHomeOwnershipTypeObj, setSelectedHomeOwnershipTypeObj] = useState<HomeOwnershipTypes>();
    const [initialData, setInitialData] = useState<CurrentHomeAddressReqObj>({});
    const { state, dispatch } = useContext(Store);
    const { loanInfo, states, countries, homeOwnershipTypes, primaryBorrowerInfo }: any = state.loanManager;
    const [btnClick, setBtnClick] = useState<boolean>(false);
    const [selectedOption, setSelectedOption] = useState<boolean>(true);
    const [showForm, setShowForm] = useState<boolean>(false)
    const [homeAddress, setHomeAddress] = useState<string>("");
    const [isCurrentHomeSet, setIsCurrentHomeSet] = useState<boolean>(false);
    const [primaryHomeAddress, setPrimaryHomeAddress] = useState<CurrentHomeAddressReqObj>({})
    const [secHomeAddress, setSecHomeAddress] = useState<CurrentHomeAddressReqObj>({})
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        if (primaryBorrowerInfo)
            setCurrentHomeAddress()
    }, [primaryBorrowerInfo])

    const getPrimaryBorrowerAddress = async () => {
        let borrowerId = primaryBorrowerInfo ? primaryBorrowerInfo.id : 0;
        if (borrowerId > 0) {

            let res: any = await GettingToKnowYouActions.getBorrowerAddress(+LocalDB.getLoanAppliationId(), +borrowerId);
            if (res && res.data) {
                if (ErrorHandler.successStatus.includes(res.statusCode)) {
                    setInitialDataValues(res.data)
                    setPrimaryHomeAddress(res.data);
                    setIsLoading(false)

                    if (loanInfo.ownTypeId === OwnTypeEnum.PrimaryBorrower) setShowForm(true)
                    return res.data;
                }
                else {
                    ErrorHandler.setError(dispatch, res);
                }
            }
        }
        setIsLoading(false);
    }

    const getCurrentBorrowerAddress = async () => {

        let borrowerId = loanInfo ? loanInfo.borrowerId : 0
        if (borrowerId > 0) {

            let res: any = await GettingToKnowYouActions.getBorrowerAddress(+LocalDB.getLoanAppliationId(), +borrowerId);
            if (res && res.data) {
                if (ErrorHandler.successStatus.includes(res.statusCode)) {
                    // setInitialDataValues(res.data)
                    setSecHomeAddress(res.data)
                    setIsCurrentHomeSet(true)
                    setInitialData(res.data)
                    setIsLoading(false)
                    if (loanInfo.ownTypeId === OwnTypeEnum.PrimaryBorrower) setShowForm(true)
                    return res.data;
                }
                else {
                    ErrorHandler.setError(dispatch, res);
                }
            }
            else {
                // setInitialDataValues({})
            }
        }
    }
    const checkFormValidity = () => {
    }


    const setCurrentHomeAddress = async () => {
        let res = await getCurrentBorrowerAddress()
        if (!res) getPrimaryBorrowerAddress()
    }

    const setInitialDataValues = (dataValues) => {

        if (!_.isEmpty(dataValues)) {
            // setHomeAddressData(dataValues)

            const { street, city, stateId, zipCode, countryId } = dataValues;

            let state = states && states?.filter((s: State) => s.id === stateId)[0];
            let country = countries && countries?.filter((c: Country) => c.id === countryId)[0];

            let completeAddress: string = "";
            completeAddress += street ? street : ""
            completeAddress += city ? ", " + city : ""
            completeAddress += state && state.shortCode ? ", " + state.shortCode : "";
            completeAddress += zipCode ? " " + zipCode : "";
            completeAddress += country && country.shortCode ? ", " + country.shortCode : "";

            setHomeAddress(completeAddress);
        }
        else {
            // setHomeAddressData({})
            setHomeAddress("")
        }

    }

    const prepareAPIData = () => {
        const { street_address, unit, city, zip_code, housingStatus, monthlyRent } = getValues();
        let stateEle = document.querySelector("#state") as HTMLSelectElement;
        let countryEle = document.querySelector("#country") as HTMLSelectElement;
        let stateObj = states && states?.filter((s: State) => s.name === stateEle.value)[0];
        let countryObj = countries && countries?.filter((c: Country) => c.name === countryEle.value)[0];
        let housingStatusObj = homeOwnershipTypes && homeOwnershipTypes?.filter((housingStat: HomeOwnershipTypes) => housingStat.name === housingStatus)[0]
        let dataObj: CurrentHomeAddressReqObj = {
            loanApplicationId: +LocalDB.getLoanAppliationId(),
            id: +loanInfo.borrowerId,
            street: street_address,
            unit: unit,
            city: city,
            stateId: stateObj ? stateObj.id : null,
            stateName: stateObj ? stateObj.name : "",
            countryId: countryObj ? countryObj.id : null,
            countryName: countryObj ? countryObj.name : "",
            zipCode: zip_code,
            housingStatusId: housingStatusObj ? housingStatusObj.id : null,
            rent: monthlyRent ? +monthlyRent.replace(/\,/g, '') : null,
            // rent: null,
            state: NavigationHandler.getNavigationStateAsString()
        }
        return dataObj;

    }

    const onSubmit = async () => {

        if (!btnClick) {
            setBtnClick(true);

            let reqData: CurrentHomeAddressReqObj = prepareAPIData()

            let res = await GettingToKnowYouActions.addOrUpdatePrimaryBorrowerAddress(reqData);
            if (res) {
                if (ErrorHandler.successStatus.includes(res.statusCode)) {
                    if (loanInfo.ownTypeId === OwnTypeEnum.PrimaryBorrower) {
                        if (reqData.housingStatusId == HousingStatusEnum.Own) {
                            NavigationHandler.enableFeatures(MyPropertiesStepsConstants.OwnHousingStatusSteps);
                            NavigationHandler.disableFeatures(MyPropertiesStepsConstants.RentorNoPrimaryHousingStatusSteps);
                            if (!NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PropertyTypeMyProperties)) {
                                NavigationHandler.disableFeature(MyPropertiesSteps.CurrentResidence);
                            }
                            else {
                                NavigationHandler.enableFeature(MyPropertiesSteps.CurrentResidence);
                            }
                        }
                        else {
                            NavigationHandler.disableFeatures(MyPropertiesStepsConstants.OwnHousingStatusSteps);
                            NavigationHandler.enableFeatures(MyPropertiesStepsConstants.RentorNoPrimaryHousingStatusSteps);
                            dispatch({
                                type: LoanApplicationActionsType.SetMyPropertyInfo,
                                payload: null,
                            });
                        }
                    }
                    NavigationHandler.moveNext();
                }
                else {
                    ErrorHandler.setError(dispatch, res);
                }
            }
        }
    };

    const setFieldValues = async () => {

    }



    const getTitle = () => {
        if (loanInfo) {
            if (loanInfo.ownTypeId == OwnTypeEnum.PrimaryBorrower) {
                return `Thanks ${StringServices.capitalizeFirstLetter(primaryBorrowerInfo?.name)}. Please tell us about ${StringServices.capitalizeFirstLetter(loanInfo.borrowerName)}'s current home.`
            }
            else {
                return `Thanks ${StringServices.capitalizeFirstLetter(loanInfo.borrowerName)}. Please tell us about your current home.`
            }
        }
        return "";
    }

    const getHomeAddressLabel = () => {
        if (loanInfo) {

            if (loanInfo.ownTypeId === 2) {
                return `${StringServices.capitalizeFirstLetter(loanInfo.borrowerName)}'s Current Home Address`
            }
            else {
                return `Current Home Address`
            }
        }
        return "";
    }

    const renderCurrentHome = () => {

        return (
            <div className="compo-abt-yourSelf fadein">
                <PageHead
                    title="Personal Information"
                    handlerBack={() => {
                        setcurrentStep("about_your_self");
                    }}
                />
                <TooltipTitle title={getTitle()} />

                <div className="comp-form-panel colaba-form">
                    <form
                        id="current-home-form"
                        data-testid="current-home-form"
                        className="colaba-form"
                        onSubmit={handleSubmit(onSubmit)}
                        autoComplete="off">
                        <div className="row form-group">

                            <HomeAddressFields
                                checkFormValidity={checkFormValidity}
                                toggleSection={toggleSection}
                                setToggleSection={setToggleSection}
                                register={register} errors={errors}
                                getValues={getValues}
                                setValue={setValue}
                                clearErrors={clearErrors}
                                setFieldValues={setFieldValues}
                                initialData={initialData}
                                control={control}
                                showForm={showForm}
                                caller={HomeAddressCaller.CurrentHomeAddress}
                                homeAddressLabel={getHomeAddressLabel()}
                                homeAddressPlaceholder={"Search Home Address"}
                                restrictCountries={false}
                                unregister={unregister} />

                            <HousingStatus checkFormValidity={checkFormValidity} toggleSection={toggleSection} register={register} errors={errors} getValues={getValues} setValue={setValue} clearErrors={clearErrors} selectedHomeOwnershipTypeObj={selectedHomeOwnershipTypeObj} setSelectedHomeOwnershipTypeObj={setSelectedHomeOwnershipTypeObj} initialData={initialData} />
                        </div>

                        <div className="form-footer">
                            <button
                                // disabled ={isButtonActive}
                                data-testid={"current-home-btn"}
                                id={"current-home-btn"}
                                className="btn btn-primary"
                                onClick={() => {
                                    if (errors) {
                                        setToggleSection(true)
                                    }
                                    handleSubmit(onSubmit)
                                    //  setcurrentStep("maritalStatus");
                                }}>
                                {"Save & Continue"}
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        );
    }

    const onAddressCheck = () => {

        if (!selectedOption) {
            setInitialData(secHomeAddress)
        }
        else {
            setInitialData(primaryHomeAddress)
        }
        console.log()
        setShowForm(true)
    }

    const applicantHasSameAddressQuestion = () => {

        return (

            <div className="compo-abt-yourSelf fadein">
                <PageHead title="Personal Information" />
                <TooltipTitle title={`Thanks! Please tell us about ${loanInfo && StringServices.capitalizeFirstLetter(loanInfo.borrowerName)}'s current home address.`} />

                <div className="comp-form-panel colaba-form">
                    <form
                        id="address-form-form"
                        data-testid="address-form-form"
                        className="colaba-form listaddress-warp current-home-address"
                        onSubmit={handleSubmit2(onAddressCheck)}
                        autoComplete="off">

                        <div className="row">
                            <div className="col-md-12">
                                <div className="form-group">
                                    <h4>{`Is ${loanInfo && StringServices.capitalizeFirstLetter(loanInfo.borrowerName)}’s current home address the same as yours?`}</h4>

                                    <span className="list-add form-group"><span className="icon-add"><AddressHomeIcon /></span><span dangerouslySetInnerHTML={{ __html: homeAddress }}></span></span>

                                    <div className="clearfix">
                                        <InputRadioBox
                                            id=""
                                            className=""
                                            name="addressCheck"
                                            register={register2}
                                            value={"Yes"}
                                            rules={{
                                                required: "Please select one"
                                            }}
                                            onChange={() => { setSelectedOption(true) }}
                                            errors={errors2}
                                        >Yes</InputRadioBox>
                                    </div>

                                    <div className="clearfix">
                                        <InputRadioBox
                                            id=""
                                            className=""
                                            name="addressCheck"
                                            register={register2}
                                            onChange={() => { setSelectedOption(false) }}
                                            value={"No"}
                                            rules={{
                                                required: "Please select one"
                                            }}
                                            errors={errors2}
                                        >No</InputRadioBox>
                                    </div>

                                    {errors2?.addressCheck && <span className="form-error no-padding" role="alert" data-testid="addressCheck-error">{errors2?.addressCheck?.message}</span>}
                                </div>

                            </div>
                        </div>

                        <div className="form-footer">

                            <button
                                disabled={selectedOption === undefined ? true : false}
                                className="btn btn-primary" onClick={handleSubmit2(onAddressCheck)} >
                                {"Save & Continue"}
                            </button>
                        </div>
                    </form>
                </div>

            </div>
        )
    }
    const showLoader = () => {
        return (
            <div className="compo-hcwh fadein">
                <Fragment>
                    <PageHead
                        title="Personal Information"
                    />
                </Fragment>

                <Fragment>
                    <TooltipTitle />
                </Fragment>
                {isLoading &&
                    <div><Loader type="page" /></div>
                }



            </div>
        );
    }

    return (
        isLoading ? showLoader() : loanInfo.ownTypeId === 2 && !showForm && !isCurrentHomeSet ? applicantHasSameAddressQuestion() : renderCurrentHome()
    );
};

export default AboutCurrentHome;
