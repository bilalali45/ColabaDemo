import React, { ChangeEvent } from "react";
import { useForm } from "react-hook-form";

import InputField from "../../../../../Shared/Components/InputField";
import InputRadioBox from "../../../../../Shared/Components/InputRadioBox";
import { AddressHomeIcon } from "../../../../../Shared/Components/SVGs";

import { CommaFormatted } from "../../../../../Utilities/helpers/CommaSeparteMasking";
import { PropertyMortgageFirstStepAPIProps } from "../PropertyMortgageFirstStep/PropertyMortgageFirstStep";


type PropertyFirstMortgageFormProps = {
  propTax: string;
  setPropTax: Function;
  insurance: string;
  setInsurance: Function;
  haveMortgage: boolean | null;
  setHaveMortgage: Function;
  onSave: Function;
  homeAddress: string;
  setFloodIns:Function;
  floodIns:string;
};
export const PropertyFirstMortgageForm = ({
  propTax,
  setPropTax,
  insurance,
  setInsurance,
  haveMortgage,
  setHaveMortgage,
  onSave,
  homeAddress,
  setFloodIns,
  floodIns
}: PropertyFirstMortgageFormProps) => {
  const { register, errors, handleSubmit, clearErrors } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const onSubmit = (data: PropertyMortgageFirstStepAPIProps) => {
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
      <form
        id="has-first-mortgage-form"
        data-testid="has-first-mortgage-form"
        autoComplete="off">
        <div className="row form-group">
          <div className="col-sm-12">
            <h4>Do you currently have any mortgages against this property?</h4>
          </div>
          <div className="col-md-6">
            <InputRadioBox
              id="mortgage_yes"
              data-testid="mortgage_yes"
              className=""
              name="have_mortgage"
              register={register}
              rules={{
                required: "Please select one",
              }}
              errors={errors}
              onChange={() => {
                clearErrors("have_mortgate");
                setHaveMortgage(true);
              }}
              checked={haveMortgage && haveMortgage ? true : false}
              value={"Yes"}>
              Yes
            </InputRadioBox>
          </div>

          <div className="col-md-12">
            <InputRadioBox
              id="mortgage_no"
              data-testid="mortgage_no"
              className=""
              name="have_mortgage"
              register={register}
              rules={{
                required: "Please select one",
              }}
              errors={errors}
              onChange={() => {
                clearErrors("have_mortgate");
                setHaveMortgage(false);
              }}
              checked={
                haveMortgage === null ? false : !haveMortgage ? true : false
              }
              value={"No"}>
              No
            </InputRadioBox>
          </div>
          {errors?.have_mortgage && (
            <span
              className="form-error no-padding"
              role="alert"
              data-testid="have_mortgage-error">
              {errors?.have_mortgage?.message}
            </span>
          )}
        </div>

        {haveMortgage !== null && !haveMortgage && (
          <div className="row form-group">
            <div className="col-md-6">
              <InputField
                label={"Property Taxes"}
                data-testid="prop_tax"
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
            </div>
            <div className="col-md-6">
              <InputField
                label={"Homeowner’s Insurance"}
                data-testid="insurance"
                id="insurance"
                name="insurance"
                icon={<i className="zmdi zmdi-money"></i>}
                type={"text"}
                placeholder={"Estimated Annual Premium"}
                register={register}
                value={insurance ? CommaFormatted(insurance) : ""}
                rules={{
                  required: "This field is required.",
                  validate: {
                    validity: (value) =>
                      !(value === "0.00") || "Amount must be greater than 0",
                  },
                }}
                errors={errors}
                onChange={(e: ChangeEvent<HTMLInputElement>) => {
                  clearErrors("insurance");
                  const value = e.currentTarget.value;
                let res = CheckInputValidity(value);
                if (res) setInsurance(value.replace(/\,/g, ""));
                }}
              />
            </div>

            <div className="col-md-6">
              <InputField
                label={"Flood Insurance"}
                data-testid="flood_insurance"
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
            </div>

          </div>
        )}

        <div className="form-footer">
          <button
            className="btn btn-primary"
            id="have_mortgage_save"
            data-testid="have_mortgage_save"
            // disabled={true}
            onClick={handleSubmit(onSubmit)}>
            {"Save and continue"}
          </button>
        </div>
      </form>
    </div>
  );
};
