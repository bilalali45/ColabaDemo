import React, { ChangeEvent } from "react";
import { useForm } from "react-hook-form";

import InputCheckedBox from "../../../../../Shared/Components/InputCheckedBox";
import InputField from "../../../../../Shared/Components/InputField";
import InputRadioBox from "../../../../../Shared/Components/InputRadioBox";
import { AddressHomeIcon } from "../../../../../Shared/Components/SVGs";

import { CommaFormatted } from "../../../../../Utilities/helpers/CommaSeparteMasking";
import { PropertyMortgageSecondStepPostObj } from "../PropertyMortgageSecondStepDetails/PropertyMortgageSecondStepDetails";


type SecondCurrentResidenceMortgageDetailsProps = {
  secondPayment: string;
  setSecondPayment: Function;
  secondPaymentBalance: string;
  setSecondPaymentBalance: Function;
  isHELOC: boolean;
  setIsHELOC: Function;
  creditLimit: string;
  setCreditLimit: Function;
  onSave: Function;
  homeAddress: string;
  showPaidOff: boolean;
  isPaidOff: boolean | null;
  setIsPaidOff: Function;
};

export const SecondCurrentResidenceMortgageDetails = ({
  secondPayment,
  setSecondPayment,
  secondPaymentBalance,
  setSecondPaymentBalance,
  isHELOC,
  setIsHELOC,
  creditLimit,
  setCreditLimit,
  onSave,
  homeAddress,
  showPaidOff,
  isPaidOff,
  setIsPaidOff,
}: SecondCurrentResidenceMortgageDetailsProps) => {
  const { register, errors, handleSubmit, clearErrors } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const onSubmit = (data: PropertyMortgageSecondStepPostObj) => {
    onSave(data);
  };

  const CheckInputValidity = (value: string) => {
    if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
      return false;
    }
    return true;
  };

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

      <div className="row mb-10">
        <div className="col-md-6">
          <InputField
            label={"Second Mortgage Payment"}
            data-testid="second_Payment"
            id="second_Payment"
            name="second_Payment"
            icon={<i className="zmdi zmdi-money"></i>}
            type={"text"}
            placeholder={"Monthly Payment"}
            onChange={(e: ChangeEvent<HTMLInputElement>) => {
              clearErrors("second_Payment");
              const value = e.currentTarget.value;
              let res = CheckInputValidity(value);
              if (res) setSecondPayment(value.replace(/\,/g, ""));
            }}
            register={register}
            value={secondPayment ? CommaFormatted(secondPayment) : ""}
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
            label={"Unpaid Second Mortgage Balance"}
            data-testid="second_pay_bal"
            id="second_pay_bal"
            name="second_pay_bal"
            icon={<i className="zmdi zmdi-money"></i>}
            type={"text"}
            placeholder={"Mortgage Balance"}
            onChange={(e: ChangeEvent<HTMLInputElement>) => {
              clearErrors("second_pay_bal");
              const value = e.currentTarget.value;
              let res = CheckInputValidity(value);
              if (res) setSecondPaymentBalance(value.replace(/\,/g, ""));
            }}
            register={register}
            value={
              secondPaymentBalance ? CommaFormatted(secondPaymentBalance) : ""
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
      </div>
      
        
      <div className="Equityline noborder">
      {showPaidOff && (
          <div className="form-group">
            <div className="form-group">
              <h4>Will this mortgage be paid off at or before closing?</h4>
            </div>
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
                id="mortgage_no"
                data-testid="mortgage_no"
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
                checked={(isPaidOff === null || isPaidOff === undefined ) ? false : !isPaidOff ? true : false}
                value={"No"}>
                No
              </InputRadioBox>
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
        )}
        <div className="row form-group">
          <div className="col-md-6">
            <InputCheckedBox
              id="heloc"
              data-testid="heloc"
              className="nowrap-text"
              name="heloc"
              label={`This is a Home Equity Line of Credit (HELOC)`}
              checked={isHELOC === null ? false : isHELOC}
              value={isHELOC === null ? "" : isHELOC?.toString()}
              onChange={() => {setIsHELOC(!isHELOC); setCreditLimit("")}}
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
                  data-testid="credit_limit"
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
                        !(value === "0.00") || "Amount must be greater than 0",
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
          id="second_mortgage_save"
          data-testid="second_mortgage_save"
          // disabled={true}
          onClick={handleSubmit(onSubmit)}>
          {"Save and continue"}
        </button>
      </div>
    </div>
  );
};
