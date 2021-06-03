import React, { ChangeEvent } from "react";
import { useForm } from "react-hook-form";

import InputCheckedBox from "../../../../../Shared/Components/InputCheckedBox";
import InputField from "../../../../../Shared/Components/InputField";
import InputRadioBox from "../../../../../Shared/Components/InputRadioBox";
import { AddressHomeIcon } from "../../../../../Shared/Components/SVGs";

import { TenantConfigFieldNameEnum } from "../../../../../Utilities/Enumerations/TenantConfigEnums";
import { CommaFormatted } from "../../../../../Utilities/helpers/CommaSeparteMasking";
import { PropertyMortgageFirstStepDetailsPostObj } from "../PropertyMortgageFirstStepDetails/PropertyMortgageFirstStepDetails";

import { NavigationHandler } from "./../../../../../Utilities/Navigation/NavigationHandler";

type PropertyFirstMortgageDetailsFormProps = {
    firstPayment: string;
    setFirstPayment: Function;
    firstPaymentBalance: string;
    setFirstPaymentBalance: Function;
    propTax: string;
    setPropTax: Function;
    isTaxIncInPayment: boolean;
    setIsTaxIncludedInPayment: Function;
    propInsurance: string;
    setPropInsurance: Function;
    isInsuranceIncInPayment: boolean;
    setIsInsuranceIncludedInPayment: Function;
    isHELOC: boolean;
    setIsHELOC: Function;
    creditLimit: string;
    setCreditLimit: Function;
    onSave: Function;
    homeAddress: string;
    setFloodIns: Function;
    floodIns: string;
    showPaidOff: boolean;
    isPaidOff: boolean | null;
    setIsPaidOff: Function;
    isFloodInsuranceIncInPayment: boolean;
    setIsFloodInsuranceIncludedInPayment: Function;
};
export const PropertyFirstMortgageDetailsForm = ({
    firstPayment,
    setFirstPayment,
    firstPaymentBalance,
    setFirstPaymentBalance,
    propTax,
    setPropTax,
    isTaxIncInPayment,
    setIsTaxIncludedInPayment,
    propInsurance,
    setPropInsurance,
    isInsuranceIncInPayment,
    setIsInsuranceIncludedInPayment,
    isHELOC,
    setIsHELOC,
    creditLimit,
    setCreditLimit,
    onSave,
    homeAddress,
    setFloodIns,
    floodIns,
    showPaidOff,
    isPaidOff,
    setIsPaidOff,
    isFloodInsuranceIncInPayment,
    setIsFloodInsuranceIncludedInPayment
}: PropertyFirstMortgageDetailsFormProps) => {
    const { register, errors, handleSubmit, clearErrors } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    const onSubmit = (data: PropertyMortgageFirstStepDetailsPostObj) => {
        onSave(data);
    };

    const CheckInputValidity = (value: string) => {
        if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
            return false;
        }
        return true;
    };

    console.log(isPaidOff)
    return (
        <div className="comp-form-panel income-panel colaba-form">
            <div className="row form-group">
                <div className="col-md-12">
                    <div className="listaddress-warp">
                        <div className="list-add">
                            <div className="icon-add">
                                <AddressHomeIcon />
                            </div>
                            <div className="cont-add">{homeAddress}</div>
                        </div>
                    </div>
                </div>
            </div>

            <form
                id="first-mortgage-details-form"
                data-testid="first-mortgage-details-form"
                autoComplete="off">
                <div className="row form-group">
                    <div className="col-md-6">
                        <InputField
                            label={"First Mortgage Payment"}
                            id="first_Payment"
                            name="first_Payment"
                            icon={<i className="zmdi zmdi-money"></i>}
                            type={"text"}
                            placeholder={"Monthly Payment"}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                clearErrors("first_Payment");
                                const value = e.currentTarget.value;
                                let res = CheckInputValidity(value);
                                if (res) setFirstPayment(value.replace(/\,/g, ""));
                            }}
                            register={register}
                            value={firstPayment ? CommaFormatted(firstPayment) : ""}
                            rules={{
                                required: "This field is required.",
                                validate: {
                                    validity: (value) =>
                                        !(value === "0.00") || "Amount must be greater than 0",
                                },
                            }}
                            errors={errors}
                        />
                    </div>
                    <div className="col-md-6">
                        <InputField
                            label={"Unpaid First Mortgage Balance"}
                            id="first_pay_bal"
                            name="first_pay_bal"
                            icon={<i className="zmdi zmdi-money"></i>}
                            type={"text"}
                            placeholder={"Mortgage Balance"}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                clearErrors("first_pay_bal");
                                const value = e.currentTarget.value;
                                let res = CheckInputValidity(value);
                                if (res) setFirstPaymentBalance(value.replace(/\,/g, ""));
                            }}
                            register={register}
                            value={
                                firstPaymentBalance ? CommaFormatted(firstPaymentBalance) : ""
                            }
                            rules={{
                                required: "This field is required.",
                                validate: {
                                    validity: (value) =>
                                        !(value === "0.00") || "Amount must be greater than 0",
                                },
                            }}
                            errors={errors}
                        />
                    </div>

                    <div className="col-md-6">
                        <InputField
                            label={"Property Taxes"}
                            id="prop_tax"
                            name="prop_tax"
                            icon={<i className="zmdi zmdi-money"></i>}
                            type={"text"}
                            placeholder={"Estimated Annual Taxes"}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                clearErrors("prop_tax");
                                const value = e.currentTarget.value;
                                let res = CheckInputValidity(value);
                                if (res) setPropTax(value.replace(/\,/g, ""));
                            }}
                            register={register}
                            value={propTax ? CommaFormatted(propTax) : ""}
                            rules={{
                                required: "This field is required.",
                                validate: {
                                    validity: (value) =>
                                        !(value === "0.00") || "Amount must be greater than 0",
                                },
                            }}
                            errors={errors}
                        />

                        {NavigationHandler.isFieldVisible(
                            TenantConfigFieldNameEnum.TaxIncludedInPayment
                        ) && (
                                <div className="subcheck">
                                    <InputCheckedBox
                                        id="tax_inc_in_pay"
                                        className=""
                                        name="tax_inc_in_pay"
                                        label={`Included in payment?`}
                                        checked={
                                            isTaxIncInPayment === null ? false : isTaxIncInPayment
                                        }
                                        value={
                                            isTaxIncInPayment === null
                                                ? ""
                                                : isTaxIncInPayment?.toString()
                                        }
                                        onChange={() =>
                                            setIsTaxIncludedInPayment(!isTaxIncInPayment)
                                        }
                                        register={register}
                                        rules={
                                            {
                                                // required: "This field is required.",
                                            }
                                        }
                                        errors={errors}></InputCheckedBox>
                                </div>
                            )}
                    </div>

                    <div className="col-md-6">
                        <InputField
                            label={"Homeowner’s Insurance"}
                            id="prop_insurance"
                            name="prop_insurance"
                            icon={<i className="zmdi zmdi-money"></i>}
                            type={"text"}
                            placeholder={"Estimated Annual Premium"}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                clearErrors("prop_insurance");
                                const value = e.currentTarget.value;
                                let res = CheckInputValidity(value);
                                if (res) setPropInsurance(value.replace(/\,/g, ""));
                            }}
                            register={register}
                            value={propInsurance ? CommaFormatted(propInsurance) : ""}
                            rules={{
                                required: "This field is required.",
                                validate: {
                                    validity: (value) =>
                                        !(value === "0.00") || "Amount must be greater than 0",
                                },
                            }}
                            errors={errors}
                        />

                        {NavigationHandler.isFieldVisible(
                            TenantConfigFieldNameEnum.InsuranceIncludedInPayment
                        ) && (
                                <div className="subcheck">
                                    <InputCheckedBox
                                        id="ins_inc_in_pay"
                                        className=""
                                        name="ins_inc_in_pay"
                                        label={`Included in payment?`}
                                        checked={
                                            isInsuranceIncInPayment === null
                                                ? false
                                                : isInsuranceIncInPayment
                                        }
                                        value={
                                            isInsuranceIncInPayment === null
                                                ? ""
                                                : isInsuranceIncInPayment?.toString()
                                        }
                                        onChange={() =>
                                            setIsInsuranceIncludedInPayment(!isInsuranceIncInPayment)
                                        }
                                        register={register}
                                        rules={
                                            {
                                                // required: "This field is required.",
                                            }
                                        }
                                        errors={errors}></InputCheckedBox>
                                </div>
                            )}


                    </div>

                    <div className="col-md-6">
                        <InputField
                            label={"Flood Insurance"}
                            id="flood_insurance"
                            name="flood_insurance"
                            icon={<i className="zmdi zmdi-money"></i>}
                            type={"text"}
                            placeholder={"Estimated Annual Premium"}
                            register={register}
                            value={floodIns ? CommaFormatted(floodIns) : ""}
                            rules={{
                                required: "This field is required.",
                                validate: {
                                    validity: (value) =>
                                        !(value === "0.00") || "Amount must be greater than 0",
                                },
                            }}
                            errors={errors}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                clearErrors("flood_insurance");
                                const value = e.currentTarget.value;
                                let res = CheckInputValidity(value);
                                if (res) setFloodIns(value.replace(/\,/g, ""));
                            }}
                        />

                        {NavigationHandler.isFieldVisible(
                            TenantConfigFieldNameEnum.InsuranceIncludedInPayment
                        ) &&
                            <div className="subcheck">
                                <InputCheckedBox
                                    id="flood_ins_inc_in_pay"
                                    className=""
                                    name="flood_ins_inc_in_pay"
                                    label={`Included in payment?`}
                                    checked={
                                        isFloodInsuranceIncInPayment === null
                                            ? false
                                            : isFloodInsuranceIncInPayment
                                    }
                                    value={
                                        isFloodInsuranceIncInPayment === null
                                            ? ""
                                            : isFloodInsuranceIncInPayment?.toString()
                                    }
                                    onChange={() =>
                                        setIsFloodInsuranceIncludedInPayment(!isFloodInsuranceIncInPayment)
                                    }
                                    register={register}
                                    rules={
                                        {
                                            // required: "This field is required.",
                                        }
                                    }
                                    errors={errors}></InputCheckedBox>
                            </div>
                        }
                    </div>
                </div>

                <div className="Equityline">

                    {showPaidOff && (
                        <>
                            <div className="form-group">
                                <h4>Will this mortgage be paid off at or before closing?</h4>
                            </div>
                            <div className="form-group">
                                <div className="clearfix">
                                    <InputRadioBox
                                        id="paid_yes"
                                        data-testid="paid_yes"
                                        className=""
                                        name="isPaid"
                                        register={register}
                                        rules={{
                                            required: "Please select one",
                                        }}
                                        errors={errors}
                                        onChange={() => {
                                            clearErrors("isPaid");
                                            setIsPaidOff(true);
                                        }}
                                        checked={isPaidOff && isPaidOff ? true : false}
                                        value={"Yes"}>
                                        Yes
              </InputRadioBox>
                                </div>

                                <div className="clearfix">
                                    <InputRadioBox
                                        id="paid_no"
                                        data-testid="paid_no"
                                        className=""
                                        name="isPaid"
                                        register={register}
                                        rules={{
                                            required: "Please select one",
                                        }}
                                        errors={errors}
                                        onChange={() => {
                                            clearErrors("isPaid");
                                            setIsPaidOff(false);
                                        }}
                                        checked={(isPaidOff === null || isPaidOff === undefined) ? false : !isPaidOff ? true : false}
                                        value={"No"}>
                                        No</InputRadioBox>
                                </div>
                                {errors?.isPaid && (
                                    <span
                                        className="form-error no-padding"
                                        role="alert"
                                        data-testid="isPaid-error">
                                        {errors?.isPaid?.message}
                                    </span>
                                )}
                            </div>
                        </>
                    )}
                    <div className="row form-group">
                        <div className="col-md-6">
                            <InputCheckedBox
                                id="heloc"
                                // data-testid="heloc"
                                className="nowrap-text"
                                name="heloc"
                                label={`This is a Home Equity Line of Credit (HELOC)`}
                                checked={isHELOC === null ? false : isHELOC}
                                value={isHELOC === null ? "" : isHELOC?.toString()}
                                onChange={() => {
                                    setIsHELOC(!isHELOC);
                                    setCreditLimit("");
                                }}
                                register={register}
                                rules={
                                    {
                                        // required: "This field is required.",
                                    }
                                }
                                errors={errors}></InputCheckedBox>
                            {isHELOC && (
                                <div className="i-cl">
                                    <InputField
                                        label={"Credit Limit"}                                        
                                        id="credit_limit"
                                        name="credit_limit"
                                        icon={<i className="zmdi zmdi-money"></i>}
                                        type={"text"}
                                        placeholder={"Amount"}
                                        onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                            clearErrors("credit_limit");
                                            const value = e.currentTarget.value;
                                            let res = CheckInputValidity(value);
                                            if (res) setCreditLimit(value.replace(/\,/g, ""));
                                        }}
                                        register={register}
                                        value={creditLimit ? CommaFormatted(creditLimit) : ""}
                                        rules={{
                                            required: "This field is required.",
                                            validate: {
                                                validity: (value) =>
                                                    !(value === "0.00") ||
                                                    "Amount must be greater than 0",
                                            },
                                        }}
                                        errors={errors}
                                    />
                                </div>
                            )}
                        </div>
                    </div>

                </div>
                <div className="form-footer">
                    <button
                        className="btn btn-primary"
                        id="first_mortgage_save"
                        data-testid="first_mortgage_save"
                        // disabled={true}
                        onClick={handleSubmit(onSubmit)}>
                        {"Save and continue"}
                    </button>
                </div>
            </form>
        </div>
    );
};
