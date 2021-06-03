import React, {ChangeEvent, useContext, useEffect, useState} from "react";
import InputField from "../../../../../../../Shared/Components/InputField";
import InputDatepicker from "../../../../../../../Shared/Components/InputDatepicker";

import {Controller, useForm} from "react-hook-form";
import {
    maskPhoneNumber,
    unMaskPhoneNumber,
} from "../../../../../../../Utilities/helpers/PhoneMasking";

import DropdownList from "../../../../../../../Shared/Components/DropdownList";
import Dropdown from "react-bootstrap/Dropdown";

import {ApplicationEnv} from "../../../../../../../lib/appEnv";
import {Store} from "../../../../../../../store/store";
import {BusinessActionTypes} from "../../../../../../../store/reducers/BusinessIncomeReducer";
import {DateServices} from "../../../../../../../Utilities/helpers/DateHelper";

import {CurrentBusinessDetails} from "../../../../../../../Entities/Models/Business";

import {BusinessType, IncomeInfo, LoanInfoType} from "../../../../../../../Entities/Models/types";

import BusinessActions from "../../../../../../../store/actions/BusinessActions";
import {NavigationHandler} from "../../../../../../../Utilities/Navigation/NavigationHandler";

import {ErrorHandler} from "../../../../../../../Utilities/helpers/ErrorHandler";
import _ from 'lodash';
import Loader from "../../../../../../../Shared/Components/Loader";


export const BusinessIncomeType = () => {
    const [startDate, setStartDate] = useState<any>(null);
    const [businessName, setBusinessName] = useState<string>("");
    const [jobTitle, setJobTitle] = useState<string>("");
    const [phoneNumber, setPhoneNumber] = useState<string>("");
    const [ownershipPercentage, setOwnershipPercentage] = useState<string>("");
    const [selectedBusinessType, setSelectedBusinessType] = useState<string | undefined>("");


    const {state, dispatch} = useContext(Store);
    const {
        businessInfo
    }: any = state.business;
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const incomeInfo: IncomeInfo = loanManager.incomeInfo;
    const [isClicked, setIsClicked] = useState<boolean>(false);
    const [businessTypes, setBusinessTypes] = useState<Array<BusinessType>>([]);
    const {
        register,
        errors,
        handleSubmit,
        setValue,
        clearErrors,
        control,
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useEffect(() => {
        fetchData()
    }, [loanInfo, incomeInfo,businessTypes ]);
    const fetchData = async () => {
        if (loanInfo && loanInfo.borrowerId != null) {
            let response = await BusinessActions.getAllBusinessTypes();
            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    if(businessTypes.length === 0) {
                            setBusinessTypes(response.data)
                            setIsClicked(false)
                        }

                    if (incomeInfo.incomeId && loanInfo.borrowerId && businessTypes.length > 0 && _.isEmpty(businessInfo)) {
                        let response = await BusinessActions.getBusinessIncome(loanInfo.borrowerId, incomeInfo.incomeId);
                        if (response) {
                            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                                let selectedIncomeType  = await businessTypes && businessTypes?.filter((businessType) => businessType.id === response.data.incomeTypeId)[0]
                                setSelectedBusinessType(selectedIncomeType?.name);
                                setBusinessName(response.data.businessName);
                                setPhoneNumber(response.data.businessPhone);
                                setStartDate(new Date(response.data.startDate));
                                setValue("startDate", new Date(response.data.startDate));
                                setJobTitle(response.data.jobTitle);
                                setOwnershipPercentage(response.data.ownershipPercentage);

                                dispatch({type: BusinessActionTypes.SetCurrentBusinessDetails, payload: response.data});

                            }
                            else {
                                ErrorHandler.setError(dispatch, response);
                            }
                        }
                    } else if (!_.isEmpty(businessInfo)) { /*if businessInfo already set in store*/

                        let selectedIncomeType = await businessTypes && businessTypes?.filter((businessType) => businessType.id === businessInfo?.incomeTypeId)[0]

                        if(selectedIncomeType)
                        setSelectedBusinessType(selectedIncomeType.name);

                        setBusinessName(businessInfo?.businessName);
                        setPhoneNumber(businessInfo?.businessPhone);
                        setStartDate(new Date(businessInfo?.startDate));
                        setValue("startDate", new Date(businessInfo?.startDate));
                        setJobTitle(businessInfo?.jobTitle);
                        setOwnershipPercentage(businessInfo?.ownershipPercentage);
                    }

                }
                else {
                    ErrorHandler.setError(dispatch, response);
                }
            }

        }

    }


    const onSubmit = async (data) => {
        if (!isClicked) {
            setIsClicked(true)

            let currentBusinessDetails: CurrentBusinessDetails = {...businessInfo}
            currentBusinessDetails.startDate = DateServices.convertDateToUTC(data.startDate);
            if (loanInfo.loanApplicationId) currentBusinessDetails.loanApplicationId = loanInfo.loanApplicationId
            if (loanInfo.borrowerId)currentBusinessDetails.borrowerId = loanInfo.borrowerId

            let selectedIncomeType = await businessTypes && businessTypes?.filter((businessType) => businessType.name === data.businessType)[0]

            console.info(selectedBusinessType);

            if (selectedIncomeType?.id) currentBusinessDetails.incomeTypeId = selectedIncomeType?.id
            currentBusinessDetails.ownershipPercentage = +ownershipPercentage;
            currentBusinessDetails.businessPhone = unMaskPhoneNumber(data.businessPhoneNumber);
            currentBusinessDetails.businessName = data.businessName;
            currentBusinessDetails.jobTitle = data.jobTitle;

            //let title = `${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s ${StringServices.capitalizeFirstLetter(typeName)}`;
            dispatch({type: BusinessActionTypes.SetCurrentBusinessDetails, payload: currentBusinessDetails});
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Business/BusinessAddress`);
        }
    };

    const onPhoneNumberChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
        clearErrors("businessPhoneNumber");
        const mobileNumber = unMaskPhoneNumber(event.target.value);
        if (mobileNumber.length > 0 && !/^[0-9]{1,10}$/g.test(mobileNumber)) {
            return false;
        }
        // if (
        //   mobileNumber.length === 10 &&
        //   !checkValidUSNumber(mobileNumber)
        // ) {
        //   setError("homePhoneNumber", {
        //     type: "server",
        //     message: "Please enter US Phone Number only.",
        //   });
        //   return false;
        // }

        setPhoneNumber(mobileNumber);
        return true;
    };

    const onBusinessTypeSelection = async (incomeTypeId: string | number) => {
        let selectedIncomeType = await businessTypes && businessTypes?.filter((businessType) => businessType.id === +incomeTypeId)[0]
        setSelectedBusinessType(selectedIncomeType?.name);
        clearErrors("businessType")
    }



    return (
        businessTypes.length>0?
        <>
            <div>
                <form
                    id="employer-info-form"
                    data-testid="employer-info-form"
                    onSubmit={handleSubmit(onSubmit)}
                    autoComplete="off">
                    <div className="p-body">
                        <div className="row">
                            <div className="col-md-6">
                                <DropdownList
                                    label={"Select Your Business Type"}
                                    data-testid="business-type"
                                    id="business-type"
                                    placeholder="Business Type"
                                    name="businessType"
                                    onDropdownSelect={onBusinessTypeSelection}
                                    value={selectedBusinessType}
                                    register={register}
                                    rules={{
                                        required: "This field is required.",
                                    }}
                                    errors={errors}
                                >
                                    {businessTypes && businessTypes.map((businessType: any) => (
                                        <Dropdown.Item data-testid={"businessType-options"} key={businessType.id}
                                                    eventKey={businessType.id}>
                                            {businessType.name}
                                        </Dropdown.Item>
                                    ))}
                                </DropdownList>
                            </div>
                        </div>
                        {(selectedBusinessType)&&
                        <div className="row">
                            <div className="col-sm-6">
                                <InputField
                                    label={"Business Name"}
                                    data-testid={"business-name"}
                                    id={"business-name"}
                                    name="businessName"
                                    type={"text"}
                                    placeholder={"Business Name Here"}
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                        setBusinessName(e.target.value);
                                        clearErrors("businessName");
                                    }}
                                    value={businessName}
                                    register={register}
                                    rules={{
                                        required: "This field is required.",
                                    }}
                                    errors={errors}
                                />
                            </div>
                            <div className="col-sm-6">
                                <InputField
                                    label={"Business Phone Number"}
                                    data-testid={"business-phone-number"}
                                    id={"employer-phone-number"}
                                    name="businessPhoneNumber"
                                    type={"text"}
                                    placeholder={"XXX-XXX-XXXX"}
                                    onChange={onPhoneNumberChangeHandler}
                                    value={!!phoneNumber ? maskPhoneNumber(phoneNumber) : ""}
                                    register={register}
                                    maxLength={14}
                                    rules={{
                                        minLength: {
                                            value: 14,
                                            message: "Please enter US Phone Number only",
                                        },
                                        required: "This field is required.",
                                    }}
                                    errors={errors}
                                />

                            </div>
                            <div className="col-sm-6">
                                {
                                    <Controller
                                        control={control}
                                        name={"startDate"}
                                        render={() => (
                                            <InputDatepicker
                                                isPreviousDateAllowed={true}
                                                // className={`${errors && 'error'}`}
                                                id={"startDate"}
                                                data-testid={"business-start-date"}
                                                label={`Business Start Date`}
                                                placeholder={"MM/DD/YYYY"}
                                                dateFormat="MM/dd/yyyy"
                                                name={"startDate"}
                                                handleOnChange={(date: any) => {
                                                    setStartDate(date);
                                                    setValue("startDate", date);
                                                    clearErrors("startDate")
                                                }}
                                                autoComplete={"off"}
                                                selected={startDate}
                                                handleOnChangeRaw={(e : ChangeEvent<HTMLInputElement>) => {
                                                    e.preventDefault();
                                                }}
                                                errors={errors}
                                                ref={register}
                                                isFutureDateAllowed = {false}
                                            />
                                        )}
                                        errors={errors}
                                        ref={register}
                                        rules={{
                                            required: "This field is required.",
                                            validate: {
                                                // validity: value => !(value > new Date())  || "DOB cannot be future date."
                                            },
                                        }}
                                        defaultValue={""}
                                    />
                                }
                            </div>
                            <div className="col-sm-6">
                                <InputField
                                    label={"Job Title"}
                                    data-testid={"job-title"}
                                    id={"job-title"}
                                    name="jobTitle"
                                    type={"text"}
                                    placeholder={"Job Title"}
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                        clearErrors("jobTitle");
                                        if (e.target.value.length > 0 && !/^[a-zA-Z0-9%&(.'\-\s]{1,150}$/g.test(e.target.value)) {
                                            return false
                                        }
                                        setJobTitle(e.target.value);
                                        return true;
                                    }}
                                    value={jobTitle}
                                    register={register}
                                    rules={{
                                        required: "This field is required.",
                                    }}
                                    errors={errors}
                                />
                            </div>
                            <div className="col-sm-6">
                                <InputField
                                    label={"Ownership Percentage"}
                                    data-testid={"ownership-percentage"}
                                    id={"ownership-percentage"}
                                    name="ownershipPercentage"
                                    icon={<i>%</i>}
                                    type={"text"}
                                    placeholder={"Percentage"}
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                        let value = e.target.value;
                                        if ( value != "" && (isNaN(+value) || +value < 1 || +value > 100) ) {

                                            return;
                                        } else {
                                            setOwnershipPercentage(value);
                                            clearErrors("ownershipPercentage");
                                        }


                                    }}
                                    value={ownershipPercentage}
                                    register={register}
                                    rules={{
                                        required: "This field is required.",
                                    }}
                                    errors={errors}
                                />
                            </div>
                        </div>
                        }

                    </div>
                    <div className="p-footer">
                        <button
                            id="employer-info-next"
                            data-testid="business-info-next"
                            className="btn btn-primary"
                            type="submit"
                            disabled={false}
                            onChange={handleSubmit(onSubmit)}>
                            Next
                        </button>
                    </div>
                </form>
            </div>
        </>:<Loader type="widget" />
    );
};
