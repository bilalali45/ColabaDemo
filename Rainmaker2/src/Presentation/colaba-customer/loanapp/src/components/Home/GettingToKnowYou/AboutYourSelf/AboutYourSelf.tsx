import React, { useContext, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import InputField from "../../../../Shared/Components/InputField";
import {
    maskPhoneNumber,
    unMaskPhoneNumber,
} from "../../../../Utilities/helpers/PhoneMasking";
import { GettingToKnowYouSteps } from "../../../../store/actions/LeftMenuHandler";
import { LocalDB } from "../../../../lib/LocalDB";
import BorrowerActions from "../../../../store/actions/BorrowerActions";
import {
    LoanInfoType,
    PrimaryBorrowerInfo,
} from "../../../../Entities/Models/types";
import { Store } from "../../../../store/store";
import { BorrowerBasicInfo } from "../../../../Entities/Models/BorrowerInfo";
import { LoanApplicationActionsType } from "../../../../store/reducers/LoanApplicationReducer";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { OwnTypeEnum } from "../../../../Utilities/Enum";
import { truncate } from "../../../../Utilities/helpers/TruncateString";
import { checkValidUSNumber } from "../../../../Utilities/helpers/PhoneMasking";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

import { TenantConfigFieldNameEnum } from "../../../../Utilities/Enumerations/TenantConfigEnums";

export const AboutYourSelf = () => {
  const { state, dispatch } = useContext(Store);
  const loanManager: any = state.loanManager;
  const loanInfo: LoanInfoType = loanManager.loanInfo;

    const {
        register,
        errors,
        handleSubmit,
        setError,
        clearErrors,
        setValue,
        reset
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "firstError",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    const [legalFirstName, setLegalFirstName] = useState<string>("");
    const [middleName, setMiddleName] = useState<string>("");
    const [legalLastName, setLegalLastName] = useState<string>("");
    const [suffix, setsuffix] = useState<string>("");
    const [workPhone, setworkPhone] = useState<string>("");
    const [cellPhone, setCellPhone] = useState<string>("");
    const [homePhone, sethomePhone] = useState<string>("");
    const [phoneExt, setPhoneExt] = useState<string>("");
    const [isPrimary, setIsPrimary] = useState<boolean | undefined>(undefined);
    const [btnClick, setBtnClick] = useState<boolean>(false);
    const [error, setErrors] = useState<boolean>(false);
    const [fieldsConfiguration, setFieldsConfiguration] = useState<any>({
        isEmailAddressVisible: true,
        isEmailAddressRequired: true,
        isHomePhoneNumberVisible: true,
        isHomePhoneNumberRequired: false,
        isWorkNumberVisible: true,
        isWorkNumberRequired: false,
        isCellNumberVisible: true,
        isCellNumberRequired: false
    });

    useEffect(() => {
        checkBorrowerExist();
    }, [loanInfo?.borrowerId]);



    const populateFieldsConfiguration = (isPrimaryBorrower: boolean) => {
        setFieldsConfiguration({
            isEmailAddressVisible: !isPrimaryBorrower ? NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorrowerEmailAddress) : true,
            isEmailAddressRequired: !isPrimaryBorrower ? NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorrowerEmailAddress, true) : true,
            isHomePhoneNumberVisible: (!isPrimaryBorrower ? NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorrowerHomeNumber) : NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorrowerHomeNumber)),
            isHomePhoneNumberRequired: (!isPrimaryBorrower ? NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorrowerHomeNumber, false) : NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PrimaryBorrowerHomeNumber, false)),
            isWorkNumberVisible: (!isPrimaryBorrower ? NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorrowerWorkNumber) : NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorrowerWorkNumber)),
            isWorkNumberRequired: (!isPrimaryBorrower ? NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorrowerWorkNumber, false) : NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PrimaryBorrowerWorkNumber, false)),
            isCellNumberVisible: (!isPrimaryBorrower ? NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorrowerCellNumber) : NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorrowerCellNumber)),
            isCellNumberRequired: (!isPrimaryBorrower ? NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorrowerCellNumber, true) : NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PrimaryBorrowerCellNumber, true))
        });
    }

    const checkBorrowerExist = async () => {
        if (!loanInfo?.borrowerId) {
            // checking borrower in Store
            let isBorrowerAlreadyExist = await getAllBorrower(); //Checking any borrower created or not
            if (!isBorrowerAlreadyExist) {
                // If borrower not created, then filled with login data as basic info
                setIsPrimary(true);
                populateFieldsConfiguration(true);
                populatePrimaryBorrower();
            } else {
                reset();
                loanInfo?.ownTypeId === OwnTypeEnum.PrimaryBorrower ? setIsPrimary(true) : loanInfo?.ownTypeId === OwnTypeEnum.SecondaryBorrower ? setIsPrimary(false) : ''
                populateFieldsConfiguration(false);
            }
        } else {
            if (loanInfo?.ownTypeId === OwnTypeEnum.PrimaryBorrower) {
                setIsPrimary(true);
                populateFieldsConfiguration(true);
            } else {
                setIsPrimary(false);
                populateFieldsConfiguration(false);
            }
            getBorrowerInfo(loanInfo?.borrowerId);
        }
    };

    const getAllBorrower = async () => {
        let loanApplicationid = LocalDB.getLoanAppliationId();
        if (loanApplicationid) {
            let response = await BorrowerActions.getAllBorrower(+loanApplicationid);

            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    return response.data.length > 0 ? true : false;
                }
            }
        }
        return null;
    };

    const getBorrowerInfo = async (borrowerId: number) => {
        let loanApplicationid = LocalDB.getLoanAppliationId();
        let response: BorrowerBasicInfo = await BorrowerActions.getBorrowerInfo(
            +loanApplicationid,
            borrowerId
        );
        populateFields(response);
    };

    const populatePrimaryBorrower = async () => {
        let response: BorrowerBasicInfo = await BorrowerActions.populatePrimaryBorrower();
        populateFields(response);
    };

    const addOrUpdatedPrimaryBorrower = async (data) => {
        if (!error) {
            if (!btnClick) {
                setBtnClick(true);
                let borrowerBasicInfo = new BorrowerBasicInfo(
                    +LocalDB.getLoanAppliationId(),
                    loanInfo?.borrowerId,
                    data.legalFirstName,
                    data.legalLastName,
                    data.middleName,
                    data.suffix,
                    data.emailAddress,
                    unMaskPhoneNumber(data.homePhoneNumber),
                    unMaskPhoneNumber(data.workPhoneNumber),
                    data.extension,
                    unMaskPhoneNumber(data.cellPhoneNumber),
                    NavigationHandler.getNavigationStateAsString()
                );
                let response: any = await BorrowerActions.addOrUpdateBorrowerInfo(
                    borrowerBasicInfo
                );
                if (response) {
                    if (response.ownTypeId === OwnTypeEnum.PrimaryBorrower) {
                        let borrowerInfo: PrimaryBorrowerInfo = {
                            id: response.id,
                            name: legalFirstName,
                        };
                        dispatch({
                            type: LoanApplicationActionsType.SetPrimaryBorrowerInfo,
                            payload: borrowerInfo,
                        });
                    }

                    const allBorrwers = await BorrowerActions.getAllBorrower(+LocalDB.getLoanAppliationId());
                    if (allBorrwers?.data.length > 1) {
                        NavigationHandler.disableFeature(GettingToKnowYouSteps.CoApplicant);
                    } else {
                        NavigationHandler.enableFeature(GettingToKnowYouSteps.CoApplicant);
                    }

                    dispatch({
                        type: LoanApplicationActionsType.SetLoanInfo,
                        payload: {
                            ...loanInfo,
                            borrowerId: response.id,
                            ownTypeId: response.ownTypeId,
                            borrowerName: data.legalFirstName + ' ' + data.legalLastName,
                        },
                    });
                    LocalDB.setBorrowerId(String(response.id));
                    NavigationHandler.moveNext();
                }
            }
        } else {
            setError("workPhoneNumber", {
                type: "server",
                message: "This field is required.",
            });
        }
    };

    const onChangeHandler = (
        event: React.FormEvent<HTMLInputElement>,
        field: string
    ) => {

        const value = event.currentTarget.value;
        if (value.length > 0 && !/^[A-Za-z\s&(%'.-]+$/.test(value)) {
            return;
        }
        switch (field) {
            case "firstName":
                setLegalFirstName(value);
                break;
            case "middleName":
                setMiddleName(value);
                break;
            case "lastName":
                setLegalLastName(value);
                break;
        }
    };

    const populateFields = (borrowerInfo: BorrowerBasicInfo) => {
        setValue("legalFirstName", borrowerInfo.firstName);
        setValue("legalLastName", borrowerInfo.lastName);
        setValue("middleName", borrowerInfo.middleName);
        setValue("suffix", borrowerInfo.suffix);
        setValue("emailAddress", borrowerInfo.email);
        setValue(
            "homePhoneNumber",
            !!borrowerInfo.homePhone ? maskPhoneNumber(borrowerInfo.homePhone) : ""
        );
        setValue(
            "workPhoneNumber",
            !!borrowerInfo.workPhone ? maskPhoneNumber(borrowerInfo.workPhone) : ""
        );
        setValue("extension", borrowerInfo.workExt);
        setValue("cellPhoneNumber", !!borrowerInfo.cellPhone ? maskPhoneNumber(borrowerInfo.cellPhone) : "");

        setLegalFirstName(!!borrowerInfo.firstName ? borrowerInfo.firstName : "");
        setLegalLastName(!!borrowerInfo.lastName ? borrowerInfo.lastName : "");
        setMiddleName(!!borrowerInfo.middleName ? borrowerInfo.middleName : "");
        setsuffix(!!borrowerInfo.suffix? borrowerInfo.suffix: "");
        setworkPhone(!!borrowerInfo.workPhone ? borrowerInfo.workPhone : "");
        setCellPhone(!!borrowerInfo.cellPhone ? borrowerInfo.cellPhone : "");
        sethomePhone(!!borrowerInfo.homePhone ? borrowerInfo.homePhone : "");
        setPhoneExt(!!borrowerInfo.workExt ? borrowerInfo.workExt : "");
    };

    return (
        <div className="compo-abt-yourSelf fadein">
            <PageHead title="Personal Information" />
            <TooltipTitle
                title={isPrimary === true ? `Hey ${truncate(legalFirstName, 50)}, Please provide few details about yourself.`
                    : isPrimary === false ?
                        `Please provide a few details about your co-applicant` : ''
                }
            />
            <form data-testid="personal-info-form">
                <div className="comp-form-panel colaba-form">
                    <div className="row form-group">
                        <div className="col-md-6">
                            <InputField
                                label={
                                    isPrimary === true
                                        ? "Legal First Name"
                                        : isPrimary === false
                                            ? "Co-applicant Legal First Name"
                                            : ""
                                }
                                data-testid="LegalFirstName"
                                id="LegalFirstName"
                                name="legalFirstName"
                                type={"text"}
                                placeholder={"Legal First Name"}
                                autoFocus={true}
                                maxLength={100}
                                register={register}
                                value={legalFirstName}
                                onChange={(event) => {
                                    clearErrors("legalFirstName")
                                    onChangeHandler(event, "firstName")
                                }
                                }
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                            />
                        </div>
                        <div className="col-md-6">
                            <InputField
                                label={"Middle Name"}
                                data-testid="MiddleName"
                                id="MiddleName"
                                name="middleName"
                                type={"text"}
                                placeholder={"Middle Name"}
                                value={middleName}
                                maxLength={100}
                                register={register}
                                onChange={(event) => {
                                    clearErrors("middleName")
                                    onChangeHandler(event, "middleName")
                                }
                                }
                                errors={errors}
                            />
                        </div>
                        <div className="col-md-6">
                            <InputField
                                label={
                                    isPrimary === true
                                        ? "Legal Last Name"
                                        : isPrimary === false
                                            ? "Co-applicant Legal Last Name"
                                            : ""
                                }
                                data-testid="LegalLastName"
                                id="LegalLastName"
                                name="legalLastName"
                                type={"text"}
                                placeholder={"Legal Last Name"}
                                value={legalLastName}
                                maxLength={100}
                                register={register}
                                onChange={(event) => {
                                    clearErrors("legalLastName")
                                    onChangeHandler(event, "lastName")
                                }}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                            />
                        </div>
                        <div className="col-md-6">
                            <InputField
                                label={"Suffix"}
                                data-testid="Suffix"
                                id="Suffix"
                                name="suffix"
                                type={"text"}
                                register={register}
                                placeholder={"Jr., Sr., III, IV, etc."}
                                value={suffix}
                                onChange={(event) => {
                                    let value = event.currentTarget.value;
                                    if (value.length > 0 && !/^[A-Za-z\s,]+$/.test(value)) {
                                        return;
                                    } else {
                                        setsuffix(value);
                                    }
                                }}
                            />
                        </div>
                        {
                            (fieldsConfiguration.isEmailAddressVisible) &&
                            <div className="col-md-6">
                                <InputField
                                    label={"Email Address"}
                                    data-testid="EmailAddress"
                                    id="EmailAddress"
                                    name="emailAddress"
                                    type={"email"}
                                    placeholder={"Email Address"}
                                    maxLength={100}
                                    register={register}
                                    onChange={() => {
                                        clearErrors("emailAddress")
                                    }}
                                    rules={{
                                        required: {
                                            value: fieldsConfiguration.isEmailAddressRequired,
                                            message: "This field is required.",
                                        },
                                        pattern: {
                                            value: /^[A-Z0-9._-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                            message: "Please enter a valid email address.",
                                        },
                                    }}
                                    errors={errors}
                                />
                            </div>
                        }
                        {
                            fieldsConfiguration.isHomePhoneNumberVisible &&
                            <div className="col-md-6">
                                <InputField
                                    label={"Home Phone Number"}
                                    data-testid="HomePhoneNumber"
                                    id="HomePhoneNumber"
                                    name="homePhoneNumber"
                                    type={"text"}
                                    placeholder={"XXX-XXX-XXXX"}
                                    maxLength={14}
                                    rules={{
                                        required: {
                                            value: fieldsConfiguration.isHomePhoneNumberRequired,
                                            message: "This field is required.",
                                        },
                                        minLength: {
                                            value: 14,
                                            message: "Please enter US Phone Number only",
                                        },
                                    }}
                                    value={!!homePhone ? maskPhoneNumber(homePhone) : ""}
                                    register={register}
                                    onChange={(event) => {
                                        clearErrors();
                                        const mobileNumber = unMaskPhoneNumber(event.target.value);
                                        if (
                                            mobileNumber.length > 0 &&
                                            !/^[0-9]{1,10}$/g.test(mobileNumber)
                                        ) {
                                            return false;
                                        }
                                        if (
                                            mobileNumber.length === 10 &&
                                            !checkValidUSNumber(mobileNumber)
                                        ) {
                                            setError("homePhoneNumber", {
                                                type: "server",
                                                message: "Please enter US Phone Number only.",
                                            });
                                            return false;
                                        }

                                        sethomePhone(mobileNumber);
                                        return true;
                                    }}
                                    errors={errors}
                                />
                            </div>
                        }
                        {
                            fieldsConfiguration.isWorkNumberVisible &&
                            <div className="col-md-6">
                                <div className="row">
                                    <div className="col-md-8">
                                        <InputField
                                            label={"Work Phone Number "}
                                            data-testid="WorkPhoneNumber "
                                            id="WorkPhoneNumber"
                                            name="workPhoneNumber"
                                            type={"text"}
                                            placeholder={"XXX-XXX-XXXX"}
                                            value={!!workPhone ? maskPhoneNumber(workPhone) : ""}
                                            register={register}
                                            rules={{
                                                required: {
                                                    value: fieldsConfiguration.isWorkNumberRequired,
                                                    message: "This field is required.",
                                                },
                                                minLength: {
                                                    value: 14,
                                                    message: "Please enter US Phone Number only",
                                                },
                                            }}
                                            onChange={(event) => {
                                                clearErrors();
                                                setErrors(false);
                                                const mobileNumber = unMaskPhoneNumber(
                                                    event.target.value
                                                );
                                                if (
                                                    mobileNumber.length > 0 &&
                                                    !/^[0-9]{1,10}$/g.test(mobileNumber)
                                                ) {
                                                    return false;
                                                }

                                                if (
                                                    mobileNumber.length === 10 &&
                                                    !checkValidUSNumber(mobileNumber)
                                                ) {
                                                    setError("workPhoneNumber", {
                                                        type: "server",
                                                        message: "Please enter US Phone Number only.",
                                                    });
                                                    return false;
                                                }

                                                if (mobileNumber.length === 0 && (phoneExt?.length > 0)) {
                                                    setError("workPhoneNumber", {
                                                        type: "server",
                                                        message: "This field is required.",
                                                    });
                                                    setworkPhone(mobileNumber);
                                                    setErrors(true);
                                                    return false;
                                                }
                                                setworkPhone(mobileNumber);
                                                return true;
                                            }}
                                            errors={errors}
                                        />
                                    </div>
                                    <div className="col-md-4">
                                        <InputField
                                            //disabled={workPhone.length === 10 ? false : true}
                                            label={""}

                                            data-testid="ext"
                                            id="extension"
                                            name="extension"
                                            type={"text"}
                                            placeholder={"EXT. XXXX"}
                                            register={register}
                                            value={phoneExt}
                                            onChange={(event) => {
                                                setErrors(false);
                                                let value = event.currentTarget.value;
                                                if (workPhone.length === 0 && (value?.length > 0)) {
                                                    setError("workPhoneNumber", {
                                                        type: "server",
                                                        message: "This field is required.",
                                                    });
                                                    setErrors(true);

                                                }

                                                if (value.length > 8) {
                                                    return false;
                                                }

                                                if (value.length > 0 && !/^[0-9]{1,10}$/g.test(value)) {
                                                    let val = phoneExt == null ? "" : phoneExt;
                                                    setPhoneExt(val);
                                                    return false;
                                                }

                                                setPhoneExt(value);
                                                return true;
                                            }}
                                            errors={errors}
                                        />
                                    </div>
                                </div>
                            </div>
                        }
                        {
                            fieldsConfiguration.isCellNumberVisible &&
                            <div className="col-md-6" >
                                <InputField
                                    label={"Cell Phone Number"}
                                    data-testid="CellPhoneNumber"
                                    id="CellPhoneNumber"
                                    name="cellPhoneNumber"
                                    type={"text"}
                                    placeholder={"XXX-XXX-XXXX"}
                                    maxLength={14}
                                    value={!!cellPhone ? maskPhoneNumber(cellPhone) : ""}
                                    rules={{
                                        required: {
                                            value: fieldsConfiguration.isCellNumberRequired,
                                            message: "This field is required."
                                        },
                                        minLength: {
                                            value: 14,
                                            message: "Please enter US Phone Number only",
                                        },
                                    }}
                                    register={register}
                                    onChange={(event) => {
                                        clearErrors();
                                        const mobileNumber = unMaskPhoneNumber(event.target.value);
                                        if (
                                            mobileNumber.length > 0 &&
                                            !/^[0-9]{1,10}$/g.test(mobileNumber)
                                        ) {
                                            return false;
                                        }

                                        if (
                                            mobileNumber.length === 10 &&
                                            !checkValidUSNumber(mobileNumber)
                                        ) {
                                            setError("cellPhoneNumber", {
                                                type: "server",
                                                message: "Please enter US Phone Number only....",
                                            });
                                            return false;
                                        }
                                        setCellPhone(mobileNumber);
                                        return true;
                                    }}
                                    errors={errors}
                                />
                            </div>
                        }

                    </div>
                    <div className="form-footer">
                        <button
                            data-testid="ays-submit-btn"
                            id="ays-submit-btn"
                            className="btn btn-primary"
                            type="submit"
                            onClick={handleSubmit(addOrUpdatedPrimaryBorrower)}
                        >
                            {"Save & Continue"}
                        </button>
                    </div>
                </div>
            </form>
        </div>
    );
};
